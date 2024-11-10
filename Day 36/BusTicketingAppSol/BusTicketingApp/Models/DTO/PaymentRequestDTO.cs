namespace BusTicketingApp.Models.DTO
{
    public class PaymentRequestDTO
    {
        public int BookingId { get; set; }
        public PaymentTypes Type { get; set; }
    }
}
