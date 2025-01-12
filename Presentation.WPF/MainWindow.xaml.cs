using Presentation.WPF.ViewModels;
using System.Windows;
using System.Windows.Input;


namespace Presentation.WPF;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
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