using ContactListApp.Business.Data;
using ContactListApp.Business.Models;

namespace Business.Test.Data;

public class FileService_Tests
{
    [Fact]
    public void LoadListFromFile_ShouldReturnEmptyList_WhenFileIsEmpty()
    {
        //Arrange
        var fileService = new FileService("TestData", "empty.json");

        if (!Directory.Exists("TestData"))
            Directory.CreateDirectory("TestData");
        File.WriteAllText("TestData/empty.json", "");

        //Act
        var result = fileService.LoadListFromFile();

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public void SaveAndLoadList_ShouldPersistDataCorrectly()
    {
        //Arrange
        var fileService = new FileService("TestData", "test.json");
        var contacts = new List<ContactEntity>
        {
            new ContactEntity
            {
                FirstName = "Andreas",
                LastName = "Laine",
                Email = "andreas@domain.com",
                PhoneNumber = "1234567890",
                StreetAddress = "Borås",
                PostalCode = "12345",
                City = "Borås"
            }
        };

        //Act
        fileService.SaveListToFile(contacts);
        var loadedContacts = fileService.LoadListFromFile();

        //Assert
        Assert.Single(loadedContacts);
        Assert.Equal("andreas@domain.com", loadedContacts.First().Email);
    }

    [Fact]
    public void LoadListFromFile_ShouldHandleInvalidJsonGracefully()
    {
        //Arrange
        var fileService = new FileService("TestData", "invalid.json");
        if (!Directory.Exists("TestData"))
            Directory.CreateDirectory("TestData");
        File.WriteAllText("TestData/invalid.json", "invalid json");

        //Act
        var result = fileService.LoadListFromFile();

        //Assert
        Assert.Empty(result);
    }
}

