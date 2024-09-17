using System.ComponentModel;
using System.Xml.Serialization;

namespace ClinicManagement
{
    internal class Program
    {
        List<Doctor> doctors = new List<Doctor>();
        void InvokeDoctor()
        {
            ClinicManagementServices services = new ClinicManagementServices();
            doctors.Add(services.AddDoctors());
            foreach(var item in doctors)
            {
                Console.WriteLine(item);
            }
            
        }
        void InvokePatients()
        {
            Patients patients = new Patients();
            patients.CallForInputPatients();
            Console.WriteLine(patients);
        }
        void Menu()
        {

            int option = -1;
            do
            {
                Console.WriteLine("1-Doctors Registration\n2-Patients Registration\n3-Check for Appointments\n0-Exit");
                option =Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0: Console.WriteLine("Exit");
                        break;
                    case 1:InvokeDoctor();
                        break;
                    case 2:
                        InvokePatients();
                        break;   
                }

            } while (option != 0);
        }
        
        static void Main(string[] args)
        {
            
            new Program().Menu();
        }
    }
}
