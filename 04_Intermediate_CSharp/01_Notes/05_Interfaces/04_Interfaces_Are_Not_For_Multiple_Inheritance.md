## Interfaces are **NOT** for Multiple Inheritance

A common misconception is that interfaces exist to provide multiple inheritance.

While C# allows a class to implement multiple interfaces, **this is not the primary purpose of interfaces**.

The main purpose of interfaces is to:

* Define a common **contract**.
* Promote **loose coupling**.
* Enable **Dependency Injection (DI)**.
* Improve **extensibility**, **testability**, and **maintainability**.

### Example

```csharp
public interface IDatabase
{
    void Save();
}

public class SqlDatabase : IDatabase
{
    public void Save() { }
}

public class MongoDatabase : IDatabase
{
    public void Save() { }
}
```

`OrderService` depends on `IDatabase`, so it can work with **SqlDatabase**, **MongoDatabase**, or any future database without changing its own code.

> **Remember:** Multiple interface implementation is a feature of interfaces, **not their primary purpose**.
