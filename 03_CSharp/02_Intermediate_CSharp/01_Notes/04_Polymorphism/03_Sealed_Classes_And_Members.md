# 4.3 Sealed Classes and Members 

<br>

> [!Tip]
> Not much used, ms docs just gave this method t slightly increase performance

> [!Important]
> `Seal` , nor inherit further, nor override

<br>

A **sealed class** cannot be inherited.

A **sealed method** cannot be overridden further down the hierarchy.

```text
sealed  =  "This is the end of the line."
```

<br>

### Opposite of Abstract
1. Abstract do allow child to changing behaviour of parent
2. Sealed dont allow child to change behaviour of parent

<br>

# Why Seal Anything?

In 4.1 and 4.2 you learned to open things up — `virtual` and `abstract` let derived classes change behaviour.

`sealed` is the opposite — it **locks behaviour down**.

Two reasons to seal:

```text
1. Design intent    — this class is complete, inheritance would break it
2. Performance      — sealed methods skip virtual dispatch (minor, but real)
```

<br>

# Sealed Class — Cannot Be Inherited

```csharp
public sealed class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a circle");
    }
}
```

Trying to inherit from it:

```csharp
public class SpecialCircle : Circle { }   // ✗ Compile error
```

```text
'SpecialCircle': cannot derive from sealed type 'Circle'
```

<br>

# Sealed Method — Cannot Be Overridden Further

A sealed method stops the override chain at a specific level.

It can only be used on a method that is **already overriding** something — you can't seal a fresh method.

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
    public sealed override void Draw()     // ← sealed here
    {
        Console.WriteLine("Drawing a circle");
    }
}

public class SpecialCircle : Circle
{
    public override void Draw() { }        // ✗ Compile error
}
```

```text
'SpecialCircle.Draw()': cannot override inherited member 'Circle.Draw()' because it is sealed
```

> `Circle` can still be inherited — just not its `Draw()` method.

<br>

# The Override Chain

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/2218f64f-c37f-432e-b6ea-cd10b82069f4" />
</div>
<br>

```text
Shape               virtual void Draw()     ← can be overridden
   │
   ▼
Circle              sealed override Draw()  ← overrides Shape, but locks here
   │
   ▼
SpecialCircle       override Draw()         ✗ blocked — Circle.Draw is sealed
```

Without `sealed`:

```text
Shape               virtual void Draw()
   │
   ▼
Circle              override void Draw()    ← can still be overridden
   │
   ▼
SpecialCircle       override void Draw()    ✓ allowed
```

<br>

# Sealed Class vs Sealed Method

```text
┌────────────────────┬──────────────────────────────────────────────────┐
│   sealed class     │  No one can inherit this class at all            │
├────────────────────┼──────────────────────────────────────────────────┤
│   sealed method    │  No one can override this method further         │
│                    │  (the class itself can still be inherited)       │
└────────────────────┴──────────────────────────────────────────────────┘
```

```csharp
// sealed class — blocks inheritance entirely
public sealed class Circle : Shape { }

// sealed method — blocks only this one method from being overridden
public class Circle : Shape
{
    public sealed override void Draw() { }
}
```

<br>

# Real-World Example — `string`

`string` in C# is a sealed class.

```csharp
public sealed class String { ... }
```

Microsoft sealed it because:
- `string` behaviour must be **predictable** — if you could inherit and override `Equals()` or `GetHashCode()`, dictionaries and comparisons would silently break
- Security — sealed classes can't be spoofed by a subclass pretending to be `string`

You see this pattern across the .NET base class library — `int`, `bool`, and all primitive wrappers are sealed.

<br>

# Memory Map — Sealed Stops the Chain

```text
                  Shape
                 (virtual Draw)
                     │
                     ▼
                  Circle
              (sealed override Draw)
               /              \
              ▼                ▼
       SpecialCircle       FancyCircle
      override Draw()     override Draw()
           ✗                    ✗
      compile error         compile error

Circle itself can still be inherited —
only Draw() is blocked from further override.
```

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
    public sealed override void Draw()
    {
        Console.WriteLine("Drawing a circle");
    }
}

public class SpecialCircle : Circle
{
    // ✓ Can add new methods
    public void Glow()
    {
        Console.WriteLine("Glowing circle");
    }

    // ✗ Cannot override Draw — it is sealed in Circle
    // public override void Draw() { }
}
```

```csharp
Shape s = new SpecialCircle();
s.Draw();   // Drawing a circle  ← Circle.Draw(), cannot be changed further
```

<br>

# When to Use `sealed`

```text
✓  The class has a complete, correct implementation
   that should not be altered by inheritance

✓  You want to prevent accidental misuse —
   someone inheriting a class and subtly breaking its contract

✓  Security-sensitive types — sealing prevents impersonation
   via subclasses

✓  Performance-critical paths — sealed methods allow the JIT
   compiler to skip virtual dispatch and inline the call directly

✗  Don't seal everything by default — it kills extensibility
   and makes testing harder (mocking requires inheritance)
```

<br>

# `sealed` + `abstract` — Not Allowed Together

`abstract` means "must be subclassed."
`sealed` means "cannot be subclassed."

They directly contradict each other.

```csharp
public abstract sealed class Shape { }   // ✗ Compile error
```

```text
'Shape': an abstract class cannot be sealed
```

<br>

# Best Practices

- Seal a class when you are **certain** it should never be a base class.
- Seal a method when you want to lock **one specific behaviour** while still allowing the class to be extended.
- Prefer sealing at the method level over sealing the entire class — gives more flexibility to consumers.
- Don't seal just for performance — the JIT already devirtualizes in many cases. Seal for correctness and intent.

<br>

# Summary

```text
virtual   →  override   →  sealed override   →  ✗ blocked
(open)        (changed)     (locked here)
```

- `sealed class` — no one can inherit this class
- `sealed method` — no one can override this method further, but the class can still be inherited
- Can only seal a method that is already overriding something
- `abstract sealed` is illegal — they contradict each other
- Real-world example: `string` is sealed in .NET

<br>

# Interview Questions

### What is a sealed class?

A class that cannot be used as a base class. Any attempt to inherit from it causes a compile error.

<br>

### What is a sealed method?

A method that overrides a virtual/abstract method but prevents any further class in the hierarchy from overriding it again.

<br>

### Can a sealed class have methods that can be overridden?

No — since a sealed class cannot be inherited, there is nothing to override in.

<br>

### Can you seal a method in a non-sealed class?

Yes. The class can still be inherited; only that specific method is locked from further override.

<br>

### Why is `string` sealed in C#?

To guarantee consistent, predictable behaviour. If `string` could be subclassed, its `Equals()`, `GetHashCode()`, and other contract methods could be silently overridden, breaking collections, comparisons, and security checks.

<br>

### What is the difference between `sealed` and `abstract`?

`abstract` forces subclassing. `sealed` prevents it. They cannot be combined.
