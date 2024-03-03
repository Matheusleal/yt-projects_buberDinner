using BuberDinner.Api.Common.Http;
using ErrorOr;

namespace BuberDinner.Api.Common;

public class BaseRoute
{
    protected IResult Problem(HttpContext context, List<Error> errors)
    {
        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };


        context.Items[HttpContextItemKeys.Errors] = errors;

        return Results.Problem(statusCode: statusCode, title: firstError.Description);
    }
}