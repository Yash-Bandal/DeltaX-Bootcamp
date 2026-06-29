# 2. Delegates

A **delegate** is a type that holds a reference to a method.

Just like a variable holds a value, a delegate variable holds a **method** — and you can call that method through the delegate, pass it around, or store it.

```csharp
// A delegate type — defines the method signature it can hold
public delegate void Notify(string message);

// A method that matches that signature
public void SendEmail(string message)
{
    Console.WriteLine($"Email: {message}");
}

// Store the method in the delegate variable
Notify notify = SendEmail;

// Call the method through the delegate
notify("Order confirmed!");   // Email: Order confirmed!
```

> The delegate doesn't care **which** method it points to — only that the method matches the **signature** (return type + parameters).

<br>

# Why Do Delegates Exist?

Without delegates, you hardwire which method gets called:

```csharp
public class OrderProcessor
{
    public void Process(Order order)
    {
        // process order...
        SendEmail(order.Email);      // hardwired — what if you want SMS instead?
    }

    private void SendEmail(string to) { }
}
```

With a delegate, you make the method itself a parameter:

```csharp
public class OrderProcessor
{
    public void Process(Order order, Notify notify)
    {
        // process order...
        notify(order.Email);    // caller decides which method runs
    }
}
```

The caller chooses the behaviour. The class doesn't need to change.

```csharp
processor.Process(order, SendEmail);    // notify by email
processor.Process(order, SendSMS);      // notify by SMS — same Process()
```

<br>

# Declaring a Delegate Type

```csharp
public delegate ReturnType DelegateName(ParameterType param, ...);
```

Examples:

```csharp
public delegate void   Notify(string message);         // void, one string param
public delegate int    Calculate(int a, int b);        // returns int, two int params
public delegate bool   Validate(string input);         // returns bool, one string
public delegate string Transform(string input);        // returns string, one string
```

The delegate type defines the **contract** — any method matching this signature can be assigned to it.

<br>

# Assigning Methods to Delegates


<br>
<div align = "center">
  <img  width = "600" alt="image" src="https://github.com/user-attachments/assets/d0198162-2d88-4401-b346-68805f81e53a" />

</div>
<br>

```csharp
public delegate int Calculate(int a, int b);

// Methods that match the signature
public int Add(int a, int b)      => a + b;
public int Subtract(int a, int b) => a - b;
public int Multiply(int a, int b) => a * b;
```

```csharp
Calculate op;

op = Add;
Console.WriteLine(op(3, 4));    // 7

op = Subtract;
Console.WriteLine(op(10, 3));   // 7

op = Multiply;
Console.WriteLine(op(3, 4));    // 12
```

Same variable `op`. Different methods. Different results.

<br>

# Multicast Delegates — Chaining Methods

A delegate can hold **more than one method** at a time using `+=`.

When invoked, it calls **all** of them in order.

<br>
<div align = "center">
  <img  width = "600" alt="image" src="https://github.com/user-attachments/assets/ef51e6c6-64e9-4a06-9488-2c2b888b1cb8" />


</div>
<br>


```csharp
public delegate void Notify(string message);

public void SendEmail(string msg) => Console.WriteLine($"Email: {msg}");
public void SendSMS(string msg)   => Console.WriteLine($"SMS: {msg}");
public void LogMessage(string msg)=> Console.WriteLine($"Log: {msg}");
```

```csharp
Notify notify = SendEmail;
notify += SendSMS;
notify += LogMessage;

notify("Order shipped!");
```

```text
Email: Order shipped!
SMS: Order shipped!
Log: Order shipped!
```

Remove a method with `-=`:

```csharp
notify -= SendSMS;

notify("Order cancelled!");
```

```text
Email: Order cancelled!
Log: Order cancelled!
```

> Multicast delegates are the foundation of **events** (covered in topic 4).

<br>

# Delegates as Method Parameters

This is the most important use case — passing behaviour into a method.

```csharp
public delegate bool Filter(int number);

public List<int> GetNumbers(List<int> numbers, Filter filter)
{
    var result = new List<int>();

    foreach (int n in numbers)
    {
        if (filter(n))         // call whatever method was passed in
            result.Add(n);
    }

    return result;
}
```

```csharp
bool IsEven(int n)    => n % 2 == 0;
bool IsPositive(int n)=> n > 0;
bool IsLarge(int n)   => n > 100;

var numbers = new List<int> { -5, 2, 0, 7, 150, 42 };

var evens    = GetNumbers(numbers, IsEven);      // 2, 0, 42
var positives= GetNumbers(numbers, IsPositive);  // 2, 7, 150, 42
var large    = GetNumbers(numbers, IsLarge);     // 150
```

One method. Pluggable behaviour. This pattern is the core of LINQ.

<br>

# Built-in Delegate Types — `Action` and `Func`

Writing a custom delegate type for every use case gets tedious.

.NET provides two generic delegates that cover almost all cases:


<br>
<div align = "center">

<img width = "600" alt="image" src="https://github.com/user-attachments/assets/8dd50b9a-6029-404a-bddd-34198b5074ea" />

</div>
<br>


## `Action<T>` — method that returns void

```csharp
Action            →  void method, no parameters
Action<T>         →  void method, one parameter
Action<T1, T2>    →  void method, two parameters
// up to Action<T1...T16>
```

