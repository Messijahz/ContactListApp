using Presentation.Console.MainApp.Factories;
using Presentation.Console.MainApp.Models;
using System.Diagnostics;

namespace Presentation.Console.MainApp.Services;

    public class ContactService
    {
    public List<ContactEntity> _contacts = [];
    public readonly FileService _fileService = new();

    public bool Create(ContactRegistrationForm form)
    {
        try
        {
            ContactEntity contactEntity = ContactFactory.Create(form);

            _contacts.Add(contactEntity);
            _fileService.SaveListToFile(_contacts);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public void Add(ContactEntity contactEntity)
        {
            _contacts.Add(contactEntity);
            _fileService.SaveListToFile(_contacts);
        }


        public IEnumerable<ContactEntity> GetAll()
        {
                _contacts = _fileService.LoadListFromFile();
                return _contacts;
        }

    
}

