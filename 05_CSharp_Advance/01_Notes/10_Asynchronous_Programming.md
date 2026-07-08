# 10. Async Programming

[Understanding](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/05_CSharp_Advance/01_Notes/Asybc_Understanding.md)

<br>

## What is Async?

Async programming allows a program to perform long-running operations without blocking the main thread.

Commonly used for:

* API calls
* Database operations
* File reading/writing
* Network requests


<br>

<div align = "center">
<img width="350" alt="image" src="https://github.com/user-attachments/assets/8420286b-5c94-4e5a-bf33-a64459bc4466" />
</div>

<br>

> [!Tip]
> Think of ordering food:
>
> ### Sync
>```
> You order food.
>```
> You stand at counter doing nothing for 20 minutes.
>
> ```
> You = blocked
>```
>
> 
> ### Async + await
> You order food.
> 
> They give you a token.
> 
> You sit, talk, use phone.
> 
> When ready, they call you.
> ```
> You = free
>```
> await means:
> 
> > "Continue this method after the result comes, but don't waste the thread while waiting."

<br>

# Synchronous vs Asynchronous

<br>

<div align = "center">
<img width="350" alt="image" src="https://github.com/user-attachments/assets/8420286b-5c94-4e5a-bf33-a64459bc4466" />
 <img width="500" alt="image" src="https://github.com/user-attachments/assets/0b259bde-cd4d-408b-8360-941ba5d65f3f" />
</div>

<br>

## Synchronous

Code waits until the task completes.

```csharp
DownloadFile();

Console.WriteLine("Done");
```

Flow:

```text
Start Download
      |
Wait until finished
      |
Print Done
```

<br>

## Asynchronous

Program can continue while waiting.

```csharp
await DownloadFileAsync();

Console.WriteLine("Done");
```

The thread is not blocked.

<br>

# async Keyword

`async` marks a method as asynchronous.

Syntax:

```csharp
public async Task MethodName()
{

}
```

Example:

```csharp
public async Task GetDataAsync()
{
    
}
```

<br>

# await Keyword

> [!Note]
> A method waits, but the thread does not wait.

`await` waits for an asynchronous operation to finish without blocking the thread.

Syntax:

```csharp
await SomeAsyncMethod();
```

Example:

```csharp
await Task.Delay(3000);
```

<br>

# Basic Async Example

```csharp
public async Task DownloadAsync()
{
    Console.WriteLine("Start");

    await Task.Delay(3000);

    Console.WriteLine("Finished");
}
```

Flow:

```text
Start

(wait 3 seconds)

Finished
```

<br>

# Task

`Task` represents an asynchronous operation.

## No Return Value

```csharp
public async Task SaveAsync()
{
    await Task.Delay(1000);
}
```

Similar to:

```csharp
void
```

but asynchronous.

<br>

# Task<T>

Used when async method returns a value.

Syntax:

```csharp
async Task<returnType>
```

Example:

```csharp
public async Task<int> GetNumberAsync()
{
    await Task.Delay(1000);

    return 10;
}
```

Usage:

```csharp
int result =
    await GetNumberAsync();
```

<br>

# Async Method Naming Convention

Async methods should end with `Async`.

Good:

```csharp
GetUserAsync()

SaveFileAsync()

DownloadDataAsync()
```

Bad:

```csharp
GetUser()

SaveFile()
```

<br>

# Task.Delay()

Creates an asynchronous delay.

Example:

```csharp
await Task.Delay(2000);
```

Waits:

```text
2 seconds
```

Commonly used for testing async behavior.

<br>

# Real Example: API Call

```csharp
public async Task<string> GetUserAsync()
{
    using HttpClient client =
        new HttpClient();

    string result =
        await client.GetStringAsync(
            "https://api.com/users"
        );

    return result;
}
```

<br>

# Multiple Async Tasks

## Sequential Execution

Runs one after another.

```csharp
await Task1();

await Task2();
```

Flow:

```text
Task1 Finish
      |
Task2 Start
```

<br>

## Parallel Execution

Run together.

```csharp
Task task1 = Task1();

Task task2 = Task2();

await Task.WhenAll(
    task1,
    task2
);
```

Both execute at the same time.

<br>

# Task.WhenAll()

Waits for multiple tasks.

```csharp
await Task.WhenAll(tasks);
```

Example:

```csharp
await Task.WhenAll(
    DownloadImageAsync(),
    DownloadVideoAsync()
);
```

<br>

# Task.WhenAny()

Completes when the first task finishes.

```csharp
await Task.WhenAny(
    task1,
    task2
);
```

Used when fastest response is needed.

<br>

# Async Main Method

Modern C# supports async Main.

```csharp
static async Task Main()
{
    await RunAsync();
}
```

<br>

# Exception Handling with Async

Use normal try-catch.

```csharp
try
{
    await GetDataAsync();
}
catch(Exception ex)
{
    Console.WriteLine(
        ex.Message
    );
}
```

<br>

# Avoid async void

Bad:

```csharp
public async void Save()
{

}
```

Good:

```csharp
public async Task SaveAsync()
{

}
```

Use `async void` only for event handlers.

<br>

# Common Async Return Types

| Type    | Use                          |
| ------- | ---------------------------- |
| Task    | Async method without result  |
| Task<T> | Async method returning value |
| void    | Only event handlers          |

<br>

# Real-World Usage

ASP.NET API:

```csharp
public async Task<IActionResult> GetUsers()
{
    var users =
        await database.Users.ToListAsync();

    return Ok(users);
}
```

<br>

# Best Practices

* Use async for I/O operations.
* Always await async methods.
* Name async methods with `Async`.
* Avoid `async void`.
* Use `Task.WhenAll()` for parallel tasks.
* Handle exceptions with try-catch.

<br>

# Common Flow

```text
Call Method

    |

Start Async Work

    |

await

    |

Resume After Completion
```

<br>

# Key Takeaways

* `async` enables asynchronous methods.
* `await` waits without blocking threads.
* `Task` represents async work.
* `Task<T>` returns values asynchronously.
* Async improves application responsiveness.
* Used heavily in APIs, databases, files, and network operations.
