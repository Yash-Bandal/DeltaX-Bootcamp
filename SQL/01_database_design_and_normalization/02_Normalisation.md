<img width="626" height="434" alt="image" src="https://github.com/user-attachments/assets/be09dcdd-7525-4a9f-b8df-ab7f337c31be" /># Database Normalization

## What is Database Normalization?
Database Normalization is the process of organizing data in a database to:

- Minimize **data redundancy** (duplicate data)
- Improve **data consistency**
- Reduce **data anomalies**
- Improve **database maintainability**

There are **six normal forms (1NF–6NF)**, but in real-world applications, most databases are normalized up to **Third Normal Form (3NF)**.


<br>


> [!Tip]
> ### `2NF` vs `3NF` major difference
>  - In 2NF, the Primary key is a **Composite Primary Key**
>  - In 3NF, the Primary key is perfectly fine, **Normal primary key**
> 

<br>

# Why Do We Need Normalization?

Without normalization, the same information is stored repeatedly, causing several problems.

## Problems Caused by Data Redundancy

### 1. Data Redundancy
- Same data is stored multiple times.
- Increases storage requirements.

**Example**

| EmployeeID | EmployeeName | Department | Location |
|------------|--------------|------------|----------|
| 1 | Alice | IT | London |
| 2 | Bob | IT | London |
| 3 | Charlie | IT | London |

Here, **IT** and **London** are repeated for every employee.

using `3NF`

**Departments**
| DepartmentID | Department | Location |
| ------------ | ---------- | -------- |
| 1            | IT         | London   |

**Employees**
| EmployeeID | EmployeeName | DepartmentID |
| ---------- | ------------ | ------------ |
| 1          | Alice        | 1            |
| 2          | Bob          | 1            |
| 3          | Charlie      | 1            |


<br>

### 2. Data Inconsistency

If department information changes, every row must be updated.

Example:

Old:

```
Department = IT
Location = London
```

New:

```
Department = IT
Location = Manchester
```

If one row is missed during update:

| Employee | Department | Location |
|----------|------------|----------|
| Alice | IT | Manchester |
| Bob | IT | London |

Now the database contains conflicting information.

<br>

### 3. Disk Space Wastage

Repeated values consume unnecessary storage.

Instead of storing:

```
IT
IT
IT
IT
IT
```

Store it once in a separate table and reference it using an ID.

<br>

### 4. Poor Performance

Operations like:

- INSERT
- UPDATE
- DELETE

become slower because duplicate data exists in many rows.


<br>

---

<br>



# First Normal Form (1NF)

## Definition

A table is in **First Normal Form (1NF)** if:

- Every column contains **atomic (single) values**
- No repeating groups
- Every row is uniquely identified by a Primary Key

<br>
<div align = "center">
      <img width="650" alt="image" src="https://github.com/user-attachments/assets/6509c45b-f416-4c70-b89c-2e8350622c6b" />
</div>
<br>

## Rules of 1NF

### Rule 1: Atomic Values

Each cell should contain only one value.

### Wrong

| Student | Subjects |
|----------|----------|
| John | Math, Science |

The Subjects column stores multiple values.

> [!caution]
> Now, if non-atomic, its not possible to apply SEELECT, DELETE, INSERT on just one subject

<br>

### Correct

| Student | Subject |
|----------|----------|
| John | Math |
| John | Science |

Each cell now contains only one value.

<br>

### Rule 2: No Repeating Columns

Avoid columns like:

| Student | Subject1 | Subject2 | Subject3 |
|----------|-----------|-----------|-----------|

Problems:

- Many NULL values
- Difficult to add new subjects
- Requires ALTER TABLE frequently

Instead create:

| Student | Subject |
|----------|----------|
| John | Math |
| John | Science |

<br>

### Rule 3: Primary Key

Each row should be uniquely identifiable.

Example:

```
StudentID
```

or

```
EmployeeID
```

<br>

## 1NF Solution

Split data into multiple tables and connect them using a **Foreign Key**.

---

<br>



# Second Normal Form (2NF)


## Definition

A table is in **Second Normal Form (2NF)** if:

- It satisfies **1NF**
- Redundant data is moved into separate tables
- Relationships are maintained using Foreign Keys
- Ensure no `Partial Dependency`

