namespace DriverLicence.Business.DTOs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace DriverLicence.Models.DTOs
    {
        // DTO for creating a new person
        public class PersonCreateDto
        {
           
            public string NationalNumber { get; set; }

          
            public string FullName { get; set; }

            public DateTime BirthDate { get; set; }

          
            public string PhoneNumber { get; set; }

          
            public string Email { get; set; }

            public string Nationality { get; set; }

            public string ImagePath { get; set; }

            public string Gender { get; set; }
        }

        // DTO for updating an existing person
        public class PersonUpdateDto
        {
         
            public string FullName { get; set; }

            public DateTime BirthDate { get; set; }

            public string PhoneNumber { get; set; }

            public string Email { get; set; }

           
            public string Nationality { get; set; }

            public string ImagePath { get; set; }

           
            public string Gender { get; set; }
        }

        // DTO for returning person details
        public class PersonDto
        {
            public string NationalNumber { get; set; }
            public string FullName { get; set; }
            public DateTime BirthDate { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Nationality { get; set; }
            public string ImagePath { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }
        }

        // DTO for list views (simplified data)
        public class PersonListDto
        {
            public string NationalNumber { get; set; }
            public string FullName { get; set; }
            public DateTime BirthDate { get; set; }
            public string Nationality { get; set; }
            public string Gender { get; set; }
        }
    }
}
