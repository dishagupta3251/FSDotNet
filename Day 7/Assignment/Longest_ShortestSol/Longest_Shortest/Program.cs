namespace Longest_Shortest
{
    internal class Program
    {
        string[] Input()
        {
            
            Console.WriteLine("Enter the string separated by comma");
            string str = Console.ReadLine();
            Console.WriteLine();
            if (!str.Contains(','))
            {
                throw new ArgumentException("Invalid input, you must enter string separated by comma");
            }
            string [] output = str.Split(',');
            return output;
           
        }
        static void Main(string[] args)
        {
            try
            {
                string[] words = new Program().Input();
                ICalulate longest = new Longest();
                longest.calculate(words);
                ICalulate shortest = new Shortest();
                shortest.calculate(words);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }




        }
    }
}
