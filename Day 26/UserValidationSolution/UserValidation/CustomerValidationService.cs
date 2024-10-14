using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UserValidation
{
    internal class CustomerValidationService : IValidationService
    {
        List<Customer> customers;
        public CustomerValidationService() { 
            customers = new List<Customer>();
        }
        public string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void GetAllValidCustomer(Customer customer)
        {
            int i = 0;
            foreach(var c in customers)
            {
                i++;
                Console.WriteLine("ID: "+i);
                Console.WriteLine(c);
                Console.WriteLine();
            }
        }

        public Customer GetCustomer()
        {
            Customer customer = new Customer();

            Console.Write("Enter your Name: ");
            customer.Name = Console.ReadLine().ToUpper();

            Console.Write("Enter your Date of Birth (YYYY-MM-DD): ");
            customer.DateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter your Gender (M/F): ");
            customer.Gender = Console.ReadLine().ToUpper();

            Console.Write("Enter your Address: ");
            customer.Address = Console.ReadLine();

            do
            {
                Console.Write("Enter your Email: ");
                customer.Email = Console.ReadLine();
            } while (VerifyEmail(customer.Email) == false);
            

            return customer;
        }

        public bool SaveCustomerData(Customer customer,string otp)
        {
            bool isSuccess=true;
            try
            {
                if (VerifyOTP(otp))
                {
                    customers.Add(customer);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Verification successfull!!");
                    Console.ResetColor();
                    Console.WriteLine();    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new Exception("Verification unsuccessfull");
                    
                }
            }
            catch(Exception ex)
            {
                isSuccess = false;
                Console.WriteLine(ex.Message);

            }
           return isSuccess;
            
        }

      
        public bool VerifyEmail(string email)
        {
            bool valid = true;
            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch(FormatException ex) { 

                valid = false;
                Console.WriteLine(ex.Message);
            }
            return valid;
        }

        public bool VerifyOTP(string otp)
        {
            bool isValid = true;
            Console.WriteLine("Enter the otp");
            var userOtp=Console.ReadLine();
            try
            {
                if (otp == null) { throw new Exception("Otp cannot be empty"); }
                else
                {
                    if (otp != userOtp) { isValid = false; }
                 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return isValid;
        }
    }
}
