using ReportingSystem.Core.Domain.Common;
using ReportingSystem.Core.Domain.Exceptions;
using System.Linq;

namespace ReportingSystem.Core.Domain.Entities;

/// <summary>
/// Represents a transformation script as an aggregate root.
/// It manages the script's metadata and its version history.
/// </summary>
public class TransformationScript : AggregateRoot<Guid>
{
    private readonly List<TransformationScriptVersion> _versions = new();

    /// <summary>
    /// Gets the user-defined name of the script.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the ID of the currently active script version.
    /// </summary>
    public Guid ActiveScriptVersionId { get; private set; }

    /// <summary>
    /// Gets the collection of all versions of this script.
    /// </summary>
    public virtual IReadOnlyCollection<TransformationScriptVersion> Versions => _versions.AsReadOnly();
    
    // Private constructor for EF Core
    private TransformationScript() : base(Guid.NewGuid()) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="TransformationScript"/> class.
    /// </summary>
    /// <param name="name">The name of the script.</param>
    /// <param name="initialContent">The initial JavaScript content.</param>
    /// <param name="createdByUserId">The ID of the user creating the script.</param>
    public TransformationScript(string name, string initialContent, Guid createdByUserId) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleValidationException("Transformation script name cannot be empty.");

        if (string.IsNullOrWhiteSpace(initialContent))
            throw new BusinessRuleValidationException("Transformation script content cannot be empty.");
            
        Name = name;

        var initialVersion = new TransformationScriptVersion(Id, 1, initialContent, createdByUserId);
        _versions.Add(initialVersion);
        ActiveScriptVersionId = initialVersion.Id;
    }
    
    /// <summary>
    /// Updates the content of the script, creating a new version.
    /// </summary>
    /// <param name="newContent">The new JavaScript content.</param>
    /// <param name="updatedByUserId">The ID of the user performing the update.</param>
    public void UpdateContent(string newContent, Guid updatedByUserId)
    {
        if (string.IsNullOrWhiteSpace(newContent))
            throw new BusinessRuleValidationException("Transformation script content cannot be empty.");

        var currentVersion = GetActiveVersion();
        if (currentVersion.Content == newContent)
        {
            // No changes, do nothing.
            return;
        }

        var newVersionNumber = _versions.Max(v => v.VersionNumber) + 1;
        var newVersion = new TransformationScriptVersion(Id, newVersionNumber, newContent, updatedByUserId);
        
        _versions.Add(newVersion);
        ActiveScriptVersionId = newVersion.Id;
    }

    /// <summary>
    /// Reverts the script to a previous version by creating a new version from the old content.
    /// </summary>
    /// <param name="versionIdToRevert">The ID of the historical version to revert to.</param>
    /// <param name="revertedByUserId">The ID of the user performing the revert.</param>
    public void RevertToVersion(Guid versionIdToRevert, Guid revertedByUserId)
    {
        var versionToRestore = _versions.FirstOrDefault(v => v.Id == versionIdToRevert);
        if (versionToRestore == null)
            throw new EntityNotFoundException(nameof(TransformationScriptVersion), versionIdToRevert);

        if (versionToRestore.Id == ActiveScriptVersionId)
            throw new BusinessRuleValidationException("Cannot revert to the currently active version.");

        UpdateContent(versionToRestore.Content, revertedByUserId);
    }
    
    /// <summary>
    /// Changes the name of the transformation script.
    /// </summary>
    /// <param name="newName">The new name for the script.</param>
    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new BusinessRuleValidationException("Transformation script name cannot be empty.");
            
        Name = newName;
    }

    /// <summary>
    /// Gets the currently active script version entity.
    /// </summary>
    /// <returns>The active TransformationScriptVersion.</returns>
    public TransformationScriptVersion GetActiveVersion()
    {
        var activeVersion = _versions.FirstOrDefault(v => v.Id == ActiveScriptVersionId);
        if (activeVersion is null)
        {
            // This indicates a data integrity issue. The aggregate should always have a valid active version.
            throw new InvalidOperationException($"Could not find active version '{ActiveScriptVersionId}' for script '{Id}'.");
        }
        return activeVersion;
    }
}