using ReportingSystem.Core.Domain.Common;

namespace ReportingSystem.Core.Domain.Entities;

/// <summary>
/// Represents a specific, immutable version of a transformation script.
/// This entity is part of the TransformationScript aggregate.
/// </summary>
public class TransformationScriptVersion : Entity<Guid>
{
    /// <summary>
    /// Gets the ID of the parent TransformationScript.
    /// </summary>
    public Guid TransformationScriptId { get; private init; }

    /// <summary>
    /// Gets the parent TransformationScript.
    /// </summary>
    public virtual TransformationScript TransformationScript { get; private init; }

    /// <summary>
    /// Gets the sequential version number.
    /// </summary>
    public int VersionNumber { get; private init; }

    /// <summary>
    /// Gets the JavaScript content of this version.
    /// In the database, this content should be encrypted. The domain model deals with plaintext.
    /// </summary>
    public string Content { get; private init; }

    /// <summary>
    /// Gets the timestamp when this version was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; private init; }
    
    /// <summary>
    /// Gets the ID of the user who created this version.
    /// </summary>
    public Guid CreatedByUserId { get; private init; }

    // Private constructor for EF Core
    private TransformationScriptVersion() : base(Guid.NewGuid()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransformationScriptVersion"/> class.
    /// This constructor is internal to ensure versions are only created via the TransformationScript aggregate root.
    /// </summary>
    internal TransformationScriptVersion(Guid transformationScriptId, int versionNumber, string content, Guid createdByUserId) 
        : base(Guid.NewGuid())
    {
        TransformationScriptId = transformationScriptId;
        VersionNumber = versionNumber;
        Content = content;
        CreatedByUserId = createdByUserId;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}