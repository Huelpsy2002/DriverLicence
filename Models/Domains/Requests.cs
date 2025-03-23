using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Requests
    {
        [Key]
        public int RequestNumber { get; set; }
        public DateTime Date { get; set; }
        public string state { get; set; }
        public int TotalFees { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public Services Services { get; set; }
        public int ServiceId { get; set; }
        public TestAppointments TestAppointment { get; set; }
    }
}
