using BuberDinner.Api.Errors;
using BuberDinner.Api.Handlers;
using BuberDinner.Api.Middleware;
using BuberDinner.Api.Routes;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddProblemDetails(ProblemDetailsCustomization.AddCustomization);
}

var app = builder.Build();
{
    app.UseExceptionHandler(ExceptionHandler.ResolveProblems);

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();

    app.MapAuthenticationRoutes();

    app.Run();
}