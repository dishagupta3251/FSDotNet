using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDetails
{
    public class Details
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public void TakeInput(int id)
        {
            try
            {
                ID = id;
                Console.Write("Enter your name: ");
                Name = Console.ReadLine();
                Console.Write("Email:");
                Email = Console.ReadLine();
                Console.Write("Phone: ");
                Phone = Console.ReadLine();
                Console.Write("Address: ");
                Address = Console.ReadLine();
                Console.WriteLine();

            }
            catch ( FormatException ex)
            {
                Console.WriteLine("Inproper input format");
            }
            
        }
    }
}
