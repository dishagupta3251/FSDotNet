using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagement
{
	public class Doctor
	{
		public static int DoctorCount = 100;
		public int ID { get; private set; }
		public string? Name { get; set; } = string.Empty;
		public int Age { get; set; }
		public string? Gender { get; set; } = string.Empty;
		public string? PhoneNo { get; set; } = string.Empty;
		public string? Specialization { get; set; } = string.Empty;
		public string? Password { get; set; } = string.Empty;

		public Doctor() { }
		public Doctor(string name, int age, string gender, string no, string specialization, string password)
		{
			Name = name;
			Age = age;
			Gender = gender;
			PhoneNo = no;
			Specialization = specialization;
			Password = password;
		}

		public void Register()
		{
			ID = DoctorCount++;
			Console.Write("Enter your name: ");
			Name = Console.ReadLine().ToLower();
			Console.Write("Enter your age: ");
			Age = Convert.ToInt32(Console.ReadLine());
			Console.Write("Enter your gender: ");
			Gender = Console.ReadLine();
			Console.Write("Enter your phone number: ");
			PhoneNo = Console.ReadLine();
			Console.Write("Enter your specialization: ");
			Specialization = Console.ReadLine();
			Console.Write("Create a password: ");
			Password = Console.ReadLine();
			Console.WriteLine($"{Name} registered successfully!");
		}

		public void Login(List<Doctor> doctors)
		{
			Console.WriteLine("Welcome Back! Please Login");
			Doctor doctor = null;
			while (doctor == null)
			{
				Console.Write("Name: ");
				string name = Console.ReadLine().ToLower();
				doctor = doctors.FirstOrDefault(p => p.Name == name);
				try
				{
					if (doctor != null)
					{
						Console.Write("Password: ");
						string password = Console.ReadLine();
						if (doctor.Password == password)
						{
							Console.WriteLine("Login successful");
							Console.WriteLine();
							DoctorLoginMenu(doctor.ID);
						}
						else
						{
							Console.WriteLine("Incorrect Password");
						}
					}
					else throw new ArgumentException("Name not found!");
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error: " + ex.Message);
				}
			}
		}

		public void DoctorLoginMenu(int ID)
		{
			int option = -1;
			while (option != 3)
			{
				Console.WriteLine("1-View Patients\n2-View Appointments\n3-Back");
				option = Convert.ToInt32(Console.ReadLine());
				switch (option)
				{
					case 1:
						new ClinicManagementServices().GetPatients();
						break;
					case 2:
						new ClinicManagementServices().ViewAppointments(ID, "doctor");
						break;
				}
			}
		}

		public override string ToString()
		{
			return $"Doctor ID: {ID}\nName: {Name}\nAge: {Age}\nPhone: {PhoneNo}\nSpecialization: {Specialization}";
		}
	}
}
