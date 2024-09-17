using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5
{
    internal class NumToWord
    {
        using System;

namespace NumberToWords
{
    class Program
    {
        static void Main()


        {
            Console.Write("Enter a number (0 to 9999): ");
            string input = Console.ReadLine();

           
            int number = int.Parse(input);

            Console.WriteLine($"{number} in words is: {ConvertNumberToWords(number)}");
        }

        static string ConvertNumberToWords(int number)
        {
            if (number == 0) return "Zero";

            string[] ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string words = "";

            if (number / 1000 > 0)
            {
                words += ones[number / 1000] + " Thousand";
                number %= 1000;
                if (number > 0) words += " ";
            }

           
            if (number / 100 > 0)
            {
                words += ones[number / 100] + " Hundred";
                number %= 100;
                if (number > 0) words += " and ";
            }

            if (number >= 20)
            {
                words += tens[number / 10];
                number %= 10;
                if (number > 0) words += " ";
            }
            else if (number >= 10)
            {
                words += teens[number - 10];
                number = 0;
            }

            
            if (number > 0)
            {
                words += ones[number];
            }

            return words.Trim();
        }
    }
}

    }
}
