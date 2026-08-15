using System;
using System.Collections.Generic;

namespace CSharpFundamentalsAssign
{
    public static class Assignment3
    {
        // Displays the three smallest numbers.
        public static void PrintList(List<int> arr)
        {
            Console.WriteLine("The 3 smallest Numbers are : ");
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
        }

        // Returns the smallest number from the given list.
        public static int GetSmallest(List<int> nums)
        {
            int minVal = nums[0];
            for (int i = 1; i < nums.Count; i++)
            {
                if (nums[i] < minVal)
                {
                    minVal = nums[i];
                }
            }
            return minVal;
        }
        

        public static void GetThreeSmallestNums()
        {
            Console.WriteLine("Assignment 3 - Find Smallest Three Integers");
   
            while (true)
            {
                // Take input
                Console.Write("Enter numbers separated by commas: ");
                var input = Console.ReadLine();

                // base cases
                //1. Empty or white space
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid Empty/Null List .. Please Input Again.!");
                    continue;
                }
                 
                var strNums = input.Split(','); //split to str[]

                //check if inputs count <5
                if(strNums.Length < 5)
                {
                    Console.WriteLine("Invalid List .. Please Input Again.!");
                    continue;
                }

                // Get3smallest()
                // Store the parsed integers.
                var nums = new List<int>();
                bool isValid = true;    // Tracks if all inp values are valid inst.

                // Convert each string to an integer.
                foreach (var item in strNums)
                {
                    bool isNum = int.TryParse(item.Trim(), out int num);

                    if (!isNum)
                    {
                        isValid = false;
                        Console.WriteLine("Invalid List");
                        break;
                    }
                    nums.Add(num);
                }
                if(isValid == false)
                {
                    continue;
                }

                // Get the smallest 3
                //copy of original list 
                var bufferList = new List<int>(nums);
                var smallestThree = new List<int>();

                while (smallestThree.Count < 3)
                {
                    int smallest = GetSmallest(bufferList);
                    smallestThree.Add(smallest);
                    bufferList.Remove(smallest);  // Remove the found smallest number to get the next smallest.
                }

                PrintList(smallestThree);

                break;
            }
        }
    }
}
