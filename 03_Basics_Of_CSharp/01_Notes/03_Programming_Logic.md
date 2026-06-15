# 3. Programming Logic

Programming logic is what makes applications intelligent.

Using programming logic, a program can:

* Perform calculations
* Compare values
* Make decisions
* Repeat tasks automatically

<br>

## 3.1 Operators

Operators are symbols used to perform operations on values and variables.

<br>

### Arithmetic Operators

Used for mathematical calculations.

| Operator | Meaning             | Example |
| -------- | ------------------- | ------- |
| +        | Addition            | 10 + 5  |
| -        | Subtraction         | 10 - 5  |
| *        | Multiplication      | 10 * 5  |
| /        | Division            | 10 / 5  |
| %        | Modulus (Remainder) | 10 % 3  |

Example:

```csharp
int a = 10;
int b = 3;

Console.WriteLine(a + b); // 13
Console.WriteLine(a - b); // 7
Console.WriteLine(a * b); // 30
Console.WriteLine(a / b); // 3
Console.WriteLine(a % b); // 1
```

<br>

### Comparison Operators

Used to compare values.

Result is always `true` or `false`.

| Operator | Meaning                  |
| -------- | ------------------------ |
| ==       | Equal To                 |
| !=       | Not Equal To             |
| >        | Greater Than             |
| <        | Less Than                |
| >=       | Greater Than or Equal To |
| <=       | Less Than or Equal To    |

Example:

```csharp
int age = 21;

Console.WriteLine(age >= 18);
```

Output:

```text
True
```

<br>

### Logical Operators

Used to combine conditions.

| Operator | Meaning |
| -------- | ------- |
| &&       | AND     |
| ||       | OR      |
| !        | NOT     |

Example:

```csharp
int age = 21;
bool hasLicense = true;

Console.WriteLine(age >= 18 && hasLicense);
```

Output:

```text
True
```

<br>

### Assignment Operators

Used to assign values.

```csharp
int number = 10;
```

Shortcuts:

```csharp
number += 5;
number -= 2;
number *= 3;
number /= 2;
```

Equivalent:

```csharp
number = number + 5;
```

<br>

### Increment and Decrement

```csharp
int count = 5;

count++;
count--;
```

<div align = "center">
<img width="600"  alt="image" src="https://github.com/user-attachments/assets/c00b7e3e-5196-45e7-ac2d-a28821621e81" />
<img width="600" alt="image" src="https://github.com/user-attachments/assets/bd2a8ada-9cce-470d-8ddc-e24223599493" />
</div>


Very common in loops.

<br>

## 3.2 Comments

Comments are notes for developers.

The compiler ignores them.

<br>

### Single-Line Comment

```csharp
// Calculate total price
int total = 100;
```

<br>

### Multi-Line Comment

```csharp
/*
    This block calculates
    employee salary
*/
```

<br>

### Best Practice

Comment **why** something is done.

Avoid commenting obvious code.

Bad:

```csharp
// Increment count
count++;
```

Good:

```csharp
// Increase retry attempt after failed login
retryCount++;
```

<br>

## 3.3 Conditional Statements

Conditional statements allow programs to make decisions.

### Real-World Example

```text
If age >= 18
    Allow voting
Else
    Reject
```

<br>

### if Statement

```csharp
int age = 20;

if (age >= 18)
{
    Console.WriteLine("Adult");
}
```

<br>

### if-else Statement

```csharp
int age = 16;

if (age >= 18)
{
    Console.WriteLine("Adult");
}
else
{
    Console.WriteLine("Minor");
}
```

Output:

```text
Minor
```

<br>

### else if Statement

Used when multiple conditions exist.

```csharp
int marks = 75;

if (marks >= 90)
{
    Console.WriteLine("A");
}
else if (marks >= 75)
{
    Console.WriteLine("B");
}
else
{
    Console.WriteLine("C");
}
```

Output:

```text
B
```

