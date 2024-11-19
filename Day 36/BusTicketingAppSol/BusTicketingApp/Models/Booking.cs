using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Models
{
    public class Booking
    {

            [Key]
            public int BookingId { get; set; }   
            
            public DateTime BookingDate { get; set; }
        public string BusNumber { get; set; } = string.Empty;
            public DateTime BookedForDate {  get; set; }
            public DaysOfWeek BookedForDay { get; set; }

        public string BookedSeats { get; set; } = string.Empty;

            public decimal TotalFare { get; set; }
        public string IsConfirmed { get; set; } = string.Empty;     
            public int CustomerId { get; set; }

            [JsonIgnore]
            public Customer Customer { get; set; }
           
            public int RouteId { get; set; }
            public AvailableRoute Route { get; set; }
            
            public int BusId { get; set; }
            public Bus Bus { get; set; }

            public Payment Payment { get; set; }
        public IEnumerable<SeatsBooked> SeatsBooked { get; set; } 
    }

    }
