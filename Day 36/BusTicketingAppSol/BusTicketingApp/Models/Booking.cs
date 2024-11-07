namespace BusTicketingApp.Models
{
    public class Booking
    {
      
            public int BookingId { get; set; }             // Primary key
            public DateTime BookingDate { get; set; }      // Date of booking
            public decimal TotalFare { get; set; }         // Total fare based on seat type and other factors
            public bool IsConfirmed { get; set; }          // Indicates if the booking is confirmed

            public int UserId { get; set; }
            public User User { get; set; }

            public int RouteId { get; set; }
            public AvailableRoute Route { get; set; }

            public int SeatId { get; set; }
            public Seats Seat { get; set; }
        }

    }
