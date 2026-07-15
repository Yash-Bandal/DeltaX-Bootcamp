### 1. Why `ToList()`
LINQ, like where returns [IEnumerable](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/Concepts/01_Definations.md#definitions), which is different from LIST, if you want to return List, use `.ToList()`
```csharp
List<string> result = movies
    .Where(m => m.StartsWith("A")) //returns IEnumerable
    .ToList();  //returns List
```
