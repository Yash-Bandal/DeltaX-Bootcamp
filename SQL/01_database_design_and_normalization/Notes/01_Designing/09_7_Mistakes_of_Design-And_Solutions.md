# Common Database Design Mistakes

These are some of the most common mistakes made when designing relational databases and the recommended solutions.

<br>

### Overview
| Mistake | Better Design |
|----------|---------------|
| Business key as PK | Use surrogate key |
| Store calculated data | Calculate when needed |
| Spaces in table names | Use `snake_case` |
| No foreign keys | Enforce referential integrity |
| Multiple values in one column | Normalize into separate columns |
| Multiple similar columns | Create child tables |
| Wrong data types | Use appropriate native types |

<br>

<div align = "center">
<img width="200" alt="image" src="https://github.com/user-attachments/assets/ae6ea89f-c7b0-4632-82ab-7562ce2666be" />
</div>

<br>

# 1. Using Business Data as the Primary Key

Business identifiers may seem unique, but they can change over time.

## ❌ Bad Design

Using a Social Security Number (SSN) as the primary key.

### Employee

| ssn (PK) | name |
|----------|------|
| 123-45-6789 | Alice |
| 234-56-7890 | Bob |

Suppose Alice's SSN is corrected.

```
123-45-6789
↓

123-45-6790
```

Now every table referencing this employee must also be updated.

## Why is this bad?

- Business values can change.
- External systems control these values.
- Updating primary keys is expensive.

<br>

## ✅ Better Design

Use a surrogate key.

### Employee

| employee_id (PK) | ssn | name |
|------------------|-----|------|
| 1 | 123-45-6789 | Alice |
| 2 | 234-56-7890 | Bob |

Other tables reference `employee_id`, while SSN becomes just another attribute.

<br>

# 2. Storing Redundant Data, 

> [!Important]
> Do not store values that can be calculated.

## ❌ Bad Design

| employee_id | dob | age |
|--------------|------------|-----|
| 1 | 1998-06-15 | 27 |

Next year:

```
Age should become 28.
```

Someone must update every row.

## Why is this bad?

- Data becomes inconsistent.
- Requires manual updates.
- Same information stored twice.

<br>

## ✅ Better Design

Store only the date of birth.

| employee_id | dob |
|--------------|------------|
| 1 | 1998-06-15 |

Calculate age when needed.

```sql
SELECT
    TIMESTAMPDIFF(YEAR, dob, CURDATE()) AS age
FROM employee;
```

<br>

# 3. Poor Table Names

Avoid spaces and special characters.

## ❌ Bad Design

```
Customer Order
```

Every query becomes:

```sql
SELECT *
FROM "Customer Order";
```

or

```sql
SELECT *
FROM [Customer Order];
```

depending on the database.

<br>

## ✅ Better Design

Use snake_case.

```
customer_order
```

Now queries become

```sql
SELECT *
FROM customer_order;
```

Easy to type.

Easy to read.

<br>

# 4. Ignoring Referential Integrity

Relationships should be enforced by the database.

Referential integrity is a database rule that ensures relationships between tables remain valid.

Simply put:
> A foreign key value must always refer to an existing row in the parent table (or be NULL if allowed). It prevents invalid or orphaned records.

## ❌ Bad Design

### Customer

| customer_id |
|--------------|
| 1 |
| 2 |

### Orders

| order_id | customer_id |
|-----------|-------------|
| 101 | 50 |

Customer 50 doesn't exist.

The database allowed invalid data.

<br>

## Why is this bad?

- Orphan records
- Invalid relationships
- Broken reports

<br>

## ✅ Better Design

```sql
CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    customer_id INT,

    FOREIGN KEY (customer_id)
        REFERENCES customer(customer_id)
);
```

Now inserting

```text
customer_id = 50
```

fails unless Customer 50 exists.

<br>

# 5. Storing Multiple Values in One Column

Each column should contain one piece of information.

## ❌ Bad Design

| customer_id | address |
|--------------|------------------------------------------------|
| 1 | 15 Park Street, New York, NY, 10001, USA |

Need all customers in New York?

Very difficult.

Need only ZIP codes?

Even harder.

<br>

## ✅ Better Design

| customer_id | street | city | state | zip_code | country |
|--------------|--------|------|--------|-----------|----------|
| 1 | 15 Park Street | New York | NY | 10001 | USA |

Now searching becomes easy.

```sql
SELECT *
FROM customer
WHERE city = 'New York';
```

<br>

# 6. Creating Multiple Columns for the Same Kind of Data

Sometimes an entity can have multiple values of the same type.

## ❌ Bad Design

### Customer

| customer_id | home_phone | mobile_phone | work_phone |
|--------------|------------|--------------|-------------|
| 1 | 111111 | 999999 | NULL |

Later the customer wants another phone.

Where do you store it?

Need another column:

```
fax_phone
office_phone
emergency_phone
```

The table keeps growing.

<br>

## ✅ Better Design

### Customer

| customer_id | name |
|--------------|------|
| 1 | Alice |

### Phone Type

| phone_type_id | type |
|---------------|------|
| 1 | Home |
| 2 | Mobile |
| 3 | Work |

### Phone Number

| phone_id | customer_id | phone_type_id | number |
|-----------|--------------|---------------|---------|
| 1 | 1 | 1 | 111111 |
| 2 | 1 | 2 | 999999 |
| 3 | 1 | 3 | 555555 |

Adding another phone is simply another row.

No schema changes required.

<br>

# 7. Choosing Incorrect Data Types

Use the correct data type for each column.

## ❌ Bad Design

```sql
birth_date VARCHAR(100)
salary VARCHAR(50)
```

Problems:

- Dates are stored as text.
- Numbers cannot be sorted properly.
- More storage than necessary.

<br>

## ✅ Better Design

```sql
birth_date DATE
salary DECIMAL(10,2)
created_at DATETIME
is_active BOOLEAN
```

Choose the smallest appropriate type.

Examples:

| Data | Good Type |
|-------|-----------|
| Name | VARCHAR(100) |
| Age | TINYINT |
| Salary | DECIMAL(10,2) |
| Birth Date | DATE |
| Login Time | DATETIME |
| Price | DECIMAL(8,2) |
| Active Status | BOOLEAN |

