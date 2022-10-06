using BeachOrganizer.Domain.Entities;

namespace BeachOrganizer.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);