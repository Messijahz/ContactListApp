
using Presentation.Console.MainApp.Factories;
using Presentation.Console.MainApp.Models;
using Xunit;

namespace Business.Test.Factories;

public class ContactFactory_Tests
{
    [Fact]

    public void Create_ShouldReturnEmptyContactRegistrationForm()
    {

        // Arrange & Act
        var result = ContactFactory.Create();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContactRegistrationForm>(result);
    }

    [Fact]
    public void Create_ShouldReturnValidContactEntity_FromContactRegistrationForm()
    {
        // Arrange
        var contactRegistrationForm = new ContactRegistrationForm
        {
            FirstName = "Andreas",
            LastName = "Laine",
            Email = "andreas.laine@domain.com",
            PhoneNumber = "0737777777",
            StreetAddress = "Borås 7",
            PostalCode = "777 77",
            City = "Borås"
        };

        //Act
        var result = ContactFactory.Create(contactRegistrationForm);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<ContactEntity>(result);
        Assert.NotEmpty(result.Id);
        Assert.Equal(contactRegistrationForm.FirstName, result.FirstName);
        Assert.Equal(contactRegistrationForm.LastName, result.LastName);
        Assert.Equal(contactRegistrationForm.Email, result.Email);
        Assert.Equal(contactRegistrationForm.PhoneNumber, result.PhoneNumber);
        Assert.Equal(contactRegistrationForm.StreetAddress, result.StreetAddress);
        Assert.Equal(contactRegistrationForm.PostalCode, result.PostalCode);
        Assert.Equal(contactRegistrationForm.City, result.City);
    }
}


    

