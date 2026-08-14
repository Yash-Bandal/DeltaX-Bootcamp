# Points to Rememmber

## Index
1. [Primary Key vs Unique](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/SQL/Points_To_Remember.md#1-primary-key-vs-unique)
2. [Where vs Having - No Aggregates with WHERE](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/SQL/Points_To_Remember.md#2-where-vs-having)
3. [UNION vs UNION ALL](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/SQL/Points_To_Remember.md#3-union-vs-union-all)
4. [UNION vs Join](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/SQL/Points_To_Remember.md#4-union-vs-join)
5. [With and Without Group BY](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/SQL/Points_To_Remember.md#5-with-and-without-group-by)
6. [Group by Primary Key + Display Columns](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/SQL/Points_To_Remember.md#6-group-by-primary-key--display-columns)
7. [Group By and Aggregate](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/SQL/Points_To_Remember.md#7-dont-group-by-aggregating-column)

<br>


### 1. Primary key vs Unique 
| Feature | Primary Key | Unique Key |
| :--- | :--- | :--- |
| **Quantity** | Only **one** allowed per table. | **Multiple** allowed per table. |
| **NULL Values** | Strictly **no NULL** values allowed. | Allows **one NULL** value (varies by database). |
| **Default Index** | Automatically creates a **Clustered Index**. | Automatically creates a **Non-Clustered Index**. |


<br>



### 2. WHERE vs HAVING

> [!caution]
> You cannot use where with aggregate

<img width="610" height="328" alt="image" src="https://github.com/user-attachments/assets/d4b25306-93c6-48ec-9c0d-d4632334a205" />


❌ Incorrect
```sql
SELECT *
FROM tblEmployee
WHERE SUM(Salary) > 4000;
```
Error:
```
An aggregate may not appear in the WHERE clause...
```

✅ Correct: Use HAVING
```sql
SELECT Department,
       SUM(Salary) AS TotalSalary
FROM tblEmployee
GROUP BY Department
HAVING SUM(Salary) > 4000;
```

<br>

## 3. UNION vs UNION ALL

| Feature | UNION | UNION ALL |
|---------|-------|-----------|
| Removes duplicate rows | ✅ Yes | ❌ No |
| Keeps duplicate rows | ❌ No | ✅ Yes |
| Performance | Slower (checks duplicates) | Faster |
| Use when | Need unique results | Need all results, including duplicates |

### Example Tables

#### Table A

| Name |
|------|
| Alice |
| Bob |
| Charlie |

#### Table B

| Name |
|------|
| Bob |
| David |
| Emma |

### UNION

```sql
SELECT Name FROM TableA
UNION
SELECT Name FROM TableB;
```

#### Result

| Name |
|------|
| Alice |
| Bob |
| Charlie |


> **Note:** Duplicate values (e.g., **Bob**) are removed.

<br>

### UNION ALL

```sql
SELECT Name FROM TableA
UNION ALL
SELECT Name FROM TableB;
```

### Result

| Name |
|------|
| Alice |
| Bob |
| Charlie |
| Bob |


duplicate Bob not removed


<br>

## 4. UNION vs JOIN

| Feature | UNION | JOIN |
|---------|-------|------|
|Direction | Horizontal (makes table wider) | Vertical (makes table taller)|
| Purpose | Combines rows from two or more `SELECT` queries | Combines columns from two or more tables |
| Combines 🏷️ | Rows | Columns |
| Number of Columns | Must be the same in both queries | Can be different |
| Matching Condition | Not required | Required (`ON` clause, except `CROSS JOIN`) |
| Duplicate Handling | `UNION` removes duplicates, `UNION ALL` keeps them | No duplicate removal by default |
| Use Case | Merge similar data from multiple tables | Retrieve related data from multiple tables |

<br>

## 5. With and Without Group BY
Think of it like this:

Without GROUP BY:

```sql
SELECT COUNT(*)
FROM Table;
```

➡ Counts every row in the table.

With GROUP BY:
```sql
SELECT Actor1, Actor2, COUNT(*)
FROM ...
GROUP BY Actor1, Actor2;
```

It's almost as if SQL does this internally:
```
For each unique (Actor1, Actor2):

    Count how many rows belong to THIS pair

    Output one row
```
So COUNT(*) is per group, not for the entire table.

A nice way to think about it is:

GROUP BY splits the table into many mini-tables, and COUNT(*) counts rows inside each mini-table.

This mental model works for SUM(), AVG(), MIN(), MAX(), etc. as well.


<br>

## 6. Group by Primary Key + Display Columns

Whenever you use an aggregate function (`COUNT`, `SUM`, `AVG`, `MIN`, `MAX`):

> **Every non-aggregated column in the `SELECT` must appear in the `GROUP BY` clause.**

### ❌ Wrong 🏷️

```sql
SELECT
    C.Name,
    COUNT(S.subscribed_to)
FROM Channel_Profile C
INNER JOIN Subscription S
    ON C.Id = S.subscribed_to
GROUP BY
    S.subscribed_to;
```

**Reason:** `C.Name` is selected but not included in `GROUP BY`.



### ✅ Right

```sql
SELECT
    C.Name,
    COUNT(S.subscribed_to)
FROM Channel_Profile C
INNER JOIN Subscription S
    ON C.Id = S.subscribed_to
GROUP BY
    C.Id,
    C.Name;
```



### Easy Memory Trick

✅ **Ask yourself:**

> **"Am I printing this column directly?"**

If **Yes**, then:
- Either put it in `GROUP BY`
- Or wrap it inside an aggregate function.

Otherwise, SQL Server throws an error.


<br>




## 7 Dont Group By Aggregating Column
Do not GROUP BY the column that you're aggregating.

WRONG
```sql
SELECT Department, AVG(Salary)
FROM Employees
GROUP BY Department, Salary;
```
