using ContactListApp.Business.Interfaces;
using ContactListApp.Business.Models;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;


namespace Presentation.WPF.ViewModels;

public class AddContactViewModel : INotifyPropertyChanged
{
    private readonly IContactService _contactService;

    public ContactRegistrationForm NewContact { get; set; } = new();

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public AddContactViewModel(IContactService contactService)
    {
        _contactService = contactService;

        SaveCommand = new RelayCommand<object>(SaveContact);
        CancelCommand = new RelayCommand<object>(Cancel);
    }

    private void SaveContact(object? parameter)
    {
        if (string.IsNullOrWhiteSpace(NewContact.FirstName) || 
            string.IsNullOrWhiteSpace(NewContact.LastName) ||
            string.IsNullOrWhiteSpace(NewContact.Email) ||
            string.IsNullOrWhiteSpace(NewContact.PhoneNumber) ||
            string.IsNullOrWhiteSpace(NewContact.StreetAddress) ||
            string.IsNullOrWhiteSpace(NewContact.PostalCode) ||
            string.IsNullOrWhiteSpace(NewContact.City))
        {
            MessageBox.Show("Please fill in all required fields.");
            return;
        }

        if (_contactService.Create(NewContact))
        {
            if (System.Windows.Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.ShowContactList();
            }
        }
        else
        {
            MessageBox.Show("Failed to save the contact. Please try again.");
        }
    }   

    private void Cancel(object? parameter)
    {
        if (System.Windows.Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
        {
            mainViewModel.ShowContactList();
        }
    }
}
