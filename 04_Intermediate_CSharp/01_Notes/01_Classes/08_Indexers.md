# 1.8 Indexers

An **Indexer** allows an object to be accessed like an array using square brackets (`[]`).

Instead of calling methods like `Get()` and `Set()`, an indexer provides a cleaner and more intuitive syntax.

<br>

# What is an Indexer?

An **Indexer** is a special property that enables objects to be indexed like arrays or collections.

Without an indexer:

```csharp
customer.SetPhoneNumber(0, "9876543210");

string number = customer.GetPhoneNumber(0);
```

With an indexer:

```csharp
customer[0] = "9876543210";

string number = customer[0];
```

This syntax is simpler and easier to read.

<br>

# Why Do We Need Indexers?

Indexers are useful when a class internally stores a collection of data.

Examples:

* Shopping Cart
* Student Marks
* Phone Book
* Library Books
* Custom Collections

They allow users of the class to access elements naturally using an index.

<br>

# Indexer Syntax

```csharp
public dataType this[indexType index]
{
    get
    {
        // Return value
    }

    set
    {
        // Assign value
    }
}
```

* `this` represents the current object.
* The index can be an `int`, `string`, or any valid type.

<br>

# Example

```csharp
class Person
{
    private string[] phoneNumbers = new string[3];

    public string this[int index]
    {
        get
        {
            return phoneNumbers[index];
        }

        set
        {
            phoneNumbers[index] = value;
        }
    }
}
```

Usage

```csharp
Person person = new Person();

person[0] = "9876543210";
person[1] = "9123456780";

Console.WriteLine(person[0]);
```

Output

```
9876543210
```

<br>

# How an Indexer Works

When writing

```csharp
person[0] = "9876543210";
```

the compiler internally calls the **set** accessor.

When reading

```csharp
string number = person[0];
```

the compiler calls the **get** accessor.

Just like properties, indexers use `get` and `set`.

<br>

# Indexers with Different Data Types

An indexer is **not limited to integers**.

Example using a string key:

```csharp
public string this[string key]
{
    get
    {
        return dictionary[key];
    }

    set
    {
        dictionary[key] = value;
    }
}
```

This allows access like:

```csharp
settings["Theme"] = "Dark";
```

<br>

# Property vs Indexer

| Property                  | Indexer                           |
| ------------------------- | --------------------------------- |
| Accessed by name          | Accessed using `[]`               |
| Has an identifier         | Uses the `this` keyword           |
| Represents a single value | Represents a collection of values |
| Example: `person.Name`    | Example: `person[0]`              |

<br>

# Best Practices

* Use indexers only when the class logically represents a collection.
* Validate indexes before accessing elements.
* Use meaningful index types (`int`, `string`, etc.).
* Keep indexer logic simple.

<br>

# Common Mistakes

## Using an indexer when a property is more appropriate

Incorrect

```csharp
employee[0]
```

Better

```csharp
employee.Name
```

If the class stores a single value, use a property—not an indexer.

<br>

## Not checking index bounds

```csharp
return phoneNumbers[index];
```

If `index` is invalid, an exception occurs.

Always validate the index when appropriate.

<br>

# Interview Questions

### What is an indexer?

An indexer is a special class member that allows an object to be accessed like an array using square brackets (`[]`).

<br>

### Which keyword is used to define an indexer?

The `this` keyword.

<br>

### Can an indexer have both `get` and `set` accessors?

Yes. Like properties, indexers can have `get`, `set`, or both.

<br>

### What is the difference between a property and an indexer?

A property accesses a single value using a name, while an indexer accesses a collection of values using an index.

<br>

### When should you use an indexer?

When your class represents or manages a collection of data and array-like access makes the API more intuitive.

<br>

# Summary

* Indexers allow objects to be accessed like arrays.
* They are defined using the `this` keyword.
* Indexers use `get` and `set` accessors, similar to properties.
* They provide a clean way to access elements in a collection.
* Use indexers only when array-style access is meaningful for the class.
