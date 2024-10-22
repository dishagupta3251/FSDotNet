﻿namespace WebApplication1.Models.DTO
{
    public class EventDTO
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
