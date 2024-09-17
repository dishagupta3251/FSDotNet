using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems
{
    internal class Divisbleby3
    {
        public void calculate()
        {
            List<int> num = new List<int>();
            string res = "";
            while (true)
            {
                Console.Write("Enter a value or -1 to exit: ");
                int i = Convert.ToInt32(Console.ReadLine());
                if (i == -1)
                {
                    break;
                }
                num.Add(i);
            }
            for (int i = 0; i < num.Count; i++) {
                if (num[i] % 10 == 3 || num[i] % 3 == 0)
                {
                    res = res + " " + num[i];
                }
            }
            Console.WriteLine("All the numbers are"+res);

        }
    }
}


