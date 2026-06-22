# 1.4 Methods

Methods define the behavior of a class. They contain the logic that performs a specific task and are executed only when they are called.

Methods help organize code, promote reusability, and reduce duplication.


<br>

> [!Note]
> 1. We dont use `ref` and `out` paramter modifiers much, they are considered a bit of bad practise, `out` is used little
> 2. `params` us used widely

<br>

# What is a Method?

A **method** is a block of code inside a class that performs a specific operation.

Example:

```csharp
class Person
{
    public void Introduce()
    {
        Console.WriteLine("Hello!");
    }
}
```

Calling the method:

```csharp
Person person = new Person();

person.Introduce();
```

<br>

# Method Syntax

```csharp
accessModifier returnType MethodName(parameters)
{
    // Method body
}
```

Example

```csharp
public int Add(int a, int b)
{
    return a + b;
}
```

<br>

# Methods with Parameters

Parameters allow values to be passed into a method.

```csharp
public void Greet(string name)
{
    Console.WriteLine($"Hello, {name}");
}
```

Calling the method:

```csharp
Greet("Yash");
```

`"Yash"` is the **argument**, while `string name` is the **parameter**.

> [!Tip]
> 1. Parameters are variables
> 2. Arguments are values

<br>

# Methods with Return Values

A method can return a value using the `return` keyword.

```csharp
public int Square(int number)
{
    return number * number;
}
```

Usage

```csharp
int result = Square(5);

Console.WriteLine(result);
```

Output

```
25
```

<br>

# Method Signature

A **method signature** uniquely identifies a method.

A method signature consists of:

* Method name
* Number of parameters
* Type of parameters
* Order of parameters

The **return type is NOT part of the method signature.**

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/952f6b69-077f-498b-865b-1b248c90a89f" />
</div>




Example

```csharp
public void Print(string name)
{
}

public void Print(string name, int age)
{
}
```

These methods have different signatures.

The following is **not allowed** because only the return type is different.

```csharp
public int Calculate()
{
}

public double Calculate()
{
}
```

<br>

# Method Overloading

**Method Overloading** means defining multiple methods with the same name but different signatures.

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/b5b47dc1-034e-49aa-9c08-07b30752955a" />
</div>






Example

```csharp
class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}
```

Usage

```csharp
calculator.Add(2, 3);

calculator.Add(2, 3, 4);

calculator.Add(2.5, 3.8);
```

But what if we have a log of numbers, eg 10 Numbers\
In such cases, use Array Parameter

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/49066f45-54ef-4c22-b51b-3b3da46e0654" />
</div>




