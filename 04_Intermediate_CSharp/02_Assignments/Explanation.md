

# C# Intermediate Assignment 2 - Mentor Interview Questions & Answers

> **Goal:** Understand the concepts behind your code, not just memorize answers.


## Code
```csharp
using System;
using System.Collections.Generic;

namespace CSharpIntermediateAssign
{
    // Interface 
    public interface ICalculator
    {
        int Add(int num1, int num2);
        int Add(int num1, int num2, int num3);
        float Add(float fnum1, float fnum2);
        double GetResult();
    }

    public interface IAdvancedCalculator : ICalculator
    {
        double Power(int baseNum, int exponent);     
    }

    // Calculator class - Add Implementation
    public class Calculator : ICalculator
    {
        private double _result;

        protected double Result
        {
            get { return _result; }
            set { _result = value; }
        }
       
        public virtual double GetResult()
        { 
            return _result;
        }
        public int Add(int num1, int num2)
        {
            _result = num1 + num2;
            return num1 + num2;
        }
        public int Add(int num1, int num2, int num3)
        {
            _result = num1 + num2 + num3;
            return num1 + num2 + num3;
        }
        public float Add(float fnum1, float fnum2)
        {
            _result = fnum1 + fnum2;
            return fnum1 + fnum2;
        }
    }

    // Advanced Calculator class - Power Implementation
    public class AdvancedCalculator :  Calculator, IAdvancedCalculator
    {
        public double Power(int baseNum, int exponent)
        {
            Result = Math.Pow(baseNum, exponent);
            return Result;
        }

        public override double GetResult() 
        {
            //return _result* Power(10,6);//we manipulate'_result'
            return Result * 1_000_000;
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Calculator Demo ===");

            ICalculator calculator = new Calculator(); //Icalculator ref type

            Console.WriteLine(calculator.Add(10, 20));
            Console.WriteLine(calculator.Add(10, 20, 30));
            Console.WriteLine(calculator.Add(10.5f, 20.25f));
            Console.WriteLine(calculator.GetResult());

            Console.WriteLine();

            Console.WriteLine("\n=== Advanced Calculator Demo ===");

            IAdvancedCalculator advancedCalculator = new AdvancedCalculator();

            Console.WriteLine(advancedCalculator.Power(3, 2));
            Console.WriteLine(advancedCalculator.GetResult());
            //Console.WriteLine(advancedCalculator.Add(20,3.4f));


            //IAdvancedCalculator calculator = new AdvancedCalculator();

            //while (true)
            //{
            //    Console.WriteLine("\n===== Calculator Menu =====");
            //    Console.WriteLine("1. Add 2 Integers");
            //    Console.WriteLine("2. Add 3 Integers");
            //    Console.WriteLine("3. Add 2 Floating Point Numbers");
            //    Console.WriteLine("4. Power");
            //    Console.WriteLine("5. Get Result");
            //    Console.WriteLine("0. Exit");

            //    Console.Write("\nEnter your choice: ");
            //    string choice = Console.ReadLine();

            //    switch (choice)
            //    {
            //        case "1":
            //            {
            //                Console.Write("Enter first integer: ");
            //                if (!int.TryParse(Console.ReadLine(), out int num1))
            //                {
            //                    Console.WriteLine("Invalid first integer.");
            //                    break;
            //                }

            //                Console.Write("Enter second integer: ");
            //                if (!int.TryParse(Console.ReadLine(), out int num2))
            //                {
            //                    Console.WriteLine("Invalid second integer.");
            //                    break;
            //                }

            //                Console.WriteLine($"Result = {calculator.Add(num1, num2)}");
            //                break;
            //            }

            //        case "2":
            //            {
            //                Console.Write("Enter first integer: ");
            //                if (!int.TryParse(Console.ReadLine(), out int num1))
            //                {
            //                    Console.WriteLine("Invalid first integer.");
            //                    break;
            //                }

            //                Console.Write("Enter second integer: ");
            //                if (!int.TryParse(Console.ReadLine(), out int num2))
            //                {
            //                    Console.WriteLine("Invalid second integer.");
            //                    break;
            //                }

            //                Console.Write("Enter third integer: ");
            //                if (!int.TryParse(Console.ReadLine(), out int num3))
            //                {
            //                    Console.WriteLine("Invalid third integer.");
            //                    break;
            //                }

            //                Console.WriteLine($"Result = {calculator.Add(num1, num2, num3)}");
            //                break;
            //            }

            //        case "3":
            //            {
            //                Console.Write("Enter first number: ");
            //                if (!float.TryParse(Console.ReadLine(), out float num1))
            //                {
            //                    Console.WriteLine("Invalid first number.");
            //                    break;
            //                }

            //                Console.Write("Enter second number: ");
            //                if (!float.TryParse(Console.ReadLine(), out float num2))
            //                {
            //                    Console.WriteLine("Invalid second number.");
            //                    break;
            //                }

            //                Console.WriteLine($"Result = {calculator.Add(num1, num2)}");
            //                break;
            //            }

            //        case "4":
            //            {
            //                Console.Write("Enter base: ");
            //                if (!int.TryParse(Console.ReadLine(), out int baseNum))
            //                {
            //                    Console.WriteLine("Invalid base.");
            //                    break;
            //                }

            //                Console.Write("Enter exponent: ");
            //                if (!int.TryParse(Console.ReadLine(), out int exponent))
            //                {
            //                    Console.WriteLine("Invalid exponent.");
            //                    break;
            //                }

            //                Console.WriteLine($"Result = {calculator.Power(baseNum, exponent)}");
            //                break;
            //            }

            //        case "5":
            //            {
            //                Console.WriteLine($"Latest Result (Micros) = {calculator.GetResult()}");
            //                break;
            //            }

            //        case "0":
            //            {
            //                Console.WriteLine("Exiting Calculator...");
            //                return;
            //            }

            //        default:
            //            {
            //                Console.WriteLine("Invalid choice. Please try again.");
            //                break;
            //            }
            //    }
            //}

        }
    }
}
```

