using System;
using System.Security.Cryptography;
using System.Text;
using ReportingSystem.Shared.Common.Validation;

namespace ReportingSystem.Shared.Common.Security
{
    /// <summary>
    /// Provides common, reusable cryptographic functions.
    /// This class uses industry-standard implementations from the .NET framework and does not implement custom algorithms.
    /// </summary>
    public static class CryptographyHelpers
    {
        // Optional: A static entropy byte array can add an extra layer of security for DPAPI, 
        // but it must be stored and managed securely. For simplicity and broad applicability within a service context,
        // we will omit it here, relying on the LocalMachine scope, which is standard for service-level encryption.
        // private static readonly byte[] s_entropy = { 9, 8, 7, 6, 5 };

        /// <summary>
        /// Computes the SHA256 hash of a given string.
        /// </summary>
        /// <param name="rawData">The plaintext data to be hashed. Must not be null.</param>
        /// <returns>A lowercase hexadecimal string representation of the SHA256 hash.</returns>
        /// <exception cref="ArgumentNullException">Thrown if rawData is null.</exception>
        public static string ComputeSha256Hash(string rawData)
        {
            Guard.AgainstNull(rawData, nameof(rawData));

            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Encrypts a string using the Windows Data Protection API (DPAPI) with the local machine scope.
        /// This is suitable for encrypting data that will only be decrypted by the same application on the same machine.
        /// Fulfills part of REQ-SEC-DTR-003.
        /// </summary>
        /// <param name="plainText">The string to encrypt. Must not be null.</param>
        /// <returns>A Base64 encoded string representing the encrypted data.</returns>
        /// <exception cref="ArgumentNullException">Thrown if plainText is null.</exception>
        /// <exception cref="CryptographicException">Thrown if the encryption fails.</exception>
        public static string Encrypt(string plainText)
        {
            Guard.AgainstNull(plainText, nameof(plainText));
            
            var data = Encoding.UTF8.GetBytes(plainText);
            
            // The scope is set to LocalMachine, meaning the data can be decrypted by any process
            // running on the same machine, but not on other machines.
            var encryptedData = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
            
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// Decrypts a string that was encrypted using the Windows Data Protection API (DPAPI) with the local machine scope.
        /// </summary>
        /// <param name="encryptedText">The Base64 encoded string to decrypt. Must not be null or whitespace.</param>
        /// <returns>The original plaintext string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if encryptedText is null or whitespace.</exception>
        /// <exception cref="CryptographicException">Thrown if the decryption fails (e.g., data is corrupt or from another machine).</exception>
        /// <exception cref="FormatException">Thrown if the encryptedText is not a valid Base64 string.</exception>
        public static string Decrypt(string encryptedText)
        {
            Guard.AgainstNullOrWhiteSpace(encryptedText, nameof(encryptedText));

            var encryptedData = Convert.FromBase64String(encryptedText);
            
            var decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.LocalMachine);
            
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}