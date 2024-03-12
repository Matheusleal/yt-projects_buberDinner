using BuberDinner.Api.Common;
using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Routes;

public static partial class InjectRoutes
{
    public static IEndpointRouteBuilder MapMenuRoutes(this IEndpointRouteBuilder app)
    {
        var routes = new MenuRoutes();

        var router = app
            .MapGroup("/hosts/{hostId}/menu")
            .WithName("Menu")
            .RequireAuthorization()
            .WithOpenApi();

        router.MapPost("/", routes.CreateMenuAsync)
            .Produces<IResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        return router;
    }
}

public class MenuRoutes : BaseRoute
{
    public async Task<IResult> CreateMenuAsync(
        [FromServices] IMapper mapper,
        [FromServices] ISender mediator,
        HttpContext context,
        Guid hostId,
        CreateMenuRequest request)
    {
        //var command = mapper.Map<CreateMenuCommand>((request, hostId));

        var fullCreateRequest = new FullCreateMenuRequest(request, hostId.ToString());
        var command = mapper.Map<CreateMenuCommand>(fullCreateRequest);

        var createdMenuResult = await mediator.Send(command);

        return createdMenuResult.Match(
          menu => Results.Ok(mapper.Map<MenuResponse>(menu)),
          errors => Problem(context, errors));
    }
}
