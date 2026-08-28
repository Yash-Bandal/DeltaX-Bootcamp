
# SQL Practice Queries — Basic & Advanced

Database schema used:

```text
Actors
  Id, Name, Sex, DateOfBirth, ...

Movies
  Id, Name, YearOfRelease, Plot, ProducerId, Language, Profit, ...

Producers
  Id, Name, DateOfBirth, ...

Actor_Movies
  MovieId, ActorId
```

---

# Basic

## 1. Actors who have acted in no movies

### Query

```sql
SELECT
    A.Name AS ActorName
FROM Foundation.Actors A
LEFT JOIN Foundation.Actor_Movies AM
    ON A.Id = AM.ActorId
WHERE AM.ActorId IS NULL;
```

### Logic

Start with all actors. `LEFT JOIN` keeps actors even when no mapping exists. `NULL` on the `Actor_Movies` side means the actor has no movie.

### Output

| ActorName           |
| ------------------- |
| Alia Bhatt          |
| Actor Without Movie |

---

## 2. Movies that have no actors

### Query

```sql
SELECT
    M.Name AS MovieName
FROM Foundation.Movies M
LEFT JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
WHERE AM.MovieId IS NULL;
```

### Logic

Start with all movies. Keep movies for which no `Actor_Movies` row exists.

### Output

| MovieName           |
| ------------------- |
| Movie Without Actor |

---

## 3. Actors and the movies they have acted in

### Query

```sql
SELECT
    A.Name AS ActorName,
    M.Name AS MovieName
FROM Foundation.Actors A
INNER JOIN Foundation.Actor_Movies AM
    ON A.Id = AM.ActorId
INNER JOIN Foundation.Movies M
    ON M.Id = AM.MovieId;
```

### Logic

`Actors → Actor_Movies → Movies`. `INNER JOIN` returns only actors having movie mappings.

### Output

| ActorName      | MovieName       |
| -------------- | --------------- |
| Vicky Kaushal  | URI             |
| Yami Gautam    | URI             |
| Christian Bale | The Dark Knight |

---

## 4. Actors and movies, including actors with no movies

### Query

```sql
SELECT
    A.Name AS ActorName,
    M.Name AS MovieName
FROM Foundation.Actors A
LEFT JOIN Foundation.Actor_Movies AM
    ON A.Id = AM.ActorId
LEFT JOIN Foundation.Movies M
    ON M.Id = AM.MovieId;
```

### Logic

Start from `Actors` and use `LEFT JOIN`, so every actor remains even when no movie exists.

### Output

| ActorName     | MovieName   |
| ------------- | ----------- |
| Vicky Kaushal | URI         |
| Vicky Kaushal | Article 370 |
| Alia Bhatt    | NULL        |

---

## 5. Movies with producers and actors

### Query

```sql
SELECT
    M.Name AS MovieName,
    P.Name AS ProducerName,
    A.Name AS ActorName
FROM Foundation.Movies M
INNER JOIN Foundation.Producers P
    ON M.ProducerId = P.Id
INNER JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
INNER JOIN Foundation.Actors A
    ON AM.ActorId = A.Id;
```

### Logic

Start from Movies and join its producer and actor mappings.

### Output

| MovieName       | ProducerName      | ActorName      |
| --------------- | ----------------- | -------------- |
| URI             | Aditya Dhar       | Vicky Kaushal  |
| URI             | Aditya Dhar       | Yami Gautam    |
| The Dark Knight | Christopher Nolan | Christian Bale |

---

## 6. Movies with producers and actors, including movies with no actors

### Query

```sql
SELECT
    M.Name AS MovieName,
    P.Name AS ProducerName,
    A.Name AS ActorName
FROM Foundation.Movies M
INNER JOIN Foundation.Producers P
    ON M.ProducerId = P.Id
LEFT JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
LEFT JOIN Foundation.Actors A
    ON AM.ActorId = A.Id;
```

### Logic

Producer is required, but actors are optional. Therefore use `LEFT JOIN` for actors.

### Output

| MovieName           | ProducerName | ActorName     |
| ------------------- | ------------ | ------------- |
| URI                 | Aditya Dhar  | Vicky Kaushal |
| URI                 | Aditya Dhar  | Yami Gautam   |
| Movie Without Actor | Aditya Dhar  | NULL          |

---

## 7. Movies with producers and actors, including movies with no actors

This is the same requirement as Query 6.

### Query

```sql
SELECT
    M.Name AS MovieName,
    P.Name AS ProducerName,
    A.Name AS ActorName
FROM Foundation.Movies M
INNER JOIN Foundation.Producers P
    ON M.ProducerId = P.Id
LEFT JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
LEFT JOIN Foundation.Actors A
    ON AM.ActorId = A.Id;
```

