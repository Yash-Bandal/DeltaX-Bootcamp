### Dependency Injection
Dependency Injection is a design pattern where an object receives the objects it depends on from the outside, instead of creating them itself.

It reduces coupling

Program.cs
```csharp
ActorService actorService = new ActorService(new ActorRepository()); // DI
```

And inside\
ActorSerrvice.cs
```csharp
private readonly IActorRepository _actorRepository;

public ActorService(IActorRepository actorRepository)
{
    _actorRepository = actorRepository;
}
```

<br>

### Normal
```csharp
ActorService actorService = new ActorService(); 
```

And inside\
ActorSerrvice.cs
```csharp
private readonly IActorRepository _actorRepository;
 
public ActorService(IActorRepository actorRepository)
{
    _actorRepository = new ActorRepository();
}
```

<br>

## Note
We build in this order
```
✓ Models
✓ Repositories
✓ Services
✓ MovieService

⬇

Program.cs
```


Q. thnks, but why program agains interface instead of class

Q. Why what when use, what if not used


## TryParse vs TryParseExact for datetime
```csharp
if (!DateTime.TryParse(Console.ReadLine(), out DateTime dob))
{
    throw new InvalidDataException("Invalid date.");
}
```

Accepts inputs like:
```
12/10/2002
12-10-2002
October 12, 2002
2002-10-12
```
The exact accepted formats depend on the current culture.


```csharp
if (!DateTime.TryParseExact(
        Console.ReadLine(),
        "dd/MM/yyyy",
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        out DateTime dob))
{
    throw new InvalidDataException("Enter date in dd/MM/yyyy format.");
}
```
This accepts only:
```
12/10/2002
```
It rejects:
```
12-10-2002
2002/10/12
October 12, 2002
```

### Future validation

```csharp
if (movie.YearOfRelease > DateTime.Now.Year)
    throw new InvalidDataException("Movie year cannot be in the future.");
```
