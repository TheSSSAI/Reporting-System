using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportingSystem.Application.Abstractions;
using ReportingSystem.Application.Abstractions.Persistence;
using ReportingSystem.Application.Abstractions.Services;
using ReportingSystem.Infrastructure.Caching;
using ReportingSystem.Infrastructure.Persistence;
using ReportingSystem.Infrastructure.Persistence.Repositories;
using ReportingSystem.Infrastructure.Scripting;
using ReportingSystem.Infrastructure.Services;

namespace ReportingSystem.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all services defined in the Infrastructure layer to the dependency injection container.
    /// This is the composition root for the entire Infrastructure project.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="configuration">The application configuration for retrieving settings like connection strings.</param>
    /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataProtection();
        
        AddPersistence(services, configuration);
        AddScriptingEngine(services, configuration);
        AddCaching(services, configuration);
        AddExternalServices(services);
        
        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Database connection string 'DefaultConnection' not found in configuration.");
        }

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions => 
                npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        
        // Registering repositories with a Scoped lifetime, aligned with the DbContext's lifetime.
        services.AddScoped<IScriptRepository, EfCoreScriptRepository>();
        services.AddScoped<IAuditLogRepository, EfCoreAuditLogRepository>();
        // Add other repositories here as they are created
        // e.g., services.AddScoped<IReportConfigurationRepository, EfCoreReportConfigurationRepository>();
    }

    private static void AddScriptingEngine(IServiceCollection services, IConfiguration configuration)
    {
        // Configure Jint options from the "JintEngine" section of appsettings.json
        // This fulfills REQ-SEC-DTR-001 for configurability.
        services.Configure<JintEngineOptions>(configuration.GetSection("JintEngine"));

        // JintTransformationEngine is registered as Scoped to ensure a fresh, isolated
        // engine instance for each request/operation, which is crucial for security and thread safety.
        services.AddScoped<ITransformationEngine, JintTransformationEngine>();
    }

    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis");
        if (string.IsNullOrWhiteSpace(redisConnectionString))
        {
            // Caching is optional, so we can fall back to in-memory or no-op cache if not configured.
            // For now, we'll make it explicit. A more advanced setup might have a fallback.
            throw new InvalidOperationException("Redis connection string 'Redis' not found in configuration.");
        }

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "ReportingSystem_";
        });
        
        // Register our custom cache service wrapper as a Singleton as the underlying client is thread-safe.
        services.AddSingleton<ICacheService, RedisCacheService>();
    }
    
    private static void AddExternalServices(IServiceCollection services)
    {
        // Register other infrastructure services
        
        // PDF Generator - Scoped as it may manage resources for a single generation operation.
        services.AddScoped<IPdfGenerator, PuppeteerPdfGenerator>();
        
        // System Clock - Singleton as it is stateless.
        // It's a best practice to abstract away DateTime.UtcNow for testability.
        services.AddSingleton<IClock, SystemClock>();

        // Polly resiliency policies are typically configured in the ServiceHost
        // when setting up HttpClientFactory, so they are omitted here.
        
        // Serilog logging is also configured at the Host level.
    }
}