### Logic

`Movies → Producer` is mandatory; `Movies → Actors` is optional.

---

## 8. Movies with no producer information

### Query

```sql
SELECT
    M.Name AS MovieName
FROM Foundation.Movies M
LEFT JOIN Foundation.Producers P
    ON M.ProducerId = P.Id
WHERE P.Id IS NULL;
```

### Logic

Keep all movies, then find those where the producer side has no match.

### Output

| MovieName              |
| ---------------------- |
| Movie Without Producer |

---

## 9. Movies whose plot contains "adventure"

### Query

```sql
SELECT
    Name AS MovieName
FROM Foundation.Movies
WHERE Plot LIKE '%adventure%';
```

### Logic

`%` means any characters before or after the word.

### Output

| MovieName       |
| --------------- |
| Adventure Movie |

---

## 10. Actors sorted by DOB ascending

### Query

```sql
SELECT
    Name AS ActorName,
    DateOfBirth
FROM Foundation.Actors
ORDER BY DateOfBirth ASC;
```

### Logic

Ascending DOB means oldest first.

### Output

| ActorName     | DateOfBirth |
| ------------- | ----------- |
| Paresh Rawal  | 1955-05-30  |
| Aamir Khan    | 1965-03-14  |
| Vicky Kaushal | 1988-05-16  |

---

## 11. Producers sorted alphabetically

### Query

```sql
SELECT
    Name AS ProducerName
FROM Foundation.Producers
ORDER BY Name ASC;
```

### Logic

Sort producer names alphabetically.

---

## 12. Actors sorted by gender and then name

### Query

```sql
SELECT
    Name AS ActorName,
    Sex
FROM Foundation.Actors
ORDER BY
    Sex ASC,
    Name ASC;
```

### Logic

First group/sort by gender, then alphabetically within each gender.

---

## 13. Earliest year of release

### Query

```sql
SELECT
    MIN(YearOfRelease) AS EarliestYear
FROM Foundation.Movies;
```

### Logic

`MIN()` returns the smallest release year.

### Output

| EarliestYear |
| -----------: |
|         2007 |

---

## 14. Number of movies produced by each producer

### Query

```sql
SELECT
    P.Name AS ProducerName,
    COUNT(M.Id) AS MoviesCount
FROM Foundation.Producers P
LEFT JOIN Foundation.Movies M
    ON P.Id = M.ProducerId
GROUP BY
    P.Id,
    P.Name;
```

### Logic

Group movies by producer and count them. `LEFT JOIN` also displays producers with zero movies.

### Output

| ProducerName           | MoviesCount |
| ---------------------- | ----------: |
| Aditya Dhar            |           3 |
| Christopher Nolan      |           3 |
| Rajkumar Hirani        |           3 |
| Mani Ratnam            |           2 |
| Producer Without Movie |           0 |

---

## 15. Producers who produced more than 2 movies

### Query

```sql
SELECT
    P.Name AS ProducerName,
    COUNT(M.Id) AS MoviesCount
FROM Foundation.Producers P
INNER JOIN Foundation.Movies M
    ON P.Id = M.ProducerId
GROUP BY
    P.Id,
    P.Name
HAVING COUNT(M.Id) > 2;
```

### Logic

`GROUP BY` creates one group per producer. `HAVING` filters groups based on their movie count.

### Output

| ProducerName      | MoviesCount |
| ----------------- | ----------: |
| Aditya Dhar       |           3 |
| Christopher Nolan |           3 |
| Rajkumar Hirani   |           3 |

---

## 16. Years having more than 1 movie

### Query

```sql
SELECT
    YearOfRelease,
    COUNT(*) AS MoviesCount
FROM Foundation.Movies
GROUP BY YearOfRelease
HAVING COUNT(*) > 1;
```

### Logic

Group by release year, count movies, then keep years with count > 1.

### Output

| YearOfRelease | MoviesCount |
| ------------: | ----------: |
|          2024 |           2 |
|          2023 |           2 |

---

## 17. Change language of movies released in 2023

### Query

```sql
UPDATE Foundation.Movies
SET Language = 'English'
WHERE YearOfRelease = 2023;
```

### Logic

Update only rows whose release year is 2023.

### Before

| Movie       | Year | Language |
| ----------- | ---: | -------- |
| Oppenheimer | 2023 | English  |

### After

| Movie       | Year | Language |
| ----------- | ---: | -------- |
| Oppenheimer | 2023 | English  |

---

