using System.Windows;
using TicTacToe.Core.ViewModels;

namespace TicTacToe;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void BtnCloseClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
}
