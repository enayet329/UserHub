using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    internal class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsBlocked { get; set; } = false; 
        public bool IsDeleted { get; set; } = false;
    }
}
