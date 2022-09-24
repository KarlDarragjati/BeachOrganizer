using BeachOrganizer.Domain.Entities;

namespace BeachOrganizer.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}