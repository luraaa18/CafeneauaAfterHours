using Avalonia;
using Avalonia.Styling;
using CafeneauaAfterHours.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CafeneauaAfterHours.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private ViewModelBase _currentPage = new AcasaViewModel();
    [ObservableProperty] private string _activePage = "acasa";
    [ObservableProperty] private bool _isSettingsOpen;
    [ObservableProperty] private bool _isHelpOpen;
    [ObservableProperty] private bool _isMenuOpen;
    [ObservableProperty] private double _baseFontSize = 14;

    public MainWindowViewModel()
    {
        BaseFontSize = App.Settings.Size == "mare" ? 19 : 14;
        NavigationService.OnNavigate += page => NavigateTo(page);
    }

    [RelayCommand]
    private void NavigateTo(string page)
    {
        IsMenuOpen = false;
        ActivePage = page;
        CurrentPage = page switch
        {
            "meniu" => new MeniuViewModel(),
            "rezervare" => new RezervareViewModel(),
            "despre" => new DespreViewModel(),
            "contact" => new ContactViewModel(),
            _ => new AcasaViewModel()
        };
    }

    [RelayCommand] private void ToggleMenu() => IsMenuOpen = !IsMenuOpen;
    [RelayCommand] private void OpenSettings() => IsSettingsOpen = true;
    [RelayCommand] private void OpenHelp() => IsHelpOpen = true;

    [RelayCommand]
    private void ClosePanels()
    {
        IsSettingsOpen = false;
        IsHelpOpen = false;
    }

    [RelayCommand]
    private void SetTheme(string theme)
    {
        App.Settings.Theme = theme;
        if (Application.Current is not null)
        {
            Application.Current.RequestedThemeVariant = theme == "dark" ? ThemeVariant.Dark : ThemeVariant.Light;
        }
        ToastService.Show("Tema a fost schimbată");
    }

    [RelayCommand]
    private void SetSize(string size)
    {
        App.Settings.Size = size;
        BaseFontSize = size == "mare" ? 19 : 14;
        ToastService.Show("Mărimea textului a fost schimbată");
    }
}
