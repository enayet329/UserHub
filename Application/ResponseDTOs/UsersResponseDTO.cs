namespace Application.ResponseDTOs
{
    public record UsersResponseDTO
    {   
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime LastLoginTime { get; set; } = DateTime.Now;

        public bool IsBlocked { get; set; } = false;

    }
}
