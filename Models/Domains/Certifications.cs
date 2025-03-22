namespace DriverLicence.Models.Domains
{
    public class Certifications
    {
        public int CertID { get; set; }
        public string Description { get; set; }
        public string CertImage { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
