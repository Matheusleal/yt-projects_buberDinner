using BuberDinner.Application.Commom.Interfaces.Authentication;
using BuberDinner.Application.Commom.Interfaces.Persistence;
using BuberDinner.Application.Commom.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		ConfigurationManager configuration
		)
	{
		services
			.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName))

			.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
			.AddSingleton<IDateTimeProvider, DateTimeProvider>()

			.AddScoped<IUserRepository, UserRepository>()

			;
		return services;
	}
}