using ErrorOr;
using MediatR;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Commom.Interfaces.Persistence;
using BuberDinner.Application.Commom.Interfaces.Authentication;

namespace BuberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.Add(user);

        string token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
          user,
          token
        );
    }
}