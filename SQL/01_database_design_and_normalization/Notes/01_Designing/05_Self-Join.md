# SQL Self Join

A **Self Join** is a join where a table is joined with **itself**. It is useful when the rows within the same table are related to each other.

Unlike other joins, a self join **does not require another table**. Instead, the same table is referenced twice using **table aliases**.

<br>

## Why Use a Self Join?

A Self Join is commonly used when:

- An employee reports to another employee.
- Products have parent products.
- Categories have parent categories.
- Family relationships (parent-child).
- Comparing rows within the same table.

<br>

## Example Table

### `employee`

| employee_id | employee_name | manager_id |
| :---------- | :------------ | :--------- |
| 1 | Alice | NULL |
| 2 | Bob | 1 |
| 3 | Charlie | 1 |
| 4 | David | 2 |
| 5 | Emma | 2 |

Here:

- Alice is the CEO (no manager).
- Bob and Charlie report to Alice.
- David and Emma report to Bob.

<br>

## Problem

Display each employee along with their manager's name.

### Expected Output

| Employee | Manager |
| :------- | :------ |
| Alice | NULL |
| Bob | Alice |
| Charlie | Alice |
| David | Bob |
| Emma | Bob |

<br>

## Solution: Self Join

Since both the employee and manager information exist in the **same table**, we join the table with itself.

```sql
SELECT
    e.employee_name AS Employee,
    m.employee_name AS Manager
FROM employee AS e
LEFT JOIN employee AS m
    ON e.manager_id = m.employee_id;
```

<br>

## Understanding the Query

```sql
FROM employee AS e
```

Treats the table as the **Employee** table.

```sql
LEFT JOIN employee AS m
```

Treats the same table again as the **Manager** table.

```sql
ON e.manager_id = m.employee_id
```

Matches each employee's `manager_id` with the manager's `employee_id`.

<br>

## Visualization

```
employee (e)                  employee (m)

employee_id                   employee_id
employee_name                 employee_name
manager_id                    manager_id

      │
      │ e.manager_id
      ▼
m.employee_id
```

Example:

```
Bob
manager_id = 1
      │
      ▼
Alice
employee_id = 1
```

<br>

## Using INNER JOIN

If you only want employees who have managers:

```sql
SELECT
    e.employee_name AS Employee,
    m.employee_name AS Manager
FROM employee AS e
INNER JOIN employee AS m
    ON e.manager_id = m.employee_id;
```

### Output

| Employee | Manager |
| :------- | :------ |
| Bob | Alice |
| Charlie | Alice |
| David | Bob |
| Emma | Bob |

Notice that **Alice is excluded** because she does not have a manager.

<br>

## LEFT JOIN vs INNER JOIN

### LEFT JOIN

- Returns **all employees**.
- Employees without managers will have `NULL` as the manager.

### INNER JOIN

- Returns **only employees that have managers**.
- Employees without matching managers are excluded.

<br>

## Why Table Aliases Are Required

Without aliases, SQL cannot distinguish between the two instances of the same table.

Instead of writing:

```sql
employee.employee_name
```

we write:

```sql
e.employee_name
m.employee_name
```

where:

- `e` → Employee
- `m` → Manager

<br>

