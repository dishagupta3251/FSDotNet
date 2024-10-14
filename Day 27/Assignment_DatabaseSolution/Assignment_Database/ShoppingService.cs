using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Assignment_Database
{
    internal class ShoppingService : IShoppingServices
    {
        SqlConnection connection = new SqlConnection("server=5CD413DKR1\\DEMOINSTANCE;Integrated Security=true;Initial catalog=Northwind;TrustServerCertificate=true;");
        public void GetAllOrders()
        {
            Console.Write("Enter your Customer ID: ");
            string id = Console.ReadLine();
            string query = "Select * from Orders where CustomerID=@custID order by OrderDate desc ";
            SqlCommand cmd = new SqlCommand(query,connection) ;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            try
            {
                cmd.Parameters.AddWithValue("custID", id);
                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows) {
                    Console.Write($"OrderID: {row["OrderID"]} ");
                    Console.WriteLine($"OrderDate: {row["OrderDate"]}");
                    Console.WriteLine("------------------------------------");
                }
                
                
            }
            catch (Exception e){ Console.WriteLine(e.Message); }
            
        }

        public void GetOrderSummary(int ord_no)
        {
           
            SqlCommand sqlCommand = new SqlCommand("Select o.OrderID,o.ShipVia, c.ContactName, p.ProductName from Orders o join Customers c on o.CustomerID=c.CustomerID join [Order Details] od on od.OrderID=o.OrderID join Products p on p.ProductId=od.ProductID where o.OrderID=@Id "
                ,connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sqlCommand;
            DataSet dataSet = new DataSet();
            try
            {
                sqlCommand.Parameters.AddWithValue("Id", ord_no);
                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"OrderID: {row["OrderID"]} ");
                    Console.WriteLine($"Customer Name: {row["ContactName"]}");
                    Console.WriteLine($"Shipper ID: {row["ShipVia"]}");
                    Console.WriteLine($"Product Name: {row["ProductName"]}");
                    Console.WriteLine("------------------------------------");
                }


            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        }

        public void ViewShipperDetails(int ord_no)
        {
            SqlCommand sqlCommand = new SqlCommand("Select o.OrderID, s.ShipperID, s.CompanyName, s.Phone from Orders o join Shippers s on o.ShipVia=s.ShipperID  where o.OrderID=11011 "
                , connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sqlCommand;
            DataSet dataSet = new DataSet();
            try
            {
                sqlCommand.Parameters.AddWithValue("Id", ord_no);
                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"ShipperID: {row["ShipperID"]} ");
                    Console.WriteLine($"Company Name: {row["CompanyName"]}");
                    Console.WriteLine($"Phone: {row["Phone"]}");
                    Console.WriteLine("------------------------------------");
                }


            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
