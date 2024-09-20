using System;

namespace ClinicManagement
{
	internal class Appointments
	{
		public int DoctorID { get; set; }
		public DateTime dateTime { get; set; }
		public DateTime closeTime { get; set; }
		public int PatientID { get; set; }

		public void CallForInput(int patientId)
		{
			Console.Write("Enter doctor id: ");
			DoctorID = Convert.ToInt32(Console.ReadLine());
			Console.Write("Enter the date and time (yyyy-mm-dd hh:mm): ");
			dateTime = DateTime.Parse(Console.ReadLine());
			PatientID = patientId;
			closeTime = dateTime.AddMinutes(30);
		}
	}
}
