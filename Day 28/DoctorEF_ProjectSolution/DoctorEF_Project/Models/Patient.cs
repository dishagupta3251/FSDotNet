

namespace DoctorEF_Project.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }

    }
}
