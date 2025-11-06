using FluentValidation;
using MediatR;
using ReportingSystem.Core.Domain.Entities;
using ReportingSystem.Service.Application.Common.Interfaces;

namespace ReportingSystem.Service.Application.Features.Scripts.Commands.CreateScript
{
    // REQ-FUNC-DTR-004: The system shall provide full CRUD functionality for transformation scripts.
    // US-043: Create and write a JavaScript transformation script
    public class CreateScript
    {
        public record Command : IRequest<Guid>
        {
            public string Name { get; init; } = string.Empty;
            public string Content { get; init; } = string.Empty;
            public string? Description { get; init; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(v => v.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

                RuleFor(v => v.Content)
                    .NotEmpty().WithMessage("Script content cannot be empty.");

                RuleFor(v => v.Description)
                    .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            }
        }

        public class Handler : IRequestHandler<Command, Guid>
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

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _currentUserService.UserId;
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("Cannot create script without a valid user context.");
                }

                var isNameTaken = await _context.TransformationScripts
                    .AnyAsync(s => s.Name.ToLower() == request.Name.ToLower(), cancellationToken);

                if (isNameTaken)
                {
                    throw new Common.Exceptions.ValidationException($"A transformation script with the name '{request.Name}' already exists.");
                }

                var script = new TransformationScript
                {
                    Name = request.Name,
                    Description = request.Description,
                    CreatedBy = userId,
                    CreatedAt = DateTime.UtcNow
                };

                var firstVersion = new TransformationScriptVersion
                {
                    TransformationScriptId = script.Id,
                    Content = request.Content,
                    Version = 1,
                    CreatedBy = userId,
                    CreatedAt = DateTime.UtcNow
                };

                script.ActiveVersionId = firstVersion.Id;
                script.Versions.Add(firstVersion);

                await _context.TransformationScripts.AddAsync(script, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await _auditLogger.LogAuditEventAsync(new AuditEvent
                {
                    EventType = "TRANSFORMATION_SCRIPT_CREATED",
                    SubjectId = userId,
                    SubjectType = "User",
                    ResourceId = script.Id.ToString(),
                    ResourceType = "TransformationScript",
                    Details = $"User '{userId}' created transformation script '{script.Name}' (Version 1)."
                });

                return script.Id;
            }
        }
    }
}