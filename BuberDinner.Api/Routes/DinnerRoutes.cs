using System.Security.Claims;
using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Api.Common;

namespace BuberDinner.Api.Routes;

public static partial class InjectRoutes
{
    public static IEndpointRouteBuilder MapDinnerRoutes(this IEndpointRouteBuilder app)
    {
        var routes = new DinnerRoutes();

        var router = app
            .MapGroup("/dinners")
            .WithName("Dinner")
            .RequireAuthorization()
            .WithOpenApi();

        router.MapGet("/list", routes.ListDinnersAsync)
        .Produces<ClaimsPrincipal>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status409Conflict)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status404NotFound);

        return router;
    }
}

public class DinnerRoutes : BaseRoute
{
    public async Task<IResult> ListDinnersAsync(
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
