using BeachOrganizer.Application.Services.Authentication;
using BeachOrganizer.Application.Services.Authentication.Commands;
using BeachOrganizer.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BeachOrganizer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();

        return services;
    }
}