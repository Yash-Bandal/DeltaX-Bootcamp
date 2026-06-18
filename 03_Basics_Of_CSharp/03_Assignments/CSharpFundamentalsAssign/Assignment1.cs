using System;

namespace CSharpFundamentalsAssign
{
    public static class Assignment1
    {
        public static void CalculateTotal()
        {
            Console.WriteLine("Assignment 1 - Find Sum of All Numbers ");
          
            int totalSum = 0;


            while (true)
            {
                // 1. Take input from user
                Console.Write("Enter Number (Enter \" ok \" to Stop) : ");
                var input = Console.ReadLine(); //default string


                // base cases
                //1. Handle white or empty space..
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Enter a valid Number");
                    continue;
                }

                // Stop Condition
                if (input.Trim().ToLower() == "ok") //use trim to avoid " ok   "
                {
                    break;
                }


                // Handle invalid string input
                bool isValidNum = int.TryParse(input, out int parsedInput);
                if (!isValidNum)
                {
                    Console.WriteLine("Enter a valid Number");
                    continue;
                }

                totalSum += parsedInput; 
                // No need of 'Convert.YoInt32' after 'TryParse'
                //totalSum += Convert.ToInt32(ParsedInput); 

            }

            Console.WriteLine("Total Sum = {0}", totalSum);


            Console.WriteLine();
        }
    }
}
