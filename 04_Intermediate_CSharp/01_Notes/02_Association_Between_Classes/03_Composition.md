# 2.3 Composition

Composition is an Object-Oriented Programming (OOP) principle where one class **contains** or **uses** another class to achieve its functionality.

Composition represents a **"has-a" relationship** and is often preferred over inheritance because it creates loosely coupled and more maintainable code.

<br>

# What is Composition?

Composition is a relationship where one object is built using one or more other objects.

Example

```text
Car has an Engine
Computer has a Processor
House has Rooms
Library has Books
```

Notice that these are **"has-a"** relationships, not **"is-a"** relationships.

<br>

# Why Do We Need Composition?

Suppose we are designing a `Car`.

A car is **not** an engine.

Instead,

```text
Car
 └── Engine
```

The `Car` class should contain an `Engine` object instead of inheriting from it.

This models the real world more accurately.

<br>

# Example

```csharp id="m8bhpr"
class Engine
{
    public void Start()
    {
        Console.WriteLine("Engine Started");
    }
}

class Car
{
    private Engine engine = new Engine();

    public void StartCar()
    {
        engine.Start();
        Console.WriteLine("Car is Ready");
    }
}
```

Usage

```csharp id="a5b1yj"
Car car = new Car();

car.StartCar();
```

Output

```text id="lmv2u0"
Engine Started
Car is Ready
```

The `Car` object **uses** the `Engine` object instead of inheriting from it.

<br>

# Has-A vs Is-A Relationship

Understanding these two relationships helps decide whether to use composition or inheritance.

### Is-A Relationship (Inheritance)

```text
Dog is an Animal
Car is a Vehicle
Manager is an Employee
```

Use **Inheritance**.

<br>

### Has-A Relationship (Composition)

```text
Car has an Engine
Person has an Address
Computer has a Keyboard
School has Students
```

Use **Composition**.

<br>

# Composition vs Inheritance

| Composition              | Inheritance                                  |
| ------------------------ | -------------------------------------------- |
| Has-a relationship       | Is-a relationship                            |
| Uses other objects       | Inherits from another class                  |
| Loosely coupled          | Can become tightly coupled                   |
| More flexible            | Less flexible                                |
| Preferred for code reuse | Preferred only for true "is-a" relationships |

<br>

# Why Composition is Preferred

Imagine replacing a car's engine.

```text
Old Engine
      ↓
New Engine
```

Only the engine changes.

The `Car` class remains the same.

If `Car` inherited from `Engine`, replacing the engine would require changing the inheritance hierarchy.

Composition keeps classes independent and easier to modify.

This is why the OOP design principle says:

> **Favor Composition over Inheritance.**

<br>

# Composition Promotes Loose Coupling

Using composition reduces dependency between classes.

Example

```csharp id="qlifv3"
class Logger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

class OrderProcessor
{
    private readonly Logger logger;

    public OrderProcessor(Logger logger)
    {
        this.logger = logger;
    }

    public void Process()
    {
        logger.Log("Processing Order...");
    }
}
```

`OrderProcessor` **has a** `Logger`.

It does not inherit from `Logger`.

<br>

# When to Use Composition

Use composition when:

* One object contains another object.
* Objects work together to complete a task.
* There is a **"has-a"** relationship.
* Flexibility and maintainability are important.

Examples

* Car has an Engine.
* Customer has an Address.
* Order has Order Items.
* University has Departments.
* Mobile Phone has a Battery.

<br>

# Best Practices

* Prefer composition over inheritance whenever possible.
* Use inheritance only for genuine "is-a" relationships.
* Keep classes focused on a single responsibility.
* Design classes to work together rather than depend heavily on each other.

<br>

# Common Mistakes

## Using inheritance for a "has-a" relationship

Incorrect

```text
Car inherits Engine
```

Correct

```text
Car has an Engine
```

<br>

## Choosing inheritance only to reuse code

Do not use inheritance simply because it avoids writing duplicate code.

Always check whether an **"is-a"** relationship actually exists.

<br>

## Creating tightly coupled classes

Avoid creating unnecessary dependencies between classes.

Composition helps keep classes independent and easier to replace.

<br>

# Interview Questions

### What is composition?

Composition is an OOP principle where one class contains or uses another class to achieve its functionality.

<br>

### What type of relationship does composition represent?

A **"has-a"** relationship.

<br>

### What is the difference between composition and inheritance?

* Composition models a **has-a** relationship.
* Inheritance models an **is-a** relationship.

<br>

### Why is composition preferred over inheritance?

Because it creates loosely coupled, flexible, reusable, and maintainable code.

<br>

### Give some real-world examples of composition.

* Car has an Engine.
* Person has an Address.
* Computer has a Processor.
* Order has Order Items.

<br>

# Summary

* Composition means one class contains or uses another class.
* It represents a **"has-a"** relationship.
* Composition promotes loose coupling and flexibility.
* Inheritance should only be used for **"is-a"** relationships.
* A widely accepted OOP principle is **"Favor Composition over Inheritance."**
