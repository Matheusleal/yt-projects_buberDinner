using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Api.Common;
using BuberDinner.Contracts.Authentication;
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

    authRoutes
      .MapPost("/register", PostRegisterAsync)
      .WithName("Register")
      .Produces<AuthenticationResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status409Conflict)
      .ProducesProblem(StatusCodes.Status500InternalServerError);

    authRoutes
      .MapPost("/login", PostLoginAsync)
      .WithName("Login")
      .Produces<AuthenticationResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status409Conflict)
      .ProducesProblem(StatusCodes.Status500InternalServerError)
      .ProducesProblem(StatusCodes.Status401Unauthorized)
      .ProducesProblem(StatusCodes.Status404NotFound);

    return app;
  }

  private async Task<IResult> PostRegisterAsync(
      [FromServices] ISender mediator,
      [FromServices] IMapper mapper,
      HttpContext context,
      RegisterRequest request)
  {
    var query = mapper.Map<RegisterCommand>(request);
    var loginResult = await mediator.Send(query);

    return loginResult.Match(
      user => Results.Ok(mapper.Map<AuthenticationResponse>(user)),
      errors => Problem(context, errors)
    );
  }

  private async Task<IResult> PostLoginAsync(
      [FromServices] ISender mediator,
      [FromServices] IMapper mapper,
      HttpContext context,
      LoginRequest request)
  {
    var query = mapper.Map<LoginQuery>(request);
    var loginResult = await mediator.Send(query);

    return loginResult.Match(
      user => Results.Ok(mapper.Map<AuthenticationResponse>(user)),
      errors => Problem(context, errors)
    );
  }
}