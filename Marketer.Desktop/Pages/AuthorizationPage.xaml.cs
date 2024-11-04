using System.Windows;
using System.Windows.Controls;
using Marketer.Desktop.ViewModels;

namespace Marketer.Desktop.Pages;

public partial class AuthorizationPage
{
    private readonly AuthorizationViewModel _viewModel;
    private readonly NavigationService _navigation;

    public AuthorizationPage(
        AuthorizationViewModel viewModel,
        NavigationService navigation)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _navigation = navigation;
        DataContext = _viewModel;

        UsernameTextBox.TextChanged += (s, e) => UpdateLoginButtonState();
        PasswordTextBox.PasswordChanged += (s, e) => UpdateLoginButtonState();

        RegisterUsernameTextBox.TextChanged += (s, e) => UpdateRegisterButtonState();
        RegisterPasswordTextBox.PasswordChanged += (s, e) => UpdateRegisterButtonState();
        ConfirmPasswordTextBox.PasswordChanged += (s, e) => UpdateRegisterButtonState();
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Password))
        {
            MessageBox.Show(
                "Please fill out all required fields.",
                "Validation Error",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        try
        {
            _viewModel.IsBusy = true;

            var success = await _viewModel.LoginCommand.ExecuteAsync(null);
            if (success)
            {
                _navigation.NavigateTo<HomePage>();
            }
        }
        finally
        {
            _viewModel.IsBusy = false;
        }
    }

    private async void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(RegisterUsernameTextBox.Text) ||
            string.IsNullOrWhiteSpace(RegisterPasswordTextBox.Password) ||
            string.IsNullOrWhiteSpace(ConfirmPasswordTextBox.Password))
        {
            MessageBox.Show(
                "Please fill out all required fields.",
                "Validation Error",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        try
        {
            _viewModel.IsBusy = true;

            var success = await _viewModel.RegistrationCommand.ExecuteAsync(null);
            if (success)
            {
                _navigation.NavigateTo<HomePage>();
            }
        }
        finally
        {
            _viewModel.IsBusy = false;
        }
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is AuthorizationViewModel viewModel)
        {
            viewModel.Password = ((PasswordBox)sender).Password;
        }
    }

    private void UpdateLoginButtonState()
    {
        _viewModel.IsLoginEnabled = !string.IsNullOrWhiteSpace(UsernameTextBox.Text)
                                    && !string.IsNullOrWhiteSpace(PasswordTextBox.Password);
    }

    private void UpdateRegisterButtonState()
    {
        _viewModel.IsRegisterEnabled = !string.IsNullOrWhiteSpace(RegisterUsernameTextBox.Text)
                                       && !string.IsNullOrWhiteSpace(RegisterPasswordTextBox.Password)
                                       && !string.IsNullOrWhiteSpace(ConfirmPasswordTextBox.Password);
    }
}