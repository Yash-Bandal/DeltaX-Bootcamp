# 3.3 Upcasting and Downcasting

Upcasting and Downcasting are techniques used to convert objects between base and derived types.

They are fundamental concepts in inheritance and are heavily used in polymorphism.

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/1ee7d860-5f59-4280-b37b-2e4e85a7f598" />
</div>
<br>

# Understanding the Inheritance Hierarchy

Consider the following classes:

```csharp id="fgh321"
class Person
{
}

class Student : Person
{
}
```

Relationship:

```text id="klm123"
Person
   ▲
   │
Student
```

A `Student` **is a** `Person`.

This relationship allows casting between these types.

<br>

# What is Upcasting?

**Upcasting** is converting a derived class object into a base class reference.

Syntax
```csharp
ChildClass childObject = new ChildClass();
ParentClass parentObject = childObject;
```

Example

```csharp id="abc123"
Student student = new Student();

Person person = student;
```

or

```csharp id="def456"
Person person = (Person)student;
```

Both are valid.

The explicit cast is unnecessary because upcasting happens automatically.

<br>

# Why is Upcasting Safe?

Every `Student` object is also a `Person`.

Therefore:

```text id="ghi789"
Student → Person
```

is always safe.

The runtime already knows that a Student contains all Person members.

<br>

# Example

```csharp id="jkl012"
class Person
{
    public void Walk()
    {
        Console.WriteLine("Walking");
    }
}

class Student : Person
{
}
```

Usage

```csharp id="mno345"
Student student = new Student();

Person person = student;

person.Walk();
```

Output

```text id="pqr678"
Walking
```

<br>

# What Happens After Upcasting?

After upcasting:

```csharp id="stu901"
Student student = new Student();

Person person = student;
```

The object is still a `Student`.

Only the reference type changes.

Memory representation:

```text id="vwx234"
Person Reference
        │
        ▼
     Student Object
```

The actual object remains unchanged.

<br>

# What Members Are Accessible?

```csharp id="yz1234"
Student student = new Student();

Person person = student;
```

Only members available in `Person` can be accessed through the `person` reference.

Example

```csharp id="abc567"
class Student : Person
{
    public void Study()
    {
    }
}
```

```csharp id="def890"
person.Study(); // Error
```

Even though the object is a Student, the reference type is Person.

<br>

# What is Downcasting?

**Downcasting** converts a base class reference back to a derived class reference.

Example

```csharp id="ghi123"
Person person = new Student();

Student student = (Student)person;
```

Unlike upcasting, downcasting requires an explicit cast.

<br>

# Why is Downcasting Risky?

Not every Person is a Student.

Example

```csharp id="jkl456"
Person person = new Person();

Student student = (Student)person;
```

Runtime error:

```text id="mno789"
InvalidCastException
```

Because the object is actually a Person, not a Student.

<br>

# Safe Downcasting with "is"

The `is` operator checks compatibility before casting.

```csharp id="pqr123"
Person person = new Student();

if (person is Student)
{
    Student student = (Student)person;
}
```

This prevents runtime exceptions.

<br>

# Safe Downcasting with "as"

The `as` operator attempts a cast and returns `null` if it fails.

```csharp id="stu456"
Person person = new Student();

Student student = person as Student;
```

If conversion fails:

```text id="vwx789"
student == null
```

No exception is thrown.

<br>

# "is" vs "as"

## Using is

```csharp id="yz0123"
if (person is Student)
{
}
```

Returns:

```text id="abc234"
true / false
```

<br>

## Using as

```csharp id="def567"
Student student = person as Student;
```

Returns:

```text id="ghi890"
Student object or null
```

<br>

# Real-World Example

```csharp id="jkl901"
ArrayList list = new ArrayList();

list.Add(1);
list.Add("Hello");
list.Add(new Student());
```

When retrieving items:

```csharp id="mno234"
Student student = (Student)list[2];
```

A downcast is required because `ArrayList` stores objects as `object`.

<br>

# Upcasting and Polymorphism

Upcasting is heavily used in polymorphism.

Example

```csharp id="pqr567"
Person person = new Student();
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

```csharp id="stu890"
Person person = new Student();
```

The object is still a Student.

Only the reference type changed.

<br>

## Unsafe Downcasting

```csharp id="vwx123"
Person person = new Person();

Student student = (Student)person;
```

Results in `InvalidCastException`.

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
