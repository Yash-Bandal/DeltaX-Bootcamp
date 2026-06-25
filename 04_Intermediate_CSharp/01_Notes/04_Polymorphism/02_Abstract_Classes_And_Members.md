# 4.2 Abstract Classes and Members

An **abstract class** is a class that is **incomplete by design**.

It defines a **contract 🏷️** — what derived classes *must* implement — without providing the implementation itself.

```text
Abstract Class  =  "Here is what you MUST have."
Concrete Class  =  "Here is HOW it works."
```

<br>

# The Problem That Abstract Solves

In 4.1, `Shape.Draw()` had a default body:

```csharp
public class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("Drawing a shape");   // ← meaningless default
    }
}
```

But a generic "shape" has no meaningful `Draw()`.
Only **Circle**, **Rectangle**, **Triangle** know how to draw themselves.

The `virtual` approach has a flaw — derived classes *can* override `Draw()`, but they are **not forced to**.
You could forget to override it, and the meaningless base version silently runs.

**Abstract** fixes this — it forces every derived class to provide an implementation, or it won't compile.

<br>

# `abstract` Keyword — Two Uses

<br>
<div align = "center">
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/cb2075b1-81a2-4556-913f-f30cc01f36e8" />
</div>
<br>

```text
                      ┌───────────────────┬─────────────────────────────────────────────────────┐
                      │  abstract class   │  Cannot be instantiated — must be inherited         │
                      ├───────────────────┼─────────────────────────────────────────────────────┤
                      │  abstract method  │  No body — derived class MUST override it           │
                      └───────────────────┴─────────────────────────────────────────────────────┘
```

Both go together — abstract methods can only live inside abstract classes.

<br>

# Declaring an Abstract Class

```csharp
public abstract class Shape
{
    public abstract void Draw();   // no body, no { }
}
```

Rules:
- `abstract` keyword before `class`
- Abstract methods have **no body** — just the signature followed by `;`
- No `virtual` needed — abstract is implicitly virtual

<br>

# Cannot Instantiate an Abstract Class

```csharp
Shape s = new Shape();   // ✗ Compile error
```

```text
Cannot create an instance of the abstract class or interface 'Shape'
```

> [!important]
> Abstract classes exist only to be inherited. 🏷️ They are **blueprints**, not objects.

<br>

# Derived Class Must Implement All Abstract Members

```csharp
public abstract class Shape
{
    public abstract void Draw();
}

public class Circle : Shape
{
    public override void Draw()              // ✓ must use override
    {
        Console.WriteLine("Drawing a circle");
    }
}
```

If you forget to implement `Draw()` in `Circle`:

```text
✗ Compile error:
'Circle' does not implement inherited abstract member 'Shape.Draw()'
```

> The compiler enforces the contract. No silent fallback like `virtual`.

<br>

# Abstract Class — What It Can Contain

Unlike an interface (coming in section 5), an abstract class can have **a mix** of:

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/8b1420ce-9358-40c8-b88e-76e99b3bfb00" />
</div>


<br>

```text
┌──────────────────────────────────┬────────────────────────────────┐
│  abstract members                │  concrete members              │
│  (no body — must be overridden)  │  (have body — inherited as-is) │
├──────────────────────────────────┼────────────────────────────────┤
│  abstract void Draw();           │  public void Move() { ... }    │
│  abstract string Name { get; }   │  public int Id { get; set; }   │
│                                  │  constructor                   │
│                                  │  fields                        │
└──────────────────────────────────┴────────────────────────────────┘
```

This is the key difference from interfaces — abstract classes can hold **shared state and logic**.

<br>

# Full Example

```csharp
public abstract class Shape
{
    // Abstract — no body, derived MUST override
    public abstract void Draw();

    // Concrete — shared by all derived classes
    public void Move(int x, int y)
    {
        Console.WriteLine($"Moving to ({x}, {y})");
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
```

```csharp
Shape[] shapes = { new Circle(), new Rectangle() };

foreach (Shape s in shapes)
{
    s.Draw();    // polymorphic — each calls its own Draw()
    s.Move(1, 2); // shared — same for all
}
```

```text
Drawing a circle
Moving to (1, 2)
Drawing a rectangle
Moving to (1, 2)
```

<br>

# Memory Map — Abstract Class Hierarchy

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/f3cf462f-80ba-4aed-8554-55c56f52c0d3" />
</div>

<br>


```text
          ┌─────────────────────────┐
          │   Shape  (abstract)     │
          │─────────────────────────│
          │  + Draw()  ← no body   │    ← cannot instantiate this
          │  + Move()  ← has body  │
          └──────────┬──────────────┘
                     │
          ┌──────────┴──────────┐
          │                     │
  ┌───────▼────────┐   ┌────────▼────────┐
  │  Circle        │   │  Rectangle      │
  │────────────────│   │─────────────────│
  │ + Draw() ──────│   │ + Draw() ───────│    ← each provides its own body
  │   "circle"     │   │   "rectangle"   │
  │ + Move() ──────│   │ + Move() ───────│    ← inherited from Shape
  └────────────────┘   └─────────────────┘

  new Circle()   ✓         new Shape()  ✗
  new Rectangle() ✓
```

<br>

# Abstract Properties

You can also declare abstract **properties**.

