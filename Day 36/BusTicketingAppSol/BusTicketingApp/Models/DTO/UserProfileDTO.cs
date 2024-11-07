namespace BusTicketingApp.Models.DTO
{
 
    public class UserProfileDTO
    {
      
        public string FullName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Roles Roles { get; set; }
    }
}