```csharp
Action<string> print = Console.WriteLine;
print("Hello");   // Hello

Action<int, int> add = (a, b) => Console.WriteLine(a + b);
add(3, 4);        // 7
```

## `Func<T, TResult>` — method that returns a value

```csharp
Func<TResult>             →  no params, returns TResult
Func<T, TResult>          →  one param, returns TResult
Func<T1, T2, TResult>     →  two params, returns TResult
// last type param is always the return type
```

```csharp
Func<int, int, int> add     = (a, b) => a + b;
Func<string, int>   length  = s => s.Length;
Func<int, bool>     isEven  = n => n % 2 == 0;

Console.WriteLine(add(3, 4));       // 7
Console.WriteLine(length("hello")); // 5
Console.WriteLine(isEven(6));       // True
```

## `Predicate<T>` — shorthand for `Func<T, bool>`

```csharp
Predicate<int>    isEven = n => n % 2 == 0;
Predicate<string> isLong = s => s.Length > 10;
```

> In practice, use `Action` and `Func` — you rarely need to declare your own delegate type.

<br>

# Full Example — Pluggable Notification

```csharp
public class Order
{
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public decimal Total { get; set; }
}

public class OrderProcessor
{
    public void Process(Order order, Action<string> notify)
    {
        Console.WriteLine($"Processing order — total: {order.Total:C}");

        // notify however the caller decides
        notify($"Your order of {order.Total:C} has been placed.");
    }
}
```

```csharp
var processor = new OrderProcessor();
var order = new Order
{
    CustomerEmail = "yash@example.com",
    CustomerPhone = "+91-9000000000",
    Total = 1499.00m
};

// Email notification
processor.Process(order, msg =>
    Console.WriteLine($"[EMAIL] → {order.CustomerEmail}: {msg}"));

// SMS notification
processor.Process(order, msg =>
    Console.WriteLine($"[SMS]   → {order.CustomerPhone}: {msg}"));

// Log only
processor.Process(order, msg =>
    Console.WriteLine($"[LOG]   {msg}"));
```

```text
Processing order — total: ₹1,499.00
[EMAIL] → yash@example.com: Your order of ₹1,499.00 has been placed.
Processing order — total: ₹1,499.00
[SMS]   → +91-9000000000: Your order of ₹1,499.00 has been placed.
Processing order — total: ₹1,499.00
[LOG]   Your order of ₹1,499.00 has been placed.
```

`OrderProcessor.Process` never changes. The notification behaviour is fully swappable.

<br>

# Memory Map — How a Delegate Stores a Method

```text
                    Delegate variable
                   ┌──────────────────┐
          notify   │  method ref ─────┼──► SendEmail()
                   │  target obj ─────┼──► (null for static methods)
                   └──────────────────┘

                    Multicast delegate
                   ┌──────────────────┐
          notify   │  invocation list │
                   │  [0] ────────────┼──► SendEmail()
                   │  [1] ────────────┼──► SendSMS()
                   │  [2] ────────────┼──► LogMessage()
                   └──────────────────┘
                        called in order on invoke
```

A delegate is an object on the heap that holds:
- A reference to the method
- A reference to the instance the method belongs to (null for static methods)

<br>

# Delegate vs Interface — When to Use Which

```text
┌─────────────────────────────────────────┬──────────────────────────────────────────┐
│              Delegate                   │               Interface                  │
├─────────────────────────────────────────┼──────────────────────────────────────────┤
│  Single method, pluggable behaviour     │  Multiple related methods, full contract │
│  Pass behaviour as a parameter          │  Define a type relationship              │
│  Callback, event, short-lived operation │  Repository, service, strategy pattern   │
│  Action<T>, Func<T,R>, lambdas          │  IEmailService, IRepository<T>           │
└─────────────────────────────────────────┴──────────────────────────────────────────┘
```

Rule of thumb:
- **One method** you want to pass around → delegate / `Action` / `Func`
- **Multiple methods** forming a contract → interface

<br>

# Summary

```text
delegate void Notify(string msg)     ← defines the signature

Notify n = SendEmail;                ← assign a method
n("hello");                          ← call it

n += SendSMS;                        ← multicast — add another
n("hello");                          ← calls both

Action<string> a = SendEmail;        ← built-in — no custom delegate needed
Func<int, bool> f = n => n > 0;     ← built-in with return type
```

- A delegate is a **type-safe function pointer** — holds a method reference
- The method must match the delegate's **signature** exactly
- Multicast: `+=` adds methods, `-=` removes, all called on invoke
- Pass behaviour into methods using `Action<T>` and `Func<T, TResult>`
- Custom delegate types are rarely needed — `Action` / `Func` cover most cases
- Delegates are the foundation of **events** and **LINQ**

<br>

# Interview Questions

### What is a delegate?

A type that holds a reference to a method with a specific signature. It allows methods to be passed as parameters, stored in variables, and called indirectly.

### What is a multicast delegate?

A delegate holding more than one method reference, built with `+=`. Invoking it calls all methods in the invocation list in order.

### What is the difference between `Action` and `Func`?

`Action` is a built-in delegate for methods that return `void`. `Func` is for methods that return a value — the last type parameter is always the return type.

### When would you use a delegate over an interface?

When you need to pass a single method as a parameter or callback. Interfaces are better when you need a full contract with multiple methods.

### What is `Predicate<T>`?

A shorthand for `Func<T, bool>` — a delegate that takes one argument and returns `true` or `false`. Used for filtering.
