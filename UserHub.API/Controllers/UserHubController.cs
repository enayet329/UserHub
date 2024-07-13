using Application.Contracts;
using Application.DTOs;
using Application.ResponseDTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegisterResponseDTO>> Register([FromBody] RegisterDTO registerDTO)
        {
            var response = await _userRepository.RegisterUserAsync(registerDTO);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO loginDTO)
        {

            var response = await _userRepository.LoginUserAsync(loginDTO);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return Ok(users);
        }

        [HttpPut("unblock")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UnblockUser([FromBody] UserActionDTO userEmail)
        {
            var response = await _userRepository.UnblockUserAsync(userEmail);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("block")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BlockUser([FromBody] UserActionDTO user)
        {
            var response = await _userRepository.BlockUserAsync(user);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteUser([FromBody] UserActionDTO userId)
        {
            var response = await _userRepository.DeletUserAsync(userId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}