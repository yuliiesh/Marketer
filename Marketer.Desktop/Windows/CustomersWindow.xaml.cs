using Marketer.Desktop.ViewModels;

namespace Marketer.Desktop.Windows;

public partial class CustomersWindow
{
    public CustomersWindow(CustomersViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}