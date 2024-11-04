using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Marketer.Common.Customers;
using Marketer.Common.Customers.Create;
using Marketer.Desktop.Commands;
using Marketer.Desktop.Pages;

namespace Marketer.Desktop.ViewModels;

public sealed class CreateCustomerViewModel : INotifyPropertyChanged
{
    private readonly ICustomerHandler _customerHandler;
    private readonly NavigationService _navigation;

    private string _firstName;
    private string _lastName;
    private string _age;

    public CreateCustomerViewModel(ICustomerHandler customerHandler, NavigationService navigation)
    {
        _customerHandler = customerHandler;
        _navigation = navigation;
        CreateCustomerCommand = new RelayCommand(CreateCustomer, CanCreateCustomer);
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();
            (CreateCustomerCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();
            (CreateCustomerCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }

    public string Age
    {
        get => _age;
        set
        {
            _age = value;
            OnPropertyChanged();
            (CreateCustomerCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }

    public ICommand CreateCustomerCommand { get; }

    private async void CreateCustomer()
    {
        if (int.TryParse(Age, out int age))
        {
            var request = new CreateCustomerRequest
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = age
            };

            await _customerHandler.Create(request, default);
            _navigation.NavigateTo<HomePage>();
        }
    }

    private bool CanCreateCustomer()
    {
        return !string.IsNullOrWhiteSpace(FirstName) &&
               !string.IsNullOrWhiteSpace(LastName) &&
               int.TryParse(Age, out int age) && age > 0;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}