using System.ComponentModel.DataAnnotations;

namespace BusTicketingApp.Models
{
   
    public class Seats
    {
        
        public int SeatsId { get; set; }     
        
   
        public int SeatNumber { get; set; }   
        public string Side {  get; set; }=string.Empty;
        public string SeatType { get; set; }=string.Empty;

        public bool IsBooked { get; set; }
        public decimal Price { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        public SeatsBooked SeatsBooked { get; set; }



            
    }
}
