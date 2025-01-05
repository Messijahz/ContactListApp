using ContactListApp.Business.Factories;
using ContactListApp.Business.Data;
using ContactListApp.Business.Models;
using System.Diagnostics;

namespace ContactListApp.Business.Services;

public class ContactService
    {
        public List<ContactEntity> _contacts = [];
        public readonly FileService _fileService = new();

        public bool Create(ContactRegistrationForm form)
        {
            try
            {
                _contacts = _fileService.LoadListFromFile();

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

