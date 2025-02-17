﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public DateTime LastLoginTime { get; set; } = DateTime.Now;

        public bool IsBlocked { get; set; } = false;

    }
}
