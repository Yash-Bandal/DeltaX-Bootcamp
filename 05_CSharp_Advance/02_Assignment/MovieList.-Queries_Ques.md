# Data 
```js
MovieList
[
    {
        Name : "Avatar",
        YearOfRelease : 2009,
        Plot : "Humans explore Pandora.",

        Actors :
        [
            {
                Id : 1,
                Name : "Sam Worthington"
            },
            {
                Id : 2,
                Name : "Zoe Saldana"
            }
        ],

        Producer :
        {
            Id : 1,
            Name : "James Cameron"
        }
    },

    {
        Name : "Men in Black",
        YearOfRelease : 1997,
        Plot : "Secret agents protect Earth.",

        Actors :
        [
            {
                Id : 3,
                Name : "Will Smith"
            },
            {
                Id : 4,
                Name : "Tommy Lee Jones"
            }
        ],

        Producer :
        {
            Id : 2,
            Name : "Walter Parkes"
        }
    }
]
```

# LINQ Practice using IMDB Movie Dataset

The following examples assume:

```csharp
List<Movie> MovieList;
```

where each `Movie` contains:

- Name
- YearOfRelease
- Plot
- Producer
- List<Actor>

<br>

# 1. Get all movies

### Query

```csharp
var result = MovieList;
```

### Output

```text
[
    Avatar,
    Avatar 2,
    Men in Black,
    Oppenheimer
]
```

<br>

# 2. Get all movie names

### Query

```csharp
var result = MovieList
    .Select(m => m.Name);
```

### Output

```text
[
    "Avatar",
    "Avatar 2",
    "Men in Black",
    "Oppenheimer"
]
```

<br>

# 3. Get all movie names and years

### Query

```csharp
var result = MovieList
    .Select(m => $"{m.Name} - {m.YearOfRelease}");
```

### Output

```text
[
    "Avatar - 2009",
    "Avatar 2 - 2022",
    "Men in Black - 1997"
]
```

<br>

# 4. Get movies released after 2010

### Query

```csharp
var result = MovieList
    .Where(m => m.YearOfRelease > 2010);
```

### Output

```text
[
    Avatar 2,
    Oppenheimer
]
```

<br>

# 5. Get movie names released after 2010

### Query

```csharp
var result = MovieList
    .Where(m => m.YearOfRelease > 2010)
    .Select(m => m.Name);
```

### Output

```text
[
    "Avatar 2",
    "Oppenheimer"
]
```

<br>

# 6. Get movies produced by James Cameron

### Query

```csharp
var result = MovieList
    .Where(m => m.Producer.Name == "James Cameron");
```

### Output

```text
[
    Avatar,
    Avatar 2
]
```

<br>

# 7. Get names of movies produced by James Cameron

### Query

```csharp
var result = MovieList
    .Where(m => m.Producer.Name == "James Cameron")
    .Select(m => m.Name);
```

### Output

```text
[
    "Avatar",
    "Avatar 2"
]
```

<br>

# 8. Get first Avatar movie

### Query

```csharp
var result = MovieList
    .FirstOrDefault(m => m.Name.Contains("Avatar"));
```

### Output

```text
Movie
{
    Name = "Avatar"
}
```

<br>

# 9. Check if any movie released after 2025

### Query

```csharp
var result = MovieList
    .Any(m => m.YearOfRelease > 2025);
```

### Output

```text
false
```

<br>

# 10. Check whether all movies have producers

### Query

```csharp
var result = MovieList
    .All(m => m.Producer != null);
```

### Output

```text
true
```

<br>

# 11. Count all movies

### Query

```csharp
var result = MovieList.Count();
```

### Output

```text
4
```

<br>

# 12. Count movies after 2010

### Query

```csharp
var result = MovieList
    .Count(m => m.YearOfRelease > 2010);
```

### Output

```text
2
```

<br>

# 13. Sort movies by release year

### Query

```csharp
var result = MovieList
    .OrderBy(m => m.YearOfRelease);
```

### Output

```text
Men in Black
Avatar
Avatar 2
Oppenheimer
```

<br>

# 14. Sort newest movies first

### Query

```csharp
var result = MovieList
    .OrderByDescending(m => m.YearOfRelease);
```

### Output

```text
Oppenheimer
Avatar 2
Avatar
Men in Black
```

<br>

# 15. Sort by Producer then Release Year

### Query

```csharp
var result = MovieList
    .OrderBy(m => m.Producer.Name)
    .ThenBy(m => m.YearOfRelease);
```

### Output

```text
Grouped by producer, then oldest to newest
```

<br>

# 16. Get all producer names

### Query

```csharp
var result = MovieList
    .Select(m => m.Producer.Name);
```

### Output

```text
[
    James Cameron,
    James Cameron,
    Walter Parkes,
    Christopher Nolan
]
```

<br>

# 17. Get unique producer names

### Query

```csharp
var result = MovieList
    .Select(m => m.Producer.Name)
    .Distinct();
```

### Output

```text
[
    James Cameron,
    Walter Parkes,
    Christopher Nolan
]
```

<br>

# 18. Get all actor collections

### Query

```csharp
var result = MovieList
    .Select(m => m.Actors);
```

### Output

