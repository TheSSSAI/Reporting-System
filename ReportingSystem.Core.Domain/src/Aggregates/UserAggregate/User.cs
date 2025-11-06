using ReportingSystem.Core.Domain.Common;
using ReportingSystem.Core.Domain.Exceptions;
using ReportingSystem.Core.Domain.ValueObjects;

namespace ReportingSystem.Core.Domain.Aggregates.UserAggregate;

/// <summary>
/// Represents a user of the system. This is an aggregate root.
/// </summary>
public class User : AggregateRoot<Guid>
{
    /// <summary>
    /// Gets the unique username for the user.
    /// </summary>
    public string Username { get; private set; }

    /// <summary>
    /// Gets the user's email address.
    /// </summary>
    public EmailAddress Email { get; private set; }

    /// <summary>
    /// Gets the user's full name.
    /// </summary>
    public string FullName { get; private set; }

    /// <summary>
    /// Gets the ID of the role assigned to the user.
    /// </summary>
    public int RoleId { get; private set; }
    
    /// <summary>
    /// Gets the assigned role. Navigation property.
    /// </summary>
    public virtual Role Role { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the user's account is active.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this is the primary administrator account,
    /// which has special protections (e.g., cannot be deleted or deactivated).
    /// </summary>
    public bool IsPrimaryAdmin { get; private set; }

    // Private constructor for EF Core
    private User() : base(Guid.NewGuid()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="username">The username for the user.</param>
    /// <param name="email">The email address for the user.</param>
    /// <param name="fullName">The full name of the user.</param>
    /// <param name="roleId">The ID of the assigned role.</param>
    /// <param name="isPrimaryAdmin">Flag indicating if this is the primary administrator account.</param>
    public User(string username, EmailAddress email, string fullName, int roleId, bool isPrimaryAdmin = false) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new BusinessRuleValidationException("Username is required.");
        
        if (string.IsNullOrWhiteSpace(fullName))
            throw new BusinessRuleValidationException("Full name is required.");

        Username = username;
        Email = email ?? throw new BusinessRuleValidationException("Email is required.");
        FullName = fullName;
        RoleId = roleId;
        IsActive = true;
        IsPrimaryAdmin = isPrimaryAdmin;
    }

    /// <summary>
    /// Updates the user's details.
    /// </summary>
    /// <param name="fullName">The user's new full name.</param>
    /// <param name="email">The user's new email address.</param>
    public void UpdateDetails(string fullName, EmailAddress email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new BusinessRuleValidationException("Full name is required.");

        FullName = fullName;
        Email = email ?? throw new BusinessRuleValidationException("Email is required.");
    }

    /// <summary>
    /// Changes the user's role.
    /// </summary>
    /// <param name="newRoleId">The ID of the new role.</param>
    public void ChangeRole(int newRoleId)
    {
        if (IsPrimaryAdmin && newRoleId != RoleId) // Assuming role IDs are stable. A better check might involve role names.
        {
            throw new BusinessRuleValidationException("The primary administrator's role cannot be changed.");
        }
        
        if (newRoleId <= 0)
        {
            throw new BusinessRuleValidationException("Invalid Role ID specified.");
        }

        if (RoleId != newRoleId)
        {
            RoleId = newRoleId;
            // In a full implementation, this might raise a domain event.
            // AddDomainEvent(new UserRoleChangedEvent(Id, newRoleId));
        }
    }

    /// <summary>
    /// Deactivates the user's account, preventing them from logging in.
    /// </summary>
    public void Deactivate()
    {
        if (IsPrimaryAdmin)
        {
            throw new BusinessRuleValidationException("The primary administrator account cannot be deactivated.");
        }

        if (IsActive)
        {
            IsActive = false;
            // In a full implementation, this might raise a domain event.
            // AddDomainEvent(new UserDeactivatedEvent(Id));
        }
    }
    
    /// <summary>
    /// Activates the user's account, allowing them to log in.
    /// </summary>
    public void Activate()
    {
        if (!IsActive)
        {
            IsActive = true;
            // In a full implementation, this might raise a domain event.
            // AddDomainEvent(new UserActivatedEvent(Id));
        }
    }
}