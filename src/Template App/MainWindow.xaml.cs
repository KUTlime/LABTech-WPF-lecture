using System.Windows.Controls;

namespace TemplateApp;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
        {
            int row = Grid.GetRow(btn);
            Grid.SetRow(btn, row == 0 ? 1 : 0);
        }
    }
}
