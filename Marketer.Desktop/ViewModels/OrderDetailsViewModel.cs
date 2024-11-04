using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Marketer.Common.Customers;
using Marketer.Common.Orders;
using Marketer.Data.Models;
using Marketer.Desktop.Commands;

namespace Marketer.Desktop.ViewModels;

public class OrderDetailsViewModel : INotifyPropertyChanged
{
    private readonly ICustomerHandler _customerHandler;
    private readonly IOrderHandler _orderHandler;

    private ObservableCollection<CustomerDto> _customers;
    private CustomerDto _selectedCustomer;
    private ObservableCollection<OrderDto> _orders;
    private ObservableCollection<ProductModel> _products;

    public OrderDetailsViewModel(ICustomerHandler customerHandler, IOrderHandler orderHandler)
    {
        _customerHandler = customerHandler;
        _orderHandler = orderHandler;
        LoadCustomersAsync();

        SelectOrderCommand = new RelayCommand<OrderDto>(SelectOrder);
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
            if (_selectedCustomer != null)
            {
                LoadOrdersAsync(_selectedCustomer.Id);
            }
        }
    }

    public ObservableCollection<OrderDto> Orders
    {
        get => _orders;

        set
        {
            _orders = value;
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

    public ICommand SelectOrderCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private async void LoadCustomersAsync()
    {
        var customers = await _customerHandler.GetAll(CancellationToken.None);
        Customers = new ObservableCollection<CustomerDto>(customers.Where(x => x.OrdersCount != 0));
    }

    private async void LoadOrdersAsync(Guid customerId)
    {
        var orders = await _orderHandler.GetOrders(customerId, CancellationToken.None);
        Orders = new ObservableCollection<OrderDto>(orders);
        Products = [];
    }

    private void SelectOrder(OrderDto order)
    {
        Products = new ObservableCollection<ProductModel>(order.Products);
    }
}