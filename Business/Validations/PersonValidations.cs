using DriverLicence.Models.Domains;
using System.Text.RegularExpressions;

namespace DriverLicence.Business.Validations
{
    public class PersonValidations
    {


        private List<string> _errors;

        public PersonValidations()
        {
            _errors = new List<string>();
        }

        public List<string> Validate(Person person)
        {
            _errors.Clear();
            ValidateNationalNumber(person.NationalNumber);
            ValidateFullName(person.FullName);
            ValidateBirthDate(person.BirthDate);
            ValidatePhoneNumber(person.PhoneNumber);
            ValidateEmail(person.Email);
            ValidateNationality(person.Nationality);
            ValidateGender(person.Gender);
            return _errors;
        }

        private void ValidateNationalNumber(string nationalNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber))
            {
                _errors.Add("National number is required.");
                return;
            }
            if (nationalNumber.Length > 15)
            {
                _errors.Add("National number cannot exceed 15 characters.");
            }
        }

        private void ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                _errors.Add("Full name is required.");
                return;
            }
            if (fullName.Length > 25)
            {
                _errors.Add("Full name cannot exceed 25 characters.");
            }
            // Check if full name contains only allowed characters
            if (!Regex.IsMatch(fullName, @"^[a-zA-Z\s\-'\.]+$"))
            {
                _errors.Add("Full name can only contain letters, spaces, hyphens, apostrophes, and periods.");
            }
        }

        private void ValidateBirthDate(DateTime birthDate)
        {
            if (birthDate == default)
            {
                _errors.Add("Birth date is required.");
                return;
            }
            if (birthDate > DateTime.Now)
            {
                _errors.Add("Birth date cannot be in the future.");
            }

            // Check if person is at least 16 years old (assuming this is for a driver's license application)
            var minAge = DateTime.Now.AddYears(-16);
            if (birthDate > minAge)
            {
                _errors.Add("Person must be at least 16 years old.");
            }
        }

        private void ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                _errors.Add("phone number is required");
                return;
            }
            if (phoneNumber.Length > 20)
            {
                _errors.Add("Phone number cannot exceed 20 characters.");
            }
            // Basic phone number format check
            if (!Regex.IsMatch(phoneNumber, @"^\+?[0-9\s\-\(\)]+$"))
            {
                _errors.Add("Phone number format is invalid.");
            }
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _errors.Add("email is required");
                return;
            }
            if (email.Length > 30)
            {
                _errors.Add("Email cannot exceed 30 characters.");
            }
            // Email format check
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _errors.Add("Email format is invalid.");
            }
        }

        private void ValidateNationality(string nationality)
        {
            if (string.IsNullOrWhiteSpace(nationality))
            {
                _errors.Add("nationality is required");
                return;
            }
            if (nationality.Length > 30)
            {
                _errors.Add("Nationality cannot exceed 30 characters.");
            }
        }

        private void ValidateGender(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
            {
                _errors.Add("gender is required");
                return;
            }
            if (gender.Length > 10)
            {
                _errors.Add("Gender cannot exceed 10 characters.");
            }

            // Check if gender is one of the allowed values
            var allowedGenders = new[] { "Male", "Female"};
            if (!allowedGenders.Contains(gender))
            {
                _errors.Add("Gender must be 'Male', 'Female'");
            }
        }
    }
}