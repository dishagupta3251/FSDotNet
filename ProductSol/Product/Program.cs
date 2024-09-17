using System.ComponentModel;
using System.Globalization;
using System.Transactions;

namespace Product
{
    internal class Program
    {
        public Product[] p;
        public void get(Product[] product){
            p=product;
        }
        public void viewAll()
        {
            for (int i = 0; i < p.Length; i++)
            {
                Console.WriteLine("Id: " + p[i].ID);
                Console.WriteLine("Name: " + p[i].Name);
                Console.WriteLine("Price: " + p[i].Price);
                Console.WriteLine("Quantity: " + p[i].Quantity);
                Console.WriteLine();
            }
        }
        public void menu()
        {
            Console.WriteLine("Choose if you are buyer or supplier");
            Console.WriteLine("1-Customer");
            Console.WriteLine("2-Supplier");
            Console.WriteLine("0-Exit");
            
                 int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        buyer_supplierMenu("Buy Products");
                        break;
                    case 2:
                        buyer_supplierMenu("Add Products");
                        break;
                case 0: break;
                    
                }
           
           
        }
        public void buyer_supplierMenu(string choice)
        {
            Console.WriteLine($"1-{choice}");
            Console.WriteLine("2-View All Products");
            Console.WriteLine("0-Exit");
            int opt = Convert.ToInt32(Console.ReadLine());
            switch (opt)
            {
                case 1:
                    service(choice);
                    buyer_supplierMenu(choice);
                    break;
                case 2:
                    viewAll();
                    buyer_supplierMenu(choice);
                    break;
                case 0:
                    break;
            }
        }

        public Customer customerDetails()
        {
            Console.WriteLine("Enter your details");
            Customer customer = new Customer();
            Console.Write("Customer ID: ");
            customer.Id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Name: ");
            customer.Name = Console.ReadLine();
            Console.Write("Phone: ");
            customer.Phone = Console.ReadLine();
            Console.WriteLine();
            return customer;

        }
        
        public void service(string choice)
        {
            ProductService service = new ProductService();
            if(choice=="Buy Products")
            {
                Customer c = customerDetails();
                Console.Write("Enter Product ID: ");
                int pId=Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Quantity: ");
                int pQuantity= Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                service.Buy(pId,  pQuantity, c, p);

            }
            else
            {
                Supplier s=new Supplier();
                Console.Write("Supplier ID: ");
                s.ID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Name: ");
                s.Name = Console.ReadLine();
                Console.Write("Enter Product ID: ");
                int pId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Quantity: ");
                int pQuantity = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                service.Add(pId, pQuantity, p,s);

            }

        }
        static void Main(string[] args)
        {
            Program program = new Program();
            Product[] p=new Product[3]
            {
                new Product(101,"Oil",100.0,10),
                new Product(102,"Salt",150.0,4),
                new Product(103,"Sugar",300.0,5),

            };
            program.get(p);
            program.menu();
           

            
        }
    }
}
