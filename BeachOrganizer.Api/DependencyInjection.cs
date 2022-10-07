using BeachOrganizer.Api.Common.Errors;
using BeachOrganizer.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BeachOrganizer.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, BeachOrganizerProblemDetailsFactory>();
        services.AddMappings();
        
        return services;
    }
}