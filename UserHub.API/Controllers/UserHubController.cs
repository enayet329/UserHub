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
        private readonly IUserRepository _user;
        public UserHubController(IUserRepository user)
        {
            _user = user;
        }

        [HttpPost]
        public async Task<ActionResult<RegisterResponseDTO>> Register([FromBody] RegisterDTO registerDTO)
        {
            var response = await _user.RegisterUserAsync(registerDTO);
            return Ok(response);
        }
    }
}
