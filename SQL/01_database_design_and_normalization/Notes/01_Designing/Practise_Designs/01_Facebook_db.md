# Facebook Database SQL Practice

## Database Tables

<br>
<div align = "center">
<img width="650" alt="image" src="https://github.com/user-attachments/assets/c69cd20e-ba1c-4ae2-9e6b-1efcd40106bc" />
</div>
<br>

- `user_profile` → Stores user information.
- `user_post` → Stores posts created by users.
- `post_like` → Stores which user liked which post.
- `post_comment` → Stores comments made on posts.
- `friendship` → Stores friendships between users.

<br>

# 1. Who are Alice's friends?

### Query

I want to get **Requester** details where **acceptor** is **Alice**

Then

I want to get **Acceptor** details where **requester** is **Alice**

And Union

```sql
SELECT
    P.id,
    P.given_name,
    P.surname,
    P.country
FROM friendship F
INNER JOIN user_profile P
    ON F.profile_request = P.id
INNER JOIN user_profile A
    ON A.id = F.profile_accept
WHERE A.given_name = 'Alice'

UNION

SELECT
    P.id,
    P.given_name,
    P.surname,
    P.country
FROM friendship F
INNER JOIN user_profile P
    ON F.profile_accept = P.id
INNER JOIN user_profile A
    ON A.id = F.profile_request
WHERE A.given_name = 'Alice';
```
1. Start with `friendship` (F).
2. Join `user_profile` as P using profile_request to get the **requester**'s details.
3. Join `user_profile` again as A using profile_accept to get the **accepter**'s details.
4. Filter where the accepter is Alice.
5. Return the requester (P)

Then reverse for below

| profile_request|P.Name | profile_accept | P.Name|
| --------------- |--| -------------- | -------|
| 5            |  F1 | 14             | Alice |
| 9             |  F2 | 14             | Alice |


| profile_request|P.Name | profile_accept | P.Name|
| --------------- |--| -------------- | -------|
| 14            |  Alice | 4             | F3 |
| 14             |  Alice | 7             | F4 |

### Logic

- A user can appear in either friendship column.
- Get friends from both directions.
- `UNION` removes duplicates.

<br>

# 2. How many likes does a post have?

### Query

```sql
SELECT COUNT(*) AS TotalLikes
FROM post_like
WHERE post_id = 101;
```

### Logic

- Each row in `post_like` represents one like.
- Count rows for the given `post_id`.

<br>

# 3. Which posts has Bob liked?

## Only Post IDs

```sql
SELECT
    UP.given_name AS UserName,
    PL.post_id
FROM user_profile UP
INNER JOIN post_like PL
    ON PL.profile_id = UP.id
WHERE UP.given_name = 'Bob';
```

### Logic

- Find Bob.
- Join with `post_like`.
- Return liked post IDs.

<br>

## Post IDs + Post Text

```sql
SELECT
    UP.given_name AS UserName,
    PL.post_id,
    P.written_text
FROM user_profile UP
INNER JOIN post_like PL
    ON PL.profile_id = UP.id
INNER JOIN user_post P
    ON PL.post_id = P.id
WHERE UP.given_name = 'Bob';
```

### Logic

- Join User → Likes → Posts.
- Fetch post text.

<br>

# 4. How many comments are on a post?

### Query

```sql
SELECT COUNT(*) AS TotalComments
FROM post_comment
WHERE post_id = 101;
```

### Logic

- Every row in `post_comment` is one comment.
- Count comments for the post.

<br>

# 5. Show all comments on a post along with user name

### Query

```sql
SELECT
    UP.given_name AS UserName,
    PC.comment_text
FROM post_comment PC
INNER JOIN user_profile UP
    ON PC.profile_id = UP.id
WHERE PC.post_id = 101;
```

### Logic

- Join comments with users.
- Filter by post.

<br>

# 6. Which user created a particular post?

### Query

```sql
SELECT
    UP.given_name AS UserName,
    P.id AS PostID,
    P.written_text
FROM user_profile UP
INNER JOIN user_post P
    ON P.profile_id = UP.id
WHERE P.id = 101;
```

### Logic

- Every post stores its creator's `profile_id`.
- Join Posts → User.

<br>

# 7. What posts did Alice publish today?

### Query

```sql
SELECT
    UP.given_name AS UserName,
    P.written_text
FROM user_profile UP
INNER JOIN user_post P
    ON P.profile_id = UP.id
WHERE
    UP.given_name = 'Alice'
    AND CAST(P.created_datetime AS DATE) = CAST(GETDATE() AS DATE);
```

### Logic

- Join User → Posts.
- Compare only the date portion.

<br>

# Additional SQL Practice Questions

## User & Post

### List all posts with creator name.

