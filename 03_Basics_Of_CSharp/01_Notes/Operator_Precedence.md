## Operator Precedence

Operator precedence determines which operation is executed first when an expression contains multiple operators.

### Example

```csharp
int result = 10 + 5 * 2;
```

Output:

```text
20
```

Evaluation:

```text
10 + (5 * 2)
= 10 + 10
= 20
```

Since `*` has higher precedence than `+`, multiplication happens first.

<br>

### Common Precedence Order

| Priority | Operators                        |   |   |
| -------- | -------------------------------- | - | - |
| 1        | `()`                             |   |   |
| 2        | `*`, `/`, `%`                    |   |   |
| 3        | `+`, `-`                         |   |   |
| 4        | `==`, `!=`, `>`, `<`, `>=`, `<=` |   |   |
| 5        | `&&`                             |   |   |
| 6        | `                                |   | ` |
| 7        | `=`                              |   |   |

<br>

### Example

```csharp
bool result = 10 > 5 && 20 > 15;
```

Evaluation:

```text
10 > 5      → true
20 > 15     → true

true && true
→ true
```

<br>

### Best Practice

Use parentheses when expressions become complex.

```csharp
int result = 10 + (5 * 2) - 3;
```

This improves readability and avoids confusion.

<br>

### Quick Rule

```text
PEMDAS

Parentheses
Multiplication / Division
Addition / Subtraction
Comparisons
AND (&&)
OR (||)
Assignment (=)
```
