# 3.3 Upcasting and Downcasting

Upcasting is the conversion of a derived (child) class reference to a base (parent) class reference, 

while downcasting converts a base class reference back to a derived class reference.

These operations change the type of the `reference (✓)` used to access an object, but they never modify the underlying runtime type of the `object (X)`.

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/1ee7d860-5f59-4280-b37b-2e4e85a7f598" />
</div>
<br>

# Understanding the Inheritance Hierarchy

Consider the following classes:

```csharp
public class Parent
{
    public void IntroduceParent()
    {
        Console.WriteLine("I am a Parent");
    }
}

public class Child : Parent
{
    public void IntroduceChild()
    {
        Console.WriteLine("I am a Child");
    }
}
```

Relationship:

```text
Parent
   ▲
   │
Child
```

A `Child` **is a** `Parent`.

This relationship allows casting between these types.

<br>

# What is Upcasting?

**Upcasting** is converting a derived class object into a base class reference.

Syntax
```csharp
Child child = new Child();
Parent parent = child;
```

The explicit cast is unnecessary because upcasting happens automatically.

```csharp
Parent parent = (Parent)child; // also valid, but unnecessary
```

<br>

**Example : Changing Object Reference type**
```csharp
namespace CSharpdoubleermediate
{
    public class Parent
    {
        public void IntroduceParent()
        {
            Console.WriteLine("I am a Parent");
        }
    }

    public class Child : Parent
    {
        public void IntroduceChild()
        {
            Console.WriteLine("I am a child");
        }

        public void ShowParent()
        {

            IntroduceParent();
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            // Child object
            Child child1 = new Child();

            // Upcasting
            Parent parent1 = child1; //We just change reference

            //Parent parent1 = new Child(); 

            /*
             Here we are NOT creating a parent object with new Parent()
            SInce we are using Inheritance, child object already contains parent members

            child1 ───────┐
                          │
                          ▼
                +--------------------+
                | Child Object       |
                +-------------------+
                | Parent Part       |
                | IntroduceParent() |
                | Child Part        |
                | IntroduceChild()  |
                +-------------------+
                          ▲
                          │
            parent1 ──────┘

             Thus object created for parent that needs IntroPar() , from Child object, already has the inherited
             method, so no need of creating another seperate parent object, instead use the one from child itself
     
             */


            child1.IntroduceChild();
            child1.IntroduceParent();

            parent1.IntroduceParent();
            //parent1.IntroduceChild(); //error because parent cant access child

            // Downcasting  - Allow child2 to access child objects from parent1
            Child child2 = (Child)parent1;
            child2.IntroduceParent();
            child2.IntroduceChild();

            // No error accesing Child method
            parent1.IntroduceParent();

        }
    }
}
```


<br>

# Why is Upcasting Safe?

> [!Note]
> Upcasting simply simulates creation of seperate `parent` object, without creating it seperately

Every `Child` object is also a `Parent`.

Therefore:

```text
Child → Parent
```

is always safe.

The runtime already knows that a `Child` contains all `Parent` members.

<br>

# What Happens After Upcasting?

```csharp
Child child1 = new Child();

// Upcasting
Parent parent1 = child1; // We just change the reference
```

The object is still a `Child`.

Only the reference type changes.

```text
child1 ───────┐
              │
              ▼
    +--------------------+
    | Child Object       |
    +--------------------+
    | Parent Part        |
    | IntroduceParent()  |
    | Child Part         |
    | IntroduceChild()   |
    +--------------------+
              ▲
              │
parent1 ──────┘
```

> The actual object remains unchanged. `parent1` and `child1` both point to the same `Child` object.

<br>

# What Members Are Accessible?

After upcasting, only members available in `Parent` can be accessed through the `parent1` reference.

```csharp
Child child1 = new Child();
Parent parent1 = child1;

parent1.IntroduceParent(); // ✓ OK
parent1.IntroduceChild();  // ✗ Error — reference type is Parent
```

Even though the object is a `Child`, the reference type is `Parent`.

<br>

# What is Downcasting?

**Downcasting** converts a base class reference back to a derived class reference.

Unlike upcasting, downcasting requires an **explicit cast**.

```csharp
Child child1 = new Child();

// Upcasting
Parent parent1 = child1;

// Downcasting
Child child2 = (Child)parent1;

child2.IntroduceParent(); // ✓
child2.IntroduceChild();  // ✓ — Child members accessible again
```

<br>

# Why is Downcasting Risky?

Not every `Parent` is a `Child`.

If `parent1` points to an actual `Parent` object (not a `Child`), the cast fails:

```text
child1 ───────┐
              ▼
    +----------------+                  +----------------+
    | Child Object   |                  | Parent Object  |
    +----------------+                  +----------------+
    | Parent Part    |                  | Parent Part    |
    | Child Part     |                  +----------------+
    +----------------+
                                                  ▲
                                                  │
                                        parent1 ──┘
```

```csharp
Parent parent1 = new Parent(); // points to an actual Parent object

Child child2 = (Child)parent1; // Runtime error!
```

```text
Unhandled exception. System.InvalidCastException
```

Because the object is actually a `Parent`, not a `Child`.

<br>

