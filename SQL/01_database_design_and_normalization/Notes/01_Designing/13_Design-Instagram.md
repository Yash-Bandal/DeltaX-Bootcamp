# Database Design: Instagram

In this tutorial, we'll design a simplified relational database for an application similar to **Instagram**.

> **Note**
>
> This is an educational database design that demonstrates common relational database concepts.
> The actual Instagram database is far more complex and distributed across many services.


<br>



# Step 1: Understand the Requirements

Before creating tables, identify what the application should do.

Suppose we want to support these features:

- Users can create accounts.
- Users can upload photos and videos.
- A post may contain multiple photos/videos.
- Users can follow other users.
- Users can like and comment on posts.
- Users can tag people in photos.
- Users can apply filters and effects.
- Users can create Stories and Reels.

These requirements tell us what information needs to be stored.



<br>



# Step 2: Identify the Main Entities

Look for the important nouns.

- User
- Post
- Media
- Comment
- Like
- Filter
- Effect
- Story

These usually become database tables.


<br>



# Step 3: Design the User Table

Everything revolves around users.

Before someone can upload a photo, like a post, or follow another user, they must first have an account.


<br>
<div align = "center">
<img width="158" height="163" alt="image" src="https://github.com/user-attachments/assets/75c6e6ca-0719-4408-b301-3fd77b091a3f" />
</div>
<br>


## User

| user_id | profile_name | first_name | last_name | signup_date |
|----------|--------------|------------|-----------|-------------|
| 1 | harry_p | Harry | Potter | 2024-01-15 |
| 2 | hermione | Hermione | Granger | 2024-02-10 |

Notice that we use

```
user_id
```

as the primary key.

Although usernames are usually unique, users may be allowed to change them.

A primary key should remain stable.


<br>



# Step 4: Design Posts

Now the application should allow users to upload posts.

Ask yourself:

> **Who creates a post?**

A user.

Can one user create many posts?

Yes.

Can one post belong to many users?

No.

Therefore,

```
One User

↓

Many Posts
```


<br>
<div align = "center">
<img width="405" height="158" alt="image" src="https://github.com/user-attachments/assets/efc9e347-045a-4238-be30-6789566bbf5b" />
</div>
<br>

This is a **One-to-Many** relationship.

## Post

| post_id | user_id | caption | created_at |
|----------|----------|----------|------------|
| 101 | 1 | My vacation! | 2025-06-10 |

The `user_id` is a foreign key pointing to the author.


<br>



# Step 5: Why Separate Media from Posts?

Suppose Instagram allowed only one image per post.

Then this design would work.

## Post

| post_id | image |
|----------|--------|
| 101 | beach.jpg |

But Instagram allows carousel posts.

<br>
<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/a473ed79-65ec-4348-a3ad-cc2d87adaf4e" />
</div>
<br>

Example:

```
Vacation

↓

Photo 1

Photo 2

Photo 3

Video
```

Now one post contains multiple media files.

This is another relationship.

```
One Post

↓

Many Media Files
```

Instead of adding columns like

```
image1

image2

image3

video1
```

we create another table.

## Post Media

| media_id | post_id | media_file | position |
|-----------|----------|------------|----------|
| 1 | 101 | beach.jpg | 1 |
| 2 | 101 | sunset.jpg | 2 |
| 3 | 101 | waves.mp4 | 3 |

The `position` determines the order of images in the carousel.


<br>



# Step 6: Followers

Users should be able to follow each other.

Ask two questions.

Can one user follow many users?

Yes.

Can one user be followed by many users?

Also yes.

Therefore,

<br>
<div align = "center">
<img width="419" height="301" alt="image" src="https://github.com/user-attachments/assets/b75c750c-3527-404f-a153-709fded7b271" />
</div>
<br>

```
Many Users

        ↔

Many Users
```

This is a **self-referencing many-to-many relationship**.

We create a junction table.

## Follower

| follower_id | followed_id |
|--------------|-------------|
| Harry | Hermione |
| Harry | Ron |
| Ron | Harry |

