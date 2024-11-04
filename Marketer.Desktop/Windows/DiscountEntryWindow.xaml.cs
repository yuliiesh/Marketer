using System.Windows;

namespace Marketer.Desktop.Windows;

public partial class DiscountEntryWindow
{
    public int? Discount { get; private set; }

    public DiscountEntryWindow()
    {
        InitializeComponent();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(DiscountTextBox.Text, out int discount) && discount is >= 0 and <= 100)
        {
            Discount = discount;
            DialogResult = true;
        }
        else
        {
            MessageBox.Show(
                "Please enter a valid discount between 0 and 100.",
                "Invalid Input",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}