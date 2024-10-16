using DoctorEF_Project.Services;

namespace DoctorEF_Project
{
    internal class Program
    {
        PatientServices patientServices = new PatientServices();
        DoctorService doctorServices = new DoctorService();
        HospitalService hospitalService = new HospitalService();
        void MenuPatient()
        {
            int choice = -1;
            do
            {
                Console.WriteLine("1-Login");
                Console.WriteLine("2-Register");
                choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                try
                {
                    var doctors = doctorServices.GetAll();
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Name: ");
                            string name = Console.ReadLine();
                            var patient = patientServices.Get(name);
                            if (patient != null) {
                                Console.WriteLine("1-View your appointments\n2-Book Appointment");
                                int ch = Convert.ToInt32(Console.ReadLine());
                                if (ch == 1) hospitalService.GetAppointmentByPatient(patient.Id);
                                else {
                                    Console.WriteLine("List of All Doctors");
                                    doctorServices.Print(doctors);
                                    hospitalService.BookAppointment(patient.Id); }
                                 }
                            else Console.WriteLine("Patient not found");
                            break;
                        case 2:
                            var newPatient = patientServices.Insert();
                            Console.WriteLine("List of All Doctors");
                            doctorServices.Print(doctors);
                            Console.WriteLine("Book Appointment");
                            hospitalService.BookAppointment(newPatient.Id);
                            break;



                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            } while (choice != 0);

        }
        void MenuDoctor()
        {
            int choice = -1;
            do
            {
                Console.WriteLine("1-Login");
                Console.WriteLine("2-Register");
                choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Name: ");
                            string name = Console.ReadLine();
                            var doctor = doctorServices.Get(name);
                            if (doctor != null) hospitalService.GetAppointmentByDoctor(doctor.Id);
                            else Console.WriteLine("Doctor not found");
                            break;
                        case 2:
                            doctorServices.Insert();

                            break;



                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            } while (choice != 0);

        }
        void RunApp()
        {
         
            Console.WriteLine("Select your choice");
            Console.WriteLine("1-Doctor ");
            Console.WriteLine("2-Patient ");
            Console.WriteLine("3-View All Appointments");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    MenuDoctor();
                    break;
                case 2:
                    MenuPatient();
                    break;
                case 3:
                    var appointments=hospitalService.GetAllAppointment();
                    hospitalService.Print(appointments);

                    break;
            }
        }
        static void Main(string[] args)
        {
            new Program().RunApp();
        }
    }
}
