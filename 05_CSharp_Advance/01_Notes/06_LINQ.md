# 6. LINQ — Language Integrated Query ( [Examples](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/05_CSharp_Advance/01_Notes/Vault/01_LINQ_Examples.md) )


LINQ lets you query **any collection** using a consistent, readable syntax — directly in C#.

Instead of writing manual loops to filter, sort, and transform data, you write declarative expressions that read almost like English.


<br>

> [!tip]
> **IEnumerable**
> 1. It is the foundation for all collection types in .NET (like arrays and lists)
> 2. IEnumerable makes a collection loopable. It acts as a wrapper that exposes a pointer (called an "enumerator").

<br>


```csharp
// Without LINQ
List<int> result = new List<int>();
foreach (int n in numbers)
{
    if (n > 10)
        result.Add(n * 2);
}

// With LINQ
List<int> result = numbers
    .Where(n => n > 10)
    .Select(n => n * 2)
    .ToList();
```

Same outcome. LINQ version is shorter, readable, and composable.

<br>

# How LINQ Works — Under the Hood

LINQ is built entirely on:
- **Extension methods** on `IEnumerable<T>` (from `System.Linq`)
- **Lambda expressions** as the filter/transform arguments

```csharp
using System.Linq;   // ← brings all LINQ methods into scope

List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

numbers.Where(n => n > 2);   // Where is an extension method on IEnumerable<int>
                              // n => n > 2 is a lambda — the filter
```

> [!important]
> ## Note that..
>  Any class implementing `IEnumerable<T>` gets all LINQ methods automatically:
`List<T>`, `Array`, `Dictionary`, `string`, database results, XML — everything.

<br>


<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/9a8fa71d-a874-46b5-a0cb-16f66ab638d5" />
</div>
<br>

# The Setup — Data Used in All Examples

```csharp
public class Student
{
    public string Name    { get; set; }
    public int    Age     { get; set; }
    public string City    { get; set; }
    public double Marks   { get; set; }
}

List<Student> students = new List<Student>
{
    new Student { Name = "Yash",      Age = 20, City = "Pune",    Marks = 88.5 },
    new Student { Name = "Aarav",     Age = 22, City = "Mumbai",  Marks = 74.0 },
    new Student { Name = "Priya",     Age = 21, City = "Pune",    Marks = 91.0 },
    new Student { Name = "Rohan",     Age = 20, City = "Delhi",   Marks = 65.5 },
    new Student { Name = "Sneha",     Age = 23, City = "Mumbai",  Marks = 82.0 },
    new Student { Name = "Karan",     Age = 21, City = "Pune",    Marks = 55.0 },
    new Student { Name = "Ananya",    Age = 22, City = "Delhi",   Marks = 93.0 },
};
```

<br>

# ─────────────────────────────────────
# FILTERING
# ─────────────────────────────────────

<br>

## `Where` — filter by condition

Returns elements that match a predicate.

```csharp
var puneStudents = students
    .Where(s => s.City == "Pune");

// Yash, Priya, Karan
```

Multiple conditions:

```csharp
var topPune = students
    .Where(s => s.City == "Pune" && s.Marks > 80);

// Yash (88.5), Priya (91.0)
```

<br>

## `OfType<T>` — filter by type

Useful with mixed collections.

```csharp
List<object> mixed = new List<object> { 1, "hello", 2, "world", 3 };

var strings = mixed.OfType<string>();   // "hello", "world"
var ints    = mixed.OfType<int>();      // 1, 2, 3
```

<br>

# ─────────────────────────────────────
# PROJECTION
# ─────────────────────────────────────

<br>

## `Select` — transform each element

Maps each element to something new.

```csharp
// Get just the names
var names = students.Select(s => s.Name);
// Yash, Aarav, Priya, Rohan, Sneha, Karan, Ananya

// Transform to a new shape
var summary = students.Select(s => new
{
    s.Name,
    Pass = s.Marks >= 60
});
// { Name = "Yash", Pass = True }
// { Name = "Karan", Pass = False }
// ...
```

<br>

## `SelectMany` — flatten nested collections

When each element contains a collection, `SelectMany` flattens them into one sequence.

```csharp
List<List<int>> matrix = new List<List<int>>
{
    new List<int> { 1, 2, 3 },
    new List<int> { 4, 5, 6 },
    new List<int> { 7, 8, 9 }
};

var flat = matrix.SelectMany(row => row);
// 1, 2, 3, 4, 5, 6, 7, 8, 9
```

<br>

# ─────────────────────────────────────
# ORDERING
# ─────────────────────────────────────

<br>

## `OrderBy` / `OrderByDescending` — sort

