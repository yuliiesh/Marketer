using System.Windows.Controls;
using Marketer.Common.Orders;
using Marketer.Desktop.ViewModels;

namespace Marketer.Desktop.Pages;

public partial class OrderDetailsPage
{
    public OrderDetailsPage(OrderDetailsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataContext is OrderDetailsViewModel viewModel && sender is DataGrid { SelectedItem: OrderDto selectedOrder })
        {
            viewModel.SelectOrderCommand.Execute(selectedOrder);
        }
    }
}