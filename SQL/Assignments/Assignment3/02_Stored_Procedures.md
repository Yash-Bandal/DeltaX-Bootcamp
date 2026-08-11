# SQL Assignment 3 - Stored Procedures Notes


Database Used:

```sql
USE IMDB_Dummy;
GO
```

<br>
<div align = "center">
    <img width="700" alt="image" src="https://github.com/user-attachments/assets/bc957e51-c774-4018-94f2-0079000d7e19" />
</div>
<br>






# Stored Procedure 1 - Insert Movie

## Requirement

Insert a new movie along with its actors.

A movie is stored in **two tables**.

```text
Movies

+

Actor_Movies
```

<br>

## Procedure

```sql
CREATE PROCEDURE Foundation.usp_InsertMovie
(
    @Name VARCHAR(150),
    @YearOfRelease INT,
    @Plot VARCHAR(300),
    @PosterImagePath VARCHAR(200),
    @ProducerId INT,
    @Language VARCHAR(50),
    @Profit INT,
    @ActorIds VARCHAR(50)
)
AS
BEGIN

    DECLARE @MovieId INT;

    INSERT INTO Foundation.Movies
    (
        Name,
        YearOfRelease,
        Plot,
        PosterImagePath,
        ProducerId,
        Language,
        Profit
    )
    VALUES
    (
        @Name,
        @YearOfRelease,
        @Plot,
        @PosterImagePath,
        @ProducerId,
        @Language,
        @Profit
    );

    SET @MovieId = SCOPE_IDENTITY();

    INSERT INTO Foundation.Actor_Movies
    (
        MovieId,
        ActorId
    )
    SELECT
        @MovieId,
        CAST(value AS INT)
    FROM STRING_SPLIT(@ActorIds, ',');

END;
GO
```

<br>

## Execution

```sql
EXEC Foundation.usp_InsertMovie
    @Name = 'Animal',
    @YearOfRelease = 2023,
    @Plot = 'Action Drama',
    @PosterImagePath = 'animal.jpg',
    @ProducerId = 3,
    @Language = 'Hindi',
    @Profit = 900,
    @ActorIds = '7,8,9';
```

<br>

## Logic

```text
Receive Movie Details

↓

Insert Movie

↓

Get newly generated MovieId

↓

Convert

'7,8,9'

↓

7
8
9

↓

Insert into Actor_Movies

(MovieId, ActorId)

↓

Done
```

<br>

## Before

### Movies

|Id|Movie|
|---:|---|
|11|URI|

### Actor_Movies

|MovieId|ActorId|
|---:|---:|
|11|1|
|11|2|

<br>

## After

### Movies

|Id|Movie|
|---:|---|
|11|URI|
|12|Animal|

### Actor_Movies

|MovieId|ActorId|
|---:|---:|
|11|1|
|11|2|
|12|7|
|12|8|
|12|9|

<br>

## New Concepts

### SCOPE_IDENTITY()

Returns the **Identity value of the row that was just inserted**.

Example

```sql
INSERT INTO Movies(...);

SET @MovieId = SCOPE_IDENTITY();
```

If SQL generated

```
MovieId = 12
```

then

```
@MovieId = 12
```

<br>

### STRING_SPLIT()

Converts a comma-separated string into rows.

```sql
SELECT value
FROM STRING_SPLIT('7,8,9', ',');
```

Output

|value|
|---:|
|7|
|8|
|9|

<br>

---

<br>

# Stored Procedure 2 - Delete Movie

## Requirement

Delete a movie and its actor mappings.

<br>

## Procedure

```sql
CREATE PROCEDURE Foundation.usp_DeleteMovie
(
    @MovieId INT
)
AS
BEGIN

    DELETE FROM Foundation.Actor_Movies
    WHERE MovieId = @MovieId;

    DELETE FROM Foundation.Movies
    WHERE Id = @MovieId;

END;
GO
```

<br>

## Execution

```sql
EXEC Foundation.usp_DeleteMovie
    @MovieId = 12;
```

<br>

## Logic

```text
Movie

↓

Actor_Movies

(Delete Child First)

↓

Movies

(Delete Parent)
```

<br>

## Before

### Movies

|Id|Movie|
|---:|---|
|12|Animal|

### Actor_Movies

|MovieId|ActorId|
|---:|---:|
|12|7|
|12|8|
|12|9|

<br>

## After

