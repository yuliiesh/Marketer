using System.Windows;
using System.Windows.Controls;

namespace Marketer.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void NavigateTo(Page page)
    {
        MainFrame.Navigate(page);
    }
}