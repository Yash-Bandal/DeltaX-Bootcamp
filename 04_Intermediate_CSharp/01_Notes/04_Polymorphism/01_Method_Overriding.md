# Polymorphism

> [!Note]
> Polymorphism is a core concept in Object-Oriented Programming (OOP) that allows a single interface or method to represent different behaviors based on the specific object being used.
>
> The word literally translates to "many forms."

<br>


| Feature | Compile-Time (Static) | Runtime (Dynamic) |
| :--- | :--- | :--- |
| **Alternative Names** | Static Binding, Early Binding | Dynamic Binding, Late Binding |
| **How it is Achieved** | Method Overloading, Operator Overloaded | Method Overriding |
| **Resolution Time** | During code compilation | During program execution |
| **Execution Speed** | Faster (resolved beforehand) | Slower (resolved at runtime) |
| **Flexibility** | Lower | Higher |

<br>

```
 [ Press Run Button ] 
         │
         ▼
 1. COMPILE-TIME  ──► (Translates code. Checks for typos/syntax errors)
         │
         │ If successful...
         ▼
 2. RUNTIME       ──► (Launches the app. Executes the actual logic)

```

Errors that appear automatically while you are typing—before you ever press the "Run" button—are caught during compile time, \
specifically by a real-time process called static analysis.Modern code editors run a `background compiler` or `linter` that 
constantly reads your text to catch mistakes early.

<br>

# 4.1 Method Overriding



Method overriding lets a **derived class** provide its own implementation of a method already defined in the **base class**.

The method must have the same **name**, **return type**, and **parameters**.

This is also called **Runtime Polymorphism** — the decision of which method to call is made at **runtime**, based on the actual object type, not the reference type.

<br>

### Mind - `Child` Overrides (Replaces) Virtual Member of `Parent` 🏷️

<br>

# The Problem Without Overriding

Without overriding, every object calls the same base class method — regardless of what it actually is.

```csharp
public class Shape
{
    public void Draw()
    {
        Console.WriteLine("Drawing a shape");
    }
}

public class Circle : Shape { }

public class Rectangle : Shape { }
```

```csharp
Shape s1 = new Circle();
Shape s2 = new Rectangle();

s1.Draw();   // Drawing a shape
s2.Draw();   // Drawing a shape
```

Both print the same thing — the derived types have no voice.

This is where `virtual` and `override` come in.

<br>

# The Three Keywords

<br>
<div align = "center">
  <img width="800" alt="image" src="https://github.com/user-attachments/assets/da1f3577-2c70-4197-bf48-ebe8bf7659b3" />
</div>
<br>

```text
                        ┌─────────────┬──────────────────────────────────────────────────────┐
                        │   virtual   │  Base class — "this method CAN be overridden"        │
                        ├─────────────┼──────────────────────────────────────────────────────┤
                        │   override  │  Derived class — "I am replacing this method"        │
                        ├─────────────┼──────────────────────────────────────────────────────┤
                        │     new     │  Derived class — "I am HIDING this method (not OOP)" │
                        └─────────────┴──────────────────────────────────────────────────────┘
```

<br>

# `virtual` — Making a Method Overridable

By default, all methods in C# are **sealed** — they cannot be overridden.

Mark a base class method with `virtual` to allow derived classes to override it.

```csharp
public class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("Drawing a shape");
    }
}
```

> `virtual` = "I give permission to override this."

<br>

# `override` — Providing a New Implementation

In the derived class, use `override` to replace the virtual method.

```csharp
public class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a circle");
    }
}

public class Rectangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a rectangle");
    }
}
```

> `override` = "I am replacing the base class version."

<br>

# Runtime Polymorphism in Action

```csharp
Shape s1 = new Shape();
Shape s2 = new Circle();
Shape s3 = new Rectangle();

s1.Draw();   // Drawing a shape
s2.Draw();   // Drawing a circle    ← runtime picks Circle.Draw()
s3.Draw();   // Drawing a rectangle ← runtime picks Rectangle.Draw()
```

<br>

**Full code:**

```csharp
using System.Collections;

namespace CSharpdoubleermediate
{
    public class Shape
    {
        public virtual void Draw() //method in parent with name 'Draw'
        {
            Console.WriteLine("Drawing a shape");
        }
    }

    public class Circle : Shape
    {
        public override void Draw() //method in child with same name 'Draw'
        {
            Console.WriteLine("Drawing a circle");
        }
    }

    public class Rectangle : Shape
    {
        public override void Draw() //method in child with same name 'Draw'
        {
            Console.WriteLine("Drawing a rectangle");
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {   
            Shape shape = new Shape(); 
            shape.Draw();

            // Parent p = new Child() -> ~ Parent p = childObj
            Shape circle = new Circle();  //notice we are implicitely 'UPCASTING', instead of creating seperate parent class "Shape" again
            circle.Draw();

            Shape rectangle = new Rectangle();
            rectangle.Draw();
        }
    }
}
```
Output:
```
Drawing a shape
Drawing a circle
Drawing a rectangle
```


