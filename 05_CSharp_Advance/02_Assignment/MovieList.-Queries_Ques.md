# LINQ Practice — IMDB Movie Dataset

Practice queries using an IMDB-style dataset of Movies, Actors, and Producers.

<br>

## Data Model

```csharp
List<Movie> MovieList;
```

Each `Movie` contains:

- `Name`
- `YearOfRelease`
- `Plot`
- `Producer`
- `List<Actor> Actors`

### Sample Data

```js
MovieList
[
    {
        Name : "Avatar",
        YearOfRelease : 2009,
        Plot : "Humans explore Pandora.",

        Actors :
        [
            { Id : 1, Name : "Sam Worthington" },
            { Id : 2, Name : "Zoe Saldana" }
        ],

        Producer : { Id : 1, Name : "James Cameron" }
    },

    {
        Name : "Men in Black",
        YearOfRelease : 1997,
        Plot : "Secret agents protect Earth.",

        Actors :
        [
            { Id : 3, Name : "Will Smith" },
            { Id : 4, Name : "Tommy Lee Jones" }
        ],

        Producer : { Id : 2, Name : "Walter Parkes" }
    }
]
```

> [!Tip]
> The full practice set below also references `Avatar 2` (James Cameron, 2022), `Oppenheimer` (Christopher Nolan), and `I Am Legend` (Akiva Goldsman, 2007) as part of the same list.

<br>

## Part 1 — Core Queries (Movie / Actor / Producer objects)

<br>

### 1. Get all movies

```csharp
var result = MovieList;
```

```text
[ Avatar, Avatar 2, Men in Black, Oppenheimer ]
```

<br>

### 2. Get all movie names

```csharp
var result = MovieList
    .Select(m => m.Name);
```

```text
[ "Avatar", "Avatar 2", "Men in Black", "Oppenheimer" ]
```

<br>

### 3. Get all movie names and years

```csharp
var result = MovieList
    .Select(m => $"{m.Name} - {m.YearOfRelease}");
```

```text
[ "Avatar - 2009", "Avatar 2 - 2022", "Men in Black - 1997" ]
```

<br>

### 4. Get movies released after 2010

```csharp
var result = MovieList
    .Where(m => m.YearOfRelease > 2010);
```

```text
[ Avatar 2, Oppenheimer ]
```

<br>

### 5. Get movie names released after 2010

```csharp
var result = MovieList
    .Where(m => m.YearOfRelease > 2010)
    .Select(m => m.Name);
```

```text
[ "Avatar 2", "Oppenheimer" ]
```

<br>

### 6. Get movies produced by James Cameron

```csharp
var result = MovieList
    .Where(m => m.Producer.Name == "James Cameron");
```

```text
[ Avatar, Avatar 2 ]
```

<br>

### 7. Get names of movies produced by James Cameron

```csharp
var result = MovieList
    .Where(m => m.Producer.Name == "James Cameron")
    .Select(m => m.Name);
```

```text
[ "Avatar", "Avatar 2" ]
```

<br>

### 8. Get first Avatar movie

```csharp
var result = MovieList
    .FirstOrDefault(m => m.Name.Contains("Avatar"));
```

```text
Movie { Name = "Avatar" }
```

<br>

### 9. Check if any movie released after 2025

```csharp
var result = MovieList
    .Any(m => m.YearOfRelease > 2025);
```

```text
false
```

<br>

### 10. Check whether all movies have producers

```csharp
var result = MovieList
    .All(m => m.Producer != null);
```

```text
true
```

<br>

### 11. Count all movies

```csharp
var result = MovieList.Count();
```

```text
4
```

<br>

### 12. Count movies after 2010

```csharp
var result = MovieList
    .Count(m => m.YearOfRelease > 2010);
```

```text
2
```

<br>

### 13. Sort movies by release year

```csharp
var result = MovieList
    .OrderBy(m => m.YearOfRelease);
```

```text
Men in Black
Avatar
Avatar 2
Oppenheimer
```

<br>

### 14. Sort newest movies first

```csharp
var result = MovieList
    .OrderByDescending(m => m.YearOfRelease);
```

```text
Oppenheimer
Avatar 2
Avatar
Men in Black
```

