﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;


    }
}
