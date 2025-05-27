using System.Text;
using System.Windows;
using System.Windows.Controls;
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

        }
        


        //private void btnClickMe_Click(object sender, RoutedEventArgs e)
        //{
        //    tbHello.Text = "Button clicked!";
        //}

        //Command="SystemCommands.CloseWindowCommand"
        //            CommandTarget="{Binding RelativeSource={RelativeSource AncestorType=Window}}"
    }
}