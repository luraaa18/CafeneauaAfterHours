using CafeneauaAfterHours.Services;
using CommunityToolkit.Mvvm.Input;

namespace CafeneauaAfterHours.ViewModels;

public partial class AcasaViewModel : ViewModelBase
{
    [RelayCommand]
    private void NavigheazaLa(string page) => NavigationService.NavigateTo(page);
}
