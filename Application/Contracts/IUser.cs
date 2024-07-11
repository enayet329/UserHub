using Application.DTOs;
using Application.ResponseDTOs;

namespace Application.Contracts
{
    public interface IUser
    {
        Task<RegisterResponseDTO> RegisterUserAsync(RegisterDTO registerDTO);
        Task<LoginResponseDTO> LoginUserAsync(LoginDTO loginDTO);
        Task<UserActionResponseDTO> DeletUserAsync(UserActionDTO userId);
        Task<UserActionResponseDTO> BlockUserAsync(UserActionDTO userId);
        Task<UserActionResponseDTO> UnblockUserAsync(UserActionDTO userId);
    }
}
