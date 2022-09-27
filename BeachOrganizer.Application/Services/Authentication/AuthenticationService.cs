using BeachOrganizer.Application.Common.Errors;
using BeachOrganizer.Application.Common.Interfaces.Authentication;
using BeachOrganizer.Application.Common.Interfaces.Persistence;
using BeachOrganizer.Domain.Entities;
using FluentResults;
using OneOf;

namespace BeachOrganizer.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            // Case with custom exception
            //throw new DuplicateEmailException();
            
            // Case with errors and OneOf nuget package
            //return new DuplicateEmailError()
            
            // Case with errors and FluentResults nuget package
            return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
        }
        
        // 2. Create user (generate unique ID) & Persist to DB
        var user = new User()
        {
            FirstName = firstName, 
            LastName = lastName, 
            Email = email, 
            Password = password
        };
        
        _userRepository.Add(user);
        
        // 3. Create JWT token
        user.Id = Guid.NewGuid();
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user, 
            token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1. Validate the user exists
        // ReSharper disable once ConvertTypeCheckPatternToNullCheck
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email does not exist.");
        }
        
        // 2. Validate the password is correct
        if (user.Password != password)
        {
            throw new Exception("Invalid password.");
        }
        
        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new(
            user, 
            token);
    }
}