# SQL Server – Triggers (Quick Notes)

## What is a Trigger?

A **Trigger** is a special type of stored procedure that **automatically executes** when a specified event occurs on a table or view.

**Events:**

* `INSERT`
* `UPDATE`
* `DELETE`

<br>

# Types of Triggers

## 1. DML Trigger (Data Manipulation Language)

Executes automatically when data is inserted, updated, or deleted.

### Types

* **AFTER Trigger** (Default)
* **INSTEAD OF Trigger**

<br>

## 2. DDL Trigger (Data Definition Language)

Executes when database objects are modified.

Examples:

* `CREATE`
* `ALTER`
* `DROP`

<br>

## 3. LOGON Trigger

Executes automatically when a user logs into SQL Server.

Used for:

* Login auditing
* Restricting logins
* Security checks

<br>

# AFTER Trigger

Runs **after** the SQL statement successfully executes.

### Syntax

```sql
CREATE TRIGGER TriggerName
ON TableName
AFTER INSERT | UPDATE | DELETE
AS
BEGIN
    -- Trigger logic
END;
```

### Example

Automatically update `UpdatedAt` whenever an actor is modified.

```sql
CREATE TRIGGER TR_Foundation_Actor_Update
ON Foundation.Actors
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE A
    SET UpdatedAt = GETDATE()
    FROM Foundation.Actors A
    INNER JOIN inserted I
        ON A.Id = I.Id;
END;
```

<br>

# INSTEAD OF Trigger

Runs **instead of** the original `INSERT`, `UPDATE`, or `DELETE` operation.

Useful for:

* Validation
* Preventing deletes
* Making views updatable

### Syntax

```sql
CREATE TRIGGER TriggerName
ON TableName
INSTEAD OF DELETE
AS
BEGIN
    -- Custom logic
END;
```

### Example

Prevent deleting producers.

```sql
CREATE TRIGGER TR_Foundation_Producer_Delete
ON Foundation.Producers
INSTEAD OF DELETE
AS
BEGIN
    PRINT 'Deleting producers is not allowed.';
END;
```

<br>

# DDL Trigger

Executes when database objects are created, altered, or dropped.

### Syntax

```sql
CREATE TRIGGER TriggerName
ON DATABASE
FOR CREATE_TABLE, ALTER_TABLE, DROP_TABLE
AS
BEGIN
    PRINT 'DDL Trigger Fired';
END;
```

<br>

# LOGON Trigger

Executes whenever a user logs into SQL Server.

### Syntax

```sql
CREATE TRIGGER TriggerName
ON ALL SERVER
FOR LOGON
AS
BEGIN
    PRINT 'User Logged In';
END;
```

<br>

# Special Trigger Tables

SQL Server provides two logical tables inside DML triggers.

## inserted

Contains the **new values**.

Used in:

* INSERT
* UPDATE

Example

```sql
SELECT *
FROM inserted;
```

<br>

## deleted

Contains the **old values**.

Used in:

* DELETE
* UPDATE

Example

```sql
SELECT *
FROM deleted;
```

<br>

# Trigger Management

Disable

```sql
DISABLE TRIGGER TR_Foundation_Actor_Update
ON Foundation.Actors;
```

Enable

```sql
ENABLE TRIGGER TR_Foundation_Actor_Update
ON Foundation.Actors;
```

Drop

```sql
DROP TRIGGER TR_Foundation_Actor_Update;
```

<br>

# Best Practices

* Keep trigger logic simple.
* Use `SET NOCOUNT ON`.
* Avoid complex or recursive triggers.
* Prefer constraints when possible; use triggers only when business logic requires them.

<br>

# Quick Revision

| Type                   | Fires On                                | Use                               |
| ---------------------- | --------------------------------------- | --------------------------------- |
| **AFTER Trigger**      | After `INSERT`, `UPDATE`, `DELETE`      | Audit, logging, update timestamps |
| **INSTEAD OF Trigger** | Instead of `INSERT`, `UPDATE`, `DELETE` | Validation, prevent operations    |
| **DDL Trigger**        | `CREATE`, `ALTER`, `DROP`               | Audit schema changes              |
| **LOGON Trigger**      | User login                              | Security and login auditing       |

### Interview Tip

* **AFTER Trigger** → Executes **after** the operation succeeds.
* **INSTEAD OF Trigger** → Replaces the original operation.
* `inserted` contains **new rows**, while `deleted` contains **old rows** inside DML triggers.
