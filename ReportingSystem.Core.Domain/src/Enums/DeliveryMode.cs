namespace ReportingSystem.Core.Domain.Enums;

/// <summary>
/// Specifies the delivery method for a generated report.
/// This enum corresponds to requirements for various delivery destinations
/// like US-064 (Email), US-063 (Cloud Storage), US-066 (FTP/SFTP),
/// US-065 (Local/Network), and US-067 (REST API).
/// </summary>
public enum DeliveryMode
{
    /// <summary>
    /// No delivery destination is specified. The report is only stored locally.
    /// </summary>
    None = 0,

    /// <summary>
    /// Delivery via email using SMTP.
    /// </summary>
    Email,

    /// <summary>
    /// Delivery to a cloud storage provider like Amazon S3 or Azure Blob Storage.
    /// </summary>
    CloudStorage,

    /// <summary>
    /// Delivery to a server using FTP or SFTP.
    /// </summary>
    FtpOrSftp,

    /// <summary>
    /// Delivery to a local folder or a network UNC path.
    /// </summary>
    LocalOrNetworkStorage,

    /// <summary>
    /// Delivery via a RESTful API call to an external system.
    /// </summary>
    RestApi,

    /// <summary>
    /// The report content is returned directly in the body of an API response.
    /// </summary>
    ApiResponse
}