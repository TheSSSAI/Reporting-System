using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Spi;
using ReportingSystem.Service.Api.Middleware;
using ReportingSystem.Service.Application.Common.Behaviors;
using ReportingSystem.Service.Core.Jobs;

namespace ReportingSystem.Service.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();

        services.AddScoped<ExceptionHandlingMiddleware>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });

        // Configure JWT Authentication
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];

        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT SecretKey is not configured.");
        }

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        services.AddAuthorization();

        // Configure Swagger/OpenAPI
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Reporting System API", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(Application.Common.Interfaces.IApplicationDbContext).Assembly;
        
        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(applicationAssembly);

            // Register MediatR pipeline behaviors
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        return services;
    }

    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Quartz.NET services
        services.AddSingleton<IJobFactory, DependencyInjectionJobFactory>();
        services.AddSingleton<ReportGenerationJob>();

        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            // Configure persistent job store
            q.UsePersistentStore(s =>
            {
                s.UseProperties = true;
                s.SetProperty("quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");
                s.SetProperty("quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz");
                s.SetProperty("quartz.jobStore.tablePrefix", "QRTZ_");
                s.SetProperty("quartz.jobStore.dataSource", "default");
                s.SetProperty("quartz.dataSource.default.provider", "Sqlite"); // Using the specified provider
                s.SetProperty("quartz.dataSource.default.connectionString", configuration.GetConnectionString("DefaultConnection"));
                s.UseClustering(c =>
                {
                    c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
                    c.CheckinInterval = TimeSpan.FromSeconds(10);
                });
            });

            // Register jobs
            var jobKey = new JobKey(nameof(ReportGenerationJob));
            q.AddJob<ReportGenerationJob>(opts => opts.WithIdentity(jobKey).StoreDurably());
        });

        // Add the Quartz.NET hosted service, which starts the scheduler
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}

/// <summary>
/// Custom Job Factory to enable Dependency Injection in Quartz Jobs.
/// </summary>
internal class DependencyInjectionJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DependencyInjectionJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        try
        {
            // Using a scope to resolve jobs ensures that scoped services like DbContext are handled correctly.
            var scope = _serviceProvider.CreateScope();
            var job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
            
            // This is a bit of a hack to manage the scope's lifetime along with the job's lifetime.
            // A more complex implementation might use a different approach, but this is effective for simple scenarios.
            if (job is IDisposable disposable)
            {
                // This will dispose the scope when the job is disposed.
                return new ScopedJob(scope, disposable);
            }
            
            return job!;
        }
        catch (Exception e)
        {
            throw new SchedulerException($"Problem instantiating job '{bundle.JobDetail.JobType.FullName}'", e);
        }
    }

    public void ReturnJob(IJob job)
    {
        (job as IDisposable)?.Dispose();
    }
    
    // Wrapper to manage the DI scope lifetime
    private class ScopedJob : IJob, IDisposable
    {
        private readonly IServiceScope _scope;
        private readonly IDisposable _job;

        public ScopedJob(IServiceScope scope, IDisposable job)
        {
            _scope = scope;
            _job = job;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return ((IJob)_job).Execute(context);
        }

        public void Dispose()
        {
            _job.Dispose();
            _scope.Dispose();
        }
    }
}