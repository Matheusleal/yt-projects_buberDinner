using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Commom.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
  string GenerateToken(User user);

}
