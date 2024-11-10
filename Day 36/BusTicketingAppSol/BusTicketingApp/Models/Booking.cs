using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Models
{
    public class Booking
    {
      
            public int BookingId { get; set; }   
            
            public DateTime BookingDate { get; set; }  
            public DaysOfWeek BookedForDay { get; set; }
            public List<SeatsResponseDTO> SeatsBooked {  get; set; }
            public decimal TotalFare { get; set; }         
            public bool IsConfirmed { get; set; }         
            public int CustomerId { get; set; }
            public Customer Customer { get; set; }

            public int RouteId { get; set; }
            public AvailableRoute Route { get; set; }

            public int SeatId { get; set; }
            public Seats Seat { get; set; }

            public Payment Payment { get; set; }
        }

    }
