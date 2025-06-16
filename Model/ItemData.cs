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
        private static readonly ObservableCollection<Item> _items;
        public static ReadOnlyObservableCollection<Item> Items { get; }

        static ItemData()
        {
            _items = ItemStorageService.LoadItems();
            Items = new ReadOnlyObservableCollection<Item>(_items);
            _items.CollectionChanged += (_, _) => ItemStorageService.SaveItems(_items);
        }

        public static void Add(Item item)
        {
            _items.Add(item);
        }

        public static void Remove(Item item)
        {
            _items.Remove(item);
        }

        public static void Clear()
        {
            _items.Clear();
        }
    }


}
