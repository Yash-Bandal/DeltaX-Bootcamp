# Many to Many
### Definition
A **many-to-many relationship** occurs when multiple records in one table are associated with multiple records in another.

#### 1. What is a Many-to-Many Relationship? (0:30 - 01:27)
A **many-to-many** relationship occurs when multiple records in one table relate to multiple records in another table. 
* **Example:** A *Student* can enroll in many *Classes*, and a single *Class* can have many *Students* enrolled in it.


[Junction Tables](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/SQL/01_database_design_and_normalization/02_Assignment.md#assignment---database-design).

  
<br>
<div align = "center">
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/8f137b64-f78c-417f-bcb0-b1d8402a7134" />
</div>
<br>



#### 2. The Modeling Problem (02:34)
You cannot implement a many-to-many relationship directly using standard relational database foreign keys. If you try to place a foreign key in either table, you would be forced to list multiple values in a single cell, which violates fundamental database normalization rules (specifically, it breaks atomicity).

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/233081dc-93ce-4d5f-8d02-27d7c36210fa" />
</div>


#### 3. The Solution: Associative Tables (02:34 - 03:18)
To resolve this, you must introduce an **associative table** (also commonly known as a **junction**, **link**, or **bridge** table). 
  * This table acts as a mediator between the two main entities.
  * It converts one many-to-many relationship into two separate **one-to-many** relationships.

#### 4. Design and Implementation (03:18 - 05:47)
To design the associative table:
  * Create a new table (e.g., *Student_Class*).
  * Include the **Primary Keys** of the two original tables as **Foreign Keys** in this new table.
  * **Example Structure:** 
      * `Student_ID` (Foreign Key pointing to *Students*)
      * `Class_ID` (Foreign Key pointing to *Classes*)
  * Each row in this table represents a single unique enrollment instance.

<br>
<div align = "center">
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/ef1464d2-1ba6-4c2c-a30b-0d1612ecac58" />
  <br>
  <p>Can Add Its own Primary key, if you may need the entity again later</p>
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/0c83cc9d-c0b7-414f-9244-9c0994fdbfe6" />
  <br/>
  <p>Or add extra column and use it as a independent table</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/747416df-db29-4f4d-964a-28d1eae65a3b" />

</div>
<br>





<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/9cb64465-7045-4d36-9f2c-3da3e4aa7f48" />
</div>
<br>











#### 5. Best Practices for Naming (05:47)
It is standard practice to name the associative table by combining the names of the two tables it links to ensure clarity. 
* Good examples: *Student_Class*, *Enrollment*, or *Course_Registration*.
