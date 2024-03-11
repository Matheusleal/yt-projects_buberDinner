using Microsoft.AspNetCore.Diagnostics;

namespace BuberDinner.Api.Common.Handlers;

public static class ExceptionHandler
{
    public static void ResolveProblems(IApplicationBuilder handler)
    {
        handler.Run(async context =>
        {
            Exception? ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            //var (statusCode, message) = ex switch
            //{
            //    IError se => ((int)se.StatusCode, se.ErrorMessage),
            //    _ => (StatusCodes.Status500InternalServerError, "An error occurred while processing your request.")
            //};

            await Results
                .Problem(ex.Message, ex.GetType().Name, StatusCodes.Status500InternalServerError)
                .ExecuteAsync(context);
        });
    }
}