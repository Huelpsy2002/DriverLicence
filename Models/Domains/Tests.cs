using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Tests
    {
        [Key]
        public int TestId { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public TestClasses TestClass { get; set; }
        public int TestClassId { get; set; }

        public TestAppointments TestAppointment { get; set; }
        public int TestAppointmentId { get; set; }
    }
}
