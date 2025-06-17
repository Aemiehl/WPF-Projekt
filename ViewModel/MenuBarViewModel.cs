using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using WPF_Projekt.View.Windows;
using WPF_Projekt.MVVM;
using WPF_Projekt.Model;
using CommunityToolkit.Mvvm.Messaging;
using WPF_Projekt.Messages;

namespace WPF_Projekt.ViewModel
{
    class MenuBarViewModel
    {

        public MenuBarViewModel()
        {
        }


        public RelayCommand ToggleDarkmodeCommand => new RelayCommand(execute => ToggleDarkmode()/*, canExecute => { return true; }*/);
        public RelayCommand OpenAddWindowCommand => new RelayCommand(execute => OpenAddWindow()/*, canExecute => { return true; }*/);

        private void ToggleDarkmode()
        {
            
        }

        private void OpenAddWindow()
        {
            WeakReferenceMessenger.Default.Send(new SetMainWindowOpacityMessage(0.7));
            AddWindow addWindow = new AddWindow(Application.Current.MainWindow);
            addWindow.ShowDialog(); //stop main until Window is closed else only show()
            WeakReferenceMessenger.Default.Send(new SetMainWindowOpacityMessage(1.0));
        }





        //private void OnDarkmodeCheckboxChanged(object sender, RoutedEventArgs e)
        //{
        //    var parentWindow = Window.GetWindow(this) as MainWindow;
        //    if (parentWindow == null || parentWindow.DarkmodeColor == null)
        //        return;

        //    if (CheckBox_Darkmode.IsChecked == true)
        //    {
        //        parentWindow.DarkmodeColor.Color = Colors.Black;
        //    }
        //    else
        //    {
        //        parentWindow.DarkmodeColor.Color = Colors.White;
        //    }
        //}

        //private void Add_Click(object sender, RoutedEventArgs e)
        //{
        //    AddWindow addWindow = new AddWindow(Window.GetWindow(this));
        //    Opacity = 0.7; // Set opacity to indicate loading state
        //    addWindow.ShowDialog(); //stop main until Window is closed else only show()
        //    Opacity = 1; // Reset opacity after creating the window
        //}
    }
}
