using BuberDinner.Domain.UserAggregate;

namespace BuberDinner.Application.Commom.Interfaces.Persistence;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}