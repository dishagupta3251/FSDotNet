using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Models
{

    public enum BusTypes
    {
        AC,
        Non_AC
    }
    public enum BusStatus
    {
        Running,
        Regret
    }
   
    public class Bus
    {
        [Key]
        public int BusId { get; set; }
        public string BusNumber { get; set; }=string.Empty;
        public BusTypes BusType {  get; set; }

        public int NumberOfSeats { get; set; }
        public BusStatus Status { get; set; }
        public decimal StandardFare { get; set; }
        public decimal PremiumFare { get; set; }
        
        public int RouteId { get; set; }
        public AvailableRoute AvailableRoutes { get; set; }

        public int OperatorID { get; set; }
        public BusOperator Operator { get; set; }

        public IEnumerable<BusSchedule> Schedules { get; set; } =new List<BusSchedule>();

        public IEnumerable<Booking> Booking { get; set; } = new List<Booking>();
        public IEnumerable<Seats> Seats { get; set; } =new List<Seats>();

        public IEnumerable<SeatsBooked> SeatsBooked { get; set; } =new List<SeatsBooked>();
       


    }
}
