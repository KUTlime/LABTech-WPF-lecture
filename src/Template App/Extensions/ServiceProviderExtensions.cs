namespace TemplateApp.Extensions;

internal static class ServiceProviderExtensions
{
    internal static IServiceCollection ConfigureServices(this IServiceCollection services) =>
        services.AddTransient<MainWindow>();
}
