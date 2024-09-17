using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Promotion
{
    public class Employee:IComparable<Employee>,IEquatable<Employee>
    {
        public int id, age;
        public string name;
        public double salary;
        public Employee()
        {
        }
        public Employee(int id, int age, string name, double salary)
        {
            this.id = id;
            this.age = age;
            this.name = name;
            this.salary = salary;
        }
        public Employee TakeEmployeeDetailsFromUser()
        {
            Employee employee = new Employee();
            
               
                Console.WriteLine("Please enter the employee Id");
                employee.id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter the employee name");
                employee.name = Console.ReadLine() ?? "";
                Console.WriteLine("Please enter the employee age");
                employee.age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter the employee salary");
                employee.salary = Convert.ToDouble(Console.ReadLine());
                
            return employee;
        }
        public Employee Modify(int id)
        {
            Employee employee = new Employee();
            employee.id = id;
            Console.WriteLine("Please enter the employee name");
            employee.name = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter the employee age");
            employee.age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter the employee salary");
            employee.salary = Convert.ToDouble(Console.ReadLine());
            return employee;
        }
        public override string ToString()
        {
            return  "Employee ID : " +id + "\nName:" + name + " \nAge:" + age +
            " \nSalary:" + salary;
        }

        public int CompareTo(Employee? other)
        {
            return this.salary.CompareTo(other.salary);
        }

        public bool Equals(Employee? other)
        {
            if(this.id==other.id) return true;
            return false;
        }


        public int Id
        {
            get => id; set => id = value; }
public int Age
        {
            get => age; set => age = value; }
public string Name
        {
            get => name; set => name = value; }
public double Salary
        {
            get => salary; set => salary = value; }
        }
    }
