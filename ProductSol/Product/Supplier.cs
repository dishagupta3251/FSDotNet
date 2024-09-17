using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public Supplier() { }
        public Supplier(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
