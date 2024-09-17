using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Validate : IValidateAge
    {
        public Customer Customer { get; set; }
        public Validate()
        {
            Customer = new Customer();
        }
        public void ValidateAge()
        {
            Customer.TakeInput();
            DateTime dob = Customer.DateofBirth;
            int age =DateTime.Now.Year- dob.Year;
            Console.WriteLine("Your age is "+age);
            if (age >= 18)
            {
                Console.WriteLine($"{Customer.Name} you are eligible to vote");
            }
            else
            Console.WriteLine($"{Customer.Name} you are not eligible to vote");
        }
    }

}
