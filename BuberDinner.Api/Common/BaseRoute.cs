using ErrorOr;
using BuberDinner.Api.Common.Http;

namespace BuberDinner.Api.Common;

public class BaseRoute
{
    protected IResult Problem(HttpContext context, List<Error> errors)
    {
        if(errors.Count is 0)
            return Results.Problem();

        if (errors.All(x => x.Type == ErrorType.Validation))
            return ValidationProblem(errors);

        context.Items[HttpContextItemKeys.Errors] = errors;

        return Problem(errors.First());
    }

    private static IResult ValidationProblem(List<Error> errors)
    {
        var modelErrors = errors
            .GroupBy(x => x.Code)
            .ToDictionary(x => x.Key, x => x.Select(y => y.Description).ToArray());

        return Results.ValidationProblem(modelErrors);
    }
    private static IResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Results.Problem(statusCode: statusCode, title: error.Description);
    }
}