using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Projekt.Model;
using WPF_Projekt.ViewModel;

namespace WPF_Projekt.View.UserControls
{
    public partial class AppContainer : UserControl
    {
        public AppContainer()
        {
            InitializeComponent();
            AppContainerViewModel vm = new AppContainerViewModel();
            DataContext = vm;
        }

    }
}
