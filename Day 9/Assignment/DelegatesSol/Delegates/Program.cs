using System;

namespace ClinicManagement
{
	internal class Program
	{
		void MenuForRegistrationLogin(string check)
		{
			ClinicManagementServices services = new ClinicManagementServices();
			int option = -1;
			do
			{
				Console.WriteLine("1-Register \n2-Login \n0-Back");
				option = Convert.ToInt32(Console.ReadLine());
				switch (option)
				{
					case 0:
						Menu();
						break;
					case 1:
						services.Register(check);
						break;
					case 2:
						services.Login(check);
						break;
					default:
						Console.WriteLine("Invalid input");
						break;
				}
			} while (option != 0);
		}

		void Menu()
		{
			int option = -1;
			do
			{
				Console.WriteLine("1-Doctors \n2-Patients \n0-Exit");
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

		static void Main(string[] args)
		{
			new Program().Menu();
		}
	}
}
