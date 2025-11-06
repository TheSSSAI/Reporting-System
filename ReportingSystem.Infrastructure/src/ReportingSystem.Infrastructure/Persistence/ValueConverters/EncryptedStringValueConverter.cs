using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography;

namespace ReportingSystem.Infrastructure.Persistence.ValueConverters
{
    /// <summary>
    /// Implements an EF Core Value Converter to transparently encrypt and decrypt string properties at rest.
    /// This is a critical component for fulfilling the security requirement REQ-SEC-DTR-003.
    /// It uses the .NET Data Protection APIs for secure key management and cryptographic operations.
    /// </summary>
    public class EncryptedStringValueConverter : ValueConverter<string, string>
    {
        private readonly IDataProtector _protector;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptedStringValueConverter"/> class.
        /// </summary>
        /// <param name="provider">The IDataProtectionProvider used to create a protector for a specific purpose.</param>
        /// <param name="purpose">A purpose string to isolate protected payloads. Payloads protected with one purpose cannot be unprotected with another.</param>
        public EncryptedStringValueConverter(IDataProtectionProvider provider, string purpose)
            : base(
                // Encryption function: Model -> Provider (Database)
                // Takes a plaintext string and returns a protected (encrypted and signed) string.
                v => Encrypt(v, CreateProtector(provider, purpose)),
                // Decryption function: Provider (Database) -> Model
                // Takes a protected string and returns the original plaintext.
                v => Decrypt(v, CreateProtector(provider, purpose))
            )
        {
            _protector = CreateProtector(provider, purpose);
        }

        private static IDataProtector CreateProtector(IDataProtectionProvider provider, string purpose)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider), "A valid IDataProtectionProvider is required for encryption.");
            }
            if (string.IsNullOrWhiteSpace(purpose))
            {
                throw new ArgumentException("A non-empty purpose string is required to create a data protector.", nameof(purpose));
            }
            return provider.CreateProtector(purpose);
        }

        private static string Encrypt(string value, IDataProtector protector)
        {
            // Do not encrypt null or empty strings. This is an optimization and prevents
            // storing a protected value for what is essentially an empty field.
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return protector.Protect(value);
        }

        private static string Decrypt(string value, IDataProtector protector)
        {
            // Do not attempt to decrypt null or empty strings.
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            try
            {
                // Unprotect will throw a CryptographicException if the payload has been tampered with,
                // the key is incorrect, or the data is otherwise corrupted. We intentionally let this
                // exception propagate up to the DbContext level, as it indicates a critical data
                // integrity issue that should fail the entire data operation.
                return protector.Unprotect(value);
            }
            catch (CryptographicException ex)
            {
                // In a real-world scenario with high security, you might want to log this failure
                // to a security-specific log sink to detect tampering attempts.
                // For now, we re-throw a more informative exception to aid in debugging.
                throw new CryptographicException("Failed to decrypt data. The data may have been tampered with or the encryption key has changed.", ex);
            }
        }
    }
}