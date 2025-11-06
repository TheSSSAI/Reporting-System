using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReportingSystem.Service.Application.Common.Interfaces;
using System.Data;

namespace ReportingSystem.Service.Application.Common.Behaviors;

/// <summary>
/// A MediatR pipeline behavior that wraps command handlers in a database transaction.
/// This ensures that operations that modify the database are atomic (either fully complete or are rolled back).
/// It identifies commands based on naming convention and skips queries to avoid unnecessary transaction overhead.
/// </summary>
/// <typeparam name="TRequest">The type of the request being handled.</typeparam>
/// <typeparam name="TResponse">The type of the response from the handler.</typeparam>
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IApplicationDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="dbContext">The application database context.</param>
    public TransactionBehavior(
        ILogger<TransactionBehavior<TRequest, TResponse>> logger,
        IApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Handles the request by wrapping it in a transaction if it is a command.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="next">The delegate for the next action in the pipeline.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation, containing the handler's response.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        // Simple heuristic: Only apply transactions to requests that are commands (i.e., write operations).
        // A more robust approach could be a marker interface like ITransactionalRequest.
        bool isCommand = requestName.EndsWith("Command");

        if (!isCommand)
        {
            return await next();
        }

        // Avoid creating a new transaction if one already exists
        if (_dbContext.Database.CurrentTransaction != null)
        {
            return await next();
        }

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
            var transactionId = transaction.TransactionId;

            try
            {
                _logger.LogInformation("Beginning database transaction {TransactionId} for {RequestName}", transactionId, requestName);

                var response = await next();

                await _dbContext.SaveChangesAsync(cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
                
                _logger.LogInformation("Committed database transaction {TransactionId} for {RequestName}", transactionId, requestName);
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during transaction {TransactionId} for {RequestName}. Rolling back.", transactionId, requestName);

                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        });
    }
}