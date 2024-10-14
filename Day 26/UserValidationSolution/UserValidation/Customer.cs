using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserValidation
{
    internal class Customer
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; }= string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        public override string ToString()
        {
            return $"\n Name: {Name} \n Email: {Email} \n Gender: {Gender} \n Address: {Address} \n Date of Birth: {DateOfBirth} ";
        }
    }
}
