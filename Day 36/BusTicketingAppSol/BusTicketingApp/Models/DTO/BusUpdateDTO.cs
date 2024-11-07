namespace BusTicketingApp.Models.DTO
{
    public class BusUpdateDTO
    {
        public string BusName { get; set; }
        public BusTypes BusType { get; set; }
        public int NumberOfSeats { get; set; }
        public string Status { get; set; }
        public decimal StandardFare { get; set; }
        public decimal PremiumFare { get; set; }
    }
}
