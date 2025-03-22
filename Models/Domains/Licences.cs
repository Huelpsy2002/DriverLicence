using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Licences
    {
        [Key]
        public int LicenceNumber { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public DateTime ExpireTime { get; set; }
        public string LicenceState { get; set; }

        public int LicenceClassId { get; set; }
        public LicenceClasses LicenceClass { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Drivers Driver { get; set; }

    }
}
