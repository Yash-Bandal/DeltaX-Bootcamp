# 6. Time Handling

## 6.1 DateTime

### What is DateTime?

`DateTime` is a built-in struct used to represent a specific date and time.

Common use cases:

* User registration date
* Order date
* Login time
* Birth date
* Scheduling
* Logging


<br>

## Getting Current Date and Time

### Current Local Date & Time

```csharp
DateTime now = DateTime.Now;

Console.WriteLine(now);
```

Example Output:

```text
07/07/2025 10:30:45 AM
```

<br>

### Current Date Only

```csharp
Console.WriteLine(DateTime.Today);
```

Output:

```text
07/07/2025 12:00:00 AM
```

(Time is set to midnight.)

<br>

### Current UTC Time

```csharp
Console.WriteLine(DateTime.UtcNow);
```

Used in:

* APIs
* Cloud applications
* Databases
* Global applications

Prefer storing UTC time in databases.

<br>

## Creating a DateTime

```csharp
DateTime birthday =
    new DateTime(2003, 10, 15);
```

With time:

```csharp
DateTime meeting =
    new DateTime(2025, 7, 10, 14, 30, 0);
```

Parameters:

```text
Year
Month
Day
Hour
Minute
Second
```

<br>

## Accessing Date Parts

```csharp
DateTime now = DateTime.Now;
```

```csharp
Console.WriteLine(now.Year);
Console.WriteLine(now.Month);
Console.WriteLine(now.Day);

Console.WriteLine(now.Hour);
Console.WriteLine(now.Minute);
Console.WriteLine(now.Second);
```

Example Output:

```text
2025
7
7
14
30
45
```

<br>

## Day and Month Information

```csharp
Console.WriteLine(now.DayOfWeek);
Console.WriteLine(now.DayOfYear);
```

Example:

```text
Monday
188
```

<br>

## Formatting DateTime

### Short Date

```csharp
Console.WriteLine(now.ToShortDateString());
```

Output:

```text
07/07/2025
```

<br>

### Long Date

```csharp
Console.WriteLine(now.ToLongDateString());
```

Output:

```text
Monday, July 07, 2025
```

<br>

### Short Time

```csharp
Console.WriteLine(now.ToShortTimeString());
```

Output:

```text
02:30 PM
```

<br>

### Long Time

```csharp
Console.WriteLine(now.ToLongTimeString());
```

Output:

```text
02:30:45 PM
```

<br>

### Custom Formatting

```csharp
Console.WriteLine(
    now.ToString("dd/MM/yyyy")
);
```

Output:

```text
07/07/2025
```

Common format specifiers:

| Format | Output  |
| ------ | ------- |
| dd     | Day     |
| MM     | Month   |
| yyyy   | Year    |
| HH     | 24-Hour |
| hh     | 12-Hour |
| mm     | Minutes |
| ss     | Seconds |

Example:

```csharp
Console.WriteLine(
    now.ToString("dd-MM-yyyy HH:mm:ss")
);
```

<br>

