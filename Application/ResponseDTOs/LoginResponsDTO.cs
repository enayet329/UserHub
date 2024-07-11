namespace Application.ResponseDTOs
{
    public record LoginResponseDTO(bool IsSuccess, string Message, string Token);
}
