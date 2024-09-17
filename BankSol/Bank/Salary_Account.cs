using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class Salary_Account:ITransaction
    {
        public double charge = 0;
        public double minBalance = 0.0;
        public string Name;
        public double currentBalance;
        public Salary_Account() { }
        public Salary_Account(string name, double currentbalance)
        {
            Name = name;
            currentBalance = currentbalance;
        }
        public void withdraw(double amount)
        {
            if (currentBalance < amount) { Console.WriteLine("Sorry! Cannot withdraw, Low Balance"); }
            else
            {
                currentBalance = currentBalance - currentBalance * charge;
                Console.WriteLine($"{Name} your balance after deducing transaction charge {currentBalance}");
                currentBalance = currentBalance - amount;
            }
            Console.WriteLine($"{Name} current Balance is {currentBalance}");

        }
        public void add(double amount)
        {
            currentBalance = currentBalance - currentBalance * charge;
            Console.WriteLine($"{Name} your balance after deducing transaction charge {currentBalance}");
            currentBalance += amount;
            Console.WriteLine($"{Name} current Balance is {currentBalance}");
        }

    }
}
