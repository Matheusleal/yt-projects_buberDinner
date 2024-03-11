using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Api.Common;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BuberDinner.Api.Routes;

public static class InjectDinnerRoutes
{
    public static IEndpointRouteBuilder MapDinnerRoutes(this IEndpointRouteBuilder app)
    {
        var dinnerRoutes = new DinnerRoutes();
        return dinnerRoutes.MapRoutes(app);
    }
}

public class DinnerRoutes : BaseRoute
{
    public override IEndpointRouteBuilder MapRoutes(IEndpointRouteBuilder app)
    {
        var routes = app
            .MapGroup("/dinners")
            .WithName("Dinner")
            .RequireAuthorization()
            .WithOpenApi();

        routes.MapGet("/list", ListDinnersAsync)
        .Produces<ClaimsPrincipal>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status409Conflict)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status404NotFound);

        return routes;
    }

    private async Task<IResult> ListDinnersAsync(
        [FromServices] ISender mediator,
        [FromServices] IMapper mapper,
        HttpContext context)
    {
        await Task.CompletedTask;

        return Results.Ok(context.User);

        // return Results.Ok(Array.Empty<string>());

        // var query = new GetAllDinnersQuery();
        // var dinnerResult = await mediator.Send(query);
        // return dinnerResult.Match(
        //     dinners => Results.Ok(dinners),
        //     errors => Problem(context, errors)
        // );
    }
}
