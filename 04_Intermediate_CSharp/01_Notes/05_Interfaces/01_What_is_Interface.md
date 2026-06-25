# 5.1 What is an Interface

An **interface** is a **contract**.

It defines *what* a class must do — but says nothing about *how*.

```csharp
public interface IDrawable
{
    void Draw();    // no body — just the contract
}
```

Any class that implements `IDrawable` **must** provide a `Draw()` method.

<br>

# Interface vs Abstract Class — The Key Distinction

You just learned abstract classes also enforce contracts. So why interfaces?

```text
Abstract Class  →  related things sharing a common base
                   e.g. Shape → Circle, Rectangle

Interface       →  unrelated things sharing a capability
                   e.g. Circle, Car, Person  all can be IDrawable
```

A `Car` and a `Circle` have nothing in common — they can't share a base class.
But both can be drawable. That's an interface.

<br>

# Declaring an Interface

```csharp
public interface IDrawable
{
    void Draw();
}
```

Rules:
- Name starts with `I` by convention — `IDrawable`, `ILogger`, `IDisposable`
- All members are `public` and have **no body** by default
- No fields, no constructors
- No `abstract` keyword needed — it's implied

<br>

# Implementing an Interface

<br>

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/065a11eb-8e22-4316-825f-708665b78edb" />
</div>

<br>



```csharp
public interface IDrawable
{
    void Draw();
}
```
```csharp
public class Circle : IDrawable
{
    public void Draw()
    {
        Console.WriteLine("Drawing a circle");
    }
}

public class Car : IDrawable
{
    public void Draw()
    {
        Console.WriteLine("Drawing a car");
    }
}
```

The class **must** implement every member of the interface — or it won't compile.

<br>

# Multiple Interfaces — The Real Power

A class can only inherit **one** base class.
But it can implement **multiple** interfaces.

<br>

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/5542361c-e73c-451d-87ec-b45227748ef7" />

</div>

<br>

```csharp
public interface IDrawable
{
    void Draw();
}

public interface IResizable
{
    void Resize(int factor);
}

public class Circle : Shape, IDrawable, IResizable
{
    public override void Draw()    { ... }
    public void Resize(int factor) { ... }
}
```

```text
Circle
  │
  ├── inherits   Shape       (one base class only)
  ├── implements IDrawable   (capability 1)
  └── implements IResizable  (capability 2)
```

<br>

# Interface as a Type

An interface can be used as a reference type — just like a base class.

```csharp
IDrawable d1 = new Circle();
IDrawable d2 = new Car();

d1.Draw();   // Drawing a circle
d2.Draw();   // Drawing a car
```

This is polymorphism through interfaces — completely unrelated classes, same call.

<br>

# What an Interface Can and Cannot Have

```text
┌─────────────────────────────┬──────────┬──────────────┐
│                             │Interface │Abstract Class│
├─────────────────────────────┼──────────┼──────────────┤
│ Method signatures           │   ✓      │     ✓        │
│ Properties (signature only) │   ✓      │     ✓        │
│ Concrete methods            │   ✓ *    │     ✓        │
│ Fields                      │   ✗      │     ✓        │
│ Constructors                │   ✗      │     ✓        │
│ Multiple inheritance        │   ✓      │     ✗        │
└─────────────────────────────┴──────────┴──────────────┘
* default interface methods — C# 8+, rarely used
```

<br>


# Summary

```text
interface IDrawable
    └── void Draw()          ← contract, no body

class Circle : IDrawable
    └── void Draw() { ... }  ← must implement

class Car    : IDrawable
    └── void Draw() { ... }  ← must implement

IDrawable d = new Circle();
d.Draw();   ← polymorphism — interface as type
```

- An interface is a pure contract — defines what, not how
- Any class can implement it regardless of its inheritance chain
- A class can implement multiple interfaces
- Name interfaces with a leading `I`
- Sections 5.2–5.5 cover the real-world uses: testability, extensibility, and polymorphism

<br>

> [!Tip]
> C# **does not support multiple inheritance of classes**. A class can inherit from only **one** base (parent) class.
>
> **Valid**
>
> ```csharp
> class Dog : Animal
> {
> }
> ```
>
> **Invalid**
>
> ```csharp
> class Dog : Animal, LivingThing
> {
> }
> ```
>
> To achieve similar functionality, C# allows a class to **implement multiple interfaces**.
>
> ```csharp
> interface IWalk
> {
>     void Walk();
> }
>
> interface ISwim
> {
>     void Swim();
> }
>
> class Duck : IWalk, ISwim
> {
>     public void Walk()
>     {
>     }
>
>     public void Swim()
>     {
>     }
> }
> ```
>
> **Therefore:**
>
> * C# supports **single inheritance** for classes.
> * C# supports **multiple interface implementation**.

<br>


# Interview Questions

### What is an interface?

A contract that defines method and property signatures a class must implement, with no implementation details.

<br>

### What is the difference between an interface and an abstract class?

An abstract class is for related types sharing a common base with some shared logic. An interface is a capability contract any unrelated type can implement. A class can only have one base class but implement many interfaces.

<br>

### Can you instantiate an interface?

No — but you can use it as a reference type pointing to any object that implements it.

<br>

### Why does the `I` prefix convention exist?

To immediately signal at the call site that you're working with an interface, not a class.

<br>
