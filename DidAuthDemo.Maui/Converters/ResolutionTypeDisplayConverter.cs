using System.Globalization;

namespace DidAuthDemo.Maui.Converters;

public class ResolutionTypeDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var didParts = value.ToString().Split(':');
        return didParts[1] switch
        {
            "web" => "Web",
            "github" => "Github",
            _ => "Unknown"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
