using ContactListApp.Business.Data;
using ContactListApp.Business.Interfaces;
using ContactListApp.Business.Models;
using ContactListApp.Business.Services;
using Presentation.WPF.ViewModels;
using System.ComponentModel;
using System.Diagnostics;


namespace Presentation.WPF.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IContactService _contactService;

    private object _currentView = null!;
    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public MainViewModel(IContactService contactService)
    {
        _contactService = contactService;
        CurrentView = new ContactListViewModel(_contactService);
    }

    public void ShowContactList()
    {
        CurrentView = new ContactListViewModel(_contactService);
    }

    public void ShowAddContact()
    {
        CurrentView = new AddContactViewModel(_contactService);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void ShowEditContact(ContactEntity contact)
    {
        Debug.WriteLine($"Navigating to EditContactViewModel with: {contact.FirstName} {contact.LastName}");
        CurrentView = new EditContactViewModel(contact, _contactService);
    }
}