### Movies

Movie removed.

### Actor_Movies

Mappings removed.

<br>

---

<br>

# Stored Procedure 3 - Delete Producer

## Requirement

Delete

- Producer
- Movies of that Producer
- Actor mappings of those movies

<br>

## Procedure

```sql
CREATE PROCEDURE Foundation.usp_DeleteProducerMovie
(
    @ProducerId INT
)
AS
BEGIN

    DELETE AM
    FROM Foundation.Actor_Movies AM
    INNER JOIN Foundation.Movies M
        ON AM.MovieId = M.Id
    WHERE M.ProducerId = @ProducerId;

    DELETE FROM Foundation.Movies
    WHERE ProducerId = @ProducerId;

    DELETE FROM Foundation.Producers
    WHERE Id = @ProducerId;

END;
GO
```

<br>

## Execution

```sql
EXEC Foundation.usp_DeleteProducerMovie
    @ProducerId = 50;
```

<br>

## Logic

```text
Producer

↓

Movies

↓

Actor_Movies

Delete Bottom to Top

↓

Actor_Movies

↓

Movies

↓

Producer
```

<br>

## Before

### Producer

|Id|Name|
|---:|---|
|3|Rajkumar Hirani|

### Movies

|Id|ProducerId|
|---:|---:|
|7|3|
|8|3|

### Actor_Movies

|MovieId|ActorId|
|---:|---:|
|7|1|
|7|2|
|8|5|

<br>

## After

Producer deleted.

Movies deleted.

Mappings deleted.

<br>

## Important Concept

### DELETE with JOIN

```sql
DELETE AM
FROM Foundation.Actor_Movies AM
INNER JOIN Foundation.Movies M
    ON AM.MovieId = M.Id
WHERE M.ProducerId = @ProducerId;
```

This **does NOT** mean

```
Delete the Actor_Movies table.
```

It means

> **Delete the rows from the `Actor_Movies` table that participate in this result set.**

Think of it like

```sql
SELECT AM.*
FROM ...
WHERE ...
```

Replace only

```sql
SELECT AM.*
```

with

```sql
DELETE AM
```

The same rows that would have been selected are deleted.

<br>

---

<br>

# Stored Procedure 4 - Delete Actor

## Requirement

Delete an actor.

Do **not** delete movies.

Only remove the actor and its mappings.

<br>

## Procedure

```sql
CREATE PROCEDURE Foundation.usp_DeleteActor
(
    @ActorId INT
)
AS
BEGIN

    DELETE FROM Foundation.Actor_Movies
    WHERE ActorId = @ActorId;

    DELETE FROM Foundation.Actors
    WHERE Id = @ActorId;

END;
GO
```

<br>

## Execution

```sql
EXEC Foundation.usp_DeleteActor
    @ActorId = 20;
```

<br>

## Logic

```text
Actor

↓

Actor_Movies

(Delete Mapping)

↓

Actors

(Delete Actor)

↓

Movie remains unchanged
```

<br>

## Before

### Movie

```
URI

Vicky
Yami
Paresh
```

### Actor_Movies

|MovieId|ActorId|
|---:|---:|
|1|1|
|1|2|
|1|3|

<br>

## After deleting ActorId = 2

### Movie

```
URI

Vicky
Paresh
```

### Actor_Movies

|MovieId|ActorId|
|---:|---:|
|1|1|
|1|3|

Movie is **not deleted** because multiple actors can belong to the same movie.

<br>

# Overall Flow

## Insert Movie

```
Movie Details

↓

Movies

↓

MovieId

↓

Actor_Movies
```

<br>

## Delete Movie

```
Actor_Movies

↓

Movies
```

<br>

## Delete Producer

```
Actor_Movies

↓

Movies

↓

Producer
```

<br>

## Delete Actor

```
Actor_Movies

↓

Actor
```

<br>

#  Concepts Learned

- `SCOPE_IDENTITY()` returns the identity of the row inserted in the current scope.
- `STRING_SPLIT()` converts a comma-separated string into multiple rows.
- Always delete **child records before parent records** to satisfy foreign key constraints.
- `DELETE AM FROM ... JOIN ...` deletes only the matching rows from the table represented by alias `AM`, **not the entire table**.
- A safe way to understand `DELETE ... JOIN` is to first write it as `SELECT AM.*`; then replace `SELECT AM.*` with `DELETE AM`.