## 18. Delete Movies_Actors entries for Johnny Depp

### Query

```sql
DELETE AM
FROM Foundation.Actor_Movies AM
INNER JOIN Foundation.Actors A
    ON AM.ActorId = A.Id
WHERE A.Name = 'Johnny Depp';
```

### Logic

Join mappings with Actors, identify Johnny Depp, and delete only his mapping rows.

`DELETE AM` means:

> Delete the matching rows from `Actor_Movies` represented by alias `AM`.

---

## 19. Add rating column with default value 5

### Query

```sql
ALTER TABLE Foundation.Movies
ADD Rating INT
    CONSTRAINT DF_Movies_Rating
    DEFAULT 5;
```

### Logic

Adds `Rating`. New rows receive `5` automatically when no rating is supplied.

---

## 20. Movies having at least 3 actors

### Query

```sql
SELECT
    M.Name AS MovieName,
    COUNT(AM.ActorId) AS ActorCount
FROM Foundation.Movies M
INNER JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
GROUP BY
    M.Id,
    M.Name
HAVING COUNT(AM.ActorId) >= 3;
```

### Logic

Group actor mappings by movie and keep groups having at least 3 actors.

### Output

| MovieName       | ActorCount |
| --------------- | ---------: |
| URI             |          3 |
| The Dark Knight |          3 |

---

## 21. Movies, producers and actor count after 2010

### Query

```sql
SELECT
    M.Name AS MovieName,
    P.Name AS ProducerName,
    COUNT(AM.ActorId) AS ActorCount
FROM Foundation.Movies M
INNER JOIN Foundation.Producers P
    ON M.ProducerId = P.Id
LEFT JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
WHERE M.YearOfRelease > 2010
GROUP BY
    M.Id,
    M.Name,
    P.Id,
    P.Name;
```

### Logic

Filter movies after 2010, group by movie and producer, then count actors.

### Output

| MovieName   | ProducerName      | ActorCount |
| ----------- | ----------------- | ---------: |
| URI         | Aditya Dhar       |          3 |
| Article 370 | Aditya Dhar       |          2 |
| Oppenheimer | Christopher Nolan |          2 |

---

## 22. Producer total profit, only producers with more than 1 movie

### Query

```sql
SELECT
    P.Name AS ProducerName,
    SUM(M.Profit) AS TotalProfit
FROM Foundation.Producers P
INNER JOIN Foundation.Movies M
    ON P.Id = M.ProducerId
GROUP BY
    P.Id,
    P.Name
HAVING COUNT(M.Id) > 1;
```

### Logic

Group movies by producer, calculate total profit, and retain producers having more than one movie.

### Output

| ProducerName      | TotalProfit |
| ----------------- | ----------: |
| Aditya Dhar       |        1100 |
| Christopher Nolan |        3100 |
| Rajkumar Hirani   |        1750 |

---

## 23. Movies with producers and actors, including movies with no actors

### Query

```sql
SELECT
    M.Name AS MovieName,
    P.Name AS ProducerName,
    A.Name AS ActorName
FROM Foundation.Movies M
INNER JOIN Foundation.Producers P
    ON M.ProducerId = P.Id
LEFT JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
LEFT JOIN Foundation.Actors A
    ON AM.ActorId = A.Id;
```

### Logic

Use `LEFT JOIN` from Movies to Actor_Movies so movies without actors remain.

---

## 24. Actors, their movies and producers, including actors with no movies

### Query

```sql
SELECT
    A.Name AS ActorName,
    M.Name AS MovieName,
    P.Name AS ProducerName
FROM Foundation.Actors A
LEFT JOIN Foundation.Actor_Movies AM
    ON A.Id = AM.ActorId
LEFT JOIN Foundation.Movies M
    ON M.Id = AM.MovieId
LEFT JOIN Foundation.Producers P
    ON P.Id = M.ProducerId;
```

### Logic

Start with Actors and use `LEFT JOIN` throughout so actors without movies are retained.

### Output

| ActorName     | MovieName   | ProducerName |
| ------------- | ----------- | ------------ |
| Vicky Kaushal | URI         | Aditya Dhar  |
| Vicky Kaushal | Article 370 | Aditya Dhar  |
| Alia Bhatt    | NULL        | NULL         |

---

## 25. Movies, producers and actor count, including movies with no actors

### Query

```sql
SELECT
    M.Name AS MovieName,
    P.Name AS ProducerName,
    COUNT(AM.ActorId) AS ActorCount
FROM Foundation.Movies M
LEFT JOIN Foundation.Producers P
    ON M.ProducerId = P.Id
LEFT JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
GROUP BY
    M.Id,
    M.Name,
    P.Id,
    P.Name;
```

