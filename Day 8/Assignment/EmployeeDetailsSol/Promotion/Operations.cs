using System;
using System.Collections.Generic;
using System.Linq;

namespace Promotion
{
    public class Operations
    {
       
        Dictionary<int, Employee> employees = new Dictionary<int, Employee>()
        {
            {101,new Employee(101, 23, "Disha", 237887) }
        };
        // int nextId = 100;

        public void GetDetails()
        {
            Console.WriteLine("All the employees are:");
            foreach (var employee in employees.Values)
            {
                Console.WriteLine($"{employee}\n");
            }

        }

        public void Add()
        {
            Employee employee = new Employee().TakeEmployeeDetailsFromUser();
            if (employees.ContainsKey(employee.Id))
            {
                Console.WriteLine("ID already exists. Enter a new unique ID.");
                return;
            }

            //nextId++;
            employees[employee.Id] = employee;
        }

        public void Sorting()
        {
            var sortedEmployees = employees.Values.OrderBy(e => e.Salary).ToList();
            Console.WriteLine("All the employees sorted according to salary are:");
            foreach (Employee employee in sortedEmployees)
            {
                Console.WriteLine(employee + "\n");
            }
        }

        public void Find()
        {
            Console.WriteLine("Enter the name to be searched:");
            string name = Console.ReadLine();
            var matchingNames = employees.Values.Where(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (Employee employee in matchingNames)
            {
                Console.WriteLine(employee);
                Console.WriteLine();
            }
        }

        public void Check()
        {
            Console.WriteLine("Enter the details of employee to compare age:");
            Employee employee = new Employee().TakeEmployeeDetailsFromUser();
            var matchingNames = employees.Values.Where(item => item.Age > employee.Age).ToList();
            Console.WriteLine("All the employees elder than the given employee are:");
            foreach (Employee e in matchingNames)
            {
                Console.WriteLine(e);
                Console.WriteLine();
            }
        }

        public void Remove()
        {
            Console.WriteLine("Enter the ID to be removed:");
            string input = Console.ReadLine();

            try
            {
                int id = int.Parse(input);
                if (employees.ContainsKey(id))
                {
                    employees.Remove(id);
                    Console.WriteLine("Employee removed successfully.");
                }
                else
                {
                    Console.WriteLine("Employee ID not found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        public void FindId()
        {
            Console.WriteLine("Enter the ID to find the employee:");


            int id = Convert.ToInt32(Console.ReadLine());
            if (employees.ContainsKey(id))
            {
                Console.WriteLine("Employee details:");
                Console.WriteLine(employees[id]);
            }
            else
            {
                Console.WriteLine("Employee ID not found.");
            }
        }



        public void Edit()
        {
            Console.WriteLine("Enter the ID to be modified:");

            int id = Convert.ToInt32(Console.ReadLine());
            if (employees.ContainsKey(id))
            {
                Employee modifiedEmployee = new Employee().Modify(id);
                employees[id] = modifiedEmployee;
                Console.WriteLine("Employee updated successfully.");
                Console.WriteLine(modifiedEmployee);
            }
            else
            {
                Console.WriteLine("Employee ID not found.");
            }


        }
    }
}

