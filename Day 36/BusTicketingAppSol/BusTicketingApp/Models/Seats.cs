using System.ComponentModel.DataAnnotations;

namespace BusTicketingApp.Models
{
   
    public class Seats
    {
        
        public int SeatsId { get; set; }     
        
        public int CustomerId { get; set; } 
        public int SeatNumber { get; set; }   
        public string Side {  get; set; }
        public string SeatType { get; set; }
        public bool IsBooked { get; set; }
        public decimal Price { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        public Booking Booking { get; set; }

        public Customer Customer { get; set; }



            
    }
}