Even though `s2` and `s3` are declared as `Shape`, the **actual object type** decides which `Draw()` runs.

```text
Reference Type    Actual Object      Method Called
─────────────     ─────────────      ─────────────
Shape             Shape()            Shape.Draw()
Shape             Circle()      →    Circle.Draw()   ✓ overridden
Shape             Rectangle()   →    Rectangle.Draw() ✓ overridden
```

This is the core of polymorphism.

<br>

# Calling the Base Method with `base`

Sometimes you want to **extend** the base method, not completely replace it.

Use `base.MethodName()` to call the original base class implementation.

```csharp
public class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("Setting up canvas");
    }
}

public class Circle : Shape
{
    public override void Draw()
    {
        base.Draw();                        // ← calls Shape.Draw() first
        Console.WriteLine("Drawing a circle");
    }
}
```

```csharp
Circle c = new Circle();
c.Draw();
```

```text
Setting up canvas
Drawing a circle
```

> Think of `base.Draw()` as "do what the parent does, then add my own behaviour."

<br>

# Method Hiding with `new` — What NOT to Do

### Imagine `new` as `hide` 🏷️(when upcasting used)

If a derived class defines a method with the **same name** as a base class method but **without** `override`, it **hides** the base method.

The compiler warns you to add `new` explicitly to make it intentional.

```csharp
public class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("Drawing a shape");
    }
}

public class Circle : Shape
{
    public new void Draw()     // hiding, NOT overriding
    {
        Console.WriteLine("Drawing a circle");
    }
}
```

### The critical difference

```csharp
Circle c = new Circle();
c.Draw();          // Drawing a circle   (calls Circle.Draw)

Shape s = new Circle();
s.Draw();          // Drawing a shape    ← !! uses Shape.Draw, not Circle.Draw
```

With `new` (hiding), the reference type decides the method — **not the actual object**.

With `override`, the actual object always decides.

<br>

## **Example**
### With Upcasting
```csharp
using System.Collections;

namespace CSharpdoubleermediate
{
    public class Shape
    {
        public virtual void Draw() //method in parent with name 'Draw'
        {
            Console.WriteLine("Drawing a shape");
        }
    }

    public class Circle : Shape
    {
        // Think 'new' hides the child method, and displays parent method,
        public new void Draw() //method in child with same name 'Draw'
        {
            Console.WriteLine("Drawing a circle");
        }   
    }

    public class Rectangle : Shape
    {
        public new void Draw() //method in child with same name 'Draw'
        {
            Console.WriteLine("Drawing a rectangle");
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {   
            Shape shape = new Shape();
            shape.Draw();

            Shape circle = new Circle();
            circle.Draw();

            Shape rectangle = new Rectangle();
            rectangle.Draw();
        }
    }
}

```
Output:
```
Drawing a shape
Drawing a shape
Drawing a shape
```

### Without Upcasting
```csharp
using System.Collections;

namespace CSharpdoubleermediate
{
    public class Shape
    {
        public virtual void Draw() //method in parent with name 'Draw'
        {
            Console.WriteLine("Drawing a shape");
        }
    }

    public class Circle : Shape
    {
        public new void Draw() //method in child with same name 'Draw'
        {
            Console.WriteLine("Drawing a circle");
        }   
    }

    public class Rectangle : Shape
    {
        public new void Draw() //method in child with same name 'Draw'
        {
            Console.WriteLine("Drawing a rectangle");
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {   
            Shape shape = new Shape();
            shape.Draw();

            Circle circle = new Circle();
            //Shape circle = new Circle();
            circle.Draw();

            Rectangle rectangle = new Rectangle();
            //Shape rectangle = new Rectangle();
            rectangle.Draw();
        }
    }
}

```
```
Drawing a shape
Drawing a circle
Drawing a rectangle
```


<br>
<div align = "center">
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/51188b3c-f894-4181-b0a4-5f619db64741" />
</div>
<br>

```text
                     override                        new (hiding)
                  ───────────                     ──────────────
Shape s = new Circle();
s.Draw()     →   Circle.Draw()  ✓ polymorphic    Shape.Draw()  ✗ not polymorphic
```

> `new` breaks polymorphism. Avoid it unless you specifically want to hide and you understand the consequences.

<br>

# `override` vs `new` — Memory Map

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/242deb14-415e-4992-967b-fdbb477ba62e" />
</div>
<br>

```text
override                                  new (hiding)
────────                                  ────────────

Shape ref                                 Shape ref
    │                                         │
    ▼                                         ▼
Circle object                             Circle object
┌──────────────────────┐                 ┌──────────────────────┐
│ vtable pointer ──────┼──►Circle.Draw() │ vtable pointer ──────┼──►Shape.Draw()
│                      │                 │                      │
│ (Shape part hidden)  │                 │ Circle.Draw() ────── │ ← separate, unreachable
└──────────────────────┘                 └──────────────────────┘

Runtime looks up vtable → gets Circle     Runtime uses reference type → gets Shape
```

