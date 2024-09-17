using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Customer() { }
        public Customer(int id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;

        }
    }
}
