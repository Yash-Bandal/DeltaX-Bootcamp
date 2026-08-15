# SQL Assignment 3 - Indexing & Query Performance 

## Overview

| Question | Key Learning |
|-----------|--------------|
| Search by Name | Create a **Non-Clustered Index** on `Movies.Name`. |
| Primary Key | Creates a **Clustered Index** by default if one doesn't already exist. |
| DATEDIFF() | Avoid functions on indexed columns; compare the column directly to a computed value. |
| JOIN Performance | Index filter columns (`Producers.Name`) and join columns (`Movies.ProducerId`) to enable efficient seeks. |

Database Used:

```sql
USE IMDB_Dummy;
GO
```

<br>

# 1. Search Movies by Name

## Question

Users most frequently search for movies using:

```sql
SELECT *
FROM Foundation.Movies
WHERE Name = 'Bugonia';
```

Which type of index would be most efficient?

Write the SQL statement to create that index.

<br>

## Answer

A **Non-Clustered Index** should be created on the **Name** column.

<br>

## Reason

- The table already has a **Primary Key (`Id`)**, which by default creates a **Clustered Index** (if one doesn't already exist).
- A table can have **only one Clustered Index**, so we should not change the physical order of rows just because users frequently search by movie name.
- A **Non-Clustered Index** creates a separate sorted structure on `Name`, allowing SQL Server to quickly locate matching rows.
- Clustered index is efficient in terms of searching in Range (Between), and Non clustered in terms of equality (Filtering - with WHERE and all)
  
<br>

## Query

```sql
CREATE NONCLUSTERED INDEX IX_Movies_Name
ON Foundation.Movies(Name);
```

<br>

## Search Flow

Without Index

```text
Movies Table

↓

Scan every row

↓

Find 'Bugonia'
```

With Index

```text
Name Index

↓

Index Seek

↓

Locate matching row

↓

Fetch movie data
```

<br>

---

<br>

# 2. Does PRIMARY KEY automatically create a Clustered Index?

## Question

When a `PRIMARY KEY` constraint is created on a table, is a Clustered Index automatically created?

<br>

## Answer

**Yes**, by default SQL Server creates a **Clustered Index** for a Primary Key **only if the table does not already have a Clustered Index**.

If a Clustered Index already exists, SQL Server creates the Primary Key as a **Unique Non-Clustered Index**.

<br>

## Example

```sql
CREATE TABLE DemoTable
(
    Id INT PRIMARY KEY,
    Name VARCHAR(50)
);
```

Result:

```text
Primary Key

↓

Clustered Index on Id
```

If another Clustered Index already exists:

```text
Primary Key

↓

Unique Non-Clustered Index
```

<br>

## Important Points

- One table can have **only one Clustered Index**.
- A table can have **multiple Non-Clustered Indexes**.


<br>

---

<br>

# 3. Index on DOB and DATEDIFF()

## Question

A Non-Clustered Index exists on `DOB`.

```sql
SELECT Id
FROM Actors
WHERE DATEDIFF(DAY, DOB, GETDATE()) > 30;
```

Will SQL Server use the index efficiently?

Rewrite the query.

<br>

## Answer

**No.**

The query applies a function (`DATEDIFF`) on the indexed column (`DOB`).

SQL Server must calculate `DATEDIFF()` for every row before comparing it, preventing efficient index usage.

<br>

## Original Query

```sql
SELECT Id
FROM Actors
WHERE DATEDIFF(DAY, DOB, GETDATE()) > 30;
```

<br>

## Mathematical Conversion

```text
Solution:

DATEDIFF(DAY, DOB, GETDATE()) > 30

=

GETDATE() - DOB > 30

Let

T = GETDATE()
D = DOB

T - D > 30
multiplying - on both sides

-T + D < -30
     D < -30 + T
	 D < T - 30

	↓

DOB < GETDATE() - 30 Days
DOB < DATEADD(DAY, -30, GETDATE())
```

<br>

## Better Query

```sql
SELECT Id
FROM Actors
WHERE DOB < DATEADD(DAY, -30, GETDATE());
```

<br>

## Why is this better?

Old Query

```text
Every Row

↓

Calculate DATEDIFF()

↓

Compare
```

New Query

```text
Calculate Date Once

↓

Compare DOB directly

↓

Index Seek
```

<br>


## Key Concept

Avoid applying functions like:

- `DATEDIFF()`
- `YEAR()`
- `MONTH()`
- `UPPER()`
- `LOWER()`

directly on indexed columns in the `WHERE` clause.

Instead, rewrite the condition so the indexed column is compared directly.


<br>

---

<br>


# 4. Indexes for JOIN Performance

## Question

Retrieve all movies produced by **Aditya Chopra**.

```sql
SELECT M.Name
FROM Foundation.Movies M
INNER JOIN Foundation.Producers P
    ON M.ProducerId = P.Id
WHERE P.Name = 'Aditya Chopra';
```

Which columns should be indexed?

<br>


## Answer

The following columns should be indexed:

1. `Producers.Name`
2. `Movies.ProducerId`

`Producers.Id` does **not** require another index because it is already indexed as the Primary Key.

<br>


## Why?

### Producers.Name

Used in:

```sql
WHERE P.Name = 'Aditya Chopra'
```

Allows SQL Server to quickly locate the producer.

<br>

### Movies.ProducerId

Used in:

```sql
ON M.ProducerId = P.Id
```

Allows SQL Server to quickly locate all movies belonging to that producer.

<br>


## Create Indexes

```sql
CREATE NONCLUSTERED INDEX IX_Producers_Name
ON Foundation.Producers(Name);
```

```sql
CREATE NONCLUSTERED INDEX IX_Movies_ProducerId
ON Foundation.Movies(ProducerId);
```

<br>


## Query Flow

Without Indexes

```text
Scan Producers

↓

Find Aditya Chopra

↓

Scan Movies

↓

Find matching ProducerId
```

With Indexes

```text
Index Seek on Producers.Name

↓

Get Producer Id

↓

Index Seek on Movies.ProducerId

↓

Return Movies
```

<br>


## Why is it faster?

- Avoids scanning the entire `Producers` table.
- Avoids scanning the entire `Movies` table.
- Uses **Index Seek** instead of **Table Scan**.
- Improves JOIN performance, especially for large datasets.

<br>


