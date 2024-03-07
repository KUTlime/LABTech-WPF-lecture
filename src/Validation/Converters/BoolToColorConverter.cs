using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Validation.Converters;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is bool hasError
        ? hasError
            ? Brushes.Red
            : Brushes.Transparent
        : (object)Brushes.Transparent;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
