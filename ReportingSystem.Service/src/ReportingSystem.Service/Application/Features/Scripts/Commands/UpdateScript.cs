using FluentValidation;
using MediatR;
using ReportingSystem.Core.Domain.Entities;
using ReportingSystem.Service.Application.Common.Exceptions;
using ReportingSystem.Service.Application.Common.Interfaces;

namespace ReportingSystem.Service.Application.Features.Scripts.Commands.UpdateScript
{
    // REQ-FUNC-DTR-004: The system shall provide full CRUD functionality for transformation scripts.
    // US-044: Edit an existing JavaScript transformation script
    public class UpdateScript
    {
        public record Command : IRequest
        {
            public Guid Id { get; init; }
            public string Name { get; init; } = string.Empty;
            public string Content { get; init; } = string.Empty;
            public string? Description { get; init; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(v => v.Id)
                    .NotEmpty();

                RuleFor(v => v.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

                RuleFor(v => v.Content)
                    .NotEmpty().WithMessage("Script content cannot be empty.");

                RuleFor(v => v.Description)
                    .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IApplicationDbContext _context;
            private readonly IAuditLogger _auditLogger;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationDbContext context, IAuditLogger auditLogger, ICurrentUserService currentUserService)
            {
                _context = context;
                _auditLogger = auditLogger;
                _currentUserService = currentUserService;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _currentUserService.UserId;
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("Cannot update script without a valid user context.");
                }

                var script = await _context.TransformationScripts
                    .Include(s => s.Versions.OrderByDescending(v => v.Version))
                    .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

                if (script == null)
                {
                    throw new NotFoundException(nameof(TransformationScript), request.Id);
                }

                // Check for name uniqueness if the name has changed
                if (script.Name.ToLower() != request.Name.ToLower())
                {
                    var isNameTaken = await _context.TransformationScripts
                        .AnyAsync(s => s.Id != request.Id && s.Name.ToLower() == request.Name.ToLower(), cancellationToken);

                    if (isNameTaken)
                    {
                        throw new Common.Exceptions.ValidationException($"A transformation script with the name '{request.Name}' already exists.");
                    }
                }
                
                var latestVersion = script.Versions.First();

                // US-044 AC-006: Saving a script with no changes does not create a new version
                bool hasContentChanged = latestVersion.Content != request.Content;
                bool hasMetadataChanged = script.Name != request.Name || script.Description != request.Description;

                if (!hasContentChanged && !hasMetadataChanged)
                {
                    // No changes detected, so we can return early.
                    return;
                }
                
                script.Name = request.Name;
                script.Description = request.Description;
                script.LastModifiedBy = userId;
                script.LastModifiedAt = DateTime.UtcNow;

                if (hasContentChanged)
                {
                    var newVersion = new TransformationScriptVersion
                    {
                        TransformationScriptId = script.Id,
                        Content = request.Content,
                        Version = latestVersion.Version + 1,
                        CreatedBy = userId,
                        CreatedAt = DateTime.UtcNow
                    };
                    
                    script.Versions.Add(newVersion);
                    script.ActiveVersionId = newVersion.Id;
                    
                    _context.TransformationScriptVersions.Add(newVersion);
                }
                
                _context.TransformationScripts.Update(script);
                await _context.SaveChangesAsync(cancellationToken);

                await _auditLogger.LogAuditEventAsync(new AuditEvent
                {
                    EventType = "TRANSFORMATION_SCRIPT_UPDATED",
                    SubjectId = userId,
                    SubjectType = "User",
                    ResourceId = script.Id.ToString(),
                    ResourceType = "TransformationScript",
                    Details = $"User '{userId}' updated transformation script '{script.Name}'. New active version is {script.Versions.First().Version}."
                });
            }
        }
    }
}