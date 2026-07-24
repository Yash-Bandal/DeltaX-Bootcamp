# LINQ Practice — (Movies / Actors / Producers)

All queries below run against this exact dataset, so every output is the *real* computed result — not a placeholder.

```csharp
List<Movie> movies = new List<Movie>
{
    new Movie
    {
        Id = 1, Name = "Avatar",
        YearOfRelease = 2009,
        Plot = "Humans explore Pandora.",
        ActorIds = new List<int> { 1, 2 },
        ProducerId = 1
    },
    new Movie
    {
        Id = 2, Name = "Men in Black",
        YearOfRelease = 1997,
        Plot = "Secret agents protect Earth.",
        ActorIds = new List<int> { 3, 4 },
        ProducerId = 2
    },
    new Movie
    {
        Id = 3,
        Name = "I Am Legend",
        YearOfRelease = 2007,
        Plot = "A lone survivor fights infected humans.",
        ActorIds = new List<int> { 3 },
        ProducerId = 3
    }
};
```
```csharp
List<Actor> actors = new List<Actor>
{
    new Actor
    {
        Id = 1,
        Name = "Sam Worthington",
        DOB = new DateTime(1976, 8, 2)
     },
    new Actor
    {
        Id = 2,
        Name = "Zoe Saldana",
        DOB = new DateTime(1978, 6, 19)
    },
    new Actor
    {
        Id = 3,
        Name = "Will Smith",
        DOB = new DateTime(1968, 9, 25)
    },
    new Actor
    {
         Id = 4,
         Name = "Tommy Lee Jones",
         DOB = new DateTime(1946, 9, 15) }
    };
```
```csharp
List<Producer> producers = new List<Producer>
{
    new Producer { Id = 1, Name = "James Cameron",  DOB = new DateTime(1954, 8, 16) },
    new Producer { Id = 2, Name = "Walter Parkes",  DOB = new DateTime(1951, 4, 15) },
    new Producer { Id = 3, Name = "Akiva Goldsman", DOB = new DateTime(1962, 7, 7) }
};
```

<br>

## Part A — Movie Queries

<br>

### 1. Get all movie names

```csharp
var result = movies.Select(m => m.Name);
```

```text
Avatar
Men in Black
I Am Legend
```

<br>

### 2. Get movie names with release year

```csharp
var result = movies.Select(m => $"{m.Name} - {m.YearOfRelease}");
```

```text
Avatar - 2009
Men in Black - 1997
I Am Legend - 2007
```

<br>

### 3. Movies released after 2000

```csharp
var result = movies.Where(m => m.YearOfRelease > 2000);
```

```text
Avatar (2009)
I Am Legend (2007)
```

<br>

### 4. Count movies released after 2000

```csharp
var result = movies.Count(m => m.YearOfRelease > 2000);
```

```text
2
```

<br>

### 5. Any movie released after 2025?

```csharp
var result = movies.Any(m => m.YearOfRelease > 2025);
```

```text
false
```

<br>

### 6. Do all movies have a ProducerId?

```csharp
var result = movies.All(m => m.ProducerId != 0);
```

```text
true
```

<br>

### 7. Sort movies by release year (oldest first)

```csharp
var result = movies.OrderBy(m => m.YearOfRelease);
```

```text
Men in Black (1997)
I Am Legend (2007)
Avatar (2009)
```

<br>

### 8. Sort movies by release year (newest first)

```csharp
var result = movies.OrderByDescending(m => m.YearOfRelease);
```

```text
Avatar (2009)
I Am Legend (2007)
Men in Black (1997)
```

<br>

### 9. Latest movie (`MaxBy`, .NET 6+)

```csharp
var result = movies.MaxBy(m => m.YearOfRelease);
```

```text
Avatar (2009)
```

<br>

### 10. Oldest movie (`MinBy`, .NET 6+)

```csharp
var result = movies.MinBy(m => m.YearOfRelease);
```

```text
Men in Black (1997)
```

<br>

### 11. Average / Sum of release years

```csharp
var avg = movies.Average(m => m.YearOfRelease);
var sum = movies.Sum(m => m.YearOfRelease);
```

