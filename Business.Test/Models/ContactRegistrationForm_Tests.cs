using System.ComponentModel.DataAnnotations;
using ContactListApp.Business.Models;
using Xunit;

namespace ContactListApp.Data;

    public class ContactRegistrationForm_Tests
    {
        private bool ValidateModel(object model, out List<ValidationResult> results)
        {
            var context = new ValidationContext(model);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, true);
        }

        [Fact]
        public void FirstName_ShouldFail_IfTooShort()
        {
            //Arrange
            var form = new ContactRegistrationForm { FirstName = "A" };

            //Act
            var isValid = ValidateModel(form, out var results);

            //Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "First name must contain at least 2 characters.");
        }

        [Fact]
        public void LastName_ShouldFail_IfTooShort()
        {
            //Arrange
            var form = new ContactRegistrationForm { LastName = "A" };

            //Act
            var isValid = ValidateModel(form, out var results);

            //Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Last name must contain at least 2 characters.");
        }

        [Fact]
        public void Email_ShouldFail_IfInvalidFormat()
        {
            //Arrange
            var form = new ContactRegistrationForm { Email = "invalid-email" };

            //Act
            var isValid = ValidateModel(form, out var results);

            //Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Email must be in a valid format (eg: username@domain.com).");
        }

        [Fact]
        public void PhoneNumber_ShouldFail_IfInvalid()
        {
            //Arrange
            var form = new ContactRegistrationForm { PhoneNumber = "123abc" };

            //Act
            var isValid = ValidateModel(form, out var results);

            //Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Phone number must be a valid phone number.");
        }

        [Fact]
        public void StreetAddress_ShouldFail_IfTooShort()
        {
            //Arrange
            var form = new ContactRegistrationForm { StreetAddress = "A" };

            //Act
            var isValid = ValidateModel(form, out var results);

            //Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Street address must contain at least 2 characters.");
        }

        [Fact]
        public void PostalCode_ShouldFail_IfTooShort()
        {
            //Arrange
            var form = new ContactRegistrationForm { PostalCode = "1" };

            //Act
            var isValid = ValidateModel(form, out var results);

            //Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Postal code must contain at least 3 characters.");
        }

        [Fact]
        public void City_ShouldFail_IfTooShort()
        {
            //Arrange
            var form = new ContactRegistrationForm { City = "A" };

            //Act
            var isValid = ValidateModel(form, out var results);

            //Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "City must contain at least 2 characters.");
        }

        [Fact]
        public void AllFields_ShouldPass_IfValid()
        {
            //Arrange
            var form = new ContactRegistrationForm
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
            var isValid = ValidateModel(form, out var results);

            //Assert
            Assert.True(isValid);
            Assert.Empty(results);
        }
    }

