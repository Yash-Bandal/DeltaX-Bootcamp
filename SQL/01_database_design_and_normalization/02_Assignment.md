# Assignment - Database Design

## Problem 1:
Design the database for a movie listing site like IMDB.com. The different Entities and their properties are as follows:

The relationships of the different entities are as follows:
- Movies Can have only a single producer.
- Movies can have multiple Actors acting in it.
- An Actor can act in multiple movies
- A Producer can produce multiple movies.



<div align = "center">
<img width="650" alt="image" src="https://github.com/user-attachments/assets/ab731e3e-f356-422c-a0dd-47f2eab9cfe4" />
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
<img width="650" alt="image" src="https://github.com/user-attachments/assets/07de511e-df46-4a38-8a10-11ba62936814" />
</div>


## Swiggy/Zomato Database Design - Relationship Explanation


### Note this

| Relationship | Read it as |
|--------------|------------|
| Restaurants → Dishes_Restaurants | One Restaurant serves many Menu Items. |
| Dishes → Dishes_Restaurants | One Dish can be served by many Restaurants. |
| Dishes_Restaurants → Ratings | One Restaurant's Dish can receive many Ratings. |
| Users → Ratings | One User can submit many Ratings. |



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

<br>

> [!Important]
> **Listing Tables** are not just to break **many-to-many** relationship, but also to implement **one-to-many** relationship, consider below case

<br>


###  Now 
We have a requirement, that 

1. One restaurant can have many addresses,
2. One user can have many address - Home , office

See, `Users`, and `Restaurants` both have **Adress** column, but  we want a seperate `Address` entity, that will have its seperate cols, 

> [!caution]
> Dont create a `AddressId` in `Restaurants` and then map to multiple `id` of `Address` table
> 
> it looks like `Addressid = [1,2,3,4]` this breaks normalization, 1Nf automicity constraint ❌
> <div align = "center">
> <img width="359" height="104" alt="image" src="https://github.com/user-attachments/assets/94bcb28b-4b57-4b1b-a3ff-83d05eccb3ce" />
></div>

### Now
(we could have done that, directly map `RestaurantId` fkey inside Address to primary key\
Id of Resto, but then we would not be able to use `Address` for users, so follow below design)
<div align = "center">
<img width="372" height="116" alt="image" src="https://github.com/user-attachments/assets/82fcb7e4-1638-4098-9a63-4b9b59a0dfec" />
</div>

### Thus
**Solution:**
1. Create a seperate Address entity
2. Create seperate junction tables,

This ensures, 1 resto -> many addresses, 

<div align = "center">
    <img width="650" alt="image" src="https://github.com/user-attachments/assets/19c18aca-173f-4ca0-983e-51731b915ff3" />
</div>
