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
            DataContext = this;
        }

        //OnPropertyChanged()
        public event PropertyChangedEventHandler? PropertyChanged;

        private string descriptionText;
        public string DescriptionText
        {
            get { return descriptionText; }
            set
            {
                descriptionText = value;
                //dont do this
                //Description.Text = descriptionText;

                //do this
                //OnPropertyChanged()
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DescriptionText)));

            }
        }

        private string inputText;
        public string InputText
        {
            get { return inputText; }
            set
            {
                inputText = value;
                OnPropertyChanged();
            }
        }

        private string placeholderText;
        public string PlaceholderText
        {
            get { return placeholderText; }
            set
            {
                placeholderText = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Remove_Text(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button clicked, re");
            InputText = "";
            txtInput.Focus();
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
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
