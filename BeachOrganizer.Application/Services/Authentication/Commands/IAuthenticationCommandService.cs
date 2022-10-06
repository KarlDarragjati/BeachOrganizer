using BeachOrganizer.Application.Services.Authentication.Common;
using ErrorOr;

namespace BeachOrganizer.Application.Services.Authentication;

public interface IAuthenticationCommandService
{
     ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}