using BeachOrganizer.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BeachOrganizer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}