using BuberDinner.Api.Common;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Routes;

public class AuthenticationRoutes : BaseRoute
{
  public IEndpointRouteBuilder MapAuthenticationRoutes(IEndpointRouteBuilder app)
  {
    var authRoutes = app
    .MapGroup("/auth")
    .WithName("Authentication")
    .WithOpenApi();

    authRoutes.MapPost("/register", (
      HttpContext context,
      [FromServices] IAuthenticationService authenticationService,
      RegisterRequest request
      ) =>
    {
      var authResult = authenticationService.Register(
        request.FirstName,
        request.LastName,
        request.Email,
        request.Password
      );

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

    authRoutes.MapPost("/login", (
      HttpContext context,
      [FromServices] IAuthenticationService authenticationService,
      LoginRequest request
      ) =>
    {
      var loginResult = authenticationService.Login(
        request.Email,
        request.Password
      );

      return loginResult.Match(
        user => Results.Ok(MapResponse(user)),
        errors => Results.Problem(statusCode: StatusCodes.Status401Unauthorized, title: errors.First().Description)
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