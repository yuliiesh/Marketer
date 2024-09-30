using Marketer.Authorization.Login;
using Marketer.Authorization.Registration;

namespace Marketer.Authorization;

public interface ILoginHandler
{
    Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken);
    Task<RegistrationResponse> Register(RegistrationRequest request, CancellationToken cancellationToken);
}