using BeachOrganizer.Application.Common.Interfaces.Authentication;
using BeachOrganizer.Application.Common.Interfaces.Persistence;
using BeachOrganizer.Application.Services.Authentication.Common;
using BeachOrganizer.Domain.Common.Errors;
using BeachOrganizer.Domain.Entities;
using ErrorOr;
using MediatR;

namespace BeachOrganizer.Application.Services.Authentication.Commands.Register;

public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // 1. Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        
        // 2. Create user (generate unique ID) & Persist to DB
        var user = new User()
        {
            FirstName = command.FirstName, 
            LastName = command.LastName, 
            Email = command.Email, 
            Password = command.Password
        };
        
        _userRepository.Add(user);
        
        // 3. Create JWT token
        user.Id = Guid.NewGuid();
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user, 
            token);
    }
}