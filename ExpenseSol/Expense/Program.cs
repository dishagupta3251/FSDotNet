namespace Expense
{
    internal class Program
    {
        Employee CreateEmployee()
        {
            Employee employee = new Employee();
            Console.WriteLine("Name: ");
            employee.Name = Console.ReadLine() ?? "";
            Console.WriteLine("Salary: ");
            employee.Salary = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Designation/ Position: ");
            employee.Designation = Console.ReadLine() ?? "";
            Console.WriteLine("DateOfBirth: ");
            employee.DateOfBirth = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Travel Expense: ");
            employee.Travel = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Meal Expense: ");
            employee.Meal = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Other Supplies: ");
            employee.Supplies = Convert.ToDouble(Console.ReadLine());

            return employee;

        }

        static void Main(string[] args)
        {
            Program p = new Program();
            var employee = p.CreateEmployee();
            Console.WriteLine($"Name :{employee.Name} \nSalary : {employee.Salary} \nDesignation: {employee.Designation} \nDateofBirth: {employee.DateOfBirth}");
            double totalExpense = employee.Meal + employee.Supplies + employee.Travel;
            Console.WriteLine($"{employee.Name}'s total Expense is {totalExpense} ");
            Console.WriteLine("Remaining amount after expense is " + (employee.Salary - totalExpense));
        }
    }
}
