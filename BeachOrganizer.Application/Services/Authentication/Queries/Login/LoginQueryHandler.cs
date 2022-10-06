using BeachOrganizer.Application.Common.Interfaces.Authentication;
using BeachOrganizer.Application.Common.Interfaces.Persistence;
using BeachOrganizer.Application.Services.Authentication.Commands.Register;
using BeachOrganizer.Application.Services.Authentication.Common;
using BeachOrganizer.Domain.Common.Errors;
using BeachOrganizer.Domain.Entities;
using ErrorOr;
using MediatR;

namespace BeachOrganizer.Application.Services.Authentication.Queries.Login;

public class LoginQueryHandler : 
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // 1. Validate the user exists
        // ReSharper disable once ConvertTypeCheckPatternToNullCheck
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // 2. Validate the password is correct
        if (user.Password != query.Password)
        {
            return new[] { Errors.Authentication.InvalidCredentials };
        }
        
        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user, 
            token);
    }
}