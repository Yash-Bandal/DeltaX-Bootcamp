# Dapper Setup & Repository Refactoring — ASP.NET Core 5

> **Reference:** As per the video tutorial  
> **Project:** IMDB API  
> **Database:** MS SQL Server

<br>

## 1. Install Required NuGet Packages

First, install the required NuGet packages through **Manage NuGet Packages**.

| Package | Version from Video |
|--|--|
| **Dapper** | **2.0.90** |
| **Microsoft.Data.SqlClient** | **5.1.0** |

### Purpose

- **Dapper** → Micro ORM used to execute SQL queries and map query results to C# objects.
- **Microsoft.Data.SqlClient** → .NET data provider used to connect to Microsoft SQL Server.

<br>

# 2. Create `ConnectionString.cs`

Create `ConnectionString.cs` at the **root level**, alongside:

- `Program.cs`
- `Startup.cs`

```csharp
namespace IMDB_API
{
    public class ConnectionString
    {
        public string IMDBDB { get; set; }
    }
}
```

### Why create this class?

You could access configuration directly using `IConfiguration`, but a cleaner approach is to create a class representing the configuration.

`ConnectionString` is a simple **POCO (Plain Old CLR Object)** that represents our connection-string configuration.

<br>

# 3. Add Connection String to `appsettings.json`

Add the connection string under the `ConnectionStrings` section:

```json
{
  "ConnectionStrings": {
    "IMDBDB": "Data Source=localhost;Initial Catalog=IMDB;Integrated Security=True;"
  }
}
```

Here:

- `Data Source=localhost` → SQL Server is running on the local machine.
- `Initial Catalog=IMDB` → Database name is `IMDB`.
- `Integrated Security=True` → Windows Authentication is being used.

<br>

# 4. Register `ConnectionString` with DI

Inside `Startup.cs`, register the configuration class with the Dependency Injection container.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddAutoMapper(typeof(MappingProfile));

    // Dapper -> map configuration to ConnectionString class
    // Registered to DI
    services.Configure<ConnectionString>(
        Configuration.GetSection("ConnectionStrings")
    );
}
```

### What does this mean?

```csharp
services.Configure<ConnectionString>(
    Configuration.GetSection("ConnectionStrings")
);
```

It means:

> **Bind the `ConnectionStrings` configuration section to the `ConnectionString` class, and make that configuration available through the Options system.**

The values from:

```json
"ConnectionStrings": {
    "IMDBDB": "..."
}
```

are mapped to:

```csharp
public class ConnectionString
{
    public string IMDBDB { get; set; }
}
```

<br>

# 5. Access the Connection String in Repository

Now, inside the repository where the connection string is required, inject `IOptions<ConnectionString>`.

For example, in `ActorRepository.cs`.

## Initial approach

Add the property:

```csharp
private readonly IOptions<ConnectionString> _connectionString;
```

and constructor:

```csharp
public ActorRepository(IOptions<ConnectionString> connectionString)
{
    _connectionString = connectionString;
}
```

However, we can access the actual `ConnectionString` object through:

```csharp
_connectionString.Value
```

Therefore, we can simplify this.

## Better approach

```csharp
private readonly ConnectionString _connectionString;

public ActorRepository(IOptions<ConnectionString> connectionString)
{
    _connectionString = connectionString.Value;
}
```

Now `_connectionString` directly contains our configuration object.

For example:

```csharp
_connectionString.IMDBDB
```

gives us the actual database connection string.

<br>

## Why not simply inject `ConnectionString`?

`ConnectionString` is just our own POCO/class:

```csharp
public class ConnectionString
{
    public string IMDBDB { get; set; }
}
```

ASP.NET Core's DI container does not automatically know how to construct this object from `appsettings.json`.

`IOptions<T>` provides the bridge.

### `IOptions<T>`

`IOptions<T>` is a built-in ASP.NET Core mechanism for reading configuration values and making them available through Dependency Injection.

The flow is:

```text
appsettings.json
       ↓
Configuration
       ↓
IOptions<ConnectionString>
       ↓
ConnectionString
       ↓
