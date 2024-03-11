using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping;
using BuberDinner.Api.Routes;

namespace BuberDinner.Api;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddMappings();

    services.AddProblemDetails(ProblemDetailsCustomization.AddCustomization);

    return services;
  }

  public static IEndpointRouteBuilder MapPresentation(this IEndpointRouteBuilder app)
  {
    app.MapAuthenticationRoutes();
    app.MapDinnerRoutes();

    return app;
  }
}