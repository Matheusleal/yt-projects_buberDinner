using BuberDinner.Api.Common.Handlers;
using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Routes;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

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

    new AuthenticationRoutes().MapAuthenticationRoutes(app);

    app.Run();
}