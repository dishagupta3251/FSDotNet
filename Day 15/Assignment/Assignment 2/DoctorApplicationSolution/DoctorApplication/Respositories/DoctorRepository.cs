using DoctorApplication.Exceptions;
using DoctorApplication.Interfaces;
using DoctorApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApplication.Respositories
{
    public class DoctorRepository : IRepository<int, Doctor>
    {
        List<Doctor> _doctors = new List<Doctor>
        {
            new Doctor{ Id = 1, Name = "Dr. John Doe", Specialization = "Cardiologist" },
            new Doctor{ Id = 2, Name = "Dr. Jane Smith", Specialization = "Neurologist" },
            new Doctor{ Id = 3, Name = "Dr. Alex Brown", Specialization = "Dermatologist" },
            new Doctor{ Id = 4, Name = "Dr. Emma Green", Specialization = "Pediatrician" },
            new Doctor{ Id = 5, Name = "Dr. Michael White", Specialization = "General Surgeon" }
        };

        public async Task<Doctor> AddAsync(Doctor entity)
        {
            _doctors.Add(entity);
            return entity;
        }

        public async Task<Doctor> DeleteAsync(int id)
        {
            var doctor = await GetAsync(id);
            if (doctor != null)
            {
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
            return _doctors.FirstOrDefault(d => d.Id == id);
        }

        public async Task<Doctor> UpdateAsync(Doctor entity)
        {
            var oldDoctor = await GetAsync(entity.Id);
            if (oldDoctor == null)
            {
                throw new NoSuchDoctorFound();
            }
            oldDoctor.Name = entity.Name;
            oldDoctor.Specialization = entity.Specialization;
            return oldDoctor;
        }
    }
}
