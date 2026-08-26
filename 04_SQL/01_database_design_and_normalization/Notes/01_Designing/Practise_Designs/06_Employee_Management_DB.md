# Employee Management System — Database Design

Design a relational database for an **Employee Management System** that manages an organization's offices, branches, departments, employees, roles, addresses, salaries, leaves, and payrolls.

The organization has multiple offices. Each office can have multiple branches, and each branch has its own address. Addresses contain area, locality, and pincode information, while pincode details such as city, state, and country are maintained separately.

Each branch can operate multiple departments, and a department can operate across multiple branches. Therefore, the relationship between branches and departments must support many-to-many associations.

Each employee belongs to one department. An employee may have multiple personal addresses. An address assigned to an employee must not be shared with another employee.

Employees can hold multiple roles during their employment, and role assignments must maintain the period during which each role was held using `from_date` and `to_date`.

Employees can take multiple leaves, with each leave storing its date and reason.

The system must maintain salary history for employees. An employee can have multiple salary records, with each record storing the salary amount, annual salary, bonus, and validity period.

The organization also maintains payroll records for employees. Each payroll record should identify the employee and the salary record used for that payroll, along with the total payroll amount and report information.

### Required Entities

The database should contain the following entities:

* Offices
* Branches
* Addresses
* PostalAddresses
* Departments
* Branch_Departments
* Employees
* Employee_Addresses
* Roles
* Employee_Roles
* Leaves
* Salaries
* Payrolls

### Important Business Rules

1. One office can have many branches, but each branch belongs to one office.
2. Each branch has one address, and an address can belong to at most one branch.
3. One pincode can be associated with many addresses.
4. One employee can have multiple personal addresses, but one employee address cannot be assigned to multiple employees.
5. One department can have many employees, while each employee belongs to one department.
6. A branch can have many departments, and a department can exist in many branches.
7. An employee can have multiple roles over time, and a role can be assigned to multiple employees.
8. An employee can have multiple leaves.
9. An employee can have multiple salary records to maintain salary history.
10. An employee can have multiple payroll records.
11. Each payroll references the salary record applicable to that payroll.
12. Foreign keys and appropriate UNIQUE constraints must be used to enforce the required relationships and cardinalities.


<br>
<div align = "center">
<img width="700" alt="image" src="https://github.com/user-attachments/assets/1babfa03-f049-4c60-96c2-00866717b16c" />
</div>
<br>


| Table                  | Relationship                                                                                                                                 |
| ---------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- |
| **Offices**            | One office can have **many branches**. `Branches.office_id → Offices.id`                                                                     |
| **Branches**           | Each branch belongs to **one office** and has **one address**. `Branches.address_id → Addresses.id`                                          |
| **Addresses**          | An address can belong to **one branch** or **one employee** depending on usage. `office_id` is no longer needed in this design.              |
| **PostalAddresses**    | One pincode can be associated with **many addresses**. `Addresses.pincode → PostalAddresses.pincode`                                         |
| **Employees**          | Each employee belongs to **one department** and can have **many addresses, leaves, salaries, payrolls, and roles over time**.                |
| **Employee_Addresses** | Junction/association table between employees and addresses. Each address can be assigned to **only one employee** if `address_id` is UNIQUE. |
| **Departments**        | One department can have **many employees**. Departments and branches have an **M:N relationship** through `Branch_Departments`.              |
| **Branch_Departments** | Junction table implementing **Branch M:N Department**.                                                                                       |
| **Roles**              | Roles and employees have an **M:N relationship** through `Employee_Roles`.                                                                   |
| **Employee_Roles**     | Stores which roles an employee has/had, along with `from_date` and `to_date`.                                                                |
| **Leaves**             | One employee can have **many leaves**. `Leaves.emp_id → Employees.id`                                                                        |
| **Salaries**           | One employee can have **many salary records**, allowing salary history. `Salaries.emp_id → Employees.id`                                     |
| **Payrolls**           | One employee can have **many payroll records**. Each payroll references the applicable salary through `salary_id`.                           |
