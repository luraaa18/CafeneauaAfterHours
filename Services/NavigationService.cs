using System;

namespace CafeneauaAfterHours.Services;

public static class NavigationService
{
    public static event Action<string>? OnNavigate;

    public static void NavigateTo(string page)
    {
        OnNavigate?.Invoke(page);
    }
}