Repository
```

### Important

Learn more about:

> **`IOptions<T>` in ASP.NET Core**

It is commonly used for strongly typed configuration.

<br>

# 6. Replace the Static List with SQL Queries

Previously, the repository was using an in-memory list:

```csharp
private readonly List<Actor> _actors = new List<Actor>();
```

Now that we are using SQL Server, this is no longer required.

Instead, write SQL queries that retrieve data from the database.

The SQL can be formatted using:

[Poor SQL Formatter](https://poorsql.com/?utm_source=chatgpt.com)

<br>

## Get All Actors

Create the query:

```csharp
const string query = @"
SELECT [Id]
    ,[Name]
    ,[Bio]
    ,[DateOfRelease] AS [DOB]
    ,[Gender]
FROM [Actors] (NOLOCK)";
```

### `AS [DOB]`

The database column is:

```text
DateOfRelease
```

while the C# model expects:

```text
DOB
```

Therefore, SQL aliasing is used:

```sql
[DateOfRelease] AS [DOB]
```

Dapper can then map the result to the `Actor` property.

<br>

## `NOLOCK`

```sql
FROM [Actors] (NOLOCK)
```

`NOLOCK` allows SQL Server to read without taking shared locks.

> Note: `NOLOCK` can allow dirty/uncommitted reads, so it should not be treated as a universally safe default. Understand its consistency implications before using it in production.

<br>

## Execute the Query with Dapper

Create a SQL connection:

```csharp
using (var connection = new SqlConnection(_connectionString.IMDBDB))
{
    return connection.Query<Actor>(query);
}
```

Or using the shorter syntax:

```csharp
using var connection =
    new SqlConnection(_connectionString.IMDBDB);

return connection.Query<Actor>(query);
```

### `using`

`using` ensures that the connection is disposed when execution leaves its scope.

This prevents resources from remaining open unnecessarily.

<br>

# Get All Actors and Get Actor by ID

Putting the two methods together:

## `Get()`

```csharp
public IEnumerable<Actor> Get()
{
    const string query = @"
SELECT [Id]
    ,[Name]
    ,[Bio]
    ,[DateOfRelease] AS [DOB]
    ,[Gender]
FROM [Actors] (NOLOCK)";

    using var connection =
        new SqlConnection(_connectionString.IMDBDB);

    return connection.Query<Actor>(query);
}
```

<br>

## `Get(int id)`

```csharp
public Actor Get(int id)
{
    const string query = @"
SELECT [Id]
    ,[Name]
    ,[Bio]
    ,[DateOfRelease] AS [DOB]
    ,[Gender]
FROM [Actors] (NOLOCK)
WHERE Id = @Id";

    using var connection =
        new SqlConnection(_connectionString.IMDBDB);

    return connection.QueryFirstOrDefault<Actor>(
        query,
        new { Id = id }
    );
}
```

### Dapper Parameter

This:

```csharp
new { Id = id }
```

creates an anonymous object containing a property called `Id`.

It supplies the value for:

```sql
@Id
```

in the SQL query.

So:

```csharp
WHERE Id = @Id
```

gets its value from:

```csharp
new { Id = id }
```

### Important

The `new` keyword here is **not what returns the value**.

It creates an **anonymous object** that Dapper uses as the query's parameter object.

<br>

# 7. Create a Base Repository

At this point, we notice that these lines are repeated in multiple repository methods:

```csharp
using var connection =
    new SqlConnection(_connectionString.IMDBDB);

return connection.Query<Actor>(query);
```

Similarly:

```csharp
using var connection =
    new SqlConnection(_connectionString.IMDBDB);

return connection.QueryFirstOrDefault<Actor>(
    query,
    new { Id = id }
);
```

The common responsibility is:

1. Create a SQL connection.
2. Execute a Dapper query.
3. Return the result.

Instead of repeating this logic in every repository, create a **Base Repository**.

<br>

# 8. Create `BaseRepository<T>`

Create:

```text
Repository
└── BaseRepository.cs
```

Implementation:

```csharp
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace IMDB_API.Repository
{
    public class BaseRepository<T> where T : class
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> Get(string query)
        {
            using var connection =
                new SqlConnection(_connectionString);

            return connection.Query<T>(query);
        }

        public T Get(string query, object parameters)
        {
            using var connection =
                new SqlConnection(_connectionString);

            return connection.QueryFirstOrDefault<T>(
                query,
                parameters
            );
        }
    }
}
```

<br>

## Understanding `BaseRepository<T>`

```csharp
public class BaseRepository<T> where T : class
```

This is a **generic repository class**.

`T` represents the entity being worked with.

For example:

```csharp
BaseRepository<Actor>
```

means:

```text
T = Actor
```

Therefore:

```csharp
Query<T>
```

becomes effectively:

```csharp
Query<Actor>
```

The constraint:

```csharp
where T : class
```

means `T` must be a reference type.

<br>

# 9. Inherit the Base Repository

Previously, `ActorRepository` looked like:

```csharp
public class ActorRepository : IActorRepository
{
    private readonly ConnectionString _connectionString;