```csharp
// Sort by marks ascending
var byMarks = students.OrderBy(s => s.Marks);
// Karan(55), Rohan(65.5), Aarav(74), Sneha(82), Yash(88.5), Priya(91), Ananya(93)

// Sort by marks descending
var topFirst = students.OrderByDescending(s => s.Marks);
// Ananya(93), Priya(91), Yash(88.5)...
```

<br>

## `ThenBy` / `ThenByDescending` — secondary sort

```csharp
var sorted = students
    .OrderBy(s => s.City)           // primary — alphabetical city
    .ThenByDescending(s => s.Marks); // secondary — best marks first within city

// Delhi:  Ananya(93), Rohan(65.5)
// Mumbai: Sneha(82),  Aarav(74)
// Pune:   Priya(91),  Yash(88.5), Karan(55)
```

<br>

# ─────────────────────────────────────
# AGGREGATION
# ─────────────────────────────────────

<br>

## `Count` — how many elements

```csharp
int total    = students.Count();                          // 7
int puneCount= students.Count(s => s.City == "Pune");    // 3
```

<br>

## `Sum` / `Average` / `Min` / `Max`

```csharp
double totalMarks = students.Sum(s => s.Marks);          // 549.0
double avgMarks   = students.Average(s => s.Marks);      // 78.43
double lowest     = students.Min(s => s.Marks);          // 55.0
double highest    = students.Max(s => s.Marks);          // 93.0
```

<br>

## `Aggregate` — custom accumulator

Reduces a sequence to a single value using a custom function.

```csharp
// Concatenate all names
string allNames = students.Aggregate("", (acc, s) =>
    acc == "" ? s.Name : acc + ", " + s.Name);

// Yash, Aarav, Priya, Rohan, Sneha, Karan, Ananya
```

<br>

# ─────────────────────────────────────
# ELEMENT RETRIEVAL
# ─────────────────────────────────────

<br>

## `First` / `FirstOrDefault`

```csharp
// First matching element — throws if none found
Student first = students.First(s => s.City == "Mumbai");
// Aarav

// Safe version — returns null if none found
Student? maybe = students.FirstOrDefault(s => s.City == "Tokyo");
// null — no exception
```

<br>

## `Last` / `LastOrDefault`

```csharp
Student last = students.Last(s => s.Age == 21);
// Karan (last 21-year-old in the list)
```

<br>

## `Single` / `SingleOrDefault`

Expects **exactly one** result. Throws if zero or more than one match.

```csharp
Student top = students.Single(s => s.Marks == 93.0);
// Ananya — only one with exactly 93.0

Student? check = students.SingleOrDefault(s => s.Marks > 99);
// null — no exception
```

<br>

## `ElementAt`

```csharp
Student third = students.ElementAt(2);   // Priya (index 2)
```

<br>

# ─────────────────────────────────────
# QUANTIFIERS
# ─────────────────────────────────────

<br>

## `Any` — does at least one match?

```csharp
bool hasToppers = students.Any(s => s.Marks > 90);    // True
bool hasKids    = students.Any(s => s.Age < 18);      // False
```

<br>

## `All` — do all match?

```csharp
bool allPass  = students.All(s => s.Marks >= 50);     // True
bool allPune  = students.All(s => s.City == "Pune");  // False
```

<br>

## `Contains` — is this element in the sequence?

```csharp
List<string> cities = new List<string> { "Pune", "Mumbai", "Delhi" };

bool hasPune    = cities.Contains("Pune");     // True
bool hasLondon  = cities.Contains("London");   // False
```

<br>

# ─────────────────────────────────────
# GROUPING
# ─────────────────────────────────────

<br>

## `GroupBy` — group elements by a key

Returns `IEnumerable<IGrouping<TKey, TElement>>` — a sequence of groups.

```csharp
var byCity = students.GroupBy(s => s.City);

foreach (var group in byCity)
{
    Console.WriteLine($"\n{group.Key}:");
    foreach (var s in group)
        Console.WriteLine($"  {s.Name} — {s.Marks}");
}
```

```text
Pune:
  Yash   — 88.5
  Priya  — 91.0
  Karan  — 55.0

Mumbai:
  Aarav  — 74.0
  Sneha  — 82.0

Delhi:
  Rohan  — 65.5
  Ananya — 93.0
```

Group and aggregate together:

```csharp
var cityAverage = students
    .GroupBy(s => s.City)
    .Select(g => new
    {
        City    = g.Key,
        Average = g.Average(s => s.Marks),
        Count   = g.Count()
    })
    .OrderByDescending(x => x.Average);

// Delhi  — Avg: 79.25, Count: 2
// Mumbai — Avg: 78.00, Count: 2
// Pune   — Avg: 78.17, Count: 3
```

<br>

