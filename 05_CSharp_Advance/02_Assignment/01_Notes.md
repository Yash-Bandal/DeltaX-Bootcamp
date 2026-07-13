## Questions

1. Difference between `First` and `FirstOrDefault`
    - `First` returns the first element that matches the condition, like where
        - If nothing found, `First` returns exception, so application crashes
    - If we use `FirstOrDefault`, it returns `null`, therefore application is not breaked

2. Where, select, Max, Min does not print or write anything,     

3. Use `Join` instead `foreach`, because
   - `foreach` prints **`Console.WriteLine()`** everytime,
   - `Join`, when used inside Console.Writeline(), it prints only once, after joining multiple list eles

    ```csharp
    // Single time print, optimized
    Console.WriteLine(string.Join(", ", data));
    
    // Less optimized
    data.ToList().ForEach(x => Console.WriteLine(x));
    ```

4. Suppose the user enters:
```
1,2,3
```
Step 1: Split()
```csharp
actorInput.Split(',')
```
Produces:
```
["1", "2", "3"]
```
This is a `string[]`.

  
 - `Select()` processes one element at a time.
   ```csharp
   .Select(id =>
    {
        if (!int.TryParse(id.Trim(), out int parseId))
        {
            throw new InvalidMovieException("Invalid Actor ID Format");
        }
    
        return parseId;
    })
    ```
   Iteration 1:
    ```
    id = "1"
    
    parseId = 1
    
    return 1
    ```
    Iteration 2:
    ```
    id = "2"
    
    parseId = 2
    
    return 2
    ```
