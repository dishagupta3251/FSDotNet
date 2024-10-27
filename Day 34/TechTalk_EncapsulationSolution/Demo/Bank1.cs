using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTalk_Encapsulation
{
    public class Bank1
    {
        public int Amount;
    }
    class Program
    {
        public static void Main()
        {
            Bank1 bank = new Bank1();
           
            bank.Amount = 50;
            Console.WriteLine(bank.Amount);
            bank.Amount = -150;
            Console.WriteLine(bank.Amount);
            Console.ReadKey();
        }
    }
}
