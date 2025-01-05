using System.ComponentModel.DataAnnotations;

namespace ContactListApp.Business.Models;

public class ContactRegistrationForm
    {
        
        [Required (ErrorMessage = "First name is required.")]
        [MinLength(2, ErrorMessage = "First name must contain at least 2 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(2, ErrorMessage = "Last name must contain at least 2 characters.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "Email must be in a valid format (eg: username@domain.com).")] 
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(?:\+?\d{1,3})?(([-.\s()]?\d{2,4}){2,3}|\d{7,15})$", ErrorMessage = "Phone number must be a valid phone number.")] 
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Street address is required.")]
        [MinLength(2, ErrorMessage = "Street address must contain at least 2 characters.")]
        public string StreetAddress { get; set; } = null!;

        [Required(ErrorMessage = "Postal code is required.")]
        [MinLength(3, ErrorMessage = "Postal code must contain at least 3 characters.")]
        public string PostalCode { get; set; } = null!;

        [Required(ErrorMessage = "City is required.")]
        [MinLength(2, ErrorMessage = "City must contain at least 2 characters.")]
        public string City { get; set; } = null!;
    }