using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Marketer.Common.Customers;
using Marketer.Common.Discounts;
using Marketer.Common.Discounts.Create;
using Marketer.Data.Models;
using Marketer.Desktop.Commands;
using Marketer.Desktop.Windows;

namespace Marketer.Desktop.ViewModels;

public sealed class CustomersViewModel : INotifyPropertyChanged
{
    private readonly ICustomerHandler _customerHandler;
    private readonly IDiscountHandler _discountHandler;

    private ObservableCollection<CustomerDto> _customers;
    private CustomerDto _selectedCustomer;

    public CustomersViewModel(ICustomerHandler customerHandler, IDiscountHandler discountHandler)
    {
        _customerHandler = customerHandler;
        _discountHandler = discountHandler;
        LoadCustomersAsync();
        DeleteCustomerCommand = new RelayCommand<CustomerDto>(DeleteCustomer, CanDeleteCustomer);
        AddDiscountCommand = new RelayCommand<CustomerDto>(AddDiscount, CanAddDiscount);
    }

    public ObservableCollection<CustomerDto> Customers
    {
        get => _customers;
        set
        {
            _customers = value;
            OnPropertyChanged();
        }
    }

    public CustomerDto SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            _selectedCustomer = value;
            OnPropertyChanged();
            (DeleteCustomerCommand as RelayCommand<CustomerDto>)?.RaiseCanExecuteChanged();
            (AddDiscountCommand as RelayCommand<CustomerDto>)?.RaiseCanExecuteChanged();
        }
    }

    public ICommand DeleteCustomerCommand { get; }
    public ICommand AddDiscountCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private async void LoadCustomersAsync()
    {
        var customers = await _customerHandler.GetAll(default);
        Customers = new ObservableCollection<CustomerDto>(customers);
    }

    private static bool CanDeleteCustomer(CustomerDto customer) => customer != null;

    private async void DeleteCustomer(CustomerDto customer)
    {
        try
        {
            var customerModel = new CustomerModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
            };
            await _customerHandler.Delete(customerModel, default);
            Customers.Remove(customer);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private static bool CanAddDiscount(CustomerDto customer) => customer != null;

    private async void AddDiscount(CustomerDto customer)
    {
        var discountWindow = new DiscountEntryWindow();
        if (discountWindow.ShowDialog() != true || discountWindow.Discount is not > 0)
        {
            return;
        }

        var createDiscountRequest = new CreateDiscountRequest
        {
            CustomerId = customer.Id,
            Discount = discountWindow.Discount.Value
        };

        await _discountHandler.CreateDiscount(createDiscountRequest, default);
    }
}