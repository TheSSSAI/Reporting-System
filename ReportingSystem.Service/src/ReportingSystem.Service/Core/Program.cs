using System.Diagnostics;
using ReportingSystem.Infrastructure;
using ReportingSystem.Service.Application;
using ReportingSystem.Service.Core;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

// Configure bootstrap logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

// It is recommended to use a unique name for the service
// to avoid conflicts, especially in environments with many services.
// The service name should be descriptive and preferably namespaced.
const string serviceName = "ReportingSystemBackendService";

try
{
    Log.Information("Starting {ServiceName} host", serviceName);

    var builder = WebApplication.CreateBuilder(args);

    // --- Host Configuration ---
    // Configure to run as a Windows Service. This is platform-specific.
    // In a cross-platform environment, this would be conditional.
    builder.Host.UseWindowsService(options =>
    {
        options.ServiceName = serviceName;
    });

    // --- Logging Configuration ---
    // Replace default logger with Serilog, configured from appsettings.json
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", serviceName)
        // In production, you would typically write to a file (rolling file)
        // or a log aggregation service (e.g., Seq, Splunk, ELK stack).
        .WriteTo.Console(new RenderedCompactJsonFormatter())
        .WriteTo.Debug());
        
    // --- Dependency Injection Configuration ---
    // This is the composition root. We call extension methods from each layer
    // to register their services, adhering to Clean Architecture principles.
    
    // Register services from the Core layer (API, Application)
    // This includes controllers, MediatR, FluentValidation, etc.
    builder.Services.AddCoreServices(builder.Configuration);

    // Register services from the Application layer (CQRS handlers, behaviors, etc.)
    // Note: AddCoreServices already calls AddApplicationServices. Kept for clarity.
    // builder.Services.AddApplicationServices(builder.Configuration);

    // Register services from the Infrastructure layer (Repositories, External Services, etc.)
    builder.Services.AddInfrastructureServices(builder.Configuration);

    var app = builder.Build();

    // --- HTTP Request Pipeline Configuration ---
    // The order of middleware is critical for security, performance, and functionality.

    // Use custom global exception handler as the first middleware
    // to catch all subsequent exceptions.
    app.UseInfrastructureExceptionHandling();
    
    if (app.Environment.IsDevelopment())
    {
        // In development, use Swagger for API documentation and testing.
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reporting System API v1");
            c.RoutePrefix = "swagger"; // Access at /swagger
        });
    }
    else
    {
        // In production, enforce HTTPS and use HSTS for security.
        app.UseHsts();
    }

    // Redirect HTTP requests to HTTPS.
    app.UseHttpsRedirection();

    // Serve static files (for the React frontend).
    app.UseStaticFiles();

    // Add routing capabilities to the pipeline.
    app.UseRouting();
    
    // Prometheus metrics endpoint
    app.MapMetrics();

    // Authentication middleware must come before Authorization.
    app.UseAuthentication();
    app.UseAuthorization();

    // Map controller endpoints.
    app.MapControllers();

    // Fallback for Single Page Application (SPA) routing.
    // Any request that doesn't match an API endpoint will be served the index.html.
    app.MapFallbackToFile("index.html");

    // --- Database Migration ---
    // Automatically apply pending EF Core migrations on startup.
    // This is a common pattern for simplifying deployment.
    Log.Information("Applying database migrations...");
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
    Log.Information("Database migrations applied successfully.");

    // --- Run Application ---
    await app.RunAsync();

    return 0;
}
catch (Exception ex)
{
    // Log any unhandled exception that occurs during startup.
    // This is the final safety net.
    Log.Fatal(ex, "{ServiceName} host terminated unexpectedly.", serviceName);
    
    // For services, it's good practice to provide an exit code.
    // This can be monitored by service management tools.
    return 1;
}
finally
{
    // Ensure all buffered logs are written to sinks before the application closes.
    Log.CloseAndFlush();
}

/// <summary>
/// Partial Program class for WebApplicationFactory in integration tests.
/// Do not add any code here. This is a marker for the test project.
/// </summary>
public partial class Program { }