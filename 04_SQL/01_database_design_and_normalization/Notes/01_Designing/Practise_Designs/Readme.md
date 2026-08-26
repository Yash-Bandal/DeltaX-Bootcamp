###  Practise More
1. Spotify
2. Pet adoption system - extra- services for pets, client/user/pet owner booking the services

<br>

### Check Prompt
```
Is the shared design Ok, check all relations, anomalies, redundancies, consistency
and appropriate constraint usage
```

<br>


### Rules
Yep. Based on the patterns we've been discussing, here are the **general DB design observations** I'd keep as rules rather than tying them to one particular schema.

### General relationship observations

1. **1 : N → FK goes on the N side**

   * `Office 1 → N Branches`
   * `Branches.office_id → Offices.id`

2. **1 : 1 → FK goes on the dependent side**

   * Make the FK `UNIQUE` to enforce 1:1.
   * Example: `Branch.address_id UNIQUE`.

3. **M : N → create a junction table**

   * `Employee M ↔ N Role`
   * `Employee_Roles(employee_id, role_id)`

4. **Don't create a junction table for every relationship**

   * Use it primarily for M:N.
   * For 1:N, put the FK directly in the N-side table.

5. **For 1:1, don't blindly create a separate relationship table**

   * Usually an FK + `UNIQUE` is enough.
   * A separate table is useful if the relationship itself has many attributes or needs independent existence.

6. **A foreign key represents a relationship, not merely a connection between two tables.** 🏷️

   * Before adding an FK, ask: *What business relationship does this represent?*

7. **Junction table represents a relationship between its two parent entities.** 🏷️
   - Don't treat the junction table itself as the entity being liked/owned/etc.
---

### Entity vs relationship

7. **If something has its own attributes and independent meaning, it is usually an entity/table.**

   * Example: `Address`, `Employee`, `Department`.

8. **If something only represents the association between two entities, it is usually a junction/relationship table.**

   * Example: `Employee_Roles`.

9. **Don't duplicate a relationship just because you can reach the same entity through another table.** 🏷️

   * If `Payroll → Salary → Employee`, adding `employee_id` directly to Payroll may create redundant/inconsistent data.

10. **Ask whether an attribute belongs to an entity or to the relationship.**

* If the amount varies for each `Payroll ↔ SalaryComponent` association, `amount` belongs in `Payroll_Salaries`, not `Salaries`.

---

### Cardinality / constraints

11. **Cardinality should be enforced with constraints where possible.**

* 1:1 → `UNIQUE(FK)`
* 1:N → normal FK
* M:N → junction table.

12. **A composite `UNIQUE(A, B)` means the combination is unique, not each column individually.**

13. **If a value itself must never repeat, make that column individually `UNIQUE`.**

14. **Nullable FK usually means the relationship is optional.**

* `manager_id NULL` → employee may have no manager.

15. **Don't assume every FK implies mandatory participation.**

* `NOT NULL` determines whether the relationship is mandatory from that table's side.

---

### Avoiding redundancy

16. **Store a fact in one appropriate place whenever possible.** 

17. **Don't store values that can be reliably derived through relationships unless there's a deliberate reason.**

* Example: storing `department_id` in a table when it can already be determined through another FK.

18. **Redundant FKs can create contradictory data.**

* Example:

```text
Payroll.employee_id = 5
Payroll.salary_id → Salary.employee_id = 7
```

Now the database is inconsistent.

19. **Before adding an FK, ask:**

> "Can I already determine this entity through another relationship?"

20. **Normalization helps eliminate update/insert/delete anomalies, but don't blindly normalize away meaningful business data.**

---

### History / time-based data

21. **If something changes over time and historical values matter, don't overwrite the old record.**

* Use a history/version table or effective dates.

22. **If a table represents current state only, don't add history columns just because they might be useful.**

23. **`from_date` / `to_date` usually indicate that a relationship or value is valid for a period.**

* This is especially useful for roles, assignments, pricing, employment status, etc.

---

### A very useful decision process

When designing any relationship, ask these **4 questions**:

```text
1. What is the cardinality?
   1:1 / 1:N / M:N

2. Where is the FK?
   N side / dependent side / junction table

3. Can the relationship repeat?
   If no → UNIQUE constraint may be needed

4. Am I storing the same fact twice?
   If yes → reconsider the design
```

That little checklist will catch a **surprisingly large number of DB design mistakes**.
