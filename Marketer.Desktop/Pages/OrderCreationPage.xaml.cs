using System.Windows.Controls;
using Marketer.Desktop.ViewModels;

namespace Marketer.Desktop.Pages;

public partial class OrderCreationPage : Page
{
    public OrderCreationPage(OrderCreationViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}