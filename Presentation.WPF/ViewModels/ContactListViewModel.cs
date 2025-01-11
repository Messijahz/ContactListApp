using CommunityToolkit.Mvvm.Input;
using ContactListApp.Business.Interfaces;
using ContactListApp.Business.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;



namespace Presentation.WPF.ViewModels;


public class ContactListViewModel : INotifyPropertyChanged
{
    private readonly IContactService _contactService;

public ObservableCollection<ContactEntity> Contacts { get; set; }
    public ICommand EditContactCommand { get; }
    public ICommand DeleteContactCommand { get; }
    public ICommand ToggleExpandCommand { get; }

public ContactListViewModel(IContactService contactService)
{
    _contactService = contactService;
    Contacts = new ObservableCollection<ContactEntity>(_contactService.GetAll());

        EditContactCommand = new RelayCommand<ContactEntity?>(EditContact);
        DeleteContactCommand = new RelayCommand<ContactEntity?>(DeleteContact);
        ToggleExpandCommand = new RelayCommand<ContactEntity?>(ToggleExpand);
}

    private void EditContact(ContactEntity? contact)
    {
        if (contact == null) return;

        Debug.WriteLine($"Edit contact: {contact.FirstName} {contact.LastName}");

        if (System.Windows.Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
        {
            mainViewModel.ShowEditContact(contact);
        }
    }

    private void DeleteContact(ContactEntity? contact)
    {
        if (contact == null) return;

        if (Guid.TryParse(contact.Id, out Guid contactId))
        {
            Contacts.Remove(contact);
        }
        if (_contactService.DeleteContact(contactId))
        {
            Contacts.Remove(contact);
        }
    }

    private void ToggleExpand(ContactEntity? contact)
{

    if (contact == null) return;


    contact.IsExpanded = !contact.IsExpanded;
    OnPropertyChanged(nameof(Contacts));
}

public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}