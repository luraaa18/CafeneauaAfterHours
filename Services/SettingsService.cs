using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CafeneauaAfterHours.Services;

public class SettingsService
{
    private readonly string _filePath;
    private Data _data = new();

    public SettingsService()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AfterHours");
        Directory.CreateDirectory(folder);
        _filePath = Path.Combine(folder, "settings.json");
    }

    public string Theme
    {
        get => _data.Theme;
        set { _data.Theme = value; Save(); }
    }

    public string Size
    {
        get => _data.Size;
        set { _data.Size = value; Save(); }
    }

    public Dictionary<string, int> Orders
    {
        get => _data.Orders;
        set { _data.Orders = value; Save(); }
    }

    public void Load()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _data = JsonSerializer.Deserialize<Data>(json) ?? new Data();
            }
        }
        catch { _data = new Data(); }
    }

    public void Save()
    {
        try
        {
            var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch { }
    }

    private class Data
    {
        public string Theme { get; set; } = "light";
        public string Size { get; set; } = "normal";
        public Dictionary<string, int> Orders { get; set; } = new();
    }
}
