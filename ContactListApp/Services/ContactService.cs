using Presentation.Console.MainApp.Factories;
using Presentation.Console.MainApp.Helpers;
using Presentation.Console.MainApp.Models;
using System.Diagnostics;

namespace Presentation.Console.MainApp.Services;

    public class ContactService
    {
    private readonly List<ContactEntity> _contacts = [];

    public bool Create(ContactRegistrationForm form)
    {
        try 
        {
       
            ContactEntity contactEntity = ContactFactory.Create(form);
    
            _contacts.Add(contactEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }


    public IEnumerable<ContactEntity> GetAll()
    {
        return _contacts;
    }
}

