using CommunityToolkit.Mvvm.ComponentModel;

namespace CafeneauaAfterHours.Models;

public partial class MeniuItem : ObservableObject
{
    public string Id { get; set; } = "";
    public string Nume { get; set; } = "";
    public string Descriere { get; set; } = "";
    public int Pret { get; set; }
    public string Categorie { get; set; } = "";

    [ObservableProperty] private int _comenzi;

    public bool EstePopular => Comenzi > 0;

    partial void OnComenziChanged(int value)
    {
        OnPropertyChanged(nameof(EstePopular));
        OnPropertyChanged(nameof(TextComenzi));
    }

    public string TextComenzi => $"{Comenzi} comenzi";
}
