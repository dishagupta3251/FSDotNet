using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BusTicketingApp.Models.DTO
{
    public class BusResponseDTO
    {
        public int BusId { get; set; }
        public string BusNumber { get; set; } = string.Empty;
        public string BusType { get; set; }
        public int SeatsLeft { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal StandardFare { get; set; }
        public decimal PremiumFare { get; set; }
        public string CompanyName { get; set; }=string.Empty;

        public DateTime Arrival {  get; set; }

        public DateTime Departure { get; set; }

        public string JourneyDetails { get; set; } = string.Empty;
    }
}
