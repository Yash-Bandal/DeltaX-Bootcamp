# 4. Events and Delegates

## 4.1 Delegates

### 4.1.1 What is a Delegate?

A **delegate** is a type that stores references to one or more methods.

Instead of calling a method directly, you call the **delegate**, and it invokes the assigned method(s).

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

The remote doesn't know how the TV works.

It simply stores which function to call.

Similarly, a delegate stores references to methods and invokes them when requested.

<br>

### 4.1.2 Delegate Example

Suppose we have a button.

When the button is clicked, we want to execute different methods.

```csharp
public delegate void Greet(string name);
```

Methods

```csharp
public void GreetHello(string name)
{
    Console.WriteLine("Hello " + name);
}

public void GreetBye(string name)
{
    Console.WriteLine("Bye " + name);
}
```

Assign methods

```csharp
Greet greet = GreetHello;

greet += GreetBye;

greet("Yash");
```

Output

```text
Hello Yash
Bye Yash
```

<br>

### 4.1.3 Multicast Delegate

A delegate can store multiple methods.

```text
Delegate

      │

      ├── GreetHello()

      ├── GreetBye()

      └── GreetWelcome()
```

When invoked, all assigned methods execute in order.

<br>

---

<br>


## 4.2 Events

### 4.2.1 What is an Event?

An **event** is a notification sent by one object to other objects when something happens.

Examples

- Button Clicked
- Payment Completed
- File Uploaded
- User Registered
- Order Placed

<br>

### **Code**
```csharp
using System;

namespace CSharpAdvanced
{
    internal class Program
    {
        public class EventUse
        {
            //========================================
            //1. Define Delegate Type
            //========================================
            public delegate void Notify();

            //========================================
            //2. Define Event
            //========================================
            public event Notify OnButtonClick;

            //========================================
            //3. Define Subscriber Methods
            //========================================
            public void SaveFile()
            {
                Console.WriteLine("Saving File...");
            }

            public void PlaySound()
            {
                Console.WriteLine("Playing Sound...");
            }

            public void ShowMessage()
            {
                Console.WriteLine("Showing Message...");
            }

            //========================================
            //4. Raise Event
            //========================================
            public void ButtonClick()
            {
                Console.WriteLine("Button Clicked");

                OnButtonClick?.Invoke();
            }

            //========================================
            //5. Subscribe / Unsubscribe
            //========================================
            public void RunEvent()
            {
                // Subscribe
                OnButtonClick += SaveFile;
                OnButtonClick += PlaySound;
                OnButtonClick += ShowMessage;

                // Unsubscribe
                OnButtonClick -= PlaySound;

                // Raise Event
                ButtonClick();
            }
        }

        static void Main(string[] args)
        {
            //========================================
            //6. Call inside Main
            //========================================
            EventUse ev = new EventUse();

            ev.RunEvent();
        }
    }
}
```

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

### 4.2.2 Why Do We Need Events?

Suppose we have a button.

When it is clicked,

- Save the file
- Play a sound
- Show a message

Without events

```text
Button

      │

      ├── SaveFile()

      ├── PlaySound()

      └── ShowMessage()
```

Every new task requires modifying the `Button` class.

The class becomes **tightly coupled**.

With events

```text
Button

      │

      ▼

ButtonClick Event

      │

 ┌────┼──────────┐

 ▼    ▼          ▼

SaveFile() PlaySound() ShowMessage()
```

The `Button` simply raises the event.

Subscribers decide what to do.

<br>

### 4.2.3 Steps to Create an Event

```text
1. Define Delegate

        ↓

2. Define Event

        ↓

3. Create Subscriber Methods

        ↓

4. Subscribe Methods (+=)

        ↓

5. Unsubscribe Methods (-=)

        ↓

6. Raise Event (Invoke())
```

<br>

### 4.2.4 Define Delegate

```csharp
public delegate void Notify();
```

This defines what kind of methods can subscribe.

<br>

### 4.2.5 Define Event

```csharp
public event Notify OnButtonClick;
```

The event is built on top of the delegate.

<br>

### 4.2.6 Subscriber Methods

```csharp
public void SaveFile()
{
    Console.WriteLine("Saving File...");
}

public void PlaySound()
{
    Console.WriteLine("Playing Sound...");
}

public void ShowMessage()
{
    Console.WriteLine("Showing Message...");
}
```

<br>

### 4.2.7 Subscribe to the Event

```csharp
OnButtonClick += SaveFile;

OnButtonClick += PlaySound;

OnButtonClick += ShowMessage;
```

The `+=` operator subscribes methods to the event.

<br>

### 4.2.8 Unsubscribe from the Event

```csharp
OnButtonClick -= PlaySound;
```

The `-=` operator removes a subscriber.

<br>

### 4.2.9 Raise the Event

```csharp
public void ButtonClick()
{
    Console.WriteLine("Button Clicked");

    OnButtonClick?.Invoke();
}
```

`?.Invoke()` safely checks whether anyone has subscribed before raising the event.

<br>

### 4.2.10 Event Flow

```text
ButtonClick()

      │

      ▼

OnButtonClick Event

      │

 ┌────┼──────────┐

 ▼    ▼          ▼

SaveFile() PlaySound() ShowMessage()
```

<br>

### 4.2.11 Complete Event Example

```csharp
EventUse ev = new EventUse();

ev.OnButtonClick += ev.SaveFile;
ev.OnButtonClick += ev.PlaySound;
ev.OnButtonClick += ev.ShowMessage;

ev.ButtonClick();
```

Output

```text
Button Clicked
Saving File...
Playing Sound...
Showing Message...
```

<br>

### 4.2.12 Why Events Instead of Delegates?

A public delegate can be

- Invoked by anyone
- Replaced by anyone
- Set to `null` by anyone

For example,

```csharp
greet();
```

```csharp
greet = null;
```

```csharp
greet = SomeOtherMethod;
```

This is unsafe.

An event solves this problem.

Outside code can only

```text
Subscribe

+=
```

or

```text
Unsubscribe

-=
```

Only the class that owns the event can raise it using `Invoke()`.

<br>

### 4.2.13 Common Uses of Events

Events are commonly used in

- Windows Forms
- WPF
- ASP.NET
- Unity
- File Monitoring
- Background Services
- Payment Systems
- Notification Systems

<br>

## 4.3 Delegates vs Events

| Delegate | Event |
|----------|-------|
| Stores references to methods | Built on top of delegates |
| Can invoke one or more methods | Notifies subscribers when something happens |
| Can be invoked by anyone | Can only be raised inside the declaring class |
| Supports multicast | Supports Publisher–Subscriber pattern |

<br>

## 4.4 Interview Notes

### Delegate

- Type-safe function pointer
- Stores references to methods
- Supports multicast delegates
- Enables callbacks

<br>

### Event

- Built on delegates
- Implements Publisher–Subscriber pattern
- Used for notifications
- Provides loose coupling
- Only the declaring class can raise the event

<br>

## 4.5 Key Takeaways

- A delegate stores references to methods.
- Delegates can invoke one or more methods.
- Multicast delegates execute multiple methods in order.
- `Action`, `Func`, and `Predicate` are built-in delegates.
- Events are built on delegates.
- Events allow methods to subscribe (`+=`) and unsubscribe (`-=`).
- Only the class that owns an event can raise it.
- Events implement the Publisher–Subscriber pattern and reduce coupling between classes.
