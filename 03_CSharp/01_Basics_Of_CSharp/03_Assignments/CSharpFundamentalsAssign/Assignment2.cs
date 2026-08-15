using System;

namespace CSharpFundamentalsAssign
{
    public static class Assignment2
    {
        public static void FindMax()
        {

            Console.WriteLine("Assignment 2 - Find Maximum Number ");
        

            // Take user input
            Console.WriteLine("Enter Numbers Seperated By \",\"  :");
            string input = Console.ReadLine();

            //Check if input is whitespace or empty
            if (String.IsNullOrWhiteSpace(input)) 
            {
                Console.WriteLine("Enter a valid Number");
                return;
            }

            //generate a character array
            var charNum = input.Split(',');


            //bool isFirstNum = int.TryParse(charNum[0].Trim(), out int maxNum);
            //if (!isFirstNum)
            //{
            //    Console.WriteLine("Not a valid number. Try again");
            //    return;
            //}

            //or
            int maxNum = int.MinValue;


            //Loop over (choose index 1 or 0)
            for (int i = 0; i < charNum.Length; i++)
            {
                 
                bool isNum = int.TryParse(charNum[i].Trim() , out int currNum);
              
                if (!isNum)
                {
                    Console.WriteLine("Not a valid number. Try again");
                    return; //imp , stop prog if not number
                }
                if (currNum > maxNum)
                {
                    maxNum = currNum;
                }

            }

            Console.WriteLine("Maximum Number is : {0}" , maxNum);

            Console.WriteLine();
        }
    }
}
