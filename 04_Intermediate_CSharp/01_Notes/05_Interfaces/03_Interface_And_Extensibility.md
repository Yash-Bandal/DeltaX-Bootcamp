# 5.3 Interfaces and Extensibility

<br>

> [!Tip]
> ### Refer [This](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_Intermediate_CSharp/01_Notes/05_Interfaces/Understanding.md) for proper Understanding

<br>

One of the biggest advantages of interfaces is **extensibility**.

An application should be designed so that **new features can be added without modifying existing, working code**. Interfaces make this possible by allowing different implementations to be plugged into the application.

<br>

# What is Extensibility?

**Extensibility** is the ability to add new functionality to an application **without changing existing code**.

Instead of modifying a class whenever requirements change, we simply create another class that implements the same interface.

This makes applications easier to maintain, test, and extend.

<br>

# Why is Extensibility Important?

Suppose an application saves orders in a **SQL Database**.

Later, the company decides to use:

* MongoDB
* Oracle Database
* PostgreSQL

If the application is tightly coupled to SQL, every change requires modifying the business logic.

Interfaces solve this problem.

<br>

# Without Interfaces

```csharp
public class OrderService
{
    private SqlDatabase database = new SqlDatabase();

    public void PlaceOrder()
    {
        Console.WriteLine("Processing Order...");

        database.Save();

        Console.WriteLine("Order Completed.");
    }
}
```

Now suppose the company switches to MongoDB.

We must modify `OrderService`.

```csharp
private MongoDatabase database = new MongoDatabase();
```

Every new database requires changing existing code.

This violates the **Open/Closed Principle**.

<br>

# Using Interfaces

## Step 1: Define the Interface

```csharp
public interface IDatabase
{
    void Save();
}
```

<br>

## Step 2: Create Implementations

```csharp
public class SqlDatabase : IDatabase
{
    public void Save()
    {
        Console.WriteLine("Order saved in SQL Database.");
    }
}

public class MongoDatabase : IDatabase
{
    public void Save()
    {
        Console.WriteLine("Order saved in MongoDB.");
    }
}
```

<br>

## Step 3: Use the Interface

```csharp
public class OrderService
{
    private readonly IDatabase database;

    public OrderService(IDatabase database)
    {
        this.database = database;
    }

    public void PlaceOrder()
    {
        Console.WriteLine("Processing Order...");

        database.Save();

        Console.WriteLine("Order Completed.");
    }
}
```

<br>

## Usage

Using SQL Database

```csharp
IDatabase db = new SqlDatabase();

OrderService orderService = new OrderService(db);

orderService.PlaceOrder();
```

Output

```text
Processing Order...
Order saved in SQL Database.
Order Completed.
```

<br>

Switch to MongoDB

```csharp
IDatabase db = new MongoDatabase();

OrderService orderService = new OrderService(db);

orderService.PlaceOrder();
```

Output

```text
Processing Order...
Order saved in MongoDB.
Order Completed.
```

Notice that **OrderService never changes.**

Only the implementation supplied to it changes.

<br>

# Adding New Features

Suppose the company now decides to use **Oracle Database**.

Simply create another implementation.

```csharp
public class OracleDatabase : IDatabase
{
    public void Save()
    {
        Console.WriteLine("Order saved in Oracle Database.");
    }
}
```

Usage

```csharp
IDatabase db = new OracleDatabase();

OrderService orderService = new OrderService(db);

orderService.PlaceOrder();
```

No existing classes are modified.

The application has been **extended** without changing existing code.

<br>

# Open/Closed Principle (OCP)

Interfaces support the **Open/Closed Principle**.

A class should be:

* **Open for Extension**
* **Closed for Modification**

Instead of modifying `OrderService`, we extend the application by creating another database class that implements `IDatabase`.

<br>

# Benefits of Interfaces for Extensibility

* Easily switch between different database providers.
* Add new databases without modifying business logic.
* Reduce the risk of introducing bugs.
* Promote loose coupling.
* Improve maintainability.
* Make applications easier to scale and test.

<br>

# Real-World Examples

Interfaces allow applications to swap implementations without changing business logic.

Examples:

* SQL Server, MongoDB, Oracle, PostgreSQL database providers.
* Gmail, Outlook, Amazon SES email providers.
* PayPal, Stripe, Razorpay payment gateways.
* Local storage or cloud storage providers.

The application depends on the **interface**, not the concrete implementation.

<br>

# Best Practices

* Program against interfaces, not concrete classes.
* Inject interface implementations through constructors (Dependency Injection).
* Add new implementations instead of modifying existing classes.
* Keep each implementation focused on a single responsibility.

<br>

# Common Mistakes

## Depending on Concrete Classes

```csharp
private SqlDatabase database = new SqlDatabase();
```

This creates **tight coupling**.

Instead,

```csharp
private readonly IDatabase database;
```

Now `OrderService` works with **any database** that implements `IDatabase`.

<br>

## Modifying Existing Classes

Avoid this:

```csharp
if(databaseType == "SQL")
{
    ...
}
else if(databaseType == "Mongo")
{
    ...
}
else if(databaseType == "Oracle")
{
    ...
}
```

Every new database requires modifying existing code.

Instead, simply create another class implementing `IDatabase`.

<br>

# Interview Questions

### What is extensibility?

Extensibility is the ability to add new functionality to an application without modifying existing code.

<br>

### How do interfaces improve extensibility?

Interfaces allow different implementations to be plugged into an application without changing the classes that depend on them.

<br>

### Which SOLID principle is supported by interfaces?

The **Open/Closed Principle (OCP)**.

<br>

### Why are interfaces useful for databases?

They allow applications to switch between SQL Server, MongoDB, Oracle, or any other database without changing the business logic.

<br>

# Summary

* Extensibility means adding new functionality without modifying existing code.
* Interfaces make applications extensible by allowing multiple implementations.
* `OrderService` depends on `IDatabase`, not `SqlDatabase`.
* New databases are added by creating new classes that implement `IDatabase`.
* Interfaces help follow the **Open/Closed Principle**, making applications easier to maintain, test, and scale.
