


# Database Design Story: Food Delivery App (Swiggy / Zomato)


# The Complete Story

```
Customer joins

↓

Customer saves addresses

↓

Restaurants register

↓

Restaurants create menu

↓

Customer selects restaurant

↓

Customer chooses menu items

↓

Food order is created

↓

Order stores selected address

↓

Order contains multiple menu items

↓

Driver is assigned

↓

Order status changes

↓

Food delivered

↓

Ratings are recorded
```

<br>

<img width="700"  alt="image" src="https://github.com/user-attachments/assets/8b8dfe48-6ec2-42e8-b144-ec48e23adaed" />

<br>

<img width="700" alt="image" src="https://github.com/user-attachments/assets/21fed82a-20c2-45be-84a6-ff9fb4ee3fd0" />

Don't look at this as tables and foreign keys.

Instead, imagine the journey of a customer ordering food.

---

## Step 1: Customer joins the platform

A new customer signs up.

Now ask:

> What belongs to a customer?

The customer can save multiple delivery addresses.

```
Customer
    │
    └── CustomerAddress
              │
              └── Address
```

Business Rule:

- One customer can have many addresses.
- The same address structure can also be used by restaurants.

---

## Step 2: Restaurants register

Restaurants join the platform.

Every restaurant has one physical address.

```
Restaurant
      │
      └── Address
```

Business Rule:

- One restaurant has one address.
- Many restaurants can exist in the system.

---

## Step 3: Restaurants create their menu

Every restaurant offers food items.

Ask yourself:

> Can one restaurant have many menu items?

Yes.

Can one menu item belong to multiple restaurants?

No.

```
Restaurant
      │
      └────── MenuItem
```

Business Rule:

- One restaurant offers many menu items.
- Every menu item belongs to exactly one restaurant.

---

## Step 4: Customer places an order

The customer selects a restaurant and places an order.

Now think:

What information should an order remember?

- Which customer placed it?
- Which restaurant received it?
- Which delivery address should be used?

```
Customer
      │
Restaurant
      │
Address
      │
      ▼
   FoodOrder
```

Business Rule:

Every order belongs to

- one customer
- one restaurant
- one delivery address

---

## Step 5: Order contains food items

Now ask:

Can one order contain many menu items?

Yes.

Can one menu item appear in many different orders?

Yes.

That is a Many-to-Many relationship.

Whenever you see this...

```
Order
      ↔
Menu Item
```

...you immediately create a bridge table.

```
FoodOrder
      │
      ▼
OrderMenuItem
      ▲
      │
 MenuItem
```

Business Rule:

Each row represents

> "This order contains this menu item."

It also stores the quantity ordered.

---

## Step 6: Assign a delivery driver

Once the restaurant accepts the order,

a delivery driver is assigned.

Ask:

Can one driver deliver many orders?

Yes.

Can one order have multiple drivers?

Normally no.

```
Driver
      │
      └────── FoodOrder
```

Business Rule:

One driver delivers many orders.

Each order has one assigned driver.

---

## Step 7: Track order status

Orders change over time.

```
Placed

↓

Accepted

↓

Preparing

↓

Out for Delivery

↓

Delivered
```

Instead of storing text repeatedly,

store the status separately.

```
OrderStatus
      │
      └────── FoodOrder
```

Business Rule:

Every order always has one current status.

---

## Step 8: Delivery completes

After delivery,

the order stores

- total amount
- delivery fee
- ratings
- timestamps

Notice something important:

These are **facts about the order**, not about the customer or restaurant.

That's why they belong inside `FoodOrder`.

---


# How to Think Like a Database Designer

Don't ask:

> "Which table should connect to which?"

Instead ask:

> "What happens next in the real world?"

For every step, identify:

1. **Who is performing the action?**
   - Customer
   - Restaurant
   - Driver

2. **What object is being created or used?**
   - Order
   - Menu Item
   - Address

3. **Who owns it?**
   - Customer owns addresses.
   - Restaurant owns menu items.
   - Customer owns orders.

4. **How many can exist?**
   - One customer → Many orders
   - One restaurant → Many menu items
   - One order → Many menu items
   - One driver → Many orders

5. **Is it Many-to-Many?**
   - Order ↔ Menu Item ✅
   - Create `OrderMenuItem`

---

# Final Mindset

Professional database designers don't start with tables.

They imagine the application's workflow.

```
Customer
    ↓
Browse Restaurants
    ↓
View Menu
    ↓
Select Food
    ↓
Create Order
    ↓
Choose Address
    ↓
Restaurant Receives Order
    ↓
Assign Driver
    ↓
Deliver Food
    ↓
Rate Experience
```

Every **noun** in the story becomes an **entity**, and every **arrow** becomes a **relationship**. Once you train yourself to think in workflows instead of tables, designing ER diagrams becomes a natural process instead of a memorization exercise.
