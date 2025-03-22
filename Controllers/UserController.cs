using DriverLicence.Business.DTOs;
using DriverLicence.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DriverLicence.Controllers
{
    [Route("BlogApi")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }




        [HttpPost("user/login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult<string>> Login(UserLoginDto userloginDto)
        {

            try
            {

                if (userloginDto == null)
                {
                    return BadRequest(new { message = "Invalid Data." });
                }
                var userAuthDto = await _userService.AuthenticateUser(userloginDto);

                if (userAuthDto==null)
                {
                    return Unauthorized(new { message = "invalid username or password" });
                }
                else
                {
                    return Ok( userAuthDto);
                }
            }
            catch (Exception err)
            {
                return StatusCode(500, new { error = "An unexpected error occurred", details = err.StackTrace });

            }



        }






        [HttpPost("user/register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register(CreateUserDto createUserDto)
        {
            try
            {

                var (success, errors) = await _userService.AddUser(createUserDto);

                if (!success)
                {
                    return BadRequest(new { errors = errors });

                }
                return Ok(new { message = "User Created" });

            }


            catch (Exception err)
            {
                return StatusCode(500, new { error = "An unexpected error occurred", details = err.Message });
            }

        }




        [Authorize]
        [HttpPatch("user/update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateUser(UpdateUserDto updateuserdto)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    return Unauthorized(new { error = "Invalid or missing authentication token." });
                }
                var (success, errors) = await _userService.UpdateUser(username, updateuserdto);

                if (!success)
                {
                    return BadRequest(new { errors });

                }
                return Ok(new { message = "User updated" });

            }


            catch (Exception err)
            {
                return StatusCode(500, new { error = "An unexpected error occurred", details = err.Message });
            }
        }

        [Authorize]
        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(username))
                {
                    return Unauthorized(new { error = "Invalid or missing authentication token." });
                }
                var user = await _userService.GetUser(username);
                if (user == null)
                {
                    return NotFound(new { message = $"user with username {username} does not exist" });
                }
                return Ok(new { user });

            }
            catch (Exception err)
            {
                return StatusCode(500, new { error = "An unexpected error occurred", details = err.Message });

            }
        }

        [Authorize]

        [HttpDelete("user/delete")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteUser()
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(username))
                {
                    return Unauthorized(new { error = "Invalid or missing authentication token." });
                }

                var deleted = await _userService.DeleteUser(username);
                if (!deleted)
                {
                    return NotFound(new { message = $"user with username {username} does not exist" });
                }
                return Ok(new { message = "user deleted." });

            }
            catch (Exception err)
            {
                return StatusCode(500, new { error = "An unexpected error occurred", details = err.Message });

            }
        }


    }
}
