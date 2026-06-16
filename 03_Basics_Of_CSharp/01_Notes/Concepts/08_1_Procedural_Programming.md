# C# Basics

# 8.1 Procedural Programming

## What is Procedural Programming?

Procedural Programming is a programming style where a program is divided into small, reusable methods (functions).

Instead of writing everything inside `Main()`, we organize code into methods.

### Without Methods

```csharp
static void Main()
{
    int a = 10;
    int b = 20;

    Console.WriteLine(a + b);

    int x = 30;
    int y = 40;

    Console.WriteLine(x + y);
}
```

The code becomes repetitive and difficult to maintain.

<br>

### With Methods

```csharp
static void Main()
{
    Add(10, 20);
    Add(30, 40);
}

static void Add(int a, int b)
{
    Console.WriteLine(a + b);
}
```

Now the logic is reusable.

<br>

## Why Use Methods?

Benefits:

* Avoid code duplication
* Improve readability
* Easier debugging
* Easier maintenance
* Reusable code

<br>

# Method Syntax

```csharp
accessModifier returnType MethodName(parameters)
{
    // Method Body
}
```

Example:

```csharp
public static void Greet()
{
    Console.WriteLine("Welcome");
}
```

<br>

## Method Components

```csharp
public static void Greet()
{
    Console.WriteLine("Hello");
}
```

| Part   | Meaning              |
| ------ | -------------------- |
| public | Access Modifier      |
| static | Belongs to the class |
| void   | Returns nothing      |
| Greet  | Method Name          |
| ()     | Parameters           |
| {}     | Method Body          |

<br>

# Calling a Method

A method does nothing until it is called.

```csharp
static void Main()
{
    Greet();
}

static void Greet()
{
    Console.WriteLine("Hello");
}
```

Output:

```text
Hello
```

<br>

# Parameters

Parameters allow methods to receive data.

```csharp
static void Greet(string name)
{
    Console.WriteLine($"Hello {name}");
}
```

Method call:

```csharp
Greet("Yash");
Greet("John");
```

Output:

```text
Hello Yash
Hello John
```

<br>

# Multiple Parameters

```csharp
static void Add(int a, int b)
{
    Console.WriteLine(a + b);
}
```

Usage:

```csharp
Add(10, 20);
```

Output:

```text
30
```

<br>

# Arguments vs Parameters

```csharp
Add(10, 20);
```

```text
10, 20 -> Arguments
```

```csharp
void Add(int a, int b)
```

```text
a, b -> Parameters
```

<br>

# Return Values

Instead of printing inside a method, return the result.

```csharp
static int Add(int a, int b)
{
    return a + b;
}
```

Usage:

```csharp
int result = Add(10, 20);

Console.WriteLine(result);
```

Output:

```text
30
```

<br>

## void vs Return

### void

Returns nothing.

```csharp
static void Display()
{
    Console.WriteLine("Hello");
}
```

<br>

### Return Type

Returns a value.

```csharp
static int Square(int number)
{
    return number * number;
}
```

<br>

# Method Overloading

Method Overloading allows multiple methods with the same name but different parameters.

Example:

```csharp
static int Add(int a, int b)
{
    return a + b;
}

static double Add(double a, double b)
{
    return a + b;
}
```

Usage:

```csharp
Console.WriteLine(Add(10, 20));

Console.WriteLine(Add(10.5, 20.5));
```

Output:

```text
30
31
```

Benefits:

* Improves readability
* Reuses method names
* Common in .NET libraries

<br>

# Optional Parameters

Parameters can have default values.

```csharp
static void Greet(string name = "Guest")
{
    Console.WriteLine($"Hello {name}");
}
```

Usage:

```csharp
Greet();
Greet("Yash");
```

Output:

```text
Hello Guest
Hello Yash
```

<br>

# Named Arguments

Arguments can be passed by name.

```csharp
static void Register(string name, int age)
{
    Console.WriteLine($"{name} - {age}");
}
```

Usage:

```csharp
Register(age: 21, name: "Yash");
```

Output:

```text
Yash - 21
```

<br>

# ref Parameter

Normally, arguments are passed by value.

```csharp
static void Increment(int number)
{
    number++;
}
```

```csharp
int num = 10;

Increment(num);

Console.WriteLine(num);
```

Output:

```text
10
```

The original value is not modified.

<br>

Using `ref`:

```csharp
static void Increment(ref int number)
{
    number++;
}
```

Usage:

```csharp
int num = 10;

Increment(ref num);

Console.WriteLine(num);
```

Output:

```text
11
```

Use `ref` when the method should modify the original variable.

<br>

# out Parameter

`out` allows a method to return multiple values.

Example:

```csharp
static void GetValues(out int a, out int b)
{
    a = 10;
    b = 20;
}
```

Usage:

```csharp
GetValues(out int x, out int y);

Console.WriteLine(x);
Console.WriteLine(y);
```

Output:

```text
10
20
```

Common use:

```csharp
int.TryParse()
DateTime.TryParse()
```

<br>

# Method Call Stack

Whenever a method is called:

1. Control moves to that method.
2. The method executes.
3. It returns to the caller.

Example:

```csharp
static void Main()
{
    Display();
}

static void Display()
{
    Console.WriteLine("Hello");
}
```

Execution Flow:

```text
Main()
   |
   v
Display()
   |
   v
Return to Main()
```

<br>

# Best Practices

* Keep methods small and focused.
* Give methods meaningful names.
* Avoid duplicate code.
* Return values instead of printing whenever possible.
* One method should perform one task.

Good:

```csharp
CalculateTotal()
SendEmail()
SaveCustomer()
```

Avoid:

```csharp
DoEverything()
```

<br>

# Real-World Example

```csharp
static double CalculateGST(double amount)
{
    return amount * 0.18;
}

static void Main()
{
    double gst = CalculateGST(1000);

    Console.WriteLine(gst);
}
```

This approach makes the code reusable throughout the application.

<br>

# Commonly Used Method Features

```text
Methods

Parameters

Return Values

Method Overloading

Optional Parameters

Named Arguments

ref

out
```

<br>

# Key Takeaways

* Procedural Programming organizes code into reusable methods.
* Methods improve readability and reduce code duplication.
* Parameters receive input, while arguments provide values.
* Use `return` when a method should produce a result.
* Method Overloading allows multiple methods with the same name but different parameters.
* `ref` modifies the original variable.
* `out` is used to return multiple values and is commonly seen with `TryParse()` methods.
* Small, focused methods are easier to maintain and test.