```text
[
    [Sam Worthington, Zoe Saldana],
    [Will Smith, Tommy Lee Jones]
]
```
Type:

```csharp
IEnumerable<List<Actor>>
```

## Flatten all actors (SelectMany)
```csharp
var res = MovieList
    .SelectMany(m => m.Actors)
    .Distinct()
    .OrderBy(a => a)
    .ToList();
```
Result:
```
Sam Worthington
Tommy Lee Jones
Will Smith
Zoe Saldana
```


<br>

# 19. Get all actors from every movie

### Query

```csharp
var result = MovieList
    .SelectMany(m => m.Actors);
```

### Output

```text
[
    Sam Worthington,
    Zoe Saldana,
    Will Smith,
    Tommy Lee Jones
]
```

Type:

```csharp
IEnumerable<Actor>
```

<br>

# 20. Get names of every actor

### Query

```csharp
var result = MovieList
    .SelectMany(m => m.Actors)
    .Select(a => a.Name);
```

### Output

```text
[
    Sam Worthington,
    Zoe Saldana,
    Will Smith,
    Tommy Lee Jones
]
```

<br>

# 21. Get unique actor names

### Query

```csharp
var result = MovieList
    .SelectMany(m => m.Actors)
    .Select(a => a.Name)
    .Distinct();
```

### Output

```text
[
    Sam Worthington,
    Zoe Saldana,
    Will Smith,
    Tommy Lee Jones
]
```

<br>

# 22. Get movies in which Will Smith acted

### Query

```csharp
var result = MovieList
    .Where(m => m.Actors.Any(a => a.Name == "Will Smith"))
    .Select(m => m.Name);
```

### Output

```text
[
    Men in Black,
    I Am Legend
]
```

<br>

# 23. Group movies by producer

### Query

```csharp
var result = MovieList
    .GroupBy(m => m.Producer.Name);
```

### Output

```text
James Cameron
    Avatar
    Avatar 2

Walter Parkes
    Men in Black
```

<br>

# 24. Group movies by release year

### Query

```csharp
var result = MovieList
    .GroupBy(m => m.YearOfRelease);
```

### Output

```text
1997
    Men in Black

2009
    Avatar

2022
    Avatar 2
```

<br>

# 25. Get latest release year

### Query

```csharp
var result = MovieList
    .Max(m => m.YearOfRelease);
```

### Output

```text
2023
```

<br>

# 26. Get oldest release year

### Query

```csharp
var result = MovieList
    .Min(m => m.YearOfRelease);
```

### Output

```text
1997
```

<br>

# 27. Get average release year

### Query

```csharp
var result = MovieList
    .Average(m => m.YearOfRelease);
```

### Output

```text
2012.75
```

<br>

# 28. Sum of all release years

### Query

```csharp
var result = MovieList
    .Sum(m => m.YearOfRelease);
```

### Output

```text
8051
```

<br>

# 29. Take first two movies

### Query

```csharp
var result = MovieList
    .Take(2);
```

### Output

```text
[
    Avatar,
    Avatar 2
]
```

<br>

# 30. Skip first two movies

### Query

```csharp
var result = MovieList
    .Skip(2);
```

### Output

```text
[
    Men in Black,
    Oppenheimer
]
```

<br>

# 31. Pagination (Page 2)

### Query

```csharp
var result = MovieList
    .Skip(10)
    .Take(10);
```

### Output

```text
Movies 11–20
```

<br>

# 32. Convert to Dictionary

### Query

```csharp
var result = MovieList
    .ToDictionary(
        m => m.Name,
        m => m.YearOfRelease);
```

### Output

```text
{
    Avatar : 2009,
    Avatar 2 : 2022,
    Men in Black : 1997
}
```

<br>

# 33. Reverse current order

### Query

```csharp
var result = MovieList
    .Reverse();
```

### Output

```text
[
    Oppenheimer,
    Men in Black,
    Avatar 2,
    Avatar
]
```

<br>

# 34  Latest movie
### Query
```csharp
var res = MovieList
    .OrderByDescending(m => m.YearOfRelease)
    .FirstOrDefault();
```
### Output
```
{
    Name = Avatar 2,
    YearOfRelease = 2022,
    Producer = James Cameron
}
```

<br>


# 35 Movies with more than one actor
### Query
```csharp
var res = MovieList
    .Where(m => m.Actors.Count > 1)
    .Select(m => new
    {
        m.Name,
        ActorCount = m.Actors.Count
    })
    .ToList();
```
### Output
```
[
    { Name = Avatar, ActorCount = 2 },
    { Name = Avatar 2, ActorCount = 2 },
    { Name = Men in Black, ActorCount = 2 }
]
```

<br>

# 36 Order by producer, then release year
### Query
```csharp
var res = MovieList
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
### Output
```
[
    Avatar (James Cameron, 2009),
    Avatar 2 (James Cameron, 2022),
    I Am Legend (Akiva Goldsman, 2007),
    Men in Black (Walter Parkes, 1997)
]
```

<br>

# 37. Movies after 2000 and with name avatar
Using `AND (&&)`

Filter movies:

Released after 2000\
Name contains "Avatar"
```csharp
var filteredMovies = MovieList.Where(m =>
    m.YearOfRelease > 2000 &&
    m.Name.Contains("Avatar"));
```
