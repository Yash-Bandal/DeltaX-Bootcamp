# Food Ordering App

<br>

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/1ab7d907-f4fb-41b3-a5fe-065b0d1f5c8e" />
</div>

<br>


# SQL Practice Questions

## Database Design

The database contains:

* `Customers`
* `Restaurants`
* `MenuItems`
* `Restaurant_MenuItems`
* `Orders`
* `OrderItems`
* `Reviews`

<br>


---

<br>


# Level 1 — Simple / Basic

## 5. Customers with no orders

**Question:** Retrieve the names of customers who have not placed any orders.

```sql
SELECT C.name
FROM Customers C
LEFT JOIN Orders O
    ON C.customer_id = O.customer_id
WHERE O.order_id IS NULL;
```

<br>

## 6. Restaurants with no reviews

**Question:** Retrieve the names of restaurants that have not received any reviews.

```sql
SELECT R.name
FROM Restaurants R
LEFT JOIN Reviews RV
    ON R.restaurant_id = RV.restaurant_id
WHERE RV.review_id IS NULL;
```

<br>

## 9. Top 3 restaurants with the most orders

**Question:** Retrieve the top 3 restaurants with the most orders.

```sql
SELECT TOP 3
    R.name,
    COUNT(O.order_id) AS TotalOrders
FROM Restaurants R
INNER JOIN Orders O
    ON R.restaurant_id = O.restaurant_id
GROUP BY R.restaurant_id, R.name
ORDER BY TotalOrders DESC;
```

<br>

## 10. Customers who have left the most reviews

**Question:** Retrieve the names of customers who have left the most reviews.

```sql
SELECT
    C.name,
    COUNT(RV.review_id) AS TotalReviews
FROM Customers C
INNER JOIN Reviews RV
    ON C.customer_id = RV.customer_id
GROUP BY C.customer_id, C.name
ORDER BY TotalReviews DESC;
```

<br>

## 14. Customers who ordered on their joining day

**Question:** Retrieve the names of customers who placed an order on the same day they joined.

```sql
SELECT DISTINCT
    C.name
FROM Customers C
INNER JOIN Orders O
    ON C.customer_id = O.customer_id
WHERE C.join_date = CAST(O.order_date AS DATE);
```

<br>

## 15. Restaurant with the most reviews

**Question:** Retrieve the restaurant with the most reviews.

```sql
SELECT TOP 1
    R.name,
    COUNT(RV.review_id) AS TotalReviews
FROM Restaurants R
INNER JOIN Reviews RV
    ON R.restaurant_id = RV.restaurant_id
GROUP BY R.restaurant_id, R.name
ORDER BY TotalReviews DESC;
```

<br>

## 20. Restaurants with no orders but with reviews

**Question:** Retrieve the names of restaurants that have no orders but have received reviews.

```sql
SELECT DISTINCT
    R.name
FROM Restaurants R
INNER JOIN Reviews RV
    ON R.restaurant_id = RV.restaurant_id
LEFT JOIN Orders O
    ON R.restaurant_id = O.restaurant_id
WHERE O.order_id IS NULL;
```

<br>

## 22. Restaurants with orders but no reviews

**Question:** Retrieve the names of restaurants that have received orders but no reviews.

```sql
SELECT DISTINCT
    R.name
FROM Restaurants R
INNER JOIN Orders O
    ON R.restaurant_id = O.restaurant_id
LEFT JOIN Reviews RV
    ON R.restaurant_id = RV.restaurant_id
WHERE RV.review_id IS NULL;
```

<br>


---

<br>


# Level 2 — Intermediate / Joins + Aggregation

## 1. Top 5 restaurants by revenue

**Question:** Retrieve the top 5 restaurants by total revenue generated from orders.

```sql
SELECT TOP 5
    R.name,
    SUM(O.total_amount) AS TotalRevenue
FROM Restaurants R
INNER JOIN Orders O
    ON R.restaurant_id = O.restaurant_id
GROUP BY R.restaurant_id, R.name
ORDER BY TotalRevenue DESC;
```

<br>

## 2. Top 3 customers by total spending

**Question:** Retrieve the top 3 customers who have spent the most across all restaurants.

```sql
SELECT TOP 3
    C.name,
    SUM(O.total_amount) AS TotalSpent
FROM Customers C
INNER JOIN Orders O
    ON C.customer_id = O.customer_id
GROUP BY C.customer_id, C.name
ORDER BY TotalSpent DESC;
```

