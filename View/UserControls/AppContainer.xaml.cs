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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Projekt.View.UserControls
{
    /// <summary>
    /// Interaktionslogik für AppContainer.xaml
    /// </summary>
    public partial class AppContainer : UserControl
    {
        public AppContainer()
        {
            InitializeComponent();
            for (int i = 0; i < 115; i++)
            {
                AppList.Children.Add(new Button { Content = $"App {i + 1}" });
            }
        }
    }
}
