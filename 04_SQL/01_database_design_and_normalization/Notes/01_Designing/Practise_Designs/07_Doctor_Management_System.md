# Doctor Management System - Database Design

<br>
<div align = "center">
<img width="700" alt="image" src="https://github.com/user-attachments/assets/88c2b243-2fab-466a-a0e5-e9f3336fef70" />
</div>
<br>

### Junction Table — Points to Remember

1. **Ask: "What does this attribute describe?"**

   * Entity → put in entity table.
   * Relationship → put in junction table.

2. **Relationship-specific data → junction table**

   * `Prescription ↔ Medicine` → dosage, frequency, duration.
   * `Patient ↔ Room` → arrival, discharge.

3. **Don't put changing/transaction data in the entity**

   * `Medicine.quantity` ❌ → `Inventory.quantity` ✅
   * `Patient.age` ❌ → `Patient.date_of_birth` ✅

4. **Junction table is not only for M:N**

   * Use it for 1:1 / 1:N too **if the relationship itself needs data or history**.

5. **Think of the junction table as a sentence:**

   > "Patient X stayed in Room Y **from A to B**."

6. **Normalization shortcut:**

   > If the value depends on **both entities together**, it belongs in the junction/association table.

**Your main mistake:** you were putting relationship attributes (`dosage`, `arrival/discharge`) inside the entity tables\
instead of asking **who/what the fact belongs to**.
