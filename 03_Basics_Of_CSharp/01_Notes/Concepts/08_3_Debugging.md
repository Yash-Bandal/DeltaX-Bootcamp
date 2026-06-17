# 8.3 Debugging and Best Practices

<br>

---
> [!Tip]
> 1. Press `F10` to execute 1 line at a time (step over)
> 2. Program stops where we put the breakpoint
> 3. You can move the arrow over the breakpoint, to review again
> 4. Press `F11` to execute 1 line at a time (step Into) - see online
> 5. You also have the `Watch Window` inside the header
>      - Here you can define variables, used inside the code, and observe them change
---
---
> [!Important]
> 1. A real programmer is the one, who considers all test cases
> 2. There is also `Autos` and `Locals` watch window,
>     - Autos show variables to be watched automatically
>     - Locals also same as auto, but show only major ones variables
> 3. Debugging is important maybe some developers spend weeks to get rid of a bug

---

<br>

<div align = "center">
  <img height="150" alt="image" src="https://github.com/user-attachments/assets/a320f865-516f-41a9-ac83-a3dc5a6212b3" />
<img height="150" alt="image" src="https://github.com/user-attachments/assets/51909657-2126-40bc-a308-e48b20267e8f" />
<p>Moving the pointer arrow to breakpoint </p>

  <img width="700" alt="image" src="https://github.com/user-attachments/assets/22e7529e-4560-4e88-8c7b-20a04fac8d1b" />
  <p> Autos and locals</p>
</div>





## What is Debugging?
<div align  = "center">
<img height="150" alt="image" src="https://github.com/user-attachments/assets/1f2c10e7-dbfc-41f5-a48c-380a375af9e2" />
  <img height="150 " alt="image" src="https://github.com/user-attachments/assets/750f5ecc-670e-4ce5-a526-535cf2e9cece" />
</div>



Debugging is the process of finding and fixing errors (bugs) in a program.

Instead of guessing what's wrong, a debugger lets you execute the program one line at a time and inspect values.

Benefits:

* Find bugs quickly
* Understand program flow
* Inspect variable values
* Verify logic

<br>


# Types of Errors

## 1. Syntax Errors

Mistakes in code structure.

Example:

```csharp
Console.WriteLine("Hello")
```

Missing semicolon (`;`).

Detected during compilation.

<br>

## 2. Runtime Errors

Occur while the program is running.

Example:

```csharp
int number = 10;

Console.WriteLine(number / 0);
```

Output:

```text
DivideByZeroException
```

<br>

## 3. Logical Errors

The program runs successfully but produces incorrect results.

Example:

```csharp
int total = 10 + 5 * 2;
```

If you expected `30`, your logic is wrong because operator precedence makes the result `20`.

These are the most difficult bugs to find.

<br>

# Breakpoints

A breakpoint pauses program execution at a specific line.

Visual Studio Shortcut:

```text
F9
```

Example:

```csharp
int age = 21;

Console.WriteLine(age);
```

If a breakpoint is placed on `Console.WriteLine()`, execution stops before that line executes.

Use breakpoints to inspect variables and understand program flow.

<br>

# Starting the Debugger

| Shortcut   | Purpose               |
| ---------- | --------------------- |
| F5         | Start Debugging       |
| Ctrl + F5  | Run Without Debugging |
| Shift + F5 | Stop Debugging        |

<br>

# Stepping Through Code

## Step Over (F10)

Executes the current line and moves to the next line.

If the current line calls a method, the entire method executes without entering it.

```csharp
DisplayMessage();

Console.WriteLine("Done");
```

Pressing **F10** executes `DisplayMessage()` completely and moves to the next line.

<br>

## Step Into (F11)

Enters the method being called.

Example:

```csharp
DisplayMessage();
```

Press **F11** to move inside:

```csharp
void DisplayMessage()
{
    Console.WriteLine("Hello");
}
```

Useful when debugging your own methods.

<br>

## Step Out (Shift + F11)

Finishes the current method and returns to the calling method.

Useful after you've inspected a method and want to continue.

<br>

# Watching Variables

While debugging, Visual Studio shows current variable values.

Example:

```csharp
int x = 10;
int y = 20;

int sum = x + y;
```

During debugging you can inspect:

```text
x = 10
y = 20
sum = 30
```

No need to use `Console.WriteLine()` repeatedly.

<br>

# Locals Window

Shows all local variables in the current method.

Example:

```csharp
int age = 21;
string name = "Yash";
double salary = 50000;
```

Locals Window displays:

```text
age
name
salary
```

Automatically updates as values change.

<br>

# Autos Window

Shows variables related to the current and previous line being executed.

Useful for quickly inspecting only the relevant variables.

<br>

# Call Stack

## What is Call Stack?

The Call Stack shows the order in which methods are called.

Example:

```csharp
Main();

↓

Login();

↓

ValidateUser();
```

Visual:

```text
Main()
   ↓
Login()
   ↓
ValidateUser()
```

If an error occurs inside `ValidateUser()`, the Call Stack helps trace how execution reached that point.

Very useful for debugging large applications.

<br>

# Removing Side Effects

## What are Side Effects?

A side effect is when a method changes something outside its own scope.

Example:

```csharp
int count = 0;

void Increase()
{
    count++;
}
```

Calling `Increase()` changes the external variable `count`.

Too many side effects make programs harder to debug.

<br>

## Best Practice

Prefer methods that return values instead of modifying external variables.

Instead of:

```csharp
int total = 0;

void Add(int number)
{
    total += number;
}
```

Prefer:

```csharp
int Add(int a, int b)
{
    return a + b;
}
```

This makes code easier to understand, test, and debug.

<br>

# Defensive Programming

Defensive programming means writing code that safely handles unexpected situations.

Never assume user input is always correct.

<br>

### Example

Instead of:

```csharp
int age = int.Parse(input);
```

Use:

```csharp
if(int.TryParse(input, out int age))
{
    Console.WriteLine(age);
}
else
{
    Console.WriteLine("Invalid Input");
}
```

<br>

### Null Check

```csharp
if(name != null)
{
    Console.WriteLine(name);
}
```

<br>

### File Exists Check

```csharp
if(File.Exists("data.txt"))
{
    string text =
        File.ReadAllText("data.txt");
}
```

Always validate before using resources.

<br>

# Common Debugging Tips

* Read error messages carefully.
* Use breakpoints instead of excessive `Console.WriteLine()`.
* Debug one problem at a time.
* Check variable values during execution.
* Test edge cases (empty input, null values, invalid data).
* Use `TryParse()` when accepting user input.

<br>

# Common Keyboard Shortcuts

| Shortcut    | Action                |
| ----------- | --------------------- |
| F5          | Start Debugging       |
| Ctrl + F5   | Run Without Debugging |
| Shift + F5  | Stop Debugging        |
| F9          | Toggle Breakpoint     |
| F10         | Step Over             |
| F11         | Step Into             |
| Shift + F11 | Step Out              |

<br>

# Key Takeaways

* Debugging helps identify and fix bugs.
* Errors can be syntax, runtime, or logical.
* Breakpoints pause execution for inspection.
* Use Step Over, Step Into, and Step Out to control execution.
* The Locals and Autos windows display variable values during debugging.
* The Call Stack shows the sequence of method calls.
* Reduce side effects by writing methods that return values instead of modifying external state.
* Defensive programming helps prevent common runtime errors by validating inputs and checking conditions.
