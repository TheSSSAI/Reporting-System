using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportingSystem.Service.Application.Features.Reports.Commands;
using ReportingSystem.Service.Application.Features.Reports.Queries;
using ReportingSystem.Service.Api.Dtos;

namespace ReportingSystem.Service.Api.Controllers
{
    /// <summary>
    /// API controller for managing and executing report configurations.
    /// </summary>
    [ApiController]
    [Route("api/v1/reports")]
    [Authorize(Roles = "Administrator")]
    public class ReportsController : ControllerBase
    {
        private readonly ISender _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR sender instance for dispatching commands and queries.</param>
        public ReportsController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a list of all report configurations.
        /// </summary>
        /// <remarks>
        /// Requirements: US-089
        /// </remarks>
        /// <returns>A list of report configurations.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReportConfigurationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllReports()
        {
            var query = new GetAllReportConfigurations.Query();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific report configuration by its ID.
        /// </summary>
        /// <remarks>
        /// Requirements: US-089
        /// </remarks>
        /// <param name="id">The unique identifier of the report configuration.</param>
        /// <returns>The requested report configuration.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ReportConfigurationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReportById(Guid id)
        {
            var query = new GetReportConfigurationById.Query { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new report configuration.
        /// </summary>
        /// <remarks>
        /// Requirements: US-089, US-051
        /// </remarks>
        /// <param name="dto">The data transfer object for creating a report configuration.</param>
        /// <returns>The newly created report configuration.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ReportConfigurationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportConfigurationDto dto)
        {
            var command = new CreateReportConfiguration.Command { Dto = dto };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetReportById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing report configuration.
        /// </summary>
        /// <remarks>
        /// Requirements: US-089, US-051
        /// </remarks>
        /// <param name="id">The unique identifier of the report configuration to update.</param>
        /// <param name="dto">The data transfer object with the updated details.</param>
        /// <returns>An HTTP 204 No Content response upon success.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReport(Guid id, [FromBody] UpdateReportConfigurationDto dto)
        {
            var command = new UpdateReportConfiguration.Command { Id = id, Dto = dto };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing report configuration.
        /// </summary>
        /// <remarks>
        /// Requirements: US-089
        /// </remarks>
        /// <param name="id">The unique identifier of the report configuration to delete.</param>
        /// <returns>An HTTP 204 No Content response upon success.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            var command = new DeleteReportConfiguration.Command { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Triggers the on-demand generation of a specific report.
        /// </summary>
        /// <remarks>
        /// This endpoint supports both synchronous and asynchronous execution based on the report's delivery configuration.
        /// - **Synchronous**: If configured for synchronous API delivery, the request will block until the report is generated (or times out) and return the file content directly.
        /// - **Asynchronous**: If configured for asynchronous API delivery, the endpoint will immediately return an HTTP 202 Accepted response with a job ID and a status URL for polling.
        /// 
        /// Requirements: US-090, US-091, US-094
        /// </remarks>
        /// <param name="id">The unique identifier of the report configuration to generate.</param>
        /// <returns>
        /// An `IActionResult` that is either:
        /// - A `FileStreamResult` with the report content (200 OK) for synchronous execution.
        /// - A `AcceptedResult` with job details (202 Accepted) for asynchronous execution.
        /// - An error response (4xx/5xx) for failures.
        /// </returns>
        [HttpPost("{id:guid}/generate")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Synchronous success
        [ProducesResponseType(typeof(JobInitiationDto), StatusCodes.Status202Accepted)] // Asynchronous success
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)] // e.g., Not configured for API delivery
        public async Task<IActionResult> GenerateReport(Guid id)
        {
            var command = new GenerateReport.Command { ReportConfigurationId = id };
            var result = await _mediator.Send(command);

            return result.Match<IActionResult>(
                syncResult => File(syncResult.ContentStream, syncResult.ContentType, syncResult.FileName),
                asyncResult => Accepted(asyncResult.StatusUrl, asyncResult)
            );
        }
    }
}