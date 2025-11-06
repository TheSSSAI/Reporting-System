using System;
using System.Text.Json.Serialization;

namespace ReportingSystem.Shared.Contracts.Reports
{
    /// <summary>
    /// Specifies the response contract for successfully initiating an asynchronous job, as per US-094.
    /// This is an alias for the JobInitiationResultDto mentioned in the SDS.
    /// </summary>
    /// <param name="JobId">The unique identifier for the queued job.</param>
    /// <param name="StatusUrl">The URL to poll for the job's status.</param>
    public record AsyncJobSubmissionResponse(
        [property: JsonPropertyName("jobId")] Guid JobId,
        [property: JsonPropertyName("statusUrl")] string StatusUrl);
}