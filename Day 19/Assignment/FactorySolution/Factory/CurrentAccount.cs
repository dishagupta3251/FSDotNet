using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    internal class CurrentAccount : Account
    {
        public override void Show(double money)
        {
            Console.WriteLine("Current account");
        }
    }
}
