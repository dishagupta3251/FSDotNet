namespace DoctorApplication.Models
{
    public class Appointment:IEquatable<Appointment>
    {
    
            public int Id { get; set; }
            public int PatientId { get; set; }
            public int DoctorId { get; set; }
            public DateTime AppointmentStartDate { get; set; }
           public DateTime AppointmentEndDate => AppointmentStartDate.AddMinutes(30);

        public bool Equals(Appointment? other)
        {
            return this.Id==other?.Id;
        }
    }
}
