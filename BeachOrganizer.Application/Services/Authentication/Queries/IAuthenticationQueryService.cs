using BeachOrganizer.Application.Services.Authentication.Common;
using ErrorOr;

namespace BeachOrganizer.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
     ErrorOr<AuthenticationResult> Login(string email, string password);
}