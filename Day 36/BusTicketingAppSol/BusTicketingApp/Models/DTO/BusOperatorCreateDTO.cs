namespace BusTicketingApp.Models.DTO
{
    public class BusOperatorCreateDTO
    {
        public string OperatorName { get; set; } = string.Empty;

        public string Username {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string OperatorContact { get; set; } = string.Empty;

    }
}
