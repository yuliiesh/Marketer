using Marketer.Desktop.ViewModels;

namespace Marketer.Desktop.Pages;

public partial class CustomerDiscountPage
{
    public CustomerDiscountPage(CustomerDiscountsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}