# 2.1 Class Coupling

Class Coupling refers to the degree of dependency between two classes.

If one class heavily depends on another, they are **tightly coupled**.

If a class has minimal dependency on others, it is **loosely coupled**.

In Object-Oriented Programming, **low (loose) coupling is preferred**.

<br>

<div align = "center">
<img width="600"  alt="image" src="https://github.com/user-attachments/assets/73681672-978f-41e0-8961-46fbb600f398" />
</div>



<br>





# What is Coupling?

Coupling measures **how much one class knows about or depends on another class**.



Example

```text
Class A  ------->  Class B
```

If changes in `Class B` force changes in `Class A`, the classes are tightly coupled.

<br>

<div align = "center">
<img width="600"  alt="image" src="https://github.com/user-attachments/assets/ac89a478-78ba-475a-a653-9169fef67581" />
</div>




<br>

# Why is Coupling Important?

Good software should be:

* Easy to maintain.
* Easy to test.
* Easy to modify.
* Easy to extend.

High coupling makes all of these difficult.

<br>

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/9f0ff06f-1e16-45a9-ac74-a39ccaf8a7ca" />
    <p>Tight Coupling - Full Arrows</p>
</div>

In Tight Coupling, all classes are very dependent on each other, 

Thus, when we change 1 class, we might have to change many classes, that rises the need for loose coupling

<br>




Low coupling makes classes more independent and reusable.

<br>

In loose coupling, we idealize to have very minimum changes, isolated changes in software that dont affect other classes

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/49cc2242-dbf8-4e06-9213-3185c0aaa2ff" />
<p>Loose Coupling - Dotted Arrows</p>
</div>







<br>


<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/ecb924ec-9494-4e97-8e99-fb53ebef24d2" />
</div>

Thus we need to understand relationships

<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/38b7a61b-1233-46c6-a600-1d7ff0b0c998" />
</div>

<br>

# Tight Coupling

In **tight coupling**, one class directly creates or depends on another class.

Example

```csharp
class Logger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

class OrderProcessor
{
    private Logger logger = new Logger();

    public void Process()
    {
        logger.Log("Processing order...");
    }
}
```

Here, `OrderProcessor` is tightly coupled to `Logger`.

If `Logger` changes or needs to be replaced, `OrderProcessor` must also change.

<br>

# Problems with Tight Coupling

* Difficult to test.
* Difficult to replace dependencies.
* Harder to maintain.
* Reduced code reusability.
* Changes in one class affect others.

<br>

# Loose Coupling

In **loose coupling**, classes depend on abstractions rather than concrete implementations.

Instead of creating dependencies directly, they receive them from outside.

Example

```csharp
class Logger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

class OrderProcessor
{
    private readonly Logger logger;

    public OrderProcessor(Logger logger)
    {
        this.logger = logger;
    }

    public void Process()
    {
        logger.Log("Processing order...");
    }
}
```

The `Logger` object is supplied from outside, making the classes less dependent on each other.

> In later chapters, you'll see that interfaces make loose coupling even more powerful.

<br>

# Tight Coupling vs Loose Coupling

| Tight Coupling                        | Loose Coupling                    |
| ------------------------------------- | --------------------------------- |
| Classes depend directly on each other | Classes have minimal dependencies |
| Difficult to modify                   | Easy to modify                    |
| Difficult to test                     | Easy to test                      |
| Less reusable                         | More reusable                     |
| High dependency                       | Low dependency                    |

<br>

# Real-World Example

Imagine a restaurant.

**Tightly Coupled**

The waiter always works with only one chef.

If that chef is absent, the waiter cannot serve customers.

**Loosely Coupled**

The waiter can work with any available chef.

If one chef is unavailable, another can take over without changing the waiter's workflow.

This flexibility is the goal of loose coupling.

<br>

# How to Achieve Loose Coupling

Some common techniques include:

* Use interfaces.
* Use dependency injection.
* Depend on abstractions instead of concrete classes.
* Keep each class focused on a single responsibility.

These topics will be explored further in upcoming chapters.

<br>

# Best Practices

* Prefer loose coupling over tight coupling.
* Avoid creating dependencies inside a class using `new` whenever possible.
* Keep classes independent.
* Depend on abstractions rather than implementations.

<br>

# Common Mistakes

## Creating dependencies directly

```csharp
private Logger logger = new Logger();
```

This tightly couples the class to a specific implementation.

<br>

## One class doing too much

When a class has many responsibilities, it often becomes tightly coupled with multiple classes.

Follow the **Single Responsibility Principle (SRP)** by keeping each class focused on one job.

<br>

# Interview Questions

### What is class coupling?

Class coupling is the degree of dependency between two classes.

<br>

### What is tight coupling?

Tight coupling occurs when one class depends heavily on another concrete class.

<br>

### What is loose coupling?

Loose coupling occurs when classes have minimal dependencies and interact through abstractions or externally provided dependencies.

<br>

### Why is loose coupling preferred?

Because it improves maintainability, flexibility, testability, and code reusability.

<br>

### How can loose coupling be achieved?

By using interfaces, dependency injection, and depending on abstractions instead of concrete implementations.

<br>

# Summary

* Coupling measures how dependent one class is on another.
* Tight coupling creates strong dependencies and reduces flexibility.
* Loose coupling minimizes dependencies and improves maintainability.
* Prefer designing classes that are independent and easy to replace.
* Interfaces and dependency injection are common techniques for achieving loose coupling.
