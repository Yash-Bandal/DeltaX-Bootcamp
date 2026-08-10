# SQL Server – Indexes 

## What is an Index?

An **Index** is a database object that helps SQL Server locate rows quickly without scanning the entire table.

📖 **Analogy:** Like an index in a book—you directly jump to the required page instead of reading every page.

Without Index

```text
Search Row
     ↓
Full Table Scan ❌
```

With Index

```text
Search Row
     ↓
Index
     ↓
Required Row ✅
```

<br>

# Why Use Indexes?

* Faster data retrieval (`SELECT`)
* Reduces Full Table Scans
* Improves `WHERE`, `JOIN`, `ORDER BY`, and `GROUP BY` performance

**Trade-off**

* Faster Reads ✅
* Slower INSERT, UPDATE, DELETE ❌ (indexes must also be updated)

# Types of Indexes

<br>
<div align = "center">
     <img width="500" alt="image" src="https://github.com/user-attachments/assets/a567b3dc-85f4-47c0-97e4-8f5eef9ab5c1" />
</div>
<br>


---

<br>

# Database Pages

 Data is stored in pages, user sees a **Table**, that is a list of `rows` and `cols`, but at backend they are `Pages`, like this below

 A page is a smallest unit of data storage in a Database (8kb). It stores anything (Data, Metadata, Indexes).
 

SQL Server stores data in **8 KB Pages**.

A page contains:

* Page Header
* Data Rows
* Offset Array

Multiple pages form a table.

<br>
<div align = "center">
     <p>Data is stored in pages, user sees a list of rows and cols, but at backend it is stored like this</p>
     <img width="500" alt="image" src="https://github.com/user-attachments/assets/4032bd05-4cc7-4fc1-a61f-76b3b8215d37" />
     <br>
     <p>This is how a page of 8kb looks combining multiple bytes</p>
    <img width="500" alt="image" src="https://github.com/user-attachments/assets/2ac17b28-f430-429d-b751-18eb4693c39f" />


</div>
<br>


<br>

# Heap

Think of heap storage as You have Index pages of book, but all are very randomly stored inside a box, such that it is hard to find a single row we want

A table **without a clustered index** is called a **Heap**.

Characteristics

* Data stored randomly
* Faster inserts
* Slower reads (often requires Full Table Scan)
  
<br>
<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/bac53c74-d75f-45dd-aec7-781dff48cd48" />
</div>
<br>

<br>

# B-Tree (Balanced Tree)

Indexes are organized as a **B-Tree**.

```text
Root Node
    │
Intermediate Nodes
    │
Leaf Nodes
```

This structure allows SQL Server to locate rows efficiently.

<br>


## 1. Clustered Index

Physically sorts the table data based on the indexed column.

<br>
<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/241d6b60-e5f6-4800-9dea-c5630ee68d01" />
     <img width="500" alt="image" src="https://github.com/user-attachments/assets/ce72601b-5987-4a39-b7c6-e75c3f08922e" />
     <img width="500" alt="image" src="https://github.com/user-attachments/assets/9b6e98f8-20a0-4adb-804d-1657e060ad0f" />
</div>
<br>


Leaf nodes contain the **actual data rows**.


```sql
CREATE CLUSTERED INDEX IX_Foundation_Movies_Id
ON Foundation.Movies (Id);
```

### Characteristics

* Only **1** per table
* Usually created on the Primary Key
* Best for range searches and sorting

Example

```sql
SELECT *
FROM Foundation.Movies
WHERE Id BETWEEN 2 AND 5;
```

<br>

## 2. Non-Clustered Index

A separate structure that stores:

* Indexed column(s)
* Pointer to the actual row

```sql
CREATE NONCLUSTERED INDEX IX_Foundation_Actors_Name
ON Foundation.Actors (Name);
```

### Characteristics

* Multiple allowed per table
* Faster lookups on searched columns
* Slightly slower writes because indexes are maintained

Example

```sql
SELECT *
FROM Foundation.Actors
WHERE Name = 'Yami Gautam';
```

<br>

# Composite Index

An index on multiple columns.

```sql
CREATE INDEX IX_Foundation_Movies_Producer_Year
ON Foundation.Movies (ProducerId, YearOfRelease);
```

Works best for

```sql
WHERE ProducerId = 1
```

or

```sql
WHERE ProducerId = 1
AND YearOfRelease = 2024
```

<br>

# Leftmost Prefix Rule

For

```sql
CREATE INDEX IX_Foundation_Movies_Producer_Year
ON Foundation.Movies (ProducerId, YearOfRelease);
```

✅ Uses Index

```sql
WHERE ProducerId = 1
```

```sql
WHERE ProducerId = 1
AND YearOfRelease = 2024
```

❌ Doesn't efficiently use the index

```sql
WHERE YearOfRelease = 2024
```

Because the **leftmost column (`ProducerId`) is skipped.**

<br>

# Create Index

Clustered

```sql
CREATE CLUSTERED INDEX IX_Foundation_Movies_Id
ON Foundation.Movies (Id);
```

Non-Clustered

```sql
CREATE NONCLUSTERED INDEX IX_Foundation_Actors_Name
ON Foundation.Actors (Name);
```

<br>

# Drop Index

```sql
DROP INDEX IX_Foundation_Actors_Name
ON Foundation.Actors;
```

<br>

# View Indexes

In SQL Server Management Studio (SSMS):

```text
Database
 └── Tables
      └── Table
           └── Indexes
```

<br>

# When to Create an Index

Good candidates:

* Frequently searched columns (`WHERE`)
* Foreign Keys
* JOIN columns
* ORDER BY columns
* GROUP BY columns

Examples

```sql
CREATE INDEX IX_Foundation_Movies_ProducerId
ON Foundation.Movies (ProducerId);

CREATE INDEX IX_Foundation_ActorMovies_ActorId
ON Foundation.Actor_Movies (ActorId);

CREATE INDEX IX_Foundation_ActorMovies_MovieId
ON Foundation.Actor_Movies (MovieId);
```

<br>

# Avoid Indexes On

* Small tables
* Columns with frequent updates
* Columns with very few unique values (e.g., `Sex`)

<br>

# Clustered vs Non-Clustered

| Feature             | Clustered   | Non-Clustered         |
| ------------------- | ----------- | --------------------- |
| Physical data order | Yes         | No                    |
| Leaf nodes          | Actual data | Pointer to data       |
| Allowed per table   | 1           | Multiple              |
| Read performance    | Faster      | Fast                  |
| Write performance   | Slower      | Better than clustered |

<br>

# Quick Revision

| Topic                | Key Point                                         |
| <br><br><br><br><br><br>-- | <br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br>- |
| Index                | Speeds up data retrieval                          |
| Heap                 | Table without a clustered index                   |
| Page Size            | 8 KB                                              |
| B-Tree               | Root → Intermediate → Leaf                        |
| Clustered Index      | Physically sorts table data                       |
| Non-Clustered Index  | Separate structure with row pointers              |
| Composite Index      | Index on multiple columns                         |
| Leftmost Prefix Rule | Query must start with the leftmost indexed column |
| `CREATE INDEX`       | Creates an index                                  |
| `DROP INDEX`         | Removes an index                                  |

### Interview Tips

* A **Primary Key** creates a **Clustered Index** by default (unless specified otherwise).
* Every index improves **reads** but adds overhead to **INSERT**, **UPDATE**, and **DELETE** operations.
* Use **Clustered Index** for columns used in range queries (e.g., `Id`, `CreatedAt`) and **Non-Clustered Index** for frequently searched or joined columns (e.g., `Name`, `ProducerId`).
