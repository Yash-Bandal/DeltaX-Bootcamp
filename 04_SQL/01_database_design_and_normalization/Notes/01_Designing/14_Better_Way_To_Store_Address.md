# Address Database Design Notes

## Why Not Store Address in One Column?

### Bad Design

```sql
Customer
--------
CustomerId
Name
Address
```

Example:

```
Address = "Flat 302, ABC Apartment, FC Road, Pune, Maharashtra, 411004, India"
```

### Problems

- Difficult to search (e.g., all customers from Pune)
- Uses slow `LIKE` queries
- Cannot validate individual parts (State, Country, Postal Code)
- Harder to maintain and display in UI

<br>

## Better Design - Store Address Components

```sql
Address
-------
AddressId (PK)
UnitNumber
StreetNumber
AddressLine1
AddressLine2
City
Region/State
PostalCode
CountryId (FK)
```

### Notes

- **Unit/Street Number:** Store as `VARCHAR` (e.g., `3A`, `12B`)
- **Postal Code:** Store as `VARCHAR` (supports formats like `SW1A 1AA`)
- **Country:** Use a separate `Country` lookup table

<br>

## Country Lookup Table

```sql
Country
-------
CountryId (PK)
CountryName
CountryCode
```

Relationship:

```
Country (1)
     |
     |------< Address (Many)
```

Benefits:
- Prevents duplicate country names
- Ensures valid country selection
- Easy to update country information

<br>

# Why Separate Address into Its Own Table?

Instead of:

```sql
Customer
--------
CustomerId
Name
Address
```

Use:

```sql
Customer
--------
CustomerId
Name
```

```sql
Address
-------
AddressId
...
```

### Reason

Addresses are **entities**, not just attributes.

<br>

# The Moving Problem

Suppose:

```
Customer
    |
    +-- Address = Pune
```

Customer moves to Mumbai.

If you update the address:

```
Pune ❌
Mumbai ✅
```

The old address is lost.

This is a problem because:

- Previous orders may need the old address
- Audit/history is lost

<br>

# Better Solution

Keep addresses separate and link them.

```
Customer

CustomerId
Name
```

```
Address

AddressId
AddressLine1
City
PostalCode
CountryId
```

```
CustomerAddress

CustomerId
AddressId
AddressType
IsActive
CreatedDate
```

Relationship:

```
Customer
    |
    |------< CustomerAddress >------|
                                    |
                                Address
```

<br>

## Benefits of a Linking Table

- Preserve address history
- Support multiple addresses per customer
- Store address metadata

Example:

| Customer | Address | Type |
|----------|---------|------|
| Yash | Pune | Home |
| Yash | Mumbai | Office |
| Yash | Delhi | Shipping |

---

## Common Metadata

```text
CustomerAddress
---------------
CustomerId
AddressId
AddressType
IsActive
CreatedDate
```

- **AddressType** → Home, Office, Billing, Shipping
- **IsActive** → Current address or old address
- **CreatedDate** → When the address was assigned

<br>

# Design Tips

- Use **lookup tables** for fixed values like Country.
- Store **postal codes as text**, not numbers.
- Design according to the application's needs:
  - Small local app → Simpler design.
  - International application → Flexible address structure.

<br>

# Interview Points

- Avoid storing the entire address in one field.
- Split addresses into meaningful components.
- Treat **Address** as a separate entity.
- Use a **Country lookup table**.
- Use a **CustomerAddress linking table** to support:
  - Address history
  - Multiple addresses
  - Address types