But, always declaring a new `Array` in the `Argument` doesnt look fine, so we use [`params` modifier](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_Intermediate_CSharp/01_Notes/01_Classes/04_Methods.md#3-params) 



Benefits

* Improves readability.
* Reuses the same method name for similar operations.
* Avoids unnecessary method names.

<br>

**Example 1:**
```csharp
namespace CSharpdoubleermediate
{
    public class Volume
    {
        // Methods

        //Cube volume
        public double FindVolume(double side)
        {
            return side * side * side;
        }

        //Cuboid Volume
        public double FindVolume(double length, double breadth, double height)
        {
            return length * breadth * height;
        }

        // Cylinder Volume
        public double FindVolume(double radius, double height)
        {
            return Math.PI  * radius * radius * height;
        }

    }
    internal partial class Program
    {
        public static void Main(string[] args)
        {
            Volume cube = new Volume();
            Console.WriteLine("Volume of Cube is " + cube.FindVolume(5) + " m³ ");

            Volume cuboid = new Volume();
            Console.WriteLine("Volume of Cuboid is " + cuboid.FindVolume(5,6,7) + " m³ ");

            Volume cylinder = new Volume();
            Console.WriteLine("Volume of Cylinder is " + cylinder.FindVolume(5,6) + " m³ ");

        }
    }
}
```
**Example 2:**
```csharp
namespace CSharpdoubleermediate
{
    public class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        //paramter numbers
        public void Move(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        // Overload with new signature (Point Object instead of numbers)
        public void Move(Point newLocation)
        {
            if (newLocation == null) //defensive prog
            {
                throw new ArgumentNullException("newLocation"); //exception is a class
            }
            Move(newLocation.X, newLocation.Y); //instead of re initializing with 'this'

            //this.X = newLocation.X;
            //this.Y = newLocation.Y;
        }
    }

    internal partial class Program
    {
        public static void Main(string[] args)
        {

            //global error handling
            try
            {
                var point = new Point(10, 20);
                point.Move(null);
                //point.Move(new Point(40,60));

                // Overload 1
                Console.WriteLine("Point is at ({0}, {1}) ", point.X, point.Y);

                // Overload 2
                point.Move(100, 200);
                Console.WriteLine("Point is now at ({0}, {1}) ", point.X, point.Y);
            }
            catch (Exception)
            {
                Console.WriteLine("An Unexpected Error happened");
            }
        }
    }
}

```


> [!tip]
> Prefer global error handling techniques like `try..catch` blocks and Right exception throw methods, to avoud failure of code

<br>

# Parameter Modifiers

C# provides parameter modifiers that change how arguments are passed to methods.

## 1. ref

> [!Note]
> It is not much used, and also suggested to use it less 

The `ref` keyword passes a variable **by reference**, allowing the method to modify the original value.

Both the caller and the method must use `ref`.

<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/781d96f1-033f-490e-b66b-aa849a3efb77" />
<p>Think, if we pass variable 'a', will 'a' become 3, No, because it becomes value type modification </p>
<p>Thus we use 'ref' keyword </p>
    <img width="400" alt="image" src="https://github.com/user-attachments/assets/a6c3531e-6071-4dbc-bd68-03e2a5b6b447" />
    <p>Using ref, original 'a' argument is passed, instead of a new copy creation</p>
</div>





```csharp
public void Increment(ref int number)
{
    number++;
}

int value = 10;

Increment(ref value);

Console.WriteLine(value);
```

Output

```
11
```

The variable **must be initialized** before passing it with `ref`.

<br>

## 2. out

The `out` keyword is used when a method needs to return multiple values.

> It somewhat allows to return multiple values, which isn't a good practise, instead we prefer creating different classes

<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/bd537d02-30d5-4039-9ec7-8080ffd562e0" />
</div>

```csharp
public void GetUser(out string name)
{
    name = "Yash";
}

string userName;

GetUser(out userName);

Console.WriteLine(userName);
```

Output

```
Yash
```

Unlike `ref`, the variable **does not need to be initialized** before being passed.

A common real-world example is `int.TryParse()`.

```csharp
int.TryParse("123", out int number);
```

**Example:**
```csharp
namespace CSharpdoubleermediate
{
    internal partial class Program
    {
        public static void Main(string[] args)
        {

            //using parse, need global exception handling
            try
            {
                var num = int.Parse("Abc");
            }
            catch (Exception)
            {
                Console.WriteLine("Conversiion failed");
            }

            // using tryparse, that dont throw exception
            int number;
            bool isNum = int.TryParse("Abc", out number);
            if (isNum)
            {
                Console.WriteLine("Its a number");
            }
            else
            {
                Console.WriteLine("Conversiion failed");
            }
        }

    }
}

```

<br>

## 3. params

The `params` keyword allows a method to accept a variable number of arguments.

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/7bb1da10-0dcf-420b-a425-6adf5c494a60" />
</div>




```csharp
public int Sum(params int[] numbers)
{
    int total = 0;

    foreach (int number in numbers)
        total += number;

    return total;
}
```

Usage

```csharp
Sum(1, 2);

Sum(1, 2, 3, 4);

Sum(10, 20, 30, 40, 50);
```

`params` makes methods more flexible without requiring multiple overloads.


<br>

**Full example:**
```csharp
namespace CSharpdoubleermediate
{

    public class Calculator
    {
        public int Add(params int[] arr)
        {
            int res = 0;
            foreach (var num in arr)
            {
                res += num;
            }
            return res;
        }
    }

    internal partial class Program
    {
        public static void Main(string[] args)
        {
            //UsePoints();
            //UseParams();
        }

        public static void UseParams()
        {
            Calculator calculator = new Calculator();
            Console.WriteLine(calculator.Add(1));
            Console.WriteLine(calculator.Add(1, 2));
            Console.WriteLine(calculator.Add(1, 2, 3));
            Console.WriteLine(calculator.Add(1, 2, 3, 4));
            Console.WriteLine(calculator.Add(new int[] { 1, 2, 3, 4, 5 }));
        }


    }
}

```
**Output:**
```
1
3
6
10
15
```

<br>

# Why `ref` is a Code Smell

Although `ref` is useful in some scenarios, it is generally considered a **design smell**.

Reasons:

* The method changes variables outside its own scope.
* Makes code harder to understand.
* Creates hidden side effects.
* Increases coupling between methods.
* Makes debugging more difficult.

Bad

```csharp
UpdateSalary(ref salary);
```

The caller cannot easily tell that `salary` will change.

Better

```csharp
salary = CalculateUpdatedSalary(salary);
```

This makes the code predictable and easier to maintain.

Use `ref` only when absolutely necessary.

<br>

# Best Practices

* Keep methods focused on a single responsibility.
* Use meaningful method names.
* Prefer returning values instead of modifying parameters.
* Use method overloading for similar operations.
* Avoid using `ref` unless there is a clear performance or design requirement.
* Use `params` when accepting a variable number of arguments.
* Use `out` mainly for methods like `TryParse()` or when returning multiple values.

<br>

# Common Mistakes

## Confusing Parameters and Arguments

```csharp
public void Print(string name) // Parameter
{
}

Print("Yash"); // Argument
```

<br>

## Thinking Return Type is Part of the Method Signature

Incorrect

```csharp
public int Calculate()
{
}

public double Calculate()
{
}
```

This causes a compile-time error.

<br>

## Overusing `ref`

Using `ref` to modify values unnecessarily leads to tightly coupled and less maintainable code.

<br>

# Interview Questions

### What is a method?

A method is a block of code inside a class that performs a specific task.

<br>

### What is a method signature?

A method signature consists of the method name and its parameter list (number, types, and order of parameters). The return type is not part of the signature.

<br>

### What is method overloading?

Method overloading is defining multiple methods with the same name but different parameter lists.

<br>

### What is the difference between `ref` and `out`?

| ref                                         | out                                      |
| <br><br><br><br><br><br><br><br><br><br><br><br><br><br>- | <br><br><br><br><br><br><br><br><br><br><br><br><br>- |
| Variable must be initialized before passing | Variable does not need to be initialized |
| Used to modify an existing value            | Used to return values from a method      |

<br>

### What is the purpose of `params`?

`params` allows a method to accept a variable number of arguments.

<br>

### Why should `ref` generally be avoided?

Because it introduces side effects, increases coupling, and makes code harder to understand and maintain.

<br>

# Summary

* Methods define the behavior of a class.
* Methods can accept parameters and return values.
* A method signature includes the method name and parameter list.
* Method overloading allows multiple methods with the same name but different signatures.
* `ref`, `out`, and `params` modify how arguments are passed to methods.
* Prefer returning values over using `ref` whenever possible.
* Keep methods small, readable, and focused on a single responsibility.


<br>

---
---

<br>