> `override` updates the **virtual dispatch table (vtable)** entry.
> `new` creates a **separate method slot** — it does not touch the vtable.

<br>

# Rules for Method Overriding

```text
✓  Base method must be  virtual / abstract / override
✓  Derived method must use  override
✓  Same name, return type, and parameters
✓  Same or less restrictive access modifier
✗  Cannot override static methods
✗  Cannot override non-virtual methods (use new instead)
✗  Cannot narrow the access (public → private is not allowed)
```

<br>

# Overriding Properties

You can override properties the same way as methods.

```csharp
public class Shape
{
    public virtual string Name => "Shape";
}

public class Circle : Shape
{
    public override string Name => "Circle";
}
```

```csharp
Shape s = new Circle();
Console.WriteLine(s.Name);   // Circle
```

<br>

# Overriding `ToString()`

`ToString()` is a `virtual` method on `object` — you can override it in any class.

```csharp
public class Circle : Shape
{
    public double Radius { get; set; }

    public override string ToString()
    {
        return $"Circle with radius {Radius}";
    }
}
```

```csharp
Circle c = new Circle { Radius = 5 };
Console.WriteLine(c);   // Circle with radius 5
```

<br>

# Compile-time vs Runtime Polymorphism

| | Compile-time | Runtime |
|---|---|---|
| Also called | Method overloading | Method overriding |
| Decided at | Compile time | Runtime |
| Keyword | (none — different parameters) | `virtual` + `override` |
| Inheritance needed | No | Yes |

<br>

# Full Example

```csharp
public class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("Drawing a shape");
    }
}

public class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a circle");
    }
}

public class Rectangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a rectangle");
    }
}

public class Triangle : Shape
{
    // Does NOT override Draw()
    // Inherits Shape.Draw() as-is
}
```

```csharp
Shape[] shapes =
{
    new Shape(),
    new Circle(),
    new Rectangle(),
    new Triangle()
};

foreach (Shape s in shapes)
{
    s.Draw();
}
```

```text
Drawing a shape
Drawing a circle
Drawing a rectangle
Drawing a shape       ← Triangle falls back to Shape.Draw()
```

> This is the power of polymorphism — one loop, multiple behaviours, all through a base class reference.

<br>

# Best Practices

- Mark a method `virtual` only if you **intend** for it to be overridden.
- Always use `override` (not `new`) when overriding in a derived class.
- Use `base.Method()` when you want to extend, not replace.
- Avoid deep override chains — they get hard to trace.
- Prefer composition over deeply nested inheritance hierarchies.

<br>

# Common Mistakes

## Forgetting `virtual` on the base method

```csharp
public class Shape
{
    public void Draw() { }      // ✗ not virtual
}

public class Circle : Shape
{
    public override void Draw() { }  // ✗ Compiler error
}
```

## Using `new` thinking it's the same as `override`

```csharp
Shape s = new Circle();
s.Draw();   // With 'new': calls Shape.Draw() — surprising!
            // With 'override': calls Circle.Draw() — correct!
```

## Changing access modifier to more restrictive

```csharp
public class Shape
{
    public virtual void Draw() { }
}

public class Circle : Shape
{
    protected override void Draw() { }  // ✗ Compiler error
}
```

<br>

# Summary

```text
Base class:           virtual void Draw()    ← permission to override

Derived class:       override void Draw()    ← replaces base version
                           or
                        new void Draw()      ← hides base version (not polymorphic)
```

- `virtual` marks a method as overridable in the base class.
- `override` replaces the base method — runtime picks the derived version even through a base reference.
- `new` hides the base method — reference type decides which version runs, not the object.
- `override` = **runtime polymorphism**. `new` = **method hiding**.
- Use `base.Method()` to extend rather than fully replace a base implementation.
- Without `virtual`/`override`, you cannot achieve true polymorphic behaviour.

<br>

# Interview Questions

### What is method overriding?

Providing a new implementation in a derived class for a method already defined in the base class, using `virtual` in the base and `override` in the derived class.

<br>

### What is the difference between `override` and `new`?

`override` achieves polymorphism — the actual object type decides the method at runtime.
`new` hides the base method — the reference type decides, breaking polymorphism.

<br>

### What is the difference between method overriding and method overloading?

| | Overriding | Overloading |
|---|---|---|
| Inheritance needed | Yes | No |
| Same parameters | Yes | No (different signature) |
| Resolved at | Runtime | Compile time |

<br>

### Can you override a non-virtual method?

No. The base method must be `virtual`, `abstract`, or already `override`.

<br>

### What does `base.Method()` do?

Calls the base class implementation from inside the overriding method — useful when you want to extend, not replace.

<br>

### What happens if a derived class does not override a virtual method?

The base class version is used. Derived classes are not forced to override.