**Example : Creating Parent Object()**
```csharp
namespace CSharpdoubleermediate
{
    public class Parent
    {
        public void IntroduceParent()
        {
            Console.WriteLine("I am a Parent");
        }
    }

    public class Child : Parent
    {
        public void IntroduceChild()
        {
            Console.WriteLine("I am a child");
        }

        public void ShowParent()
        {

            IntroduceParent();
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            // Child object
            Child child1 = new Child();
            Parent parent1 = new Parent();
            //Parent parent1 = new Child(); 


            child1.IntroduceChild();
            child1.IntroduceParent();

            parent1.IntroduceParent();

            /*
             Here we are creating a parent object with new Parent()
            SInce we are using Inheritance, child object already contains parent members


            child1 ───────┐
                          │
                          ▼
                +----------------+                  +----------------+ 
                | Child Object   |                  | Parent Object  |
                +----------------+                  +----------------+
                | Parent Part    |                  | Parent Part    |
                | Child Part     |                  +----------------+
                +----------------+
                                                              ▲
                                                              │
                                                parent1 ──────┘


             */

            // Downcasting
            Child child2 = (Child)parent1;

            // Not possible because parent1 points to a Parent object,
            // not a Child object.

            child2.IntroduceParent(); // error
            child2.IntroduceChild();

            // Unhandled exception. System.InvalidCastException:


        }
    }
}

```

<br>

# Safe Downcasting with `is`

The `is` operator checks compatibility before casting.

<br>
<div align = "center">
   <img width="400" alt="image" src="https://github.com/user-attachments/assets/62230c92-732b-4ae1-9992-6c405f57cc50" />
</div>
<br>

```csharp
Child child1 = new Child();
Parent parent1 = child1;

parent1.IntroduceParent();

// Safe Downcasting using 'is'
if (parent1 is Child child2)
{
    child2.IntroduceParent();
    child2.IntroduceChild();
}

// Check and only then proceed, to avoid exceptions
```

```
I am a Parent

I am a Parent
I am a Child
```

> [!Important]
> It asks "Is `parent1` actually a `Child`?"\
> If yes, create `child2`.

<br>

# Safe Downcasting with `as`

<br>
<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/cd1019bf-ef51-481d-897e-197f9ff0c7bd" />
</div>
<br>

The `as` operator attempts a cast and returns `null` if it fails.

```csharp
Child child1 = new Child();
Parent parent1 = child1;

// Safe Downcasting using 'as'
Child child2 = parent1 as Child;

if (child2 != null)
{
    child2.IntroduceParent();
    child2.IntroduceChild();
}
```

If the conversion fails, `child2 == null`. No exception is thrown.

> [!Important]
> "Try converting `parent1` to `Child`."\
> If fail → `child2 == null`

<br>

# `is` vs `as`

## Using `is`

```csharp
if (parent1 is Child child2)
{
}
```

Returns:

```text
true / false
```

<br>

## Using `as`

```csharp
Child child2 = parent1 as Child;
```

Returns:

```text
Child object or null
```

<br>

# Real-World Example

```csharp
ArrayList list = new ArrayList();

list.Add(new Parent());
list.Add(new Child());
```

When retrieving items:

```csharp
Child child = (Child)list[1];
```

A downcast is required because `ArrayList` stores objects as `object`.

<br>

# Upcasting and Polymorphism

Upcasting is heavily used in polymorphism.

```csharp
Parent p = new Child();
```

A method can work with many derived types through a base class reference.

This is one of the key benefits of inheritance.

<br>

# Best Practices

* Prefer upcasting whenever possible.
* Avoid unnecessary downcasting.
* Use `is` or `as` when downcasting.
* Program against base types and abstractions.
* Use polymorphism instead of repeatedly checking types.

<br>

# Common Mistakes

## Assuming the reference type changes the object type

```csharp
Parent parent1 = new Child();
```

The object is still a `Child`. Only the reference type changed.

<br>

## Unsafe Downcasting

```csharp
Parent parent1 = new Parent();

Child child2 = (Child)parent1; // InvalidCastException
```

<br>

## Overusing Downcasting

Frequent downcasting is often a sign that the design could be improved using polymorphism.

<br>

# Interview Questions

### What is upcasting?

Upcasting is converting a derived class object into a base class reference.

<br>

### Is upcasting implicit or explicit?

Usually implicit because it is always safe.

<br>

### What is downcasting?

Downcasting is converting a base class reference back to a derived class reference.

<br>

### Why is downcasting dangerous?

Because the underlying object may not actually be of the target derived type.

<br>

### What is the difference between `is` and `as`?

| is                     | as                     |
| ---------------------- | ---------------------- |
| Returns true or false  | Returns object or null |
| Used for checking type | Used for safe casting  |

<br>

### Which is safer: upcasting or downcasting?

Upcasting, because every derived object is also a base object.

<br>

# Summary

* Upcasting converts a derived object to a base class reference.
* Upcasting is safe and usually implicit.
* Downcasting converts a base reference back to a derived reference.
* Downcasting requires an explicit cast.
* Unsafe downcasting can cause `InvalidCastException`.
* Use `is` and `as` for safer downcasting.
* Upcasting is a key concept behind polymorphism and object-oriented design.
