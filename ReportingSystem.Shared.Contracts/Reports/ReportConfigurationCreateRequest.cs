using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Reports
{
    /// <summary>
    /// Specifies the data required to create a new report configuration.
    /// </summary>
    /// <param name="Name">The name of the report.</param>
    /// <param name="Description">An optional description for the report.</param>
    /// <param name="ConnectorId">The ID of the data connector to use.</param>
    /// <param name="TransformationScriptId">The optional ID of the transformation script.</param>
    /// <param name="Schedule">The optional CRON schedule for the report.</param>
    /// <param name="OutputFormat">The output format (e.g., 'PDF', 'CSV').</param>
    /// <param name="TemplateId">The optional template ID (required for HTML/PDF).</param>
    /// <param name="DeliveryDestinations">A list of delivery destination configurations.</param>
    public record ReportConfigurationCreateRequest(
        [Required]
        [StringLength(100)]
        string Name,

        [StringLength(500)]
        string? Description,

        [Required]
        Guid ConnectorId,

        Guid? TransformationScriptId,
        string? Schedule,

        [Required]
        string OutputFormat,

        Guid? TemplateId,

        [Required]
        [MinLength(1)]
        IReadOnlyList<object> DeliveryDestinations); // Placeholder for specific destination configs
}