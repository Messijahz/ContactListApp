using ContactListApp.Business.Data;
using ContactListApp.Business.Services;
using System.ComponentModel;

namespace Presentation.WPF.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly ContactService _contactService;

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

    public MainViewModel()
    {

        var fileService = new FileService();
        _contactService = new ContactService(fileService);

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
}
