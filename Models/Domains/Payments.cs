using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Payments
    {
        [Key]
        public int Id {get;set;}
        public string PaymentMethod { get; set; }
        public string PaymentDetails { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string recipetNumber { get; set; }
        public string GatewayResponse { get; set; }
        public Transactions Transaction { get; set; }
        public int TransactionId { get; set; }
    }
}
