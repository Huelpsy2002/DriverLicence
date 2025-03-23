using DriverLicence.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public int PaiedAmount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TransactionStatus Status { get; set; }
        public int RefrenceId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<Payments> Payments { get; set; }
    }
}
