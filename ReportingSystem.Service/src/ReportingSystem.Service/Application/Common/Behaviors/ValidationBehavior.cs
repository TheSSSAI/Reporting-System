using FluentValidation;
using MediatR;
using ReportingSystem.Service.Application.Common.Exceptions;

namespace ReportingSystem.Service.Application.Common.Behaviors;

/// <summary>
/// A MediatR pipeline behavior that intercepts incoming requests and performs validation.
/// It uses FluentValidation to validate the request and throws a ValidationException
/// if any validation errors are found, preventing the request from reaching the handler.
/// </summary>
/// <typeparam name="TRequest">The type of the request being handled.</typeparam>
/// <typeparam name="TResponse">The type of the response from the handler.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators">An enumerable of validators for the request, injected by the DI container.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Handles the request by first performing validation and then, if successful, passing the request to the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="next">The delegate for the next action in the pipeline.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation, containing the handler's response.</returns>
    /// <exception cref="ValidationException">Thrown when validation fails.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            var errorDictionary = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
            
            throw new ValidationException(errorDictionary);
        }

        return await next();
    }
}