### Logic

Start from Movies. `LEFT JOIN` keeps movies even without producers or actors. `COUNT(AM.ActorId)` returns `0` when there are no actor mappings.

### Output

| MovieName              | ProducerName | ActorCount |
| ---------------------- | ------------ | ---------: |
| URI                    | Aditya Dhar  |          3 |
| Movie Without Actor    | Aditya Dhar  |          0 |
| Movie Without Producer | NULL         |          2 |

---

## 26. Producers who have not produced any movies

### Query

```sql
SELECT
    P.Name AS ProducerName
FROM Foundation.Producers P
LEFT JOIN Foundation.Movies M
    ON P.Id = M.ProducerId
WHERE M.Id IS NULL;
```

### Logic

Start with all producers. A `NULL` movie means no movie was found for that producer.

### Output

| ProducerName           |
| ---------------------- |
| Producer Without Movie |

---

# Advanced

## 27. Top 3 actors who acted in the most movies

### Query

```sql
SELECT TOP 3
    A.Name AS ActorName,
    COUNT(AM.MovieId) AS MoviesCount
FROM Foundation.Actors A
INNER JOIN Foundation.Actor_Movies AM
    ON A.Id = AM.ActorId
GROUP BY
    A.Id,
    A.Name
ORDER BY
    COUNT(AM.MovieId) DESC;
```

### Logic

Group movies by actor, count them, sort descending and take the first 3.

### Output

| ActorName      | MoviesCount |
| -------------- | ----------: |
| Vicky Kaushal  |           3 |
| Yami Gautam    |           3 |
| Cillian Murphy |           3 |

---

## 28. Producers who produced movies in all available languages

### Query

```sql
SELECT
    P.Name AS ProducerName
FROM Foundation.Producers P
INNER JOIN Foundation.Movies M
    ON P.Id = M.ProducerId
GROUP BY
    P.Id,
    P.Name
HAVING COUNT(DISTINCT M.Language) =
       (
           SELECT COUNT(DISTINCT Language)
           FROM Foundation.Movies
       );
```

### Logic

Count the distinct languages produced by each producer and compare it with the total number of available languages.

### Output

| ProducerName                    |
| ------------------------------- |
| Producer covering all languages |

---

## 29. Producers who produced movies in more than 1 language

### Query

```sql
SELECT
    P.Name AS ProducerName,
    COUNT(DISTINCT M.Language) AS LanguageCount
FROM Foundation.Producers P
INNER JOIN Foundation.Movies M
    ON P.Id = M.ProducerId
GROUP BY
    P.Id,
    P.Name
HAVING COUNT(DISTINCT M.Language) > 1;
```

### Logic

Group by producer and count unique languages. Keep producers with more than one.

### Output

| ProducerName    | LanguageCount |
| --------------- | ------------: |
| Rajkumar Hirani |             2 |

---

## 30. Producers with more than 3 unique actors

### Query

```sql
SELECT
    P.Name AS ProducerName,
    COUNT(DISTINCT AM.ActorId) AS UniqueActorCount
FROM Foundation.Producers P
INNER JOIN Foundation.Movies M
    ON P.Id = M.ProducerId
INNER JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
GROUP BY
    P.Id,
    P.Name
HAVING COUNT(DISTINCT AM.ActorId) > 3;
```

### Logic

Producer → Movies → Actor_Movies. Count distinct actors per producer and keep counts greater than 3.

### Output

| ProducerName | UniqueActorCount |
| ------------ | ---------------: |
| Aditya Dhar  |                4 |

---

## 31. Movies with more than 2 actors and profit greater than average 🏷️

### Query

```sql
SELECT
    M.Name AS MovieName,
    M.Profit,
    COUNT(AM.ActorId) AS ActorCount
FROM Foundation.Movies M
INNER JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
GROUP BY
    M.Id,
    M.Name,
    M.Profit
HAVING COUNT(AM.ActorId) > 2
   AND M.Profit >
       (
           SELECT AVG(Profit)
           FROM Foundation.Movies
       );
```

### Logic

* Group actors by movie.
* Keep movies with more than 2 actors.
* Subquery calculates average profit.
* Keep movies above that average.

### Output

| MovieName       | Profit | ActorCount |
| --------------- | -----: | ---------: |
| URI             |    500 |          3 |
| The Dark Knight |   1000 |          3 |

---

## 32. Producers who produced movies released in consecutive years

### Query

