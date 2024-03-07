using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using TicTacToe.Core.ViewModels;

namespace TicTacToe.Converters;

public class BoardCellBackgroundStateToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is BoardCellBackgroundState state
            ? state switch
            {
                BoardCellBackgroundState.WinMove => Brushes.Green,
                BoardCellBackgroundState.Normal => Application.Current.Resources["DefaultButtonBackground"],
                _ => Binding.DoNothing,
            }
            : Binding.DoNothing;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
