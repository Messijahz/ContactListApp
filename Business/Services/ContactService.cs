using ContactListApp.Business.Factories;
using ContactListApp.Business.Models;
using System.Diagnostics;
using ContactListApp.Business.Interfaces;


namespace ContactListApp.Business.Services;

public class ContactService : IContactService
{
    private List<ContactEntity> _contacts = new();
    public readonly IFileService _fileService;

    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public bool Create(ContactRegistrationForm form)
    {
        try
        {
            _contacts = _fileService.LoadListFromFile();

            if (_contacts.Any(c => !string.IsNullOrWhiteSpace(c.Email) && c.Email.Equals(form.Email, StringComparison.OrdinalIgnoreCase)))
            {
                Debug.WriteLine("A contact with this email already exists. Operation aborted.");
                return false;
            }

            var contactEntity = ContactFactory.Create(form);
            _contacts.Add(contactEntity);

            _fileService.SaveListToFile(_contacts);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating contact: {ex.Message}");
            return false;
        }
    }

    public IEnumerable<ContactEntity> GetAll()
    {
        _contacts = _fileService.LoadListFromFile();
        return _contacts;
    }

    public bool Add(ContactEntity contactEntity)
    {
        try
        {
            _contacts = _fileService.LoadListFromFile();

            if (_contacts.Any(c => c.Email.Equals(contactEntity.Email, StringComparison.OrdinalIgnoreCase)))
            {
                Debug.WriteLine("A contact with this email already exists.");
                return false;
            }

            _contacts.Add(contactEntity);
            _fileService.SaveListToFile(_contacts);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding contact: {ex.Message}");
            return false;
        }
    }

    public bool UpdateContact(ContactEntity updatedContact)
    {
       var contact = _contacts.FirstOrDefault(c => c.Id == updatedContact.Id);
        if (contact == null)
        {
            Debug.WriteLine("Contact not found.");
            return false;
        }

        contact.FirstName = updatedContact.FirstName;
        contact.LastName = updatedContact.LastName;
        contact.Email = updatedContact.Email;
        contact.PhoneNumber = updatedContact.PhoneNumber;
        contact.StreetAddress = updatedContact.StreetAddress;
        contact.PostalCode = updatedContact.PostalCode;
        contact.City = updatedContact.City;
      

        _fileService.SaveListToFile(_contacts);
        return true;
    }

    public bool DeleteContact(Guid contactId)
    {
        var contact = _contacts.FirstOrDefault(c => Guid.TryParse(c.Id, out Guid contactGuid) && contactGuid == contactId);

        if (contact == null)
        {
            Debug.WriteLine("Contact not found.");
            return false;
        }

        _contacts.Remove(contact);
        _fileService.SaveListToFile(_contacts);
        return true;
    }
}

