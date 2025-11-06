using Microsoft.EntityFrameworkCore;
using ReportingSystem.Application.Interfaces;
using ReportingSystem.Domain.Entities;
using System.Linq;

namespace ReportingSystem.Infrastructure.Persistence.Repositories;

/// <summary>
/// EF Core implementation of the IScriptRepository interface.
/// Handles data access for TransformationScript and TransformationScriptVersion entities.
/// </summary>
public class EfCoreScriptRepository : IScriptRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfCoreScriptRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context.</param>
    public EfCoreScriptRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Retrieves a transformation script by its ID, including all its versions and the active version.
    /// </summary>
    /// <param name="id">The unique identifier of the script.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The transformation script if found; otherwise, null.</returns>
    public async Task<TransformationScript?> GetByIdWithVersionsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.TransformationScripts
            .Include(s => s.Versions.OrderByDescending(v => v.VersionNumber))
            .Include(s => s.ActiveVersion)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a transformation script by its ID, including only the active version's content.
    /// </summary>
    /// <param name="id">The unique identifier of the script.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The transformation script if found; otherwise, null.</returns>
    public async Task<TransformationScript?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.TransformationScripts
            .Include(s => s.ActiveVersion)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
    
    /// <summary>
    /// Retrieves a transformation script by its name. This is used for uniqueness checks.
    /// </summary>
    /// <param name="name">The name of the script.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The transformation script if found; otherwise, null.</returns>
    public async Task<TransformationScript?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        // Case-insensitive search for names
        return await _context.TransformationScripts
            .AsNoTracking()
            .FirstOrDefaultAsync(s => EF.Functions.ILike(s.Name, name), cancellationToken);
    }

    /// <summary>
    /// Retrieves all transformation scripts, including their active versions.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of all transformation scripts.</returns>
    public async Task<IEnumerable<TransformationScript>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TransformationScripts
            .Include(s => s.ActiveVersion)
            .AsNoTracking()
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Adds a new transformation script to the database.
    /// </summary>
    /// <param name="script">The transformation script to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddAsync(TransformationScript script, CancellationToken cancellationToken = default)
    {
        await _context.TransformationScripts.AddAsync(script, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing transformation script. This typically involves adding a new version
    /// and updating the active version pointer.
    /// </summary>
    /// <param name="script">The transformation script with its updated state.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpdateAsync(TransformationScript script, CancellationToken cancellationToken = default)
    {
        // EF Core's change tracker handles adding new versions and updating the parent script
        _context.TransformationScripts.Update(script);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Deletes a transformation script and all its associated versions.
    /// </summary>
    /// <param name="id">The unique identifier of the script to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var script = await _context.TransformationScripts
            .Include(s => s.Versions)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        
        if (script != null)
        {
            // Deleting the aggregate root will cascade delete its versions due to the database relationship
            _context.TransformationScripts.Remove(script);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}