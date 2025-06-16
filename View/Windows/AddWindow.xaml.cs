 using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WPF_Projekt.ViewModel;

namespace WPF_Projekt.View.Windows
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow(Window parentWindow)
        {   
            Owner = parentWindow;
            InitializeComponent();
            AddWindowViewModel vm = new AddWindowViewModel();
            DataContext = vm;
        }
    }
}
