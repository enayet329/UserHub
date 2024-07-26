using Application.Contracts;
using Application.DTOs;
using Application.ResponseDTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services.TokenServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserHubContext _userHubContext;
        private readonly JwtTokenGenerator _tokenGenerator;
        public UserRepository(UserHubContext hubContext, JwtTokenGenerator tokenGenerator)
        {
            _userHubContext = hubContext;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponseDTO> LoginUserAsync(LoginDTO loginDTO)
        {
            var getUser = await FindUserByEmail(loginDTO.Email!);

            if (getUser == null || getUser.IsBlocked)
            {
                var message = getUser == null ? "User not found with this email" : "User is blocked by admin";
                return new LoginResponseDTO(false, message);
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
                return new LoginResponseDTO(false, "Invalid password or email");
            }

        }

        public async Task<RegisterResponseDTO> RegisterUserAsync(RegisterDTO registerDTO)
        {
            var getUser = await FindUserByEmail(registerDTO.Email);

            if (getUser != null)
            {
                string message = getUser.IsBlocked ? "User is blocked by admin" : "User already exists with this email";
                return new RegisterResponseDTO(false, message);
            }


            var user = new User()
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                IsBlocked = false,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            };

            await _userHubContext.UserHub.AddAsync(user);
            await _userHubContext.SaveChangesAsync();

            return new RegisterResponseDTO(true, "User registered successfully now you can login");
        }
        public async Task<List<UsersResponseDTO>> GetUsersAsync()
        {
            var users = await _userHubContext.UserHub.ToListAsync();

            var userDtos = users.Select(u => new UsersResponseDTO
            {
                Name = u.Name,
                Email = u.Email,
                LastLoginTime = u.LastLoginTime,
                IsBlocked = u.IsBlocked
            }).ToList();

            return userDtos;
        }

        public async Task<UserActionResponseDTO> UnblockUserAsync(UserActionDTO userEmail)
        {
            if (userEmail.UserEmail == null || userEmail.UserEmail.Count == 0)
            {
                return new UserActionResponseDTO(false, "No user selected to unblock");
            }

            var userToUnblock = await _userHubContext.UserHub.Where(u =>
                                                userEmail.UserEmail.Contains(u.Email)).ToListAsync();

            if (userToUnblock.Count == 0)
            {
                return new UserActionResponseDTO(false, "No user found to unblock");
            }

            foreach (var user in userToUnblock)
            {
                if (user.IsBlocked == true)
                {
                    user.IsBlocked = false;
                }
            }

            await _userHubContext.SaveChangesAsync();

            return new UserActionResponseDTO(true, "User unblocked successfully");
        }


        public async Task<UserActionResponseDTO> BlockUserAsync(UserActionDTO user)
        {
            if (user.UserEmail == null || user.UserEmail.Count == 0)
            {
                return new UserActionResponseDTO(false, "No user selected to block");
            }


            var usersToBlock = await _userHubContext.UserHub
                            .Where(u => user.UserEmail.Contains(u.Email))
                            .ToListAsync();

            if (usersToBlock.Count == 0)
            {
                return new UserActionResponseDTO(false, "No user found to block");
            }

            foreach (var users in usersToBlock)
            {
                if (users.IsBlocked == false)
                {
                    users.IsBlocked = true;
                }
            }

            await _userHubContext.SaveChangesAsync();

            return new UserActionResponseDTO(true, "User blocked successfully");
        }

        public async Task<UserActionResponseDTO> DeletUserAsync(UserActionDTO userId)
        {
            if (userId.UserEmail == null || userId.UserEmail.Count == 0)
            {
                return new UserActionResponseDTO(false, "No user selected to delete");
            }
            var usersToDelete = await _userHubContext.UserHub
                            .Where(u => userId.UserEmail.Contains(u.Email))
                            .ToListAsync();

            if (usersToDelete.Count == 0)
            {
                return new UserActionResponseDTO(false, "No user found to delete");
            }

            _userHubContext.UserHub.RemoveRange(usersToDelete);
            await _userHubContext.SaveChangesAsync();

            return new UserActionResponseDTO(true, "User deleted successfully ");

        }


        private async Task<User> FindUserByEmail(string email) =>
            await _userHubContext.UserHub.FirstOrDefaultAsync(e => e.Email == email);
    }
}
