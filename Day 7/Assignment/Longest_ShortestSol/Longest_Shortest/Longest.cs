using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longest_Shortest
{
    public class Longest:ICalulate
    {
        public void calculate(string[] s)
        {
            int max = 0;string str="";
            for (int i = 0; i < s.Length; i++) {
                if (s[i].Length>max)
                {
                    max = s[i].Length;
                    str = s[i];
                }
            }
            Console.WriteLine("Longest string is "+str);
        }
    }
}
