using DidAuthDemo.Maui.Common;
using System.Globalization;

namespace DidAuthDemo.Maui.Converters;

public class DidTypeDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((DidType)value).ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value;
    }
}
