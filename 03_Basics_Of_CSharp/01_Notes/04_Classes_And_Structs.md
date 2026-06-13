
# 4. Classes and Structures

## 4.1 Classes

### What is a Class?

A class is a blueprint for creating objects.

Think of a class as a template.

Example:

```text
Blueprint → House
Class     → Object
```

A class defines:

* Data (Properties/Fields)
* Behavior (Methods)

<br>

### Creating a Class

```csharp
class Person
{
    public string Name;
    public int Age;
}
```

Here, `Person` is a class.

<br>

### Creating an Object

```csharp
Person person1 = new Person();

person1.Name = "Yash";
person1.Age = 21;
```

Access values:

```csharp
Console.WriteLine(person1.Name);
Console.WriteLine(person1.Age);
```

Output:

```text
Yash
21
```

<br>

### Class with Methods

```csharp
class Person
{
    public string Name;

    public void Introduce()
    {
        Console.WriteLine("Hello, I am " + Name);
    }
}
```

Usage:

```csharp
Person person = new Person();

person.Name = "Yash";

person.Introduce();
```

Output:

```text
Hello, I am Yash
```

<br>

### Real-World Example

```text
Class: Car

Properties:
- Brand
- Color
- Speed

Methods:
- Start()
- Stop()
- Accelerate()
```

Objects:

```text
BMW Car
Audi Car
Tesla Car
```

All are created from the same blueprint.

<br>

### Why Use Classes?

Benefits:

* Code organization
* Reusability
* Easier maintenance
* Object-Oriented Programming (OOP)

Most enterprise applications are built using classes.

<br>

### Class Example

```csharp
class Employee
{
    public string Name;
    public double Salary;
}
```

```csharp
Employee emp = new Employee();

emp.Name = "John";
emp.Salary = 50000;
```

<br>

## 4.2 Structs

### What is a Struct?

A struct is similar to a class.

It can contain:

* Fields
* Properties
* Methods
* Constructors

Example:

```csharp
struct Point
{
    public int X;
    public int Y;
}
```

Usage:

```csharp
Point point;

point.X = 10;
point.Y = 20;

Console.WriteLine(point.X);
```

Output:

```text
10
```

<br>

### When to Use Structs?

Use structs for small data objects.

Examples:

```text
Point
Coordinate
Color
Rectangle
Date
```

<br>

### Class vs Struct

| Class                    | Struct                           |
| ------------------------ | -------------------------------- |
| Reference Type           | Value Type                       |
| Stored on Heap           | Usually stored on Stack          |
| Can be Null              | Cannot be Null (unless nullable) |
| Better for large objects | Better for small objects         |
| More commonly used       | Less commonly used               |

<br>

### Example

#### Class

```csharp
class Person
{
    public string Name;
}
```

#### Struct

```csharp
struct Point
{
    public int X;
    public int Y;
}
```

<br>

### Industry Rule

Most of the time use:

```csharp
class
```

Use:

```csharp
struct
```

Only when:

* Object is small
* Represents a single value
* Does not require inheritance
* Performance is important

<br>

### Common Struct Examples in .NET

```csharp
DateTime
TimeSpan
Guid
Decimal
```

These are all structs.

<br>

## Quick Comparison

### Class

* Blueprint for objects
* Reference Type
* Most commonly used
* Supports inheritance
* Ideal for business objects

Examples:

```text
Employee
Customer
Product
Order
Student
```

<br>

### Struct

* Small lightweight object
* Value Type
* Faster for small data
* No inheritance
* Used less frequently

Examples:

```text
Point
DateTime
Color
Coordinates
```

<br>

## Key Takeaways

* A class is a blueprint used to create objects.
* Objects contain data and behavior.
* Classes are Reference Types.
* Structs are Value Types.
* Classes are used in most real-world applications.
* Structs are best for small, lightweight data structures.
* If unsure, use a class.
