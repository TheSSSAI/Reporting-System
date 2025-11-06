using MediatR;
using ReportingSystem.Service.Api.Dtos;
using ReportingSystem.Service.Application.Common.Exceptions;
using ReportingSystem.Service.Application.Common.Interfaces;

namespace ReportingSystem.Service.Application.Features.Scripts.Queries.GetScriptById
{
    // REQ-FUNC-DTR-004: The system shall provide full CRUD functionality for transformation scripts.
    public class GetScriptById
    {
        public record Query : IRequest<ScriptDto>
        {
            public Guid Id { get; init; }
        }

        public class Handler : IRequestHandler<Query, ScriptDto>
        {
            private readonly IApplicationDbContext _context;
            // In a real application, AutoMapper or a similar mapping library would be used.
            // For this generation, I will perform manual mapping.
            // private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ScriptDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var script = await _context.TransformationScripts
                    .AsNoTracking()
                    .Include(s => s.ActiveVersion)
                    .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

                if (script == null || script.ActiveVersion == null)
                {
                    throw new NotFoundException(nameof(Core.Domain.Entities.TransformationScript), request.Id);
                }

                // Manual Mapping from Domain Entity to DTO
                var scriptDto = new ScriptDto
                {
                    Id = script.Id,
                    Name = script.Name,
                    Description = script.Description,
                    Content = script.ActiveVersion.Content,
                    ActiveVersion = new ScriptVersionDto
                    {
                        Id = script.ActiveVersion.Id,
                        Version = script.ActiveVersion.Version,
                        CreatedAt = script.ActiveVersion.CreatedAt,
                        CreatedBy = script.ActiveVersion.CreatedBy
                    },
                    CreatedAt = script.CreatedAt,
                    CreatedBy = script.CreatedBy,
                    LastModifiedAt = script.LastModifiedAt,
                    LastModifiedBy = script.LastModifiedBy
                };

                return scriptDto;
            }
        }
    }
}