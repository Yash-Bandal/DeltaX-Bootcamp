# 3.1 Access Modifiers in Inheritance

Access modifiers control the visibility and accessibility of class members.

When inheritance is introduced, access modifiers become even more important because they determine **which members a derived class can access from its base class**.

<br>

# Why are Access Modifiers Important?

Access modifiers help us:

* Protect internal implementation details.
* Enforce encapsulation.
* Prevent accidental misuse.
* Expose only what is necessary.
* Build maintainable and secure code.

Good object-oriented design is largely about controlling access to data and behavior.

<br>

# Access Modifiers Overview

C# provides the following access modifiers:

* `public`
* `private`
* `protected`
* `internal`
* `protected internal`

<br>

# public

A `public` member is accessible from anywhere.

Example

```csharp
class Person
{
    public string Name;
}
```

Usage

```csharp
Person person = new Person();

person.Name = "Yash";
```

Since the member is public, any class can access it.

<br>

# private

A `private` member is accessible only within the class where it is declared.

Example

```csharp
class Person
{
    private int age;
}
```

This is not allowed:

```csharp
Person person = new Person();

person.age = 22; // Compile-time error
```

Even derived classes cannot access private members directly.

```csharp
class Student : Person
{
    public void Test()
    {
        age = 20; // Error
    }
}
```

<br>

# protected

A `protected` member is accessible:

* Inside the declaring class.
* Inside derived classes.

Example

```csharp
class Person
{
    protected string Name;
}

class Student : Person
{
    public void SetName()
    {
        Name = "Yash";
    }
}
```

The derived class can directly access the protected member.

<br>

# internal

An `internal` member is accessible anywhere within the same assembly (project).

Example

```csharp
internal class Logger
{
}
```

All classes in the same project can access it.

Classes outside the assembly cannot.

<br>

# protected internal

A `protected internal` member is accessible:

* Anywhere within the same assembly.
* From derived classes outside the assembly.

Example

```csharp
class Person
{
    protected internal string Name;
}
```

This is a combination of `protected` and `internal`.

<br>

# Access Modifier Comparison

| Modifier           | Same Class | Derived Class | Same Assembly | Outside Assembly           |
| ------------------ | ---------- | ------------- | ------------- | -------------------------- |
| public             | Yes        | Yes           | Yes           | Yes                        |
| private            | Yes        | No            | No            | No                         |
| protected          | Yes        | Yes           | No            | No                         |
| internal           | Yes        | Yes           | Yes           | No                         |
| protected internal | Yes        | Yes           | Yes           | Yes (derived classes only) |

<br>

# Access Modifiers and Inheritance

Consider the following:

```csharp
class Person
{
    public string PublicField;
    private string PrivateField;
    protected string ProtectedField;
}

class Student : Person
{
    public void Test()
    {
        PublicField = "A";
        ProtectedField = "B";

        // PrivateField = "C"; // Error
    }
}
```

Accessible:

```text
PublicField
ProtectedField
```

Not Accessible:

```text
PrivateField
```

<br>

# Why Protected Breaks Encapsulation

At first glance, `protected` seems useful because it allows derived classes to reuse data.

However, it weakens encapsulation.

Example

```csharp
class Person
{
    protected int Age;
}
```

Now every derived class can directly modify `Age`.

```csharp
class Student : Person
{
    public void UpdateAge()
    {
        Age = -100;
    }
}
```

The base class loses control over its own data.

This breaks one of the main goals of encapsulation:

> A class should control its own state.

<br>

# Why Protected Should Be Avoided

Many developers overuse `protected`.

Problems:

* Exposes implementation details to derived classes.
* Creates tighter coupling between base and derived classes.
* Makes future changes harder.
* Reduces encapsulation.

Instead of exposing fields as protected:

```csharp
protected int Age;
```

Prefer:

```csharp
private int age;

public void SetAge(int value)
{
    if (value > 0)
        age = value;
}
```

Now the class maintains full control over its state.

<br>

# When is Protected Acceptable?

Protected can be useful when:

* Derived classes genuinely need access.
* Exposing behavior rather than raw data.
* Building frameworks or extensible libraries.

Even then, prefer protected methods over protected fields.

Better:

```csharp
protected void Validate()
{
}
```

Avoid:

```csharp
protected int age;
```

<br>

# Best Practices

* Default to `private`.
* Expose only what is necessary.
* Prefer methods and properties over protected fields.
* Avoid exposing internal implementation details.
* Use `protected` sparingly.
* Preserve encapsulation whenever possible.

<br>

# Common Mistakes

## Making everything public

```csharp
public string Name;
public int Age;
```

This exposes internal state unnecessarily.

<br>

## Using protected fields

```csharp
protected decimal Salary;
```

Derived classes can modify the value without validation.

Prefer private fields with controlled access.

<br>

## Confusing internal with public

`internal` means accessible within the same assembly, not everywhere.

<br>

# Interview Questions

### What is the purpose of access modifiers?

Access modifiers control the visibility and accessibility of classes and class members.

<br>

### Which access modifier is most restrictive?

`private`

<br>

### Can a derived class access private members of its base class?

No.

<br>

### What does protected mean?

A protected member can be accessed within its declaring class and by derived classes.

<br>

### Why does protected weaken encapsulation?

Because derived classes gain direct access to implementation details and internal state.

<br>

### Which access modifier should be used by default?

`private`

<br>

# Summary

* Access modifiers control member visibility.
* `public` exposes members everywhere.
* `private` restricts access to the declaring class.
* `protected` allows access in derived classes.
* `internal` limits access to the same assembly.
* `protected internal` combines protected and internal behavior.
* Overusing `protected` weakens encapsulation.
* Prefer private fields and controlled access through methods or properties.
* Start with `private` and increase visibility only when necessary.
