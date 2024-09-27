using DoctorApplication.Models;

namespace DoctorApplication.Interfaces
{
    public interface IBookAppointmentServices
    {
        public Task<IEnumerable<Appointment>> ShowAppointments();
        public Task<Appointment> DeleteAppointment();
        public Task<Appointment> CreateAppointment();
        public Task<Appointment> UpdateAppointment();
    }
}
