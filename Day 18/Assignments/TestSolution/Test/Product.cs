using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Operator overloading
namespace Test
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        Discount discount_obj;

        public Product()
        {
            discount_obj = new Discount();
            discount_obj.discount = 0.20;
        }

        public static Product operator +(Product a, Product b)
        {
            Product product = new Product();
            product.Id = a.Id;
            product.Name = a.Name+b.Name;
            product.Price = a.Price+b.Price;
            return product;
        }

        public override string ToString() { 
            return Name+" "+Price+" ";
        }
        public double Calculate(double price)
        {
            if(price > 500) price=price-discount_obj.discount*price;
            return price;

        }
        //inner class
        class Discount
        {
           
            public double discount { get; set; }
        }
    }
}
