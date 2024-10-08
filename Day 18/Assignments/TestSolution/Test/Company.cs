using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Company
    {
        public static string Name { get; set; }
        static Company()
        {
            Name = "Celsior";
        }
        public Company(){
            Name = "Genspark";
            }

    }
}
