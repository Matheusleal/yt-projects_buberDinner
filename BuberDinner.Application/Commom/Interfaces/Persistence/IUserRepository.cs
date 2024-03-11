using BuberDinner.Domain.User;

namespace BuberDinner.Application.Commom.Interfaces.Persistence;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}