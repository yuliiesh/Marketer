using Marketer.Data.Models;

namespace Marketer.Authorization.Registration;

public class RegistrationResponse
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public UserModel User { get; set; }
}