using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class ProductService : ISupplier, ICustomer
    {
        public bool IsAvailable(Product[] p, int ID)
        {
            int c = 0;
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].ID == ID)
                {
                    c++;
                    break;

                }
            }
            return c == 0;
        }
        public void Buy(int id, int quantity, Customer customer, Product[] product)
        {
            if (IsAvailable(product, id))
            {
                Console.WriteLine("Product not available");
                Console.WriteLine();
            }
            else
            {
                for (int i = 0; i < product.Length; i++)
                {
                    if (product[i].ID == id && product[i].Quantity >= quantity)
                    {
                        product[i].Quantity = product[i].Quantity - quantity;
                        Console.WriteLine($"{customer.Name} has bought {quantity} pieces of {product[i].Name}");
                        Console.WriteLine();
                    }
                    
                }
               
            }
        }
        
            
           

        public void Add(int id, int quantity, Product[] product, Supplier s)
        {
            if (IsAvailable(product, id))
            {
                Console.WriteLine("Product not available");
                Console.WriteLine();

            }
            else
            {
                for(int i = 0;i<product.Length;i++)
                {
                    if(product[i].ID == id)
                        {product[i].Quantity += quantity;
                    Console.WriteLine($" {s.Name} added  {quantity} of {product[i].Name}");
                    Console.WriteLine($"Now we have {product[i].Quantity} quantites of {product[i].Name}");
                        break;
                    }
                }
                
                
            }

        }
    }
    }

