using DriverLicence.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Person
    {
        

        [Key]
        [StringLength(255)]
        public string NationalNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Nationality { get; set; }

        public string ImagePath { get; set; }

        [StringLength(10)]
        public Gender Gender { get; set; }

        public  User User { get; set; }


    }
}