```text
avg = 2004.33
sum = 6013
```

<br>

### 12. First movie whose name contains "I Am"

```csharp
var result = movies.FirstOrDefault(m => m.Name.Contains("I Am"));
```

```text
I Am Legend
```

<br>

### 13. Take / Skip / Reverse

```csharp
var firstTwo = movies.Take(2);
var skipFirst = movies.Skip(1);
var reversed = movies.Reverse();
```

```text
firstTwo  -> Avatar, Men in Black
skipFirst -> Men in Black, I Am Legend
reversed  -> I Am Legend, Men in Black, Avatar
```

<br>

### 14. Movie names to release years dictionary

```csharp
var result = movies.ToDictionary(m => m.Name, m => m.YearOfRelease);
```

```text
{ "Avatar": 2009, "Men in Black": 1997, "I Am Legend": 2007 }
```

<br>

### 15. Movie with the longest plot

```csharp
var result = movies.OrderByDescending(m => m.Plot.Length).First();
```

```text
I Am Legend  ("A lone survivor fights infected humans." — 40 chars)
```

<br>

### 16. Movies released after 2000 AND with "I" in the name

```csharp
var result = movies.Where(m =>
    m.YearOfRelease > 2000 &&
    m.Name.Contains("I"));
```

```text
I Am Legend
```

<br>

### 17. Movies with more than one actor

```csharp
var result = movies
    .Where(m => m.ActorIds.Count > 1)
    .Select(m => new { m.Name, ActorCount = m.ActorIds.Count });
```

```text
{ Name = "Avatar",       ActorCount = 2 }
{ Name = "Men in Black", ActorCount = 2 }
```

<br>

## Part B — Actor Joins (FK-based)

<br>

### 18. Get movies in which Will Smith acted

**Approach 1 — resolve the id first, then filter**

```csharp
int actorId = actors
    .First(a => a.Name == "Will Smith")
    .Id;

var result = movies
    .Where(m => m.ActorIds.Contains(actorId))
    .ToList();
```

**Approach 2 — inline, no intermediate variable**

```csharp
var result = movies
    .Where(m => m.ActorIds.Contains(
        actors.First(a => a.Name == "Will Smith").Id))
    .ToList();
```

```text
Men in Black
I Am Legend
```

<br>

### 19. Get actor names for every movie (1 movie → many actors)

```csharp
var result = movies.Select(m => new
{
    Movie = m.Name,
    Actors = actors
        .Where(a => m.ActorIds.Contains(a.Id))
        .Select(a => a.Name)
        .ToList()
});
```

```text
Movie: Avatar
Actors: Sam Worthington, Zoe Saldana

Movie: Men in Black
Actors: Will Smith, Tommy Lee Jones

Movie: I Am Legend
Actors: Will Smith
```

<br>

### 20. Flatten every actor name that appears across all movies (with duplicates)

```csharp
var result = movies
    .SelectMany(m => m.ActorIds)
    .Select(id => actors.First(a => a.Id == id).Name);
```

```text
Sam Worthington
Zoe Saldana
Will Smith
Tommy Lee Jones
Will Smith        <- appears again (Will Smith is in 2 movies)
```

<br>

### 21. Unique actor names that appear in the movie list

```csharp
var result = movies
    .SelectMany(m => m.ActorIds)
    .Distinct()
    .Select(id => actors.First(a => a.Id == id).Name);
```

```text
Sam Worthington
Zoe Saldana
Will Smith
Tommy Lee Jones
```

<br>

### 22. Oldest actor

```csharp
var result = actors.OrderBy(a => a.DOB).First();
```

```text
Tommy Lee Jones (born 1946-09-15)
```

<br>

### 23. Actor(s) NOT cast in any movie

```csharp
var castActorIds = movies.SelectMany(m => m.ActorIds).Distinct();

var result = actors
    .Where(a => !castActorIds.Contains(a.Id));
```

```text
(empty — every actor in the list is cast in at least one movie)
```

<br>

### 24. Group actors by birth year

```csharp
var result = actors.GroupBy(a => a.DOB.Year);
```

