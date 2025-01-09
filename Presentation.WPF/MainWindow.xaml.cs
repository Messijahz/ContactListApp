using Presentation.WPF.ViewModels;
using System.Windows;
using System.Windows.Input;


namespace Presentation.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainViewModel();
    }

    private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            DragMove();
    }


public void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}