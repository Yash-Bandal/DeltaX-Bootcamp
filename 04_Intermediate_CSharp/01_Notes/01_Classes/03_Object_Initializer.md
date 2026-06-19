
# 1.3 Object Initializers

Object Initializers provide a concise way to create an object and assign values to its fields or properties in a single statement.

They improve readability and eliminate the need for multiple assignment statements after object creation.

<div align = "center">
<img width="600"  alt="image" src="https://github.com/user-attachments/assets/58e43534-0ed0-44d7-8daa-7fbe83369202" />
<p>What is Object initializer</p>
</div>


<br>

# Why Use Object Initializers?

Without an object initializer:

```csharp
Person person = new Person();

person.Name = "Yash";
person.Age = 22;
```

Using an object initializer:


```csharp
Person person = new Person
{
    Name = "Yash",
    Age = 22
};
```

Both produce the same result, but the second approach is cleaner and easier to read.

<br>

# Syntax

```csharp
ClassName objectName = new ClassName
{
    Property1 = value1,
    Property2 = value2
};
```

<br>

# Example

```csharp
class Person
{
    public string Name;
    public int Age;
}

Person person = new Person
{
    Name = "Yash",
    Age = 22
};

Console.WriteLine($"{person.Name} - {person.Age}");
```

Output

```
Yash - 22
```

<br>


# How It Works


When using an object initializer:

1. The object is created using the constructor.
2. The constructor finishes execution.
3. The specified fields or properties are assigned their values.

This means constructors always execute **before** the object initializer assigns values.


<div align = "center">
  <img width="400"  alt="image" src="https://github.com/user-attachments/assets/0724d2cf-9d1c-460a-b707-70ec8a2ee081" />
<p>Suppose this is the class that needs Object</p>

<img width="600"  alt="image" src="https://github.com/user-attachments/assets/538443eb-e348-4654-90a7-149d7758a966" />
<p>Normal Object Initialization (Big and High maintainance)</p>

<img width="600" alt="image" src="https://github.com/user-attachments/assets/d98ca88e-81c4-4373-ade0-4cd68cc1ae2c" />
<p>With Object Initializer</p>

</div>

<br>

# Object Initializers with Constructors

You can combine constructors and object initializers.

```csharp
class Person
{
    public string Name;
    public int Age;

    public Person(string name)
    {
        Name = name;
    }
}

Person person = new Person("Yash")
{
    Age = 22
};
```

Execution order:

1. Constructor sets `Name`.
2. Object initializer sets `Age`.

<br>

# Benefits

* Cleaner and more readable code.
* Initializes objects in a single statement.
* Reduces repetitive assignment code.
* Works well when many fields or properties need to be set.

<br>

# Best Practices

* Use object initializers when assigning multiple values immediately after object creation.
* Prefer object initializers for readability, especially when working with models or DTOs.
* Avoid using them if complex initialization logic is required—use constructors instead.

<br>

# Common Mistakes

## Forgetting the braces

Incorrect

```csharp
Person person = new Person();
Name = "Yash";
```

Correct

```csharp
Person person = new Person
{
    Name = "Yash"
};
```

<br>

## Using object initializers instead of constructors for required data

If an object cannot exist without certain values, initialize them through a constructor rather than relying on an object initializer.

<br>

## Interview Questions

**What is an object initializer?**

An object initializer is a C# feature that allows fields or properties to be assigned values during object creation without calling separate assignment statements.

<br>

**Does an object initializer replace constructors?**

No. The constructor always executes first, followed by the object initializer.

<br>

**When should you use object initializers?**

When you want to initialize multiple fields or properties in a clean and readable way immediately after creating an object.



<br>

---
---

<br>

We need to aboid multiple constructors 