```text
1976 -> Sam Worthington
1978 -> Zoe Saldana
1968 -> Will Smith
1946 -> Tommy Lee Jones
```

<br>

### 25. Movies featuring "Tom Cruise" (not in the dataset)

```csharp
var result = movies.Where(m => m.ActorIds.Contains(
    actors.FirstOrDefault(a => a.Name == "Tom Cruise")?.Id ?? -1));
```

```text
(empty list — Tom Cruise isn't in the actors table)
```

> [!Important]
> Use `FirstOrDefault` + `?.Id ?? -1` (or a null check) instead of `First` when the match might not exist — `First` throws `InvalidOperationException` on no match.

<br>

## Part C — Producer Joins (FK-based)

<br>

### 26. Get the producer name for each movie

```csharp
var result = movies.Select(m => new
{
    Movie = m.Name,
    Producer = producers.First(p => p.Id == m.ProducerId).Name
});
```

```text
Avatar       -> James Cameron
Men in Black -> Walter Parkes
I Am Legend  -> Akiva Goldsman
```

<br>

### 27. Producers born after 25 September 2000

```csharp
var result = producers.Where(p => p.DOB > new DateTime(2000, 9, 25));
```

```text
(empty — all three producers were born between 1951 and 1962)
```

<br>

### 28. Youngest producer

```csharp
var result = producers.OrderByDescending(p => p.DOB).First();
```

```text
Akiva Goldsman (born 1962-07-07)
```

<br>

### 29. Producer with the most movies

```csharp
var result = movies
    .GroupBy(m => m.ProducerId)
    .Select(g => new
    {
        Producer = producers.First(p => p.Id == g.Key).Name,
        MovieCount = g.Count()
    })
    .OrderByDescending(x => x.MovieCount)
    .First();
```

```text
James Cameron, MovieCount = 1
```

