using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Projekt.Model;
using WPF_Projekt.MVVM;

namespace WPF_Projekt.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<Item> Items => ItemData.Items;
    }
}
