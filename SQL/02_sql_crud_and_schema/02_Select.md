# SQL Server – SELECT Statement 

## SELECT

Used to retrieve data from one or more tables.

### Syntax

```sql
SELECT column1, column2
FROM TableName;
```

Retrieve all columns:

```sql
SELECT *
FROM Foundation.Actor;
```

Retrieve specific columns:

```sql
SELECT Name, DateOfBirth
FROM Foundation.Actor;
```

> **Best Practice:** Avoid `SELECT *` in production. Retrieve only the required columns.

<br>

# DISTINCT

Removes duplicate rows from the result.

### Syntax

```sql
SELECT DISTINCT ColumnName
FROM TableName;
```

Example:

```sql
SELECT DISTINCT Sex
FROM Foundation.Actor;
```

Output

```text
Male
Female
```

<br>

# WHERE Clause

Filters rows based on a condition.

### Syntax

```sql
SELECT *
FROM TableName
WHERE condition;
```

Examples

```sql
SELECT *
FROM Foundation.Actor
WHERE Sex = 'Male';
```

```sql
SELECT *
FROM Foundation.Movie
WHERE YearOfRelease > 2020;
```

Comparison Operators

| Operator     | Meaning               |
| --------------- | ----------------------- |
| `=`          | Equal                 |
| `<>` or `!=` | Not Equal             |
| `>`          | Greater Than          |
| `<`          | Less Than             |
| `>=`         | Greater Than or Equal |
| `<=`         | Less Than or Equal    |

<br>

# Wildcards (LIKE)

Used with `LIKE` for pattern matching.

| Wildcard | Meaning                          |
| --------------- | ----------------------- |
| `%`      | Zero or more characters          |
| `_`      | Exactly one character            |
| `[ABC]`  | Any one listed character         |
| `[A-Z]`  | Character range                  |
| `[^ABC]` | Not one of the listed characters |

Examples

> [!Note]
> #### You also have `NOT LIKE`
> ```sql
> SELECT *
> FROM Foundation.Actor
> WHERE Name NOT LIKE 'A%';
> ```


Starts with A

```sql
SELECT *
FROM Foundation.Actor
WHERE Name LIKE 'A%';
```

Ends with l

```sql
WHERE Name LIKE '%l';
```

Contains "an"

```sql
WHERE Name LIKE '%an%';
```

Second character is 'a'

```sql
WHERE Name LIKE '_a%';
```

Starts with A, B, or C

```sql
WHERE Name LIKE '[ABC]%';
```

Starts with any letter from A to M

```sql
WHERE Name LIKE '[A-M]%';
```

Doesn't start with A, B, or C

```sql
WHERE Name LIKE '[^ABC]%';
```

<br>

# AND / OR Operators

Combine multiple conditions.

### AND

Both conditions must be true.

```sql
SELECT *
FROM Foundation.Movie
WHERE YearOfRelease > 2020
AND ProducerId = 1;
```

### OR

At least one condition must be true.

```sql
SELECT *
FROM Foundation.Actor
WHERE Sex = 'Female'
OR Name = 'Paresh Rawal';
```

<br>

# ORDER BY

Sorts the result.

Ascending (Default)

```sql
SELECT *
FROM Foundation.Actor
ORDER BY Name;
```

Descending

```sql
SELECT *
FROM Foundation.Actor
ORDER BY Name DESC;
```

Multiple columns

```sql
SELECT *
FROM Foundation.Movie
ORDER BY YearOfRelease DESC, Name ASC;
```

<br>

# TOP

Returns the first **N** rows.

```sql
SELECT TOP 5 *
FROM Foundation.Actor;
```

<br>

# TOP PERCENT

Returns the top percentage of rows.

```sql
SELECT TOP 50 PERCENT *
FROM Foundation.Movie;
```

<br>

# SELECT Execution Order

Although we write:

```sql
SELECT Name
FROM Foundation.Actor
WHERE Sex = 'Male'
ORDER BY Name;
```

SQL Server processes it as:

1. `FROM`
2. `WHERE`
3. `SELECT`
4. `ORDER BY`

<br>

# Best Practices

* Use **specific column names** instead of `SELECT *`.
* Use `DISTINCT` only when needed (it adds extra processing).
* Always use `ORDER BY` if the result order matters.
* Use `WHERE` to filter data as early as possible.
* Prefer `TOP ... ORDER BY` together for predictable results.

<br>

# Quick Revision

| Keyword         | Purpose                 |
| --------------- | ----------------------- |
| `SELECT`        | Retrieve data           |
| `*`             | Select all columns      |
| `DISTINCT`      | Remove duplicate rows   |
| `WHERE`         | Filter rows             |
| `LIKE`          | Pattern matching        |
| `%`             | Zero or more characters |
| `_`             | One character           |
| `AND`           | All conditions true     |
| `OR`            | Any condition true      |
| `ORDER BY`      | Sort results            |
| `ASC`           | Ascending (default)     |
| `DESC`          | Descending              |
| `TOP n`         | First n rows            |
| `TOP n PERCENT` | First n% of rows        |
