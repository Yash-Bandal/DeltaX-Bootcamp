## Definitions

1. **IEnumerable<string>** : A sequence (collection) of strings that can be enumerated (looped through).
    | `List<string>`                       | `IEnumerable<string>`                          |
    | ------------------------------------ | ---------------------------------------------- |
    | Concrete collection                  | Interface                                      |
    | Stores data                          | Represents a sequence of data                  |
    | Can add/remove items                 | Read-only from the consumer's perspective      |
    | Supports indexing (`list[0]`)        | No indexing 🏷️                                |
    | Has methods like `Add()`, `Remove()` | Only enumeration (plus LINQ extension methods) |
    | Implements `IEnumerable<string>`     | Doesn't store data itself                      |
    
    Example 1: List<string>
    ```csharp
    List<string> movies = new()
    {
        "Avatar",
        "Titanic",
        "Inception"
    };
    
    movies.Add("Alien");
    
    Console.WriteLine(movies[0]);   // Avatar
    ```
    A List owns the data.
    
    Example 2: IEnumerable<string>
    ```csharp
    IEnumerable<string> movies = new List<string>
    {
        "Avatar",
        "Titanic",
        "Inception"
    };
    
    foreach (var movie in movies)
    {
        Console.WriteLine(movie);
    }
    ```
    This works because List<string> implements IEnumerable<string>.
    
    However, this won't compile:
    ```csharp    
    movies.Add("Alien");   // ❌ Error
    ```
2. **Dependency Injection** : Dependency Injection (DI) in C# is a design pattern where a class receives its required dependencies from an external source rather than creating them itself using the new keyword
