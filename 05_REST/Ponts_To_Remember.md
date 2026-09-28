# Points to Remember

<br>

## Index

1. [How to Choose which Controller?](#1-how-to-choose-which-controller)
2. [Choosing Route vs Query Parameter](#2-choosing-route-vs-query-parameter)
3. [Parent, Child & Junction Table](#3-parent-child--junction-table)
4. [Property vs Entity](#4-property-vs-entity)
5. [Using `_async`](#using-_async)


<br>

## Insights
1. Why we use `Scoped` instead of `Singleton` for db backed requests? Like we used singleton for repos where List was used
```
Since we want our application to be stateless, we don't want the server/repository to keep information 
from one HTTP request and use it in another request. Therefore, for a database-backed repository, we prefer Scoped.

Singleton can also give the same result when using SQL Server, but it keeps the same repository object
 alive for the entire application. We don't need that because the database already stores our data.

If we add something like List<Actor> _cache, then we intentionally want to keep some data between requests.
In that case, the cache can be a Singleton because the cache itself needs to share data. But we don't necessarily
 need to make the whole repository Singleton.

Stateless REST does not mean "the server cannot store any data."
It means the server should not depend on previous HTTP request state to understand the current request.
```

<br>

## 1. How to Choose which controller?
Don't ask:
> "What is the first resource in the URL?"

Ask:
> "What resource is the endpoint actually operating on?"\
The Parent resorce just gives the context of the Child Resource

Example 1
```
GET /users/10/orders
What are we getting? ->  Orders.
→ OrdersController
```

<br>

---

<br>


## 2. Choosing Route vs Query Paramter
> Q. Why we use query params
> ```
> GET /api/tasks?status=pending&priority=high&dueDate..
> ```
> Instead of route paparam
> ```
> GET /api/tasks/status/priority/dueDate
> ```

* **Route parameter → identifies a specific resource / mandatory part of the resource**
* **Query parameter → filters, searches, sorts, or optionally modifies the result**

### Example: Tasks

**Specific task — route parameter:**

```http
GET /api/tasks/15
```
`15` is mandatory because it identifies **which task**.


**Filtering tasks — query parameters:**

```http
GET /api/tasks?status=pending&priority=high
```

The filters are optional.. not mandaatory


```http
GET /api/tasks?status=pending
```

```http
GET /api/tasks?priority=high
```
Even if we dont apply filters and simply do ->
```http
GET /api/tasks
```

It will return all tasks, filter just adds on convinience / limit

Now, why not we do
```http
GET /api/tasks/?id=15
```
We **can** do it

There is nothing technically wrong with it.

The difference is mainly about **API semantics and conventions**.\
and using the about syntax does not follow `REST Convention`

### Query parameter is meant for → `filtering a collection`

```http
GET /api/tasks?status=pending
```



### Route parameter is meant for → `identifying one resource`

```http
GET /api/tasks/15
```


### Why prefer `/tasks/15` for ID?

Because an ID usually **identifies a resource**, rather than merely filtering a collection.



<br>

---

<br>



## 3. Parent, Child & Junction Table

```http
POST /v1/carts/{cartId}/items
```

→ **ItemsController**

* We don't create `CartItemsController` just because `CartItems` is a junction table.
* `cartId` gives **parent context**; `items` is the resource being operated on.
* Junction tables are usually **relationship/DB implementation details**.

```text
Cart → CartItems → Item
 ↑                  ↑
Parent           Resource
```
Inside ItemsController, we could have operations such as:\
GetCartItems()\
AddItemToCart()\
RemoveItemFromCart()

> [!Important]

Don't decide the controller based on:

> "Which tables exist in the database?"

Instead ask:

> "What resource is the API operating on?"



<br>

---

<br>

### 4. Property vs Entity

```http
PATCH /v1/orders/{orderId}/status
```
**Controller → `OrdersController`**

Here, we are changing the **status of a specific Order**.

`Status` is a property/part of the Order being operated on.

```text
Order
 ├── Id
 ├── Status        ← being updated
 └── ...
```

Therefore:

```text
PATCH /v1/orders/{orderId}/ status
                    ↓
              OrdersController
```

We don't use:

```http
PATCH /v1/status
```

or create a `StatusController` simply because `Status` exists as a property/enum.

### When would we use `StatusController`?

When **Status itself is a separate entity/resource** that we are managing.

For example, if the database has:

```text
Status
----------------
Id
Name
Description
```

and we need to manage those Status records:

<br>

---

<br>
    

### Using `_async()`

#### Think of a waiter 🍽️
**Without async:**
> Waiter takes your order → stands beside the kitchen for 10 minutes → does nothing → brings food.

**With async:**
> Waiter takes your order → sends it to kitchen → serves another table → kitchen signals when food is ready → waiter comes back.

The food still takes 10 minutes.

`async` just means the waiter isn't standing uselessly beside the kitchen.

That's what "the thread is free" means.

#### Usage
- You don't use async on every method in an ASP.NET Core application.

- You generally use async when the method is performing an asynchronous operation, especially I/O such as `database calls`, `HTTP calls`, or `file operations`.

> CPU-only/simple calculation → synchronous is often fine.

> Waiting for DB/API/file/network → async is usually preferred.


**Q. But isnt await introducing synchronousness, and here we are using combination of async and await, so at the end the thread waits, so isn't it synchronous like fully**
> No. This is the key misconception to clear up:
>
> `await` does not make the thread wait. It makes the method's logical execution wait for the result, while the thread is released.
