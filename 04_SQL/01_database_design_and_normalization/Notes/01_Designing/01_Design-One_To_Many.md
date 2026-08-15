# One To Many
### Definition
A **one-to-many relationship** occurs when a single record in one table is related to multiple records in another table. For example, one customer can place many orders, but each order belongs to only one customer.

### **7 Steps to Design a One-to-Many Relationship**
The following process helps identify and document these relationships within a database:

1.  **Understand the Definition:** Recognize that tables represent objects (like people or products) and that a one-to-many link allows you to efficiently relate these objects.
2.  **Write a Sentence:** Describe what you want to store in a simple sentence, such as "Store the **cars** and the **showroom** they are located in".
3.  **Identify the Objects:** Extract the **nouns** from your sentence (e.g., "customer" and "orders") to determine what your tables will be.
4.  **Determine the Relationship Type:** Ask two questions: "Does Object A have many Object Bs?" and "Does Object B have many Object As?". 
    *   If **only one** side makes sense (e.g., a showroom has many cars, but a car is only in one showroom), it is a **one-to-many relationship**.
    *   If **both** make sense, it is many-to-many.
5.  **Create the Diagram:** Draw two separate tables in an **Entity Relationship Diagram (ERD)**.
6.  **Draw a Connecting Line:** Place a single line between the two tables to indicate they are related.
7.  **Add Relationship Symbols:** Use **Crow’s Foot notation** to show which side is the "many" side. This is done by adding angled lines (resembling a crow's foot) to the end of the line touching the "many" table.

### **Key Examples**
*   **Customer to Orders:** One customer has many orders.
*   **Showroom to Cars:** One showroom has many cars.
