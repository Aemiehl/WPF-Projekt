using System;
using System.Collections.Generic;
//OnPropertyChanged()
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaktionslogik für TextBoxMod.xaml
    /// </summary>

                                                   //OnPropertyChanged()
    public partial class TextBoxMod : UserControl, INotifyPropertyChanged
    {
        public TextBoxMod()
        {
            InitializeComponent();
            //DataContext = this;
        }

        //OnPropertyChanged()
        public event PropertyChangedEventHandler? PropertyChanged;

        public static readonly DependencyProperty DescriptionTextProperty =
         DependencyProperty.Register(
             nameof(DescriptionText),
             typeof(string),
             typeof(TextBoxMod),
             new PropertyMetadata(""));

        public string DescriptionText
        {
            get => (string)GetValue(DescriptionTextProperty);
            set => SetValue(DescriptionTextProperty, value);
        }


        public static readonly DependencyProperty InputTextProperty =
        DependencyProperty.Register(
            nameof(InputText),
            typeof(string),
            typeof(TextBoxMod),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
    );

        public string InputText
        {
            get => (string)GetValue(InputTextProperty);
            set => SetValue(InputTextProperty, value);
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
        DependencyProperty.Register(
            nameof(PlaceholderText),
            typeof(string),
            typeof(TextBoxMod),
            new PropertyMetadata(""));
        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Remove_Text(object sender, RoutedEventArgs e)
        {
            InputText = "";
            txtInput.Focus();
        }

        private void TxtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text))
            {
                Placeholder.Visibility = Visibility.Visible;
            }
            else
            {
                Placeholder.Visibility = Visibility.Hidden;
            }
        }
    }
}
