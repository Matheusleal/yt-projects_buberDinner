using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Commom.Interfaces.Persistence;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}