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
