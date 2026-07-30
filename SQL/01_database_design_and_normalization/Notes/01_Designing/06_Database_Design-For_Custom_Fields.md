# Database Design for Custom Fields

Many applications allow users to create **custom fields**, such as adding extra information to customers, products, or employees.

**Examples:**
- CRM → Customer's Birthday, LinkedIn Profile
- E-commerce → Product Warranty, Material
- HR System → Employee Blood Group, Passport Number

Since these fields are not known in advance, the database must be designed to store them efficiently.

There is **no single best solution**. The right approach depends on your application's requirements, flexibility, performance, and scalability.

<br>

## Example Scenario

Suppose we have a `customer` table.

### Customer Table

| customer_id | name |
| :---------- | :--- |
| 1 | Alice |
| 2 | Bob |

Users want to add custom fields like:

- Birthday
- Favorite Color
- Membership Level
- Twitter Handle

Let's see different ways to store them.

<br>

# 1. EAV (Entity-Attribute-Value)

The **EAV model** stores data vertically instead of horizontally.

Instead of creating new columns, every custom field becomes a new row.

<br>
<div align = "center">
    <img width="600" alt="image" src="https://github.com/user-attachments/assets/b0f43d55-debe-4cfb-b939-6c6d972d96bf" />
</div>
<br>

### Tables

#### `customer`

| customer_id | name |
| :---------- | :--- |
| 1 | Alice |

#### `customer_attribute`

| customer_id | attribute | value |
| :---------- | :-------- | :---- |
| 1 | Birthday | 12-Jan-2000 |
| 1 | Favorite Color | Blue |
| 1 | Membership | Gold |

<br>

### Query

```sql
SELECT *
FROM customer_attribute
WHERE customer_id = 1;
```

### Advantages

- Less space taken for `sparse` columns
- Extremely flexible
- Unlimited custom fields
- No schema changes

### Disadvantages

- Difficult to enforce data validation, and data types
- No effective column indexing
- Complex queries
- Poor performance for large datasets


<br>

# 2. Modified EAV

A variation of EAV where attributes are stored separately.

<br>
<div align = "center">
    <img width="600" alt="image" src="https://github.com/user-attachments/assets/c83a40fe-95fc-4e40-867e-6c6a9d81feb0" />
</div>
<br>

### Tables

#### `attribute`

| attribute_id | attribute_name |
| :----------- | :------------- |
| 1 | Birthday |
| 2 | Favorite Color |
| 3 | Membership |

#### `customer_attribute`

| customer_id | attribute_id | value |
| :---------- | :----------- | :---- |
| 1 | 1 | 12-Jan-2000 |
| 1 | 2 | Blue |
| 1 | 3 | Gold |

This reduces duplicate attribute names and improves consistency.

### Advantages
- Additional data validation compared to EAV
- Less need for additional functions to transform data 
- Highly flexible than EAV
- Better normalization
- Saves storage
- Easier to manage attributes

### Disadvantages

- Still requires complex joins
- Queries become lengthy

<br>

# 3. Single Table

Store every possible custom field as a column.

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/379dfaa7-a228-458c-9487-5aa3befdd4c4" />
</div>
<br>

### Customer Table

| customer_id | name | birthday | favorite_color | membership |
| :---------- | :--- | :------- | :------------- | :--------- |
| 1 | Alice | 12-Jan-2000 | Blue | Gold |

### Advantages

- Very fast queries
- Simple SQL
- Easy indexing
- Data validation possible     
- Good Performance

### Disadvantages
- Hard to work with so many tables
- Many unused (`NULL`) columns
- Requires schema changes whenever a new field is added
- Not scalable for frequently changing custom fields

<br>

# 4. Class Table Inheritance

Store common fields in one table and specialized fields in separate tables.

### customer

| customer_id | name |
| :---------- | :--- |
| 1 | Alice |

### premium_customer

| customer_id | membership | reward_points |
| :---------- | :--------- | :------------ |
| 1 | Gold | 1200 |

### Advantages

- Well normalized
- Easy to maintain
- Avoids many NULL values

### Disadvantages

- More joins required
- More tables to manage

<br>

# 5. Concrete/Class Table Inheritance

Create separate tables for each customer type.

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/130953a8-f36e-44a2-9e28-2e84375af6af" />
</div>
<br>

### regular_customer

| customer_id | name |
| :---------- | :--- |
| 2 | Bob |

### premium_customer

| customer_id | name | membership | reward_points |
| :---------- | :--- | :--------- | :------------ |
| 1 | Alice | Gold | 1200 |

### Advantages

- No joins needed
- Fast access

### Disadvantages

- Data duplication
- Difficult to maintain common fields

<br>

# 6. Normalized Tables

Create proper relational tables for every custom feature.

Example:

Instead of storing "Address" as text, create an `address` table.

### customer

| customer_id | name |
| :---------- | :--- |
| 1 | Alice |

### address

| address_id | customer_id | city | country |
| :--------- | :---------- | :--- | :------ |
| 1 | 1 | Mumbai | India |

### Advantages

- Highly normalized
- Best data integrity
- Easy validation

### Disadvantages

- More tables
- More joins

<br>

# 7. JSON

Store all custom fields inside a JSON column.

### Customer Table

| customer_id | name | custom_fields |
| :---------- | :--- | :------------ |
| 1 | Alice | `{"Birthday":"12-Jan-2000","Favorite Color":"Blue","Membership":"Gold"}` |

### Insert Example

```sql
INSERT INTO customer
VALUES (
    1,
    'Alice',
    '{"Birthday":"12-Jan-2000","Favorite Color":"Blue","Membership":"Gold"}'
);
```

### Advantages

- Very flexible
- No schema changes
- Easy to add new fields
- Supported by modern databases like PostgreSQL and MySQL

### Disadvantages

- Harder to validate
- Some queries become slower
- Indexing JSON fields can be more complex

<br>

# 8. Dynamic Schema

The application changes the database structure at runtime.

Example:

```sql
ALTER TABLE customer
ADD COLUMN linkedin_profile VARCHAR(100);
```

Whenever users create a new custom field, the application automatically modifies the table.

### Advantages

- Behaves like normal columns
- Fast queries

### Disadvantages

- Frequent schema changes
- Difficult to maintain
- Risky for large production databases

<br>

# Comparison

| Design | Flexibility | Performance | Complexity |
| :------ | :---------: | :---------: | :--------: |
| EAV | ⭐⭐⭐⭐⭐ | ⭐⭐ | ⭐⭐⭐⭐⭐ |
| Modified EAV | ⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐ |
| Single Table | ⭐ | ⭐⭐⭐⭐⭐ | ⭐ |
| Class Table Inheritance | ⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐ |
| Concrete Table Inheritance | ⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐ |
| Normalized Tables | ⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ |
| JSON | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐ |
| Dynamic Schema | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |

<br>

# Which Approach Should You Use?

- **Single Table** → Fixed set of fields
- **EAV** → Unlimited custom fields with highly dynamic data
- **Modified EAV** → Better version of EAV
- **Normalized Tables** → Strong relational design
- **JSON** → Modern applications with flexible attributes
- **Class Table Inheritance** → Different entity types sharing common fields
- **Concrete Table Inheritance** → Separate entity types with minimal joins
- **Dynamic Schema** → Rarely used; suitable only for applications that can safely modify the database schema