    public ActorRepository(
        IOptions<ConnectionString> connectionString)
    {
        _connectionString = connectionString.Value;
    }
}
```

Now make `ActorRepository` inherit from:

```csharp
BaseRepository<Actor>
```

### Updated version

```csharp
public class ActorRepository
    : BaseRepository<Actor>, IActorRepository
{
    public ActorRepository(
        IOptions<ConnectionString> connectionString)
        : base(connectionString.Value.IMDBDB)
    {
    }
}
```

### What happens here?

```csharp
: BaseRepository<Actor>
```

means:

> `ActorRepository` inherits the functionality of `BaseRepository` for the `Actor` entity.

And:

```csharp
: base(connectionString.Value.IMDBDB)
```

passes the database connection string to the constructor of `BaseRepository`.

The base repository receives:

```csharp
public BaseRepository(string connectionString)
{
    _connectionString = connectionString;
}
```

<br>

# 10. Replace Repeated Connection Code

Now the `ActorRepository` no longer needs:

```csharp
private readonly ConnectionString _connectionString;
```

or:

```csharp
using var connection =
    new SqlConnection(_connectionString.IMDBDB);
```

The connection logic is already handled by `BaseRepository`.

<br>

## Updated `ActorRepository`

```csharp
public class ActorRepository
    : BaseRepository<Actor>, IActorRepository
{
    public ActorRepository(
        IOptions<ConnectionString> connectionString)
        : base(connectionString.Value.IMDBDB)
    {
    }

    public IEnumerable<Actor> Get()
    {
        const string query = @"
SELECT [Id]
    ,[Name]
    ,[Bio]
    ,[DateOfRelease] AS [DOB]
    ,[Gender]
FROM [Actors] (NOLOCK)";

        return Get(query);
    }

    public Actor Get(int id)
    {
        const string query = @"
SELECT [Id]
    ,[Name]
    ,[Bio]
    ,[DateOfRelease] AS [DOB]
    ,[Gender]
FROM [Actors] (NOLOCK)
WHERE Id = @Id";

        return Get(query, new { Id = id });
    }
}
```

<br>

# 11. What Changed?

### Before

Each repository method handled:

```text
SQL query
    ↓
Create SqlConnection
    ↓
Execute Dapper query
    ↓
Return result
```

### After

The responsibility is separated:

```text
ActorRepository
    ↓
Defines Actor-specific SQL
    ↓
BaseRepository
    ↓
Creates SqlConnection
    ↓
Executes Dapper query
    ↓
Returns result
```

This removes repeated connection/query execution code from individual repositories.

<br>

# 12. Next: Dapper Stored Procedures

The next topic is:

> **Dapper + Stored Procedures**

Learn how Dapper works with SQL Server stored procedures, including:

- `CommandType.StoredProcedure`
- Input parameters
- Output parameters
- Returning results
- Executing stored procedures using Dapper

Reference:

[Learn Dapper — Stored Procedures](https://www.learndapper.com/stored-procedures?utm_source=chatgpt.com#commandtype.storedprocedure)

<br>

# Key Concepts to Remember

| Concept | Purpose |
|---|---|
| **Dapper** | Micro ORM for executing SQL and mapping results |
| **Microsoft.Data.SqlClient** | SQL Server database provider |
| **ConnectionString POCO** | Strongly typed representation of configuration |
| **IOptions<T>** | Connects configuration to strongly typed classes through DI |
| **SqlConnection** | Represents a connection to SQL Server |
| **Query<T>()** | Executes a query and maps multiple rows to `T` |
| **QueryFirstOrDefault<T>()** | Gets the first matching result or default |
| **Anonymous object** | Supplies Dapper query parameters |
| **using** | Disposes the database connection |
| **BaseRepository<T>** | Centralizes common database/query logic |
| **Generic `<T>`** | Allows the base repository to work with different entities |
| **Stored Procedure** | SQL Server procedure that can be executed through Dapper |