**Example:**
```csharp
using System;

namespace LearnNumbers
{ 
internal class Program
    {
        static void Main(string[] args) 
        {

            // Normal conditional blocks
            bool isGoldMember = true;


            double  price = 0;
            if (isGoldMember)
            {
                price = 100;
            }
            else
            {
                price = 200;
            }
            Console.WriteLine(price);


            //Ternary consitional statements
            double tprice = (isGoldMember) ? 100 : 200; //you can skip ()
            Console.WriteLine(tprice);
        }
    }
}
```
Output:
```
100
100
```

<br>

### Nested if

An if statement inside another if.

```csharp
int age = 21;
bool hasLicense = true;

if (age >= 18)
{
    if (hasLicense)
    {
        Console.WriteLine("Can Drive");
    }
}
```

Use sparingly. Too much nesting reduces readability.

<br>

### switch Statement

Useful when checking multiple fixed values.

```csharp
int day = 3;

switch (day)
{
    case 1:
        Console.WriteLine("Monday");
        break;

    case 2:
        Console.WriteLine("Tuesday");
        break;

    case 3:
        Console.WriteLine("Wednesday");
        break;

    default:
        Console.WriteLine("Invalid Day");
        break;
}
```

Output:

```text
Wednesday
```

<br>

### Ternary Operator

Short form of if-else.

```csharp
int age = 20;

string result =
    age >= 18 ? "Adult" : "Minor";

Console.WriteLine(result);
```

Output:

```text
Adult
```

Widely used in modern codebases.

<br>

## 3.4 Loops

Loops execute code repeatedly.

Instead of writing:

```csharp
Console.WriteLine("Hello");
Console.WriteLine("Hello");
Console.WriteLine("Hello");
```

Use a loop.

<br>

## 3.4.1 For Loop

Best when the number of iterations is known.

Syntax:

```csharp
for(initialization; condition; update)
{
    // code
}
```

Example:

```csharp
for(int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}
```

Output:

```text
1
2
3
4
5
```

<br>

### Loop Breakdown

```csharp
for(int i = 1; i <= 5; i++)
```

| Part      | Purpose     |
| --------- | ----------- |
| int i = 1 | Start value |
| i <= 5    | Condition   |
| i++       | Increment   |

---

### Reverse Loop

```csharp
for(int i = 5; i >= 1; i--)
{
    Console.WriteLine(i);
}
```

Output:

```text
5
4
3
2
1
```

<br>

## 3.4.2 While Loop

Best when the number of iterations is unknown.

Syntax:

```csharp
while(condition)
{
    // code
}
```

Example:

```csharp
int count = 1;

while(count <= 5)
{
    Console.WriteLine(count);
    count++;
}
```

Output:

```text
1
2
3
4
5
```

<br>

### Real-World Example

Keep asking until valid input is entered.

```csharp
while(userInput != "admin")
{
    Console.WriteLine("Try Again");
}
```

This is a common use case in real applications.

<br>

### Infinite Loop

Be careful.

```csharp
while(true)
{
    Console.WriteLine("Running...");
}
```

This loop never stops unless explicitly terminated.

<br>

## For vs While

| For Loop          | While Loop            |
| ----------------- | --------------------- |
| Known iterations  | Unknown iterations    |
| Counter-based     | Condition-based       |
| Common for arrays | Common for user input |

Examples:

```csharp
// For Loop
for(int i = 0; i < 10; i++)
{
}
```

```csharp
// While Loop
while(isRunning)
{
}
```

<br>

## Common Mistakes

### Using = Instead of ==

Wrong:

```csharp
if(age = 18)
{
}
```

Correct:

```csharp
if(age == 18)
{
}
```

<br>

### Forgetting Increment

Wrong:

```csharp
int i = 1;

while(i <= 5)
{
    Console.WriteLine(i);
}
```

Infinite loop.

Correct:

```csharp
i++;
```

<br>

### Missing Break in Switch

```csharp
case 1:
    Console.WriteLine("Monday");
    break;
```

Always remember `break` unless intentionally omitted.


* Always avoid accidental infinite loops.
* Programming logic is the foundation of all real-world applications.



<br>

---
---

<br>
