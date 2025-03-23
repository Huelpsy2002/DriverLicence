using DriverLicence.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Payments
    {
        [Key]
        public int Id {get;set;}
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CreditCard;
        public string PaymentDetails { get; set; }
        public DateTime Date { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string recipetNumber { get; set; }
        public string GatewayResponse { get; set; }
        public Transactions Transaction { get; set; }
        public int TransactionId { get; set; }
    }
}
