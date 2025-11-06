using ReportingSystem.Core.Domain.Common;
using ReportingSystem.Core.Domain.Entities;
using ReportingSystem.Core.Domain.Enums;
using ReportingSystem.Core.Domain.Exceptions;

namespace ReportingSystem.Core.Domain.Aggregates.ReportConfigurationAggregate;

/// <summary>
/// Represents the central aggregate root for a report configuration.
/// It encapsulates all settings required to generate and deliver a report,
/// acting as the consistency boundary for all report-related operations.
/// </summary>
public class ReportConfiguration : AggregateRoot<Guid>
{
    private readonly List<DeliveryDestination> _deliveryDestinations = [];

    /// <summary>
    /// The unique, user-defined name of the report configuration.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// An optional, detailed description of the report's purpose.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// The ID of the ConnectorConfiguration that provides the data for this report.
    /// </summary>
    public Guid ConnectorConfigurationId { get; private set; }
    
    /// <summary>
    /// The optional ID of the active TransformationScriptVersion used to process the data.
    /// </summary>
    public Guid? TransformationScriptId { get; private set; }
    
    /// <summary>
    /// The optional ID of the Template used for rendering HTML/PDF reports.
    /// </summary>
    public Guid? TemplateId { get; private set; }
    
    /// <summary>
    /// The optional ID of the JsonSchema used for validating the output of a transformation.
    /// </summary>
    public Guid? JsonSchemaId { get; private set; }

    /// <summary>
    /// The final file format of the generated report.
    /// </summary>
    public OutputFormat OutputFormat { get; private set; }

    /// <summary>
    /// The scheduling information for this report.
    /// </summary>
    public ReportSchedule Schedule { get; private set; }
    
    /// <summary>
    /// A read-only collection of delivery destinations for the report.
    /// </summary>
    public IReadOnlyCollection<DeliveryDestination> DeliveryDestinations => _deliveryDestinations.AsReadOnly();

    /// <summary>
    /// The ID of the user who created this configuration.
    /// </summary>
    public Guid CreatedByUserId { get; private set; }

    /// <summary>
    /// The ID of the user who last updated this configuration.
    /// </summary>
    public Guid? UpdatedByUserId { get; private set; }

    /// <summary>
    /// The timestamp when the configuration was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; private set; }

    /// <summary>
    /// The timestamp of the last update to the configuration.
    /// </summary>
    public DateTime? UpdatedAtUtc { get; private set; }

    // Private constructor for EF Core
    private ReportConfiguration() : base(Guid.NewGuid()) { }

    /// <summary>
    /// Creates a new ReportConfiguration.
    /// </summary>
    /// <param name="id">The unique identifier for the report configuration.</param>
    /// <param name="name">The name of the report configuration.</param>
    /// <param name="connectorConfigurationId">The ID of the data connector.</param>
    /// <param name="createdByUserId">The ID of the user creating the configuration.</param>
    /// <param name="description">An optional description.</param>
    public ReportConfiguration(Guid id, string name, Guid connectorConfigurationId, Guid createdByUserId, string? description = null) : base(id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleValidationException("Report name cannot be empty.");
        if (connectorConfigurationId == Guid.Empty)
            throw new BusinessRuleValidationException("A valid data connector must be selected.");

        Name = name;
        ConnectorConfigurationId = connectorConfigurationId;
        Description = description;
        CreatedByUserId = createdByUserId;
        CreatedAtUtc = DateTime.UtcNow;
        Schedule = ReportSchedule.CreateDisabled();
        OutputFormat = OutputFormat.CSV; // Default format
    }

    /// <summary>
    /// Updates the metadata of the report configuration.
    /// </summary>
    /// <param name="name">The new name for the report.</param>
    /// <param name="description">The new optional description.</param>
    /// <param name="updatedByUserId">The ID of the user performing the update.</param>
    public void UpdateMetadata(string name, string? description, Guid updatedByUserId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleValidationException("Report name cannot be empty.");

        Name = name;
        Description = description;
        SetUpdateAudit(updatedByUserId);
    }
    
    /// <summary>
    /// Sets or clears the transformation script for the report.
    /// </summary>
    /// <param name="transformationScriptId">The ID of the transformation script, or null to clear.</param>
    /// <param name="updatedByUserId">The ID of the user performing the update.</param>
    public void SetTransformation(Guid? transformationScriptId, Guid updatedByUserId)
    {
        TransformationScriptId = transformationScriptId;
        SetUpdateAudit(updatedByUserId);
    }
    
