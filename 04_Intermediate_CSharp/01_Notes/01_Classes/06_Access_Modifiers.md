# 1.6 Access Modifiers

Access modifiers define the **visibility** and **accessibility** of classes and their members (fields, methods, properties, constructors, etc.).

They are one of the key tools used to implement **Encapsulation**, a fundamental principle of Object-Oriented Programming.

<br>

<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/ea4010ae-07f0-43ef-85a7-8bd661894cdc" />
</div>




<br>

# What is Encapsulation?

**Encapsulation** is the process of hiding an object's internal data and exposing only what is necessary.

Instead of allowing direct access to data, we control how it is read or modified.

<br>

> [!Note]
> 1. Encapsulation is a practice of bundling data (fields) and the methods,
> 2. Access specifiers are just fields to implement Encapsulation
>
> | **Encapsulation**                                  | **Abstraction**                                                        |
> | -------------------------------------------------- | ---------------------------------------------------------------------- |
> | **Hides data** by restricting direct access to it. | **Hides implementation (complexity)** by exposing only the necessary functionality. |
> | **Focus:** Protect the object's internal state.    | **Focus:** Simplify usage by hiding complexity.                        |
>

<br>


### Real-World Example

Think of a car.

* You can press the accelerator.
* You can apply the brakes.
* You cannot directly control the engine's internal components.

The car hides its implementation and exposes only the necessary controls.

Similarly, a class hides its internal data and exposes controlled access through methods or properties.

<br>

# Why Do We Need Access Modifiers?

Not every field or method should be accessible from everywhere.

Access modifiers help us:

* Hide implementation details.
* Protect object data.
* Prevent accidental modifications.
* Improve security.
* Enforce encapsulation.
* Make code easier to maintain.

<br>

# Types of Access Modifiers

<br>

<div align = "center">
  <img width="401" height="404" alt="image" src="https://github.com/user-attachments/assets/88dd3926-8dde-4f55-90d1-ced3305d8373" />
</div>

<br>

C# provides the following access modifiers:

* `public`
* `private`
* `protected`
* `internal`
* `protected internal`
* `private protected`

> In this chapter, the focus is mainly on **public** and **private**. The remaining modifiers are covered later while studying inheritance.



<br>

<div align = "center">
<img width="600" height="338" alt="image" src="https://github.com/user-attachments/assets/de491bbd-6041-484b-97cc-adb21a27772d" />
</div>


<br>

# public

A `public` member can be accessed from anywhere.

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

<br>

# private

A `private` member can only be accessed within its own class.

Example

```csharp
class Person
{
    private int Age;
}
```

Trying to access it outside the class causes a compile-time error.

```csharp
Person person = new Person();

person.Age = 22;    // Error
```

<br>

# Default Access Level

If no access modifier is specified for a class member, it is **private** by default.

```csharp
class Person
{
    int Age;
}
```

is equivalent to

```csharp
class Person
{
    private int Age;
}
```

<br>

# What is Wrong with Public Fields?

Consider this class.

```csharp
class BankAccount
{
    public decimal Balance;
}
```

Now anyone can write:

```csharp
account.Balance = -5000;
```

The object has no control over its own data.

This breaks encapsulation.



<br>

# Why Should Fields Be Private?

Fields represent the internal state of an object.

Making them `private` protects the object from invalid or unexpected changes.

Instead of exposing fields directly, expose controlled access through **methods** or **properties**.

Example

```csharp
class BankAccount
{
    private decimal balance;

    public void Deposit(decimal amount)
    {
        if (amount > 0)
            balance += amount;
    }

    public decimal GetBalance()
    {
        return balance;
    }
}
```

Usage

```csharp
BankAccount account = new BankAccount();

account.Deposit(500);

Console.WriteLine(account.GetBalance());
```

Notice that the `balance` field cannot be modified directly.

The class decides **how** it changes.

<br>

# Public Accessors

A **public accessor** is a public method or property that provides controlled access to a private field.

Example

```csharp
class Employee
{
    private string name;

    public void SetName(string value)
    {
        name = value;
    }

    public string GetName()
    {
        return name;
    }
}
```
Later, we'll see that **properties** provide a cleaner way to achieve the same result.


<br>

# `Get()` and  `Set()`

<br>

<div align = "center">
<img width="500"  alt="image" src="https://github.com/user-attachments/assets/a2a513b1-5a9d-41ca-a81a-5ec461a2c2e2" />
</div>

<br>

With this code, if we craate a instance of this class, we cannot access the `private` name field of the class,

We need to call `SetName()` or `GetName()` method here 

This is the **advantage** of Getter and setter methods
1. If the input is invalid or empty (here if input is null string), the variable field will be set  to wrong value
2. Fields are static, they dont have any  **logic**,
3. In order to implement `Input validation` , we use getters and setter, as they have some logic  



<br>

# Access Modifier Comparison

| Modifier             | Accessible From                                        |
| -------------------- | ------------------------------------------------------ |
| `public`             | Anywhere                                               |
| `private`            | Same class only                                        |
| `protected`          | Same class and derived classes                         |
| `internal`           | Same assembly                                          |
| `protected internal` | Same assembly or derived classes                       |
| `private protected`  | Same class or derived classes within the same assembly |

<br>

# Best Practices

* Make fields `private` by default.
* Expose data through methods or properties.
* Hide implementation details.
* Grant only the minimum required access.
* Avoid public fields unless there is a specific reason.

<br>

# Common Mistakes

## Making every field public

```csharp
public string Name;
public int Age;
public decimal Salary;
```

This breaks encapsulation and allows unrestricted modification.

<br>

## Accessing private members directly

```csharp
employee.Salary = 50000;
```

This is not allowed if `Salary` is private.

<br>

## Using fields instead of properties

Public fields cannot perform validation or enforce business rules.

Properties provide controlled access and are preferred in most scenarios.

<br>

# Interview Questions

### What is encapsulation?

Encapsulation is the process of hiding an object's internal data and exposing only the necessary functionality through controlled access.

<br>

### Why should fields be private?

Private fields protect the object's internal state, prevent invalid modifications, and improve maintainability.

<br>

### What are public accessors?

Public accessors are methods or properties that provide controlled access to private fields.

<br>

### What is the default access modifier for class members?

`private`

<br>

### What is the difference between `public` and `private`?

* `public` members are accessible from anywhere.
* `private` members are accessible only within the same class.

<br>

# Summary

* Access modifiers control the visibility of class members.
* Encapsulation hides internal implementation and exposes only what is necessary.
* Fields should generally be `private`.
* Public methods or properties act as controlled accessors for private fields.
* Prefer exposing behavior instead of exposing data directly.
* Proper use of access modifiers results in safer, more maintainable, and robust code.
