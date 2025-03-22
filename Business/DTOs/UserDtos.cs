using DriverLicence.Models.Domains;
using DriverLicence.Business.DTOs;
namespace DriverLicence.Business.DTOs
{
    // For retrieving user data (GET operations)
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
        public string NationalNumber { get; set; }
        public PersonDto personDto { get; set; }
    }

    // For creating a new user (POST operations)
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }        
        public PersonCreateDto personCreateDto { get; set; }
    }

    // For updating a user (PUT operations)
    public class UpdateUserDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; } // Optional - can be null if not changing
        public bool? Active { get; set; }
        public string? Role { get; set; }
        public PersonUpdateDto personUpdateDto {get;set;}
    }

    // For authentication
    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    // For user authentication response
    public class UserAuthDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; } // For JWT or other auth tokens
    }
}
