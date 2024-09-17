using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public interface ISupplier
    {
        public void Add(int id, int quantity, Product[] product, Supplier s);

    }
}