<br>

## 3. Top 5 most popular menu items

**Question:** Retrieve the top 5 most popular menu items by quantity ordered across all restaurants.

```sql
SELECT TOP 5
    MI.name,
    SUM(OI.quantity) AS TotalQuantityOrdered
FROM MenuItems MI
INNER JOIN Restaurant_MenuItems RMI
    ON MI.item_id = RMI.item_id
INNER JOIN OrderItems OI
    ON RMI.restaurant_id = OI.restaurant_id
    AND RMI.item_id = OI.item_id
GROUP BY MI.item_id, MI.name
ORDER BY TotalQuantityOrdered DESC;
```

<br>

## 4. Top 3 restaurants by average rating

**Question:** Retrieve the top 3 restaurants with the highest average customer rating.

```sql
SELECT TOP 3
    R.name,
    AVG(RV.rating * 1.0) AS AverageRating
FROM Restaurants R
INNER JOIN Reviews RV
    ON R.restaurant_id = RV.restaurant_id
GROUP BY R.restaurant_id, R.name
ORDER BY AverageRating DESC;
```

<br>

## 7. Most ordered menu item category

**Question:** Retrieve the most ordered menu item category across all restaurants.

```sql
SELECT TOP 1
    MI.category,
    SUM(OI.quantity) AS TotalQuantityOrdered
FROM MenuItems MI
INNER JOIN Restaurant_MenuItems RMI
    ON MI.item_id = RMI.item_id
INNER JOIN OrderItems OI
    ON RMI.restaurant_id = OI.restaurant_id
    AND RMI.item_id = OI.item_id
GROUP BY MI.category
ORDER BY TotalQuantityOrdered DESC;
```

<br>

## 8. Customers ordering from at least 2 restaurants

**Question:** Retrieve the names of customers who have ordered from at least 2 different restaurants.

```sql
SELECT
    C.name,
    COUNT(DISTINCT O.restaurant_id) AS RestaurantsOrderedFrom
FROM Customers C
INNER JOIN Orders O
    ON C.customer_id = O.customer_id
GROUP BY C.customer_id, C.name
HAVING COUNT(DISTINCT O.restaurant_id) >= 2;
```

<br>

## 12. Top 3 restaurants by quantity sold

**Question:** Retrieve the top 3 restaurants with the highest total quantity of items sold.

```sql
SELECT TOP 3
    R.name,
    SUM(OI.quantity) AS TotalQuantitySold
FROM Restaurants R
INNER JOIN OrderItems OI
    ON R.restaurant_id = OI.restaurant_id
GROUP BY R.restaurant_id, R.name
ORDER BY TotalQuantitySold DESC;
```

<br>

## 19. Customers ordering from the same restaurant more than once

**Question:** Retrieve the names of customers who have ordered from the same restaurant more than once.

```sql
SELECT
    C.name,
    O.restaurant_id,
    COUNT(O.order_id) AS OrderCount
FROM Customers C
INNER JOIN Orders O
    ON C.customer_id = O.customer_id
GROUP BY
    C.customer_id,
    C.name,
    O.restaurant_id
HAVING COUNT(O.order_id) > 1;
```

<br>

## 21. Customers ordering from at least 2 restaurants

**Question:** Retrieve the names of customers who have ordered from at least 2 different restaurants.

```sql
SELECT
    C.name
FROM Customers C
INNER JOIN Orders O
    ON C.customer_id = O.customer_id
GROUP BY C.customer_id, C.name
HAVING COUNT(DISTINCT O.restaurant_id) >= 2;
```

> Q21 is intentionally the same concept as Q8.

<br>


---

<br>

# Level 3 — Complex

## 11. Customers who ordered the same menu item more than once

**Question:** Retrieve customers who have ordered the same menu item more than once.

```sql
SELECT
    C.name,
    OI.item_id,
    COUNT(*) AS TimesOrdered
FROM Customers C
INNER JOIN Orders O
    ON C.customer_id = O.customer_id
INNER JOIN OrderItems OI
    ON O.order_id = OI.order_id
GROUP BY
    C.customer_id,
    C.name,
    OI.item_id
HAVING COUNT(*) > 1;
```

If "more than once" means **total quantity > 1**, use:

```sql
HAVING SUM(OI.quantity) > 1;
```

<br>

## 13. Customers who ordered from all restaurants

**Question:** Retrieve the names of customers who have ordered from all restaurants.

