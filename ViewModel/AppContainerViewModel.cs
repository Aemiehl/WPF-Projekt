using System.Collections.ObjectModel;
using System.Windows;
using WPF_Projekt.Model;
using WPF_Projekt.MVVM;
using WPF_Projekt.View.UserControls;
using System.Windows.Controls;

namespace WPF_Projekt.ViewModel
{
    internal class AppContainerViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<Item> Items => ItemData.Items;
    }
}
