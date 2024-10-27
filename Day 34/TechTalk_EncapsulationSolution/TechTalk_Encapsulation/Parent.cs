using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTalk_Encapsulation
{
    public class Parent
    {
        internal void print() { Console.WriteLine("Hello"); }
        public void print2() { Console.WriteLine("This is print2"); }
        protected void print3() { Console.WriteLine("This is print3"); }
        private void  print4() { Console.WriteLine("This is print4"); }

        public void display()
        {
            print4();
        }

    }

    
}
