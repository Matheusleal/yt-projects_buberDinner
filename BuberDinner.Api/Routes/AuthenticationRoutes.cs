using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Routes;

public static class AuthenticationRoutes
{
  public static IEndpointRouteBuilder MapAuthenticationRoutes(this IEndpointRouteBuilder app)
  {
    app.MapPost("/auth/register", (
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

      var response = new AuthenticationResponse(
        authResult.User.Id,
        authResult.User.FirstName,
        authResult.User.LastName,
        authResult.User.Email,
        authResult.Token
      );

      return Results.Ok(response);
    })
      .WithName("Register")
      .WithOpenApi();

    app.MapPost("/auth/login", (
      [FromServices] IAuthenticationService authenticationService,
      LoginRequest request
      ) =>
    {
      var loginResult = authenticationService.Login(
        request.Email,
        request.Password
      );

      var response = new AuthenticationResponse(
        loginResult.User.Id,
        loginResult.User.FirstName,
        loginResult.User.LastName,
        loginResult.User.Email,
        loginResult.Token
      );

      return Results.Ok(response);
    })
      .WithName("Login")
      .WithOpenApi();

    return app;
  }
}