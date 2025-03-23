using System.ComponentModel.DataAnnotations;

namespace DriverLicence.Models.Domains
{
    public class TestClasses
    {
        [Key]
        public int TestClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int ClassFees { get; set; }

        public Tests Test { get; set; }

    }
}
