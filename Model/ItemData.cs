using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WPF_Projekt.Model
{

    public static class ItemData
    {
        public static ObservableCollection<Item> Items { get; private set; }

        static ItemData()
        {
            Items = ItemStorageService.LoadItems();

            // Optional: Auto-Save bei Änderungen
            Items.CollectionChanged += (_, _) => ItemStorageService.SaveItems(Items);
        }
    }
    

}
