# Database Design: Product Attributes

Designing a database for **product attributes** is a common challenge, especially in eCommerce where different products have different characteristics.

> [!Tip]
> ### Base, One to many, Many to Many
> 
> ### Think,
> 1. One product has Many Attributes/Entries
> 2. One Attributes may have many values
> 3. Thus Entity -> Many Attributes -> 1 Attribute Many values

For example:

- A **Laptop** may have:
  - RAM
  - Processor
  - Storage
  - Screen Size

- A **T-Shirt** may have:
  - Color
  - Size
  - Material

- A **Phone** may have:
  - Storage
  - Battery Capacity
  - Camera

Since every product type has different attributes, creating a column for every possible attribute is not practical.

<br>

<div align = "center">
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/9e03ccb7-e242-4ba1-a793-e208c60e4a4f" />
  <p>Can add more</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/2d87ad35-ef63-4aca-b8d2-69d04e3eed97" />
</div>

<br>

# The Design Challenge

Suppose we create a simple `product` table.

| product_id | name | color | size | material | RAM | Storage | Battery |
| :--------- | :--- | :---- | :--- | :------- | :-- | :------ | :------ |
| 1 | T-Shirt | Blue | L | Cotton | NULL | NULL | NULL |
| 2 | Laptop | NULL | NULL | NULL | 16 GB | 512 GB | NULL |
| 3 | Phone | Black | NULL | NULL | NULL | 128 GB | 5000 mAh |

### Problems

- Many unused (`NULL`) columns
- Difficult to maintain
- New attributes require schema changes

Example:

```sql
ALTER TABLE product
ADD COLUMN processor VARCHAR(100);
```

Every time a new product type introduces a new attribute, the table must be modified.

<br>

# Better Solution: EAV (Entity-Attribute-Value)

Instead of storing attributes as columns, store them as **rows**.

The EAV model consists of three parts:

- **Entity** → Product
- **Attribute** → Color, Size, RAM, Storage
- **Value** → Blue, Large, 16 GB, 512 GB

<br>

## Step 1: Product Table (Entity)

### `product`

| product_id | product_name |
| :--------- | :----------- |
| 1 | T-Shirt |
| 2 | Laptop |
| 3 | Phone |

<br>

## Step 2: Attribute Table

Store all possible attributes.

### `attribute`

| attribute_id | attribute_name |
| :----------- | :------------- |
| 1 | Color |
| 2 | Size |
| 3 | Material |
| 4 | RAM |
| 5 | Storage |
| 6 | Battery |

<br>

## Step 3: Product Attribute Values

Store attribute values for each product.

### `product_attribute_value`

| product_id | attribute_id | value |
| :--------- | :----------- | :---- |
| 1 | 1 | Blue |
| 1 | 2 | L |
| 1 | 3 | Cotton |
| 2 | 4 | 16 GB |
| 2 | 5 | 512 GB |
| 3 | 1 | Black |
| 3 | 5 | 128 GB |
| 3 | 6 | 5000 mAh |

Notice that we only store attributes that actually exist.

<br>

# Table Relationships

```
product
--------
product_id (PK)
product_name

        │
        │
        ▼

product_attribute_value
-----------------------
product_id (FK)
attribute_id (FK)
value

        ▲
        │
        │

attribute
---------
attribute_id (PK)
attribute_name
```

<br>

# Example Query

Retrieve all attributes of the Laptop.

```sql
SELECT
    p.product_name,
    a.attribute_name,
    pav.value
FROM product AS p
INNER JOIN product_attribute_value AS pav
    ON p.product_id = pav.product_id
INNER JOIN attribute AS a
    ON pav.attribute_id = a.attribute_id
WHERE p.product_name = 'Laptop';
```

### Output

| Product | Attribute | Value |
| :------ | :-------- | :---- |
| Laptop | RAM | 16 GB |
| Laptop | Storage | 512 GB |

<br>

# Example: Find All Blue Products

```sql
SELECT
    p.product_name
FROM product AS p
INNER JOIN product_attribute_value AS pav
    ON p.product_id = pav.product_id
INNER JOIN attribute AS a
    ON pav.attribute_id = a.attribute_id
WHERE
    a.attribute_name = 'Color'
    AND pav.value = 'Blue';
```

### Output

| Product |
| :------ |
| T-Shirt |

<br>

# Benefits of EAV

- **Flexibility**
  - Add new attributes without changing the database schema.

- **Scalability**
  - Supports different product categories in the same database.

- **Space Efficiency**
  - Stores only existing attribute values.
  - Avoids large numbers of `NULL` columns.

- **Easy Expansion**
  - Adding a new attribute only requires inserting a row.

Example:

```sql
INSERT INTO attribute (attribute_name)
VALUES ('Processor');
```

No table modification is required.

<br>

# Disadvantages of EAV

- Queries require multiple joins.
- More difficult to understand than a simple table.
- Searching and filtering can be slower.
- Proper indexing is important for good performance.

<br>

# Comparison

## Traditional Design

| product_id | name | color | RAM | Storage |
| :--------- | :--- | :---- | :-- | :------ |
| 1 | T-Shirt | Blue | NULL | NULL |
| 2 | Laptop | NULL | 16 GB | 512 GB |

### Problems

- Many `NULL` values
- Schema changes required
- Not suitable for diverse products

<br>

## EAV Design

| product_id | attribute | value |
| :--------- | :-------- | :---- |
| 1 | Color | Blue |
| 2 | RAM | 16 GB |
| 2 | Storage | 512 GB |

### Advantages

- No `NULL` values
- Unlimited attributes
- No schema changes

<br>

# Key Design Principles

- Keep **Products**, **Attributes**, and **Attribute Values** in separate tables.
- Connect tables using **Primary Keys (PK)** and **Foreign Keys (FK)**.
- Store only the attributes that apply to a product.
- Use indexes on `product_id` and `attribute_id` to improve query performance.
- EAV is ideal when products have **many different or changing attributes**, but it introduces more complex queries compared to a traditional table design.

<br>

