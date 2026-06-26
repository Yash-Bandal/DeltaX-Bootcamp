# 5.3 Interfaces and Extensibility

<br>
### Refere [This](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_Intermediate_CSharp/01_Notes/05_Interfaces/Understanding.md) for proper Understanding
<br>

One of the biggest advantages of interfaces is **extensibility**.

An application should be designed so that **new features can be added without modifying existing, working code**. Interfaces make this possible by allowing different implementations to be plugged into the application.

<br>

# What is Extensibility?

**Extensibility** is the ability to add new functionality to an application **without changing existing code**.

Instead of modifying a class every time a new requirement appears, we simply create a new class that implements the required interface.

This makes applications easier to maintain and less prone to bugs.

<br>

# Why is Extensibility Important?

Suppose an application sends notifications.

Initially, it only supports Email.

Later, the client asks to support:

* SMS
* Push Notifications
* WhatsApp

If the application is tightly coupled, the existing code must be modified every time a new notification type is added.

This increases the risk of introducing bugs.

Interfaces solve this problem.

<br>

# Without Interfaces

```csharp
class NotificationService
{
    public void Send(string type)
    {
        if (type == "Email")
        {
            Console.WriteLine("Sending Email");
        }
        else if (type == "SMS")
        {
            Console.WriteLine("Sending SMS");
        }
    }
}
```

Every new notification type requires modifying `NotificationService`.

This violates the **Open/Closed Principle**.

<br>

# Using Interfaces

Step 1: Define an interface.

```csharp
public interface INotificationSender
{
    void Send();
}
```

<br>

Step 2: Create implementations.

```csharp
class EmailSender : INotificationSender
{
    public void Send()
    {
        Console.WriteLine("Sending Email");
    }
}

class SmsSender : INotificationSender
{
    public void Send()
    {
        Console.WriteLine("Sending SMS");
    }
}
```

<br>

Step 3: Use the interface.

```csharp
class NotificationService
{
    private readonly INotificationSender sender;

    public NotificationService(INotificationSender sender)
    {
        this.sender = sender;
    }

    public void Notify()
    {
        sender.Send();
    }
}
```

Usage

```csharp
NotificationService service =
    new NotificationService(new EmailSender());

service.Notify();
```

To switch to SMS:

```csharp
NotificationService service =
    new NotificationService(new SmsSender());

service.Notify();
```

Notice that **`NotificationService` never changes**.

Only a different implementation is supplied.

<br>

# Adding New Features

Suppose the client now requests WhatsApp notifications.

Simply create another implementation.

```csharp
class WhatsAppSender : INotificationSender
{
    public void Send()
    {
        Console.WriteLine("Sending WhatsApp Message");
    }
}
```

Usage

```csharp
NotificationService service =
    new NotificationService(new WhatsAppSender());
```

No existing classes are modified.

The application has been **extended** without changing existing code.

<br>

# Open/Closed Principle (OCP)

Interfaces support the **Open/Closed Principle**.

A class should be:

* **Open for extension**
* **Closed for modification**

Instead of changing existing classes, extend the application by creating new implementations.

<br>

# Benefits of Interfaces for Extensibility

* Add new features without modifying existing code.
* Reduce the risk of introducing bugs.
* Promote loose coupling.
* Improve maintainability.
* Make applications easier to scale.

<br>

# Real-World Examples

Interfaces make it easy to swap implementations.

Examples:

* Email, SMS, or WhatsApp notification senders.
* PayPal, Stripe, or Razorpay payment gateways.
* SQL Server, MySQL, or PostgreSQL database providers.
* Local storage or cloud storage providers.

The application depends on the interface, not the concrete implementation.

<br>

# Best Practices

* Program against interfaces, not concrete classes.
* Inject interface implementations through constructors.
* Create a new implementation instead of modifying existing code.
* Keep each implementation focused on a single responsibility.

<br>

# Common Mistakes

## Depending on concrete classes

```csharp
private EmailSender sender = new EmailSender();
```

This creates tight coupling.

Instead,

```csharp
private readonly INotificationSender sender;
```

<br>

## Modifying existing classes for every new feature

Avoid repeatedly adding `if` or `switch` statements for new behaviors.

Instead, create a new implementation of the interface.

<br>

# Interview Questions

### What is extensibility?

Extensibility is the ability to add new functionality to an application without modifying existing code.

<br>

### How do interfaces improve extensibility?

Interfaces allow new implementations to be added without changing the classes that depend on them.

<br>

### Which SOLID principle is supported by interfaces and extensibility?

The **Open/Closed Principle (OCP)**.

<br>

### Why is extensibility important?

It reduces maintenance effort, minimizes bugs, and allows applications to grow without affecting existing functionality.

<br>

# Summary

* Extensibility means adding new functionality without changing existing code.
* Interfaces make applications extensible by allowing multiple implementations.
* Classes should depend on interfaces rather than concrete implementations.
* New features are added by creating new classes that implement the interface.
* Interfaces help follow the **Open/Closed Principle**, making applications more maintainable and scalable.
