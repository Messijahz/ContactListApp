using System.ComponentModel.DataAnnotations;
using ContactListApp.Business.Factories;
using ContactListApp.Business.Models;
using ContactListApp.Business.Interfaces;

namespace Presentation.Console.MainApp.Services;


using System;




public class MenuService : IMenuService
{
    private readonly IContactService _contactService;

    public MenuService(IContactService contactService)
    {
        _contactService = contactService;
    }

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
        Console.WriteLine($"{"1.",-5} Create Contact");
        Console.WriteLine($"{"2.",-5} View Contact");
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
        Console.Clear();

        var contactRegistrationForm = ContactFactory.Create();

        contactRegistrationForm.FirstName = PromptAndValidate("Enter your first name: ", nameof(contactRegistrationForm.FirstName));
        contactRegistrationForm.LastName = PromptAndValidate("Enter your last name: ", nameof(contactRegistrationForm.LastName));
        contactRegistrationForm.Email = PromptAndValidate("Enter your email: ", nameof(contactRegistrationForm.Email));
        contactRegistrationForm.PhoneNumber = PromptAndValidate("Enter your phone number: ", nameof(contactRegistrationForm.PhoneNumber));
        contactRegistrationForm.StreetAddress = PromptAndValidate("Enter your street address: ", nameof(contactRegistrationForm.StreetAddress));
        contactRegistrationForm.PostalCode = PromptAndValidate("Enter your postal code: ", nameof(contactRegistrationForm.PostalCode));
        contactRegistrationForm.City = PromptAndValidate("Enter your city: ", nameof(contactRegistrationForm.City));

        var result = _contactService.Create(contactRegistrationForm);

        OutputMessage(result ? "Contact created successfully!" : "Contact creation failed!");
    }

    private void ViewOption()
    {
        var contacts = _contactService.GetAll();

        Console.Clear();
        Console.WriteLine("---------CONTACTS---------");

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


    public string PromptAndValidate(string prompt, string propertyName)
    {

        var form = new ContactRegistrationForm();

        while (true)
        {
            Console.WriteLine();
            Console.Write(prompt);
            var input = Console.ReadLine() ?? string.Empty;

            var results = new List<ValidationResult>();

            var property = form.GetType().GetProperty(propertyName);
            property?.SetValue(form, input);

            var context = new ValidationContext(form) { MemberName = propertyName };

            if (Validator.TryValidateProperty(input, context, results))
            {
                return input;
            }

            Console.WriteLine($"{results[0].ErrorMessage}. Please try again.");

        }

    }
}