<br>

### 15. Sort by Producer, then Release Year

```csharp
var result = MovieList
    .OrderBy(m => m.Producer.Name)
    .ThenBy(m => m.YearOfRelease);
```

```text
Grouped by producer, then oldest to newest
```

<br>

### 16. Get all producer names (with duplicates)

```csharp
var result = MovieList
    .Select(m => m.Producer.Name);
```

```text
[ James Cameron, James Cameron, Walter Parkes, Christopher Nolan ]
```

<br>

### 17. Get unique producer names

```csharp
var result = MovieList
    .Select(m => m.Producer.Name)
    .Distinct();
```

```text
[ James Cameron, Walter Parkes, Christopher Nolan ]
```

<br>

### 18. Get all actor collections (nested)

```csharp
var result = MovieList
    .Select(m => m.Actors);
```

```text
[
    [Sam Worthington, Zoe Saldana],
    [Will Smith, Tommy Lee Jones]
]
```

Type: `IEnumerable<List<Actor>>`

<br>

### 19. Flatten all actors (`SelectMany`)

```csharp
var result = MovieList
    .SelectMany(m => m.Actors)
    .Distinct()
    .OrderBy(a => a.Name)
    .ToList();
```

```text
Sam Worthington
Tommy Lee Jones
Will Smith
Zoe Saldana
```

Type: `IEnumerable<Actor>`

<br>

### 20. Get names of every actor

```csharp
var result = MovieList
    .SelectMany(m => m.Actors)
    .Select(a => a.Name);
```

```text
[ Sam Worthington, Zoe Saldana, Will Smith, Tommy Lee Jones ]
```

<br>

### 21. Get unique actor names

```csharp
var result = MovieList
    .SelectMany(m => m.Actors)
    .Select(a => a.Name)
    .Distinct();
```

```text
[ Sam Worthington, Zoe Saldana, Will Smith, Tommy Lee Jones ]
```

<br>

### 22. Get movies in which Will Smith acted

```csharp
var result = MovieList
    .Where(m => m.Actors.Any(a => a.Name == "Will Smith"))
    .Select(m => m.Name);
```

```text
[ Men in Black, I Am Legend ]
```

<br>

### 23. Group movies by producer

```csharp
var result = MovieList
    .GroupBy(m => m.Producer.Name);
```

```text
James Cameron
    Avatar
    Avatar 2

Walter Parkes
    Men in Black
```

<br>

### 24. Group movies by release year

```csharp
var result = MovieList
    .GroupBy(m => m.YearOfRelease);
```

```text
1997
    Men in Black

2009
    Avatar

2022
    Avatar 2
```

<br>

### 25. Get latest release year

```csharp
var result = MovieList
    .Max(m => m.YearOfRelease);
```

```text
2023
```

<br>

### 26. Get oldest release year

```csharp
var result = MovieList
    .Min(m => m.YearOfRelease);
```

```text
1997
```

<br>

### 27. Get average release year

```csharp
var result = MovieList
    .Average(m => m.YearOfRelease);
```

```text
2012.75
```

<br>

### 28. Sum of all release years

```csharp
var result = MovieList
    .Sum(m => m.YearOfRelease);
```

```text
8051
```

<br>

### 29. Take first two movies

```csharp
var result = MovieList
    .Take(2);
```

```text
[ Avatar, Avatar 2 ]
```

<br>

### 30. Skip first two movies

```csharp
var result = MovieList
    .Skip(2);
```

```text
[ Men in Black, Oppenheimer ]
```

<br>

### 31. Pagination (Page 2)

```csharp
var result = MovieList
    .Skip(10)
    .Take(10);
```

```text
Movies 11–20
```

<br>

### 32. Convert to Dictionary

```csharp
var result = MovieList
    .ToDictionary(
        m => m.Name,
        m => m.YearOfRelease);
```

```text
{
    Avatar : 2009,
    Avatar 2 : 2022,
    Men in Black : 1997
}
```

<br>

### 33. Reverse current order

```csharp
var result = MovieList
    .Reverse();
```

```text
[ Oppenheimer, Men in Black, Avatar 2, Avatar ]
```

<br>

