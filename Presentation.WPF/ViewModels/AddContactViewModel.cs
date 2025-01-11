using ContactListApp.Business.Interfaces;
using ContactListApp.Business.Models;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Presentation.WPF.ViewModels;

public class AddContactViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private readonly IContactService _contactService;

    private ContactRegistrationForm _newContact = new();
    public ContactRegistrationForm NewContact
    {
        get => _newContact;
        set
        {
            _newContact = value;
            OnPropertyChanged(nameof(NewContact));
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public AddContactViewModel(IContactService contactService)
    {
        _contactService = contactService;

        SaveCommand = new RelayCommand<object>(SaveContact);
        CancelCommand = new RelayCommand<object>(Cancel);
    }

    private void SaveContact(object? parameter)
    {
        Debug.WriteLine($"NewContact.FirstName: {NewContact.FirstName}");
        Debug.WriteLine($"NewContact.LastName: {NewContact.LastName}");
        Debug.WriteLine($"NewContact.Email: {NewContact.Email}");
        Debug.WriteLine($"NewContact.PhoneNumber: {NewContact.PhoneNumber}");
        Debug.WriteLine($"NewContact.StreetAddress: {NewContact.StreetAddress}");
        Debug.WriteLine($"NewContact.PostalCode: {NewContact.PostalCode}");
        Debug.WriteLine($"NewContact.City: {NewContact.City}");


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
            if (Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
            {
                if (mainViewModel.CurrentView is ContactListViewModel contactListViewModel)
                {
                    contactListViewModel.Contacts.Add(
                        new ContactEntity
                        {
                            FirstName = NewContact.FirstName,
                            LastName = NewContact.LastName,
                            Email = NewContact.Email,
                            PhoneNumber = NewContact.PhoneNumber,
                            StreetAddress = NewContact.StreetAddress,
                            PostalCode = NewContact.PostalCode,
                            City = NewContact.City
                        });
                }

                mainViewModel.ShowContactList();
            }
        }
        else
        {
            MessageBox.Show("A contact with the same email address already exists.");
        }
    }   

    private void Cancel(object? parameter)
    {
        if (Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
        {
            mainViewModel.ShowContactList();
        }
    }
}
