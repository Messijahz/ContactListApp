namespace Presentation.Console.MainApp.Services;

using Presentation.Console.MainApp.Factories;
using Presentation.Console.MainApp.Models;
using System;



public class MenuDialogs : IMenuDialogs
{
    private readonly ContactService _contactService = new ContactService();
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
        Console.WriteLine($"{"1.",-5} Create User");
        Console.WriteLine($"{"2.",-5} View Users");
        Console.WriteLine($"{"Q.",-5} Quit");
        Console.WriteLine("-------------------------");
        Console.Write("Choose your menu option: ");
        var option = Console.ReadLine();


        switch (option.ToLower())
        {
            case "q":
                QuitOption();
                break;

            case "1":
                CreateOption();
                break;

            case "2":
                ViewOption();
                break;

            default:
                InvalidOption();
                break; // Add break statement to avoid falling through
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
        Console.WriteLine("View");

        foreach (var contact in contacts)
        {
            Console.WriteLine($"{"Id:", -10}{contact.Id}");
            Console.WriteLine($"{"First Name:", -10}{ contact.FirstName}");
            Console.WriteLine($"{"Last Name:",-10}{contact.LastName}");
            Console.WriteLine($"{"Email:",-10}{contact.Email}");
            Console.WriteLine($"{"Phone Number:",-10}{contact.PhoneNumber}");
            Console.WriteLine($"{"Street Address:",-10}{contact.StreetAddress}");
            Console.WriteLine($"{"Postal Code:",-10}{contact.PostalCode}");
            Console.WriteLine($"{"City:",-10}{contact.City}");
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
        Console.WriteLine("Invalid option. Please try again.");
        Console.ReadKey();
    }

    public void OutputMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.ReadKey();
    }
}