```sql
SELECT DISTINCT
    P.Name AS ProducerName
FROM Foundation.Producers P
INNER JOIN Foundation.Movies M1
    ON P.Id = M1.ProducerId
INNER JOIN Foundation.Movies M2
    ON P.Id = M2.ProducerId
    AND M2.YearOfRelease = M1.YearOfRelease + 1;
```

### Logic

Self-join a producer's movies and look for two movies where the second movie was released exactly one year after the first.

### Example

```text
Producer X

2023 → Movie A
2024 → Movie B

2024 = 2023 + 1
```

Therefore the producer qualifies.

### Output

| ProducerName |
| ------------ |
| Aditya Dhar  |

---

## 33. Actors who acted in movies produced by at least 2 different producers

### Query

```sql
SELECT
    A.Name AS ActorName,
    COUNT(DISTINCT M.ProducerId) AS ProducerCount
FROM Foundation.Actors A
INNER JOIN Foundation.Actor_Movies AM
    ON A.Id = AM.ActorId
INNER JOIN Foundation.Movies M
    ON M.Id = AM.MovieId
GROUP BY
    A.Id,
    A.Name
HAVING COUNT(DISTINCT M.ProducerId) >= 2;
```

### Logic

Actor → Actor_Movies → Movies → ProducerId. Count distinct producers for each actor.

### Output

| ActorName     | ProducerCount |
| ------------- | ------------: |
| Vicky Kaushal |             2 |
| Yami Gautam   |             2 |

---

## 34. Producers with highest movie profit, only producers with at least 2 movies

### Query

```sql
SELECT
    P.Name AS ProducerName,
    MAX(M.Profit) AS HighestProfit
FROM Foundation.Producers P
INNER JOIN Foundation.Movies M
    ON P.Id = M.ProducerId
GROUP BY
    P.Id,
    P.Name
HAVING COUNT(M.Id) >= 2;
```

### Logic

Group movies by producer, find the maximum profit within each group, and keep producers with at least 2 movies.

### Output

| ProducerName      | HighestProfit |
| ----------------- | ------------: |
| Aditya Dhar       |           500 |
| Christopher Nolan |          1200 |
| Rajkumar Hirani   |           800 |
| Mani Ratnam       |           700 |

---

# Key Patterns to Remember

## INNER JOIN

```text
Only matching rows
```

Use when the related record **must exist**.

---

## LEFT JOIN

```text
All rows from LEFT table
+
matching rows from RIGHT table
```

Use when the related record is **optional**.

To find missing relationships:

```sql
WHERE RightTable.Id IS NULL
```

Example:

```text
Actors
LEFT JOIN Actor_Movies
WHERE Actor_Movies.ActorId IS NULL
```

→ actors with no movies.

---

## GROUP BY + COUNT

```sql
GROUP BY Actor
COUNT(Movie)
```

→ number of movies per actor.

---

## GROUP BY + SUM

```sql
GROUP BY Producer
SUM(Profit)
```

→ total profit per producer.

---

## GROUP BY + HAVING

```sql
GROUP BY Producer
HAVING COUNT(*) > 2
```

→ filter **groups**, not individual rows.

---

## DISTINCT

```sql
SELECT DISTINCT ProducerId
```

→ remove duplicate result values.

Use `DISTINCT` when you simply need unique results; use `GROUP BY` when you need grouping/aggregation.

---

## STRING_AGG

```sql
STRING_AGG(A.Name, ', ')
```

Combines multiple rows into one comma-separated value **within each group**.

Example:

```text
Vicky
Yami
Paresh

↓

Vicky, Yami, Paresh
```

---

## Self JOIN

A table joins to itself.

Useful for questions such as:

```text
Find actors who worked together
Find producers with consecutive-year movies
```

---

## COUNT(column) vs COUNT(*)

```sql
COUNT(*)
```

Counts rows, including rows containing NULL values.

```sql
COUNT(Column)
```

Counts only **non-NULL values**.

This is especially important with `LEFT JOIN`:

```sql
COUNT(AM.ActorId)
```

can return `0` when no actor mapping exists.

---

## HAVING vs WHERE

```text
WHERE
↓
filters individual rows
↓
GROUP BY
↓
creates groups
↓
HAVING
↓
filters groups
```

Example:

```sql
WHERE YearOfRelease > 2010
```

filters movies.

```sql
HAVING COUNT(*) > 2
```

filters groups.

---

## JOIN vs Subquery

### JOIN

```text
"I need related data from another table."
```

Example:

```text
Movies → Producers
```

### Subquery

```text
"I need the result of another query to help answer this query."
```

Example:

```sql
WHERE Profit >
(
    SELECT AVG(Profit)
    FROM Movies
);
```

