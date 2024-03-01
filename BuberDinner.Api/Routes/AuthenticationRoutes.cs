using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Routes;

public static class AuthenticationRoutes
{
  public static IEndpointRouteBuilder MapAuthenticationRoutes(this IEndpointRouteBuilder app)
  {
    var authRoutes = app
    .MapGroup("/auth")
    .WithName("Authentication")
    .WithOpenApi();

    authRoutes.MapPost("/register", (
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
      .Produces<AuthenticationResponse>(StatusCodes.Status200OK);

    authRoutes.MapPost("/login", (
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
      .Produces<AuthenticationResponse>(StatusCodes.Status200OK);

    return app;
  }
}