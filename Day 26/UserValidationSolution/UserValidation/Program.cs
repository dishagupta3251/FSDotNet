using System.Xml.Serialization;

using System;
using System.Threading.Tasks;

namespace UserValidation
{
    internal class Program
    {
        static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter your choice");
            Console.WriteLine("1-Get all the valid customers");
            Console.WriteLine("0-Exit");
            Console.ResetColor();

        }
        static async Task RunApp()
        {
            CustomerValidationService customerValidationService = new CustomerValidationService();
            var customer=customerValidationService.GetCustomer();
            string otp = customerValidationService.GenerateOTP();
           

            if (customerValidationService.SaveCustomerData(customer, otp))
           { Menu();
                int choice=Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        customerValidationService.GetAllValidCustomer(customer);
                        break;
                    case 0:
                        break;
                }
            }
            
        }
         //required SendGrid API Integration
        static async Task Main(string[] args)
        {
            await RunApp(); 
        }
    }
}
