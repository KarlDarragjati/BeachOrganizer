using BeachOrganizer.Application.Common.Interfaces.Persistence;
using BeachOrganizer.Domain.Entities;

namespace BeachOrganizer.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(user => user.Email == email);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}