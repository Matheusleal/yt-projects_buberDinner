using BuberDinner.Domain.UserAggregate;

namespace BuberDinner.Application.Commom.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
  string GenerateToken(User user);
}
