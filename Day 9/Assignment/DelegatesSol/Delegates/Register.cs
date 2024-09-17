using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement
{
    internal class Register
    {
        public int ID = 100;
        public string Name {  get; set; }

        public string Email {  get; set; }

        public string PhoneNo {  get; set; }

        public void TakeInputFromConsole()
        {
            ID++;
            Console.WriteLine("Enter your name");
            Name = Console.ReadLine();
            Console.WriteLine("Enter your email");
            Email = Console.ReadLine();
            Console.WriteLine("Enter your phone no");
            PhoneNo = Console.ReadLine();

        }
    }
}
