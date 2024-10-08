using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Destructor : IDisposable
    {
        public Destructor()
        {
        }
        ~Destructor() {
            Console.WriteLine("This is destructor");
        }

        public void Dispose()
        {
            Console.WriteLine("This is dispose method");
        }
    }
}