    /// <summary>
    /// Sets the output configuration, enforcing rules related to templates.
    /// </summary>
    /// <param name="outputFormat">The report's output format.</param>
    /// <param name="templateId">The template ID, required for HTML/PDF formats.</param>
    /// <param name="updatedByUserId">The ID of the user performing the update.</param>
    public void SetOutputConfiguration(OutputFormat outputFormat, Guid? templateId, Guid updatedByUserId)
    {
        if ((outputFormat == OutputFormat.HTML || outputFormat == OutputFormat.PDF) && !templateId.HasValue)
        {
            throw new BusinessRuleValidationException("A template is required for HTML and PDF output formats.");
        }
        
        OutputFormat = outputFormat;
        TemplateId = (outputFormat == OutputFormat.HTML || outputFormat == OutputFormat.PDF) ? templateId : null;
        SetUpdateAudit(updatedByUserId);
    }

    /// <summary>
    /// Sets or clears the JSON schema for output validation.
    /// </summary>
    /// <param name="jsonSchemaId">The ID of the JSON schema, or null to clear.</param>
    /// <param name="updatedByUserId">The ID of the user performing the update.</param>
    public void SetJsonSchema(Guid? jsonSchemaId, Guid updatedByUserId)
    {
        JsonSchemaId = jsonSchemaId;
        SetUpdateAudit(updatedByUserId);
    }

    /// <summary>
    /// Updates the report's execution schedule.
    /// </summary>
    /// <param name="newSchedule">The new schedule information.</param>
    /// <param name="updatedByUserId">The ID of the user performing the update.</param>
    public void UpdateSchedule(ReportSchedule newSchedule, Guid updatedByUserId)
    {
        if (newSchedule.IsEnabled && !_deliveryDestinations.Any())
        {
            throw new BusinessRuleValidationException("A scheduled report must have at least one delivery destination.");
        }
        Schedule = newSchedule;
        SetUpdateAudit(updatedByUserId);
    }

    /// <summary>
    /// Adds a new delivery destination to the report.
    /// </summary>
    /// <param name="destination">The delivery destination to add.</param>
    /// <param name="updatedByUserId">The ID of the user performing the update.</param>
    public void AddDeliveryDestination(DeliveryDestination destination, Guid updatedByUserId)
    {
        if (_deliveryDestinations.Any(d => d.Equals(destination)))
        {
            throw new BusinessRuleValidationException("This delivery destination has already been added.");
        }
        _deliveryDestinations.Add(destination);
        SetUpdateAudit(updatedByUserId);
    }

    /// <summary>
    /// Removes a delivery destination from the report.
    /// </summary>
    /// <param name="destinationId">The ID of the destination to remove.</param>
    /// <param name="updatedByUserId">The ID of the user performing the update.</param>
    public void RemoveDeliveryDestination(Guid destinationId, Guid updatedByUserId)
    {
        var destinationToRemove = _deliveryDestinations.FirstOrDefault(d => d.Id == destinationId);
        if (destinationToRemove is null)
        {
            throw new EntityNotFoundException($"Delivery destination with ID '{destinationId}' not found on this report.");
        }
        
        if (Schedule.IsEnabled && _deliveryDestinations.Count == 1)
        {
            throw new BusinessRuleValidationException("Cannot remove the last delivery destination from a scheduled report. Disable the schedule first.");
        }

        _deliveryDestinations.Remove(destinationToRemove);
        SetUpdateAudit(updatedByUserId);
    }

    /// <summary>
    /// Clears all delivery destinations from the report.
    /// </summary>
    /// <param name="updatedByUserId">The ID of the user performing the update.</param>
    public void ClearDeliveryDestinations(Guid updatedByUserId)
    {
        if (Schedule.IsEnabled)
        {
            throw new BusinessRuleValidationException("Cannot clear all delivery destinations from a scheduled report. Disable the schedule first.");
        }
        _deliveryDestinations.Clear();
        SetUpdateAudit(updatedByUserId);
    }

    private void SetUpdateAudit(Guid updatedByUserId)
    {
        UpdatedByUserId = updatedByUserId;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}