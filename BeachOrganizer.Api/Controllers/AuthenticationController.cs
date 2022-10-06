using BeachOrganizer.Application.Services.Authentication;
using BeachOrganizer.Application.Services.Authentication.Commands.Register;
using BeachOrganizer.Application.Services.Authentication.Common;
using BeachOrganizer.Application.Services.Authentication.Queries;
using BeachOrganizer.Application.Services.Authentication.Queries.Login;
using BeachOrganizer.Contracts.Authentication;
using BeachOrganizer.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeachOrganizer.Api.Controllers;

[Route("[controller]")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            request.Email, 
            request.Password);
        ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);

        if (loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized, 
                title: loginResult.FirstError.Description);
        }
        
        return loginResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
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
}