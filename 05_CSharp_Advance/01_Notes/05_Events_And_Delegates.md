

# 5. Extension Methods

## 5.1 What are Extension Methods?

Extension Methods allow you to **add new methods to an existing class without modifying its source code or creating a derived class.**

Think of them as "adding extra features" to an existing class.

Example:

```csharp
string name = "yash";

Console.WriteLine(name.ToUpper());
```

`ToUpper()` is actually an extension method.

<br>

## 5.2 Why Do We Need Extension Methods?

Suppose you have a class:

```csharp
class Employee
{
    public string Name { get; set; }
}
```

Later, you want a method:

```text
GetFullInfo()
```

But:

* You cannot modify the class.
* The class may come from a third-party library.
* You don't want to create a child class.

Extension methods solve this problem.

<br>

## 5.3 Syntax

An extension method must:

* Be inside a **static class**
* Be a **static method**
* Use the **this** keyword before the first parameter

General Syntax:

```csharp
public static class ClassName
{
    public static ReturnType MethodName(
        this ExistingClass obj)
    {
    }
}
```

<br>

## 5.4 Creating an Extension Method

```csharp
public static class StringExtensions
{
    public static string ReverseText(this string text)
    {
        char[] chars = text.ToCharArray();

        Array.Reverse(chars);

        return new string(chars);
    }
}
```

Usage:

```csharp
string name = "Yash";

Console.WriteLine(name.ReverseText());
```

Output:

```text
hsaY
```

Notice:

We never modified the `String` class.

<br>

## 5.5 Understanding the "this" Keyword

```csharp
public static string ReverseText(
    this string text)
```

The keyword:

```text
this
```

tells C#:

> Treat this method as if it belongs to the `string` class.

Without `this`, it becomes an ordinary static method.

<br>

## 5.6 How the Compiler Sees It

When we write:

```csharp
name.ReverseText();
```

The compiler converts it to:

```csharp
StringExtensions.ReverseText(name);
```

This happens automatically.

<br>

## 5.7 Example: Integer Extension

```csharp
public static class IntegerExtensions
{
    public static bool IsEven(this int number)
    {
        return number % 2 == 0;
    }
}
```

Usage:

```csharp
int number = 10;

Console.WriteLine(number.IsEven());
```

Output:

```text
True
```

<br>

## 5.8 Extension Methods for Custom Classes

```csharp
public class Employee
{
    public string Name { get; set; }
}
```

Extension:

```csharp
public static class EmployeeExtensions
{
    public static void Display(this Employee emp)
    {
        Console.WriteLine(emp.Name);
    }
}
```

Usage:

```csharp
Employee employee = new Employee
{
    Name = "Yash"
};

employee.Display();
```

Output:

```text
Yash
```

<br>

## 5.9 Rules

Extension methods:

* Must be inside a static class.
* Must be static methods.
* First parameter must use the `this` keyword.
* Cannot access private members of the class.
* Can extend both built-in and custom classes.

<br>

## 5.10 Real-World Uses

Extension methods are widely used for:

* String helper methods
* Validation methods
* Formatting data
* LINQ
* ASP.NET Core
* Entity Framework
* Utility libraries

<br>

## 5.11 LINQ Uses Extension Methods

When you write:

```csharp
List<int> numbers =
    new List<int> {1,2,3,4};

var result =
    numbers.Where(n => n > 2);
```

It looks like `Where()` belongs to `List<T>`.

Actually:

```text
Where()

Select()

OrderBy()

First()

Last()

Count()
```

are extension methods provided by LINQ.

This is one of the biggest real-world uses of extension methods.

<br>

## 5.12 Benefits

* Extend existing classes.
* No inheritance required.
* Cleaner and more readable code.
* Keeps utility methods organized.
* Encourages code reuse.

<br>

## 5.13 Limitations

Extension methods:

* Cannot override existing methods.
* Cannot access private fields or methods.
* Are resolved at compile time.
* Should only add helper functionality, not core business logic.

<br>

## 5.14 Extension Method Flow

```text
Existing Class

        │

Create Static Class

        │

Create Static Method

        │

Use "this" Parameter

        │

Call Like Normal Method

object.ExtensionMethod()
```

<br>

## 5.15 Best Practices

* Group related extension methods together.
* Use meaningful names.
* Keep extension methods small and focused.
* Do not replace proper object-oriented design with extension methods.
* Use them for helper or utility functionality.

<br>

## 5.16 Interview Notes

### Why are Extension Methods useful?

They allow us to add functionality to existing classes without modifying their source code.

<br>

### Why must the class be static?

Extension methods belong to the class itself, not to an object.

<br>

### Why is the `this` keyword required?

It tells the compiler which type is being extended.

<br>

### Can Extension Methods access private members?

No.

Only public and accessible members can be used.

<br>

### Can Extension Methods override existing methods?

No.

If a class already contains a method with the same signature, the original method is always called.

<br>

## 5.17 Key Takeaways

* Extension Methods add new functionality to existing classes.
* They do not modify the original class.
* They must be declared inside a static class.
* They must be static methods.
* The first parameter uses the `this` keyword.
* LINQ methods like `Where()`, `Select()`, and `OrderBy()` are extension methods.
* Use extension methods for reusable helper functionality rather than core business logic.
