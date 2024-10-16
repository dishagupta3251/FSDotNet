using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorEF_Project.Models;

namespace DoctorEF_Project.Repository
{
    public interface IHospitalRepository
    {
        public Appointment BookAppointment(int id);

        public List<Appointment> GetAppointmentByDoctor(int id);

        public List<Appointment> GetAppointmentByPatient(int id);
        public List<Appointment> GetAllAppointment();
    }
}
