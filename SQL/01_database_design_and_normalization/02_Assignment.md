# Assignment - Database Design

## Problem 1:
Design the database for a movie listing site like IMDB.com. The different Entities and their properties are as follows:

The relationships of the different entities are as follows:
- Movies Can have only a single producer.
- Movies can have multiple Actors acting in it.
- An Actor can act in multiple movies
- A Producer can produce multiple movies.



<div align = "center">
<img width="700" alt="image" src="https://github.com/user-attachments/assets/ab731e3e-f356-422c-a0dd-47f2eab9cfe4" />
</div>


## Problem 2
Design an Database for the following problem
Swiggy/Zomato/UberEats

XXXX is an app used for food delivery from various restaurants.\
Restaurants have dishes, multiple restaurants can have the same dishes,\
dishes have their own ratings for every restaurant and every user.\
For Example, Users can give Dish 1 of Restaurant 1 as 5 stars.\
This needs to be preserved. Design a Database for the above scenario.

> [!Note]
> Note that, user is rating a `Dish Served by a specific Restaurant`,
> not just a `Dish`
>
> No need of `users_ratings_restaurant` table, because we already have users -> ratings -> DishRestaurant

<div align = "center">
<img width="700" alt="image" src="https://github.com/user-attachments/assets/07de511e-df46-4a38-8a10-11ba62936814" />
</div>


### Swiggy/Zomato Database Design - Relationship Explanation




## 1. Restaurants → Dishes_Restaurants

### Relationship

```text
Restaurant (1) -------- (∞) Dishes_Restaurants
```

### Meaning

One restaurant can serve many dishes.

### Example

```
Domino's
    ├── Pizza
    ├── Burger
    ├── Garlic Bread
    └── Pasta
```



```text
Dishes_Restaurants
-------------------
Id   DishId   RestaurantId
1      1            1
2      2            1
3      3            1
4      4            1
```


## 2. Dishes → Dishes_Restaurants

### Relationship

```text
Dishes (1) -------- (∞) Dishes_Restaurants
```

### Meaning

One dish can be served by many restaurants.

### Example

```
Pizza

↓

Domino's
Pizza Hut
La Pino'z
Mojo Pizza
```

Database


```text
Dishes_Restaurants

DishId = 1
RestaurantId = 1

DishId = 1
RestaurantId = 2

DishId = 1
RestaurantId = 3
```



## 3. Dishes_Restaurants → Ratings

### Relationship

```text
Dishes_Restaurants (1) -------- (∞) Ratings
```

### Meaning

One Restaurant's Dish (Menu Item) can receive many ratings.

### Example

```
Domino's Pizza
```

can be rated by

```
Rahul

Priya

John

Amit

Sneha
```



```text
Ratings

Id   DishRestaurantId   UserId

1          15              1

2          15              2

3          15              3

4          15              4
```


**Important**

The Rating belongs to

```
Domino's Pizza
```

NOT

```
Pizza
```

NOT

```
Domino's
```



## 4. Users → Ratings

### Relationship

```text
Users (1) -------- (∞) Ratings
```

### Meaning

One User can give many Ratings.

### Example

Rahul gives

```
★★★★★ Domino's Pizza

★★★★ Pizza Hut Pizza

★★ Burger King Burger
```


```text
Ratings

UserId = 7

UserId = 7

UserId = 7
```



## Complete Flow

### Step 1

Restaurant exists

```
Domino's
```



### Step 2

Dish exists

```
Pizza
```



### Step 3

Restaurant serves Pizza

SQL creates

```text
Dishes_Restaurants

Id = 15

RestaurantId = Domino's

DishId = Pizza

Price = $300
```

Read this as

> Domino's now sells Pizza.


### SQL follows the Foreign Keys

```text
Rating
   │
   ▼
DishRestaurant
   │
   ├────────► Restaurant
   │
   └────────► Dish
```

From one foreign key (`DishRestaurantId`), SQL knows

- Which Restaurant
- Which Dish


# Business Sentences (Best Way to Remember)

| Relationship | Read it as |
|--------------|------------|
| Restaurants → Dishes_Restaurants | One Restaurant serves many Menu Items. |
| Dishes → Dishes_Restaurants | One Dish can be served by many Restaurants. |
| Dishes_Restaurants → Ratings | One Restaurant's Dish can receive many Ratings. |
| Users → Ratings | One User can submit many Ratings. |

