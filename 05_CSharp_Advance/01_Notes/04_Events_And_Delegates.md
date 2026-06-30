
# 4. Events and Delegates

## 4.1 Delegates

### 4.1.1 What is a Delegate?

A **delegate** is a type that stores a reference to one or more methods.

Instead of calling a method directly, you call the delegate, and it invokes the assigned method(s).

<BR>
<div align = "center">
      <img width="600" alt="image" src="https://github.com/user-attachments/assets/e3ff738d-45f8-430e-81fe-41540f16096f" />
</div>
<BR>

### Real-World Analogy

Think of a delegate as a **remote control**.

```text
Remote Button
      │
      ▼
Assigned TV Function
```

The remote doesn't know how the TV works; it only knows which function to call.

Similarly, a delegate doesn't contain the logic—it only stores a reference to a method.

<br>


## 4.2 Events

### 4.2.1 What is an Event?

An event is a notification sent by one object to other objects when something happens.

Examples:

* Button Click
* Order Placed
* Payment Completed
* File Uploaded
* User Registered

<br>

### Real-World Analogy

Imagine a school bell.

```text
Bell Rings
      │
      ▼
Students React
Teachers React
```

The bell doesn't know who is listening.

It simply announces that something happened.

Events work the same way.

<br>

### 4.2.2 Why Use Events?

Without events:

```text
Order Service
      │
      ├── Call Email Service
      ├── Call SMS Service
      ├── Call Logger
      └── Call Inventory
```

The class becomes tightly coupled.

With events:

```text
Order Service
      │
      ▼
OrderPlaced Event
      │
 ┌────┼─────┐
 ▼    ▼     ▼
Email SMS Logger
```

New subscribers can be added without changing the Order Service.

<br>

### 4.2.3 Declaring an Event

```csharp
public delegate void OrderPlacedHandler();

public class Order
{
    public event OrderPlacedHandler OrderPlaced;
}
```

<br>

### 4.2.4 Raising an Event

```csharp
public class Order
{
    public event Action OrderPlaced;

    public void PlaceOrder()
    {
        Console.WriteLine("Order Placed");

        OrderPlaced?.Invoke();
    }
}
```

`?.Invoke()` safely checks whether anyone is subscribed.

<br>

### 4.2.5 Subscribing to an Event

```csharp
class Program
{
    static void Main()
    {
        Order order = new Order();

        order.OrderPlaced += SendEmail;
        order.OrderPlaced += SendSMS;

        order.PlaceOrder();
    }

    static void SendEmail()
    {
        Console.WriteLine("Email Sent");
    }

    static void SendSMS()
    {
        Console.WriteLine("SMS Sent");
    }
}
```

Output:

```text
Order Placed
Email Sent
SMS Sent
```

<br>

### 4.2.6 Unsubscribing

```csharp
order.OrderPlaced -= SendSMS;
```

Only remaining subscribers will execute.

<br>

### 4.2.7 Event Flow

```text
PlaceOrder()

      │

      ▼

OrderPlaced Event

      │

 ┌────┼────┐

 ▼    ▼    ▼

Email SMS Logger
```

<br>

### 4.2.8 Why Events Instead of Delegates?

A delegate can be invoked by anyone.

An event can only be raised by the class that owns it.

This provides better encapsulation.

<br>

### 4.2.9 Common Real-World Uses

Events are heavily used in:

* Windows Forms
* WPF
* ASP.NET
* Unity
* File Monitoring
* Background Services
* Payment Systems
* Notification Systems

<br>

## 4.3 Delegates vs Events

| Delegate                  | Event                                         |
| ------------------------- | --------------------------------------------- |
| Stores method references  | Notifies subscribers                          |
| Can be invoked anywhere   | Can only be raised inside the declaring class |
| Can be used independently | Built on top of delegates                     |
| Used for callbacks        | Used for notifications                        |

<br>

## 4.4 Interview Notes

### Delegate

* Type-safe function pointer
* Stores references to methods
* Supports multicast delegates
* Enables callbacks

<br>

### Event

* Built on delegates
* Implements Publisher-Subscriber pattern
* Used for notifications
* Provides loose coupling

<br>

## 4.5 Key Takeaways

* A delegate stores references to methods.
* Delegates allow methods to be passed as parameters.
* Multicast delegates can invoke multiple methods.
* `Action`, `Func`, and `Predicate` are commonly used built-in delegates.
* Events are built on delegates.
* Events follow the Publisher-Subscriber pattern.
* Events reduce coupling between classes.
* Events are widely used in UI applications, ASP.NET, and enterprise software.
