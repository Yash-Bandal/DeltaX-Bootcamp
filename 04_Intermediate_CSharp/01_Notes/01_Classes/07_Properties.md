# 1.7 Properties

Properties provide a controlled way to access the data of an object. They are one of the most commonly used features in C# and are the preferred way to expose class data.

Instead of exposing fields directly, we expose **properties**.

<br>

> [!Tip]
> Q What is it?\
> A class member that encapsulates a getter/setter for accessing a field
>
> Q Why do need a property?\
> To crate getter/setter with less code

<br>

# What is a Property?

A **property** is a class member that provides controlled access to a private field using **get** and **set** accessors.

* `get` returns the value.
* `set` assigns a value.

Properties combine the simplicity of fields with the safety of methods.

<br>

# Why Do We Need Properties?

Consider the following class:

```csharp
class Employee
{
    public int Age;
}
```

Anyone can assign an invalid value.

```csharp
employee.Age = -10;
```

To prevent this, make the field private and expose it through a property.

```csharp
class Employee
{
    private int age;

    public int Age
    {
        get { return age; }
        set
        {
            if (value >= 0)
                age = value;
        }
    }
}
```

Now invalid values cannot be assigned.

<br>

# Property Syntax

```csharp
private dataType fieldName;

public dataType PropertyName
{
    get
    {
        return fieldName;
    }

    set
    {
        fieldName = value;
    }
}
```

Example

```csharp
class Person
{
    private string name;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
}
```

Usage

```csharp
Person person = new Person();

person.Name = "Yash";

Console.WriteLine(person.Name);
```

<br>

# Understanding `get` and `set`

The `get` accessor executes whenever the property's value is read.

```csharp
string name = person.Name;
```

The `set` accessor executes whenever a value is assigned.

```csharp
person.Name = "Yash";
```

Inside the `set` accessor, the keyword `value` represents the value being assigned.

```csharp
set
{
    name = value;
}
```

<br>

# Automatic Properties (Auto-Implemented Properties)

If no additional logic is required, C# can automatically create the private backing field.

Instead of writing

```csharp
private string name;

public string Name
{
    get { return name; }
    set { name = value; }
}
```

you can simply write

```csharp
public string Name { get; set; }
```

This is called an **Auto-Implemented Property**.

It is the most commonly used type of property in modern C#.

<br>

# Read-Only Properties

A property with only a `get` accessor cannot be modified outside the class.

```csharp
public string Company
{
    get;
}
```

or

```csharp
public string Company { get; }
```

The value can be assigned inside the constructor.

```csharp
class Employee
{
    public string Company { get; }

    public Employee()
    {
        Company = "ABC Ltd";
    }
}
```

<br>

# Properties with Validation

One of the biggest advantages of properties is validation.

```csharp
class Employee
{
    private int age;

    public int Age
    {
        get
        {
            return age;
        }

        set
        {
            if (value >= 18)
                age = value;
        }
    }
}
```

Now only valid ages are accepted.

<br>

# Field vs Property

| Field                             | Property                       |
| --------------------------------- | ------------------------------ |
| Stores data                       | Controls access to data        |
| No validation                     | Can perform validation         |
| Usually private                   | Usually public                 |
| Simple variable                   | Uses `get` and `set` accessors |
| Not recommended for public access | Preferred for exposing data    |

<br>

# Expression-Bodied Property

For simple read-only properties, C# provides a shorter syntax.

```csharp
public string FullName => $"{FirstName} {LastName}";
```

This is equivalent to

```csharp
public string FullName
{
    get
    {
        return $"{FirstName} {LastName}";
    }
}
```

<br>

# Best Practices

* Keep fields private.
* Expose data through properties.
* Use auto-properties whenever validation is not required.
* Use custom properties when validation or additional logic is needed.
* Give properties meaningful PascalCase names.

<br>

# Common Mistakes

## Making fields public instead of using properties

Incorrect

```csharp
public string Name;
```

Better

```csharp
public string Name { get; set; }
```

<br>

## Forgetting validation

```csharp
employee.Age = -20;
```

Use properties to validate incoming values.

<br>

## Writing unnecessary backing fields

Don't write

```csharp
private string name;

public string Name
{
    get { return name; }
    set { name = value; }
}
```

unless additional logic is needed.

Instead, use

```csharp
public string Name { get; set; }
```

<br>

# Interview Questions

### What is a property?

A property is a class member that provides controlled access to a private field using `get` and `set` accessors.

<br>

### Why are properties preferred over public fields?

Properties support validation, encapsulation, and future flexibility without changing the class interface.

<br>

### What is an auto-implemented property?

A property where the compiler automatically creates the private backing field.

Example

```csharp
public string Name { get; set; }
```

<br>

### What is the purpose of the `value` keyword?

Inside the `set` accessor, `value` represents the value being assigned to the property.

<br>

### When should you use a custom property instead of an auto-property?

When you need validation, calculations, logging, or any additional logic while getting or setting a value.

<br>

# Summary

* Properties provide controlled access to object data.
* They use `get` and `set` accessors.
* Keep fields private and expose properties instead.
* Use auto-properties for simple data storage.
* Use custom properties when validation or additional logic is required.
* Properties are an essential part of implementing encapsulation in C#.
