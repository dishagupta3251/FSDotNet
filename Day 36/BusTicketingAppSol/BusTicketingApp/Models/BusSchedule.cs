namespace BusTicketingApp.Models
{
    public enum DaysOfWeek
    {
        Sunday,
        Monday,
        Tuesday, Wednesday,
        Thursday, Friday,
        Saturday
    }
    public class BusSchedule
    {
       
            public int BusScheduleId { get; set; }
            public int BusId { get; set; }
            public DaysOfWeek Day { get; set; }
            public DateTime Departure { get; set; }
            public DateTime Arrival { get; set; }
            public int RouteId { get; set; }
            public AvailableRoute AvailableRoute { get; set; }
            public Bus Bus { get; set; }
        
    }
}