### 34. Latest movie (full object)

```csharp
var result = MovieList
    .OrderByDescending(m => m.YearOfRelease)
    .FirstOrDefault();
```

```text
{
    Name = Avatar 2,
    YearOfRelease = 2022,
    Producer = James Cameron
}
```

<br>

### 35. Movies with more than one actor

```csharp
var result = MovieList
    .Where(m => m.Actors.Count > 1)
    .Select(m => new
    {
        m.Name,
        ActorCount = m.Actors.Count
    })
    .ToList();
```

```text
[
    { Name = Avatar, ActorCount = 2 },
    { Name = Avatar 2, ActorCount = 2 },
    { Name = Men in Black, ActorCount = 2 }
]
```

<br>

### 36. Order by Producer, then Release Year (projected)

```csharp
var result = MovieList
    .OrderBy(m => m.Producer)
    .ThenBy(m => m.YearOfRelease)
    .Select(m => new
    {
        m.Name,
        m.Producer,
        m.YearOfRelease
    })
    .ToList();
```

```text
[
    Avatar (James Cameron, 2009),
    Avatar 2 (James Cameron, 2022),
    I Am Legend (Akiva Goldsman, 2007),
    Men in Black (Walter Parkes, 1997)
]
```

<br>

### 37. Movies after 2000 named "Avatar" (`&&`)

```csharp
var result = MovieList.Where(m =>
    m.YearOfRelease > 2000 &&
    m.Name.Contains("Avatar"));
```

```text
[ Avatar, Avatar 2 ]
```

<br>

## Part 2 — Queries Against Separate Actor / Producer Lists

> [!Important]
> These examples use a different shape: `Movie` holds `ActorIds` (a list of foreign keys) instead of embedded `Actor` objects, and `actors` / `producers` are separate top-level lists that get joined via `Where(... Contains ...)`.

<br>

### 38. Show movie + actor names from `ActorIds` (1 movie → many actors)

```csharp
var result = movies.Select(movie => new
{
    Movie = movie.Name,

    Actors = actors
                .Where(actor => movie.ActorIds.Contains(actor.Id))
                .Select(actor => actor.Name)
                .ToList()
});
```

```text
Movie : Avengers
Actors:
- Robert Downey Jr.
- Chris Evans
- Scarlett Johansson

Movie : Interstellar
Actors:
- Matthew McConaughey
- Anne Hathaway

Movie : Titanic
Actors:
- Leonardo DiCaprio
```

<br>

### 39. Producers born after 25 September 2000

```csharp
var producerNames = producers
    .Where(p => p.DOB > new DateTime(2000, 9, 25))
    .Select(p => p.Name);
```

First match only:

```csharp
Producer producer = producers
    .FirstOrDefault(p => p.DOB > new DateTime(2000, 9, 25));
```

<br>

### 40. Producer with the maximum number of movies

```csharp
var result = movies
    .GroupBy(m => m.Producer.Name)
    .OrderByDescending(g => g.Count())
    .First();
```

<br>

### 41. Youngest producer

```csharp
var result = producers
    .OrderByDescending(p => p.DOB)
    .First();
```

> [!Tip]
> Sort **descending** by DOB — the largest (most recent) date of birth is the youngest person. E.g. among birth years 1980, 1999, 2003, `2003` is the youngest.

<br>

### 42. Oldest actor

```csharp
var result = actors
    .OrderBy(a => a.DOB)
    .First();
```

<br>

### 43. Movies with a producer born after 1995

```csharp
var result = movies
    .Where(m => m.Producer.DOB.Year > 1995);
```

<br>

### 44. Movies featuring actor "Tom Cruise"

```csharp
var result = movies
    .Where(m => m.Actors.Any(a => a.Name == "Tom Cruise"));
```

<br>

### 45. Group actors by birth year

```csharp
var result = actors
    .GroupBy(a => a.DOB.Year);
```

<br>

### 46. Top 3 latest movies

```csharp
var result = movies
    .OrderByDescending(m => m.YearOfRelease)
    .Take(3);
```

<br>

### 47. Movie with the longest plot

```csharp
var result = movies
    .OrderByDescending(m => m.Plot.Length)
    .First();
```
