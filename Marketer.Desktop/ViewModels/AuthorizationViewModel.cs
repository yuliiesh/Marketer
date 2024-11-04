using System.ComponentModel;
using System.Runtime.CompilerServices;
using Marketer.Common.Authorization;
using Marketer.Common.Authorization.Login;
using Marketer.Common.Authorization.Register;
using Marketer.Desktop.Commands;

namespace Marketer.Desktop.ViewModels;

public sealed class AuthorizationViewModel : INotifyPropertyChanged
{
    private readonly ILoginHandler _loginHandler;

    private string _message;
    private string _username;
    private string _password;
    private string _registerUsername;
    private string _registerPassword;
    private string _confirmPassword;
    private bool _isLoginEnabled;
    private bool _isRegisterEnabled;
    private bool _isBusy;

    public AuthorizationViewModel(ILoginHandler loginHandler)
    {
        _loginHandler = loginHandler;
        LoginCommand = new ReturningCommand<bool>(async _ => await Login());
        RegistrationCommand = new ReturningCommand<bool>(async _ => await Register());
    }

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
            UpdateLoginCommandCanExecute();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
            UpdateLoginCommandCanExecute();
        }
    }

    public string Message
    {
        get => _message;

        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }

    public string RegisterUsername
    {
        get => _registerUsername;

        set
        {
            _registerUsername = value;
            OnPropertyChanged();
        }
    }

    public string RegisterPassword
    {
        get => _registerPassword;
        set
        {
            _registerPassword = value;
            OnPropertyChanged();
            UpdateRegisterCommandCanExecute();
        }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            _confirmPassword = value;
            OnPropertyChanged();
            UpdateRegisterCommandCanExecute();
        }
    }

    public bool IsLoginEnabled
    {
        get => _isLoginEnabled;
        set
        {
            _isLoginEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool IsRegisterEnabled
    {
        get => _isRegisterEnabled;
        set
        {
            _isRegisterEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public IReturningCommand<bool> LoginCommand { get; }

    public IReturningCommand<bool> RegistrationCommand { get; }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async Task<bool> Login()
    {
        var response = await _loginHandler.Login(new LoginRequest { Username = Username, Password = Password },
            CancellationToken.None);
        Message = response.Success ? "Login successful" : response.ErrorMessage;
        return response.Success;
    }

    private async Task<bool> Register()
    {
        var response =
            await _loginHandler.Register(new RegistrationRequest { Username = Username, Password = Password },
                CancellationToken.None);
        Message = response.Success ? "Registration successful" : response.ErrorMessage;
        return response.Success;
    }

    private void UpdateLoginCommandCanExecute()
    {
        IsLoginEnabled = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }

    private void UpdateRegisterCommandCanExecute()
    {
        IsRegisterEnabled = !string.IsNullOrWhiteSpace(RegisterUsername)
                            && !string.IsNullOrWhiteSpace(RegisterPassword)
                            && !string.IsNullOrWhiteSpace(ConfirmPassword);
    }
}