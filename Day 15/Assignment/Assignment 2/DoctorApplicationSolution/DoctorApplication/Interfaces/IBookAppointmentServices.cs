using DoctorApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorApplication.Interfaces
{
    public interface IBookAppointmentServices
    {
        Task<IEnumerable<Appointment>> ShowAppointments(int patientId);
        Task<Appointment> DeleteAppointment(int appointmentId);
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task<Appointment> UpdateAppointment(Appointment appointment);
    }
}
