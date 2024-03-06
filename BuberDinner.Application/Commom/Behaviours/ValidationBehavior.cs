using MediatR;
using ErrorOr;
using FluentValidation;

namespace BuberDinner.Application.Common.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {

        if(_validator is null)
        {
            return await next();
        }

        var validationResult = _validator.Validate(request);
        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult
            .Errors
            .ConvertAll(validationFailure => Error.Validation(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage));

        return (dynamic)errors; //just use dynamic when we have sure that the response type is IErrorOr
    }
}