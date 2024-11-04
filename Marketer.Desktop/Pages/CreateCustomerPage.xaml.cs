using Marketer.Desktop.ViewModels;

namespace Marketer.Desktop.Pages;

public partial class CreateCustomerPage
{
    public CreateCustomerPage(CreateCustomerViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}