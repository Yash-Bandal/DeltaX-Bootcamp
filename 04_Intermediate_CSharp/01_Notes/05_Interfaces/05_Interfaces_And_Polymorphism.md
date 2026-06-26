# 5.5 Interfaces and Polymorphism

You already saw polymorphism through inheritance — a base class reference calling overridden methods on derived objects.

Interfaces give you the same power, but **without requiring a shared base class**.

Any unrelated class that implements the same interface can be treated as the same type.

<br>

# Quick Recap — Polymorphism via Inheritance

```csharp
public class Shape
{
    public virtual void Draw() { }
}

public class Circle : Shape
{
    public override void Draw() => Console.WriteLine("Drawing circle");
}

public class Rectangle : Shape
{
    public override void Draw() => Console.WriteLine("Drawing rectangle");
}
```

```csharp
Shape[] shapes = { new Circle(), new Rectangle() };

foreach (Shape s in shapes)
{
    s.Draw();   // runtime picks the right Draw()
}
```

This works — but only because `Circle` and `Rectangle` share a common base `Shape`.

What if the types have **nothing in common**?

<br>

# The Problem — Unrelated Types, Same Behaviour

Imagine three completely unrelated classes:

```csharp
public class Circle    { }   // a shape
public class VideoFile { }   // a media file
public class HtmlPage  { }   // a web document
```

All three can be drawn on screen. But they share no base class.

You cannot write:

```csharp
Shape[] things = { new Circle(), new VideoFile(), new HtmlPage() };   // ✗
```

`VideoFile` and `HtmlPage` are not `Shape`s. Inheritance can't bridge this.

This is exactly where **interface polymorphism** comes in.

<br>

# The Solution — A Shared Interface

Define the capability as an interface:

```csharp
public interface IDrawable
{
    void Draw();
}
```

Each class implements it independently:

```csharp
public class Circle : IDrawable
{
    public void Draw() => Console.WriteLine("Drawing a circle");
}

public class VideoFile : IDrawable
{
    public void Draw() => Console.WriteLine("Playing a video");
}

public class HtmlPage : IDrawable
{
    public void Draw() => Console.WriteLine("Rendering a page");
}
```

Now treat all three as `IDrawable`:

```csharp
IDrawable[] things = { new Circle(), new VideoFile(), new HtmlPage() };

foreach (IDrawable d in things)
{
    d.Draw();   // runtime picks the right Draw() for each
}
```

```text
Drawing a circle
Playing a video
Rendering a page
```

Polymorphism — through an interface, with no shared base class.

<br>

# Inheritance Polymorphism vs Interface Polymorphism

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/54567ab4-1435-4f6a-b313-f408590fa397" />
</div>
<br>


```text
Inheritance polymorphism          Interface polymorphism
──────────────────────            ──────────────────────

Shape                             IDrawable
  │                               (no hierarchy)
  ├── Circle                           │
  ├── Rectangle              Circle ──►┤
  └── Triangle            VideoFile ──►┤
                           HtmlPage ──►┘

Common base required          No base class required
Types must be related         Types can be completely unrelated
One base class only           Multiple interfaces allowed
```

Both give you the same loop:

```csharp
// inheritance           // interface
Shape s = new Circle();  IDrawable d = new Circle();
s.Draw();                d.Draw();
```

Runtime picks the right method in both cases.

<br>

# Multiple Interfaces — Multiple Polymorphic Behaviours


<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/1cc2a0ca-9de2-4658-b7ae-ef61a4370f31" />
</div>
<br>


A class can implement many interfaces, so it can participate in many polymorphic contexts.

```csharp
public interface IDrawable  { void Draw();   }
public interface IPrintable { void Print();  }
public interface ISaveable  { void Save();   }
```

```csharp
public class Report : IDrawable, IPrintable, ISaveable
{
    public void Draw()  => Console.WriteLine("Rendering report on screen");
    public void Print() => Console.WriteLine("Sending report to printer");
    public void Save()  => Console.WriteLine("Saving report to disk");
}
```

