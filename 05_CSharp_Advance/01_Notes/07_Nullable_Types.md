# 7. Nullable Types

## What are Nullable Types?

A nullable type allows a value type to store `null`.

Normally, value types cannot hold `null`.

Example:

```csharp
int age = null;
```

Compilation Error.

Using nullable types:

```csharp
int? age = null;
```

Now the variable can store either:

* A value
* `null`

<br>

## Why Do We Need Nullable Types?

Sometimes data is unknown or optional.

Examples:

* Date of Birth not provided
* Discount not applied
* User has not entered Age
* Order not shipped yet

Instead of assigning dummy values like `0` or `-1`, use `null`.

<br>

## Nullable Syntax

### Short Syntax

```csharp
int? age = null;

double? price = null;

bool? isActive = null;
```

<br>

### Full Syntax

```csharp
Nullable<int> age = null;
```

Equivalent to:

```csharp
int? age = null;
```

The `?` syntax is preferred.

<br>

## Assigning Values

```csharp
int? age = null;

age = 21;

Console.WriteLine(age);
```

Output:

```text
21
```

<br>

## Checking for Null

```csharp
int? age = null;

if (age == null)
{
    Console.WriteLine("Age not available");
}
```

<br>

## HasValue Property

Checks whether a nullable variable contains a value.

```csharp
int? age = 21;

Console.WriteLine(age.HasValue);
```

Output:

```text
True
```

Example:

```csharp
int? age = null;

Console.WriteLine(age.HasValue);
```

Output:

```text
False
```

<br>

## Value Property

Returns the actual value.

```csharp
int? age = 25;

Console.WriteLine(age.Value);
```

Output:

```text
25
```

### Warning

```csharp
int? age = null;

Console.WriteLine(age.Value);
```

Throws:

```text
InvalidOperationException
```

Always check `HasValue` first.

<br>

## Null-Coalescing Operator (??)

Provides a default value if the variable is `null`.

```csharp
int? age = null;

int result = age ?? 18;

Console.WriteLine(result);
```

Output:

```text
18
```

Another example:

```csharp
string? name = null;

Console.WriteLine(name ?? "Guest");
```

Output:

```text
Guest
```

This is one of the most commonly used nullable features in real-world applications.

<br>

## Null-Coalescing Assignment (??=)

Assigns a value only if the variable is `null`.

```csharp
string? name = null;

name ??= "Guest";

Console.WriteLine(name);
```

Output:

```text
Guest
```

If the variable already has a value:

```csharp
string? name = "Yash";

name ??= "Guest";
```

Output:

```text
Yash
```

<br>

## GetValueOrDefault()

Returns the stored value.

If `null`, returns the default value of the type.

```csharp
int? age = null;

Console.WriteLine(age.GetValueOrDefault());
```

Output:

```text
0
```

Custom default:

```csharp
Console.WriteLine(age.GetValueOrDefault(18));
```

Output:

```text
18
```

<br>

## Nullable Reference Types (C# 8+)

Starting from C# 8, reference types can also be marked as nullable.

```csharp
string? name = null;
```

Non-nullable:

```csharp
string name = "Yash";
```

Nullable:

```csharp
string? name = null;
```

This helps prevent `NullReferenceException`.

<br>

## Null-Conditional Operator (?.)

Safely accesses members of an object.

Without `?.`

```csharp
Console.WriteLine(person.Name);
```

If `person` is `null`, this throws:

```text
NullReferenceException
```

Using `?.`

```csharp
Console.WriteLine(person?.Name);
```

If `person` is `null`, the expression simply returns `null` instead of throwing an exception.

Very common in production code.

<br>

## Null-Forgiving Operator (!)

Tells the compiler:

> "I know this value won't be null."

```csharp
string? name = GetName();

Console.WriteLine(name!.Length);
```

Use only when you're certain the value isn't `null`.

Overusing `!` defeats nullable safety.

<br>

## Real-World Example

Employee Date of Leaving.

```csharp
class Employee
{
    public string Name { get; set; }

    public DateTime? LeavingDate { get; set; }
}
```

If the employee is still working:

```text
LeavingDate = null
```

<br>

## Common Use Cases

Nullable types are commonly used for:

* Optional form fields
* Database columns
* Date of Birth
* Discount Percentage
* Last Login Time
* Deleted Date
* Completed Date
* Shipping Date

<br>

## Nullable Methods Used Frequently

```csharp
HasValue

Value

GetValueOrDefault()

??

??=

?.
```

<br>

## Best Practices

* Use nullable types only when `null` is a valid state.
* Prefer `??` over manually writing `if (value == null)`.
* Check `HasValue` before using `.Value`.
* Avoid excessive use of the null-forgiving operator (`!`).
* Enable Nullable Reference Types in new projects.

<br>

## Interview Questions

### Can value types store null?

No.

Unless they are declared as nullable.

```csharp
int? number;
```

<br>

### Difference between int and int?

| int                | int?              |
| ------------------ | ----------------- |
| Cannot store null  | Can store null    |
| Always has a value | Value is optional |

<br>

### What does ?? do?

Returns the left value if it's not null; otherwise returns the right value.

<br>

### What does ?. do?

Safely accesses members without throwing `NullReferenceException`.

<br>

## Key Takeaways

* Nullable types allow value types to store `null`.
* `?` is shorthand for `Nullable<T>`.
* Use `HasValue` to check for a value.
* `Value` returns the stored value but throws if `null`.
* `??` provides a default value.
* `??=` assigns a value only if the variable is `null`.
* `?.` safely accesses object members.
* Nullable types are widely used in databases, APIs, and enterprise applications.
