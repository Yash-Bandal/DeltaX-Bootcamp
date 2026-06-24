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

**Example:**

**Changing Object Reference type**
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

            // Downcasting  
            Child child2 = (Child)parent1;
            child2.IntroduceParent();
            child2.IntroduceChild();

            // No error accesing Child method

        }
    }
}
```

**Creating Parent Object()**
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

# Safe Downcasting with "is"

The `is` operator checks compatibility before casting.

<br>
<div align = "center">
   <img width="400" alt="image" src="https://github.com/user-attachments/assets/62230c92-732b-4ae1-9992-6c405f57cc50" />
</div>
<br>

```csharp id="pqr123"
Person person = new Student();

if (person is Student)
{
    Student student = (Student)person;
}
```

This prevents runtime exceptions.

<br>

```csharp
using System;

namespace CSharpIntermediate
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
            Console.WriteLine("I am a Child");
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Child child1 = new Child();

            // Upcasting
            Parent parent1 = child1;

            parent1.IntroduceParent();

            Console.WriteLine();

            // Safe Downcasting using 'is'
            if (parent1 is Child child2)
            {
                child2.IntroduceParent();
                child2.IntroduceChild();
            }
        }
    }
}
```
```
I am a Parent

I am a Parent
I am a Child
```

> [!Important]
> It asks "Is parent1 actually a Child?"\
> If yes, create child2.

<br>

# Safe Downcasting with "as"

<br>
<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/cd1019bf-ef51-481d-897e-197f9ff0c7bd" />
</div>
<br>

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

```csharp
using System;

namespace CSharpIntermediate
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
            Console.WriteLine("I am a Child");
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Child child1 = new Child();

            // Upcasting
            Parent parent1 = child1;

            // Safe Downcasting using 'as'
            Child child2 = parent1 as Child;

            if (child2 != null)
            {
                child2.IntroduceParent();
                child2.IntroduceChild();
            }
        }
    }
}
```
> [!Important]
> "Try converting parent1 to Child."\
> If fail `child2 == null`

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
