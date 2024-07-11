using Application.Contracts;
using Application.DTOs;
using Application.ResponseDTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services.TokenServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    internal class UserRepository : IUser
    {
        private readonly UserHubContext _userHubContext;
        private readonly JwtTokenGenerator _tokenGenerator;
        public UserRepository(UserHubContext hubContext,JwtTokenGenerator tokenGenerator)
        {
            _userHubContext = hubContext;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponseDTO> LoginUserAsync(LoginDTO loginDTO)
        {
            var getUser = await FindUserByEmail(loginDTO.Email!);
            if (getUser == null)
            {
                return new LoginResponseDTO(false, "User not found");
            }
            bool checkPass = BCrypt.Net.BCrypt.Verify(loginDTO.Password!, getUser.Password);
            if (checkPass)
            {
                getUser.LastLoginTime = DateTime.Now;
                _userHubContext.UserHub.Update(getUser);
                await _userHubContext.SaveChangesAsync();
                var jwtToken = _tokenGenerator.GenerateToken(getUser);
                return new LoginResponseDTO(true, "User logged in successfully", jwtToken);
            }
            else
            {
                return new LoginResponseDTO(false, "Invalid password");
            }

        }

        public async Task<RegisterResponseDTO> RegisterUserAsync(RegisterDTO registerDTO)
        {
            var getUser = await FindUserByEmail(registerDTO.Email);

            if (getUser != null)
            {
                return new RegisterResponseDTO(false, "User already exists");
            }

            var user = new User()
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                CreatedAt = DateTime.Now,
                IsBlocked = false,
                IsDeleted = false,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            };

            await _userHubContext.UserHub.AddAsync(user);
            await _userHubContext.SaveChangesAsync();

            return new RegisterResponseDTO(true, "User registered successfully");
        }

        public async Task<UserActionResponseDTO> UnblockUserAsync(UserActionDTO userId)
        {
            if (userId.UserIds == null || userId.UserIds.Count == 0)
            {
                return new UserActionResponseDTO(false, "No user selected");
            }

            var userToUnblock = await _userHubContext.UserHub.Where(u => userId.UserIds.Contains(u.Id)).ToListAsync();

            foreach (var user in userToUnblock)
            {
                user.IsBlocked = false;
            }

            await _userHubContext.SaveChangesAsync();

            return new UserActionResponseDTO(true, "User unblocked successfully");
        }


        public async Task<UserActionResponseDTO> BlockUserAsync(UserActionDTO userId)
        {
            if (userId.UserIds == null || userId.UserIds.Count == 0)
            {
                return new UserActionResponseDTO(false, "No user selected");
            }


            var usersToBlock = await _userHubContext.UserHub
                            .Where(u => userId.UserIds.Contains(u.Id))
                            .ToListAsync();

            foreach (var user in usersToBlock)
            {
                user.IsBlocked = true;
            }

            await _userHubContext.SaveChangesAsync();

            return new UserActionResponseDTO(true, "User blocked successfully");
        }

        public async Task<UserActionResponseDTO> DeletUserAsync(UserActionDTO userId)
        {
            if (userId.UserIds == null || userId.UserIds.Count == 0)
            {
                return new UserActionResponseDTO(false, "No user selected");
            }
            var usersToDelete = await _userHubContext.UserHub
                            .Where(u => userId.UserIds.Contains(u.Id))
                            .ToListAsync();

            _userHubContext.UserHub.RemoveRange(usersToDelete);
            await _userHubContext.SaveChangesAsync();

            return new UserActionResponseDTO(true, "User deleted successfully");

        }


        private async Task<User> FindUserByEmail(string email) =>
            await _userHubContext.UserHub.FirstOrDefaultAsync(e => e.Email == email);
    }
}
