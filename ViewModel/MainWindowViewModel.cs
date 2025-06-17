using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Projekt.Model;
using WPF_Projekt.MVVM;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using WPF_Projekt.Messages;
using System.Windows;

namespace WPF_Projekt.ViewModel
{
    class MainWindowViewModel : ViewModelBase, IRecipient<SetMainWindowOpacityMessage>
    {
        //nicht mehr nötig da in AppContainerViewModel direkt eingebunden
        //public ReadOnlyObservableCollection<Item> Items => ItemData.Items;

        private double _mainOpacity = 1.0;
        public double MainOpacity
        {
            get => _mainOpacity;
            set => SetProperty(ref _mainOpacity, value);
        }

        public void Receive(SetMainWindowOpacityMessage message)
        {
            MainOpacity = message.Value;
        }

        public MainWindowViewModel()
        {
            IsActive = true; // WICHTIG! Sonst empfängt ViewModel keine Nachrichten
        }
    }
}
