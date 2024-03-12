using System.Diagnostics;
using BuberDinner.Api.Common.Http;
using ErrorOr;

namespace BuberDinner.Api.Common.Errors;

public static class ProblemDetailsCustomization
{
    public static void AddCustomization(ProblemDetailsOptions options)
    {
        options.CustomizeProblemDetails = (context) =>
        {
            var traceId = Activity.Current?.Id ?? context?.HttpContext?.TraceIdentifier;
            if (traceId is not null)
            {
                context?.ProblemDetails.Extensions.Add("traceId", traceId);
            }

            var errors = context?.HttpContext.Items[HttpContextItemKeys.Errors];
            if (errors is List<Error> errorList)
            {
                context?.ProblemDetails.Extensions.Add("errorCodes", errorList.Select(x => new { code = x.Code, description = x.Description }));
            }
        };
    }
}