using System;

namespace ReportingSystem.Shared.Contracts.Transformations
{
    /// <summary>
    /// Specifies the public representation of a transformation script.
    /// </summary>
    /// <param name="Id">The unique identifier for the script.</param>
    /// <param name="Name">The user-defined name of the script.</param>
    /// <param name="Content">The JavaScript content of the script.</param>
    /// <param name="LastModified">The timestamp of the last modification.</param>
    /// <param name="Version">The current active version number of the script.</param>
    public record TransformationScriptDto(
        Guid Id,
        string Name,
        string Content,
        DateTimeOffset LastModified,
        int Version);
}