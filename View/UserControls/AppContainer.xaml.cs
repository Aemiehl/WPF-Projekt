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
            //for (int i = 0; i < 115; i++)
            //{
            //    AppList.Children.Add(new Button { Content = $"App {i + 1}" });
            //}
        }
        public ObservableCollection<Item> Items
        {
            get => (ObservableCollection<Item>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(nameof(Items),
                typeof(ObservableCollection<Item>),
                typeof(AppContainer),
                new PropertyMetadata(null));



    }
}
