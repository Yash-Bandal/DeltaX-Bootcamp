# SQL Joins: Comprehensive Guide

**SQL Joins** are a fundamental feature of SQL that allow you to retrieve data from two or more related tables in a single query. Because well-designed databases split data into separate tables to maintain organization, joins are essential for combining that data into a single result set.

> [!Tip]
> ### Also See Advance Joins / Non Matching Joins
> 
> <img width="643" height="419" alt="image" src="https://github.com/user-attachments/assets/d69662ad-5829-4989-bd47-7c51ff921e2a" />


<br>
<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/3dc9ab4f-b29d-4ec7-bca7-92b1b21af6e5" />
  <br>
  <p>Why use Joins?</p>
  <img width="400" alt="image" src="https://github.com/user-attachments/assets/07e0d4e5-3d2c-44e0-b63f-e637d9941344" />


</div>
<br>

<br>

## 1. Key Concepts

- **Relationship:** Joins work by matching records between tables based on a related column, often a **unique identifier** (ID) or a **foreign key**.
- **The `ON` Keyword:** This specifies the conditions for the match (e.g., `person.company_id = company.id`).
- **Table Aliasing:** It is best practice to specify the table name for each column in the `SELECT` clause to avoid confusion when different tables have columns with the same name.

<br>

<div align = "center">
<br>
<p>Example table 1 Before Join</p>
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/235841c7-52ba-49d1-954f-5dd06c3c8d50" />
</div>
<br>

## 2. Types of Joins

### Inner Join
- **Definition:** Returns only the records that have matching values in **both** tables.
- **Behavior:** If a record in the first table has no match in the second (or vice versa), it is excluded from the results.
- **Syntax:** Use `INNER JOIN` or simply `JOIN` (the word "INNER" is optional but recommended for clarity).

<br>
<div align = "center">
  <img width="300"  alt="image" src="https://github.com/user-attachments/assets/760ca245-e279-4384-a272-d910f714c662" />
</div>
<br>

```sql
SELECT columns
FROM table1
INNER JOIN table2
ON table1.column = table2.column;
```
or
```sql
SELECT columns
FROM table1
JOIN table2
ON table1.column = table2.column;
```

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/4d698860-90c7-407e-a5b6-252be5eae4c5" />
  <p>Syntax</p>
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/da04ab86-d965-46ce-86dc-cd1cfa814a91" />
<br>
  <p>Result</p>
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/14a48a37-faad-4b9f-ab50-89b07094fac2" />


</div>
<br>





### Left Outer Join (Left Join)
- **Definition:** Returns **all records** from the "left" table (the one specified first) and the matched records from the "right" table.
- **Behavior:** If there is no match in the right table, the result will show `NULL` values for those columns.

<br>
<div align = "center">
  <img width="300"  alt="image" src="https://github.com/user-attachments/assets/c72a89e4-6e7b-4ae1-a133-da495f0eaad0" />
</div>
<br>

```sql
SELECT columns
FROM table1
LEFT JOIN table2
ON table1.column = table2.column;
```
or

```sql
SELECT columns
FROM table1
LEFT OUTER JOIN table2
ON table1.column = table2.column;
```

<br>

<div align = "center">
<br>
<p>Example table 1 Before Join</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/8195284c-1d7d-4194-99e7-04a08c84dead" />
  <p>Syntax</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/3207fcf4-e333-4ade-8604-4ee0d867fc18" />
<p>Result</p>
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/76692b24-f715-45a1-97a7-b069e90d7672" />
</div>
<br>


### Right Outer Join (Right Join)
- **Definition:** Returns **all records** from the "right" table (the one specified second) and the matched records from the "left" table.
- **Behavior:** If no match is found in the left table, `NULL` values are shown for those fields.
- **Note:** Many developers prefer using Left Joins exclusively, as any Right Join can be rewritten as a Left Join by swapping the table order.


<br>
<div align = "center">
<img  width="300" alt="image" src="https://github.com/user-attachments/assets/16450629-1729-414a-b104-8e4ec2ab8101" />
</div>
<br> 


```sql
SELECT columns
FROM table1
RIGHT JOIN table2
ON table1.column = table2.column;
```
or
```sql
SELECT columns
FROM table1
RIGHT OUTER JOIN table2
ON table1.column = table2.column;
```
<br>

<div align = "center">
<p>Example table 1 Before Join</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/f99ca5ef-aec9-4bd5-8751-9d840ba24f4f" />
  <p>Syntax</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/f2c43872-534c-403d-86ad-8fdfd2d2b251" />
<p>Output</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/446e5bea-b755-4333-8960-815b8a3b1538" />

</div>

<br>

> [!Tip]
> You can use only one join forever, (many use just left), you just have to change table positions, `Left`->`Right` and `Right`->`Left`

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/4f4412ab-0d2e-4259-87f7-17fbf49f6764" />
</div>
<br>

### Full Outer Join (Full Join) 


- **Definition:** A combination of both Left and Right Outer Joins.
- **Behavior:** It returns all records when there is a match in either the left or the right table. Rows without a match in either table will contain `NULL` values for the missing side.

> [Tip]
> Less used


<br>
<div align = "center">
  <img width="300"  alt="image" src="https://github.com/user-attachments/assets/760d60ba-361e-4b6e-a3c4-216a17ac5233" />

</div>
<br> 

<br>

```sql
SELECT columns
FROM table1
FULL JOIN table2
ON table1.column = table2.column;
```
or
```sql
SELECT columns
FROM table1
FULL OUTER JOIN table2
ON table1.column = table2.column;
```

<br>

<div align = "center">
<br>
<p>Example table 1 Before Join</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/bcf61d72-a9f2-4e14-b2a7-2d743de58b9a" />
  <p>Syntax</p>
<img width="600"  alt="image" src="https://github.com/user-attachments/assets/074969ea-f9fd-431f-b9e0-ae213771e0a4" />
<p>Output</p>
<img width="600"  alt="image" src="https://github.com/user-attachments/assets/ebfe9fe1-551b-495c-afd7-64227952e0af" />
</div>
<br>

## 3. Pro Tips & Best Practices

- **Performance:** Joins are not inherently "bad" for performance. In fact, they are a sign of a well-designed, normalized database.
- **Avoid `USING` and `NATURAL JOIN`:**
  - The `USING` keyword can break your query if field names change.
  - `NATURAL JOIN` is risky because it automatically joins on all columns with matching names, which may lead to unexpected results if the schema is updated.
- **Avoid the "Where Clause Join":** While you can join tables by listing them in the `FROM` clause and matching them in the `WHERE` clause, this is discouraged because:
  - It is not the ANSI standard way of writing joins.
  - It generally limits you to Inner Joins.
  - It becomes difficult to read and error-prone as the number of tables (e.g., 4, 5, or 10) increases.

<br>
<div align = "center">
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/fe350535-d3c3-4ef0-9110-cc9837a71c12" />
</div>
<br>

## 4. Summary Table of Join Types


<img width="523" height="401" alt="image" src="https://github.com/user-attachments/assets/44ac813b-7b00-43d9-89d4-e1e0d8399c47" />

| Join Type | Result Set Includes... |
| :-------- | :--------------------- |
| **Inner Join** | Only records with matches in both tables. |
| **Left Join** | Everything from the first table + matches from the second. |
| **Right Join** | Everything from the second table + matches from the first. |
| **Full Join** | Everything from both tables, regardless of matches. |
