namespace TemplateApp;

/// <summary>
/// Main application class. It represents whole application.
/// The Main method is generated behind the scenes.
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        ServiceCollection services = new();
        _serviceProvider = services.ConfigureServices().BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}
