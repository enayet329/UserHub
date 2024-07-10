﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    internal class LoginDTO
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}
