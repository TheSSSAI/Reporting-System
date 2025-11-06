using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportingSystem.Service.Application.Features.Scripts.Commands;
using ReportingSystem.Service.Application.Features.Scripts.Queries;
using ReportingSystem.Service.Api.Dtos;

namespace ReportingSystem.Service.Api.Controllers
{
    /// <summary>
    /// API controller for managing transformation scripts. Provides full CRUD functionality.
    /// </summary>
    [ApiController]
    [Route("api/v1/scripts")]
    [Authorize(Roles = "Administrator")]
    public class ScriptsController : ControllerBase
    {
        private readonly ISender _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptsController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR sender instance for dispatching commands and queries.</param>
        public ScriptsController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a list of all transformation scripts.
        /// </summary>
        /// <remarks>
        /// Requirements: REQ-FUNC-DTR-004
        /// </remarks>
        /// <returns>A list of transformation scripts.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ScriptDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllScripts()
        {
            var query = new GetAllScripts.Query();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific transformation script by its ID, including its version history.
        /// </summary>
        /// <remarks>
        /// Requirements: REQ-FUNC-DTR-004, REQ-FUNC-DTR-005
        /// </remarks>
        /// <param name="id">The unique identifier of the script.</param>
        /// <returns>The requested transformation script details.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ScriptDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetScriptById(Guid id)
        {
            var query = new GetScriptById.Query { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new transformation script.
        /// </summary>
        /// <remarks>
        /// Creates the script and its first version. The action is audited.
        /// Requirements: REQ-FUNC-DTR-004, REQ-SEC-DTR-004, US-043
        /// </remarks>
        /// <param name="dto">The data transfer object for creating a script.</param>
        /// <returns>The newly created script.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ScriptDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateScript([FromBody] CreateScriptDto dto)
        {
            var command = new CreateScript.Command
            {
                Name = dto.Name,
                Description = dto.Description,
                Content = dto.Content
            };

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetScriptById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing transformation script.
        /// </summary>
        /// <remarks>
        /// This action creates a new version of the script. The action is audited.
        /// Requirements: REQ-FUNC-DTR-004, REQ-FUNC-DTR-005, REQ-SEC-DTR-004, US-044
        /// </remarks>
        /// <param name="id">The unique identifier of the script to update.</param>
        /// <param name="dto">The data transfer object with the updated script details.</param>
        /// <returns>An HTTP 204 No Content response upon success.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateScript(Guid id, [FromBody] UpdateScriptDto dto)
        {
            var command = new UpdateScript.Command
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                Content = dto.Content
            };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing transformation script and all its versions.
        /// </summary>
        /// <remarks>
        /// This action will fail if the script is currently associated with any report configurations. The action is audited.
        /// Requirements: REQ-FUNC-DTR-004, REQ-SEC-DTR-004
        /// </remarks>
        /// <param name="id">The unique identifier of the script to delete.</param>
        /// <returns>An HTTP 204 No Content response upon success.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteScript(Guid id)
        {
            var command = new DeleteScript.Command { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Restores a specific version of a script as the new active version.
        /// </summary>
        /// <remarks>
        /// This creates a new version that is a copy of the specified historical version.
        /// Requirements: REQ-FUNC-DTR-005, US-100
        /// </remarks>
        /// <param name="scriptId">The unique identifier of the parent script.</param>
        /// <param name="versionId">The unique identifier of the version to restore.</param>
        /// <returns>The newly created active version DTO.</returns>
        [HttpPost("{scriptId:guid}/versions/{versionId:guid}/restore")]
        [ProducesResponseType(typeof(ScriptVersionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RestoreScriptVersion(Guid scriptId, Guid versionId)
        {
            var command = new RestoreScriptVersion.Command { ScriptId = scriptId, VersionIdToRestore = versionId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}