using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportingSystem.Domain.Entities;
using ReportingSystem.Infrastructure.Persistence.ValueConverters;
using System.Security.Cryptography;

namespace ReportingSystem.Infrastructure.Persistence.Configurations
{
    public class TransformationScriptVersionConfiguration : IEntityTypeConfiguration<TransformationScriptVersion>
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public TransformationScriptVersionConfiguration(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public void Configure(EntityTypeBuilder<TransformationScriptVersion> builder)
        {
            builder.ToTable("TransformationScriptVersions");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedOnAdd();

            builder.Property(v => v.TransformationScriptId)
                .IsRequired();

            builder.Property(v => v.VersionNumber)
                .IsRequired();

            // This is the critical configuration for REQ-SEC-DTR-003.
            // It uses a custom Value Converter to encrypt/decrypt the script content.
            // A "purpose" string is used to isolate the data protector, enhancing security.
            builder.Property(v => v.Content)
                .IsRequired()
                .HasConversion(new EncryptedStringValueConverter(_dataProtectionProvider, "TransformationScriptContent"));

            builder.Property(v => v.Created)
                .IsRequired();

            builder.Property(v => v.CreatedBy)
                .HasMaxLength(256);

            // Configure the relationship to the parent TransformationScript
            builder.HasOne<TransformationScript>()
                .WithMany(s => s.Versions)
                .HasForeignKey(v => v.TransformationScriptId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting a script deletes all its versions.

            // Ensure VersionNumber is unique within the scope of a single TransformationScript
            builder.HasIndex(v => new { v.TransformationScriptId, v.VersionNumber })
                .IsUnique();
        }
    }
}