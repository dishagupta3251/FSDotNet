namespace BusTicketingApp.Models.DTO
{
    public class LoginResponseDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; }=string.Empty;
    }
}
