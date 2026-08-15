# SQL Tutorial: How to Join 3 Tables

This guide explains how to join **three tables** in a single SQL query to retrieve related data. The same approach can be extended to **4, 5, or even more tables**.

<br>

## Example Database

Suppose we have the following tables:

### `book`

| book_id | title | publisher_id | language_id |
| :------ | :---- | :----------- | :---------- |
| 1 | SQL Basics | 101 | 1 |
| 2 | Database Design | 102 | 2 |

### `publisher`

| publisher_id | publisher_name |
| :----------- | :------------- |
| 101 | O'Reilly |
| 102 | Pearson |

### `book_language`

| language_id | language_name |
| :---------- | :------------ |
| 1 | English |
| 2 | Spanish |

<br>

## Step-by-Step Process

### 1. Select Columns from the First Table

Start by selecting the columns you need from the primary table.

```sql
SELECT
    book.book_id,
    book.title
FROM book;
```

<br>

### 2. Join to the Second Table

Use a join (`INNER JOIN`, `LEFT JOIN`, or `RIGHT JOIN`) after the `FROM` clause.

```sql
SELECT
    book.book_id,
    book.title,
    publisher.publisher_name
FROM book
INNER JOIN publisher;
```

<br>

### 3. Specify the Join Condition

Use the `ON` clause to define how the two tables are related.

Typically, this matches a **foreign key** with a **primary key**.

```sql
SELECT
    book.book_id,
    book.title,
    publisher.publisher_name
FROM book
INNER JOIN publisher
ON book.publisher_id = publisher.publisher_id;
```

<br>

### 4. Join the Third Table

Add another `JOIN` statement.

The third table can be joined with **either of the previously joined tables**, depending on the relationship.

```sql
SELECT
    book.book_id,
    book.title,
    publisher.publisher_name,
    book_language.language_name
FROM book
INNER JOIN publisher
    ON book.publisher_id = publisher.publisher_id
INNER JOIN book_language
    ON book.language_id = book_language.language_id;
```

<br>

### 5. Final Query

```sql
SELECT
    book.book_id,
    book.title,
    publisher.publisher_name,
    book_language.language_name
FROM book
INNER JOIN publisher
    ON book.publisher_id = publisher.publisher_id
INNER JOIN book_language
    ON book.language_id = book_language.language_id;
```

### Output

| book_id | title | publisher_name | language_name |
| :------ | :---- | :------------- | :------------ |
| 1 | SQL Basics | O'Reilly | English |
| 2 | Database Design | Pearson | Spanish |

<br>

## Understanding the Join Flow

```
book
 │
 ├── publisher_id ─────────────► publisher.publisher_id
 │
 └── language_id ──────────────► book_language.language_id
```

Each `JOIN` adds another related table to the result.

- First join:
  - `book` ↔ `publisher`

- Second join:
  - `book` ↔ `book_language`

The database combines all matching rows into a single result.

<br>

## Key Takeaways

- **Extensibility:** The same approach works for joining **4, 5, or more tables**.
- **Flexibility:** You can mix join types (`INNER JOIN`, `LEFT JOIN`, `RIGHT JOIN`, etc.) depending on the data you want.
- **Best Practice:** Always identify the **Primary Key (PK)** and **Foreign Key (FK)** relationships before writing joins.
- **Readability:** Place each `JOIN` on a new line and indent the `ON` condition to make complex queries easier to understand.
- **Table Aliases:** For large queries, use aliases (`b`, `p`, `bl`) to make the query shorter and cleaner.

### Same Query Using Aliases

```sql
SELECT
    b.book_id,
    b.title,
    p.publisher_name,
    bl.language_name
FROM book AS b
INNER JOIN publisher AS p
    ON b.publisher_id = p.publisher_id
INNER JOIN book_language AS bl
    ON b.language_id = bl.language_id;
```
