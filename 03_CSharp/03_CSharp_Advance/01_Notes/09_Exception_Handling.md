
# 9 Exception Handling

## What is Exception Handling?

Exception handling is used to handle runtime errors and prevent applications from crashing.

Examples:

* Invalid user input
* File not found
* Database error
* Network failure
* Divide by zero

<br>

# try-catch

## Syntax

```csharp
try
{
    // Code that may throw exception
}
catch(Exception ex)
{
    // Handle exception
}
```

<br>

## Example

```csharp
try
{
    int number = int.Parse("abc");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
```

Output:

```text
Input string was not in a correct format.
```

<br>

# Multiple Catch Blocks

Used to handle different exceptions separately.

## Syntax

```csharp
try
{
    // risky code
}
catch(FormatException ex)
{
    
}
catch(DivideByZeroException ex)
{
    
}
catch(Exception ex)
{

}
```

<br>

## Example

```csharp
try
{
    int result = 10 / 0;
}
catch(DivideByZeroException ex)
{
    Console.WriteLine("Cannot divide by zero");
}
catch(Exception ex)
{
    Console.WriteLine("Something went wrong");
}
```

Always keep general `Exception` at the end.

<br>

# finally Block

`finally` always executes whether an exception occurs or not.

Used for cleanup:

* Close files
* Close database connections
* Release resources

<br>

## Syntax

```csharp
try
{

}
catch(Exception ex)
{

}
finally
{

}
```

<br>

## Example

```csharp
try
{
    Console.WriteLine("Open File");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("Close File");
}
```

<br>

# throw Keyword

Used to manually throw exceptions.

## Syntax

```csharp
throw new Exception("Message");
```

<br>

## Example

```csharp
public void Register(int age)
{
    if(age < 18)
    {
        throw new Exception(
            "Age must be above 18"
        );
    }
}
```

<br>

# Custom Exceptions

Create your own exception type.

## Step 1: Create Exception Class

```csharp
public class InvalidAgeException : Exception
{
    public InvalidAgeException(string message)
        : base(message)
    {

    }
}
```

<br>

## Step 2: Throw Exception

```csharp
if(age < 18)
{
    throw new InvalidAgeException(
        "Invalid age"
    );
}
```

<br>

## Step 3: Handle Exception

```csharp
try
{
    Register(15);
}
catch(InvalidAgeException ex)
{
    Console.WriteLine(ex.Message);
}
```

<br>

# Exception Properties

## Message

Error information.

```csharp
ex.Message
```

<br>

## StackTrace

Shows where error happened.

```csharp
ex.StackTrace
```

<br>

## InnerException

Stores original exception.

```csharp
ex.InnerException
```

<br>

# Common Exceptions

| Exception                 | Reason                  |
| ------------------------- | ----------------------- |
| NullReferenceException    | Accessing null object   |
| DivideByZeroException     | Divide by zero          |
| FormatException           | Wrong format conversion |
| IndexOutOfRangeException  | Invalid array index     |
| FileNotFoundException     | Missing file            |
| InvalidOperationException | Invalid operation       |

<br>

# using Statement

Automatically releases resources.

Instead of:

```csharp
FileStream file = new FileStream();

try
{

}
finally
{
    file.Dispose();
}
```

Use:

```csharp
using(FileStream file = new FileStream())
{

}
```

Automatically calls `Dispose()`.

<br>

# Defensive Programming

Avoid exceptions when possible.

Bad:

```csharp
int number =
    int.Parse(input);
```

Good:

```csharp
if(int.TryParse(input, out int number))
{
    Console.WriteLine(number);
}
```

<br>

# Best Practices

* Catch specific exceptions first.
* Avoid empty catch blocks.
* Do not catch exceptions you cannot handle.
* Use `finally` for cleanup.
* Prefer `TryParse()` for expected failures.
* Throw meaningful exceptions.
* Log exceptions in real applications.

<br>

# Common Flow

```text
try
 |
Error?
 |
catch
 |
finally
 |
Continue Program
```

<br>

# Key Takeaways

* `try` contains risky code.
* `catch` handles exceptions.
* Multiple catch blocks handle specific errors.
* `finally` always executes.
* `throw` creates exceptions manually.
* Custom exceptions represent application-specific errors.
* Use defensive programming to prevent avoidable exceptions.
