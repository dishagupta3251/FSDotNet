using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardNumber
{
    class CardOperations : IOperations
    {
        public string Number {  get; set; }
        public string Reverse()
        {
            string nstr = "";
            for (int i = Number.Length-1; i >=0; i--) {
                nstr += Number[i];
            }

            return nstr;
        }
        public string Multiply()
        {
            Number = Reverse();
            string nNumber = "";
            for(int i =0;i<Number.Length;i++)
            {
                
                if(i%2!=0)
                {
                    int res = ((int)Char.GetNumericValue(Number[i])) * 2;
                   
                    if(res>9)
                    {
                       res=AddDigits(res);
                    }

                    nNumber += res;
                }
                else nNumber += Number[i];
            }
           
            return nNumber;
            

            
        }
        public int AddDigits(int num)
        {
            int sum = 0;
            while (num > 0) {
                sum = sum + num % 10;
                num /= 10;
            }
            return sum;
        }

        public int Sum()
        {
            string number=Multiply();
            int sum = 0;    
            for (int i = 0; i < number.Length; i++) { 
                sum= sum + (int)Char.GetNumericValue(number[i]);
            }
            return sum;
        }

        public void Divide()
        {
            try
            {
                int number = Sum();
                int result = number % 10;
                if (result == 0)
                    Console.WriteLine("The card Number is Valid");
                else
                    Console.WriteLine("The card number is Invalid");

            }
            catch (DivideByZeroException e) {
                Console.WriteLine(e.Message);
            }
           

            
        }

        
    }
}
