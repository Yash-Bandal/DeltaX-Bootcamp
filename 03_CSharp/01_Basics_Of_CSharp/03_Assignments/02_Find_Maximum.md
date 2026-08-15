### Problem Statement
Write a program and ask the user to enter a series of numbers separated by comma. Find the maximum of the numbers and display it on the console. For example, if the user enters “5, 3, 8, 1, 4", the program should display 8.

### Code
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    internal class Program
    {
        static void Main(string[] args)
        {
            //Assignment1.CalculateTotal();
            Assignment2.FindMax();
        }
    }
}

```
