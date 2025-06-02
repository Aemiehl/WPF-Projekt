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
    /// Interaktionslogik für TaskBar.xaml
    /// </summary>
    public partial class TaskBar : UserControl
    {
        public TaskBar()
        {
            InitializeComponent();
            this.Loaded += TaskBar_Loaded;
            
        }
        private void TaskBar_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window == null)
                return; // zur Sicherheit

            CustomWindowHelper.AttachAll(window, HeaderGrid, CloseButton, MinimizeButton, MaximizeButton);
        }
    }
}
