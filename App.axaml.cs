using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using CafeneauaAfterHours.Services;
using CafeneauaAfterHours.ViewModels;
using CafeneauaAfterHours.Views;

namespace CafeneauaAfterHours;

public partial class App : Application
{
    public static SettingsService Settings { get; } = new();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Settings.Load();
        RequestedThemeVariant = Settings.Theme == "dark" ? ThemeVariant.Dark : ThemeVariant.Light;

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
