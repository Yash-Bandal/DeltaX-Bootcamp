### Dependency Injection
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
    _actorRepository new ActorRepository();
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
