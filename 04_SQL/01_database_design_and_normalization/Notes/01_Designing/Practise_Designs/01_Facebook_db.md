# Facebook Database SQL Practice

## Database Tables

<br>
<p>Instagram</p>
<div align = "center">
    <img width="650" alt="image" src="https://github.com/user-attachments/assets/b8029e50-3e55-4e99-a7df-c4b8bd826828" />
<p>FB</p>
<img width="650" alt="image" src="https://github.com/user-attachments/assets/c69cd20e-ba1c-4ae2-9e6b-1efcd40106bc" />
</div>
<br>

- `user_profile` → Stores user information.
- `user_post` → Stores posts created by users.
- `post_like` → Stores which user liked which post.
- `post_comment` → Stores comments made on posts.
- `friendship` → Stores friendships between users.


<br>

| Important Question                                 | Number in Full Markdown |
| -------------------------------------------------- | ----------------------: |
| 1. [Total number of posts created by each user](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#9-find-the-total-number-of-posts-created-by-each-user)      |                  **#9** |
| 2. [Post with highest number of likes](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#21-find-the-post-that-has-received-the-highest-number-of-likes)               |                 **#21** |
| 3. [Total number of friends for each user](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#18-find-the-total-number-of-friends-for-each-user)           |                 **#18** |
| 4. [Users who have not created any posts](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#10-find-all-users-who-have-not-created-any-posts)            |                 **#10** |
| 5. [Users who are friends with each other](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#20-find-the-mutual-friends-of-alice-and-bob)           |                 **#20** |
| 6. [Total number of comments received by each post](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#16-find-the-number-of-comments-on-every-post)  |                 **#16** |
| 7. [Top 5 users with highest number of friends](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#19-find-the-top-5-users-with-the-highest-number-of-friends)      |                 **#19** |
| 8. [Posts that have not received any likes](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#13-find-all-posts-that-have-not-received-any-likes)          |                 **#13** |
| 9. [Total engagement for each post](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#42-find-the-total-engagement-for-each-post)                  |                 **#42** |
| 10. [Users who liked posts created by their friends](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_SQL/01_database_design_and_normalization/Notes/01_Designing/Practise_Designs/01_Facebook_db.md#46-find-users-who-have-liked-posts-created-by-their-friends) |                 **#46** |


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

## For a particular post

``` sql
SELECT COUNT(*) AS TotalLikes
FROM post_like
WHERE post_id = 101;
```

## For all posts

``` sql
SELECT
    UP.id AS PostID,
    UP.written_text,
    COUNT(PL.id) AS TotalLikes
FROM user_post UP
LEFT JOIN post_like PL
    ON UP.id = PL.post_id
GROUP BY
    UP.id,
    UP.written_text;
```

## Logic

-   Each row in `post_like` represents one like.
-   `COUNT(*)` counts likes for a specific post.
-   `LEFT JOIN` ensures posts with zero likes are included when counting
    all posts.

<br>

# 3. Which posts has Bob liked?

## Only Post IDs

``` sql
SELECT
    UP.given_name AS UserName,
    PL.post_id
FROM user_profile UP
INNER JOIN post_like PL
    ON PL.profile_id = UP.id
WHERE UP.given_name = 'Bob';
```

## Post IDs + Post Text

``` sql
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

## Logic

-   Join `user_profile → post_like → user_post`.
-   Find Bob.
-   Return the posts liked by Bob.

<br>

# 4. How many comments are on a post?

## For a particular post

``` sql
SELECT COUNT(*) AS TotalComments
FROM post_comment
WHERE post_id = 101;
```

## Logic

-   Every row in `post_comment` represents one comment.
-   Count comments for the given `post_id`.

<br>

# 5. Show all comments on a post along with user name

## Query

``` sql
SELECT
    UP.given_name AS UserName,
    PC.comment_text
FROM post_comment PC
INNER JOIN user_profile UP
    ON PC.profile_id = UP.id
WHERE PC.post_id = 101;
```

## Logic

-   Join comments with users.
-   Filter by the required post.

<br>

# 6. Which user created a particular post?

## Query

``` sql
SELECT
    UP.given_name AS UserName,
    P.id AS PostID,
    P.written_text
FROM user_profile UP
INNER JOIN user_post P
    ON P.profile_id = UP.id
WHERE P.id = 101;
```

## Logic

-   Every post stores its creator's `profile_id`.
-   Join `user_post → user_profile`.

<br>

# 7. What posts did Alice publish today?

## Query

``` sql
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

## Logic

-   Join user with posts.
-   Filter for Alice.
-   Compare only the date portion of `created_datetime`.

<br>

# 8. List all posts with creator name

## Query

``` sql
SELECT
    UP.given_name,
    P.written_text
FROM user_profile UP
INNER JOIN user_post P
    ON P.profile_id = UP.id;
```

<br>

# 9. Find the total number of posts created by each user

## Query

``` sql
SELECT
    UP.given_name,
    COUNT(P.id) AS TotalPosts
FROM user_profile UP
LEFT JOIN user_post P
    ON P.profile_id = UP.id
GROUP BY UP.given_name;
```

## Logic

-   `LEFT JOIN` ensures users with zero posts are also included.
-   `COUNT(P.id)` counts posts for each user.
-   `GROUP BY` creates one result per user.

<br>

# 10. Find all users who have not created any posts

## Query

``` sql
SELECT
    UP.given_name
FROM user_profile UP
LEFT JOIN user_post P
    ON P.profile_id = UP.id
WHERE P.id IS NULL;
```

## Logic

-   Start with all users.
-   `LEFT JOIN` keeps users even when they have no posts.
-   `P.id IS NULL` identifies users without posts.

<br>

# Likes

# 11. Find users who liked a particular post

## Query

``` sql
SELECT
    UP.given_name
FROM user_profile UP
INNER JOIN post_like PL
    ON PL.profile_id = UP.id
WHERE PL.post_id = 101;
```

<br>

# 12. Find the total number of likes on every post

## Query

``` sql
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

# 13. Find all posts that have not received any likes

## Query

``` sql
SELECT
    P.id,
    P.written_text
FROM user_post P
LEFT JOIN post_like PL
    ON PL.post_id = P.id
WHERE PL.id IS NULL;
```

## Logic

-   `LEFT JOIN` keeps posts even when they have no likes.
-   `PL.id IS NULL` identifies posts with zero likes.

<br>

# 14. Which user liked whose post?

## Query

``` sql
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

## Logic

``` text
Liker → Like → Post → Post Owner
```

<br>

# Comments

# 15. Find comments made by Bob

## Query

``` sql
SELECT
    UP.given_name,
    PC.comment_text
FROM user_profile UP
INNER JOIN post_comment PC
    ON PC.profile_id = UP.id
WHERE UP.given_name = 'Bob';
```

<br>

# 16. Find the number of comments on every post

## Query

``` sql
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

# 17. Find all posts with no comments

## Query

``` sql
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

# 18. Find the total number of friends for each user

## Query

``` sql
SELECT
    UP.given_name,
    COUNT(*) AS Friends
FROM friendship F
INNER JOIN user_profile UP
    ON UP.id = F.profile_request
    OR UP.id = F.profile_accept
GROUP BY UP.given_name;
```
Also count ids wi nriends
```sql
SELECT
    UP.id,
    UP.given_name,
    UP.surname,
    COUNT(F.id) AS friends
FROM user_profile UP
LEFT JOIN friendship F
    ON UP.id = F.profile_request
    OR UP.id = F.profile_accept
GROUP BY
    UP.id,
    UP.given_name,
    UP.surname;
```

> Note: This can be improved using `UNION` to avoid the `OR` condition
> in the join.

<br>

# 19. Find the top 5 users with the highest number of friends

## Query

``` sql
SELECT TOP 5
    UP.given_name,
    COUNT(*) AS Friends
FROM friendship F
INNER JOIN user_profile UP
    ON UP.id = F.profile_request
    OR UP.id = F.profile_accept
GROUP BY UP.given_name
ORDER BY Friends DESC;
```

<br>

# 20. Find the mutual friends of Alice and Bob

## Concept

-   Find Alice's friends.
-   Find Bob's friends.
-   Find users common to both sets.
-   This is an intersection of two friend sets.

## Using `INTERSECT`

``` sql
WITH AliceFriends AS
(
    SELECT F.profile_request AS FriendID
    FROM friendship F
    INNER JOIN user_profile A
        ON A.id = F.profile_accept
    WHERE A.given_name = 'Alice'

    UNION

    SELECT F.profile_accept AS FriendID
    FROM friendship F
    INNER JOIN user_profile A
        ON A.id = F.profile_request
    WHERE A.given_name = 'Alice'
),
BobFriends AS
(
    SELECT F.profile_request AS FriendID
    FROM friendship F
    INNER JOIN user_profile B
        ON B.id = F.profile_accept
    WHERE B.given_name = 'Bob'

    UNION

    SELECT F.profile_accept AS FriendID
    FROM friendship F
    INNER JOIN user_profile B
        ON B.id = F.profile_request
    WHERE B.given_name = 'Bob'
)
SELECT
    P.id,
    P.given_name,
    P.surname
FROM
(
    SELECT FriendID FROM AliceFriends
    INTERSECT
    SELECT FriendID FROM BobFriends
) M
INNER JOIN user_profile P
    ON P.id = M.FriendID;
```

<br>

# Aggregate Practice

# 21. Find the post that has received the highest number of likes

## Query

``` sql
SELECT TOP 1
    post_id,
    COUNT(*) AS Likes
FROM post_like
GROUP BY post_id
ORDER BY Likes DESC;
```

<br>

# 22. Find the post that has received the highest number of comments

## Query

``` sql
SELECT TOP 1
    post_id,
    COUNT(*) AS Comments
FROM post_comment
GROUP BY post_id
ORDER BY Comments DESC;
```

<br>

# 23. Find the user who has created the maximum number of posts

## Query

``` sql
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

# 24. Find the user who has liked the most posts

## Query

``` sql
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

# Advanced Queries

# 25. Find users who have never liked any post

## Query

``` sql
SELECT
    UP.id,
    UP.given_name
FROM user_profile UP
LEFT JOIN post_like PL
    ON PL.profile_id = UP.id
WHERE PL.id IS NULL;
```

<br>

# 26. Find users who have never commented

## Query

``` sql
SELECT
    UP.id,
    UP.given_name
FROM user_profile UP
LEFT JOIN post_comment PC
    ON PC.profile_id = UP.id
WHERE PC.id IS NULL;
```

<br>

# 27. Find posts with more than 5 likes

## Query

``` sql
SELECT
    P.id,
    P.written_text,
    COUNT(PL.id) AS Likes
FROM user_post P
INNER JOIN post_like PL
    ON PL.post_id = P.id
GROUP BY
    P.id,
    P.written_text
HAVING COUNT(PL.id) > 5;
```

<br>

# 28. Find posts with more comments than likes

## Query

``` sql
SELECT
    P.id,
    P.written_text,
    COUNT(DISTINCT PL.id) AS Likes,
    COUNT(DISTINCT PC.id) AS Comments
FROM user_post P
LEFT JOIN post_like PL
    ON PL.post_id = P.id
LEFT JOIN post_comment PC
    ON PC.post_id = P.id
GROUP BY
    P.id,
    P.written_text
HAVING COUNT(DISTINCT PC.id) > COUNT(DISTINCT PL.id);
```

<br>

# 29. Find users who liked their own post

## Query

``` sql
SELECT DISTINCT
    UP.given_name,
    P.id AS PostID,
    P.written_text
FROM user_profile UP
INNER JOIN post_like PL
    ON PL.profile_id = UP.id
INNER JOIN user_post P
    ON P.id = PL.post_id
    AND P.profile_id = UP.id;
```

<br>

# 30. Find users who commented on their own post

## Query

``` sql
SELECT DISTINCT
    UP.given_name,
    P.id AS PostID,
    P.written_text
FROM user_profile UP
INNER JOIN post_comment PC
    ON PC.profile_id = UP.id
INNER JOIN user_post P
    ON P.id = PC.post_id
    AND P.profile_id = UP.id;
```

<br>

# 31. Find the latest post created by every user

## Query

``` sql
SELECT
    UP.given_name,
    P.id AS PostID,
    P.written_text,
    P.created_datetime
FROM user_profile UP
INNER JOIN user_post P
    ON P.profile_id = UP.id
WHERE P.created_datetime = (
    SELECT MAX(P2.created_datetime)
    FROM user_post P2
    WHERE P2.profile_id = UP.id
);
```

<br>

# 32. Find the first post created by every user

## Query

``` sql
SELECT
    UP.given_name,
    P.id AS PostID,
    P.written_text,
    P.created_datetime
FROM user_profile UP
INNER JOIN user_post P
    ON P.profile_id = UP.id
WHERE P.created_datetime = (
    SELECT MIN(P2.created_datetime)
    FROM user_post P2
    WHERE P2.profile_id = UP.id
);
```

<br>

# 33. Find users having more than 10 friends

## Query

``` sql
SELECT
    UP.given_name,
    COUNT(*) AS Friends
FROM friendship F
INNER JOIN user_profile UP
    ON UP.id = F.profile_request
    OR UP.id = F.profile_accept
GROUP BY UP.given_name
HAVING COUNT(*) > 10;
```

<br>

# 34. Find posts liked by all friends

## Concept

For each post, check whether every friend of the post owner has liked
that post.

This is an advanced `NOT EXISTS` / relational division problem.

<br>

# 35. Find friend suggestions (friends of friends)

## Concept

If:

``` text
Alice → Bob
Bob → Charlie
```

then Charlie can be suggested as a friend for Alice.

Exclude:

-   Alice herself.
-   Alice's existing friends.

<br>

# 36. Rank users by total likes received

## Query

``` sql
SELECT
    UP.id,
    UP.given_name,
    COUNT(PL.id) AS LikesReceived,
    RANK() OVER (
        ORDER BY COUNT(PL.id) DESC
    ) AS UserRank
FROM user_profile UP
LEFT JOIN user_post P
    ON P.profile_id = UP.id
LEFT JOIN post_like PL
    ON PL.post_id = P.id
GROUP BY
    UP.id,
    UP.given_name;
```

<br>

# 37. Rank users by total posts

## Query

``` sql
SELECT
    UP.id,
    UP.given_name,
    COUNT(P.id) AS TotalPosts,
    RANK() OVER (
        ORDER BY COUNT(P.id) DESC
    ) AS UserRank
FROM user_profile UP
LEFT JOIN user_post P
    ON P.profile_id = UP.id
GROUP BY
    UP.id,
    UP.given_name;
```

<br>

# 38. Find the top 3 most active users

## Example: Based on number of posts

``` sql
SELECT TOP 3
    UP.given_name,
    COUNT(P.id) AS TotalPosts
FROM user_profile UP
INNER JOIN user_post P
    ON P.profile_id = UP.id
GROUP BY UP.given_name
ORDER BY TotalPosts DESC;
```

<br>

# 39. Find the most active commenter

## Query

``` sql
SELECT TOP 1
    UP.given_name,
    COUNT(PC.id) AS TotalComments
FROM user_profile UP
INNER JOIN post_comment PC
    ON PC.profile_id = UP.id
GROUP BY UP.given_name
ORDER BY TotalComments DESC;
```

<br>

# 40. Find the most active liker

## Query

``` sql
SELECT TOP 1
    UP.given_name,
    COUNT(PL.id) AS TotalLikes
FROM user_profile UP
INNER JOIN post_like PL
    ON PL.profile_id = UP.id
GROUP BY UP.given_name
ORDER BY TotalLikes DESC;
```

<br>

# 41. Find the most popular post based on likes and comments

## Query

``` sql
SELECT TOP 1
    P.id,
    P.written_text,
    COUNT(DISTINCT PL.id) AS Likes,
    COUNT(DISTINCT PC.id) AS Comments,
    COUNT(DISTINCT PL.id) + COUNT(DISTINCT PC.id) AS Engagement
FROM user_post P
LEFT JOIN post_like PL
    ON PL.post_id = P.id
LEFT JOIN post_comment PC
    ON PC.post_id = P.id
GROUP BY
    P.id,
    P.written_text
ORDER BY Engagement DESC;
```

<br>

# 42. Find the total engagement for each post

## Definition

``` text
Engagement = Likes + Comments
```

## Query

``` sql
SELECT
    P.id,
    P.written_text,
    COUNT(DISTINCT PL.id) AS Likes,
    COUNT(DISTINCT PC.id) AS Comments,
    COUNT(DISTINCT PL.id) + COUNT(DISTINCT PC.id) AS Engagement
FROM user_post P
LEFT JOIN post_like PL
    ON PL.post_id = P.id
LEFT JOIN post_comment PC
    ON PC.post_id = P.id
GROUP BY
    P.id,
    P.written_text;
```

## Why `DISTINCT`?

If a post has 3 likes and 2 comments, joining both tables can produce 6
intermediate rows (`3 × 2`). `COUNT(DISTINCT ...)` prevents the likes
and comments from being multiplied.

<br>

# 43. Find the average number of likes per user

## Query

``` sql
SELECT
    AVG(LikesGiven) AS AverageLikesPerUser
FROM (
    SELECT
        UP.id,
        COUNT(PL.id) AS LikesGiven
    FROM user_profile UP
    LEFT JOIN post_like PL
        ON PL.profile_id = UP.id
    GROUP BY UP.id
) AS UserLikes;
```

<br>

# 44. Find the average number of comments per post

## Query

``` sql
SELECT
    AVG(CommentCount) AS AverageCommentsPerPost
FROM (
    SELECT
        P.id,
        COUNT(PC.id) AS CommentCount
    FROM user_post P
    LEFT JOIN post_comment PC
        ON PC.post_id = P.id
    GROUP BY P.id
) AS PostComments;
```

<br>

# 45. Find the top N posts by likes using `RANK()`

## Example: Top 3

``` sql
WITH RankedPosts AS
(
    SELECT
        P.id,
        P.written_text,
        COUNT(PL.id) AS Likes,
        RANK() OVER (
            ORDER BY COUNT(PL.id) DESC
        ) AS PostRank
    FROM user_post P
    LEFT JOIN post_like PL
        ON PL.post_id = P.id
    GROUP BY
        P.id,
        P.written_text
)
SELECT
    id,
    written_text,
    Likes,
    PostRank
FROM RankedPosts
WHERE PostRank <= 3;
```

<br>

# 46. Find users who have liked posts created by their friends

## Query

``` sql
SELECT DISTINCT
    Liker.id AS LikerID,
    Liker.given_name AS Liker,
    Owner.id AS PostOwnerID,
    Owner.given_name AS PostOwner,
    P.id AS PostID,
    P.written_text
FROM post_like PL
INNER JOIN user_profile Liker
    ON Liker.id = PL.profile_id
INNER JOIN user_post P
    ON P.id = PL.post_id
INNER JOIN user_profile Owner
    ON Owner.id = P.profile_id
INNER JOIN friendship F
    ON (
        F.profile_request = Liker.id
        AND F.profile_accept = Owner.id
    )
    OR (
        F.profile_request = Owner.id
        AND F.profile_accept = Liker.id
    );
```

## Logic

-   Find who liked the post.
-   Find who owns the post.
-   Check whether the liker and post owner have a friendship record.
-   `DISTINCT` avoids duplicate results.

<br>

# Quick Concept Coverage

  SQL Concept               Questions
  ------------------------- --------------------------------------------------
  Basic `JOIN`              3, 5, 6, 8, 11, 14, 15
  `LEFT JOIN`               2, 9, 10, 12, 13, 16, 17, 25, 26
  `GROUP BY`                2, 9, 12, 16, 18, 19, 21--24, 27--28, 33, 36--44
  `HAVING`                  27, 33
  `UNION`                   1, 20
  `INTERSECT`               20
  Subquery                  31, 32, 43, 44
  `DISTINCT`                28, 29, 30, 41, 42, 46
  `TOP`                     19, 21--24, 38--41
  Window functions          36, 37, 45
  Self/relationship logic   1, 20, 35, 46
  Advanced aggregation      28, 36, 41--45
  Advanced relationships    34, 35, 46
