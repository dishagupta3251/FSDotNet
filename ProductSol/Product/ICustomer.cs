using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public interface ICustomer
    {
        public void Buy(int id, int quantity, Customer customer, Product[] product);
    }
}
