using DriverLicence.Business.DTOs.DriverLicence.Models.DTOs;
using DriverLicence.Business.Validations;
using DriverLicence.Data.Repositories;
using DriverLicence.Data.UnitOfWork;
using DriverLicence.Models.Domains;

namespace DriverLicence.Business.Services
{
    public interface IPersonService
    {
        Task<PersonDto> GetPerson(string nationalNumber);
        Task<(bool success, List<string> errors)> AddPerson(PersonCreateDto personCreateDto);
        Task<(bool success, List<string> errors)> UpdatePerson(string nationalNumber , PersonUpdateDto personUpdateDto);
        Task<bool> DeletePerson(string nationalNumber);

    }


    public class PersonService : IPersonService
    {

        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly PersonValidations _personValidator;
        public PersonService(IPersonRepository personRepository , IConfiguration configuration , IUnitOfWork unitOfWork,PersonValidations personValidations)
        {
            _personRepository = personRepository;
            _personValidator = personValidations;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        public async Task<PersonDto> GetPerson(string nationalNumber)
        {
            var person = await _personRepository.GetByNationalNumber(nationalNumber);
            if(person != null)
            {
                return new PersonDto()
                {
                    NationalNumber = person.NationalNumber,
                    Email = person.Email,
                    BirthDate = person.BirthDate,
                    FullName = person.FullName,
                    Nationality = person.Nationality,
                    Gender = person.Gender,
                    PhoneNumber = person.PhoneNumber,
                    ImagePath = person.ImagePath
                };

            }
            return null;
        }

       public async  Task<(bool success, List<string> errors)> AddPerson(PersonCreateDto personCreateDto)
        {
            var person = new Person()
            {
                NationalNumber = personCreateDto.NationalNumber,
                Email = personCreateDto.Email,
                BirthDate = personCreateDto.BirthDate,
                FullName = personCreateDto.FullName,
                Nationality = personCreateDto.Nationality,
                Gender = personCreateDto.Gender,
                PhoneNumber = personCreateDto.PhoneNumber,
                ImagePath = personCreateDto.ImagePath
            };
            var errors = _personValidator.Validate(person);
            if (errors.Count == 0)
            {
                await _personRepository.Add(person);
                await _unitOfWork.SaveChangesAsync();
                return (true, new List<string>());
            }
            return (false, errors);
        }
       public async Task<(bool success, List<string> errors)> UpdatePerson(string nationalNumber,PersonUpdateDto personUpdateDto)
        {
            var person =await _personRepository.GetByNationalNumber(nationalNumber);
            if (person != null)
            {
                person.Email = !string.IsNullOrEmpty(personUpdateDto.Email) ? personUpdateDto.Email : person.Email;
                person.FullName = !string.IsNullOrEmpty(personUpdateDto.FullName) ? personUpdateDto.FullName : person.FullName;
                person.Gender = !string.IsNullOrEmpty(personUpdateDto.Gender) ? personUpdateDto.Gender : person.Gender;
                person.Nationality = !string.IsNullOrEmpty(personUpdateDto.Nationality) ? personUpdateDto.Nationality : person.Nationality;
                person.PhoneNumber = !string.IsNullOrEmpty(personUpdateDto.PhoneNumber) ? personUpdateDto.PhoneNumber : person.PhoneNumber;
                person.ImagePath = !string.IsNullOrEmpty(personUpdateDto.ImagePath) ? personUpdateDto.ImagePath : person.ImagePath;
                person.BirthDate = personUpdateDto.BirthDate.HasValue ? personUpdateDto.BirthDate.Value : person.BirthDate;


                var errors = _personValidator.Validate(person);
                if (errors.Count == 0)
                {
                    await _personRepository.Update(person);
                    await _unitOfWork.SaveChangesAsync();
                    return (true, new List<string>());
                }
                return (false, errors);

             

            }
            return (false, new List<string>() { "person does not exist" });


        }


        public async Task<bool> DeletePerson(string nationalNumber)
        {
            var person =await _personRepository.GetByNationalNumber(nationalNumber);
            if (person != null)
            {
                _personRepository.Delete(person);
                await _unitOfWork.SaveChangesAsync();
                return true;

            }
            return false;
        }



    }
}
