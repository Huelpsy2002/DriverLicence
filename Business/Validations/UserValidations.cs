using DriverLicence.Data.Repositories;
using DriverLicence.Models.Domains;
using System.Text.RegularExpressions;

namespace DriverLicence.Business.Validations
{
    public class UserValidations
    {

        private List<string> _errors;

   

        public UserValidations()
        {
            _errors = new List<string>();
        }


        public  string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public  bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }




        public List<string> Validate(User user)
        {
            _errors.Clear();

            ValidateUsername(user.Username);
            ValidatePassword(user.Password);
            ValidateNationalNumber(user.NationalNumber);

            return _errors;
        }
        
        private void ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                _errors.Add("Username is required.");
                return;
            }

            if (username.Length < 3)
            {
                _errors.Add("Username must be at least 3 characters long.");
            }

            if (username.Length > 10)
            {
                _errors.Add("Username cannot exceed 10 characters.");
            }

            // Check if username contains only allowed characters
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_\-\.]+$"))
            {
                _errors.Add("Username can only contain letters, numbers, underscores, hyphens, and periods.");
            }
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                _errors.Add("Password is required.");
                return;
            }

            if (password.Length < 8)
            {
                _errors.Add("Password must be at least 8 characters long.");
            }

            if (password.Length > 20)
            {
                _errors.Add("Password cannot exceed 20 characters.");
            }

            // Check for password complexity
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            if (!(hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar))
            {
                _errors.Add("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
            }
        }

       

        private void ValidateNationalNumber(string nationalNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber))
            {
                _errors.Add("National number is required.");
                return;
            }

            if (nationalNumber.Length > 20)
            {
                _errors.Add("National number cannot exceed 20 characters.");
            }

          
        }

        
    }


}

