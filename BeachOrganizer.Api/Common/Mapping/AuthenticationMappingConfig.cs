using BeachOrganizer.Application.Services.Authentication.Commands.Register;
using BeachOrganizer.Application.Services.Authentication.Common;
using BeachOrganizer.Application.Services.Authentication.Queries.Login;
using BeachOrganizer.Contracts.Authentication;
using Mapster;

namespace BeachOrganizer.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        
        config.NewConfig<LoginRequest, LoginQuery>();
        
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}