# 5. Extension Methods

Extension methods let you **add new methods to an existing class** — without modifying its source code, without inheriting from it.

The class doesn't know the method exists. You just call it as if it were always there.

```csharp
Student student = new Student("Yash", 16);

student.Greet();       // looks like a Student method
student.Welcome();     // but Student class never defined these
student.IsEvenRoll();  // they live in a separate static class
```

<br>

# The Three Rules

```text
1.  Must be in a static class
2.  Must be a static method
3.  First parameter uses  'this'  keyword — that's the type being extended
```

```csharp
public static class StudentExtension          // rule 1 — static class
{
    public static void Greet(this Student student)   // rule 2 + 3
    {
        Console.WriteLine($"Hello {student.Name}");
    }
}
```

The `this Student student` parameter is the secret — it tells the compiler:
*"attach this method to the `Student` type."*

You never pass that argument yourself. The object before the dot fills it automatically.


<br>
<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/393fc294-75b2-4bdf-93f0-04912cd56bb0" />
</div>
<br>

```csharp
student.Greet();
// compiler sees: StudentExtension.Greet(student)
// you write:     student.Greet()
```

<br>

## Example
```csharp
using System;

namespace CSharpAdvanced
{
    //====================================
    //1. Create a normal class
    //====================================
    public class Student
    {
        public string Name { get; set; }
        public int RollNo { get; set; }

        public Student(string name, int rollNo)
        {
            Name = name;
            RollNo = rollNo;
        }
    }

    //====================================
    //2. Create a static class
    //   (Extension methods must be inside
    //    a static class)
    //====================================
    public static class StudentExtension
    {
        //====================================
        //3. Create Extension Method
        //   - static method
        //   - first parameter uses 'this'
        //====================================
        public static void Greet(this Student student)
        {
            Console.WriteLine($"Hello {student.Name}");
        }

        public static void Welcome(this Student student)
        {
            Console.WriteLine($"Welcome {student.Name}");
        }

        public static bool IsEvenRoll(this Student student)
        {
            return student.RollNo % 2 == 0;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //====================================
            //4. Create Object
            //====================================
            Student student = new Student("Yash", 16);

            //====================================
            //5. Call Extension Methods
            //====================================

            student.Greet();

            student.Welcome();

            if (student.IsEvenRoll())
            {
                Console.WriteLine($"{student.Name} has a Even Roll Number");
            }
            else  
            {
                Console.WriteLine($"{student.Name} has a Odd Roll Number");
            }
            
        }
    }
}
```
### Full Breakdown

```csharp
public class Student
{
    public string Name   { get; set; }
    public int    RollNo { get; set; }

    public Student(string name, int rollNo)
    {
        Name   = name;
        RollNo = rollNo;
    }
}

public static class StudentExtension
{
    // adds Greet() to every Student object
    public static void Greet(this Student student)
    {
        Console.WriteLine($"Hello {student.Name}");
    }

    // adds Welcome() to every Student object
    public static void Welcome(this Student student)
    {
        Console.WriteLine($"Welcome {student.Name}");
    }

    // adds IsEvenRoll() — returns bool
    public static bool IsEvenRoll(this Student student)
    {
        return student.RollNo % 2 == 0;
    }
}
```

```csharp
Student student = new Student("Yash", 16);

student.Greet();       // Hello Yash
student.Welcome();     // Welcome Yash

if (student.IsEvenRoll())
    Console.WriteLine($"{student.Name} has an Even Roll Number");
else
    Console.WriteLine($"{student.Name} has an Odd Roll Number");
```

```text
Hello Yash
Welcome Yash
Yash has an Even Roll Number
```

<br>

# What Actually Happens at Compile Time

Extension methods are **syntactic sugar**. The compiler rewrites the call:

```csharp
// what you write
student.Greet();

// what the compiler actually calls
StudentExtension.Greet(student);
```

They aren't real instance methods. The class is unchanged at runtime.
The compiler just makes them *look* like instance methods at the call site.

```text
student.Greet()
   │
   └──► compiler rewrites to ──► StudentExtension.Greet(student)
                                                         ▲
                                          fills the  'this'  parameter
```

<br>

# Extending Types You Don't Own

The real power — extend classes from .NET itself, third-party libraries, or sealed classes.

```csharp
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string s)
    {
        return string.IsNullOrEmpty(s);
    }

    public static string Shorten(this string s, int maxLength)
    {
        return s.Length <= maxLength ? s : s[..maxLength] + "...";
    }

    public static string Capitalise(this string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s[1..].ToLower();
    }
}
```

