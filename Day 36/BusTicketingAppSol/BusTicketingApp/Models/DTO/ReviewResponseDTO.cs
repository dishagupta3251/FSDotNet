namespace BusTicketingApp.Models.DTO
{
    public class ReviewResponseDTO
    {
        public string OperatorName { get; set; }=string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string OperatorContact { get; set; } = string.Empty;

        public IEnumerable<string> Reviews { get; set; }
    }
}
