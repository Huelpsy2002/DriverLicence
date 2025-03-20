using DriverLicence.Business.DTOs;
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



    }

    public class UserService:IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly UserValidations _userValidator;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, UserValidations userValidator,IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }




        public async Task<UserDto>GetUser(string username)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user != null)
            {
                return new UserDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Active = user.Active,
                    Role = user.Role,
                    NationalNumber = user.NationalNumber,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
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


            var errors = _userValidator.Validate(user);
            if (errors.Count == 0)
            {
                await _userRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();
                return (true, new List<string>());
            }
            return (false, errors);

        }
    }
}
