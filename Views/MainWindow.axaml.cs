using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using CafeneauaAfterHours.Services;
using CafeneauaAfterHours.ViewModels;

namespace CafeneauaAfterHours.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ToastService.OnToast += ShowToast;
    }

    private async void ShowToast(string message)
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            ToastText.Text = message;
            ToastBorder.IsVisible = true;
        });

        await Task.Delay(2200);

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            ToastBorder.IsVisible = false;
        });
    }

    private void OnOverlayTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.ClosePanelsCommand.Execute(null);
        }
    }
}
