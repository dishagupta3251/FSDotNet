namespace BusTicketingApp.Models
{
    public enum PaymentTypes
    {
        Credit_card,
        UPI,
        Wallet
    }
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public PaymentTypes Type { get; set; }
        public Booking Booking { get; set; }


    }
}
