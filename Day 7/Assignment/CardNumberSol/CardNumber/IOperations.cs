using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardNumber
{
    public interface IOperations
    {
        public string Reverse();

        public string Multiply();

        public int AddDigits(int num);
        public int Sum();

        public void Divide();

    }

}
