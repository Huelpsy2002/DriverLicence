using DriverLicence.Business.DTOs;
using DriverLicence.Business.Jwt;
using DriverLicence.Business.Validations;
using DriverLicence.Data;
using DriverLicence.Data.Repositories;
using DriverLicence.Data.UnitOfWork;
using DriverLicence.Models.Domains;
using Microsoft.AspNetCore.Identity;

namespace DriverLicence.Business.Services
{

    public interface IUserService
    {
        Task<UserDto> GetUser(string username);
        Task<(bool success, List<string> Errors)> AddUser(CreateUserDto createUserDto);
        Task<(bool success, List<string> Errors)> UpdateUser(string username,UpdateUserDto updateUserDto);
        Task<bool> DeleteUser(string username);
        Task<UserAuthDto> AuthenticateUser(UserLoginDto userLoginDto);






    }

    public class UserService:IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly UserValidations _userValidator;
        private readonly PersonValidations __personValidator;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IPersonRepository personRepository,PersonValidations personValidator, UserValidations userValidator,IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(_personRepository));
            _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
            __personValidator = personValidator ?? throw new ArgumentNullException(nameof(__personValidator));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        private void UpdateUserAndPersonData(User user , Person person, UpdateUserDto updateUserDto)
        {
            var updatePersonDto = updateUserDto.personUpdateDto; // extracting the person dto

            /// updating user 
            user.Username = !string.IsNullOrEmpty(updateUserDto.Username) ? updateUserDto.Username : user.Username;
            user.Password = !string.IsNullOrEmpty(updateUserDto.Password) ? updateUserDto.Password : user.Password;
            user.Active = updateUserDto.Active.HasValue ? updateUserDto.Active.Value : user.Active;
            user.Role = !string.IsNullOrEmpty(updateUserDto.Role) ? updateUserDto.Role : user.Role;
            ////updating person
            person.FullName = !string.IsNullOrEmpty(updatePersonDto.FullName) ? updatePersonDto.FullName : person.FullName;
            person.BirthDate = updatePersonDto.BirthDate.HasValue ? updatePersonDto.BirthDate.Value : person.BirthDate;
            person.Email = !string.IsNullOrEmpty(updatePersonDto.Email) ? updatePersonDto.Email : person.Email;
            person.PhoneNumber = !string.IsNullOrEmpty(updatePersonDto.PhoneNumber) ? updatePersonDto.PhoneNumber : person.PhoneNumber;
            person.Nationality = !string.IsNullOrEmpty(updatePersonDto.Nationality) ? updatePersonDto.Nationality : person.Nationality;
            person.Gender = !string.IsNullOrEmpty(updatePersonDto.Gender) ? updatePersonDto.Gender : person.Gender;
            person.ImagePath = !string.IsNullOrEmpty(updatePersonDto.ImagePath) ? updatePersonDto.ImagePath : person.ImagePath;
        }

        public async Task<UserDto>GetUser(string username)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user != null)
            {
                var person = await _personRepository.GetByNationalNumber(user.NationalNumber);
                PersonDto personDto = new PersonDto
                {
                    FullName = person.FullName,
                    Email = person.Email,
                    BirthDate = person.BirthDate,
                    Gender = person.Gender,
                    PhoneNumber = person.PhoneNumber,
                    Nationality = person.Nationality,
                    ImagePath = person.ImagePath
                };
                return new UserDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Active = user.Active,
                    Role = user.Role,
                    NationalNumber = user.NationalNumber,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    personDto = personDto
                };
            }
            return null;
        }

        public async Task<(bool success, List<string> Errors)> AddUser(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Username = createUserDto.Username,
                Password = createUserDto.Password,
                Role = createUserDto.Role,
                NationalNumber = createUserDto.NationalNumber
            };
            var person = new Person
            {
                NationalNumber = user.NationalNumber,
                FullName = createUserDto.personCreateDto.FullName,
                Email = createUserDto.personCreateDto.Email,
                BirthDate = createUserDto.personCreateDto.BirthDate,
                Gender = createUserDto.personCreateDto.Gender,
                PhoneNumber = createUserDto.personCreateDto.PhoneNumber,
                Nationality = createUserDto.personCreateDto.Nationality,
                ImagePath = createUserDto.personCreateDto.ImagePath
            };
            

            
            var errors = _userValidator.Validate(user);
            errors.AddRange( __personValidator.Validate(person));//adding the errors in person validator to the user validator errors list
            if(await _userRepository.GetByUsername(user.Username)!=null)
            {
                errors.Add("username already taken");
            }

            if (await _personRepository.GetByEmail(person.Email) != null)
            {
                errors.Add("email already taken");
            }

            if (errors.Count == 0)
            {

                user.Password = _userValidator.HashPassword(user.Password);
                await _personRepository.Add(person);
                await _userRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();
                return (true, new List<string>());
            }
            return (false, errors);

        }
        public async Task<(bool success, List<string> Errors)> UpdateUser(string username , UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user != null)
            {
                var person = await _personRepository.GetByNationalNumber(user.NationalNumber);
                UpdateUserAndPersonData(user, person, updateUserDto);
         
                var errors = _userValidator.Validate(user);
                errors.AddRange(__personValidator.Validate(person));
                if (await _personRepository.GetByEmail(updateUserDto.personUpdateDto.Email) != null)
                {
                    errors.Add("email already taken");
                }


                if (errors.Count == 0)
                {
                    user.Password = !string.IsNullOrEmpty(updateUserDto.Password) ? _userValidator.HashPassword(user.Password) : user.Password;  //hash the pasword only if updated
                    user.UpdatedAt = DateTime.Now;
                    _personRepository.Update(person);
                    _userRepository.Update(user);
                    await _unitOfWork.SaveChangesAsync();
                    return (true, new List<string>());
                }
                return (false, errors);

            }
            else
            {
                return (false, new List<string>() { "username already taken" });
            }


        }
        public async Task<bool> DeleteUser(string username)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user != null)
            {
                var person = await _personRepository.GetByNationalNumber(user.NationalNumber);
                _personRepository.Delete(person);
                _userRepository.Delete(user);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public async Task<UserAuthDto> AuthenticateUser(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetByUsername(userLoginDto.Username);
            if(user==null || !_userValidator.VerifyPassword(userLoginDto.Password,user.Password))
            {
                return null;
            }
            TokenService tokenService = new TokenService(_configuration);
            string token = tokenService.GenerateToken(user);
            return new UserAuthDto()
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role,
                Token = token
            };

        }

    }
}
