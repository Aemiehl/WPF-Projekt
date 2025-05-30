using System;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Attach the custom window helper to the current window
            CustomWindowHelper.AttachAll(this, HeaderGrid, CloseButton, MinimizeButton, MaximizeButton);
            this.SizeChanged += (_, __) => UpdateScrollBarViewportSize();

            for (int i = 0; i < 115; i++)
            {
                AppContainer.Children.Add(new Button { Content = $"App {i + 1}" });
            }
        }

        private void UpdateScrollBarViewportSize()
        {
            //BodyScrollbar.ViewportSize = BodyScrollbar.Maximum * (30 / BodyScrollbar.ActualHeight);
        }

        private void OnDarkmodeCheckboxChanged(object sender, RoutedEventArgs e)
        {
            if (DarkmodeColor == null)
                return;

            if (CheckBox_Darkmode.IsChecked == true){
                DarkmodeColor.Color = Colors.Black;
            }
            else
            {
                DarkmodeColor.Color = Colors.White;
            }
        }

        //private void btnClickMe_Click(object sender, RoutedEventArgs e)
        //{
        //    tbHello.Text = "Button clicked!";
        //}

        //Command="SystemCommands.CloseWindowCommand"
        //            CommandTarget="{Binding RelativeSource={RelativeSource AncestorType=Window}}"
    }
}