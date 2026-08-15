# SQL Assignment 3 - Queries Summary

Database Used: `IMDB_Dummy`

<br>

# 1. Get Age of Actors in Days

### Query

```sql
SELECT
    Name,
    DATEDIFF(DAY, DateOfBirth, GETDATE()) AS AgeInDays
FROM Foundation.Actors;
```

### Logic

- Read actors from `Actors`.
- Calculate the difference between `DateOfBirth` and today's date using `DATEDIFF`.
- Display actor name and age in days.

### Sample Output

| ActorName | AgeInDays |
|------------|----------:|
| Vicky Kaushal | 13970 |
| Yami Gautam | 13773 |
| Paresh Rawal | 25900 |

<br>

# 2. Get Actors who worked with Producer X

### Query

```sql
SELECT DISTINCT
    A.Name AS ActorName
FROM Foundation.Producers P
INNER JOIN Foundation.Movies M
    ON P.Id = M.ProducerId
INNER JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId
INNER JOIN Foundation.Actors A
    ON AM.ActorId = A.Id
WHERE P.Name = 'Aditya Dhar';
```

### Logic

- Find the producer.
- Retrieve movies produced by that producer.
- Find actors associated with those movies.
- Use `DISTINCT` to remove duplicate actor names.

### Join Path

```
Producers
    ↓
Movies
    ↓
Actor_Movies
    ↓
Actors
```

### Sample Output

| ActorName |
|------------|
| Vicky Kaushal |
| Yami Gautam |
| Paresh Rawal |

<br>

# 3. Actors who acted together in Two or More Movies

### Query

```sql
SELECT
    A1.Name AS Actor1,
    A2.Name AS Actor2,
    COUNT(*) AS MoviesTogether
FROM Foundation.Actor_Movies AM1

INNER JOIN Foundation.Actor_Movies AM2
    ON AM1.MovieId = AM2.MovieId
    AND AM1.ActorId < AM2.ActorId

INNER JOIN Foundation.Actors A1
    ON AM1.ActorId = A1.Id

INNER JOIN Foundation.Actors A2
    ON AM2.ActorId = A2.Id

GROUP BY
    A1.Name,
    A2.Name

HAVING COUNT(*) >= 2;
```

### Logic

- Self join `Actor_Movies` to compare actors in the same movie.
- `ActorId < ActorId` removes self-pairs and duplicate pairs.
- Group each actor pair.
- Count movies worked together.
- Keep only pairs appearing in two or more movies.

### Join Path

```
Actor_Movies
      ↓
Actor_Movies (Self Join)
      ↓
Actors
      ↓
Actors
```

### Sample Output

| Actor1 | Actor2 | MoviesTogether |
|---------|---------|---------------:|
| Vicky Kaushal | Yami Gautam | 2 |
| Christian Bale | Cillian Murphy | 2 |

<br>

# 4. Get the Youngest Actor

### Query

```sql
SELECT TOP 1
    Name AS ActorName,
    DateOfBirth
FROM Foundation.Actors
ORDER BY DateOfBirth DESC;
```

### Logic

- Latest date of birth means youngest actor.
- Sort DOB in descending order.
- Return first row.

### Sample Output

| ActorName | DateOfBirth |
|------------|-------------|
| Alia Bhatt | 1993-03-15 |

<br>

# 5. Actors who have Never Worked Together

### Query

```sql
SELECT
    A1.Name,
    A2.Name
FROM Foundation.Actors A1

INNER JOIN Foundation.Actors A2
    ON A1.Id < A2.Id

EXCEPT

SELECT
    A1.Name,
    A2.Name
FROM Foundation.Actor_Movies AM1

INNER JOIN Foundation.Actor_Movies AM2
    ON AM1.MovieId = AM2.MovieId

INNER JOIN Foundation.Actors A1
    ON AM1.ActorId = A1.Id

INNER JOIN Foundation.Actors A2
    ON AM2.ActorId = A2.Id

WHERE AM1.ActorId < AM2.ActorId;
```

### Logic

```
All Possible Actor Pairs

        -

Pairs who worked together

        =

Pairs who never worked together
```

### Sample Output

| Actor1 | Actor2 |
|---------|---------|
| Yami Gautam | Christian Bale |
| Paresh Rawal | Tom Hardy |

<br>

# 6. Number of Movies in Each Language

### Query

```sql
SELECT
    Language,
    COUNT(*) AS MovieCount
FROM Foundation.Movies
GROUP BY Language;
```

### Logic

- Group movies by language.
- Count movies in each language.

### Sample Output

| Language | MovieCount |
|-----------|-----------:|
| Hindi | 5 |
| English | 3 |
| Tamil | 2 |
| Marathi | 1 |

<br>

# 7. Total Profit of Movies in Each Language

### Query

```sql
SELECT
    Language,
    ISNULL(SUM(Profit),0) AS TotalProfit
FROM Foundation.Movies
GROUP BY Language;
```

### Logic

- Group movies by language.
- Add profits using `SUM`.
- Replace NULL with 0 using `ISNULL`.

### Sample Output

| Language | TotalProfit |
|-----------|------------:|
| Hindi | 2550 |
| English | 3100 |
| Tamil | 1100 |
| Marathi | 300 |

<br>

# 8. Total Profit of Movies having Actor X in Each Language

### Query

```sql
SELECT
    M.Language,
    ISNULL(SUM(M.Profit),0) AS TotalProfit
FROM Foundation.Movies M

INNER JOIN Foundation.Actor_Movies AM
    ON M.Id = AM.MovieId

INNER JOIN Foundation.Actors A
    ON AM.ActorId = A.Id

WHERE A.Name = 'Yami Gautam'

GROUP BY M.Language;
```

### Logic

- Find Actor X.
- Get movies acted by that actor.
- Retrieve movie language and profit.
- Group by language.
- Sum profits.

### Join Path

```
Actors
    ↓
Actor_Movies
    ↓
Movies
```

### Sample Output

| Language | TotalProfit |
|-----------|------------:|
| Hindi | 750 |
| Marathi | 300 |

<br>

# 9. Total Profit by Year of Release and Language

### Query

```sql
SELECT
    YearOfRelease,
    Language,
    ISNULL(SUM(Profit),0) AS TotalProfit
FROM Foundation.Movies
GROUP BY
    YearOfRelease,
    Language;
```

### Logic

- Group movies by release year and language.
- Sum profits for each combination.

### Sample Output

| Year | Language | TotalProfit |
|------:|-----------|------------:|
| 2008 | English | 1000 |
| 2019 | Hindi | 500 |
| 2022 | Tamil | 700 |
| 2023 | English | 1200 |

<br>

# 10. Number of Movies in Each Language Produced by Each Producer

### Query

```sql
SELECT
    P.Name AS ProducerName,
    M.Language,
    COUNT(M.Id) AS MovieCount
FROM Foundation.Movies M

INNER JOIN Foundation.Producers P
    ON M.ProducerId = P.Id

GROUP BY
    P.Name,
    M.Language;
```

### Logic

- Join Movies with Producers.
- Group by Producer and Language.
- Count movies for each producer-language combination.

### Join Path

```
Movies
    ↓
Producers
```

### Sample Output

| Producer | Language | MovieCount |
|-----------|-----------|-----------:|
| Aditya Dhar | Hindi | 3 |
| Christopher Nolan | English | 3 |
| Rajkumar Hirani | Hindi | 2 |
| Rajkumar Hirani | Marathi | 1 |
| Mani Ratnam | Tamil | 2 |
