# SQL Server – Stored Procedures 

## What is a Stored Procedure?

A **Stored Procedure (SP)** is a precompiled collection of one or more SQL statements stored in the database and executed as a single unit.

### Advantages

* Reusable code
* Better performance (execution plan is cached)
* Reduces network traffic
* Improves security
* Easier maintenance

<br>

# Creating Stored Procedures

## Naming Convention

Avoid `sp_` (reserved for system procedures).

Use:

```text
usp_GetActors
usp_AddMovie
usp_UpdateProducer
usp_DeleteActor
```

<br>

## Create a Procedure

```sql
CREATE PROCEDURE usp_GetActors
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Foundation.Actors;
END;
```

Execute

```sql
EXEC usp_GetActors;
```

<br>

## Procedure with Parameters

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
EXEC usp_GetMovieById 1;
```

<br>

## Multiple Parameters

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

## Default Parameter

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
EXEC usp_GetMoviesByYear 2024;
```

<br>

# Returning Values

## OUTPUT Parameter

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

## RETURN Value

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

> **Note:** Use `RETURN` for status codes. Use `OUTPUT` parameters to return data.

<br>

# Managing Stored Procedures

## ALTER Procedure

```sql
ALTER PROCEDURE usp_GetActors
AS
BEGIN
    SELECT Name, DateOfBirth
    FROM Foundation.Actors;
END;
```

<br>

## View Procedure Definition

```sql
EXEC sp_helptext 'usp_GetActors';
```
Gives the code of the procedure

> Doesn't work if created with `WITH ENCRYPTION`.

<br>

## DROP Procedure

```sql
DROP PROCEDURE usp_GetActors;
```

<br>

# Useful Options

## WITH ENCRYPTION

Hides the procedure definition.

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

## SET NOCOUNT ON

Suppresses the "(n rows affected)" message.

```sql
SET NOCOUNT ON;
```

<br>

# Quick Revision

| Statement          | Purpose                          |
| ------------------ | -------------------------------- |
| `CREATE PROCEDURE` | Create a procedure               |
| `EXEC` / `EXECUTE` | Execute a procedure              |
| `ALTER PROCEDURE`  | Modify a procedure               |
| `sp_helptext`      | View procedure definition        |
| `DROP PROCEDURE`   | Delete a procedure               |
| `@Parameter`       | Input parameter                  |
| `OUTPUT`           | Return values through parameters |
| `RETURN`           | Return integer status/value      |
| `WITH ENCRYPTION`  | Hide procedure definition        |
| `SET NOCOUNT ON`   | Suppress row count messages      |

### Interview Tip

* **Stored Procedure** → Executes SQL statements and can return result sets, output parameters, or status codes.
* Prefer **parameterized procedures** over hardcoded queries for better reusability and security.
