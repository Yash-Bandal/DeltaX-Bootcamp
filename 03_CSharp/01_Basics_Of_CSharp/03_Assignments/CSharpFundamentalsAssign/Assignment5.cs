using System;


// calculate difference
/*
 Note : If the result month and date are negative, borrow previews month to get extra
 eg:

 Today - 2026-01-01
 DOB   - 2004-09-25
-------------------
diff   - 22 (-8)(-24)

Now we dont have enough values in today to find difference
so borrow from previous month of today, eg
date - Jan 01 -> previous month = Dec 31 days

now as we have borrowed previos month, remove 1 month from current month

if month is january, also remove 1 year


so 01 -> 31 + 01 = 32   -> 32 - 25 -> 7
months = 12 + 01 = 13   -> 13 - 01 -> 12

 */

namespace CSharpFundamentalsAssign
{
    public static class Assignment5
    {
        public static void GetAge()
        {
            // Input age
            Console.WriteLine("Enter your Date of Birth in format \'yyyy-MM-dd\' : ");
            var dob = Console.ReadLine();

            DateTime today = DateTime.Today; //datetime today object

            // Empty Input
            if (string.IsNullOrWhiteSpace(dob))
            {
                Console.WriteLine("Invalid or Empty Input ..Try ageain");
                return;
            }

            // Parse date in Exact format
            bool isValidDate = DateTime.TryParseExact(
                dob.Trim(),
                "yyyy-MM-dd",
                null,
                System.Globalization.DateTimeStyles.None,
                out DateTime parsedDob
            );

            // stop if invalid
            if (!isValidDate)
            {
                Console.WriteLine("Invalid Date..Please Try again!");
                return;
            }

            // Special case - DOB never in future
            if (parsedDob > today)
            {
                Console.WriteLine("DOB cannot be in future");
                return;
            }

            // Calculate difference and Borrow logic            
            int years = today.Year - parsedDob.Year;
            int days = today.Day - parsedDob.Day;
            int months = today.Month - parsedDob.Month;

            if(days < 0) //then borrow days from prev month, 
            {
                //subtract month
                months--;

                int prevMonth = today.Month - 1; //calculated
                int prevYear = today.Year; //yet to be calculated
                if (prevMonth == 0)
                {
                    prevMonth = 12;
                    prevYear--;
                }
                days += DateTime.DaysInMonth(prevYear, prevMonth); //eg -09 + 32 = 21 
            }

            if(months < 0) //borrow 1 full year (12months) and add to months
            {
                years--;
                months += 12;
            }

            Console.WriteLine($"You are {years} years, {months} months, and {days} days old. ");
        }   
    }
}
