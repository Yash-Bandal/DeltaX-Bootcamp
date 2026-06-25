# 5.2 Interfaces and Testability

One of the most important real-world uses of interfaces is making code **testable**.

Specifically — unit testing individual classes **without** depending on external systems like databases, file systems, or email servers.

<br>

# The Problem — Tight Coupling


Consider an `OrderService` that sends a confirmation email after an order is placed.

```csharp
public class OrderService
{
    public void PlaceOrder(Order order)
    {
        // ... process order ...

        var emailService = new EmailService();   // direct dependency
        emailService.Send(order.CustomerEmail, "Order confirmed!");
    }
}
```

`OrderService` creates `EmailService` directly inside itself.

```text
OrderService
    │
    └── new EmailService()    ← hardwired, cannot be swapped
             │
             └── SMTP server  ← hits a real email server
```

**Problem**: To test `PlaceOrder()`, you must also run `EmailService` — which connects to a real SMTP server.

Every test run sends a real email. The test is **slow**, **fragile**, and **unpredictable**.

<br>

# The Fix — Depend on an Interface, Not a Class

Step 1 — Extract an interface from `EmailService`:

```csharp
public interface IEmailService
{
    void Send(string to, string subject);
}
```

Step 2 — Make `EmailService` implement it:

```csharp
public class EmailService : IEmailService
{
    public void Send(string to, string subject)
    {
        // real SMTP logic
    }
}
```

Step 3 — `OrderService` depends on the **interface**, not the concrete class:

```csharp
public class OrderService
{
    private IEmailService _emailService;

    public OrderService(IEmailService emailService)   // injected from outside
    {
        _emailService = emailService;
    }

    public void PlaceOrder(Order order)
    {
        // ... process order ...

        _emailService.Send(order.CustomerEmail, "Order confirmed!");
    }
}
```

`OrderService` no longer knows or cares what kind of email service it gets.
It only knows it has something that can `Send()`.

<br>

# Dependency Injection

This pattern — passing dependencies in from outside — is called **Dependency Injection (DI)**.

```csharp
// Production — uses real email
var service = new OrderService(new EmailService());

// Test — uses a fake
var service = new OrderService(new FakeEmailService());
```

The class doesn't change. Only what you pass in changes.

<br>

# Writing a Fake for Testing

In tests, you create a **fake** (or mock) implementation of the interface:

```csharp
public class FakeEmailService : IEmailService
{
    public List<string> SentEmails = new List<string>();

    public void Send(string to, string subject)
    {
        SentEmails.Add(to);   // no real email — just records it
    }
}
```

Now test `OrderService` without touching a real server:

```csharp
// Arrange
var fakeEmail = new FakeEmailService();
var orderService = new OrderService(fakeEmail);
var order = new Order { CustomerEmail = "yash@example.com" };

// Act
orderService.PlaceOrder(order);

// Assert
Assert.AreEqual(1, fakeEmail.SentEmails.Count);
Assert.AreEqual("yash@example.com", fakeEmail.SentEmails[0]);
```

Fast. No network. No real server. Runs in milliseconds.

<br>

# Memory Map — Before and After


<br>
<div align"center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/53b106f7-54b1-4a27-936d-b2805b2f65eb" />
  <p>Now the production vs test flow — showing what actually runs in each environment:</p>
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/f7033219-0dd6-46ef-9841-1910eac28ec4" />
</div>
<br>


```text
BEFORE — tightly coupled
─────────────────────────

OrderService
    │
    └── new EmailService()
              │
              └──► SMTP Server   ← test hits real server


AFTER — loosely coupled via interface
──────────────────────────────────────

                  IEmailService
                 /             \
     EmailService           FakeEmailService
     (production)               (tests)
          │                        │
          └──────────┬─────────────┘
                     │
               OrderService
               (only sees IEmailService)

Production:   OrderService ──► EmailService ──► SMTP
Test:         OrderService ──► FakeEmailService ──► nothing
```

`OrderService` is identical in both cases. Only the injected object differs.

<br>

# Why This Works

The interface is the **seam** — the point where you can cut and swap.

```text
Interface = a seam in your code
           where production and test implementations
           can be swapped without changing any logic
```

Without the interface, there is no seam — the class is one solid block, impossible to cut.

<br>

# Full Example

```csharp
// Interface — the contract
public interface IEmailService
{
    void Send(string to, string subject);
}

// Production implementation
public class EmailService : IEmailService
{
    public void Send(string to, string subject)
    {
        Console.WriteLine($"Sending email to {to}: {subject}");
        // real SMTP code here
    }
}

// Fake for testing
public class FakeEmailService : IEmailService
{
    public int SendCount = 0;

    public void Send(string to, string subject)
    {
        SendCount++;   // just count — no real send
    }
}

// The class being tested
public class OrderService
{
    private IEmailService _emailService;

    public OrderService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public void PlaceOrder(Order order)
    {
        _emailService.Send(order.CustomerEmail, "Your order is confirmed");
    }
}
```

```csharp
// In production
var orderService = new OrderService(new EmailService());

// In unit test
var fake = new FakeEmailService();
var orderService = new OrderService(fake);
orderService.PlaceOrder(new Order { CustomerEmail = "test@test.com" });

Console.WriteLine(fake.SendCount);   // 1
```

<br>

# The Rule

```text
Depend on abstractions (interfaces), not concretions (classes).
```

This is the **D** in SOLID — **Dependency Inversion Principle**.

If `OrderService` depends on `IEmailService` (an abstraction), it is open to any implementation.
If it depends on `EmailService` (a concrete class), it is locked to one.

<br>

# Summary

- Tight coupling makes classes impossible to test in isolation
- Extract an interface from any external dependency (`IEmailService`, `IDatabase`, `ILogger`)
- Inject the dependency through the constructor — **Dependency Injection**
- In production, inject the real implementation
- In tests, inject a fake that records calls without side effects
- The interface is the **seam** — the swappable point in your design

<br>

# Interview Questions

### Why do interfaces improve testability?

They let you replace real dependencies (database, email, file system) with fakes during tests, so you can test logic in isolation without side effects.

<br>

### What is Dependency Injection?

Passing a dependency into a class from the outside (usually via constructor) rather than creating it inside the class. The class depends on an interface, so any implementation can be injected.

<br>

### What is a fake / mock?

A test-only class that implements an interface but does nothing real — it records calls, returns fixed values, or simply does nothing. It lets tests verify behaviour without hitting real systems.

<br>

### What is the Dependency Inversion Principle?

High-level classes should depend on abstractions (interfaces), not on low-level concrete classes. This decouples them so either side can change independently.
