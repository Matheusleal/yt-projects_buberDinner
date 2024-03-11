using BuberDinner.Application.Commom.Interfaces.Persistence;
using BuberDinner.Domain.User;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();
    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(user => user.Email == email);
    }
}