The same `Report` object can now be used in three different polymorphic contexts:

```csharp
IDrawable  d = new Report();  d.Draw();   // rendering context
IPrintable p = new Report();  p.Print();  // printing context
ISaveable  s = new Report();  s.Save();   // storage context
```

Through inheritance, a class can only be polymorphic with **one** parent type.
Through interfaces, it can be polymorphic with **as many** as it implements.

<br>

# Real-World Example — A Mixed Collection


<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/9e59d5d9-5f57-4749-a9c6-b1e66bf0f8b3" />
</div>
<br>

You have a drawing application with completely different object types.
All of them are drawable — so you store them as `IDrawable`.

```csharp
public interface IDrawable
{
    void Draw();
}

public class Circle : IDrawable
{
    public void Draw() => Console.WriteLine("Drawing circle");
}

public class TextBox : IDrawable
{
    public void Draw() => Console.WriteLine("Drawing text box");
}

public class Image : IDrawable
{
    public void Draw() => Console.WriteLine("Rendering image");
}

public class Canvas
{
    private List<IDrawable> _objects = new List<IDrawable>();

    public void Add(IDrawable obj)
    {
        _objects.Add(obj);
    }

    public void DrawAll()
    {
        foreach (IDrawable obj in _objects)
        {
            obj.Draw();    // each object draws itself
        }
    }
}
```

```csharp
var canvas = new Canvas();

canvas.Add(new Circle());
canvas.Add(new TextBox());
canvas.Add(new Image());

canvas.DrawAll();
```

```text
Drawing circle
Drawing text box
Rendering image
```

`Canvas` knows nothing about `Circle`, `TextBox`, or `Image`.
It only knows about `IDrawable`.
Adding a new drawable type — say `Triangle` — requires zero changes to `Canvas`.

<br>

# Interface Reference — What Is Accessible

Just like with base class references, through an interface reference you can only access members **defined on that interface**.

```csharp
public class Circle : IDrawable
{
    public void Draw() => Console.WriteLine("Drawing circle");

    public double Radius { get; set; }   // Circle-specific
}
```

```csharp
IDrawable d = new Circle();

d.Draw();          // ✓  defined on IDrawable
d.Radius = 5;      // ✗  not on IDrawable — compile error
```

To access `Circle`-specific members, downcast:

```csharp
if (d is Circle c)
{
    c.Radius = 5;   // ✓
}
```

Same rule as upcasting/downcasting with classes — the reference type controls what's visible.

<br>

# Summary

```text
interface IDrawable
    └── void Draw()

                     Circle     ──────►┐
                     VideoFile  ──────►┤  IDrawable[]
                     HtmlPage   ──────►┘
                                            │
                                     foreach (IDrawable d)
                                         d.Draw()    ← runtime picks each
```

- Interface polymorphism = same capability, completely unrelated types
- Use an interface reference (`IDrawable d`) to hold any implementing object
- The runtime picks the right implementation at call time — exactly like inheritance polymorphism
- Through an interface reference, only interface-defined members are accessible
- One class can implement many interfaces → participate in many polymorphic contexts
- This is the foundation of extensible, loosely coupled design

<br>

# Interview Questions

### What is interface polymorphism?

Using an interface as a reference type so that any implementing class — regardless of its inheritance — can be used interchangeably through that reference.

### How does interface polymorphism differ from inheritance polymorphism?

Inheritance polymorphism requires a shared base class. Interface polymorphism works across completely unrelated types — any class implementing the interface qualifies.

### Can you store objects of different types in one collection using an interface?

Yes. `List<IDrawable>` can hold any object whose class implements `IDrawable`, regardless of what base class it has.

### What is accessible through an interface reference?

Only the members defined on the interface itself. Class-specific members require a downcast.