<br>


# Q0. Why we should not have a promise/contact for Getresult() inside IAdvancedCalculator Interface?

Because we are extending original Calculator class, not creating a new seperate AdvancedCalculator, so we dont need a seperate contract, 
It is already inheriting GetResult() through inheritance

<br>


# Q1. Why did you use interfaces?

### Weak Answer

> Because the assignment told me to.

This is an immediate red flag.

### Good Answer

I used interfaces to define a contract for calculator operations. Any class implementing `ICalculator` must provide implementations of `Add()` and `GetResult()`. This allows different calculator implementations to be used interchangeably without changing the code that depends on them.

If asked further:

* Interfaces reduce coupling.
* Interfaces improve extensibility.
* Future classes like `ScientificCalculator` or `FinancialCalculator` can implement the same interface without affecting existing code.

<br>

# Q2. Why

```csharp
ICalculator calculator = new Calculator();
```

instead of

```csharp
Calculator calculator = new Calculator();
```

### Good Answer

The object created is still `Calculator`, but the **reference type** is `ICalculator`.

This means the rest of my code depends only on the interface rather than a specific implementation.

Suppose tomorrow we create:

```csharp
ScientificCalculator
```

I can simply replace

```csharp
ICalculator calculator = new Calculator();
```

with

```csharp
ICalculator calculator = new ScientificCalculator();
```

without changing the remaining code.

This is called **Programming to an Interface rather than an Implementation**.

<br>

# Q3. Why `virtual`?

Without `virtual`

```csharp
public double GetResult()
```

the child class cannot override the behavior.

With

```csharp
public virtual double GetResult()
```

I'm telling C#:

> "This method has a default implementation, but child classes are allowed to replace it."

Then

```csharp
public override double GetResult()
```

provides the new behavior.

Without `virtual`, using `override` causes a compile-time error.

<br>

# Q4. Difference between `override` and `new`

This is a very common interview question.

## Using `override`

```csharp
class Calculator
{
    public virtual void Print()
    {
        Console.WriteLine("Calculator");
    }
}

class AdvancedCalculator : Calculator
{
    public override void Print()
    {
        Console.WriteLine("Advanced");
    }
}
```

Now

```csharp
Calculator calc = new AdvancedCalculator();
calc.Print();
```

Output

```
Advanced
```

Because **override replaces the parent's implementation** and supports **runtime polymorphism**.

<br>

## Using `new`

```csharp
class Calculator
{
    public void Print()
    {
        Console.WriteLine("Calculator");
    }
}

class AdvancedCalculator : Calculator
{
    public new void Print()
    {
        Console.WriteLine("Advanced");
    }
}
```

Now

```csharp
Calculator calc = new AdvancedCalculator();
calc.Print();
```

Output

```
Calculator
```

Why?

Because `new` performs **method hiding**, not overriding.

Method selection is based on the **reference type** at compile time.

Only

```csharp
AdvancedCalculator calc = new AdvancedCalculator();
calc.Print();
```

prints

```
Advanced
```

### Easy way to remember

`override`

> Replaces the parent's behavior (Runtime Polymorphism)

`new`

