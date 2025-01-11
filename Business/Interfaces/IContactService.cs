using ContactListApp.Business.Models;

namespace ContactListApp.Business.Interfaces;

public interface IContactService
{
    bool Create(ContactRegistrationForm form);
    IEnumerable<ContactEntity> GetAll();
    bool Add(ContactEntity contactEntity);
    bool DeleteContact(Guid contactid);
    bool UpdateContact(ContactEntity contact);
}
