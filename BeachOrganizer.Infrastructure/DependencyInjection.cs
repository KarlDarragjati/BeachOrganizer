using BeachOrganizer.Application.Common.Interfaces.Authentication;
using BeachOrganizer.Application.Common.Interfaces.Services;
using BeachOrganizer.Infrastructure.Authentication;
using BeachOrganizer.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeachOrganizer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}