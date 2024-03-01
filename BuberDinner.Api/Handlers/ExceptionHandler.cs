using Microsoft.AspNetCore.Diagnostics;

namespace BuberDinner.Api.Handlers;

public static class ExceptionHandler
{
    public static void ResolveProblems(IApplicationBuilder handler)
    {
        handler.Run(async context =>
        {
            Exception? ex = context
            .Features
            .Get<IExceptionHandlerFeature>()?
            .Error;

            await Results
            .Problem()
            .ExecuteAsync(context);
        });
    }
}