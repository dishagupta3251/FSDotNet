using System.Diagnostics.CodeAnalysis;

namespace Problems
{
    internal class Day_5
    {
        public void average()
        {
            Console.WriteLine("Enter 10 numbers");
            int[] ar = new int[10];
            int sum = 0, c = 0;
            for (int i = 0; i < 10; i++)
            {
                ar[i] = Convert.ToInt32(Console.ReadLine());
                if (ar[i] % 7 == 0)
                {
                    sum += ar[i];
                    c++;
                }
            }
            Console.WriteLine("Average of numbers that are divisible by 7 are " + sum / c);
        }
        static void Main(string[] args)
        {
            Day_5 av = new Day_5();
            av.average();
            PrimeNumbers pm = new PrimeNumbers();
            pm.calculate();
            Divisbleby3 dv = new Divisbleby3();
            dv.calculate();
        }
    }
}
