using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems
{
    internal class PrimeNumbers
    {
        public bool prime(int n)
        {
            for (int t = 2; t <= n / 2; t++)
            {
                if (n % t == 0)
                    return true;
            }
            return false;
        }
        public void calculate()
        {
            Console.WriteLine("Enter the min value");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the max value");
            int max = Convert.ToInt32(Console.ReadLine());
            List<int> numbers = new List<int>();
            for (int i = min; i <= max; i++) {
                if (prime(i) == false) numbers.Add(i);
            }
            Console.WriteLine("All the prime numbers between range is ");
            foreach (int i in numbers)
            {
                Console.WriteLine(i);
            }
    }
    }
}
