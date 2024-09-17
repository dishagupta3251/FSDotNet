using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense
{
    internal class Employee
    {
        public Employee() { }
        public Employee(int id, string name, string designation, double salary, DateTime dateOfBirth, double travel, double meal, double supplies)
        {
            Id = id;
            Name = name;
            Designation = designation;
            Salary = salary;
            DateOfBirth = dateOfBirth;
            Travel = travel;
            Meal = meal;
            Supplies = supplies;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public double Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Travel { get; set; }
        public double Meal { get; set; }
        public double Supplies { get; set; }

    
}
}
