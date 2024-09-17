using System.Threading.Channels;

namespace Bank
{
    internal class Program
    {
        double minBal;
        int percent;
        NRI_Account account = new NRI_Account("Disha", 200000.0);
        Salary_Account salary_account = new Salary_Account("ABC", 40000.0);
        Saving_Account saving_account = new Saving_Account("XYZ", 48759.0);


        public void menu()
        {
            Console.WriteLine("Please enter your account type");
            Console.WriteLine("1-NRI Account");
            Console.WriteLine("2-Salary Account");
            Console.WriteLine("3-Saving Account");
            Console.WriteLine("0-Exit");
            int option=Convert.ToInt32(Console.ReadLine());
            switch (option) {
                case 1: secondMenu("NRI");
                    break;
                case 2:
                    secondMenu("Salary");
                    break;
                case 3:
                    secondMenu("Saving");
                    break;
                case 0:
    
                    break;
            } 
        }
        public void secondMenu(string choice) {
            Console.WriteLine("1-Withdraw");
            Console.WriteLine("2-Add Money");
            int option= Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter amount");
            double amount=Convert.ToDouble(Console.ReadLine());
            switch (option) {
                case 1: if (choice == "NRI") account.withdraw(amount);
                    else if (choice == "Salary") salary_account.withdraw(amount);
                else saving_account.withdraw(amount);
                    break;

                case 2:
                    if (choice == "NRI") account.add(amount);
                    else if (choice == "Salary") salary_account.add(amount);
                    else  saving_account.add(amount);
                    break;
            }

        }
  

        static void Main(string[] args)
        {
           Program program = new Program();
            
            program.menu();
            

        }
    }
}
