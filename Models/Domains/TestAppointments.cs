using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class TestAppointments
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FinalScore { get; set; }
        public Requests Request { get; set; }
        public int RequestNumber { get; set; }

        public LicenceClasses LicenceClass { get; set; }
        public int LicenceClassId { get; set; }

        public ICollection<Tests>Tests { get; set; }
    }
}