> Hides the parent's method (Method Hiding / Compile-time Binding)

<br>

# Q5. Why inheritance?

Because

```
AdvancedCalculator
```

**is a Calculator.**

It already has

* `Add()`
* `GetResult()`

The only additional functionality is

* `Power()`

Instead of rewriting all the existing methods, inheritance allows the child class to reuse the parent's implementation.

Could this be done without inheritance?

Yes.

```csharp
class AdvancedCalculator
{
    Add()
    Add()
    Add()
    GetResult()
    Power()
}
```

But this duplicates code.

Inheritance promotes code reuse when there is an **"is-a"** relationship.

<br>

# Q7. How does the compiler know which `Add()` method to call?

This is **Method Overloading**.

The compiler matches the method based on its **signature**.

Examples

```csharp
calculator.Add(5, 6);
```

Matches

```csharp
Add(int, int)
```

<br>

```csharp
calculator.Add(5, 6, 7);
```

Matches

```csharp
Add(int, int, int)
```

<br>

```csharp
calculator.Add(5.5f, 2.5f);
```

Matches

```csharp
Add(float, float)
```

### Important

The compiler **does not consider the return type** while selecting overloaded methods.

It considers only:

* Method name
* Number of parameters
* Parameter types
* Parameter order

<br>

# Q8. Is this valid?

```csharp
ICalculator calc = new AdvancedCalculator();
```

### Yes.

Why?

Because

```
AdvancedCalculator
```

implements

```
IAdvancedCalculator
```

and

```
IAdvancedCalculator
```

inherits from

```
ICalculator
```

Therefore an `AdvancedCalculator` object satisfies the `ICalculator` contract.

This is an example of **Interface Polymorphism**.

<br>

# Q9. Can this compile?

```csharp
ICalculator calc = new AdvancedCalculator();

calc.Power(2,5);
```

### No.

Compile-time error.

Why?

Because the **reference type** is `ICalculator`.

`ICalculator` only declares

* `Add()`
* `GetResult()`

It does **not** declare

```csharp
Power()
```

Even though the object is actually an `AdvancedCalculator`, the compiler only allows access to members declared by the **reference type**.

To call `Power()`, use either

```csharp
IAdvancedCalculator calc = new AdvancedCalculator();

calc.Power(2,5);
```

or

```csharp
AdvancedCalculator calc = new AdvancedCalculator();

calc.Power(2,5);
```

### Important Rule

> **The reference type determines which members are accessible at compile time.**

<br>

# Q10. Can one class implement multiple interfaces?

### Yes.

Example

```csharp
interface ICalculator
{
    void Add();
}

interface IPrintable
{
    void Print();
}

class Calculator : ICalculator, IPrintable
{
    public void Add()
    {
    }

    public void Print()
    {
    }
}
```

A class can implement **multiple interfaces**.

However, a class **cannot inherit from multiple classes** in C#.

<br>

# Bonus Question - Interface Inheritance vs Interface Polymorphism

## Interface Inheritance

```csharp
public interface IAdvancedCalculator : ICalculator
{
    double Power(int baseNum, int exponent);
}
```

This is **Interface Inheritance** because one interface extends another interface.

<br>

## Interface Polymorphism

```csharp
ICalculator calc = new Calculator();
```

or

```csharp
ICalculator calc = new AdvancedCalculator();
```

This is **Interface Polymorphism** because an interface reference points to a concrete object.

<br>

# Mentor Tip (Very Important)

When your mentor asks **"Why?"**, avoid textbook definitions.

### Bad Answer

> Interfaces provide abstraction.

### Good Answer

Interfaces define a common contract. My code depends on the interface instead of a specific implementation, so I can replace `Calculator` with another implementation like `ScientificCalculator` without changing the code that uses it.

This demonstrates understanding rather than memorization.

<br>

# Concepts Covered in This Assignment

By completing this assignment, you have practiced:

* Classes
* Method Overloading
* Inheritance
* Interfaces
* Interface Inheritance
* Interface Polymorphism
* Access Modifiers
* Encapsulation
* Virtual Methods
* Method Overriding
* Method Hiding (`new`)
* Runtime Polymorphism
* Programming to an Interface

<br>

# Final Mentor Verdict

Your solution demonstrates a solid understanding of intermediate C# concepts.

The next step is **not** to memorize definitions, but to confidently explain:

* Why you chose a particular design.
* Why interfaces were introduced.
* Why inheritance was appropriate.
* Why `override` was needed.
* Why interface references improve flexibility.

If you can explain these concepts clearly during a code review, you are well prepared for mentor discussions on this assignment.
