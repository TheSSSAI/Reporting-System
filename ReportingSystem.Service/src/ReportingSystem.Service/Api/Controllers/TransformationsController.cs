using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportingSystem.Service.Application.Features.Transformations.Commands;
using ReportingSystem.Service.Api.Dtos;
using System.Text.Json.Nodes;

namespace ReportingSystem.Service.Api.Controllers
{
    /// <summary>
    /// API controller for handling data transformations, such as script previews.
    /// </summary>
    [ApiController]
    [Route("api/v1/transformations")]
    [Authorize(Roles = "Administrator")]
    public class TransformationsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformationsController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR sender instance for dispatching commands and queries.</param>
        /// <param name="configuration">The application configuration for accessing settings.</param>
        public TransformationsController(ISender mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        /// <summary>
        /// Executes a transformation script preview using either sample data or data from a live connector.
        /// </summary>
        /// <remarks>
        /// This endpoint provides a way to test JavaScript transformation scripts in a secure, sandboxed environment.
        /// The request enforces a server-side timeout, which is configurable.
        /// 
        /// Requirements: REQ-INTG-DTR-001, REQ-PERF-DTR-001, US-046, US-047, US-048
        /// </remarks>
        /// <param name="request">The preview request DTO containing the script content and either sample data or a connector ID.</param>
        /// <returns>The transformed JSON data, or a structured error if the preview fails.</returns>
        [HttpPost("preview")]
        [ProducesResponseType(typeof(JsonNode), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Preview([FromBody] PreviewRequestDto request)
        {
            var timeoutInSeconds = _configuration.GetValue<int>("FeatureSettings:PreviewTimeoutSeconds", 30);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutInSeconds));

            try
            {
                var command = new ExecutePreview.Command
                {
                    ScriptContent = request.ScriptContent,
                    SampleData = request.SampleData,
                    ConnectorId = request.ConnectorId
                };

                var result = await _mediator.Send(command, cts.Token);
                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                // This specific exception is caught to return a 408 status code,
                // while other exceptions are handled by the global middleware.
                var errorDetails = new ErrorDetails
                {
                    Message = $"Execution timed out. The script took longer than {timeoutInSeconds} seconds to complete.",
                    StackTrace = null, 
                    LineNumber = null 
                };
                return StatusCode(StatusCodes.Status408RequestTimeout, new ErrorResponseDto { Error = errorDetails });
            }
        }
    }
}