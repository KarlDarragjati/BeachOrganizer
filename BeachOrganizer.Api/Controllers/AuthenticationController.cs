using BeachOrganizer.Application.Common.Errors;
using BeachOrganizer.Application.Services.Authentication;
using BeachOrganizer.Contracts.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace BeachOrganizer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    // Case with custom exception
    /*[HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult =_authenticationService.Register(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password);

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
        
        return Ok(response);
    }*/
    
    // Case with errors and OneOf nuget package
    /*[HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        OneOf<AuthenticationResult, IError> registerResult =_authenticationService.Register(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password);

        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            error => Problem(statusCode: (int) error.StatusCode, title: error.ErrorMessage)
        );
    }*/
    
    // Case with errors and FluentResults nuget package
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        Result<AuthenticationResult> registerResult =_authenticationService.Register(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password);

        if (registerResult.IsSuccess)
        {
            return Ok(MapAuthResult(registerResult.Value));
        }

        var firstError = registerResult.Errors[0];

        if (firstError is DuplicateEmailError)
        {
            return Problem(statusCode: StatusCodes.Status409Conflict, title: "");
        }
        
        return Problem();
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
        return response;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult =_authenticationService.Login(
            request.Email, 
            request.Password);
        
        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
        
        return Ok(response);
    }
}