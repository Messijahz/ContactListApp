using CommunityToolkit.Mvvm.Input;
using ContactListApp.Business.Interfaces;
using ContactListApp.Business.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Presentation.WPF.ViewModels;


public class ContactListViewModel : INotifyPropertyChanged
{
    private readonly IContactService _contactService;

public ObservableCollection<ContactEntity> Contacts { get; set; }
    public ICommand ToggleExpandCommand { get; }

public ContactListViewModel(IContactService contactService)
{
    _contactService = contactService;
    Contacts = new ObservableCollection<ContactEntity>(_contactService.GetAll());

    ToggleExpandCommand = new RelayCommand<ContactEntity?>(ToggleExpand);
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