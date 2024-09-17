using System.Transactions;

namespace CardNumber
{
    internal class Program
    {
        string Input()
        {
            Console.WriteLine("Enter the card no. ");
            
            string checkNumber=Console.ReadLine();
            if (checkNumber.Length != 16) throw new Exception("Invalid Card Number");
            return checkNumber;
        }
        static void Main(string[] args)
        {

            try
            {
                Program program = new Program();
                string input=program.Input();
            
                CardOperations operations = new CardOperations();
                operations.Number=input;
                operations.Divide();
            }
            catch (Exception e) { 
                Console.WriteLine(e.Message);
            }
        }
    }
}
