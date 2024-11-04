using Marketer.Common.Authorization.Login;
using Marketer.Common.Authorization.Register;

namespace Marketer.Common.Authorization;

public interface ILoginHandler
{
    Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken);
    Task<RegistrationResponse> Register(RegistrationRequest request, CancellationToken cancellationToken);
}