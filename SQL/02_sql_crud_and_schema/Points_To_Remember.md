### Unique vs Primary key
| Feature | Primary Key | Unique Key |
| :--- | :--- | :--- |
| **Quantity** | Only **one** allowed per table. | **Multiple** allowed per table. |
| **NULL Values** | Strictly **no NULL** values allowed. | Allows **one NULL** value (varies by database). |
| **Default Index** | Automatically creates a **Clustered Index**. | Automatically creates a **Non-Clustered Index**. |


<br>



### WHERE vs HAVING

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

## UNION vs UNION ALL

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
| David |
| Emma |

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
| David |
| Emma |

duplicate Bob not removed


<br>

## UNION vs JOIN

| Feature | UNION | JOIN |
|---------|-------|------|
| Purpose | Combines rows from two or more `SELECT` queries | Combines columns from two or more tables |
| Combines | Rows | Columns |
| Number of Columns | Must be the same in both queries | Can be different |
| Matching Condition | Not required | Required (`ON` clause, except `CROSS JOIN`) |
| Duplicate Handling | `UNION` removes duplicates, `UNION ALL` keeps them | No duplicate removal by default |
| Use Case | Merge similar data from multiple tables | Retrieve related data from multiple tables |