# ─────────────────────────────────────
# JOINING
# ─────────────────────────────────────

<br>

## `Join` — inner join two sequences

```csharp
public class Course
{
    public string StudentName { get; set; }
    public string CourseName  { get; set; }
}

List<Course> courses = new List<Course>
{
    new Course { StudentName = "Yash",  CourseName = "C#"      },
    new Course { StudentName = "Priya", CourseName = "React"   },
    new Course { StudentName = "Aarav", CourseName = "Python"  },
    new Course { StudentName = "Rohan", CourseName = "C#"      },
};

var enrolled = students.Join(
    courses,
    s => s.Name,              // key from students
    c => c.StudentName,       // key from courses
    (s, c) => new             // result shape
    {
        s.Name,
        s.City,
        c.CourseName
    }
);

foreach (var e in enrolled)
    Console.WriteLine($"{e.Name} ({e.City}) → {e.CourseName}");
```

```text
Yash  (Pune)   → C#
Aarav (Mumbai) → Python
Priya (Pune)   → React
Rohan (Delhi)  → C#
```

<br>

## `GroupJoin` — left outer join

Like `Join` but keeps all elements from the left sequence, even if there's no match on the right.

```csharp
var withCourses = students.GroupJoin(
    courses,
    s => s.Name,
    c => c.StudentName,
    (s, courseGroup) => new
    {
        s.Name,
        Courses = courseGroup.Select(c => c.CourseName).DefaultIfEmpty("None")
    }
);
```

<br>

# ─────────────────────────────────────
# SET OPERATIONS
# ─────────────────────────────────────

<br>

## `Distinct` — remove duplicates

```csharp
List<string> cities = students.Select(s => s.City).Distinct().ToList();
// Pune, Mumbai, Delhi
```

<br>

## `Union` / `Intersect` / `Except`

```csharp
List<int> a = new List<int> { 1, 2, 3, 4, 5 };
List<int> b = new List<int> { 3, 4, 5, 6, 7 };

var union     = a.Union(b);      // 1,2,3,4,5,6,7    — all unique items
var intersect = a.Intersect(b);  // 3,4,5             — items in both
var except    = a.Except(b);     // 1,2               — in a but not b
```

<br>

# ─────────────────────────────────────
# PAGING
# ─────────────────────────────────────

<br>

## `Skip` / `Take` — pagination

```csharp
// Page 1 — first 3
var page1 = students.Take(3);

// Page 2 — skip first 3, take next 3
var page2 = students.Skip(3).Take(3);
```

```csharp
// Real pagination pattern
int pageSize   = 3;
int pageNumber = 2;

var page = students
    .OrderBy(s => s.Name)
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize);
```

<br>

## `SkipWhile` / `TakeWhile`

Skip/take based on a condition rather than a count.

```csharp
var numbers = new List<int> { 2, 4, 6, 7, 8, 10 };

var afterOdd  = numbers.SkipWhile(n => n % 2 == 0);  // 7, 8, 10
var untilOdd  = numbers.TakeWhile(n => n % 2 == 0);  // 2, 4, 6
```

<br>

# ─────────────────────────────────────
# MATERIALISATION
# ─────────────────────────────────────

<br>

## `ToList` / `ToArray` / `ToDictionary` / `ToHashSet`

LINQ queries are **lazy** — they don't run until you iterate or materialise.




<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/1662d4cd-41be-4b0a-ad15-658c4bc22335" />
</div>
<br>

```csharp
// Still lazy — query not run yet
var query = students.Where(s => s.Marks > 80);

// Materialise — runs the query NOW
List<Student>            list   = query.ToList();
Student[]                array  = query.ToArray();
Dictionary<string, double> dict = students.ToDictionary(s => s.Name, s => s.Marks);
HashSet<string>          cities = students.Select(s => s.City).ToHashSet();
```

`ToDictionary` example:

```csharp
var marksByName = students.ToDictionary(s => s.Name, s => s.Marks);

Console.WriteLine(marksByName["Yash"]);    // 88.5
Console.WriteLine(marksByName["Ananya"]);  // 93.0
```

<br>

# ─────────────────────────────────────
# DEFERRED EXECUTION — VERY IMPORTANT
# ─────────────────────────────────────

LINQ queries are **not executed when you define them**.

They execute when you:
- Iterate with `foreach`
- Call `ToList()`, `ToArray()`, `ToDictionary()`
- Call an aggregation like `Count()`, `Sum()`, `First()`

```csharp
var query = students.Where(s => s.Marks > 80);
// ← query NOT run yet. Nothing happened.

students.Add(new Student { Name = "Test", Marks = 95.0 });
// ← added a new student AFTER defining the query

var result = query.ToList();
// ← query runs NOW — includes the new student!
Console.WriteLine(result.Count);   // 4 (includes Test)
```

