# Rapido Database Design & SQL Practice

<br>
<div align = "center">
   <img width="574" height="340" alt="image" src="https://github.com/user-attachments/assets/1df8a3de-2f6d-4085-b5ca-9fed824beb1c" />
</div>
<br>

# Database Design

## Users

| Column | Description |
|---------|-------------|
| ID | Primary Key |
| Name | User Name |
| Email | Email Address |
| Phone_Number | Contact Number |
| Created_At | Account Creation Time |

<br>

## Riders

| Column | Description |
|---------|-------------|
| ID | Primary Key |
| Name | Rider Name |
| Phone_Number | Contact Number |
| Vehicle_Number | Vehicle Registration Number |
| Vehicle_Type | Bike, Auto, Cab, etc. |
| Created_At | Registration Time |

<br>

## Rides

| Column | Description |
|---------|-------------|
| ID | Primary Key |
| User_ID | FK → Users.ID |
| Rider_ID | FK → Riders.ID |
| Pickup_Location | Pickup Address |
| Drop_Location | Destination |
| Fare | Ride Fare |
| Status | Requested, Accepted, Started, Completed, Cancelled |
| Requested_At | Ride Request Time |
| Started_At | Ride Start Time |
| Completed_At | Ride End Time |

<br>

## Ratings

| Column | Description |
|---------|-------------|
| ID | Primary Key |
| Ride_ID | FK → Rides.ID |
| From_User_ID | User who gave rating |
| To_User_ID | User/Rider who received rating |
| Rating_Value | Rating (1-5) |
| Review | Optional Review |
| Created_At | Rating Time |

<br>

# Normalized User Design (Avoid Update Anomaly)

## Users

| Column |
|---------|
| ID |
| Name |
| Category_ID |

<br>

## Category

| Column |
|---------|
| ID |
| Name |

Example

| ID | Name |
|---------|---------|
| 1 | Customer |
| 2 | Rider |

<br>

## Customers

| Column |
|---------|
| ID |
| User_ID |
| Pickup_Location |

<br>

## Riders

| Column |
|---------|
| ID |
| User_ID |
| Vehicle_Number |

<br>

## Ratings

| Column |
|---------|
| ID |
| Ride_ID |
| From_User_ID |
| To_User_ID |

<br>

# Relationships

```
Users (1)
   │
   ├──────────────┐
   │              │
   ▼              ▼
Customers       Rides
                   ▲
                   │
                   │
                Riders
                   │
                   ▼
                Ratings
```

<br>

# SQL Practice Queries

<br>

## 1. Total Rides Per User

### Query

```sql
SELECT
    u.ID,
    u.Name,
    COUNT(r.ID) AS Total_Rides
FROM Users u
LEFT JOIN Rides r
    ON u.ID = r.User_ID
GROUP BY
    u.ID,
    u.Name;
```

### Logic

- LEFT JOIN keeps users even if they never booked.
- COUNT(r.ID) counts rides.
- GROUP BY creates one row per user.

<br>

## 2. Top 5 Riders by Completed Rides

### Query

```sql
SELECT TOP 5
    Rider_ID,
    COUNT(*) AS Total_Rides
FROM Rides
WHERE Status = 'Completed'
GROUP BY Rider_ID
ORDER BY Total_Rides DESC;
```

### Logic

- Consider only completed rides.
- Count rides for every rider.
- Sort descending.
- Return top five.

<br>

## 3. Average Rating Received by Each Rider

### Query

```sql
SELECT
    r.ID,
    r.Name,
    AVG(rt.Rating_Value) AS Avg_Rating
FROM Riders r
JOIN Ratings rt
    ON r.ID = rt.To_User_ID
GROUP BY
    r.ID,
    r.Name;
```

### Logic

- Join riders with received ratings.
- Average all ratings.
- One row per rider.

<br>

## 4. Total Earnings Per Rider

### Query

```sql
SELECT
    Rider_ID,
    SUM(Fare) AS Total_Earnings
FROM Rides
WHERE Status = 'Completed'
GROUP BY Rider_ID;
```

### Logic

- Completed rides generate earnings.
- SUM adds fares.
- GROUP BY rider.

<br>

## 5. Users Who Never Booked a Ride

### Query

```sql
SELECT
    u.ID,
    u.Name
FROM Users u
LEFT JOIN Rides r
    ON u.ID = r.User_ID
WHERE r.ID IS NULL;
```

### Logic

- LEFT JOIN keeps every user.
- Users without matching rides have NULL.
- Filter NULL values.

<br>

## 6. Most Frequent Pickup Location

### Query

```sql
SELECT TOP 1
    Pickup_Location,
    COUNT(*) AS Ride_Count
FROM Rides
GROUP BY Pickup_Location
ORDER BY Ride_Count DESC;
```

### Logic

- Count rides for every pickup location.
- Highest count is the most popular.

<br>

## 7. Ride Duration

### Query

```sql
SELECT
    ID,
    DATEDIFF(MINUTE, Started_At, Completed_At) AS Duration_Minutes
FROM Rides
WHERE Status = 'Completed';
```

### Logic

- DATEDIFF calculates minutes between start and end.
- Only completed rides have both timestamps.

<br>

## 8. Riders Having Average Rating Below 3

### Query

```sql
SELECT
    r.ID,
    r.Name,
    AVG(rt.Rating_Value) AS Avg_Rating
FROM Riders r
JOIN Ratings rt
    ON r.ID = rt.To_User_ID
GROUP BY
    r.ID,
    r.Name
HAVING AVG(rt.Rating_Value) < 3;
```

### Logic

- GROUP BY rider.
- HAVING filters grouped data.
- Return riders with average rating less than 3.

<br>

## 9. Peak Booking Hour

### Query

```sql
SELECT TOP 1
    DATEPART(HOUR, Requested_At) AS Booking_Hour,
    COUNT(*) AS Total_Rides
FROM Rides
GROUP BY
    DATEPART(HOUR, Requested_At)
ORDER BY Total_Rides DESC;
```

### Logic

- Extract hour from booking time.
- Count bookings in each hour.
- Highest count = peak booking hour.

<br>

## 10. User-Rider Pairs Having 3 or More Completed Rides Together

### Query

```sql
SELECT
    User_ID,
    Rider_ID,
    COUNT(*) AS Ride_Count
FROM Rides
WHERE Status = 'Completed'
GROUP BY
    User_ID,
    Rider_ID
HAVING COUNT(*) >= 3;
```

### Logic

- Group by both user and rider.
- Count rides between the pair.
- Keep only pairs having at least three completed rides.

<br>

# SQL Concepts Covered

- INNER JOIN
- LEFT JOIN
- GROUP BY
- HAVING
- Aggregate Functions
  - COUNT()
  - AVG()
  - SUM()
- DATEPART()
- DATEDIFF()
- ORDER BY
- TOP
- NULL Handling
- Foreign Keys
- One-to-Many Relationships
- Basic Database Normalization
