# Ownership, Lifecycle, Parent–Child Resources, and Foreign Keys

When designing APIs and databases, **ownership** and **lifecycle** help us understand how two resources are related.

They are related concepts, but they are **not the same thing as API URL structure**.

# Index

1. [Ownership — "Who does this belong to?"](#1-ownership---who-does-this-belong-to)
2. [Lifecycle — "Can this child exist without the parent?"](#2-lifecycle---can-this-child-exist-without-the-parent)
3. [Foreign Keys Represent the Database Relationship](#3-foreign-keys-represent-the-database-relationship)
4. [Foreign Key ≠ Automatically Cascade](#4-foreign-key--automatically-cascade)
5. [Independent Lifecycle Does NOT Mean "No FK Anywhere"](#5-independent-lifecycle-does-not-mean-no-fk-anywhere)
6. [Ownership Does Not Automatically Determine the URL](#6-ownership-does-not-automatically-determine-the-url)
7. [Dependent Lifecycle Can Still Have Its Own Endpoint](#7-dependent-lifecycle-can-still-have-its-own-endpoint)
8. [Why Would We Use the Nested URL?](#8-why-would-we-use-the-nested-url)
9. [Why Your Cart Example Is Different](#9-why-your-cart-example-is-different)
10. [A Useful Way to Think About It](#10-a-useful-way-to-think-about-it)
11. [Your Movie → Review Case](#11-your-movie--review-case)
12. [The Whole Concept in One Picture](#12-the-whole-concept-in-one-picture)

<br>

---

<br>


## 1. Ownership — "Who does this belong to?"

Suppose we have:

```text
User
 └── Address
```

If Address 10 belongs to User 5:

```text
Address 10
    UserId = 5
```

We can say:

> Address 10 belongs to User 5.

This is an **ownership/relationship** concept.

Another example:

```text
Movie
 └── Review
```

A review belongs to a particular movie:

```text
Review 17
    MovieId = 5
```


<br>

---

<br>



# 2. Lifecycle — "Can this child exist without the parent?"

This is the more important distinction.

### Independent lifecycle

Consider:

```text
Cart
 └── CartItem
       └── Product
```

A Product can exist without any Cart.

For example:

```text
Product 10 = iPhone
```

It can exist in the product catalogue even when:

```text
Cart 1 → deleted
Cart 2 → deleted
```

The Product still exists.

So:

```text
Product lifecycle
       ≠
Cart lifecycle
```

The Product is **independent** of the Cart.


<br>




### Dependent lifecycle

Now consider:

```text
Movie
 └── Review
```

A Review like:

```text
Review 17
Message = "Excellent movie!"
MovieId = 5
```

doesn't have meaningful existence without Movie 5.

If Movie 5 is permanently deleted, Review 17 should normally also disappear.

So:

```text
Review lifecycle
       depends on
Movie lifecycle
```

This is a **dependent lifecycle**.


<br>

---

<br>



# 3. Foreign Keys represent the database relationship

For our Movie → Review example:

```text
Movies
  Id

Reviews
  Id
  MovieId
```

We can enforce:

```sql
FOREIGN KEY (MovieId)
REFERENCES Foundation.Movies(Id)
```

This means:

> A Review cannot reference a Movie that doesn't exist.

For example, this should fail:

```text
Review.MovieId = 999
```

if Movie 999 doesn't exist.

So the FK gives us **referential integrity**.


<br>

---

<br>



# 4. Foreign Key ≠ Automatically Cascade

This is an important distinction.

You can have:

```sql
FOREIGN KEY (MovieId)
REFERENCES Movies(Id)
```

without cascade.

That means:

```text
Movie 5
   ↑
Review 10
```

If you try to delete Movie 5 while Review 10 still exists, SQL Server can reject the deletion because the Review still references it.

You can instead define:

```sql
FOREIGN KEY (MovieId)
REFERENCES Movies(Id)
ON DELETE CASCADE
```

Then:

```text
DELETE Movie 5
       ↓
Reviews belonging to Movie 5
       ↓
automatically deleted
```

So:

> **Dependent lifecycle → FK relationship is typically appropriate.**

But:

> **FK → does NOT necessarily mean `ON DELETE CASCADE`.**

Cascade is a separate database decision.


<br>

---

<br>



# 5. Independent lifecycle does NOT mean "no FK anywhere"

This is where the Cart/Product example can be confusing.

Suppose:

```text
Cart
 └── CartItem
       └── Product
```

The database can have:

```text
CartItem.CartId
    → Cart.Id

CartItem.ProductId
    → Product.Id
```

So there are **two FKs**.

But their meanings are different.

### CartItem → Cart

CartItem depends on the Cart:

```text
Cart deleted
   ↓
CartItem should disappear
```

Potentially:

```sql
CartItem.CartId
    → Cart.Id
    ON DELETE CASCADE
```

### CartItem → Product

Product has an independent lifecycle:

```text
Product deleted
   ↓
CartItem relationship needs handling
```

The Product itself isn't owned by the Cart.

So you might **not** cascade Product deletion into unrelated product resources. You may instead prevent deletion, soft-delete the Product, or handle historical cart data differently depending on the business rules.


<br>

---

<br>



# 6. Ownership does not automatically determine the URL

This is extremely important.

Suppose:

```text
User
 └── Order
```

An Order belongs to a User.

You could have:

```http
GET /users/5/orders
```

Meaning:

> Give me User 5's orders.

But you could also have:

```http
GET /orders/100
```

Meaning:

> Give me Order 100.

Both can be valid.

Why?

Because **API URL design and database lifecycle are separate concerns**.


<br>

---

<br>



# 7. Dependent lifecycle can still have its own endpoint

Your Review example is exactly this.

Review depends on Movie:

```text
Movie
 └── Review
```

But you could still expose:

```http
GET /reviews/17
```

because `17` uniquely identifies the Review.

The fact that Review depends on Movie **doesn't make this URL invalid**.

You could alternatively expose:

```http
GET /movies/5/reviews/17
```

which additionally expresses the Movie context.


<br>

---

<br>



# 8. Why would we use the nested URL?

Compare:

```http
GET /reviews/17
```

with:

```http
GET /movies/5/reviews/17
```

The first says:

> Give me Review 17.

The second says:

> Give me Review 17 **belonging to Movie 5**.

The second lets us validate the relationship.

For example:

```text
Review 17 → Movie 8
```

Someone requests:

```http
GET /movies/5/reviews/17
```

A query like:

```sql
WHERE Id = @Id
AND MovieId = @MovieId
```

returns nothing because Review 17 belongs to Movie 8, not Movie 5.


<br>

---

<br>



# 9. Why your Cart example is different

Suppose we have:

```text
Cart 1
 └── CartItem 50
       └── Product 10
```

The Product itself is an independent resource:

```text
Product 10
```

can exist without Cart 1.

Therefore:

```http
GET /products/10
```

is completely natural.

And:

```http
GET /carts/1/items
```

is asking a different question:

> Which items are currently in Cart 1?

So the parent can provide **context** without controlling the child's lifecycle.


<br>

---

<br>



# 10. A useful way to think about it

When you encounter:

```text
A → B
```

ask these questions:

### Question 1 — Relationship

> Does B belong to A?

If yes, there is some relationship.


<br>





### Question 2 — Lifecycle

> Can B meaningfully exist without A?

If:

```text
YES
```

→ B has an **independent lifecycle**.

If:

```text
NO
```

→ B has a **dependent lifecycle**.


<br>




### Question 3 — Database

> Should the database prevent B from referencing a non-existent A?

Usually, if B depends on A:

```text
B.AId → A.Id
```

with a foreign key.


<br>



### Question 4 — Delete behavior

> What should happen to B when A is deleted?

Possibilities include:

```text
CASCADE
    ↓
Delete B automatically
```

or:

```text
RESTRICT / NO ACTION
    ↓
Don't allow A to be deleted while B exists
```

or application-level handling / soft deletion, depending on the domain.


<br>




### Question 5 — API URL

Only **after that**, ask:

> Should the API expose B through A?

Possible designs:

```http
GET /A/{aId}/B
```

or:

```http
GET /B/{bId}
```

or sometimes **both**, depending on the operations.

The lifecycle does **not mechanically determine the URL**.


<br>

---

<br>



# 11. Your Movie → Review case

For your project:

```text
Movie
   │
   │ 1 → many
   ↓
Review
```

Database:

```text
Movies
---------
Id
Name
...

Reviews
---------
Id
Message
MovieId  → Movies.Id
```

Lifecycle:

```text
Movie
  ↓
Review depends on Movie
```

Therefore:

```sql
MovieId INT NOT NULL

FOREIGN KEY (MovieId)
REFERENCES Foundation.Movies(Id)
```

is appropriate.

And you can choose cascade if the business rule is:

```text
Delete Movie
    ↓
Delete its Reviews
```

API-wise, this is natural:

```http
GET    /api/movies/5/reviews
POST   /api/movies/5/reviews
GET    /api/movies/5/reviews/17
PUT    /api/movies/5/reviews/17
DELETE /api/movies/5/reviews/17
```

But that doesn't mean:

```http
GET /api/reviews/17
```

is technically impossible or wrong.


<br>

---

<br>



# 12. The whole concept in one picture

```text
                 RELATIONSHIP
                     │
              "Does B belong to A?"
                     │
             ┌───────┴───────┐
             │               │
            YES              NO
             │
             ↓
        LIFECYCLE
             │
      Can B exist without A?
             │
       ┌─────┴─────┐
       │           │
      YES          NO
       │           │
       ↓           ↓
 Independent    Dependent
 lifecycle      lifecycle
       │           │
       │           ↓
       │       Usually FK
       │           │
       │       Delete behavior
       │       (cascade/restrict/etc.)
       │
       └──────────────┐
                      ↓
              API URL is a
              separate design decision
                      │
             ┌────────┴────────┐
             ↓                 ↓
       /B/{id}           /A/{aId}/B/{id}
```

### The key takeaway

**Ownership** tells you about the relationship.

**Lifecycle** tells you whether the child depends on the parent's existence.

**Foreign keys** enforce the relationship at the database level.

**Cascade/restrict** determines what happens when the parent is deleted.

**API nesting** is a separate design decision about how you expose that relationship.

For your **Movie → Review**, the important practical fact is: **Review is dependent on Movie, so the
database should enforce `Review.MovieId → Movie.Id`; the API may still expose Review directly by its own ID if that fits the API design.**
