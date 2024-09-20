using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagement
{
	public class Patient
	{
		public static int PatientCount = 100;
		public int ID { get; private set; }
		public string Name { get; set; } = string.Empty;
		public int Age { get; set; }
		public string Gender { get; set; } = string.Empty;
		public string PhoneNo { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;

		public void Register()
		{
			ID = PatientCount++;
			Console.Write("Enter your name: ");
			Name = Console.ReadLine().ToLower();
			Console.Write("Enter your age: ");
			Age = Convert.ToInt32(Console.ReadLine());
			Console.Write("Enter your gender: ");
			Gender = Console.ReadLine();
			Console.Write("Enter your phone number: ");
			PhoneNo = Console.ReadLine();
			Console.Write("Create a password: ");
			Password = Console.ReadLine();
			Console.WriteLine($"{Name} registered successfully!");
		}

		public void Login(List<Patient> patients)
		{
			Console.WriteLine("Welcome Back! Please Login");
			Patient patient = null;
			while (patient == null)
			{
				Console.Write("Name: ");
				string name = Console.ReadLine().ToLower();
				patient = patients.FirstOrDefault(p => p.Name == name);
				if (patient != null)
				{
					Console.Write("Password: ");
					string password = Console.ReadLine();
					if (patient.Password == password)
					{
						Console.WriteLine("Login successful");
						PatientLoginMenu(patient.ID);
					}
					else
					{
						Console.WriteLine("Incorrect Password");
					}
				}
				else
				{
					Console.WriteLine("Name not found!");
				}
			}
		}

		public void PatientLoginMenu(int ID)
		{
			int option = -1;
			while (option != 4)
			{
				Console.WriteLine("1-See Doctors\n2-Book Appointment\n3-View Appointments\n4-Back");
				option = Convert.ToInt32(Console.ReadLine());
				switch (option)
				{
					case 1:
						ClinicManagementServices.GetDoctors();
						break;
					case 2:
						ClinicManagementServices.BookAppointments(ID);
						break;
					case 3:
						ClinicManagementServices.ViewAppointments(ID, "patient");
						break;
				}
			}
		}

		public override string ToString()
		{
			return $"Patient ID: {ID}\nName: {Name}\nAge: {Age}\nPhone: {PhoneNo}";
		}
	}
}
