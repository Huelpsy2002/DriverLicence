using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class LicenceClasses
    {
        [Key]
        public int LicenceClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinAllowedAge { get; set; }
        public int ClassFees { get; set; }
        public int LicenceDuration { get; set; }
        public Licences Licence { get; set; }


    }
}
