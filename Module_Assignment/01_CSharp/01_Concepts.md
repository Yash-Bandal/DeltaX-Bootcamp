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

Normal
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

