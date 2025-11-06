using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using ReportingSystem.Application.Common.Interfaces;
using ReportingSystem.Domain.Entities;
using ReportingSystem.Infrastructure.Persistence.Configurations;
using System.Reflection;

namespace ReportingSystem.Infrastructure.Persistence
{
    /// <summary>
    /// Represents the application's database context, serving as the primary interface
    /// for interacting with the PostgreSQL database via Entity Framework Core.
    /// It encapsulates the database schema and acts as a Unit of Work.
    /// </summary>
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        /// <param name="dataProtectionProvider">The provider for data protection services, used for encryption.</param>
        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IDataProtectionProvider dataProtectionProvider)
            : base(options)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        // Core Domain Entities
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<TransformationScript> TransformationScripts { get; set; } = null!;
        public DbSet<TransformationScriptVersion> TransformationScriptVersions { get; set; } = null!;
        public DbSet<ReportConfiguration> ReportConfigurations { get; set; } = null!;
        public DbSet<JsonSchema> JsonSchemas { get; set; } = null!;

        // Logging and Auditing Entities
        public DbSet<JobExecutionLog> JobExecutionLogs { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<SecurityViolationLog> SecurityViolationLogs { get; set; } = null!;

        // Configuration Entities
        public DbSet<ApplicationConfiguration> ApplicationConfigurations { get; set; } = null!;

        /// <summary>
        /// Overrides the default model creation logic to apply custom entity configurations.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This is the key integration point for REQ-SEC-DTR-003.
            // We manually apply the configuration that requires a dependency (IDataProtectionProvider).
            // All other configurations can be discovered and applied automatically.
            modelBuilder.ApplyConfiguration(new TransformationScriptVersionConfiguration(_dataProtectionProvider));

            // Apply all other IEntityTypeConfiguration classes from this assembly.
            // This makes the DbContext cleaner and keeps configuration logic with the entity.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
                // Exclude the configuration we applied manually to avoid applying it twice.
                t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                    i.GetGenericArguments()[0] != typeof(TransformationScriptVersion)));

            // Manually configure many-to-many relationship for User and Role
            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });

                b.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });
            
            // Seed initial roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Administrator" },
                new Role { RoleId = 2, Name = "Viewer" }
            );

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Here you could add logic to automatically update timestamps, etc.
            // For now, we just call the base implementation.
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}