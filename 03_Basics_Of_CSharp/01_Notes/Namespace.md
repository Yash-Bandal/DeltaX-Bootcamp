# Namespace in C# 

## What is a Namespace?

A namespace is a **named container for code**. It groups related classes, interfaces, enums, structs, and other types together and helps prevent naming conflicts.

Think of a namespace as a **labeling system** or **address system** for your code.


<br>



## Visualizing a Namespace

```text
Global
│
├── System
│   ├── String
│   ├── Console
│   └── Math
│
└── MyApp
    ├── Models
    │   └── User
    │
    ├── Services
    │   └── UserService
    │
    └── Controllers
        └── UserController
```

Each class has a unique address:

```csharp
System.Console
MyApp.Models.User
MyApp.Services.UserService
```


---
> [!important]
> The namespace is everything before the class name.
---

<br>



## What a Namespace is NOT

Many beginners imagine namespaces as containers that physically hold objects.

A namespace is **not**:

* ❌ An object in memory
* ❌ A runtime structure
* ❌ Something that gets instantiated
* ❌ A container that stores data

Namespaces mainly exist for:

* Organization
* Naming
* Avoiding conflicts

Example:

```csharp
namespace MyApp.Services
{
    class UserService
    {
    }
}
```

This does **not** create a `Services` object.

It simply tells the compiler:

> "The class UserService belongs to the group MyApp.Services."


<br>



## Why Namespaces Exist

Imagine two different libraries both have a class named `User`.

Without namespaces:

```csharp
class User
{
}

class User
{
} // Error
```

The compiler cannot determine which `User` class you're referring to.

With namespaces:

```csharp
namespace LibraryA
{
    class User
    {
    }
}

namespace LibraryB
{
    class User
    {
    }
}
```

Now both classes can exist without conflict.

Usage:

```csharp
LibraryA.User user1;
LibraryB.User user2;
```


<br>



## What the `using` Keyword Does

Suppose you have:

```csharp
namespace MyApp.Services
{
    class UserService
    {
    }
}
```

### Without `using`

You must write the full address every time:

```csharp
MyApp.Services.UserService service =
    new MyApp.Services.UserService();
```

### With `using`

```csharp
using MyApp.Services;

UserService service = new UserService();
```

### Easy Way to Think About It

When the compiler sees:

```csharp
UserService
```

it first looks inside:

```csharp
MyApp.Services
```

because of the `using` statement.

`using` does **not** import code like Python.

It simply saves typing by allowing shorter names.


<br>



## Real-World Analogy: Postal Address

Think of a namespace like a home address.

```text
India
 └── Maharashtra
      └── Mumbai
           └── Andheri
                └── House 42
```

The complete address identifies exactly one house.

Similarly:

```text
MyCompany
 └── ECommerce
      └── Services
           └── PaymentService
```

The full address identifies exactly one class.

```csharp
MyCompany.ECommerce.Services.PaymentService
```

<br>


## Complete Example

```csharp
using System;

namespace MyApp.Services
{
    class UserService
    {
        public void PrintMessage()
        {
            Console.WriteLine("User Service Running");
        }
    }
}

class Program
{
    static void Main()
    {
        MyApp.Services.UserService service =
            new MyApp.Services.UserService();

        service.PrintMessage();
    }
}
```

Output:

```text
User Service Running
```

### Simple Formula

```text
Namespace + Class Name = Full Address of a Type
```

Example:

```text
MyApp.Services + UserService
=
MyApp.Services.UserService
```
