using DoctorApplication.Interfaces;
using DoctorApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApplication.Services
{
    public class BookAppointmentServices : IBookAppointmentServices
    {
        private readonly IRepository<int, Appointment> _appointmentRepository;

        public BookAppointmentServices(IRepository<int, Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            var conflictingAppointments = appointments.Where(a =>
                a.PatientId == appointment.PatientId &&
                ((appointment.AppointmentStartDate >= a.AppointmentStartDate && appointment.AppointmentStartDate < a.AppointmentEndDate) ||
                 (appointment.AppointmentEndDate > a.AppointmentStartDate && appointment.AppointmentEndDate <= a.AppointmentEndDate))
            );

            if (!conflictingAppointments.Any())
            {
                return await _appointmentRepository.AddAsync(appointment);
            }
            return null;
        }

        public async Task<Appointment> DeleteAppointment(int appointmentId)
        {
            return await _appointmentRepository.DeleteAsync(appointmentId);
        }

        public async Task<IEnumerable<Appointment>> ShowAppointments(int patientId)
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return appointments.Where(a => a.PatientId == patientId);
        }

        public async Task<Appointment> UpdateAppointment(Appointment appointment)
        {
            return await _appointmentRepository.UpdateAsync(appointment);
        }
    }
}
