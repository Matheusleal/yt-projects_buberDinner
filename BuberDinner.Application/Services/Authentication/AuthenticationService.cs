using ErrorOr;

using BuberDinner.Application.Commom.Interfaces.Authentication;
using BuberDinner.Application.Commom.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Common.Errors;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly IUserRepository _userRepository;

	public AuthenticationService(
		IJwtTokenGenerator jwtTokenGenerator,
		IUserRepository userRepository)
	{
		_jwtTokenGenerator = jwtTokenGenerator;
		_userRepository = userRepository;
	}

	public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
	{
		if(_userRepository.GetUserByEmail(email) is not null)
		{
			return Errors.User.DuplicateEmail;
		}

		var user = new User
		{
			FirstName = firstName,
			LastName = lastName,
			Email = email,
			Password = password
		};

		_userRepository.Add(user);

		string token = _jwtTokenGenerator.GenerateToken(user);

		return new AuthenticationResult(
		  user,
		  token
		);
	}

	public ErrorOr<AuthenticationResult> Login(string email, string password)
	{

		if(_userRepository.GetUserByEmail(email) is not User user || user.Password != password)
		{
			return Errors.Authentication.InvalidCredentials;
		}

		var token = _jwtTokenGenerator.GenerateToken(user);

		return new AuthenticationResult(
		  user,
		  token
		);
	}

}