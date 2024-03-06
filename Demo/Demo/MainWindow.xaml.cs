using System.Windows;
using System.Windows.Controls;

namespace Demo;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private async void ButtonMiddleClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
        {
            Log.Text += $"\n[{DateTime.UtcNow}] [Thead: {Thread.CurrentThread.ManagedThreadId}]";
            await Task.Run(async () =>
            {
                int taskRunThreadId = Thread.CurrentThread.ManagedThreadId;
                _ = await Dispatcher.InvokeAsync(() => btn.IsEnabled = false);
                await Task.Delay(5000);
                await Dispatcher.InvokeAsync(() =>
                {
                    Log.Text += $"\n[{DateTime.UtcNow}] [Thead: {taskRunThreadId}] Hello world!";
                    btn.IsEnabled = true;
                });
            });
        }
    }

    private async void CheckBoxSettingsClick(object sender, RoutedEventArgs e) => await Task.Run(async () =>
    {
        if (sender is CheckBox box)
        {
            string suffix = "unchecked";
            if (await Dispatcher.InvokeAsync(() => box.IsChecked == true))
            {
                suffix = "checked";
            }

            _ = await Dispatcher.InvokeAsync(() => Log.Text += $"\n[{DateTime.UtcNow}] {box.Content} has been {suffix}.");
        }
    });
}
