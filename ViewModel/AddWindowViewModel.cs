using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WPF_Projekt.Model;
using WPF_Projekt.MVVM;


namespace WPF_Projekt.ViewModel
{
    partial class AddWindowViewModel : ViewModelBase  //base class for MVVM pattern, implements INotifyPropertyChanged
        //partial nun mit dem neuen CommunityToolkit.Mvvm.ComponentModel nötig
    {

        //public RelayCommand AddItemCommand => new RelayCommand(execute => AddItems()/*, canExecute => { return true; }*/);

        public AddWindowViewModel()
        {
            Item = new Item();
        }

        [ObservableProperty]
        private Item item;

        //public Item Item
        //{
        //    get { return item; }
        //    set 
        //    { 
        //        item = value;
        //        OnPropertyChanged();
        //    }
        //}

        //name automatisch das gleiche ohne Command
        [RelayCommand]
        private void AddItem()
        {
            ItemData.Add(item);
        }


    }


}
