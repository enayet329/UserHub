namespace Application.ResponseDTOs
{
    public record LoginResponseDTO(bool IsSuccess, string Message = null!, string Token = null!);
}