> [!Note]
>### Partial Dependency
> **Definition**:
> A non-key attribute depends on only a part of a composite primary key instead of the whole composite key.
>```
> Primary Key = (StudentID, CourseID)
>```
> This is called a Composite Key.
>
>  The primary key has 2 columns
>
> `(StudentID, CourseID)`
>
> But StudentName depends only on
>
> `StudentID`
>
> not on the complete key.
>
> Likewise,
>
> CourseName depends only on
>
> `CourseID`
>
> not the whole key.
> Only part of the composite key determines the value.
>
> That is why it is called **`Partial Dependency`**
>
> Thus we use **Listing / Junction Tables**

<br>
<div align = "center">
      <img width="650" alt="image" src="https://github.com/user-attachments/assets/3dde22ae-36ed-4743-b6d5-6c62978a62f0" />
</div>
<br>

## Why 2NF?

Instead of storing department information repeatedly:

### Employees

| EmployeeID | Name | DepartmentID |
|------------|------|--------------|
| 1 | Alice | 1 |
| 2 | Bob | 1 |

<br>

### Departments

| DepartmentID | Department | Location |
|--------------|------------|----------|
| 1 | IT | London |

Department information is stored only once.

<br>

## Benefits of 2NF

- Less duplication
- Easier updates
- Smaller tables
- Better consistency

---

<br>

# Third Normal Form (3NF)

## Definition

A table is in **Third Normal Form (3NF)** if:

- It satisfies **1NF**
- It satisfies **2NF**
- Every non-key attribute depends **only on the Primary Key**
- No transitive dependency

<br>
<div align = "center">
      <img width="650" alt="image" src="https://github.com/user-attachments/assets/4133b799-7c17-4d72-a1ab-6068dcd09831" />
</div>
<br>



## Functional Dependency

Every column should depend only on the Primary Key.

Example:

```
EmployeeID
      ↓
EmployeeName
DepartmentID
Salary
```

Correct because all attributes depend directly on EmployeeID.

<br>

## Transitive Dependency

A non-key column depends on another non-key column.

### Wrong

| EmployeeID | DepartmentID | DepartmentName | DepartmentHead |
|------------|--------------|----------------|----------------|

Here,

```
EmployeeID
      ↓
DepartmentID
      ↓
DepartmentName
      ↓
DepartmentHead
```

DepartmentHead depends on DepartmentName instead of EmployeeID.

This is called **Transitive Dependency**.

<br>

## Solution

Move department information into another table.

### Employees

| EmployeeID | Name | DepartmentID |
|------------|------|--------------|

<br>

### Departments

| DepartmentID | DepartmentName | DepartmentHead |
|--------------|----------------|----------------|

<br>

## Avoid Computed Columns

Do not store values that can be calculated.

### Wrong

| MonthlySalary | AnnualSalary |
|---------------|--------------|

Since

```
AnnualSalary = MonthlySalary × 12
```

Store only:

```
MonthlySalary
```

Calculate AnnualSalary when needed.

**Before 3NF**
```
Employees

+-----------+-------------+--------------+----------------+
|EmployeeID |EmployeeName |DepartmentID  |DepartmentName  |
+-----------+-------------+--------------+----------------+
|101        |Yash         |D1            |IT              |
|102        |Rahul        |D2            |HR              |
|103        |Aman         |D1            |IT              |
+-----------+-------------+--------------+----------------+

EmployeeID → DepartmentID

DepartmentID → DepartmentName

Transitive Dependency exists
```

**After 3NF**
```
Employees

+-----------+-------------+--------------+
|EmployeeID |EmployeeName |DepartmentID  |
+-----------+-------------+--------------+
|101        |Yash         |D1            |
|102        |Rahul        |D2            |
|103        |Aman         |D1            |
+-----------+-------------+--------------+


Departments

+--------------+----------------+
|DepartmentID  |DepartmentName  |
+--------------+----------------+
|D1            |IT              |
|D2            |HR              |
+--------------+----------------+
```


<br>


---

<br>

# Summary

| Normal Form | Rule |
|-------------|------|
| **1NF** | Atomic values, no repeating groups, Primary Key |
| **2NF** | Remove redundant data into separate tables and use Foreign Keys |
| **3NF** | Remove transitive dependencies and ensure every non-key attribute depends only on the Primary Key |

<br>

# Advantages of Normalization

- Reduces data redundancy
- Eliminates update anomalies
- Improves data consistency
- Saves storage space
- Makes maintenance easier
- Improves INSERT, UPDATE, and DELETE performance
- Produces a clean and scalable database design
