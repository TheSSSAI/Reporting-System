using System.Text.Json.Serialization;

namespace ReportingSystem.Shared.Contracts.Enums
{
    /// <summary>
    /// Defines the possible states of a report generation job.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JobStatus
    {
        /// <summary>
        /// Specifies that the job has been created and is waiting to be processed.
        /// </summary>
        Queued = 0,

        /// <summary>
        /// Specifies that the job is currently being processed.
        /// </summary>
        Running = 1,

        /// <summary>
        /// Specifies that the job completed successfully.
        /// </summary>
        Succeeded = 2,

        /// <summary>
        /// Specifies that the job failed during processing.
        /// </summary>
        Failed = 3,

        /// <summary>
        /// Specifies that the job was manually cancelled before completion.
        /// </summary>
        Cancelled = 4
    }
}