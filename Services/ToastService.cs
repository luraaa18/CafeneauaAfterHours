using System;

namespace CafeneauaAfterHours.Services;

public static class ToastService
{
    public static event Action<string>? OnToast;

    public static void Show(string message)
    {
        OnToast?.Invoke(message);
    }
}
