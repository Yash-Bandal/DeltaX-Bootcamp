# SQL Server Constraint Naming Conventions

## Primary Key (PK)

**Convention**

```text
PK_<Schema>_<Table>
```

**Examples**

```text
PK_Foundation_Actor
PK_Foundation_Movie
PK_Foundation_Producer
PK_Foundation_ActorMovie
```

---

## Foreign Key (FK)

**Convention**

```text
FK_<Schema>_<ChildTable>_<ParentTable>
```

**Examples**

```text
FK_Foundation_Movie_Producer
FK_Foundation_ActorMovie_Actor
FK_Foundation_ActorMovie_Movie
```

---

## Default Constraint (DF)

**Convention**

```text
DF_<Schema>_<Table>_<Column>
```

**Examples**

```text
DF_Foundation_Actor_CreatedAt
DF_Foundation_Actor_UpdatedAt

DF_Foundation_Movie_CreatedAt
DF_Foundation_Movie_UpdatedAt

DF_Foundation_Producer_CreatedAt
DF_Foundation_Producer_UpdatedAt
```

---

## Check Constraint (CK)

**Convention**

```text
CK_<Schema>_<Table>_<Column>
```

**Examples**

```text
CK_Foundation_Actor_Sex
CK_Foundation_Movie_Profit
CK_Foundation_Movie_YearOfRelease
```

---

## Unique Constraint (UQ)

**Convention**

```text
UQ_<Schema>_<Table>_<Column>
```

**Examples**

```text
UQ_Foundation_Actor_Name
UQ_Foundation_Producer_Name
UQ_Foundation_Movie_Name
```

For composite unique constraints:

```text
UQ_Foundation_ActorMovie_ActorId_MovieId
```

---

## Index (IX)

> While not a constraint, indexes typically follow the same naming convention.

**Convention**

```text
IX_<Schema>_<Table>_<Column(s)>
```

**Examples**

```text
IX_Foundation_Actor_Name
IX_Foundation_Movie_ProducerId
IX_Foundation_Movie_YearOfRelease
IX_Foundation_ActorMovie_ActorId_MovieId
```

---

## Sequence (SQ)

**Convention**

```text
SQ_<Schema>_<Name>
```

**Examples**

```text
SQ_Foundation_InvoiceNumber
SQ_Foundation_OrderNumber
```

---

## Trigger (TR)

> Triggers are database objects, not constraints, but commonly follow a consistent naming convention.

**Convention**

```text
TR_<Schema>_<Table>_<Action>
```

**Examples**

```text
TR_Foundation_Actor_Insert
TR_Foundation_Actor_Update
TR_Foundation_Movie_Delete
```

---

# Summary

| Object      | Prefix | Convention                               | Example                          |
| ----------- | ------ | ---------------------------------------- | -------------------------------- |
| Primary Key | `PK`   | `PK_<Schema>_<Table>`                    | `PK_Foundation_Actor`            |
| Foreign Key | `FK`   | `FK_<Schema>_<ChildTable>_<ParentTable>` | `FK_Foundation_Movie_Producer`   |
| Default     | `DF`   | `DF_<Schema>_<Table>_<Column>`           | `DF_Foundation_Actor_CreatedAt`  |
| Check       | `CK`   | `CK_<Schema>_<Table>_<Column>`           | `CK_Foundation_Actor_Sex`        |
| Unique      | `UQ`   | `UQ_<Schema>_<Table>_<Column>`           | `UQ_Foundation_Movie_Name`       |
| Index       | `IX`   | `IX_<Schema>_<Table>_<Column(s)>`        | `IX_Foundation_Movie_ProducerId` |
| Sequence    | `SQ`   | `SQ_<Schema>_<Name>`                     | `SQ_Foundation_InvoiceNumber`    |
| Trigger     | `TR`   | `TR_<Schema>_<Table>_<Action>`           | `TR_Foundation_Actor_Update`     |

## Recommended SQL Server Standard

```text
PK_<Schema>_<Table>
FK_<Schema>_<ChildTable>_<ParentTable>
DF_<Schema>_<Table>_<Column>
CK_<Schema>_<Table>_<Column>
UQ_<Schema>_<Table>_<Column>
IX_<Schema>_<Table>_<Column(s)>
SQ_<Schema>_<Name>
TR_<Schema>_<Table>_<Action>
```

This convention is concise, descriptive, and scales well for enterprise SQL Server projects.
