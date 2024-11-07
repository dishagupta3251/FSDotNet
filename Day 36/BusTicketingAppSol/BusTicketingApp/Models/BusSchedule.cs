namespace BusTicketingApp.Models
{
    public enum DayOfWeek
    {
        Monday,
        Tuesday, Wednesday,
        Thursday, Friday,
        Saturday,Sunday
    }
    public class BusSchedule
    {
       
            public int BusScheduleId { get; set; }
            public int BusId { get; set; }
            public DayOfWeek Day { get; set; }  
            public int RouteId { get; set; }
            public AvailableRoute AvailableRoute { get; set; }
            public Bus Bus { get; set; }
        
    }
}
