using System.Collections.ObjectModel;
using WPF_Projekt.Model;
using WPF_Projekt.MVVM;


namespace WPF_Projekt.ViewModel
{
    internal class AddWindowViewModel : ViewModelBase  //base class for MVVM pattern and implements INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; set; }

        public AddWindowViewModel()
        {
            addItem = new Item();
            Items = new ObservableCollection<Item>();
        }

        private Item addItem;

        public Item AddItem
        {
            get { return addItem; }
            set
            {
                addItem = value;
                OnPropertyChanged();
            }
        }



        

    }


}
