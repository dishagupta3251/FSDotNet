using DoctorApplication.Exceptions;
using DoctorApplication.Interfaces;
using DoctorApplication.Models;

namespace DoctorApplication.Respositories
{
    public class DoctorRepository : IRepository<int, Doctor>
    {
        List<Doctor> _doctors=new List<Doctor>();
        public async Task<Doctor> AddAsync(Doctor entity)
        {   
            _doctors.Add(entity);
            return entity;
        }

        public async Task<Doctor> DeleteAsync(int id)
        {
            var doctor=await GetAsync(id);
            if (doctor != null) { 
               _doctors.Remove(doctor);
            }
            else
            {
                throw new NoSuchDoctorFound();
            }
            return doctor;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return _doctors;
        }

        public async Task<Doctor> GetAsync(int id)
        {
            var doctor= _doctors.FirstOrDefault(d=>d.Id==id);
            return doctor;
        }

        public async Task<Doctor> UpdateAsync(Doctor entity)
        {
            var oldDoctor= await GetAsync(entity.Id);
            if (oldDoctor == null) {
                throw new NoSuchDoctorFound();
            }
            oldDoctor.Name=entity.Name;
            oldDoctor.Specialization=entity.Specialization;
            return oldDoctor;
        }

       
    }
}
