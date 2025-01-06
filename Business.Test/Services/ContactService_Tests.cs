using ContactListApp.Business.Data;
using ContactListApp.Business.Models;
using ContactListApp.Business.Services;
using Xunit;

namespace Business.Test.Services;

public class ContactService_Tests
{

    [Fact]
    public void Create_ShouldFail_WhenEmailAlreadyExists()
    {
        //Arrange
        var contactService = new ContactService(new FileService());
        var form1 = new ContactRegistrationForm
        {
            FirstName = "Andreas",
            LastName = "Laine",
            Email = "andreas@domain.com",
            PhoneNumber = "1234567890",
            StreetAddress = "Borås",
            PostalCode = "12345",
            City = "Borås"
        };
        var form2 = new ContactRegistrationForm
        {
            FirstName = "Kristoffer",
            LastName = "Laine",
            Email = "andreas@domain.com",
            PhoneNumber = "0987654321",
            StreetAddress = "Göteborg",
            PostalCode = "54321",
            City = "Göteborg"
        };

        //Act
        var result = contactService.Create(form2);

        //Assert
        Assert.False(result);
    }
}
