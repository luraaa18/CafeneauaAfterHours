using System.Text.RegularExpressions;
using CafeneauaAfterHours.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CafeneauaAfterHours.ViewModels;

public partial class ContactViewModel : ViewModelBase
{
    [ObservableProperty] private string _nume = "";
    [ObservableProperty] private string _email = "";
    [ObservableProperty] private string _subiect = "";
    [ObservableProperty] private string _mesaj = "";

    [ObservableProperty] private string _eroareNume = "";
    [ObservableProperty] private string _eroareEmail = "";
    [ObservableProperty] private string _eroareSubiect = "";
    [ObservableProperty] private string _eroareMesaj = "";
    [ObservableProperty] private string _rezultat = "";

    public string[] Subiecte { get; } = new[] { "Rezervare", "Eveniment", "Feedback" };

    [RelayCommand]
    private void Trimite()
    {
        bool ok = true;

        if (string.IsNullOrWhiteSpace(Nume)) { EroareNume = "Câmp obligatoriu"; ok = false; }
        else if (!Regex.IsMatch(Nume.Trim(), @"^[A-Za-zĂÂÎȘȚăâîșț -]{3,40}$"))
        { EroareNume = "Numele trebuie să aibă minim 3 litere"; ok = false; }
        else EroareNume = "";

        if (string.IsNullOrWhiteSpace(Email)) { EroareEmail = "Câmp obligatoriu"; ok = false; }
        else if (!Regex.IsMatch(Email.Trim(), @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
        { EroareEmail = "Email invalid"; ok = false; }
        else EroareEmail = "";

        if (string.IsNullOrWhiteSpace(Subiect)) { EroareSubiect = "Alege o opțiune"; ok = false; }
        else EroareSubiect = "";

        if (string.IsNullOrWhiteSpace(Mesaj)) { EroareMesaj = "Câmp obligatoriu"; ok = false; }
        else if (Mesaj.Trim().Length < 10)
        { EroareMesaj = "Mesajul trebuie să aibă minim 10 caractere"; ok = false; }
        else EroareMesaj = "";

        if (ok)
        {
            Rezultat = "Mesaj trimis cu succes.";
            ToastService.Show("Formular trimis");
            Nume = ""; Email = ""; Subiect = ""; Mesaj = "";
        }
        else
        {
            Rezultat = "";
            ToastService.Show("Verifică datele introduse");
        }
    }
}
