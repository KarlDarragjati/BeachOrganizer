using BeachOrganizer.Domain.Entities;

namespace BeachOrganizer.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);