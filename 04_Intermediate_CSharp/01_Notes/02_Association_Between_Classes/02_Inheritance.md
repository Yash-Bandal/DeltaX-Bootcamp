# 2.2 Inheritance

Inheritance is one of the four fundamental principles of Object-Oriented Programming (OOP).

It allows one class to **inherit the members (fields, methods, properties, etc.) of another class**, promoting code reuse and establishing an **"is-a" relationship** between classes.

<br>

> [!Note]
> In CSharp, class can have only 1 parent  

<br>

<div align = "center">

   <img width="600"  alt="image" src="https://github.com/user-attachments/assets/92649d4e-f6ae-4122-9ee7-68a2a9f564eb" />

</div>

<br>

# What is Inheritance?

**Inheritance** is the mechanism by which one class acquires the members of another class.

* The existing class is called the **Base (Parent) Class**.
* The new class is called the **Derived (Child) Class**.


<br>

<div align = "center">
<img width="640" height="480" alt="image" src="https://github.com/user-attachments/assets/bddaf5c6-e979-434d-a07f-bacc412ee5c9" />


</div>

<br>
Example

```text
Vehicle          ← Base Class
   ▲
   │
Car              ← Derived Class
```

A **Car is a Vehicle**, so inheritance is appropriate.

<br>

# Why Do We Need Inheritance?

Without inheritance, common code must be repeated in multiple classes.

Example without inheritance:

```csharp
class Car
{
    public void Start()
    {
        Console.WriteLine("Starting...");
    }
}

class Bike
{
    public void Start()
    {
        Console.WriteLine("Starting...");
    }
}
```

The `Start()` method is duplicated.

Using inheritance:

```csharp
class Vehicle
{
    public void Start()
    {
        Console.WriteLine("Starting...");
    }
}

class Car : Vehicle
{
}

class Bike : Vehicle
{
}
```

Both `Car` and `Bike` reuse the `Start()` method.

<br>

# Syntax

```csharp
class DerivedClass : BaseClass
{
}
```

Example

```csharp
class Animal
{
    public void Eat()
    {
        Console.WriteLine("Eating...");
    }
}

class Dog : Animal
{
}
```

Usage

```csharp
Dog dog = new Dog();

dog.Eat();
```

Output

```
Eating...
```

<br>

# What Gets Inherited?

A derived class inherits the accessible members of its base class, such as:

* Fields
* Methods
* Properties
* Events
* Nested Types

Constructors are **not inherited**, but they are executed during object creation (covered later).

<br>

# The "Is-A" Relationship

Inheritance should only be used when an **"is-a" relationship** exists.

Good examples

```text
Car is a Vehicle
Dog is an Animal
Student is a Person
```

Bad examples

```text
Car is an Engine
House is a Door
Customer is an Address
```

These are **"has-a" relationships** and should use **Composition**, not inheritance.

<br>

# Benefits of Inheritance

* Promotes code reuse.
* Reduces duplication.
* Improves maintainability.
* Makes code easier to extend.
* Supports polymorphism (covered later).

<br>

# Limitations of C# Inheritance

## Single Inheritance

A class can inherit from **only one base class**.

Valid

```csharp
class Dog : Animal
{
}
```

Invalid

```csharp
class Dog : Animal, LivingThing
{
}
```

C# does not support multiple class inheritance.

Instead, it supports multiple **interfaces**.

<br>

# Multi-Level Inheritance

A class can inherit from another derived class.

```text
Animal
   ▲
   │
Mammal
   ▲
   │
Dog
```

Each derived class inherits from the class directly above it.

<br>

**Example:**
```csharp
namespace CSharpdoubleermediate
{ 
    public class Animal
    {
        public string Name {
            get;
            set; 
        }

        public Animal()
        {
            this.Name = "Unknown";
        }

        public void Intro(string name)
        {
            Console.WriteLine("{0} is a Animal ", name);
        }
    }

    public class Dog : Animal
    {
        private string name = "Rock";

        public Dog()
        {
            Name = name;
            //Intro(name); //you can use parent methods inside 
        }

        public void MakeSound()
        {
            Console.WriteLine("Bark");
        }

        
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Dog dog = new Dog();
            dog.Intro("Jimmy");
            dog.MakeSound();

        }
    }
}

```

<br>

# Best Practices

* Use inheritance only for genuine **"is-a" relationships**.
* Keep inheritance hierarchies simple.
* Avoid deep inheritance chains.
* Prefer composition when inheritance doesn't naturally fit.
* Reuse behavior through inheritance instead of duplicating code.

<br>

# Common Mistakes

## Using inheritance for code reuse alone

Bad

```text
Car inherits Engine
```

A car **has an** engine—it is not an engine.

Use composition instead.

<br>

## Deep inheritance hierarchies

```text
A
▲
B
▲
C
▲
D
▲
E
```

Deep hierarchies make code harder to understand and maintain.

<br>

## Forgetting the "is-a" test

Before using inheritance, ask:

> **Can I honestly say "Derived Class is a Base Class"?**

If not, inheritance is probably the wrong choice.

<br>

# Interview Questions

### What is inheritance?

Inheritance is an OOP feature that allows one class to inherit the members of another class, promoting code reuse and establishing an "is-a" relationship.

<br>

### What is a base class?

A base class (parent class) is the class whose members are inherited by another class.

<br>

### What is a derived class?

A derived class (child class) is the class that inherits from a base class.

<br>

### What is the syntax for inheritance in C#?

```csharp
class Derived : Base
{
}
```

<br>

### Does C# support multiple inheritance?

No. A class can inherit from only one base class.

However, it can implement multiple interfaces.

<br>

### When should inheritance be used?

Use inheritance only when there is a clear **"is-a" relationship** between two classes.

<br>

# Summary

* Inheritance allows a derived class to reuse members of a base class.
* It promotes code reuse and reduces duplication.
* It models an **"is-a" relationship**.
* Constructors are not inherited.

<br>
* C# supports single inheritance but allows multiple interfaces.
* Use inheritance only when it accurately represents the relationship between objects.
