using System;
using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Transformations
{
    /// <summary>
    /// Specifies the request body for the transformation script preview endpoint, as required by US-046, US-047 and REQ-INTG-DTR-001.
    /// </summary>
    /// <param name="ScriptContent">The JavaScript content to execute.</param>
    /// <param name="SampleDataJson">User-provided sample JSON. One of 'SampleDataJson' or 'ConnectorId' must be provided.</param>
    /// <param name="ConnectorId">The ID of a connector to fetch live sample data from.</param>
    public record TransformationPreviewRequest(
        [Required] string ScriptContent,
        string? SampleDataJson,
        Guid? ConnectorId);
}