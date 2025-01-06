using ContactListApp.Business.Data;
using ContactListApp.Business.Models;
using ContactListApp.Business.Services;
using ContactListApp.Business.Interfaces;
using Moq;
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

    [Fact]
    public void Create_ShouldCallSaceListToFile_WhenContactIsValid()
    {
        //Arrange
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(fs => fs.LoadListFromFile()).Returns(new List<ContactEntity>());
        var contactService = new ContactService(mockFileService.Object);

        var form = new ContactRegistrationForm
        {
            FirstName = "Andreas",
            LastName = "Laine",
            Email = "andreas.laine@domain.com",
            PhoneNumber = "1234567890",
            StreetAddress = "Borås",
            PostalCode = "12345",
            City = "Borås",
        };

        //Act
        var result = contactService.Create(form);

        //Assert
        Assert.True(result);
        mockFileService.Verify(fs => fs.SaveListToFile(It.IsAny<List<ContactEntity>>()), Times.Once);
    }

    [Fact]
    public void Create_ShouldNotCallSaveListToFile_WhenEmailAlreadyExists()
    {
        //Arrange
        var existingContact = new ContactEntity { Email = "andreas.laine@domain.com" };
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(fs => fs.LoadListFromFile()).Returns(new List<ContactEntity> { existingContact });
        var contactService = new ContactService(mockFileService.Object);

        var form = new ContactRegistrationForm
        {
            FirstName = "Andreas",
            LastName = "Laine",
            Email = "andreas.laine@domain.com",
            PhoneNumber = "1234567890",
            StreetAddress = "Borås",
            PostalCode = "12345",
            City = "Borås",
        };

        //Act
        var result = contactService.Create(form);

        //Assert
        Assert.False(result);
        mockFileService.Verify(fs => fs.SaveListToFile(It.IsAny<List<ContactEntity>>()), Times.Never);
    }

    [Fact]
    public void GetAll_ShouldCallLoadListFromFile()
    {
        //Arrange
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(fs => fs.LoadListFromFile()).Returns(new List<ContactEntity>());
        var contactService = new ContactService(mockFileService.Object);

        //Act
        var contacts = contactService.GetAll();

        //Assert
        Assert.NotNull(contacts);
        mockFileService.Verify(fs => fs.LoadListFromFile(), Times.Once);
    }
}
