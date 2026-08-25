
# ECommerse DB


## Problem statement

```
You are asked to design the database for an e-commerce platform with the following requirements:

Users and addresses
A user can have multiple addresses.
An address belongs to exactly one user.
A user can place multiple orders.
Every order must belong to exactly one user.
A user may or may not have placed an order yet.

Products and categories
A product belongs to one subcategory.
A category can contain many products.
A category can have subcategories.
A product can have multiple variants, such as different sizes or colors.
Each variant belongs to exactly one product.

Orders
An order can contain multiple products.
A product can appear in many different orders.
For each product in an order, you need to store:
Quantity
Price at the time of purchase
The price stored in the order should not change if the product's current price changes later.

Payments
Every order must have at least one payment record.
An order can potentially have multiple payment attempts.
Each payment belongs to exactly one order.
A payment has a status such as Pending, Successful, or Failed
```



<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/0248a22c-8c69-42ec-8f78-535730f956c4" />
</div>

<br>


