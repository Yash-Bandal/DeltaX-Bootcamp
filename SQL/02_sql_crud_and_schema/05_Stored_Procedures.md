# SQL Server – Stored Procedures (Quick Notes)

## What is a Stored Procedure?

A **Stored Procedure (SP)** is a precompiled collection of one or more SQL statements stored in the database and executed as a single unit.

<br>

# Advantages

* Reusable code
* Better performance (execution plan is cached)
* Reduces network traffic
* Improves security (grant EXECUTE instead of table access)
* Easier maintenance

<br>

# Naming Convention

Avoid `sp_` (reserved for system stored procedures).

Use:

```text
usp_GetMovies
usp_AddActor
usp_UpdateProducer
usp_DeleteMovie
```

<br>

# Create a Stored Procedure

```sql
CREATE PROCEDURE usp_GetActors
AS
BEGIN
    SELECT *
    FROM Foundation.Actors;
END;
```

Execute

```sql
EXEC usp_GetActors;

-- or

EXECUTE usp_GetActors;
```

<br>

# Procedure with Parameters

```sql
CREATE PROCEDURE usp_GetMovieById
    @MovieId INT
AS
BEGIN
    SELECT *
    FROM Foundation.Movies
    WHERE Id = @MovieId;
END;
```

Execute

```sql
EXEC usp_GetMovieById @MovieId = 1;

-- or

EXEC usp_GetMovieById 1;
```

<br>

# Multiple Parameters

```sql
CREATE PROCEDURE usp_GetMovies
    @ProducerId INT,
    @Year INT
AS
BEGIN
    SELECT *
    FROM Foundation.Movies
    WHERE ProducerId = @ProducerId
      AND YearOfRelease = @Year;
END;
```

<br>

# Default Parameter Value

```sql
CREATE PROCEDURE usp_GetMoviesByYear
    @Year INT = 2023
AS
BEGIN
    SELECT *
    FROM Foundation.Movies
    WHERE YearOfRelease = @Year;
END;
```

Execute

```sql
EXEC usp_GetMoviesByYear;
```

or

```sql
EXEC usp_GetMoviesByYear 2024;
```

<br>

# OUTPUT Parameter

Returns a value through a parameter.

```sql
CREATE PROCEDURE usp_GetMovieCount
    @Count INT OUTPUT
AS
BEGIN
    SELECT @Count = COUNT(*)
    FROM Foundation.Movies;
END;
```

Execute

```sql
DECLARE @MovieCount INT;

EXEC usp_GetMovieCount @MovieCount OUTPUT;

SELECT @MovieCount;
```

<br>

# Return Value

Returns an integer status/value.

```sql
CREATE PROCEDURE usp_TotalMovies
AS
BEGIN
    RETURN (SELECT COUNT(*) FROM Foundation.Movies);
END;
```

Execute

```sql
DECLARE @Result INT;

EXEC @Result = usp_TotalMovies;

SELECT @Result;
```

> **Note:** `RETURN` is generally used for **status codes** (0 = Success, non-zero = Error). Use **OUTPUT parameters** to return data.

<br>

# ALTER Procedure

Modify an existing procedure.

```sql
ALTER PROCEDURE usp_GetActors
AS
BEGIN
    SELECT Name, DateOfBirth
    FROM Foundation.Actors;
END;
```

<br>

# DROP Procedure

Delete a procedure.

```sql
DROP PROCEDURE usp_GetActors;
```

<br>

# Encrypted Procedure

Hide the procedure definition.

```sql
CREATE PROCEDURE usp_GetActors
WITH ENCRYPTION
AS
BEGIN
    SELECT *
    FROM Foundation.Actors;
END;
```

<br>

# View Procedure Definition

```sql
sp_helptext usp_GetActors;
```

or

```sql
EXEC sp_helptext 'usp_GetActors';
```

> Won't work if the procedure was created with `WITH ENCRYPTION`.

<br>

# System Stored Procedures

Built-in procedures provided by SQL Server.

Examples

```sql
EXEC sp_help Foundation.Actors;

EXEC sp_helpdb;

EXEC sp_helptext 'usp_GetActors';
```

<br>

# Best Practices

* Prefix user procedures with `usp_`.
* Always use `BEGIN...END`.
* Use parameters instead of hardcoded values.
* Use `SET NOCOUNT ON;` to avoid "(n rows affected)" messages.

Example

```sql
CREATE PROCEDURE usp_GetActors
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Foundation.Actors;
END;
```

<br>

# Quick Revision

| Statement          | Purpose                      |
| ------------------ | ---------------------------- |
| `CREATE PROCEDURE` | Create a procedure           |
| `EXEC` / `EXECUTE` | Execute a procedure          |
| `ALTER PROCEDURE`  | Modify a procedure           |
| `DROP PROCEDURE`   | Delete a procedure           |
| `@Parameter`       | Input parameter              |
| `OUTPUT`           | Return values via parameters |
| `RETURN`           | Return integer status/value  |
| `WITH ENCRYPTION`  | Hide procedure definition    |
| `SET NOCOUNT ON`   | Suppress row count messages  |

### Interview Tip

* **Procedure** = Executes SQL and can modify data.
* **Function** = Must return a value and has more restrictions (e.g., cannot perform transactions or modify tables in the same way as procedures).
