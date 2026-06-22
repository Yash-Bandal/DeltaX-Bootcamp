# 1.5 Fields

Fields are variables declared inside a class. They store the data (state) of an object.

For example, an `Employee` object may have fields such as `Name`, `Age`, and `Salary`.




<br>

# What is a Field?

A **field** is a variable that belongs to a class or an object.

Example

```csharp
class Employee
{
    public string Name;
    public int Age;
}
```

<br>

> [!Important]
> Always initialize `List<dataType>` whenever it is declared inside class, dont leave it empty
>
> Thus, either initialze it at top, or inside constructor, but never leave it empty

<div align = "center">
    <img height="210"  alt="image" src="https://github.com/user-attachments/assets/4566ef6b-9a56-4e25-9717-09180671c1d1" />
    '  or  '
    <img height="210" alt="image" src="https://github.com/user-attachments/assets/d4f8b8ae-f225-4896-bf69-7d71d346548f" />
</div>



<br>

Creating an object

```csharp
Employee employee = new Employee();

employee.Name = "Yash";
employee.Age = 22;
```

Each object has its own copy of non-static fields.

<br>

# Field Initialization

Fields can be initialized in two ways.

## 1. Direct Field Initialization

Assign values when declaring the field.

```csharp
class Employee
{
    public string Company = "ABC Ltd";
    public int WorkingHours = 8;
}
```

The values are assigned automatically whenever an object is created.

### Pros

* Simple and concise.
* Ideal for constant or default values.
* Reduces constructor code.

### Cons

* Same value is assigned to every object.
* Cannot use constructor parameters for initialization.

<br>

## 2. Constructor Initialization

Initialize fields inside the constructor.

```csharp
class Employee
{
    public string Name;
    public int Age;

    public Employee(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

Usage

```csharp
Employee employee = new Employee("Yash", 22);
```

### Pros

* Allows each object to have different values.
* Ensures required data is provided during object creation.
* More flexible than direct initialization.

### Cons

* Requires writing constructors.
* Slightly more code.

<br>

# Direct Initialization vs Constructor Initialization

| Direct Initialization     | Constructor Initialization                  |
| ------------------------- | ------------------------------------------- |
| Uses fixed default values | Uses values provided during object creation |
| Simple and concise        | More flexible                               |
| Good for common defaults  | Good for required or dynamic data           |
| No constructor needed     | Requires a constructor                      |

<br>


# The `readonly` Modifier 


The `readonly` keyword makes a field **assignable only during declaration or inside a constructor**.

After the object is created, its value cannot be changed. (Declare once)

<div align = "center">
<img width="600"  alt="image" src="https://github.com/user-attachments/assets/06551eee-5606-4497-8436-d460a28f4a12" />
</div>




Example

```csharp
class Employee
{
    public readonly int EmployeeId;

    public Employee(int id)
    {
        EmployeeId = id;
    }
}
```

Usage

```csharp
Employee employee = new Employee(101);

employee.EmployeeId = 200;
```

The last line causes a **compile-time error**.

<br>

# Why Use `readonly`?

`readonly` improves code robustness by preventing accidental modification of important fields.

Examples:

* Employee ID
* Date Created
* Database Connection String
* Configuration Values

Once assigned, these values remain unchanged throughout the object's lifetime.

<br>

# `readonly` vs Normal Field

| Normal Field              | `readonly` Field                                            |
| ------- | -------- |
| Can be modified anytime   | Can only be assigned during declaration or in a constructor |
| Less safe                 | Prevents accidental changes                                 |
| Suitable for mutable data | Suitable for immutable data                                 |

<br>

# Best Practices

* Use fields only for storing the internal state of an object.
* Initialize constant default values directly.
* Initialize required or dynamic values through constructors.
* Mark fields as `readonly` whenever they should not change after object creation.
* Prefer **properties** over public fields for exposing data (covered in the next topic).

<br>

# Common Mistakes

## Exposing fields publicly

```csharp
public string Name;
```

Although valid, exposing fields publicly is generally discouraged.

Instead, prefer properties.

```csharp
public string Name { get; set; }
```

<br>

## Forgetting to initialize required fields

```csharp
Employee employee = new Employee();
```

This may leave important fields with default values.

Use constructors when certain fields are mandatory.

<br>

## Modifying a `readonly` field

```csharp
employee.EmployeeId = 500;
```

This results in a compile-time error because `readonly` fields cannot be modified after construction.

<br>

# Interview Questions

### What is a field?

A field is a variable declared inside a class that stores the state of an object.

<br>

### What are the two ways to initialize fields?

1. Directly during declaration.
2. Inside a constructor.

<br>

### When should you use direct field initialization?

When every object should start with the same default value.

<br>

### When should you use constructor initialization?

When values differ between objects or are required during object creation.

<br>

### What is the purpose of the `readonly` modifier?

It allows a field to be assigned only during declaration or inside a constructor, preventing modification afterward.

<br>

### Does `readonly` make an object immutable?

No. It prevents reassignment of the field itself. If the field references an object, the object's internal state can still change.

<br>
