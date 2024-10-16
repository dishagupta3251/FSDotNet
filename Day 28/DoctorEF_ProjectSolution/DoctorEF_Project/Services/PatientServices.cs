
using DoctorEF_Project.Context;
using DoctorEF_Project.Models;
using DoctorEF_Project.Repository;

namespace DoctorEF_Project.Services
{
    internal class PatientServices : IRepository<Patient>
    {
        HospitalContext hospitalContext = new HospitalContext();

        public Patient Get(string name)
        {
            var patient = GetAll().FirstOrDefault(p => p.Name == name);
            return patient;
           
        }

        public List<Patient> GetAll()
        {
            var patients = new List<Patient>();
            try
            {
                patients = hospitalContext.Patients.ToList();
            }catch(Exception e) { Console.WriteLine(e.Message); }
            return patients;
        }

        public Patient Insert()
        {
            Patient patient = new Patient();
            Console.Write("Name: ");
            patient.Name = Console.ReadLine();
            Console.Write("Age: ");
            patient.Age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Email: ");
            patient.Email = Console.ReadLine();
            try
            {
                hospitalContext.Patients.Add(patient);
                hospitalContext.SaveChanges();
                Console.WriteLine("Patient details added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Patient could not be added");
            }
            return patient;
        }

       
    }
}
