using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WPF_Projekt.Model;
using WPF_Projekt.MVVM;


namespace WPF_Projekt.ViewModel
{
    internal class AddWindowViewModel : ViewModelBase  //base class for MVVM pattern and implements INotifyPropertyChanged
    {

        public RelayCommand AddItemCommand => new RelayCommand(execute => AddItems()/*, canExecute => { return true; }*/);

        public AddWindowViewModel()
        {
            Item = new Item();
        }


        private Item item;

        public Item Item
        {
            get { return item; }
            set 
            { 
                item = value;
                OnPropertyChanged();
            }
        }

        private void AddItems()
        {
            ItemData.Add(item);
        }


    }


}
