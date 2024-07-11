using Application.Contracts;
using Application.DTOs;
using Application.ResponseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHubController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserHubController(IUserRepository user)
        {
            _userRepository = user;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponseDTO))]
        public async Task<ActionResult<RegisterResponseDTO>> Register([FromBody] RegisterDTO registerDTO)
        {
            var response = await _userRepository.RegisterUserAsync(registerDTO);
            return Ok(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDTO))]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            if(loginDTO.Email == null || loginDTO.Password == null)
            {
                return BadRequest("Email or Password cannot be null");
            }
            var response = await _userRepository.LoginUserAsync(loginDTO);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return Ok(users);
        }

        [HttpPost("unblock")]
        public async Task<IActionResult> UnblockUser([FromBody] UserActionDTO userEmail)
        {
            var response = await _userRepository.UnblockUserAsync(userEmail);
            return Ok(response);
        }

        [HttpPost("block")]
        public async Task<IActionResult> BlockUser([FromBody] UserActionDTO userId)
        {
            var response = await _userRepository.BlockUserAsync(userId);
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] UserActionDTO userId)
        {
            var response = await _userRepository.DeletUserAsync(userId);
            return Ok(response);
        }


    }
}