```sql
SELECT
    UP.given_name,
    P.written_text
FROM user_profile UP
INNER JOIN user_post P
ON P.profile_id = UP.id;
```

<br>

### Total posts by each user.

```sql
SELECT
    UP.given_name,
    COUNT(P.id) AS TotalPosts
FROM user_profile UP
LEFT JOIN user_post P
ON P.profile_id = UP.id
GROUP BY UP.given_name;
```

<br>

### Users who never created a post.

```sql
SELECT
    UP.given_name
FROM user_profile UP
LEFT JOIN user_post P
ON P.profile_id = UP.id
WHERE P.id IS NULL;
```

<br>

# Likes

### Users who liked a particular post.

```sql
SELECT
    UP.given_name
FROM user_profile UP
INNER JOIN post_like PL
ON PL.profile_id = UP.id
WHERE PL.post_id = 101;
```

<br>

### Total likes on every post.

```sql
SELECT
    P.id,
    P.written_text,
    COUNT(PL.id) AS Likes
FROM user_post P
LEFT JOIN post_like PL
ON PL.post_id = P.id
GROUP BY
P.id,
P.written_text;
```

<br>

### Posts with zero likes.

```sql
SELECT
    P.id,
    P.written_text
FROM user_post P
LEFT JOIN post_like PL
ON PL.post_id = P.id
WHERE PL.id IS NULL;
```

<br>

### Which user liked whose post?

```sql
SELECT
    Liker.given_name AS LikedBy,
    Owner.given_name AS PostOwner,
    P.written_text
FROM post_like PL
INNER JOIN user_profile Liker
ON Liker.id = PL.profile_id
INNER JOIN user_post P
ON P.id = PL.post_id
INNER JOIN user_profile Owner
ON Owner.id = P.profile_id;
```

<br>

# Comments

### Comments made by Bob.

```sql
SELECT
    UP.given_name,
    PC.comment_text
FROM user_profile UP
INNER JOIN post_comment PC
ON PC.profile_id = UP.id
WHERE UP.given_name = 'Bob';
```

<br>

### Number of comments on every post.

```sql
SELECT
    P.id,
    P.written_text,
    COUNT(PC.id) AS Comments
FROM user_post P
LEFT JOIN post_comment PC
ON PC.post_id = P.id
GROUP BY
P.id,
P.written_text;
```

<br>

### Posts with no comments.

```sql
SELECT
    P.id,
    P.written_text
FROM user_post P
LEFT JOIN post_comment PC
ON PC.post_id = P.id
WHERE PC.id IS NULL;
```

<br>

# Friendship

### Total friends of each user.

```sql
SELECT
    UP.given_name,
    COUNT(*) AS Friends
FROM friendship F
INNER JOIN user_profile UP
ON
UP.id = F.profile_request
OR
UP.id = F.profile_accept
GROUP BY UP.given_name;
```

> Note: Better solved using `UNION` to avoid `OR` in joins.

<br>

### Mutual friends of Alice and Bob

(Advanced)

Hint:
- Find Alice's friends.
- Find Bob's friends.
- Use `INTERSECT` or join the two result sets.

<br>

# Aggregate Practice

### Most liked post.

```sql
SELECT TOP 1
    post_id,
    COUNT(*) AS Likes
FROM post_like
GROUP BY post_id
ORDER BY Likes DESC;
```

<br>

### Most commented post.

```sql
SELECT TOP 1
    post_id,
    COUNT(*) AS Comments
FROM post_comment
GROUP BY post_id
ORDER BY Comments DESC;
```

<br>

### User with maximum posts.

```sql
SELECT TOP 1
    UP.given_name,
    COUNT(P.id) AS Posts
FROM user_profile UP
INNER JOIN user_post P
ON P.profile_id = UP.id
GROUP BY UP.given_name
ORDER BY Posts DESC;
```

<br>

### User who liked the most posts.

```sql
SELECT TOP 1
    UP.given_name,
    COUNT(*) AS LikesGiven
FROM user_profile UP
INNER JOIN post_like PL
ON PL.profile_id = UP.id
GROUP BY UP.given_name
ORDER BY LikesGiven DESC;
```

<br>

# Good Interview Questions

- Find users who have never liked any post.
- Find users who have never commented.
- Find users who never posted.
- Find posts with more than 5 likes.
- Find posts with more comments than likes.
- Find users who liked their own post.
- Find users who commented on their own post.
- Find latest post by every user.
- Find first post by every user.
- Find users having more than 10 friends.
- Find posts liked by all friends.
- Find friend suggestions (friends of friends).
- Rank users by total likes received.
- Rank users by total posts.
- Top 3 most active users.
- Most active commenter.
- Most active liker.
- Most popular post (Likes + Comments).
- Average likes per user.
- Average comments per post.
- Top N posts by likes (using `ROW_NUMBER()` / `RANK()`).