```sql
SELECT
    C.name
FROM Customers C
INNER JOIN Orders O
    ON C.customer_id = O.customer_id
GROUP BY C.customer_id, C.name
HAVING COUNT(DISTINCT O.restaurant_id) = (
    SELECT COUNT(*)
    FROM Restaurants
);
```

### Alternative using `NOT EXISTS`

```sql
SELECT C.name
FROM Customers C
WHERE NOT EXISTS
(
    SELECT 1
    FROM Restaurants R
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM Orders O
        WHERE O.customer_id = C.customer_id
          AND O.restaurant_id = R.restaurant_id
    )
);
```

<br>

## 16. Customers who ordered the most items in a single order

**Question:** Retrieve the names of customers who ordered the most items in a single order.

```sql
WITH OrderQuantities AS
(
    SELECT
        O.order_id,
        O.customer_id,
        SUM(OI.quantity) AS TotalItems
    FROM Orders O
    INNER JOIN OrderItems OI
        ON O.order_id = OI.order_id
    GROUP BY O.order_id, O.customer_id
)
SELECT
    C.name,
    OQ.order_id,
    OQ.TotalItems
FROM OrderQuantities OQ
INNER JOIN Customers C
    ON OQ.customer_id = C.customer_id
WHERE OQ.TotalItems = (
    SELECT MAX(TotalItems)
    FROM OrderQuantities
);
```

<br>

## 17. Customers who ordered the most expensive menu item

**Question:** Retrieve the names of customers who ordered the most expensive menu item.

Here, price is stored in `Restaurant_MenuItems`, because the same menu item can have different prices at different restaurants.

```sql
SELECT DISTINCT
    C.name
FROM Customers C
INNER JOIN Orders O
    ON C.customer_id = O.customer_id
INNER JOIN OrderItems OI
    ON O.order_id = OI.order_id
WHERE OI.price = (
    SELECT MAX(price)
    FROM Restaurant_MenuItems
);
```

<br>

## 18. Restaurant with the most orders placed on two consecutive days

**Question:** Retrieve the restaurant with the most orders placed on two consecutive days.

```sql
SELECT TOP 1
    R.name,
    COUNT(*) AS ConsecutiveDayOrders
FROM Orders O1
INNER JOIN Orders O2
    ON O1.restaurant_id = O2.restaurant_id
    AND CAST(O2.order_date AS DATE)
        = DATEADD(DAY, 1, CAST(O1.order_date AS DATE))
INNER JOIN Restaurants R
    ON O1.restaurant_id = R.restaurant_id
GROUP BY R.restaurant_id, R.name
ORDER BY ConsecutiveDayOrders DESC;
```

<br>

# Quick Concept Map

| Query | Main SQL Concept                   |
| <br>-- | <br><br><br><br><br><br><br><br><br><br><br>- |
| 1     | `SUM` + `GROUP BY` + `TOP`         |
| 2     | `SUM` + `GROUP BY`                 |
| 3     | M:N joins + `SUM`                  |
| 4     | `AVG` + `GROUP BY`                 |
| 5     | `LEFT JOIN` + `IS NULL`            |
| 6     | `LEFT JOIN` + `IS NULL`            |
| 7     | `SUM` + `GROUP BY`                 |
| 8     | `COUNT(DISTINCT)` + `HAVING`       |
| 9     | `COUNT` + `GROUP BY`               |
| 10    | `COUNT` + `GROUP BY`               |
| 11    | `GROUP BY` + `HAVING`              |
| 12    | `SUM` + `GROUP BY`                 |
| 13    | Relational division / `NOT EXISTS` |
| 14    | Date comparison                    |
| 15    | `COUNT` + `TOP`                    |
| 16    | Nested aggregation / CTE           |
| 17    | `MAX` + subquery                   |
| 18    | Self-join + date arithmetic        |
| 19    | `GROUP BY` + `HAVING`              |
| 20    | `INNER JOIN` + `LEFT JOIN`         |
| 21    | `COUNT(DISTINCT)` + `HAVING`       |
| 22    | `INNER JOIN` + `LEFT JOIN`         |

### Most important join path for this schema

```text
Customers
    │
    ▼
Orders
    │
    ▼
OrderItems
    │
    ▼
Restaurant_MenuItems
    │              │
    ▼              ▼
Restaurants     MenuItems
```

And for reviews:

```text
Customers
    │
    ▼
Reviews
    │
    ▼
Restaurant_MenuItems
    │              │
    ▼              ▼
Restaurants     MenuItems
```


