## Questions

### 1. Difference between `First` and `FirstOrDefault`
    - `First` returns the first element that matches the condition, like where
        - If nothing found, `First` returns exception, so application crashes
    - If we use `FirstOrDefault`, it returns `null`, therefore application is not breaked

### 2. Where, select, Max, Min does not print or write anything,     

### 3. Use `Join` instead `foreach`, because
   - `foreach` prints **`Console.WriteLine()`** everytime,
   - `Join`, when used inside Console.Writeline(), it prints only once, after joining multiple list eles

```csharp
// Single time print, optimized
Console.WriteLine(string.Join(", ", data));

// Less optimized
data.ToList().ForEach(x => Console.WriteLine(x));
```

### 4.   `Select()` processes one element at a time.
Suppose the user enters:
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

####  `Select()` processes one element at a time.
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

### 6. Remember, a value type variable default is `0` , `false`, or similar `0 based`, and a reference type is `null`
   
### 5. Why is the `virtual` keyword applicable only at runtime and not at compile time?

- **Method Overloading** happens **within the same class**. The compiler sees all overloaded methods together and chooses the correct one based on the **method signature (number/type of parameters)**. Hence, it is **compile-time polymorphism**.

- **Method Overriding** happens **between a base class and a derived class**. At compile time, the compiler only knows the **reference type**, not the actual object that will be created.

- At **runtime**, the CLR checks the **actual object**. If the base class method is marked with `virtual` and the derived class provides an `override`, the derived class implementation is executed.

- **If `virtual` is not used** in the base class, the derived class **cannot override** the method. Attempting to use `override` results in a compile-time error.

- **If neither `virtual` nor `override` is used**, both classes simply have their own independent methods. The method that executes depends on the **reference type**, not the actual object, so runtime polymorphism does not occur. Compiler reads it as `public new void Method()`

**Example:**

```csharp
Calculator calculator = new AdvancedCalculator();
calculator.GetResult();
```

- With `virtual` + `override` → `AdvancedCalculator.GetResult()` executes.
- Without `virtual` + `override` → `Calculator.GetResult()` executes.
  
