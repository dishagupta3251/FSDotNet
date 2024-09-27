using DoctorApplication.Exceptions;
using DoctorApplication.Interfaces;
using DoctorApplication.Models;

namespace DoctorApplication.Respositories
{
    public class AppointmentRepository : IRepository<int, Appointment>
    {
        static List<Appointment> appointments = new List<Appointment>();

        public  async Task<Appointment> AddAsync(Appointment entity)
        {
            appointments.Add(entity);
            return entity;
        }

        public async Task<Appointment> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            appointments.Remove(entity);
            return entity;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return  appointments;
        }

        public async Task<Appointment> GetAsync(int id)
        {
            var entity = appointments.FirstOrDefault(x => x.Id == id);
            if (entity == null) { 
                throw new NoSuchAppointmentException();
            }
            return entity;
        }

        public async Task<Appointment> UpdateAsync(Appointment entity)
        {
            var oldAppointment= await GetAsync(entity.Id);
            if (oldAppointment == null) {
               throw new NoSuchAppointmentException();
            }
            oldAppointment.PatientId = entity.PatientId;
            oldAppointment.DoctorId = entity.DoctorId;
            oldAppointment.AppointmentStartDate = entity.AppointmentStartDate;
            return oldAppointment;
        }
    }
}
