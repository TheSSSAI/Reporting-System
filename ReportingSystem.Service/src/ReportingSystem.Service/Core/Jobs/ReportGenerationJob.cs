using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using ReportingSystem.Service.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportingSystem.Service.Core.Jobs
{
    /// <summary>
    /// Represents the background job executed by Quartz.NET to generate a single report instance.
    /// This class acts as a thin adapter between the Quartz.NET scheduling framework and the application's core business logic.
    /// It is responsible for orchestrating the report execution process and ensuring that any catastrophic failures
    /// are caught and logged, preventing the scheduler from crashing.
    /// </summary>
    [DisallowConcurrentExecution]
    public class ReportGenerationJob : IJob
    {
        private readonly ILogger<ReportGenerationJob> _logger;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportGenerationJob"/> class.
        /// </summary>
        /// <param name="logger">The logger for capturing job execution information.</param>
        /// <param name="serviceProvider">The service provider to resolve scoped services like the report execution service.</param>
        public ReportGenerationJob(ILogger<ReportGenerationJob> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Executes the report generation job. This method is called by the Quartz.NET scheduler.
        /// It resolves the necessary application services within a new dependency injection scope,
        /// initiates the report execution, and handles any unhandled exceptions to ensure service stability.
        /// </summary>
        /// <param name="context">The execution context of the job, provided by Quartz.NET.</param>
        /// <returns>A task that represents the asynchronous execution of the job.</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            var jobExecutionLogId = GetJobExecutionLogId(context);
            if (jobExecutionLogId == Guid.Empty)
            {
                // This indicates a severe misconfiguration where the job was scheduled without the required ID.
                // The job cannot proceed.
                _logger.LogCritical("ReportGenerationJob started without a valid JobExecutionLogId in JobDataMap. Job Key: {JobKey}", context.JobDetail.Key);
                return;
            }

            _logger.LogInformation("Starting report generation job for JobExecutionLogId: {JobExecutionLogId}", jobExecutionLogId);

            try
            {
                // Create a new DI scope for this job execution. This is crucial for correctly handling
                // the lifetime of scoped services like DbContext and any other I/O related services.
                // It prevents captive dependency issues.
                await using var scope = _serviceProvider.CreateAsyncScope();
                var reportExecutionService = scope.ServiceProvider.GetRequiredService<IReportExecutionService>();

                // Delegate the entire report generation workflow to the application service.
                // The CancellationToken from the context allows for graceful shutdown and manual cancellation.
                await reportExecutionService.ExecuteReportAsync(jobExecutionLogId, context.CancellationToken);

                _logger.LogInformation("Successfully completed orchestration for report generation job. JobExecutionLogId: {JobExecutionLogId}", jobExecutionLogId);
            }
            catch (OperationCanceledException)
            {
                // This is thrown when the job is cancelled by the scheduler (e.g., during a graceful shutdown)
                // or via a manual cancellation request.
                _logger.LogWarning("Report generation job was cancelled. JobExecutionLogId: {JobExecutionLogId}", jobExecutionLogId);
                // The IReportExecutionService is expected to handle updating the job status to 'Cancelled'.
                // If it fails to do so, we will attempt to mark it as failed here as a fallback.
                await UpdateJobStatusOnCatastrophicFailure(jobExecutionLogId, "Job was cancelled.", "The operation was cancelled, likely due to a manual request or service shutdown.");
            }
            catch (Exception ex)
            {
                // This is the last-resort catch block for any unhandled exceptions that bubble up from the
                // application services. This fulfills REQ-REL-DTR-001 by ensuring that a single job failure
                // does not crash the entire Quartz.NET worker thread or the host service.
                _logger.LogCritical(ex, "An unhandled catastrophic error occurred during report generation job. JobExecutionLogId: {JobExecutionLogId}", jobExecutionLogId);

                // In the case of a catastrophic failure, we attempt to update the job's status in the database
                // to 'Failed' so that administrators are aware of the problem.
                await UpdateJobStatusOnCatastrophicFailure(jobExecutionLogId, "A catastrophic error occurred during job execution.", ex.ToString());
                
                // We DO NOT re-throw the exception, as that would crash the scheduler's worker thread.
            }
        }

        private Guid GetJobExecutionLogId(IJobExecutionContext context)
        {
            if (context.JobDetail.JobDataMap.Get(nameof(JobExecutionLogId)) is Guid id)
            {
                return id;
            }
            if (context.JobDetail.JobDataMap.GetString(nameof(JobExecutionLogId)) is string idString && Guid.TryParse(idString, out var guidFromString))
            {
                return guidFromString;
            }
            return Guid.Empty;
        }
        
        private async Task UpdateJobStatusOnCatastrophicFailure(Guid jobExecutionLogId, string errorMessage, string errorDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to update job status to 'Failed' after catastrophic error for JobExecutionLogId: {JobExecutionLogId}", jobExecutionLogId);
                await using var scope = _serviceProvider.CreateAsyncScope();
                var jobLogUpdater = scope.ServiceProvider.GetRequiredService<IJobLogUpdaterService>();
                await jobLogUpdater.MarkJobAsFailedAsync(jobExecutionLogId, errorMessage, errorDetails);
                _logger.LogInformation("Successfully updated job status to 'Failed' for JobExecutionLogId: {JobExecutionLogId}", jobExecutionLogId);
            }
            catch (Exception updateEx)
            {
                _logger.LogCritical(updateEx, "Failed to update job status after a catastrophic error for JobExecutionLogId: {JobExecutionLogId}. The job log may be in an inconsistent state.", jobExecutionLogId);
            }
        }
    }
}