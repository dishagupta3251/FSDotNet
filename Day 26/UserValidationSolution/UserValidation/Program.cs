using System.Xml.Serialization;
using SendGrid;
using SendGrid.Helpers.Mail;
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
            await SendToEmail(customer.Email, otp);

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
        public static async Task SendToEmail(string email, string otp)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("dishaguptalko@gmail.com", "Verification");
            var to = new EmailAddress(email);
            var subject = "Your OTP Code";
            var plainTextContent = $"Your OTP is: {otp}";

            var htmlContent = $@"
    <div style='font-family: Arial, sans-serif; color: #333;'>
        <div style='background-color: #4CAF50; padding: 20px; text-align: center;'>
            <h1 style='color: white;'>Customer Verification</h1>
        </div>
        <div style='padding: 20px;'>
            <h2>Hello,</h2>
            <p>Thank you for verifying your account. Please use the following OTP to complete your verification process:</p>
            <p style='text-align: center;'>
                <span style='display: inline-block; padding: 15px 30px; font-size: 20px; font-weight: bold; color: white; background-color: #4CAF50; border-radius: 5px;'>{otp}</span>
            </p>
            <p>This OTP is valid for 10 minutes. Please do not share this code with anyone.</p>
            <p>If you did not request this code, please ignore this email.</p>
            <br>
            <p>Best Regards,<br><strong>Customer Support Team</strong></p>
        </div>
        <div style='background-color: #f1f1f1; padding: 10px; text-align: center; font-size: 12px; color: #777;'>
            <p>© 2024 Customer Verification. All rights reserved.</p>
            <p>This email was sent to {email}. If you did not request this, please contact our support.</p>
        </div>
    </div>
    ";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OTP sent to your email successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to send OTP.");
                Console.ResetColor();
            }

        }
        static async Task Main(string[] args)
        {
            await RunApp(); 
        }
    }
}
