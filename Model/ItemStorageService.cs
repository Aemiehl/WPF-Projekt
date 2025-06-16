using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace WPF_Projekt.Model
{
    public static class ItemStorageService
    {
        // 1. Pfad im AppData-Verzeichnis vorbereiten
        private static readonly string SavePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WPF_Projekt", "items.json");

        // 2. Sicherstellen, dass der Ordner existiert
        static ItemStorageService()
        {
            var dir = Path.GetDirectoryName(SavePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static void SaveItems(ObservableCollection<Item> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SavePath, json);
        }

        public static ObservableCollection<Item> LoadItems()
        {
            if (!File.Exists(SavePath))
                return new ObservableCollection<Item>();

            var json = File.ReadAllText(SavePath);
            var items = JsonSerializer.Deserialize<ObservableCollection<Item>>(json);

            return items ?? new ObservableCollection<Item>();
        }
    }
}