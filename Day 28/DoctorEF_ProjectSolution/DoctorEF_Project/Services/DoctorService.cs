
using DoctorEF_Project.Context;
using DoctorEF_Project.Models;
using DoctorEF_Project.Repository;

namespace DoctorEF_Project.Services
{
    public class DoctorService : IRepository<Doctor>
    {
        HospitalContext hospitalContext = new HospitalContext();

        public Doctor Get(string name)
        {
            var doctors = GetAll();
            var doctor = doctors.FirstOrDefault(d => d.Name == name);
            return doctor;
        }

       public void Print(List<Doctor> doctors)
        {
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"Id: {doctor.Id}\nName: {doctor.Name}\nSpecialization: {doctor.Specialization}");
            }
        }

        public Doctor Insert()
        {
            Doctor doctor = new Doctor();
            Console.Write("Name: ");
            doctor.Name = Console.ReadLine();
            Console.Write("Phone: ");
            doctor.Phone = Console.ReadLine();
            Console.Write("Specialization: ");
            doctor.Specialization = Console.ReadLine();
            Console.Write("Email: ");
            doctor.Email = Console.ReadLine();
            try
            {
                hospitalContext.Doctors.Add(doctor);
                hospitalContext.SaveChanges();
                Console.WriteLine("Doctor added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Doctor could not be added");
            }
            return doctor;
        }

        public List<Doctor> GetAll()
        {
            var doctors = new List<Doctor>();
            try
            { doctors = hospitalContext.Doctors.ToList(); }
            catch(Exception e) { Console.WriteLine(e.Message); }
            return doctors;
        }

       
    }
}
