using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Promotion
{
    public class PromotionOperations
    {
        List<EmployeePromotion> names = new List<EmployeePromotion>();
        public void TakeInputFromConsole()
        {
            
            Console.WriteLine("Enter the names");
            string name = "Enter";
            do
            {
                name = Console.ReadLine();
                names.Add(new EmployeePromotion(name));
            } while (!string.IsNullOrEmpty(name));
            Console.WriteLine("The current size of the collection "+names.Capacity);
            names.TrimExcess();
            Console.WriteLine("The size after removing the extra space is " + names.Capacity);

        }
        public void CheckForPosition()
        {
            Console.WriteLine("Enter a name to check position");
            string name=Console.ReadLine();
            int index = names.FindIndex(emp => string.Equals(emp.Name, name, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(index+1);
        }
        public void Sort()
        {
            Console.WriteLine("Promoted employee list");
            names.Sort();
            foreach (EmployeePromotion p in names)
            {
                Console.WriteLine(p);
            }
        }
    }
}
