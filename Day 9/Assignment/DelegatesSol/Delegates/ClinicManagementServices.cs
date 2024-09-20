using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagement
{
	internal class ClinicManagementServices : IAuthentication
	{
		private static List<Doctor> doctors = new List<Doctor>();
		private static List<Patient> patients = new List<Patient>();
		private static List<Appointments> appointments = new List<Appointments>();

		public void Menu()
		{
			int option = -1;
			do
			{
				Console.WriteLine("1-Doctors\n2-Patients\n0-Exit");
				option = Convert.ToInt32(Console.ReadLine());
				switch (option)
				{
					case 0:
						Console.WriteLine("Exit");
						break;
					case 1:
						MenuForRegistrationLogin("doctor");
						break;
					case 2:
						MenuForRegistrationLogin("patient");
						break;
					default:
						Console.WriteLine("Invalid input");
						break;
				}
			} while (option != 0);
		}

		private void MenuForRegistrationLogin(string check)
		{
			int option = -1;
			do
			{
				Console.WriteLine("1-Register\n2-Login\n0-Back");
				option = Convert.ToInt32(Console.ReadLine());
				switch (option)
				{
					case 0:
						Menu();
						break;
					case 1:
						Register(check);
						break;
					case 2:
						Login(check);
						break;
					default:
						Console.WriteLine("Invalid input");
						break;
				}
			} while (option != 0);
		}

		public void Register(string check)
		{
			Console.WriteLine("Welcome to the registration page");
			if (check == "doctor")
			{
				var doctor = new Doctor();
				doctor.Register();
				doctors.Add(doctor);
			}
			else
			{
				var patient = new Patient();
				patient.Register();
				patients.Add(patient);
			}
		}

		public void Login(string check)
		{
			if (check == "patient")
			{
				var patient = new Patient();
				patient.Login(patients);
			}
			else
			{
				var doctor = new Doctor();
				doctor.Login(doctors);
			}
		}

		public static void GetDoctors()
		{
			Console.WriteLine("------------------------------------------------------------");
			foreach (var doctor in doctors)
			{
				Console.WriteLine(doctor);
				Console.WriteLine();
			}
			Console.WriteLine("------------------------------------------------------------");
		}

		public static void GetPatients()
		{
			Console.WriteLine("The list of available patients are: ");
			Console.WriteLine("------------------------------------------------------------");
			foreach (var patient in patients)
			{
				Console.WriteLine(patient);
				Console.WriteLine();
			}
		}

		public static void BookAppointments(int ID)
		{
			Dictionary<int, string> doctorMap = doctors.ToDictionary(d => d.ID, d => d.Name);
			Console.WriteLine("Available Doctors:");
			foreach (var doctor in doctorMap)
			{
				Console.WriteLine($"ID: {doctor.Key}, Name: {doctor.Value}");
			}

			var appointment = new Appointments();
			appointment.CallForInput(ID);

			// Check for conflicts
			var conflict = appointments.Any(a => a.DoctorID == appointment.DoctorID &&
				(appointment.dateTime < a.closeTime && appointment.closeTime > a.dateTime));

			if (conflict)
			{
				Console.WriteLine("This doctor is unavailable at the selected time.");
				return;
			}

			appointments.Add(appointment);
			Console.WriteLine("Appointment booked successfully!");
		}

		public static void ViewAppointments(int id, string userType)
		{
			Console.WriteLine("------------------------------------------------------------");
			foreach (var appointment in appointments.Where(a => (userType == "doctor" && a.DoctorID == id) ||
																(userType == "patient" && a.PatientID == id)))
			{
				Console.WriteLine($"Doctor ID: {appointment.DoctorID}, Patient ID: {appointment.PatientID}, " +
								  $"Appointment Time: {appointment.dateTime}, Ends: {appointment.closeTime}");
			}
			Console.WriteLine("------------------------------------------------------------");
		}
	}
}
