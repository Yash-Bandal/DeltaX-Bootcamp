# Repository Patterns
## What are they?
Repository Pattern is an abstraction of the Data Access Layer, it hides the Details of how data is accessed and retrieved

Without repo patterns, we may not be able to make CRUD operations conviniently


Instead of directly writing database queries throughout the application, all data-related operations are handled by repositories.

<br>

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/a909504e-5ef0-4102-ac96-b84c1db6dfbc" />
</div>
<br>

## Why Use Repository Pattern?

Benefits:

* Separates business logic from data access logic.
* Makes code cleaner and easier to maintain.
* Promotes code reusability.
* Makes unit testing easier by allowing repositories to be mocked.
* Makes switching databases easier with minimal code changes.

<br>

## Without Repository Pattern

The service directly accesses the database.

```text
Controller
     │
     ▼
Service
     │
     ▼
Database
```

Business logic and data access become tightly coupled.

<br>

## With Repository Pattern

```text
Controller
     │
     ▼
Service
     │
     ▼
Repository
     │
     ▼
Database
```

The Service only communicates with the Repository, not the database directly.

<br>

## Common CRUD Methods

```csharp
GetAll()

GetById(int id)

Add(T entity)

Update(T entity)

Delete(int id)
```

<br>

## Example Interface

```csharp
public interface IEmployeeRepository
{
    List<Employee> GetAll();
    Employee GetById(int id);
    void Add(Employee employee);
    void Update(Employee employee);
    void Delete(int id);
}
```

<br>

## Example Implementation

```csharp
public class EmployeeRepository : IEmployeeRepository
{
    public List<Employee> GetAll()
    {
        // Fetch employees from database
    }

    public Employee GetById(int id)
    {
        // Fetch employee by ID
    }

    public void Add(Employee employee)
    {
        // Insert employee
    }

    public void Update(Employee employee)
    {
        // Update employee
    }

    public void Delete(int id)
    {
        // Delete employee
    }
}
```

<br>

## Real-World Use Cases

Repository Pattern is commonly used with:

* Entity Framework Core
* ASP.NET Core Web APIs
* SQL Server
* MySQL
* MongoDB

<br>

## Interview Answer (30 Seconds)

> The Repository Pattern is a design pattern that abstracts the data access layer. It provides a clean interface for performing CRUD operations while hiding database implementation details. This improves maintainability, testability, and separation of concerns by keeping business logic independent of data access logic.

<br>
 of the database directly.
* Improves maintainability, testability, and flexibility.
