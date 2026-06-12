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


## Operator Precedence

Operator precedence determines the order in which operations are performed in an expression.

### Arithmetic Operator Precedence

```text
Highest Priority
│
├── ()
├── *  /  %
├── +  -
│
Lowest Priority
```

### Multiplication, Division, and Modulus

The operators:

```csharp
*
/
%
```

all have the **same precedence level**.

When operators have the same precedence, C# evaluates them **from left to right**.

---

### Example 1

```csharp
int result = 20 / 5 * 2;
Console.WriteLine(result);
```

Evaluation:

```text
20 / 5 = 4
4 * 2 = 8
```

Output:

```text
8
```


<br>

### Example 2

```csharp
int result = 20 * 5 / 2 % 3;
Console.WriteLine(result);
```

Evaluation:

```text
20 * 5 = 100
100 / 2 = 50
50 % 3 = 2
```

Output:

```text
2
```

<br>

### Using Parentheses

Parentheses `()` always have the highest precedence and are evaluated first.

```csharp
int result = 20 / (5 * 2);
Console.WriteLine(result);
```

Evaluation:

```text
5 * 2 = 10
20 / 10 = 2
```

Output:

```text
2
```

<br>

### Easy Rule to Remember

```text
()
*  /  %
+  -
```

For `*`, `/`, and `%`:

```text
Same Precedence
↓
Evaluate Left → Right
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


