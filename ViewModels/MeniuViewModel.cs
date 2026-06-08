using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CafeneauaAfterHours.Models;
using CafeneauaAfterHours.Services;
using CommunityToolkit.Mvvm.Input;

namespace CafeneauaAfterHours.ViewModels;

public partial class MeniuViewModel : ViewModelBase
{
    public ObservableCollection<MeniuItem> Cafele { get; } = new();
    public ObservableCollection<MeniuItem> Deserturi { get; } = new();

    private readonly List<MeniuItem> _toateItemurile = new()
    {
        new MeniuItem { Id = "espresso", Nume = "Espresso", Descriere = "Shot scurt și intens.", Pret = 9, Categorie = "cafea" },
        new MeniuItem { Id = "cappuccino", Nume = "Cappuccino", Descriere = "Espresso cu lapte și spumă.", Pret = 14, Categorie = "cafea" },
        new MeniuItem { Id = "latte", Nume = "Latte", Descriere = "Cafea cremoasă cu lapte.", Pret = 16, Categorie = "cafea" },
        new MeniuItem { Id = "cheesecake", Nume = "Cheesecake", Descriere = "Cu fructe de sezon.", Pret = 19, Categorie = "desert" },
        new MeniuItem { Id = "croissant", Nume = "Croissant", Descriere = "Copt dimineața.", Pret = 10, Categorie = "desert" },
        new MeniuItem { Id = "tiramisu", Nume = "Tiramisu", Descriere = "Cu cafea și cremă de mascarpone.", Pret = 21, Categorie = "desert" },
    };

    public MeniuViewModel()
    {
        var savedOrders = App.Settings.Orders;
        foreach (var item in _toateItemurile)
        {
            if (savedOrders.TryGetValue(item.Id, out var count))
                item.Comenzi = count;
        }
        Reordoneaza();
    }

    [RelayCommand]
    private void Comanda(MeniuItem item)
    {
        item.Comenzi++;

        var orders = App.Settings.Orders;
        orders[item.Id] = item.Comenzi;
        App.Settings.Orders = orders;

        Reordoneaza();
        ToastService.Show("Produs adăugat");
    }

    [RelayCommand]
    private void ReseteazaComenzi()
    {
        foreach (var item in _toateItemurile)
            item.Comenzi = 0;

        App.Settings.Orders = new Dictionary<string, int>();
        Reordoneaza();
        ToastService.Show("Comenzile au fost resetate");
    }

    private void Reordoneaza()
    {
        Cafele.Clear();
        foreach (var item in _toateItemurile.Where(i => i.Categorie == "cafea").OrderByDescending(i => i.Comenzi))
            Cafele.Add(item);

        Deserturi.Clear();
        foreach (var item in _toateItemurile.Where(i => i.Categorie == "desert").OrderByDescending(i => i.Comenzi))
            Deserturi.Add(item);
    }
}
