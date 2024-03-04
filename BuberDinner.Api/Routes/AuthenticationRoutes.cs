using MediatR;
using BuberDinner.Api.Common;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Authentication.Commands.Register;

namespace BuberDinner.Api.Routes;

public class AuthenticationRoutes : BaseRoute
{
  public IEndpointRouteBuilder MapAuthenticationRoutes(IEndpointRouteBuilder app)
  {
    var authRoutes = app
    .MapGroup("/auth")
    .WithName("Authentication")
    .WithOpenApi();

    authRoutes.MapPost("/register", async (
      HttpContext context,
      ISender mediator,
      RegisterRequest request
      ) =>
    {
      var command = new RegisterCommand(
        request.FirstName,
        request.LastName,
        request.Email,
        request.Password
      );
      var authResult = await mediator.Send(command);

      return authResult.Match(
        user => Results.Ok(MapResponse(user)),
        errors => Problem(context, errors)
      );

      static AuthenticationResponse MapResponse(AuthenticationResult result)
      {
        return new AuthenticationResponse(
          result.User.Id,
          result.User.FirstName,
          result.User.LastName,
          result.User.Email,
          result.Token
        );
      }
    })
      .WithName("Register")
      .Produces<AuthenticationResponse>(StatusCodes.Status200OK);

    authRoutes.MapPost("/login", async (
      HttpContext context,
      ISender mediator,
      LoginRequest request
      ) =>
    {
      var query = new LoginQuery(
        request.Email,
        request.Password
      );
      var loginResult = await mediator.Send(query);

      return loginResult.Match(
        user => Results.Ok(MapResponse(user)),
        errors => Problem(context, errors)
      );

      static AuthenticationResponse MapResponse(AuthenticationResult result)
      {
        return new AuthenticationResponse(
          result.User.Id,
          result.User.FirstName,
          result.User.LastName,
          result.User.Email,
          result.Token
        );
      }
    })
      .WithName("Login")
      .Produces<AuthenticationResponse>(StatusCodes.Status200OK);

    return app;
  }
}