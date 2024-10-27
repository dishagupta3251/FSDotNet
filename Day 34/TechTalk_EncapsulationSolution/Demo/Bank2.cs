using System;
namespace EncapsulationDemo
{
    public class Bank2
    {
        protected internal int Amount;
        
        public int GetAmount()
        {
            return Amount;
        }
        public void SetAmount(int Amount)
        {
            if (Amount > 0)
            {
                this.Amount = Amount;
            }
            else
            {
                throw new Exception("Please Pass a Positive Value");
            }
        }
    }
 
    class Program
    {
        public static void Main()
        {
            try
            {
                Bank2 bank = new Bank2();
                
                //bank.Amount = 50; //Compile Time Error
                //Console.WriteLine(bank.Amount); //Compile Time Error

                bank.SetAmount(10);
                Console.WriteLine(bank.GetAmount());

                
                bank.SetAmount(-150);
                Console.WriteLine(bank.GetAmount());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}