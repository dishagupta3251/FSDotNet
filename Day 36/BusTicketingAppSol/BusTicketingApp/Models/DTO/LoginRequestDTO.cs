﻿using System.ComponentModel.DataAnnotations;

namespace BusTicketingApp.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Email or username not found")]
        public string Input { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
