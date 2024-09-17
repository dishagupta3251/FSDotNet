namespace PrimeNumber
{
    internal class Program
    {
        public List<int> Input()
        {
            List<int> input = new List<int>();
            int num=Convert.ToInt32(Console.ReadLine());
            do
            {
                 num=Convert.ToInt32(Console.ReadLine());
                input.Add(num);

            }while (num!=0);
            return input;
        }
        public bool check(int num)
        {
            for (int i = 2; i < num/2; i++) {
                if (num % i == 0) return false;
            }
            return true;
        }
        void print(List<int> list)
        {
            Console.Write("Prime Numbers are: ");
            for (int i = 0; i < list.Count-1; i++) { 
                Console.Write(" "+list[i]);
            }
            
        }
        public void ListOfPrimeNumbers(List<int> input) {
            List<int> list = new List<int>();
            for (int i = 0; i < input.Count; i++) {
                if (check(input[i])) list.Add(input[i]);
            }
            print(list);
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter the numbers");
                new Program().ListOfPrimeNumbers(new Program().Input());
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
           
        }
    }
}