```text
Define query   →   nothing runs
Add new item   →   query still not run
.ToList()      →   NOW it runs — sees the new item
```

> This is called **deferred execution**. The query is a description of what to do, not the result of doing it.

<br>

# ─────────────────────────────────────
# LINQ QUERY SYNTAX vs METHOD SYNTAX
# ─────────────────────────────────────

LINQ has two equivalent syntaxes. Method syntax is more common in practice.

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/7a68f988-1bcc-43cf-a726-b8c83abaca6a" />
</div>
<br>

```csharp
// Method syntax (fluent) — most common
var result = students
    .Where(s => s.City == "Pune")
    .OrderByDescending(s => s.Marks)
    .Select(s => s.Name)
    .ToList();
```

```csharp
// Query syntax (SQL-like)
var result = (from s in students
              where s.City == "Pune"
              orderby s.Marks descending
              select s.Name)
             .ToList();
```

Both produce identical results. The compiler converts query syntax into method syntax calls.

```text
Method syntax  →  more powerful, all LINQ methods available
Query syntax   →  more readable for complex joins and grouping
```

<br>

# Chaining — The LINQ Pipeline

Every LINQ method returns `IEnumerable<T>` — so you can chain indefinitely.

```csharp
var result = students
    .Where(s => s.Age >= 21)           // filter
    .OrderByDescending(s => s.Marks)   // sort
    .Select(s => new                   // project
    {
        s.Name,
        s.Marks,
        Grade = s.Marks >= 90 ? "A" : s.Marks >= 75 ? "B" : "C"
    })
    .Take(3)                           // page
    .ToList();                         // materialise

foreach (var r in result)
    Console.WriteLine($"{r.Name}: {r.Marks} ({r.Grade})");
```

```text
Ananya: 93.0 (A)
Priya:  91.0 (A)
Sneha:  82.0 (B)
```

<br>

# All Major LINQ Methods — Quick Reference

```text
FILTERING          Where, OfType

PROJECTION         Select, SelectMany

ORDERING           OrderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse

AGGREGATION        Count, Sum, Average, Min, Max, Aggregate

ELEMENT            First, FirstOrDefault, Last, LastOrDefault,
                   Single, SingleOrDefault, ElementAt

QUANTIFIERS        Any, All, Contains

GROUPING           GroupBy

JOINING            Join, GroupJoin

SET                Distinct, Union, Intersect, Except

PAGING             Skip, Take, SkipWhile, TakeWhile

MATERIALISE        ToList, ToArray, ToDictionary, ToHashSet
```

<br>


<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/10170542-84fa-40ba-8edb-94299e2e7020" />
</div>
<br>


# Summary

```text
IEnumerable<T>
      │
      ├── .Where()          filter
      ├── .Select()         transform
      ├── .OrderBy()        sort
      ├── .GroupBy()        group
      ├── .Join()           combine
      ├── .First()          single element
      ├── .Any() / .All()   check
      ├── .Count() / .Sum() aggregate
      └── .ToList()         materialise (execute)

All lazy — query runs only when iterated or materialised.
All chainable — every method returns IEnumerable<T>.
All available — any IEnumerable<T> gets every method.
```

- LINQ is extension methods on `IEnumerable<T>` powered by lambdas
- **Deferred execution** — the query is a recipe, not a result
- **Method syntax** is more common; query syntax is more readable for joins
- Chains are the power — filter → sort → project → page → materialise
- Works on lists, arrays, databases (Entity Framework), XML, any `IEnumerable<T>`

<br>

# Interview Questions

### What is LINQ?

Language Integrated Query — a set of extension methods on `IEnumerable<T>` that let you filter, sort, group, and transform collections using a consistent, composable syntax.

### What is deferred execution?

A LINQ query is not run when defined — it runs when iterated or materialised with `ToList()`, `Count()`, etc. This means the query always reflects the current state of the source at execution time.

### What is the difference between `First` and `FirstOrDefault`?

`First` throws `InvalidOperationException` if no element matches. `FirstOrDefault` returns `null` (or the default value) instead. Always use `FirstOrDefault` when a match may not exist.

### What is the difference between `Select` and `SelectMany`?

`Select` maps each element to one result (1-to-1). `SelectMany` maps each element to a collection and flattens all those collections into one sequence (1-to-many, flattened).

### What is the difference between `Where` and `Any`?

`Where` returns a filtered sequence. `Any` returns a `bool` — true if at least one element matches. `Any` is for checking; `Where` is for retrieving.

### Can LINQ work on databases?

Yes — via LINQ to SQL or Entity Framework. The same syntax translates to SQL queries at runtime through expression trees, not lambda execution.
