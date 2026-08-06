# UNION vs UNION ALL

| Feature | UNION | UNION ALL |
|<br><br><br>|<br><br>-|<br><br><br>--|
| Removes duplicate rows | ✅ Yes | ❌ No |
| Keeps duplicate rows | ❌ No | ✅ Yes |
| Performance | Slower (checks duplicates) | Faster |
| Use when | Need unique results | Need all results, including duplicates |

## Example Tables

### Table A

| Name |
|------|
| Alice |
| Bob |
| Charlie |

### Table B

| Name |
|<br><br>|
| Bob |
| David |
| Emma |

## UNION

```sql
SELECT Name FROM TableA
UNION
SELECT Name FROM TableB;
```

### Result

| Name |
|------|
| Alice |
| Bob |
| Charlie |
| David |
| Emma |

> **Note:** Duplicate values (e.g., **Bob**) are removed.

<br>

## UNION ALL

```sql
SELECT Name FROM TableA
UNION ALL
SELECT Name FROM TableB;
```

### Result

| Name |
|------|

| Alice |
| Bob |
| Charlie |
| Bob |
| David |
| Emma |

> **Note:** Duplicate values are retained.

<br>

## Key Difference

- **UNION** → Returns **unique** rows (duplicates removed).
- **UNION ALL** → Returns **all** rows, including duplicates (**faster** than `UNION`).
