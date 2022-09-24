using BeachOrganizer.Domain.Entities;

namespace BeachOrganizer.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}