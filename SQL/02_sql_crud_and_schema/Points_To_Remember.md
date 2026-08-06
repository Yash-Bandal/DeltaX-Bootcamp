### Unique vs Primary key
| Feature | Primary Key | Unique Key |
| :--- | :--- | :--- |
| **Quantity** | Only **one** allowed per table. | **Multiple** allowed per table. |
| **NULL Values** | Strictly **no NULL** values allowed. | Allows **one NULL** value (varies by database). |
| **Default Index** | Automatically creates a **Clustered Index**. | Automatically creates a **Non-Clustered Index**. |


<br>

### WHERE vs HAVING
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
