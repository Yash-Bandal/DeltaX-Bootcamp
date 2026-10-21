# Patterns

| Question wording / clue              | SQL pattern                             |
| ------------------------------------ | --------------------------------------- |
| At least N                           | `HAVING COUNT(...) >= N`                |
| More than N                          | `HAVING COUNT(...) > N`                 |
| Exactly N                            | `HAVING COUNT(...) = N`                 |
| At most N                            | `HAVING COUNT(...) <= N`                |
| Fewer than N                         | `HAVING COUNT(...) < N`                 |
| How many / total number              | `COUNT(...)`                            |
| Unique / distinct items              | `COUNT(DISTINCT Column)`                |
| For each / per                       | `GROUP BY`                              |
| Share / common items                 | `SELF JOIN` + matching condition        |
| Pairs, avoid duplicate pairs         | `A.Id < B.Id`                           |
| Without / never / none               | `NOT EXISTS` or `LEFT JOIN ... IS NULL` |
| Has at least one matching record     | `EXISTS`                                |
| Highest / lowest                     | `MAX()` / `MIN()` or `ORDER BY`         |
| Top N / latest N                     | `TOP (N)` + `ORDER BY`                  |
| Before / after a date                | `WHERE DateColumn < ...` / `>`          |
| Between a range                      | `BETWEEN ... AND ...`                   |
| Starts with / contains               | `LIKE 'A%'` / `LIKE '%A%'`              |
| Combine conditions                   | `AND` / `OR`                            |
| Include records even without a match | `LEFT JOIN`                             |

Quick rule: `WHERE` filters rows; `HAVING` filters groups after aggregation.

<br>

### Pattern: Find pairs sharing at least N common items

Find pairs of users who have submitted adoption requests for `at least` two different pets in `common`.

Here at least clues to use Having, Common clues to use Group by  
```sql
SELECT 
    U1.Name as User1, 
    U2.Name as User2, 
    Count(Distinct AR1.PetId) as CommonPets
FROM AdoptionRequests AR1 
INNER JOIN AdoptionRequests AR2
    ON AR1.PetId = AR2.PetId
    AND AR1.ApplicantId < AR2.ApplicantId
INNER JOIN Users U1 
    ON AR1.ApplicantId = U1.Id
INNER JOIN Users U2 
    ON AR2.ApplicantId = U2.Id
GROUP BY U1.Id, U1.Name, U2.Id, U2.Name
HAVING Count(distinct AR1.PetId) >= 2;
```

| Wording                | Pattern                                                            |
| ---------------------- | ------------------------------------------------------------------ |
| At least 2 common pets | `HAVING COUNT(DISTINCT PetId) >= 2`                                |
| More than 3 courses    | `HAVING COUNT(*) > 3`                                              |
| Exactly 5 orders       | `HAVING COUNT(*) = 5`                                              |
| No orders              | `HAVING COUNT(*) = 0` (requires an appropriate outer-join pattern) |
| Count per user         | `GROUP BY UserId`                                                  |

Key distinction: `GROUP BY` creates groups; `COUNT` calculates quantities within them; `HAVING` filters those groups based on the quantities.
