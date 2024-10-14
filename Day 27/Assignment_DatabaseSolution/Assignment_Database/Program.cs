using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace Assignment_Database
{
    internal class Program
    {
        void ApplicationStart()
        {
            
            UserServices userServices = new UserServices();
            Console.WriteLine("Welcome!!");
            int choice = -1;
            do
            {
                Console.WriteLine();
                Console.WriteLine("1-Register");
                Console.WriteLine("2-Login");
                Console.WriteLine("0-Exit");
                Console.WriteLine("Enter your choice");
                Console.WriteLine();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        userServices.Register_User();
                        break;
                    case 2:
                        var user=userServices.Login_User();
                        if(user!=null) LoginServices(user);
                        break;
                }
            } while (choice != 0);
            
            
        }
         int GetOrderNumber()
        {
            Console.Write("Enter order no.: ");
            int ord_no = Convert.ToInt32(Console.ReadLine());
            return ord_no;
        }
        void LoginServices(string username)
        {
            
            ShoppingService shoppingService = new ShoppingService();
            Console.WriteLine($"Welcome {username}");
            int choice = -1;
            do
            {
                Console.WriteLine();
                Console.WriteLine("1-View All Your Orders");
                Console.WriteLine("2-Order Summary");
                Console.WriteLine("3-Shipper Details");
                Console.WriteLine("4-Update Password");
                Console.WriteLine("0-Exit");
                Console.WriteLine("Enter your choice");
                Console.WriteLine();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        shoppingService.GetAllOrders();
                        break;
                    case 2:
                        shoppingService.GetOrderSummary(GetOrderNumber());
                        break;
                    case 3:
                        shoppingService.ViewShipperDetails(GetOrderNumber());
                        break;
                    case 4:
                        UserServices users = new UserServices();
                        users.UpdatePassword(username);
                        break;
                    

                }
            } while (choice != 0);
            
           
            
            

        }
        
        static void Main(string[] args)
        {
           Program program = new Program();
            program.ApplicationStart();
            
        }
    }
}
