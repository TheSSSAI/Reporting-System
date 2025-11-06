namespace ReportingSystem.Core.Domain.Enums;

/// <summary>
/// Specifies the final file format for a generated report.
/// This corresponds to the mandatory selection required by US-055.
/// </summary>
public enum OutputFormat
{
    /// <summary>
    /// HyperText Markup Language format, viewable in a web browser. Requires a Handlebars template.
    /// </summary>
    HTML = 0,

    /// <summary>
    /// Portable Document Format, for printable documents. Requires a Handlebars template.
    /// </summary>
    PDF,

    /// <summary>
    /// JavaScript Object Notation format, for machine-readable data exchange.
    /// </summary>
    JSON,

    /// <summary>
    /// Comma-Separated Values format, for spreadsheet applications.
    /// </summary>
    CSV,

    /// <summary>
    /// Plain Text format, for simple text-based output.
    /// </summary>
    TXT
}