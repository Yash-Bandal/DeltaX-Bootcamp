### 1. Why `ToList()`
LINQ, like where returns [IEnumerable](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/Concepts/01_Definations.md#definitions), which is different from LIST, if you want to return List, use `.ToList()`
```csharp
List<string> result = movies
    .Where(m => m.StartsWith("A")) //returns IEnumerable
    .ToList();  //returns List
```

### 2. Why catch the exception only in Program.cs?
MovieService detects errors, but Program.cs decides how to present them to the user. This separates business logic from user interface.



### Structure
1. Service Layer - Used for Business logic, implementations and validations
2. Program.cs - Entry exit point, input output show (thus we show exception here) in console app
