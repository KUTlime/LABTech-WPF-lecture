using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MVVMDemo.Converters;

public class ExplicitBooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is bool boolValue
        ? boolValue
            ? Visibility.Visible
            : Visibility.Collapsed
        : Visibility.Collapsed;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is Visibility visibilityValue && visibilityValue == Visibility.Visible;
}
