using Marketer.Data.Models;

namespace Marketer.Common.Authorization.Register;

public class RegistrationResponse
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public UserModel User { get; set; }
}