Each row means:

> One user follows another user.

This table allows us to answer questions like:

- Who follows Harry?
- Who is Harry following?
- How many followers does Hermione have?


<br>



# Step 7: Filters and Effects

Instagram allows users to edit photos.

Think carefully.


A filter belongs to a single media item.


<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/df19b5da-4451-4579-8408-8de050efe875" />
</div>
<br>


```
Photo

↓

Vintage Filter
```

But a photo may also have multiple effects.

Example:

```
Brightness

Contrast

Saturation

Sharpness
```

One photo can have many effects.

One effect can be applied to many photos.

```
Many Media

        ↔

Many Effects
```

This requires another junction table.

Example:

| media_id | effect_id | intensity |
|-----------|-----------|-----------|
| 10 | Brightness | 70 |
| 10 | Contrast | 40 |

The `intensity` stores how strongly the effect is applied.


<br>



# Step 8: Locations

Users can attach locations to posts.

The simplest design stores:

- Latitude
- Longitude

inside the post.


<br>
<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/da92746d-221c-4566-812d-f468ae616f73" />
</div>
<br>

## Post

| post_id | latitude | longitude |
|----------|----------|-----------|
| 101 | 40.7128 | -74.0060 |

Applications can later convert these coordinates into readable locations using mapping services.


<br>



# Step 9: Tagging Users

Instagram lets users tag people inside photos.

Example:

```
Beach Photo

↓

Harry tagged

↓

Hermione tagged

↓

Ron tagged
```

Ask the questions.

Can one photo tag many users?

Yes.

Can one user be tagged in many photos?

Also yes.

Again,

```
Many

↓

Many
```

We create another junction table.

## Media Tag

| media_id | user_id | x | y |
|-----------|----------|----|----|
| 10 | Hermione | 62 | 45 |
| 10 | Ron | 25 | 78 |

The `x` and `y` coordinates tell the application where to display the tag.

Notice that these coordinates belong to the **tag**, not the user or the photo.


<br>



# Step 10: Comments

Users can comment on posts.

Ask:

Can one post have many comments?

Yes.

Can one user write many comments?

Yes.

Therefore,

```
User

↓

Comment

↓

Post
```

## Comment

| comment_id | post_id | user_id | comment |
|-------------|----------|----------|----------|
| 1 | 101 | Hermione | Beautiful! |
| 2 | 101 | Ron | Amazing picture! |


<br>



## Replies

Instagram allows replies to comments.

Instead of creating another table, a comment simply references another comment.

Example:

| comment_id | replied_to |
|-------------|------------|
| 10 | NULL |
| 11 | 10 |
| 12 | 11 |

This is called a **self-referencing relationship**.

Every reply is just another comment.


<br>



# Step 11: Likes

Can one user like many posts?

Yes.

Can one post receive many likes?

Yes.

Again,

```
Many Users

        ↔

Many Posts
```

We create a junction table.

## Post Like

| user_id | post_id |
|----------|----------|
| Harry | 101 |
| Hermione | 101 |
| Ron | 101 |

Each row represents one like.

Counting likes is simply counting rows.


<br>



# Step 12: Stories and Reels

Instagram has different kinds of posts.

Instead of creating completely separate tables,

```
Photo Post

Story

Reel
```

we can classify them.

## Post Type

| type_id | name |
|----------|------|
| 1 | Post |
| 2 | Story |
| 3 | Reel |

The `Post` table stores a `type_id`.

```
Post

↓

Post Type
```

Now adding new content types becomes much easier.

For example:

- Live Stream
- Highlight
- Event

No major schema redesign is needed.


<br>



# Putting Everything Together


<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/b4fc27c5-c393-4768-8ae2-819a98ebe4b0" />
</div>
<br>

Every table represents either:

- an **entity**
  - User
  - Post
  - Media
  - Effect

or

- a **relationship**
  - Like
  - Follow
  - Tag
  - Comment


<br>

