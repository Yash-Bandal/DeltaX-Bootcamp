# Assignment - Database Design

## Problem 1:
Design the database for a movie listing site like IMDB.com. The different Entities and their properties are as follows:

The relationships of the different entities are as follows:
- Movies Can have only a single producer.
- Movies can have multiple Actors acting in it.
- An Actor can act in multiple movies
- A Producer can produce multiple movies.

<div align = "center">
<img width="700"  alt="IMDB-Database-Design" src="https://github.com/user-attachments/assets/ec998201-9f09-40c5-9ca6-c7ba6596b898" />
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
