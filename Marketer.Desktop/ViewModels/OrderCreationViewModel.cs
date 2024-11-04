using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Marketer.Common.Customers;
using Marketer.Common.Orders;
using Marketer.Common.Orders.Create;
using Marketer.Data.Models;
using Marketer.Desktop.Commands;
using Marketer.Desktop.Pages;

namespace Marketer.Desktop.ViewModels;

public sealed class OrderCreationViewModel : INotifyPropertyChanged
{
    private readonly ICustomerHandler _customerHandler;
    private readonly IOrderHandler _orderHandler;
    private readonly NavigationService _navigation;

    private ObservableCollection<CustomerDto> _customers;
    private ObservableCollection<ProductModel> _products;
    private CustomerDto _selectedCustomer;
    private string _productName;
    private decimal _productPrice;

    public OrderCreationViewModel(
        ICustomerHandler customerHandler,
        IOrderHandler orderHandler,
        NavigationService navigation)
    {
        _customerHandler = customerHandler;
        _orderHandler = orderHandler;
        _navigation = navigation;
        Products = [];
        LoadCustomersAsync();

        AddProductCommand = new RelayCommand(AddProduct);
        CreateOrderCommand = new RelayCommand(CreateOrder, CanCreateOrder);
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

    public ObservableCollection<ProductModel> Products
    {
        get => _products;
        set
        {
            _products = value;
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
            ((RelayCommand)CreateOrderCommand).RaiseCanExecuteChanged();
        }
    }

    public string ProductName
    {
        get => _productName;
        set
        {
            _productName = value;
            OnPropertyChanged();
        }
    }

    public decimal ProductPrice
    {
        get => _productPrice;
        set
        {
            _productPrice = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddProductCommand { get; }
    public ICommand CreateOrderCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private async void LoadCustomersAsync()
    {
        var customers = await _customerHandler.GetAll(CancellationToken.None);
        Customers = new ObservableCollection<CustomerDto>(customers);
    }

    private void AddProduct()
    {
        if (!string.IsNullOrWhiteSpace(ProductName) && ProductPrice > 0)
        {
            Products.Add(new ProductModel
            {
                Name = ProductName,
                Id = Guid.NewGuid(),
                Price = ProductPrice
            });

            ProductName = string.Empty;
            ProductPrice = 0;
            ((RelayCommand)CreateOrderCommand).RaiseCanExecuteChanged();
        }
        else
        {
            MessageBox.Show("Please enter valid product details.");
        }
    }

    private bool CanCreateOrder() => SelectedCustomer != null && Products.Count > 0;

    private async void CreateOrder()
    {
        var createOrderRequest = new CreateOrderRequest
        {
            CustomerId = SelectedCustomer.Id,
            CreationDate = DateTime.Now,
            Products = Products
        };

        await _orderHandler.CreateOrder(createOrderRequest, default);

        MessageBox.Show("Order created successfully!");
        _selectedCustomer = null;
        _products.Clear();
        _navigation.NavigateTo<HomePage>();
    }
}