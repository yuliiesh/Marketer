using System.Globalization;
using System.Windows.Data;

namespace Marketer.Desktop.Converters;

public class AndBooleanConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length < 2)
        {
            return false;
        }

        foreach (var value in values)
        {
            if (value is bool b && !b)
            {
                return false;
            }
        }

        return true;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
}