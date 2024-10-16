
using DoctorEF_Project.Context;
using DoctorEF_Project.Models;
using DoctorEF_Project.Repository;

namespace DoctorEF_Project.Services
{
    public class HospitalService : IHospitalRepository
    {
        HospitalContext hospitalContext = new HospitalContext();

        public Appointment BookAppointment(int id)
        {
            Appointment appointment = new Appointment();

            appointment.PatientId = id;

            Console.WriteLine("Enter doctor id: ");
            appointment.DoctorId = Convert.ToInt32(Console.ReadLine());

            DateTime appointmentDate;
            Appointment existingAppointment;

            do
            {
                Console.WriteLine("Enter Date and Time for appointment (yyyy-MM-dd HH:mm):");
                appointmentDate = DateTime.Parse(Console.ReadLine());
                appointment.CreatedDate = appointmentDate;

                existingAppointment = GetAllAppointment().FirstOrDefault(a => a.CreatedDate == appointmentDate && a.DoctorId == appointment.DoctorId);

                if (existingAppointment != null)
                    Console.WriteLine("There is already a booking with this doctor at this time. Please select another time.");

            } while (existingAppointment != null);

            hospitalContext.Appointments.Add(appointment);
            hospitalContext.SaveChanges();

            Console.WriteLine("Appointment booked successfully!");
            return appointment;
        }

        public void Print(List<Appointment> appointments)
        {
            foreach (var appointment in appointments)
            {
                Console.WriteLine(" ID: " + appointment.AppointmentId + " Date and Time: " + appointment.CreatedDate + " PatientID: " + appointment.PatientId + " DoctorID: " + appointment.DoctorId);
            }
        }

        public List<Appointment> GetAppointmentByDoctor(int doctorId)
        {
            var appointments= hospitalContext.Appointments.Where(a => a.DoctorId == doctorId).ToList();
            if(appointments.Count==0) Console.WriteLine("No appointments yet");
            else Print(appointments);
            return appointments;
        }

        public List<Appointment> GetAppointmentByPatient(int patientId)
        {
            
          var appointments=hospitalContext.Appointments.Where(a => a.PatientId == patientId).ToList();
            if(appointments.Count==0) Console.WriteLine("No appointments booked");
            else Print(appointments);
            return appointments;
        }

        public List<Appointment> GetAllAppointment()
        {
            var appointments= hospitalContext.Appointments.ToList();
            
            return appointments;
        }
    }
}
