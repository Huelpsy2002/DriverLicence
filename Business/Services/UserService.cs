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
            if(await _userRepository.GetByUsername(user.Username)!=null)
            {
                errors.Add("username already taken");
            }
            if (errors.Count == 0)
            {
                user.Password = _userValidator.HashPassword(user.Password);
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
                user.Username = !string.IsNullOrEmpty(updateUserDto.Username) ? updateUserDto.Username : user.Username;
                user.Password = !string.IsNullOrEmpty(updateUserDto.Password)  ? updateUserDto.Password : user.Password;
                user.Active = updateUserDto.Active.HasValue ? updateUserDto.Active.Value : user.Active;
                user.Role = !string.IsNullOrEmpty(updateUserDto.Role) ? updateUserDto.Role : user.Role;



                var errors = _userValidator.Validate(user);
                if (errors.Count == 0)
                {
                    user.Password = _userValidator.HashPassword(user.Password);
                    user.UpdatedAt = DateTime.Now;
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
