# Database Design: eCommerce Website

> **Note**
>
> This is an educational database design demonstrating common relational database concepts.
> Real-world eCommerce systems (Amazon, Flipkart, etc.) are significantly more complex.

<br>

### Think like a customer using the application

This is how architects think.

Imagine opening Amazon.

You ask

What happens?

```
User logs in.

↓

User browses categories.

↓

Category shows products.

↓

User adds product.

↓

Product goes into cart.

↓

Checkout.

↓

Choose address.

↓

Choose payment.

↓

Create order.

↓

Order contains items.

↓

Leave review.
```

That story itself almost designs the database.

Each noun becomes an entity.

Each arrow becomes a relationship.


<br>


<div align = "center">
        <img width="745" height="479" alt="image" src="https://github.com/user-attachments/assets/91f8e07e-251d-422b-9cd2-799a210907e5" />
</div>

<br>

# Step 1: Understand the Requirements

Before designing the database, identify what the application should support.

The instructor defined these requirements:

- User accounts
- Contact details
- Multiple addresses
- Multiple payment methods
- Products & categories
- Product attributes (Size, Color, etc.)
- Stock management
- Shopping cart
- Shipping methods
- Orders
- User reviews
- Promotions & discounts

These requirements help identify the required tables.

<br>

<div align = "center">
<img width="700" alt="image" src="https://github.com/user-attachments/assets/62a67b67-e680-4ed8-a840-46f7383859b6" />
</div>

<br>

# Step 2: Design the User Table

Everything starts with a user account.

Users should be able to:

- Register
- Login
- Place orders
- Save addresses
- Save payment methods

## Site User

Typical fields:

```
user_id
email
phone
password
```


<br>


<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/9e95d154-1c62-4582-8abd-976790dc0dad" />
</div>

<br>


### Important

- `user_id` is the Primary Key.
- Email is used for login.
- Passwords must **never** be stored in plain text.
- Store encrypted/hashed passwords.

<br>

# Step 3: Address Management

A user may have:

- Home Address
- Office Address
- Shipping Address

So, don't store the address inside the User table.

Instead create a separate Address table.

```
User

↓

User Address

↓

Address

↓

Country
```

### Address

Contains:

- Unit Number
- Street Number
- Address Line 1
- Address Line 2
- City
- Region
- Postal Code
- Country

### Country

Instead of storing country names repeatedly,

create a lookup table.

```
Country (1)

↓

Many Addresses
```

### User Address

Ask yourself:

Can one user have many addresses?

✔ Yes

Can one address belong to multiple users?

Potentially yes (family members living together).

Therefore,

```
Many Users

        ↔

Many Addresses
```

Create a junction table.

Store:

- user_id
- address_id
- is_default

The `is_default` field allows users to select their default address.



<br>

<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/b0c2b6e4-de23-4acb-9eec-8bf6dcf5de32" />
</div>


<br>

# Step 4: Payment Methods

Users should be able to save multiple payment methods.

Examples:

- Credit Card
- Debit Card
- PayPal

## User Payment Method

Stores:

- Payment Type
- Provider (Visa, Mastercard)
- Account Number
- Expiry Date
- is_default

### Design Tip

In production systems, it's often better to use a third-party payment provider instead of storing sensitive card information.

<br>

<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/2696c366-9118-4c62-be4c-f45247a932e0" />
</div>

<br>



<br>

# Step 5: Product Categories




Products belong to categories.

Example:

```
Clothing

↓

Men

↓

T-Shirts
```

A category can contain subcategories.

Instead of creating separate tables,

use a **self-reference**.

```
Category

↓

Parent Category
```

Store:

```
parent_category_id
```

Benefits:

- Unlimited hierarchy
- No separate Category/Subcategory tables

<br>

<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/bed7931b-54b9-4b40-9401-27338f0bce68" />
</div>

<br>



# Step 6: Product

A Product represents what users see in product listings.

Example:

```
Slim T-Shirt
```

Even though it comes in many sizes and colors,

it appears only once.

Typical fields:

- Product Name
- Description
- Product Image
- Category

<br>

# Step 7: Product Variations

Products often have attributes.

Example:

```
Size

Color

Storage

Material
```

Instead of creating columns for every attribute,

store them separately.

## Variation

Represents the attribute type.

Examples:

- Size
- Color
- Storage

The instructor links Variations to Categories.

Example:

```
Clothing

↓

Size

Color

Material
```

Phones may have different variations:

```
Storage

Color

Screen Size
```

<br>

## Variation Option

Stores the possible values.

Example:

