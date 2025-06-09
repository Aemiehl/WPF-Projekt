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

        }

        private Item addItem;


        public Item AddItem
        {
            get { return AddItem; }
            set 
            { 
                AddItem = value;
                OnPropertyChanged();
            }
        }



        

    }


}
