using ContactListApp.Business.Models;
using ContactListApp.Business.Helpers;

namespace ContactListApp.Business.Factories;

    public static class ContactFactory
    {
        public static ContactRegistrationForm Create()
        {
            return new ContactRegistrationForm();
        }

        public static ContactEntity Create(ContactRegistrationForm form)
        {
            return new ContactEntity()
            {
                Id = UniqueIdentifierGenerator.GenerateUniqueId(),
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                PhoneNumber = form.PhoneNumber,
                StreetAddress = form.StreetAddress,
                PostalCode = form.PostalCode,
                City = form.City,
            };
        }
    }