```
Size

↓

XS

S

M

L

XL
```

```
Color

↓

Black

White

Blue
```

<br>

<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/2665691e-f05e-4e63-b658-56ffff118e4a" />
</div>

<br>

### Design Note

This is similar to an **Entity Attribute Value (EAV)** model.

Advantages:

- Flexible
- Easy to add new attributes

Disadvantage:

- We lose some data validation.
- Large systems may prefer dedicated tables for certain attributes.

<br>

# Step 8: Product Item (SKU)

A Product is only the definition.

A Product Item is the actual item that can be purchased.

Example:

```
Slim T-Shirt

↓

Black

↓

Size M
```

Store:

- SKU
- Stock Quantity
- Price
- Product Image

### Why separate Product and Product Item?

One Product

↓

Many Product Items

Each Product Item has a different variation combination.

The price may also vary between variations.

<br>

# Step 9: Product Configuration

How do we know a Product Item is:

```
Black

+

Size M
```

A Product Item can have many Variation Options.

A Variation Option belongs to many Product Items.

```
Many Product Items

        ↔

Many Variation Options
```

Create a junction table:

```
Product Configuration
```

This table stores the combination of:

- Product Item
- Variation Option

It is also used when customers choose product attributes on the product page.

<br>

# Step 10: Shopping Cart

Only logged-in users have saved shopping carts.

```
User

↓

Shopping Cart

↓

Shopping Cart Item
```

Shopping Cart Item stores:

- Product Item
- Quantity


<br>

<div align = "center">
        <img width="515" height="248" alt="image" src="https://github.com/user-attachments/assets/a7317636-70b2-48a5-a5a7-991e78da60f6" />
</div>

<br>


<br>

# Step 11: Orders

When checkout is completed,

the Shopping Cart becomes a Shop Order.

The instructor uses:

```
shop_order
```

instead of

```
order
```

because **ORDER** is an SQL keyword.

Store:

- User
- Payment Method
- Shipping Address
- Shipping Method
- Order Date
- Order Total
- Order Status

Shipping Methods are stored separately.

Examples:

- Standard
- Express
- Priority

Order Status examples:

- Ordered
- Processing
- Delivered


<br>

<div align = "center">
        <img width="245" height="158" alt="image" src="https://github.com/user-attachments/assets/a8da90ec-8a2f-47e6-ada0-02b0c8e80ee3" />
</div>

<br>

### Design Tip

The instructor notes another possible design:

Instead of separate Shopping Cart and Shop Order tables,

one table could represent both.

<br>

# Step 12: Order Line

One Order contains many products.

```
One Order

↓

Many Order Lines
```

Store:

- Product Item
- Quantity
- Purchase Price

### Why store Price?

Product prices change.

The Order Line stores a **snapshot** of the price paid.

Without this,

old orders would display today's prices.

<br>

# Step 13: User Reviews

Users should only review products they purchased.

Instead of linking Reviews directly to Products,

link them to the Order Line.

```
User

↓

Order Line

↓

Review
```

Store:

- Rating (1–5)
- Comment

This ensures only verified purchases can leave reviews

<br>

<div align = "center">
        <img width="245" height="158" alt="image" src="https://github.com/user-attachments/assets/a8da90ec-8a2f-47e6-ada0-02b0c8e80ee3" />
</div>


<br>

# Step 14: Promotions

Promotions apply to categories.

Ask yourself:

Can one Promotion apply to many Categories?

✔ Yes

Can one Category have many Promotions?

✔ Yes

Therefore,

```
Many Promotions

        ↔

Many Categories
```

Create a junction table:

```
Promotion Category
```

Promotion stores:

- Name
- Description
- Discount Rate
- Start Date
- End Date

<br>

<div align = "center">
<img width="497" height="370" alt="image" src="https://github.com/user-attachments/assets/a4c4991f-85d6-4537-bd00-85cc7aa9f880" />
</div>

<br>

<br>

# Important Design Decisions

- Use lookup tables (Country, Payment Type, Shipping Method, Order Status).
- Use self-joins for hierarchical categories.
- Separate Product from Product Item.
- Use junction tables for many-to-many relationships.
- Store purchase price in Order Line.
- Link reviews to purchased products.
- Store passwords securely.
- Avoid SQL reserved words like `ORDER`.

<br>

# Instructor Tips

- Third-party payment providers are safer than storing credit card details.
- EAV-style product attributes provide flexibility but may not be ideal for very large systems.
- Practice converting the ERD into SQL tables manually.
- Related tutorials on Address Design and Many-to-Many Relationships provide deeper understanding.