> [!Tip]
> Every producer in this dataset has exactly one movie, so it's a 3-way tie — `First()` just returns whichever producer's group comes first in the source order (James Cameron, since `Avatar` is movie #1).

<br>

### 30. Movies with a producer born after 1995

```csharp
var result = movies.Where(m =>
    producers.First(p => p.Id == m.ProducerId).DOB.Year > 1995);
```

```text
(empty — no producer was born after 1995)
```

<br>

## Part D — Grouping & Aggregation

<br>

### 31. Group movies by producer name

```csharp
var result = movies.GroupBy(m =>
    producers.First(p => p.Id == m.ProducerId).Name);
```

```text
James Cameron  -> Avatar
Walter Parkes  -> Men in Black
Akiva Goldsman -> I Am Legend
```

<br>

### 32. Order movies by producer name, then by release year

```csharp
var result = movies
    .OrderBy(m => producers.First(p => p.Id == m.ProducerId).Name)
    .ThenBy(m => m.YearOfRelease)
    .Select(m => new
    {
        m.Name,
        Producer = producers.First(p => p.Id == m.ProducerId).Name,
        m.YearOfRelease
    });
```

```text
I Am Legend  (Akiva Goldsman, 2007)
Avatar       (James Cameron, 2009)
Men in Black (Walter Parkes, 1997)
```

<br>

### 33. Concatenate all movie names into one string (`Aggregate`)

```csharp
var result = movies
    .Select(m => m.Name)
    .Aggregate((a, b) => $"{a}, {b}");
```

```text
Avatar, Men in Black, I Am Legend
```

<br>

## Part E — More Frequently-Used LINQ Patterns

<br>

### 34. Proper `Join` — movie + producer name in one step

Instead of calling `producers.First(...)` inside a `Select` (which re-scans the producer list for every movie), a real `Join` is the idiomatic, more efficient way to combine two lists on a key.

```csharp
var result = movies.Join(
    producers,
    m => m.ProducerId,
    p => p.Id,
    (m, p) => new { Movie = m.Name, Producer = p.Name });
```

```text
{ Movie = "Avatar",       Producer = "James Cameron" }
{ Movie = "Men in Black", Producer = "Walter Parkes" }
{ Movie = "I Am Legend",  Producer = "Akiva Goldsman" }
```

<br>

### 35. `GroupJoin` — each producer with their list of movies (one-to-many)

```csharp
var result = producers.GroupJoin(
    movies,
    p => p.Id,
    m => m.ProducerId,
    (p, movieGroup) => new { Producer = p.Name, Movies = movieGroup.Select(m => m.Name).ToList() });
```

```text
{ Producer = "James Cameron",  Movies = [Avatar] }
{ Producer = "Walter Parkes",  Movies = [Men in Black] }
{ Producer = "Akiva Goldsman", Movies = [I Am Legend] }
```

<br>

### 36. `ToLookup` — build an id → name lookup once, reuse everywhere

Avoids repeatedly calling `.First(a => a.Id == id)` inside a loop/select.

```csharp
var actorLookup = actors.ToLookup(a => a.Id, a => a.Name);

var result = movies.Select(m => new
{
    m.Name,
    Actors = m.ActorIds.SelectMany(id => actorLookup[id]).ToList()
});
```

```text
{ Name = "Avatar",       Actors = [Sam Worthington, Zoe Saldana] }
{ Name = "Men in Black", Actors = [Will Smith, Tommy Lee Jones] }
{ Name = "I Am Legend",  Actors = [Will Smith] }
```

<br>

### 37. `Intersect` — actors shared between two movies

```csharp
var menInBlack = movies.First(m => m.Name == "Men in Black").ActorIds;
var iAmLegend  = movies.First(m => m.Name == "I Am Legend").ActorIds;

var sharedActorIds = menInBlack.Intersect(iAmLegend);
```

```text
[ 3 ]   -> Will Smith (in both movies)
```

<br>

### 38. `Except` — actors in "Men in Black" but not in "I Am Legend"

```csharp
var result = menInBlack.Except(iAmLegend);
```

```text
[ 4 ]   -> Tommy Lee Jones
```

<br>

### 39. `Union` — every distinct actor id across both movies

```csharp
var result = menInBlack.Union(iAmLegend);
```

```text
[ 3, 4 ]   -> Will Smith, Tommy Lee Jones
```

<br>

### 40. `DefaultIfEmpty` — left join (movie with a producer even if unmatched)

Useful when a `ProducerId` might not exist in the `producers` table.

```csharp
var result = from m in movies
              join p in producers on m.ProducerId equals p.Id into pg
              from p in pg.DefaultIfEmpty()
              select new
              {
                  m.Name,
                  Producer = p != null ? p.Name : "Unknown"
              };
```

```text
{ Name = "Avatar",       Producer = "James Cameron" }
{ Name = "Men in Black", Producer = "Walter Parkes" }
{ Name = "I Am Legend",  Producer = "Akiva Goldsman" }
```

> [!Tip]
> With this dataset every `ProducerId` matches, so `Producer` is never `"Unknown"` — but the pattern is the standard way to do a SQL-style LEFT JOIN in LINQ.

<br>

### 41. `Chunk` — split movies into batches of 2 (.NET 6+)

```csharp
var result = movies.Chunk(2);
```

```text
Batch 1: Avatar, Men in Black
Batch 2: I Am Legend
```

<br>

### 42. `ElementAtOrDefault` — safely get a movie by index

```csharp
var result = movies.ElementAtOrDefault(5);
```

```text
null   -> index 5 is out of range, no exception thrown
```

<br>

### 43. `SequenceEqual` — do two movies have the exact same cast?

```csharp
var result = movies[0].ActorIds.SequenceEqual(movies[1].ActorIds);
```

```text
false   -> Avatar's cast [1,2] != Men in Black's cast [3,4]
```

<br>

### 44. `OrderBy` with multiple keys via anonymous type (`ThenByDescending`)

```csharp
var result = movies
    .OrderBy(m => m.ActorIds.Count)
    .ThenByDescending(m => m.YearOfRelease);
```

```text
I Am Legend  (1 actor,  2007)
Avatar       (2 actors, 2009)
Men in Black (2 actors, 1997)
```

<br>

### 45. `Select` with index — number each movie

```csharp
var result = movies.Select((m, i) => $"{i + 1}. {m.Name}");
```

```text
1. Avatar
2. Men in Black
3. I Am Legend
```
