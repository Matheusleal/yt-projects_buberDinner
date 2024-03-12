using System.Reflection;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BuberDinner.Application.Commom.Behaviours;

namespace BuberDinner.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(typeof(DependencyInjection).Assembly);

    services.AddScoped(
      typeof(IPipelineBehavior<,>),
      typeof(ValidationBehavior<,>));

    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    return services;
  }
}