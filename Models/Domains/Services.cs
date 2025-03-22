using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Services
    {
        [Key]
        public int ServiceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Fees { get; set; }
        public ICollection<Requests>Requests { get; set; }

    }
}
