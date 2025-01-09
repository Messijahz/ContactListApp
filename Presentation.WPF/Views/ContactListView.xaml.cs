using System.Windows;
using System.Windows.Controls;
using Presentation.WPF.ViewModels;

namespace Presentation.WPF.Views
{

    /// <summary>
    /// Interaction logic for ContactListView.xaml
    /// </summary>
    public partial class ContactListView : UserControl
    {
        public ContactListView()
        {
            InitializeComponent();
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.ShowAddContact();
            }
        }
    }
}