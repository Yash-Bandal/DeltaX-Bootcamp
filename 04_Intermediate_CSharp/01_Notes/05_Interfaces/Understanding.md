## Before using interface

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
