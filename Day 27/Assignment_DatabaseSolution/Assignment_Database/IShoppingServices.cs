using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Database
{
    internal interface IShoppingServices
    {
        public void GetAllOrders();
        public void GetOrderSummary(int ord_no);
        public void ViewShipperDetails(int ord_no);
      
    }
}
