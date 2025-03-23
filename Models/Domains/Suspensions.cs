using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Suspensions
    {
        [Key]
        public int Id { get; set; }
        public int TaxAmount { get; set; }
        public string Infos { get; set; }
        public DateTime Date { get; set; }
        public bool Paied { get; set; }
        public DateTime PaidDate { get; set; }

        public Licences Licence { get; set; }
        public int LicenceNumber { get; set; }
    }
}
