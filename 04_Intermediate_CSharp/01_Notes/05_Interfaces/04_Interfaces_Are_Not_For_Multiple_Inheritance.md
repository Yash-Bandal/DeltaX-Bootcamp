## Interfaces are **NOT** for Multiple Inheritance 🏷️

<br>

> [!Note]
> Chatgpt, other AI agents, some books may say Interface is used to impment inheritace , it is not possible

<br>

A common misconception is that interfaces exist to provide multiple inheritance.

While C# allows a class to implement multiple interfaces, **this is not the primary purpose of interfaces**.


### There is no code to inherit , just a contact
We dont inherit any code, we just sign contract, so there is ncode reusability with Interface like inheritance

The syntax looks similar, but concept is differnt
```csharp
public class MongoDatabase : IDatabase, ISaveFunction
```

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
