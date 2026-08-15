using System;
using System.Collections.Generic;

namespace CSharpFundamentalsAssign
{
    public static class Assignment4
    {
        // Displays the descending list
        public static void PrintList(List<int> arr)
        {
            Console.WriteLine("The Descending order of Numbers is : ");
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
        }

        // Returns the largest number from the given list.
        public static int Getlargest(List<int> nums)
        {
            int maxVal = nums[0];
            for (int i = 1; i < nums.Count; i++)
            {
                if (nums[i] > maxVal)
                {
                    maxVal = nums[i];
                }
            }
            return maxVal;
        }

        public static void PrintDescending()
        {
            Console.WriteLine("Assignment 4 - Find Descending Order of Integers");
   
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


                // Getlargest()
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

                //===================== Actual Logic ==========================

                // TC - O(n²)
                // Get the largest 3
                //copy of original list 
                //var bufferList = new List<int>(nums);
                //var sortedReverse = new List<int>();

                //while (bufferList.Count != 0)
                //{
                //    int largest = Getlargest(bufferList);
                //    sortedReverse.Add(largest);
                //    bufferList.Remove(largest);  // Remove the found largest number to get the next largest.
                //}
                //PrintList(sortedReverse);

                //=====================================================================



                // Base logic - O(n log n)

                //var bufferList = new List<int>(nums);
                //bufferList.Sort();
                //bufferList.Reverse();
                //PrintList(bufferList);

                //=================================================================

                // Bubble Sort
                var bufferList = new List<int>(nums);
                for (int i = 0; i < bufferList.Count - 1; i++)
                {
                    for (int j = 0; j < bufferList.Count - i - 1; j++)
                    {
                        if (bufferList[j] < bufferList[j + 1])
                        {
                            int temp = bufferList[j];
                            bufferList[j] = bufferList[j + 1];
                            bufferList[j + 1] = temp;
                        }
                    }
                }

                PrintList(bufferList);

                //=====================================================================


                break;
            }
        }
    }
}
