using System;
using System.Text.RegularExpressions;
using CafeneauaAfterHours.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CafeneauaAfterHours.ViewModels;

public partial class RezervareViewModel : ViewModelBase
{
    [ObservableProperty] private string _nume = "";
    [ObservableProperty] private string _email = "";
    [ObservableProperty] private string _telefon = "";
    [ObservableProperty] private DateTimeOffset? _data;
    [ObservableProperty] private string _ora = "";
    [ObservableProperty] private int _persoane = 2;
    [ObservableProperty] private string _observatii = "";

    [ObservableProperty] private string _eroareNume = "";
    [ObservableProperty] private string _eroareEmail = "";
    [ObservableProperty] private string _eroareTelefon = "";
    [ObservableProperty] private string _eroareData = "";
    [ObservableProperty] private string _eroareOra = "";
    [ObservableProperty] private string _eroarePersoane = "";

    [ObservableProperty] private string _rezultat = "";

    public string[] OreDisponibile { get; } = new[]
    {
        "09:00", "10:00", "11:00", "12:00", "13:00", "14:00",
        "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00"
    };

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

        if (string.IsNullOrWhiteSpace(Telefon)) { EroareTelefon = "Câmp obligatoriu"; ok = false; }
        else if (!Regex.IsMatch(Telefon.Trim(), @"^0[0-9]{9}$"))
        { EroareTelefon = "Telefon invalid"; ok = false; }
        else EroareTelefon = "";

        if (Data is null) { EroareData = "Câmp obligatoriu"; ok = false; }
        else if (Data.Value.Date < DateTime.Today)
        { EroareData = "Alege o dată din viitor"; ok = false; }
        else EroareData = "";

        if (string.IsNullOrWhiteSpace(Ora)) { EroareOra = "Alege o opțiune"; ok = false; }
        else EroareOra = "";

        if (Persoane < 1 || Persoane > 20)
        { EroarePersoane = "Alege între 1 și 20 persoane"; ok = false; }
        else EroarePersoane = "";

        if (ok)
        {
            Rezultat = "Rezervarea a fost trimisă cu succes.";
            ToastService.Show("Formular trimis");
            Reseteaza();
        }
        else
        {
            Rezultat = "";
            ToastService.Show("Verifică datele introduse");
        }
    }

    private void Reseteaza()
    {
        Nume = ""; Email = ""; Telefon = "";
        Data = null; Ora = ""; Persoane = 2; Observatii = "";
    }
}
