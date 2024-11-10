namespace BusTicketingApp.Models.DTO
{
    public class BusCreateDTO
    {
        public string BusNumber { get; set; }
        public BusTypes BusType { get; set; }
        public int NumberOfSeats { get; set; }
        public string Status { get; set; }
        public decimal StandardFare { get; set; }
        public decimal PremiumFare { get; set; }
        public int OperatorId { get; set; }
        public int RouteId { get; set; }
        public DayOfWeek Day { get; set; }
    }
}
