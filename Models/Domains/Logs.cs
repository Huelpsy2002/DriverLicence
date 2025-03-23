using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Logs
    {
        [Key]
        public int LogId { get; set; }
        public string infos { get; set; }
        public DateTime Date { get; set; }
        public DateTime LogType { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
