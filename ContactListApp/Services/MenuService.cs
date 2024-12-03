namespace Presentation.Console.MainApp.Services;

using Presentation.Console.MainApp.Factories;
using Presentation.Console.MainApp.Models;
using System;



public class MenuService : IMenuService
{
    private readonly ContactService _contactService = new();
    public void Show()
    {
        while (true)
        {
            MainMenu();
        }
    }

    private void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("-----------MENU----------");
        Console.WriteLine($"{"1.",-5} Create User");
        Console.WriteLine($"{"2.",-5} View Users");
        Console.WriteLine($"{"Q.",-5} Quit");
        Console.WriteLine("-------------------------");
        Console.Write("Choose your option: ");
        var option = Console.ReadLine();


        switch (option!.ToLower())
        {

            case "1":
                CreateOption();
                break;

            case "2":
                ViewOption();
                break;

            case "q":
                QuitOption();
                break;

            default:
                InvalidOption();
                break;
        }
    }


    private void CreateOption()
    {
        ContactRegistrationForm contactRegistrationForm = ContactFactory.Create();
        
        Console.Clear();

        Console.Write("Enter your first name: ");
        contactRegistrationForm.FirstName = Console.ReadLine()!;

        Console.Write("Enter your last name: ");
        contactRegistrationForm.LastName = Console.ReadLine()!;

        Console.Write("Enter your email: ");
        contactRegistrationForm.Email = Console.ReadLine()!;

        Console.Write("Enter your phone number: ");
        contactRegistrationForm.PhoneNumber = Console.ReadLine()!;

        Console.Write("Enter your street adress: ");
        contactRegistrationForm.StreetAddress = Console.ReadLine()!;

        Console.Write("Enter your postal code: ");
        contactRegistrationForm.PostalCode = Console.ReadLine()!;

        Console.Write("Enter your city: ");
        contactRegistrationForm.City = Console.ReadLine()!;

        bool result = _contactService.Create(contactRegistrationForm);

        if (result)
        {
            OutputMessage("Contact created successfully!");
        }
        else
        {
            OutputMessage("Contact creation failed!");
        }

    }

    private void ViewOption()
    {
        var contacts = _contactService.GetAll();

        Console.Clear();
        Console.WriteLine("----------USERS----------");

        foreach (var contact in contacts)
        {
            Console.WriteLine($"{"Id:", -18}{contact.Id}");
            Console.WriteLine($"{"First Name:", -18}{ contact.FirstName}");
            Console.WriteLine($"{"Last Name:",-18}{contact.LastName}");
            Console.WriteLine($"{"Email:",-18}{contact.Email}");
            Console.WriteLine($"{"Phone Number:",-18}{contact.PhoneNumber}");
            Console.WriteLine($"{"Street Address:",-18}{contact.StreetAddress}");
            Console.WriteLine($"{"Postal Code:",-18}{contact.PostalCode}");
            Console.WriteLine($"{"City:",-18}{contact.City}");
            Console.WriteLine("-------------------------");
        }

        Console.ReadKey();
    }

    private void QuitOption()
    {
        Console.Clear();
        Console.WriteLine("Do you want to exit the application? (y/n): ");
        var option = Console.ReadLine()!;

        if (option.Equals("y", StringComparison.CurrentCultureIgnoreCase))
        {
            Environment.Exit(0);
        }
    }

    private void InvalidOption()
    {
        Console.Clear();
        Console.WriteLine("Invalid option. Press any key to go back.");
        Console.ReadKey();
    }

    public void OutputMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.ReadKey();
    }

}

