using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Marketer.Common.Customers;
using Marketer.Common.Discounts;

namespace Marketer.Desktop.ViewModels
{
    public class CustomerDiscountsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CustomerDiscountDto> _customerDiscounts;
        private readonly IDiscountHandler _discountHandler;

        public CustomerDiscountsViewModel(IDiscountHandler discountHandler)
        {
            _discountHandler = discountHandler;
            LoadCustomerDiscountsAsync();
        }

        public ObservableCollection<CustomerDiscountDto> CustomerDiscounts
        {
            get => _customerDiscounts;
            set
            {
                _customerDiscounts = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void LoadCustomerDiscountsAsync()
        {
            var discounts = await _discountHandler.GetDiscounts(CancellationToken.None);
            CustomerDiscounts = new ObservableCollection<CustomerDiscountDto>(discounts);
        }
    }
}