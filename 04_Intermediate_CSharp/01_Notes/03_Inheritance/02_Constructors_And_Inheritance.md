# 3.2 Constructors and Inheritance

Constructors are not inherited, but they play an important role in inheritance.

When an object of a derived class is created, constructors are executed from the **base class to the derived class**.

Understanding this execution order is essential for building correct inheritance hierarchies.

<br>

# Are Constructors Inherited?

No.

Constructors are **not inherited** by derived classes.

Example

```csharp id="9e6y5z"
class Person
{
    public Person()
    {
    }
}

class Student : Person
{
}
```

Although `Student` inherits from `Person`, it does **not** inherit the `Person()` constructor.

Each class defines its own constructors.

<br>

# Constructor Execution Order

When creating a derived object:

```csharp id="l3twjq"
Student student = new Student();
```

The constructor execution order is:

```text id="d3yuhj"
1. Base Class Constructor
2. Derived Class Constructor
```

The base class is always initialized first.

<br>

# Example

```csharp id="0z9b7p"
class Person
{
    public Person()
    {
        Console.WriteLine("Person Constructor");
    }
}

class Student : Person
{
    public Student()
    {
        Console.WriteLine("Student Constructor");
    }
}
```

Usage

```csharp id="2g4h7d"
Student student = new Student();
```

Output

```text id="t6h1np"
Person Constructor
Student Constructor
```

<br>

# Why Does the Base Constructor Execute First?

The derived class depends on the base class.

Before creating the derived part of an object, the runtime must initialize the base part.

Think of a house:

```text id="xk7lq9"
Foundation
    ↓
Walls
    ↓
Roof
```

The foundation must be built first.

Similarly:

```text id="y5c3ms"
Base Class
    ↓
Derived Class
```

<br>

# Passing Parameters to Base Constructors

Suppose the base class requires parameters.

```csharp id="tcbuuv"
class Person
{
    public Person(string name)
    {
        Console.WriteLine(name);
    }
}
```

This derived class will not compile:

```csharp id="mnw1s5"
class Student : Person
{
    public Student()
    {
    }
}
```

Error:

```text id="k7qf6e"
Person does not contain a parameterless constructor.
```

The derived class must explicitly call the base constructor.

<br>

# The base Keyword

The `base` keyword is used to call a base class constructor.

Example

```csharp id="2d0t0w"
class Person
{
    public Person(string name)
    {
        Console.WriteLine($"Person: {name}");
    }
}

class Student : Person
{
    public Student(string name)
        : base(name)
    {
        Console.WriteLine("Student Created");
    }
}
```

Usage

```csharp id="zszn4i"
Student student = new Student("Yash");
```

Output

```text id="k70q7u"
Person: Yash
Student Created
```

<br>

# Constructor Chain in Inheritance

Consider the following hierarchy:

```text id="6vwd67"
Person
   ▲
   │
Employee
   ▲
   │
Manager
```

Code

```csharp id="h85i6z"
class Person
{
    public Person()
    {
        Console.WriteLine("Person");
    }
}

class Employee : Person
{
    public Employee()
    {
        Console.WriteLine("Employee");
    }
}

class Manager : Employee
{
    public Manager()
    {
        Console.WriteLine("Manager");
    }
}
```

Usage

```csharp id="js9wvw"
Manager manager = new Manager();
```

Output

```text id="2cn3g8"
Person
Employee
Manager
```

Constructors execute from the top of the hierarchy downwards.

<br>

# Implicit Base Constructor Call

If you don't explicitly use `base()`, C# automatically inserts a call to the parameterless constructor of the base class.

Example

```csharp id="87pp4l"
class Student : Person
{
    public Student()
    {
    }
}
```

Compiler treats it as:

```csharp id="0yylu9"
class Student : Person
{
    public Student()
        : base()
    {
    }
}
```

<br>

# Using base to Access Members

The `base` keyword can also access members of the base class.

Example

```csharp id="n6b1ux"
class Person
{
    public void Display()
    {
        Console.WriteLine("Person");
    }
}

class Student : Person
{
    public void Show()
    {
        base.Display();
    }
}
```

Although possible, `base` is most commonly used with constructors.

<br>

# Best Practices

* Keep constructors focused on initialization.
* Initialize the base class first.
* Use `base()` when the base class requires parameters.
* Avoid complex logic inside constructors.
* Design base classes so they can be initialized safely.

<br>

# Common Mistakes

## Forgetting to call a parameterized base constructor

```csharp id="bhf6xu"
class Student : Person
{
    public Student()
    {
    }
}
```

This fails if `Person` has no parameterless constructor.

<br>

## Assuming constructors are inherited

```csharp id="ys7slp"
Student student = new Student();
```

Constructors belong only to the class where they are declared.

<br>

## Writing duplicate initialization logic

Instead of repeating initialization code in derived classes, initialize common data in the base class.

<br>

# Interview Questions

### Are constructors inherited in C#?

No. Constructors are not inherited.

<br>

### What is the constructor execution order in inheritance?

Base class constructor executes first, followed by the derived class constructor.

<br>

### What is the purpose of the `base` keyword?

It is used to access members of the base class and to call base class constructors.

<br>

### What happens if a base class has only parameterized constructors?

The derived class must explicitly call one of them using `base()`.

<br>

### Why is the base constructor executed first?

Because the base part of an object must be initialized before the derived part.

<br>

# Summary

* Constructors are not inherited.
* Base class constructors always execute before derived class constructors.
* The `base` keyword is used to call base constructors.
* Constructor execution follows the inheritance hierarchy from top to bottom.
* If no `base()` call is specified, C# automatically calls the parameterless base constructor.
* Understanding constructor execution order is essential when working with inheritance.
