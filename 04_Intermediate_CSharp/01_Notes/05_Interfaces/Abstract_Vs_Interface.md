# Abstract vs Virtual

## Interview Answer (20 sec)

> **`virtual` provides a default implementation that child classes may override. `abstract` provides no implementation and forces child classes to implement it. Also, `virtual` can be used in normal classes, whereas `abstract` methods can only exist inside an abstract class.**

<br>

# Difference

| `virtual` | `abstract` |
|-----------|------------|
| Has implementation | No implementation |
| Child **may** override | Child **must** override |
| Used in normal classes | Used only in abstract classes |
| Class object can be created | Abstract class object cannot be created |

<br>

# virtual

## What?

Provides a **default implementation**.

## Why?

When child classes **can customize** the behavior if needed.

## Example

```csharp
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal Sound");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Bark");
    }
}
```

<br>

# abstract

## What?

Declares a method **without implementation**.

## Why?

To force every child class to provide its own implementation.

## Example

```csharp
abstract class Animal
{
    public abstract void Speak();
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Bark");
    }
}
```

<br>

# When to use?

## Use `virtual` when

- You already have a default/common implementation.
- Child classes may change it if required.

### Example

```
Employee.CalculateSalary()
```

Most employees use the default salary calculation.

<br>

## Use `abstract` when

- There is **no meaningful default implementation**.
- Every child **must** implement it.

### Example

```
Shape.CalculateArea()
```

A Shape cannot calculate its own area because it doesn't know whether it's a Circle or Rectangle.

<br>

# Keywords

- `virtual` → Optional override
- `abstract` → Mandatory override
- `override` → Child implementation

<br>

# Easy Trick to Remember

```
virtual  = "You can override me."
abstract = "You must override me."
```

<br>

# Can we use abstract without override?

## Interview Answer (10 sec)

> **No. An abstract method must be implemented using `override` in the first concrete (non-abstract) derived class.**

<br>

## Why?

An abstract method has **no implementation**.

```csharp
abstract class Animal
{
    public abstract void Speak();
}
```

So the child class **must** provide the implementation.

```csharp
class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Bark");
    }
}
```

Notice we use **`override`**.

<br>

## What if we don't use override?

```csharp
class Dog : Animal
{
    public void Speak()   // ❌ Error
    {
        Console.WriteLine("Bark");
    }
}
```

### Compiler Error

```
'Dog' does not implement inherited abstract member 'Animal.Speak()'
```

Because `Speak()` must **override** the abstract method.

<br>

## Exception

If the child class is also **abstract**, then it doesn't have to override immediately.

```csharp
abstract class Animal
{
    public abstract void Speak();
}

abstract class Mammal : Animal
{
    // No override required
}

class Dog : Mammal
{
    public override void Speak()
    {
        Console.WriteLine("Bark");
    }
}
```

### Here

- `Mammal` is abstract, so it can leave `Speak()` unimplemented.
- `Dog` is the first concrete class, so it **must** override it.

<br>

# Rule to Remember

```
Concrete Class
    ↓
Must override all inherited abstract methods.

Abstract Class
    ↓
May leave abstract methods unimplemented.
```

<br>

# Can an Abstract Class be used in place of an Interface?

## Interview Answer (20 sec)

> **Yes. An abstract class can be used like an interface by declaring only abstract methods. However, interfaces are preferred because C# allows multiple interfaces but only one base class.**

<br>

# Using an Interface (Recommended)

```csharp
interface IPrinter
{
    void Print();
}

class Student : IPrinter
{
    public void Print()
    {
        Console.WriteLine("Student");
    }
}

class Invoice : IPrinter
{
    public void Print()
    {
        Console.WriteLine("Invoice");
    }
}

class Report : IPrinter
{
    public void Print()
    {
        Console.WriteLine("Report");
    }
}
```

### Why?

- Unrelated classes can implement the same interface.
- No inheritance relationship is required.

<br>

# Using an Abstract Class

```csharp
abstract class Printer
{
    public abstract void Print();
}

class Student : Printer
{
    public override void Print()
    {
        Console.WriteLine("Student");
    }
}

class Invoice : Printer
{
    public override void Print()
    {
        Console.WriteLine("Invoice");
    }
}
```

This also works.

So, **an abstract class can behave like an interface.**

<br>

# Then why do we prefer Interfaces?

Because C# supports **only one base class**.

### Example

```csharp
class Person
{
}

abstract class Printer
{
    public abstract void Print();
}

class Student : Person, Printer   // ❌ Error
{
}
```

**Error:** A class can inherit only **one** base class.

<br>

# With Interface

```csharp
class Person
{
}

interface IPrinter
{
    void Print();
}

class Student : Person, IPrinter   // ✔ Works
{
    public void Print()
    {
        Console.WriteLine("Student");
    }
}
```

Works perfectly.

<br>

# Visual Representation

## Using Abstract Class

```
        Printer (Abstract)
              ▲
              │
          Student

Cannot inherit another class.
```

<br>

## Using Interface

```
           Person
              ▲
              │
          Student
              │
        Implements
              │
          IPrinter
```

Student gets both:

- Person features
- Printing capability

<br>

# Why Interfaces are Preferred

- Supports **multiple interfaces**
- Defines a **contract**
- More flexible
- Loose coupling
- Better for Dependency Injection

<br>

# Rule to Remember

```
Abstract Class
    ↓
Represents "IS-A" relationship.

Example:
Dog IS-A Animal.

<br><br><br><br><br><br><br><br><br>-

Interface
    ↓
Represents "CAN-DO" capability.

Example:
Car CAN Print.
Person CAN Print.
Report CAN Print.
```
