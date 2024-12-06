namespace BusTicketingApp.Models.DTO
{
    public class BusCreateDTO
    {
        public string BusNumber { get; set; }
        public BusTypes BusType { get; set; }
        public BusStatus Status { get; set; }

        public int NumberOfSeats = 20;
        public decimal StandardFare { get; set; }
        public decimal PremiumFare { get; set; }
        public int OperatorId { get; set; }
        public int RouteId { get; set; }
        public DaysOfWeek Day { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
    }
}