```csharp
string name = "yashodeep gaikwad";

Console.WriteLine(name.Capitalise());       // Yashodeep gaikwad
Console.WriteLine(name.Shorten(10));        // yashodeep...
Console.WriteLine("".IsNullOrEmpty());      // True
```

`string` is sealed — you can't inherit from it. Extension methods bypass that entirely.

<br>

# Extension Methods on Interfaces

You can extend an **interface** — every class implementing it instantly gets the method.

```csharp
public interface IAnimal
{
    string Name { get; }
    string Sound { get; }
}

public static class AnimalExtensions
{
    public static void Speak(this IAnimal animal)
    {
        Console.WriteLine($"{animal.Name} says {animal.Sound}!");
    }
}

public class Dog : IAnimal
{
    public string Name  => "Dog";
    public string Sound => "Woof";
}

public class Cat : IAnimal
{
    public string Name  => "Cat";
    public string Sound => "Meow";
}
```

```csharp
new Dog().Speak();   // Dog says Woof!
new Cat().Speak();   // Cat says Meow!
```

One extension method — works for every `IAnimal` automatically.
This is exactly how LINQ works — it extends `IEnumerable<T>`.

<br>

# How LINQ Uses Extension Methods

<br>
<div align = "center">

<img width="500" alt="image" src="https://github.com/user-attachments/assets/e9483851-ee43-4e10-ad24-95c1f97d3712" />

</div>
<br>

Every LINQ method you've seen is an extension method on `IEnumerable<T>`:

```csharp
// These are all extension methods defined in System.Linq.Enumerable
numbers.Where(n => n > 0)
       .Select(n => n * 2)
       .OrderBy(n => n)
       .ToList();
```

`Where`, `Select`, `OrderBy`, `ToList` — none of these are defined on `List<int>`.
They're extension methods that attach to any `IEnumerable<T>`.

```text
List<int>
    │
    └── implements IEnumerable<int>
                        │
                        └── Where(this IEnumerable<T>, Func<T, bool>)   ← extension
                        └── Select(this IEnumerable<T>, Func<T, R>)     ← extension
                        └── OrderBy(this IEnumerable<T>, ...)           ← extension
```

<br>

# Naming Convention

```text
Class being extended: Student
Extension class name: StudentExtension  or  StudentExtensions

Class being extended: string
Extension class name: StringExtensions

Interface extended:   IEnumerable<T>
Extension class name: EnumerableExtensions  (that's the real .NET name)
```

<br>

# When to Use Extension Methods

```text
✓  Adding utility methods to a class you don't control  (string, int, third-party)
✓  Adding methods to a sealed class                     (string, DateTime)
✓  Adding shared behaviour to an interface              (IAnimal, IEnumerable<T>)
✓  Keeping the original class clean                     (domain logic separate from helpers)

✗  Don't use when you own the class and can just add the method directly
✗  Don't use to work around bad design — fix the design instead
```

<br>

# Summary

```text
public static class StudentExtension
{
    public static void Greet(this Student s)    ← extends Student
    {
        Console.WriteLine($"Hello {s.Name}");
    }
}

student.Greet()
  │
  └──► compiler: StudentExtension.Greet(student)
```

- Extension method = static method in a static class with `this` on the first parameter
- Called like an instance method — the compiler rewrites it behind the scenes
- Lets you add methods to any type: classes you own, sealed classes, third-party, interfaces
- `string`, `int`, `DateTime` — all extendable even though you can't modify them
- LINQ is the largest real-world example — all built on `IEnumerable<T>` extensions

<br>

# Interview Questions

### What is an extension method?

A static method in a static class that uses `this` on its first parameter to attach itself to another type, callable as if it were an instance method on that type.

### Can you extend a sealed class?

Yes — extension methods don't require inheritance. `string` is sealed and has countless extension methods written for it.

### What does the `this` keyword do in an extension method?

It marks the parameter as the type being extended. The compiler uses it to resolve which type the method attaches to.

### What is the difference between an extension method and an instance method?

An instance method is defined inside the class. An extension method is defined outside but appears as if it's part of the class at the call site. The class itself is unmodified.

### How does LINQ relate to extension methods?

All LINQ methods (`Where`, `Select`, `OrderBy`, etc.) are extension methods defined on `IEnumerable<T>` in `System.Linq.Enumerable`.
