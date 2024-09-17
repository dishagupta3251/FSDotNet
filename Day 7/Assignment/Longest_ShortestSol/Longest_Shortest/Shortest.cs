using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longest_Shortest
{
    public class Shortest : ICalulate
    {
        public void calculate(string[] s)
        {
            int min = int.MaxValue; string str = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length < min)
                {
                    min = s[i].Length;
                    str = s[i];
                }
            }
            Console.WriteLine("Shortest string is " + str);
        }
    }
}