**Example:**
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace LearnNumbers
{
    internal partial class Program
    {
        public static void printList(List<int> ls)
        {
            for (int i = 0; i < ls.Count; i++)
            {
                Console.Write(ls[i] + " ");
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var dateTime = new DateTime(2026,06,10);
            var now = DateTime.Now;
            var today = DateTime.Today;

            Console.WriteLine("Hour :" + now.Hour);
            Console.WriteLine("Minute :" + now.Minute);
            // Hour: 11
            // Minute: 39
            
            //==========================================

            // DateTime elements are Immutable, but you can use predefined methods
            var tomorrow = now.AddDays(1);
            Console.WriteLine("Tomorrow is " + tomorrow);

            var yesterday = now.AddDays(-1);
            Console.WriteLine("Yesterday was "+ yesterday);

            //Tomorrow is 17 - 06 - 2026 11:39:35
            //Yesterday was 15 - 06 - 2026 11:39:35

            //===========================================

            //Format methods

            // Dates
            Console.WriteLine(now.ToLongDateString()); //16 June 2026
            Console.WriteLine(now.ToShortDateString()); //16-06-2026

            // Times
            Console.WriteLine(now.ToLongTimeString()); //11:41:04
            Console.WriteLine(now.ToShortTimeString()); //11:41 

            // Custom
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm")); //2026-06-16 11:41
            Console.WriteLine(now.ToString("yy-M-dd H:m")); //26-6-16 11:41

            //================================================
            //Tip : see google docs for more info
        }
    }
}

```
<br>

## Date Calculations

### Add Days

```csharp
DateTime future =
    now.AddDays(10);
```

<br>

### Add Months

```csharp
DateTime nextMonth =
    now.AddMonths(1);
```

<br>

### Add Years

```csharp
DateTime nextYear =
    now.AddYears(1);
```

<br>

### Add Hours

```csharp
DateTime later =
    now.AddHours(2);
```

<br>

## Comparing Dates

```csharp
DateTime start =
    new DateTime(2025, 1, 1);

DateTime end =
    new DateTime(2025, 12, 31);

Console.WriteLine(start < end);
```

Output:

```text
True
```

Comparison operators:

```text
<
>
<=
>=
==
!=
```

<br>

## Parsing Dates

Convert string to DateTime.

```csharp
string text = "2025-07-07";

DateTime date =
    DateTime.Parse(text);
```

<br>

### Safe Parsing

```csharp
string text = "2025-07-07";

bool success =
    DateTime.TryParse(
        text,
        out DateTime date
    );
```

Recommended for user input.

<br>

# 6.2 TimeSpan

## What is TimeSpan?

`TimeSpan` represents a duration or difference between two dates/times.

Examples:

* 2 hours
* 30 minutes
* 10 days

Unlike `DateTime`, it does **not** represent a calendar date.

<br>

## Creating TimeSpan

```csharp
TimeSpan duration =
    new TimeSpan(2, 30, 0);
```

Parameters:

```text
Hours
Minutes
Seconds
```

<br>

## Difference Between Two Dates

```csharp
DateTime start =
    new DateTime(2025, 1, 1);

DateTime end =
    new DateTime(2025, 1, 10);

TimeSpan difference =
    end - start;
```

Output:

```text
9.00:00:00
```

<br>

## Accessing TimeSpan Values

```csharp
Console.WriteLine(difference.Days);
Console.WriteLine(difference.Hours);

Console.WriteLine(difference.TotalDays);
Console.WriteLine(difference.TotalHours);
```

Example Output:

```text
9
0
9
216
```

### Difference

| Property   | Meaning                    |
| ---------- | -------------------------- |
| Days       | Whole days part            |
| TotalDays  | Complete duration in days  |
| Hours      | Hour component             |
| TotalHours | Complete duration in hours |

---

## Adding Time

```csharp
DateTime meeting =
    DateTime.Now;

meeting =
    meeting.AddHours(2);
```

<br>

## Common TimeSpan Methods

### FromDays()

```csharp
TimeSpan span =
    TimeSpan.FromDays(5);
```

<br>

### FromHours()

```csharp
TimeSpan span =
    TimeSpan.FromHours(3);
```

<br>

### FromMinutes()

```csharp
TimeSpan span =
    TimeSpan.FromMinutes(30);
```

<br>

### FromSeconds()

```csharp
TimeSpan span =
    TimeSpan.FromSeconds(45);
```

<br>

## Real-World Example

Calculate employee working hours.

```csharp
DateTime login =
    new DateTime(2025,7,7,9,0,0);

DateTime logout =
    new DateTime(2025,7,7,18,30,0);

TimeSpan workTime =
    logout - login;

Console.WriteLine(workTime.TotalHours);
```

Output:

```text
9.5
```

<br>

## Common DateTime Methods Used in Companies

```csharp
DateTime.Now
DateTime.Today
DateTime.UtcNow

AddDays()
AddMonths()
AddYears()

ToString()

Parse()
TryParse()
```

<br>

## Common TimeSpan Methods Used in Companies

```csharp
FromDays()
FromHours()
FromMinutes()

Days
Hours

TotalDays
TotalHours
TotalMinutes
```


<br>

**Example:**
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace LearnNumbers
{
    internal partial class Program
    {
        public static void printList(List<int> ls)
        {
            for (int i = 0; i < ls.Count; i++)
            {
                Console.Write(ls[i] + " ");
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var timespan = new TimeSpan(1, 10, 30); //custom confusing
            Console.WriteLine(timespan); //01:10:30

            var timespan1 = new TimeSpan(1, 0, 0); //confusing
            Console.WriteLine(timespan1); //01:00:00

            var timespan2 = TimeSpan.FromHours(1); //confusing
            Console.WriteLine(timespan2); //01:00:00

            //==============================================

            var start = DateTime.Now;
            var end = DateTime.Now.AddMinutes(2);
            var difference = end - start;
            Console.WriteLine("Difference is " + difference);

            // ========================================
            // Properties
            Console.WriteLine("Minutes :" + timespan.Minutes); //Minutes :10
            Console.WriteLine("Total Minutes :" + timespan.TotalMinutes); //Total Minutes :70.5

            // Add time
            Console.WriteLine("Add Example : "+ timespan.Add(TimeSpan.FromMinutes(8)));
            // Add Example : 01:10:30 + 00:08:00 = 01:18:30
            Console.WriteLine("Subtract Example : "+ timespan.Subtract(TimeSpan.FromMinutes(8)));
            // Add Example : 01:10:30 - 00:08:00 = 01:02:30


            //Tostring
            Console.WriteLine("ToString : " + timespan.ToString()); //ToString : 01:10:30

            //Parse - auto apply tostring to Timespan object
            Console.WriteLine("Parse : " + TimeSpan.Parse("02:01")); //Parse : 02:01:00

        }
    }
}

```

<br>

## DateTime vs TimeSpan

| DateTime                 | TimeSpan                  |
| ------------------------ | ------------------------- |
| Represents a date & time | Represents a duration     |
| Example: 10 Jul 2025     | Example: 5 days           |
| Used for timestamps      | Used for time differences |

<br>

## Key Takeaways

* `DateTime` represents a specific date and time.
* `DateTime.Now` returns the current local date and time.
* `DateTime.UtcNow` is preferred for storing timestamps in databases.
* Use `ToString()` with format specifiers to customize date and time output.
* `Parse()` and `TryParse()` convert strings into `DateTime`.
* `TimeSpan` represents a duration, not a date.
* Subtracting two `DateTime` values returns a `TimeSpan`.
* `TotalDays` and `TotalHours` are commonly used for calculating durations in real-world applications.


<br>

---
---

<br>
