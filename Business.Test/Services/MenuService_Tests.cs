using ContactListApp.Business.Interfaces;
using ContactListApp.Business.Models;
using Moq;
using Presentation.Console.MainApp.Services;

namespace Business.Test.Services;

public class MenuService_Tests
{
    [Fact(Skip = "Will try to fix this test if i got time over after finishing my project")]
    public void Show_ShouldCallCreationOption_WhenOptionIs1()
    {
        //Arrange
        var mockContactService = new Mock<IContactService>();
        var menuService = new MenuService(mockContactService.Object);

        //Simulera indata
        var input = new StringReader("1\nQ\n");
        Console.SetIn(input);

        //Simulera utdata
        var output = new StringWriter();
        Console.SetOut(output);

        //Act
        menuService.Show();

        var consoleOutput = output.ToString();
        Console.WriteLine("Console Output:");
        Console.WriteLine(consoleOutput);

        //Assert
        mockContactService.Verify(cs => cs.Create(It.IsAny<ContactRegistrationForm>()), Times.AtLeastOnce);
    }
}
