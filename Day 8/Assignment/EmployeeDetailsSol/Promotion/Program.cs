using System.Linq;
using System.Xml.Serialization;

namespace Promotion
{
    internal class Program
    {
        public void Call()
        {
            Operations operations = new Operations();
            int choice = 0;
            do
            {
                 Console.WriteLine("Enter choice");
                 Console.WriteLine(" 1-View All\n 2-Add Employee\n 3-Modify by ID\n 4-Get details by ID\n 5-Delete employee by ID\n 0-Exit ");
               
                choice =Convert.ToInt32(Console.ReadLine());
                switch (choice) {
                    case 1: operations.GetDetails(); break;
                    case 2: operations.Add();break;
                    case 3: operations.Edit();break;
                    case 4: operations.FindId();break;
                    case 5: operations.Remove();break;
                    case 0:break;
                }


            }while(choice != 0);
        }

        
        static void Main(string[] args)
        {
           
           new Program().Call();
           
        }
    }
}
