# C# Intermediate Interview Notes

> **Interview Answer Formula (15–20 sec)**
>
> 1. Definition
> 2. Difference / Why
> 3. One Example
>
> If the interviewer wants more, then explain in detail.

<br>



# 1. First vs FirstOrDefault

## Definition

- `First()` returns the first matching element.
- `FirstOrDefault()` returns the first matching element or the default value.

## Difference

| First | FirstOrDefault |
|--------|----------------|
| Throws exception if no data | Returns default value (`null`, `0`, `false`) |
| Use when data must exist | Use when data may not exist |

## Example

```csharp
var student = students.First(x => x.Id == 1);

var student = students.FirstOrDefault(x => x.Id == 1);
```

## Keywords

- Exception
- Default Value
- Null Safety


<br>



# 2. Where() vs Select()

## Definition

- `Where()` filters data.
- `Select()` transforms data.

## Example

```csharp
var adults = people.Where(x => x.Age >= 18);

var names = people.Select(x => x.Name);
```

## Keywords

- Filtering
- Projection
- LINQ


<br>



# 3. Why use string.Join() instead of foreach?

## Definition

`string.Join()` joins multiple elements into one string.

## Why?

- Single `Console.WriteLine()`
- Cleaner code
- Better readability

## Example

```csharp
Console.WriteLine(string.Join(", ", names));
```

instead of

```csharp
foreach(var name in names)
{
    Console.WriteLine(name);
}
```

## Keywords

- Single Output
- Readability


<br>



# 4. How Select() works?

Suppose user enters

```
1,2,3
```

### Step 1

```csharp
Split(',')
```

Produces

```
["1","2","3"]
```

### Step 2

```csharp
.Select(id => int.Parse(id))
```

Processes

```
"1"

↓

1

"2"

↓

2

"3"

↓

3
```

Final Result

```
[1,2,3]
```

## Keywords

- Projection
- One element at a time


<br>



# 5. Default Values

## Value Types

```
int      → 0

double   → 0

bool     → false

char     → '\0'
```

## Reference Types

```
null
```

## Keywords

- Value Type
- Reference Type


<br>



# 6. Why virtual works only at Runtime?

## Definition

`virtual` enables Runtime Polymorphism.

## Why?

Compiler only knows the **Reference Type**.

CLR checks the **Actual Object** during Runtime.

## Without virtual

Method depends on

```
Reference Type
```

## With virtual

Method depends on

```
Actual Object
```

## Example

```csharp
Calculator c = new AdvancedCalculator();

c.GetResult();
```

With `virtual`

```
AdvancedCalculator.GetResult()
```

Without `virtual`

```
Calculator.GetResult()
```

## Keywords

- Runtime
- CLR
- Polymorphism


<br>


# 7. Abstract Class vs Interface

| Abstract Class | Interface |
|---------------|-----------|
| Represents IS-A relationship | Represents CAN-DO capability |
| Can have fields | Cannot have fields |
| Can have constructors | Cannot have constructors |
| Single inheritance | Multiple interfaces allowed |

## Use

### Abstract

Common base implementation.

Example

```
Shape

↓

Circle

Rectangle
```

### Interface

Contract.

Example

```
Car

Person

Circle

↓

IDrawable
```

## Keywords

- Contract
- Inheritance
- Multiple Inheritance

---
<br>


# 8. new vs override

| override | new |
|----------|-----|
| Replaces parent implementation | Hides parent method |
| Supports Runtime Polymorphism | No Runtime Polymorphism |
| Parent method must be virtual | No requirement |

## Keywords

- virtual
- override
- Method Hiding

<br>



# 9. sealed

## Definition

Stops further overriding.

## Example

```csharp
public sealed override void Show()
{
}
```

## Use

Prevent child classes from changing behavior.


<br>



# 10. static

## Definition

Belongs to the Class.

Not to Objects.

## Why?

Access members without creating objects.

## Example

```csharp
Math.Sqrt(25);

Console.WriteLine();
```

## Keywords

- Class Member
- Shared
- No Object


<br>



# 11. const vs readonly

| const | readonly |
|--------|----------|
| Compile Time | Runtime |
| Initialize immediately | Initialize in constructor |
| Implicitly static | Instance or Static |

## Use

### const

```
PI

MaxMarks
```

### readonly

```
EmployeeId

JoiningDate
```

## Example

```csharp
const double PI = 3.14;

readonly Guid EmployeeId = Guid.NewGuid();
```

## Keywords

- Compile Time
- Runtime
- Constructor


<br>



# Interview Rule

Answer every question in this order:

```
Definition

↓

Difference / Why

↓

One Example
```

Example

**Q: Difference between const and readonly?**

> `const` is a compile-time constant and must be initialized when declared. `readonly` is assigned at runtime, usually in the constructor, and cannot be changed afterward. We use `const` for fixed values like PI and `readonly` for runtime values like EmployeeId.
