namespace BuberDinner.Api.Errors;

public static class ProblemDetailsCustomization
{
    public static void AddCustomization(ProblemDetailsOptions options)
    {
        options.CustomizeProblemDetails = ( context ) => {

            context.ProblemDetails.Extensions.Add("customProperty", "customValue");

        };
    }
}