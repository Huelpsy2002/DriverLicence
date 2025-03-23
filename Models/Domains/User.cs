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
        public Role Role { get; set; } = Role.user;

        // Foreign key reference to Person
        [Required]
        [StringLength(255)]
        public string NationalNumber { get; set; }
        public Person Person { get; set; }
        public ICollection<Certifications>Certifications { get; set; }
        public ICollection<Licences> Licences { get; set; }
        public Drivers Driver { get; set; }
        public ICollection<Requests> Requests { get; set; }
        public ICollection<Logs>Logs { get; set; }
        public ICollection<Transactions>Transactions { get; set; }
    }

}