```csharp
public abstract class Shape
{
    public abstract string Name { get; }    // no body
    public abstract double Area { get; }
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public override string Name => "Circle";

    public override double Area => Math.PI * Radius * Radius;
}
```

```csharp
Shape s = new Circle { Radius = 5 };

Console.WriteLine(s.Name);    // Circle
Console.WriteLine(s.Area);    // 78.53...
```

<br>

# `abstract` vs `virtual` — The Key Difference

```text
┌──────────┬────────────────────────────┬───────────────────────────────┐
│          │         virtual            │           abstract            │
├──────────┼────────────────────────────┼───────────────────────────────┤
│ Has body │ Yes — default impl         │ No — no body at all           │
│ Override │ Optional                   │ Mandatory                     │
│ Class    │ Concrete class             │ Must be in abstract class     │
│ Fallback │ Base version runs if not   │ Compile error if not          │
│          │ overridden                 │ overridden                    │
└──────────┴────────────────────────────┴───────────────────────────────┘
```

```csharp
// virtual — optional override, has a fallback
public virtual void Draw()
{
    Console.WriteLine("Drawing a shape");   // runs if not overridden
}

// abstract — mandatory override, no fallback
public abstract void Draw();                // derived MUST override this
```

<br>

# Can an Abstract Class Have a Constructor?

Yes — even though you can't instantiate it directly, the constructor runs when a derived class is instantiated.

```csharp
public abstract class Shape
{
    public int Id { get; }

    protected Shape(int id)       // constructor
    {
        Id = id;
    }

    public abstract void Draw();
}

public class Circle : Shape
{
    public Circle(int id) : base(id) { }   // calls Shape constructor

    public override void Draw()
    {
        Console.WriteLine($"Circle #{Id}");
    }
}
```

```csharp
Circle c = new Circle(1);
c.Draw();   // Circle #1
```

<br>

# Partial Implementation — Abstract Class Inheriting Abstract Class

An abstract class can inherit another abstract class and implement **only some** of the abstract members.
The remaining ones pass down to the next concrete class.

```csharp
public abstract class Shape
{
    public abstract void Draw();
    public abstract void Resize();
}

public abstract class ColoredShape : Shape
{
    public string Color { get; set; }

    public override void Draw()                    // implements Draw
    {
        Console.WriteLine($"Drawing in {Color}");
    }

    // Resize() still abstract — not implemented here
}

public class ColoredCircle : ColoredShape
{
    public override void Resize()                  // must implement Resize
    {
        Console.WriteLine("Resizing circle");
    }
}
```

```text
Shape (abstract)
  ├── Draw()   abstract
  └── Resize() abstract
        │
ColoredShape (abstract)
  ├── Draw()   implemented ✓
  └── Resize() still abstract
        │
ColoredCircle (concrete)
  └── Resize() implemented ✓   ← now all abstract members satisfied
```

<br>

# When to Use Abstract Classes

Use `abstract` when:

```text
✓  You have a group of related classes that share some logic
   but each must provide their own version of certain methods

✓  You want to enforce that derived classes implement specific methods
   (not just "can" override — but "must")

✓  You need shared state (fields, constructors) across the hierarchy
   (interfaces can't hold state)

✓  The base class concept is incomplete on its own
   e.g. "Shape" is not a real drawable thing — Circle and Rectangle are
```

<br>

# Abstract vs Interface — Quick Preview

| | Abstract Class | Interface |
|---|---|---|
| Can have fields | Yes | No |
| Can have constructors | Yes | No |
| Can have concrete methods | Yes | Yes (default, C# 8+) |
| Multiple inheritance | No (one base class) | Yes (multiple interfaces) |
| Use when | Shared base + enforce contract | Pure contract / capability |

> Full interfaces coverage in section 5.

<br>

# Summary

```text
abstract class Shape
│
│  abstract void Draw();     ← no body — derived MUST override
│  void Move() { ... }       ← concrete — shared by all
│
├── Circle    → override Draw() { "circle" }
├── Rectangle → override Draw() { "rectangle" }
└── Triangle  → override Draw() { "triangle" }

new Shape()     ✗  cannot instantiate
new Circle()    ✓  concrete — all abstract members implemented
```

- `abstract class` — cannot be instantiated, must be inherited
- `abstract method` — no body, derived class is **forced** to override it
- Abstract classes can mix abstract and concrete members
- Use `abstract` over `virtual` when there is no sensible default implementation
- The compiler enforces the contract — no silent fallbacks

<br>

# Interview Questions

### What is an abstract class?

A class marked `abstract` that cannot be instantiated and may contain abstract members that derived classes must implement.

<br>

### What is an abstract method?

A method with no body — only a signature. The derived class is required to provide the implementation using `override`.

<br>

### Can an abstract class have concrete methods?

Yes. It can have both abstract members (no body) and concrete members (with body), as well as fields and constructors.

<br>

### Can you instantiate an abstract class?

No. `new Shape()` where `Shape` is abstract causes a compile error.

<br>

### What is the difference between `abstract` and `virtual`?

`virtual` provides a default body and override is optional. `abstract` has no body and override is mandatory — a compile error if missing.

<br>

### Can an abstract class have a constructor?

Yes. It runs when a derived class is instantiated via `base(...)`.

<br>

### What happens if a derived class does not implement all abstract members?

Compile error — the derived class must either implement all abstract members or itself be declared `abstract`.
