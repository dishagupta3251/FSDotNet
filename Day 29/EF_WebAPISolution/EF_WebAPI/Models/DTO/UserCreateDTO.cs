﻿namespace EF_WebAPI.Models.DTO
{
    public class UserCreateDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Roles Role { get; set; }

    }
}
