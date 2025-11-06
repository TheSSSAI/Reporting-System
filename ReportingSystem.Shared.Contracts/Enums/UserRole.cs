using System.Text.Json.Serialization;

namespace ReportingSystem.Shared.Contracts.Enums
{
    /// <summary>
    /// Defines the access control roles within the system.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        /// <summary>
        /// Specifies a user with full system access.
        /// </summary>
        Administrator = 0,

        /// <summary>
        /// Specifies a user with read-only access to permitted reports.
        /// </summary>
        Viewer = 1
    }
}