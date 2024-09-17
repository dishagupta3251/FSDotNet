using System.Security.Cryptography.X509Certificates;

namespace Promotion
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
          PromotionOperations operations = new PromotionOperations();
            operations.TakeInputFromConsole();
            operations.CheckForPosition();
            operations.Sort();
            
        }
    }
}
