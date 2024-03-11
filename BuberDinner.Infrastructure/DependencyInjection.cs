using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BuberDinner.Application.Commom.Interfaces.Authentication;
using BuberDinner.Application.Commom.Interfaces.Persistence;
using BuberDinner.Application.Commom.Interfaces.Services;

using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Services;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
	{
		services
			.AddAuth(configuration)
			.AddSingleton<IDateTimeProvider, DateTimeProvider>()

			.AddScoped<IUserRepository, UserRepository>()

			;
		return services;
	}

	public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName))
			.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

		services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme);

		return services;
	}
}