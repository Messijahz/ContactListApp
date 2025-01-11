using CommunityToolkit.Mvvm.Input;
using ContactListApp.Business.Interfaces;
using ContactListApp.Business.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Presentation.WPF.ViewModels
{

    public class EditContactViewModel : INotifyPropertyChanged
    {
        private readonly IContactService _contactService;

        private ContactEntity _contact = null!;
        public ContactEntity Contact
        {
            get => _contact;
            set
            {
                _contact = value;
                OnPropertyChanged(nameof(Contact));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditContactViewModel(ContactEntity contact, IContactService contactService)
        {
            _contactService = contactService;
            Contact = contact;

            Debug.WriteLine($"EditContactViewModel initialized with: {contact.FirstName} {contact.LastName}");

            SaveCommand = new RelayCommand<Object>(_ => SaveContact());
            CancelCommand = new RelayCommand<Object>(_ => Cancel());
        }

        private void SaveContact()
        {
            if (string.IsNullOrWhiteSpace(Contact.FirstName) ||
                string.IsNullOrWhiteSpace(Contact.LastName) ||
                string.IsNullOrWhiteSpace(Contact.Email))
            {
                MessageBox.Show("First Name, Last Name and Email are required");
                return;
            }

            if (_contactService.UpdateContact(Contact))
            {
                MessageBox.Show("Contact updated successfully");
            }
            else
            {
                MessageBox.Show("Failed to update contact. Please try again.");
            }

            if (Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.ShowContactList();
            }
        }

        private void Cancel()
        {
            if (Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.ShowContactList();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}