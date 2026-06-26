## Before using interface

<br>

> [!Note]
> Interface is just a pipeline (or contract) that makes a promise. It doesn't store anything.

<br>

Suppose you have class `OrderService`
```
Order Service
```
and it needs to save its **orders**


Suppose it currently uses `Sql Database`
```
OrderService
      |
      ▼
SqlDatabase
```

So it must have Tight coupling, and sql database object in it

`OrderService.cs`
```csharp
public class OrderService
{
    private SqlDatabase db = new SqlDatabase();

    public void PlaceOrder()
    {
        db.Save();
    }
}
```

Now tomorrow , our company shifts to `MongoDB Database`
```
SQL
↓

Mongo
```
Now we need to modify  `OrderService.cs`

This is a inconvinient , bad practise, of changing the actual code of major class,

<br>

## After using interface
Now, we know that interface is a contract, like a `USB-C` standard charger, that fits to every compatible mobile

now
```
        OrderService
              |
              ▼
          IDatabase (save())
          /       \
         /         \
SqlDatabase   MongoDatabase (Have individual objects)
```
OrderService says:
```
I only need something that can Save().
```
Not
```
I specifically need SQL.
```

Interface tells the Database classes, whether they are of mongodb, sql, postgre etc, that\
Hey, my client class will need a `Save()` method, I am a middleman, You as a database class\
shall `mandatory` implement save method, that I will inject in client class

Lets make a contract, that you will vide `save()` method


Interface
```csharp
public interface IDatabase
{
    void Save();
}
```


Notice:

No implementation.

Just a promise.

Like USB-C.


**SQL Database**
```csharp
public class SqlDatabase : IDatabase
{
    public void Save()
    {
        Console.WriteLine("Saved in SQL");
    }
}
```

**MongoDB Database**
```csharp
public class MongoDatabase : IDatabase
{
    public void Save()
    {
        Console.WriteLine("Saved in Mongo");
    }
}
```
Both satisfy and implement `IDatabase` interface

**Thus with Interface**
```csharp
public class OrderService
{
    private IDatabase database;

    public OrderService(IDatabase database)
    {
        this.database = database;
    }

    public void PlaceOrder()
    {
        database.Save();
    }
}
```
```
OrderService
      |
      ▼
 IDatabase
   ▲    ▲
   |    |
 SQL Mongo
```
Now OrderService doesn't know SQL exists.



### Wait...
Who creates SQL Database then?

Excellent question.

This is where Dependency Injection comes.

Without DI

OrderService itself creates SQL.
```csharp
private SqlDatabase db = new SqlDatabase();
```

Meaning

I will decide everything.

### **With `DI`**

Somebody outside says

Here.

Use this database.

Like this.
```csharp
IDatabase db = new SqlDatabase();
```
```csharp
OrderService order = new OrderService(db);
```

OrderService didn't create it.

It was injected.

Hence the name

Dependency Injection

So it goes hands in hand with interface implementation


# Isn't Dependency Injection (DI) the same as Inheritance?

**Excellent question.**

**No.** They solve completely different problems.

## Inheritance → **IS-A Relationship**

Use inheritance when one class **is a type of** another class.

```text
Dog
  IS-A
Animal
```

Example:

```csharp
public class Animal
{
}

public class Dog : Animal
{
}
```

Here,

- Dog **is an Animal**
- Cat **is an Animal**
- Car **is NOT an Animal**

---

## Dependency Injection → **HAS-A Relationship**

Use Dependency Injection when one class **uses or depends on** another object.

```text
OrderService
    HAS-A
Database
```

Example:

```csharp
public class OrderService
{
    private readonly IDatabase _database;

    public OrderService(IDatabase database)
    {
        _database = database;
    }
}
```

Here,

- OrderService **has a Database**
- OrderService **uses a Database**
- OrderService is **NOT** a Database

---

## Notice the Difference

This makes no sense:

```text
OrderService
    IS-A
Database
```

❌ Wrong

Instead:

```text
OrderService
    HAS-A
Database
```

✅ Correct

---

## Visual Comparison

### Inheritance

```text
Dog
 │
 ▼
Animal
```

### Dependency Injection

```text
OrderService
      │
      ▼
 IDatabase
   ▲
   │
SqlDatabase
```

---

# Mind Map

```text
                    Interface
                        │
        --------------------------------
        │                              │
Defines a Contract              No Implementation
        │
        ▼
Implemented by Classes
        │
        ▼
Loose Coupling
        │
        ▼
Dependency Injection
        │
        ▼
Object is supplied from outside
        │
        ▼
Easy to Replace
Easy to Test
Easy to Extend
```

<br>

### Complete Example
```csharp
using System;

namespace CSharpIntermediate
{
    // -----------------------------
    // Interface (Contract)
    // -----------------------------
    public interface IDatabase
    {
        void Save();
    }

    // -----------------------------
    // SQL Database
    // -----------------------------
    public class SqlDatabase : IDatabase
    {
        public void Save()
        {
            Console.WriteLine("Order saved in SQL Database.");
        }
    }

    // -----------------------------
    // Mongo Database
    // -----------------------------
    public class MongoDatabase : IDatabase
    {
        public void Save()
        {
            Console.WriteLine("Order saved in MongoDB.");
        }
    }

    //Simulate fake database
    public class FakeDatabase : IDatabase
    {
        public void Save()
        {
            Console.WriteLine("Pretending to save...");
        }
    }

    // -----------------------------
    // Business Logic
    // -----------------------------
    public class OrderService
    {
        private readonly IDatabase _database; //private readonly field.

        // Dependency Injection
        public OrderService(IDatabase database)
        {
            _database = database; 
        }

      /*
      _database (reference pointing)
            │
            ▼
      (database object)   SqlDatabase/MongoDB Object
      */

        public void PlaceOrder()
        {
            Console.WriteLine("Processing Order...");

            _database.Save();

            Console.WriteLine("Order Completed.");
        }
    }

    // -----------------------------
    // Main
    // -----------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the dependency
            IDatabase db = new SqlDatabase();
            /*
            db (we made reference to object, not created it
             │
             ▼
            SqlDatabase Object
            */

            // Inject dependency
            OrderService orderService = new OrderService(db);

            orderService.PlaceOrder();

            Console.WriteLine();

            // Change database without changing OrderService
            db = new MongoDatabase();//make nre reference
            /*
            db (we made reference to object, not created it
             │
             ▼
            MongoDatabase Object
            */

            orderService = new OrderService(db);

            orderService.PlaceOrder();

            Console.WriteLine();

            //Simulate fake database
            orderService = new OrderService(db);

            orderService.PlaceOrder();
        }
    }
}
```
**Output:**
```
Processing Order...
Order saved in SQL Database.
Order Completed.

Processing Order...
Order saved in MongoDB.
Order Completed.

Processing Order... (fake)
Order saved in MongoDB.
Order Completed.

```
```
SqlDatabase
      │
implements
      ▼
IDatabase
      │
used by
      ▼
OrderService
```
Notice:

We changed
```csharp
IDatabase db = new SqlDatabase();
```
to
```csharp
IDatabase db = new MongoDatabase();
```

Nothing inside OrderService changed.

That's the whole point of interfaces + dependency injection.


<br>
Is db an Interface Object?

You wrote:
```csharp
IDatabase db = new SqlDatabase();
```
No, interface cannot be `instantiated`

so its an\
`An interface reference.`

we just change reference
```
db
 │
 ▼
SqlDatabase Object
```
to
```csharp
db = new MongoDatabase();
```
```
db
 │
 ▼
MongoDatabase Object      
```
