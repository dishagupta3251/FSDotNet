using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product(int Id, string name, double price, int quantity)
        {
            ID = Id;
            Name = name;
            Price = price;
            Quantity = quantity;

        }
    }
   
}
