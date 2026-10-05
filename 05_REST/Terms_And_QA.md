# 1. IIS and Kestrel

## Kestrel

**Kestrel** is the **built-in, cross-platform web server for ASP.NET Core**.

It receives HTTP requests and passes them into the ASP.NET Core application.

```text
Client
   ↓ HTTP Request
Kestrel
   ↓
ASP.NET Core API
   ↓
Database
```

When you run:

```bash
dotnet run
```

your ASP.NET Core application normally starts **Kestrel**.

Example:

```text
Now listening on: http://localhost:5000
```

This means Kestrel is listening for HTTP requests on port `5000`.

### Key Points

* Built into ASP.NET Core.
* Cross-platform: Windows, Linux, macOS.
* Lightweight and high-performance.
* Can directly serve an ASP.NET Core application.
* **Primarily a web server, not a reverse proxy.**



## IIS

**IIS (Internet Information Services)** is Microsoft's **web server for Windows**.

For ASP.NET Core, IIS can commonly work as a **reverse proxy** in front of Kestrel.

```text
Client
   ↓
IIS
   ↓
Kestrel
   ↓
ASP.NET Core API
   ↓
Database
```

The client communicates with IIS, and IIS forwards the request to the ASP.NET Core application running through Kestrel.

### IIS can provide

* HTTPS/TLS handling
* Request filtering
* Authentication
* Process management
* Reverse proxy functionality
* Hosting/management features for Windows applications



### IIS vs Kestrel

|                                 | Kestrel                           | IIS                                              |
| ------------------------------- | --------------------------------- | ------------------------------------------------ |
| Type                            | Web server                        | Web server                                       |
| Platform                        | Cross-platform - Linux, Windows, Mac                   | Windows                                          |
| Built into ASP.NET Core         | ✅                                 | ❌                                                |
| Can directly serve ASP.NET Core | ✅                                 | Usually through ASP.NET Core hosting integration |
| Reverse proxy                   | Not its primary role              | ✅ Commonly used                                  |
| Common usage                    | Direct hosting, containers, cloud | Windows/IIS hosting                              |


<br>

---

<br>


## Nested route 

A route where one resource is inside another resource, showing their relationship.
```
Parent → provides context
Child → resource being accessed/modified
```
Example:
```
/users/{userId}/orders
```
User = parent/context
Orders = child/resource being accessed

<br>

---

<br>

## Q/A

### 1. Why separate layers? Why not one file?

Your answer:

> We separate layers mainly for **Separation of Concerns**. Each layer has its own responsibility and hides its implementation details from the layer using it.

For example:

```text
Controller → handles HTTP
Service    → handles business logic
Repository → handles database access
```

This also follows **SRP (Single Responsibility Principle)** because each layer has a focused responsibility.

❌ Don't say:

> REST requires us to have Controller, Service and Repository layers.

REST's **layered system** principle is a different concept. It doesn't specifically mandate this application structure.

**Best interview answer:**

> **“We separate the application into layers to achieve Separation of Concerns and SRP. Each layer focuses on one responsibility and abstracts its implementation from the layer using it, which makes the application easier to maintain, test and modify.”**


<br>

### 2. Why DI? Why not just use `new`?

Your answer is also basically right.

Without DI:

```csharp
public class StorageService
{
    private IStorageService _storage = new SupabaseStorageService();
}
```

Now `StorageService` is **tightly coupled** to Supabase.

With DI:

```csharp
public StorageService(IStorageService storage)
{
    _storage = storage;
}
```

Now:

```text
             IStorageService
              /          \
     Supabase           Amazon S3
```

We can change the implementation without changing the consumer.

It also makes **unit testing/mocking easier** because we can provide a fake/mock `IStorageService`.

### One correction

Don't say:

> "SOLID tells us to use DI."

More precisely:

> **DIP (Dependency Inversion Principle) says high-level code should depend on abstractions rather than concrete implementations. DI is a technique commonly used to implement that principle.**

And **IoC** is the broader idea that the control of creating/providing dependencies is moved outside the class.

### In a go:

> **“We use DI to reduce tight coupling. Instead of a class creating its dependencies with `new`, the dependency is provided from outside through an abstraction. This makes it easier to replace implementations and mock dependencies during testing.”**
