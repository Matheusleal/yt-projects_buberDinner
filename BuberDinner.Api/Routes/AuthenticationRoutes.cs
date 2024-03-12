using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

using BuberDinner.Api.Common;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;

namespace BuberDinner.Api.Routes;

public static partial class InjectRoutes
{
    public static IEndpointRouteBuilder MapAuthenticationRoutes(this IEndpointRouteBuilder app)
    {
        var routes = new AuthenticationRoutes();

        var router = app
            .MapGroup("/auth")
            .WithName("Authentication")
            .WithOpenApi();

        router
          .MapPost("/register", routes.PostRegisterAsync)
          .WithName("Register")
          .Produces<AuthenticationResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status409Conflict)
          .ProducesProblem(StatusCodes.Status500InternalServerError);

        router
          .MapPost("/login", routes.PostLoginAsync)
          .WithName("Login")
          .Produces<AuthenticationResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status409Conflict)
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .ProducesProblem(StatusCodes.Status401Unauthorized)
          .ProducesProblem(StatusCodes.Status404NotFound);

        return router;
    }
}

public class AuthenticationRoutes : BaseRoute
{
    public async Task<IResult> PostRegisterAsync(
        [FromServices] ISender mediator,
        [FromServices] IMapper mapper,
        HttpContext context,
        RegisterRequest request)
    {
        var query = mapper.Map<TRequest>(request);
        var loginResult = await mediator.Send(query);

        return loginResult.Match(
          user => Results.Ok(mapper.Map<AuthenticationResponse>(user)),
          errors => Problem(context, errors));
    }

    public async Task<IResult> PostLoginAsync(
        [FromServices] ISender mediator,
        [FromServices] IMapper mapper,
        HttpContext context,
        LoginRequest request)
    {
        var query = mapper.Map<LoginQuery>(request);
        var loginResult = await mediator.Send(query);

        return loginResult.Match(
          user => Results.Ok(mapper.Map<AuthenticationResponse>(user)),
          errors => Problem(context, errors));
    }
}