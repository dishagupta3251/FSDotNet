using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Test
    {
        List<string> list { get; set; }
        public string this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }
        public bool this[string element]
        {
            get
            {
                foreach (var item in list)
                {
                    if (item == element) return true;
                }
                return false;
            }
           

        }
        public Test()
        {
            list = new List<string>() {"one","two","three"};
        }
    }
}
