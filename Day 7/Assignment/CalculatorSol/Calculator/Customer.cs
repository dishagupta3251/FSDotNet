using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Customer
    {
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }

        public void TakeInput()
        {
            try
            {
                Console.Write("Enter your name: ");
                Name = Console.ReadLine();
                Console.Write("Enter your date of birth in format yyyy-mm-dd:");
                DateofBirth = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter your Gender");
                Console.WriteLine("1-Male");
                Console.WriteLine("2-Female");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Gender = "Male";
                        break;
                    case 2:
                        Gender = "Female";
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            catch (FormatException ex) { Console.WriteLine("Not a right format"); }
            catch (OverflowException ex) { Console.WriteLine("the value entered is too large or small"); }

        }
    }
}
