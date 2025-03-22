using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Drivers
    {
        [Key]
        public int DriverId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Licences Licence { get; set; }
        public int LicenceId { get; set; }
    }
}
