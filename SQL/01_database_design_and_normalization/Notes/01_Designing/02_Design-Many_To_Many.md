# Many to Many
### Definition
A **many-to-many relationship** occurs when multiple records in one table are associated with multiple records in another.

<br>
<div align = "center">
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/8f137b64-f78c-417f-bcb0-b1d8402a7134" />
</div>
<br>

### **1. Identification (The Two-Question Test)**
To determine if a relationship is many-to-many, ask two questions:
*   **Question 1:** Does a record in Table A relate to many records in Table B?
*   **Question 2:** Does a record in Table B relate to many records in Table A?
*   If the answer to **both** is "yes," you have a many-to-many relationship.

### **2. The Implementation: Linking Tables**
In SQL, you cannot link two tables directly for a many-to-many relationship because it would violate **First Normal Form (1NF)**, which requires atomic values (no lists of IDs in a single cell) [History: Database Normalisation Notes].

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/233081dc-93ce-4d5f-8d02-27d7c36210fa" />
</div>
<br>



*   **Solution:** Create a third table, known as a **linking table**, **join table**, or **bridge table** [History: Car Listing Tutorial].
*   **Structure:** This table typically contains two main columns: the **Primary Key from Table A** and the **Primary Key from Table B**. Both act as Foreign Keys in this new table [Junction Tables](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/SQL/01_database_design_and_normalization/02_Assignment.md#assignment---database-design).



<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/9cb64465-7045-4d36-9f2c-3da3e4aa7f48" />
</div>
<br>




### **3. Example: Car Listings and Pictures**
Using the car listing application example:
*   **Scenario:** A car listing can have multiple pictures. In some cases, a single generic "stock photo" (like a manufacturer's logo or a standard model image) might be used for many different car listings [History: Car Listing Tutorial].
*   **Linking Table:** Instead of cluttering the `Cars` or `Pictures` tables, you create a `car_pictures` table that only stores `carID` and `pictureID`. This allows for maximum flexibility without data redundancy [History: Car Listing Tutorial].

### **4. Key Benefits**
*   **Data Integrity:** Prevents the need for "repeating groups" (e.g., Picture1, Picture2, Picture3), which is a violation of 1NF [History: Database Normalisation Notes].
*   **Flexibility:** It is easier to update or delete associations without modifying the primary records for the cars or the pictures themselves [History: Car Listing Tutorial].
*   **Scalability:** You can add an infinite number of relationships between the two entities without ever changing the table schema [History: Car Listing Tutorial].
