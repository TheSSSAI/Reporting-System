using System;
using System.Collections.Generic;

namespace ReportingSystem.Shared.Contracts.Reports
{
    /// <summary>
    /// Specifies the public representation of a complete report configuration.
    /// This DTO represents a flattened view of the domain model, suitable for public consumption.
    /// </summary>
    /// <param name="Id">The unique identifier for the report configuration.</param>
    /// <param name="Name">The user-defined name of the report.</param>
    /// <param name="Description">An optional description of the report's purpose.</param>
    /// <param name="ConnectorId">The ID of the data connector used as the data source.</param>
    /// <param name="TransformationScriptId">The optional ID of the transformation script to apply to the data.</param>
    /// <param name="Schedule">The CRON expression for the report's schedule, if any.</param>
    /// <param name="OutputFormat">The final output format of the report (e.g., 'PDF', 'CSV').</param>
    /// <param name="TemplateId">The optional ID of the template to use for rendering (for HTML/PDF).</param>
    /// <param name="DeliveryDestinations">A list of configured delivery destinations.</param>
    public record ReportConfigurationDto(
        Guid Id,
        string Name,
        string? Description,
        Guid ConnectorId,
        Guid? TransformationScriptId,
        string? Schedule,
        string OutputFormat,
        Guid? TemplateId,
        IReadOnlyList<object> DeliveryDestinations); // Using 'object' for now to represent various destination types
}