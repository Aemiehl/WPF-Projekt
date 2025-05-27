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
            // Maximieren-Korrektur
            CustomWindowHelper.AttachMaximizeFix(this);

            // Head-Leiste aktivieren (Grid mit x:Name="HeaderGrid")
            CustomWindowHelper.AttachHeaderInteraction(HeaderGrid, this);

            // Button-Klicks (Button-Namen beachten)
            CustomWindowHelper.AttachButtonEvents(this, CloseButton, MinimizeButton, MaximizeButton);

            // Smart Resize Border aktivieren (dickere Ränder für Resizing)
            CustomWindowHelper.EnableSmartResizeBorder(this, new Thickness(5));
        }
       
       

        //private void btnClickMe_Click(object sender, RoutedEventArgs e)
        //{
        //    tbHello.Text = "Button clicked!";
        //}

        //Command="SystemCommands.CloseWindowCommand"
        //            CommandTarget="{Binding RelativeSource={RelativeSource AncestorType=Window}}"
    }
}