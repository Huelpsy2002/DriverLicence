using DriverLicence.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace DriverLicence.Models.Domains
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }

        public bool Active { get; set; } = true;

        [StringLength(20)]
        public string Role { get; set; } = "user";

        // Foreign key reference to Person
        [Required]
        [StringLength(255)]
        public string NationalNumber { get; set; }
        public Person Person { get; set; }
    }

}


