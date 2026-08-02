# Designing an ER Diagram (Banking System)

This lecture provides a systematic, three-step methodology for designing an Entity-Relationship (ER) Diagram. The instructor uses a Banking System as a practical, end-to-end case study to demonstrate how to translate real-world requirements into a structured database model.

### 1. The 3-Step Design Process
*   **Step 1: Identify Entity Sets:** Define the core objects or 'things' in the system that exist independently. (e.g., Branch, Customer, Account, Employee, Loan).
*   **Step 2: Identify Attributes and Types:** Define the properties of these entities. Categorize them as:
    *   **Single-valued:** Attributes with one value per entity.
    *   **Multi-valued:** Attributes that can hold multiple values (e.g., multiple contact numbers for a customer, shown as a double oval).
    *   **Derived:** Attributes calculated from others (e.g., age from date of birth, shown as a dashed oval).
    *   **Primary Key:** The unique identifier for each entity (underlined).
*   **Step 3: Identify Relationships and Constraints:** Define how entities interact. Establish:
    *   **Mapping Cardinality:** Relationships such as 1:1, 1:N, or M:N.
    *   **Participation Constraints:** Determine if participation is **Total** (every instance must participate) or **Partial**.

### 2. Case Study: Banking System Implementation
*   **Requirement Engineering:** Before drawing, collect stakeholder needs. The banking system requires tracking:
    *   **Branches:** Identified by a unique branch name/ID.
    *   **Customers:** Interacting with accounts and loans.
    *   **Accounts:** Divided into *Savings* and *Current* types.
    *   **Loans:** Originated by branches and managed by specific employees.
*   **Generalization/Specialization:** Instead of keeping *Savings* and *Current* accounts as separate entities, generalize them into a single `Account` entity to reduce redundancy. Specific attributes (like interest rate for savings or withdrawal limits for current) can then be managed through sub-classing.
*   **Relational Logic:**
    *   **Customer-Loan:** A M:N relationship (one customer can take multiple loans; one loan can involve multiple customers).
    *   **Branch-Loan:** A 1:N relationship (a loan is originated by one specific branch).
    *   **Manager-Customer:** An employee entity (manager) is linked to a customer for service and loan oversight.


 
### Entities and attributes

| **Entity**                | **Attributes**                                                                                              |
| ------------------------- | ----------------------------------------------------------------------------------------------------------- |
| **Branch**                | **Branch_Name (PK)**, City, Assets, Liabilities                                                             |
| **Customer**              | **Cust_ID (PK)**, Name, Address *(Composite)*, Contact_No *(Multivalued)*, DOB, Age *(Derived)*             |
| **Employee**              | **Emp_ID (PK)**, Name, Contact_No, Dependent_Name *(Multivalued)*, Start_Date, Years_of_Service *(Derived - from current date and start date)* |
| **Savings Account**       | **Acc_No**, Balance, Interest_Rate, Daily_Withdrawal_Limit                                                  |
| **Current Account**       | **Acc_No**, Balance, Per_Transaction_Charges, Overdraft_Amount                                              |
| **Account (Generalized)** | **Acc_No**, Balance                                                                                         |
| **Loan**                  | **Loan_No**, Amount                                                                                         |
| **Payment (Weak Entity)** | Payment_No, Date, Amount                                                                                    |

Attribute Types
| **Type**         | **Examples**                                       |
| ---------------- | -------------------------------------------------- |
| **Composite**    | Address                                            |
| **Derived**      | Age (from DOB), Years_of_Service (from Start_Date) |
| **Multivalued**  | Contact_No, Dependent_Name                         |
| **Primary Keys** | Branch_Name, Cust_ID, Emp_ID, Acc_No, Loan_No      |


Relations
| # | Relationship                                           | Cardinality                               |
| - | ------------------------------------------------------ | ----------------------------------------- |
| 1 | **Customer** — *Borrows* — **Loan**                    | **M : N** (Many-to-Many)                  |
| 2 | **Loan** — *Originated by* — **Branch**                | **N : 1** (Many Loans → One Branch)       |
| 3 | **Loan** — *Loan Payment* — **Payment**                | **1 : N** (One Loan → Many Payments)      |
| 4 | **Customer** — *Deposits* — **Account**                | **M : N** (Many-to-Many)                  |
| 5 | **Customer** — *Banker* — **Employee**                 | **N : 1** (Many Customers → One Employee) |
| 6 | **Employee** — *Managed by* — **Employee** (Recursive) | **N : 1** (Many Employees → One Manager)  |



### 3. Final ER Considerations & Best Practices
*   **Notation Standards:** Use standard ER symbols—rectangles for entities, diamonds for relationships, and circles for attributes.
*   **Handling Weak Entities:** Recognize that some entities cannot be identified solely by their own attributes and depend on another 'identifying' entity (e.g., a specific payment entity linked to a loan).
*   **Verification:** Always ensure that relationships are binary (between two entities) or n-ary where appropriate, and check that all primary keys are correctly defined.
*   **Practice:** To master this, apply the same 3-step logic to other systems such as an *Online Food Delivery System* (entities: Customer, Order, Delivery Partner, Restaurant) or a *University Management System* (entities: Student, Course, Department, Professor).

<br>

<div align = "center">
<img width="700" alt="image" src="https://github.com/user-attachments/assets/9eaee308-71ae-466a-9200-6fa502aba6ab" />
</div>

<br>

| Parent Entity          | Child Entity           | Relationship              | Reason                                                                                                                                                               |
| ---------------------- | ---------------------- | ------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Address**            | **Branch**             | **1 : N**                 | One address can contain multiple branches (same building/business park), each branch has one address. *(If every branch has a unique address, make it 1:1 instead.)* |
| **Address**            | **Customer_Address**   | **1 : N**                 | One address can be linked to many customers.                                                                                                                         |
| **Customer**           | **Customer_Address**   | **1 : N**                 | A customer can have Home, Office, Temporary addresses.                                                                                                               |
| **Customer**           | **Customer_Contact**   | **1 : N**                 | Multiple phone numbers per customer.                                                                                                                                 |
| **Employee**           | **Employee_Dependent** | **1 : N**                 | One employee can have many dependents.                                                                                                                               |
| **Employee (Manager)** | **Employee**           | **1 : N**                 | One manager manages many employees (self-referencing FK).                                                                                                            |
| **Employee**           | **Customer**           | **1 : N**                 | One banker handles many customers. Customer belongs to one banker.                                                                                                   |
| **Branch**             | **Loan**               | **1 : N**                 | One branch originates many loans.                                                                                                                                    |
| **Loan**               | **Loan_Payment**       | **1 : N**                 | One loan has many payments.                                                                                                                                          |
| **Customer**           | **Customer_Loan**      | **1 : N**                 | Junction table.                                                                                                                                                      |
| **Loan**               | **Customer_Loan**      | **1 : N**                 | Junction table. Together forms Customer ↔ Loan = M:N.                                                                                                                |
| **Customer**           | **Customer_Account**   | **1 : N**                 | Junction table.                                                                                                                                                      |
| **Account**            | **Customer_Account**   | **1 : N**                 | Junction table. Together forms Customer ↔ Account = M:N (joint accounts).                                                                                            |
| **Account_Type**       | **Account**            | **1 : N** *(recommended)* | Each account belongs to one type (Savings/Current). Many accounts can be Savings.                                                                                    |
| **Account**            | **Savings_Account**    | **1 : 1**                 | Only if using table inheritance.                                                                                                                                     |
| **Account**            | **Current_Account**    | **1 : 1**                 | Only if using table inheritance.                                                                                                                                     |

