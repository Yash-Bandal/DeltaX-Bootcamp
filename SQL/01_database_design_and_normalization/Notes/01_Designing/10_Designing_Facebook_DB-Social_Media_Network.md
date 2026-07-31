# Database Design: Social Network (Facebook)



In this tutorial, we'll design a simple social networking database similar to **Facebook**.

Rather than creating all tables at once, we'll start with the application's requirements and design the database one feature at a time.

This is how databases are typically designed in real projects.

<br>

---

> [!Important]
> ### Observe
> There are pros and cons of every design, use the one that suits the database, and has proper implementation
>
> Eg. To store posts, we could have different tables (img, text, video), or store all in single table
>
> Here we are storing in a single table, assuming it points to the server , that stores the posts

---

<br>




# Step 1: Identify the Requirements

Before creating any tables, ask:

> **What features should the application support?**

<br>
<div align = "center">
    <img width="500" alt="image" src="https://github.com/user-attachments/assets/40625b1d-7b71-4fa4-9ce3-a1797db67f41" />
</div>
<br>

Suppose our social network should allow users to:

- Create an account
- Add friends
- Create posts
- Like posts
- Comment on posts

Each feature usually becomes one or more database tables.


<br>



# Step 2: Design the User Profile

Everything revolves around users.

Before someone can post, like, or comment, they must have an account.

So the first entity is:

```
User Profile
```
> [!Tip]
> ### Important
> Self join is useful for hierarchies , Not suitable for us

<br>
<div align = "center">
<img width="250" alt="image" src="https://github.com/user-attachments/assets/1588926c-2d56-402f-816e-907b98e563a2" />
</div>
<br>


## Attributes

A user profile may contain:

- Email
- Password
- First Name
- Last Name
- Country
- Date of Birth

### Example

| profile_id | email | first_name | last_name | country |
|------------|--------|------------|-----------|----------|
| 1 | alice@email.com | Alice | Brown | USA |
| 2 | bob@email.com | Bob | Smith | Canada |

Notice that we use an artificial primary key:

```
profile_id
```

instead of using the email address.

### Why?

Although email addresses are unique, they can change.

A primary key should be stable throughout the lifetime of the record.

Therefore, use a surrogate key such as an auto-incrementing integer or UUID.

> **Avoid naming the table `user`**, since many databases treat it as a reserved keyword. Names like `user_profile` are safer and more descriptive.


<br>



# Step 3: Design Friendships

Now users should be able to become friends.

Think about the relationship.

One user can have many friends.

Another user can also have many friends.

```
User A
    ↕
User B

Many ↔ Many
```

<br>
<div align = "center">
<img width="250" alt="image" src="https://github.com/user-attachments/assets/fe6c4881-4357-46ab-ac8b-2a751060614e" />
</div>
<br>


A **many-to-many relationship** cannot be stored directly in one table.

Instead, create a junction table.

```
user_profile
--------------
profile_id

        ▲
        │
        │

friendship

profile_request
profile_accept
```

### Example

### User Profile

| profile_id | Name |
|------------|------|
| 1 | Alice |
| 2 | Bob |
| 3 | Charlie |

### Friendship

| profile_request | profile_accept |
|-----------------|----------------|
| 1 | 2 |
| 1 | 3 |
| 2 | 3 |

This means:

- Alice is friends with Bob.
- Alice is friends with Charlie.
- Bob is friends with Charlie.

Notice that no friend information is stored inside the `user_profile` table.

Relationships belong in their own table.


<br>



# Step 4: Design Posts

Users should now be able to publish posts.

Ask:

> **Who created this post?**

A post belongs to exactly one user.

One user can create many posts.

```
One User
      │
      ▼
Many Posts
```

<br>
<div align = "center">
    <img width="400" alt="image" src="https://github.com/user-attachments/assets/f8b32e76-4870-44b9-b2c4-5c757003296e" />
</div>
<br>



This is a **one-to-many relationship**.

### User Post

| post_id | profile_id | text | media_url |
|----------|------------|------|-----------|
| 101 | 1 | Hello World! | NULL |
| 102 | 2 | My vacation | beach.jpg |

The `profile_id` is a foreign key pointing back to the author.

Instead of storing images directly in the database, applications usually store only the file path or URL.

Example:

```
https://example.com/images/photo1.jpg
```

The actual image is stored on a file server or cloud storage.


<br>

---

# Feature
## See posts friends have added 
Assume its a News feed posted on app, \
That is designed in application, no database changes



---

<br>

# Step 5: Design Likes

### Requirement
Ability to Like and add comments to posts others have added

A user can like many posts.

A post can be liked by many users.

Again, we have a many-to-many relationship.




```
Users
    ↕
Posts
```

<br>
<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/7c6092ca-82ce-4514-8c28-a979d8922150" />
</div>
<br>



So we create another junction table.

### Post Like

| post_id | profile_id | liked_at |
|----------|------------|---------------------|
| 101 | 2 | 2025-06-01 10:20 |
| 101 | 3 | 2025-06-01 11:45 |
| 102 | 1 | 2025-06-02 09:30 |

This table answers questions like:

- Who liked this post?
- Which posts did Alice like?
- When was the post liked?

Each row represents one like.


<br>



# Step 6: Design Comments

Users should also be able to comment.

Think carefully.

One post can have many comments.

One user can write many comments.

Each comment belongs to exactly one post and one user.

<br>
<div align = "center">
    <p>
we can have multiple text comments, so create seperate entity
    </p>
<img width="500" alt="image" src="https://github.com/user-attachments/assets/6e66a9b9-0757-49cf-8b07-4e74dff91734" />
</div>
<br>


### Post Comment

| comment_id | post_id | profile_id | comment_text |
|-------------|----------|------------|----------------|
| 1 | 101 | 2 | Nice picture! |
| 2 | 101 | 3 | Awesome! |
| 3 | 102 | 1 | Looks great. |

Each comment stores:

- Which post it belongs to.
- Who wrote it.
- The comment itself.


<br>



# Overall Database Design

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/6e66a9b9-0757-49cf-8b07-4e74dff91734" />
</div>
<br>


The `user_profile` table is the central entity.

Every other table references it through foreign keys.


<br>



# Why Separate Tables?

Imagine storing everything inside the user table.

| profile_id | name | friends | posts | likes | comments |
|------------|------|----------|--------|--------|-----------|

How would you store:

- 500 friends?
- 100 posts?
- 3,000 likes?

It quickly becomes impossible.

Instead, relational databases represent these as **relationships**, where each related item becomes its own row in another table.

This keeps the data:

- Normalized
- Easy to query
- Easy to maintain
- Scalable


<br>

## Example query

<br>
<div align = "center">
    <p>1</p>
<img width="450" alt="image" src="https://github.com/user-attachments/assets/4822bcf1-fa59-4407-9763-2e5333acce2f" />
    <p>2</p>
<img width="450" alt="image" src="https://github.com/user-attachments/assets/0cd19943-c588-460a-ab69-c4beb97c45ba" />
</div>
<br>

<br>



# Example Questions the Database Can Answer

Because of this design, we can easily answer questions such as:

- Who are Alice's friends?
- How many likes does a post have?
- Which posts has Bob liked?
- How many comments are on a post?
- Which user created a particular post?
- What posts did Alice publish today?

Each question can be answered by joining the appropriate tables.

<br>


# Overview

- Start by identifying the application's features.
- Every major entity becomes a table.
- One-to-many relationships use foreign keys.
- Many-to-many relationships require junction tables.
- Store relationships separately instead of embedding lists inside records.
- Use surrogate primary keys instead of business values.
- Design the database around relationships, not screens or forms.
