
# 2. Variables and Data Handling

## 2.1 Variables

### What is a Variable?

A variable is a named storage location used to hold data.

Think of a variable as a labeled box:

```text
Age Box   -> 21
Name Box  -> Yash
Price Box -> 99.99
```

The value inside the box can change during program execution.

<br>

### Variable Declaration

```csharp
int age = 21;
string name = "Yash";
double price = 99.99;
```

Syntax:

```csharp
dataType variableName = value;
```

<br>

### Common Data Types

| Type   | Example |
| ------ | ------- |
| int    | 10      |
| double | 10.5    |
| char   | 'A'     |
| string | "Hello" |
| bool   | true    |

<div align = "center">
    
<img width="500" alt="image" src="https://github.com/user-attachments/assets/bbe83ebe-40c5-4d9a-8278-9e26a1341879" />
    <img width="400" alt="image" src="https://github.com/user-attachments/assets/f9e3fd2c-55ed-44cf-98e0-21cb0fd087bb" />
    <img width="400" alt="image" src="https://github.com/user-attachments/assets/6b1d3fe1-54ea-49ae-ad52-80b6ebacaaa7" />
</div>

---
> [!Important]
> Use `f` while declaring `float`
>
> and
>
> `m` for `double`
>
> because default decinal numbers are `Dooubles`
>
> eg
> ```csharp
> float fnum = 1.2f;
> double dnum = 1.5m;
> ```
---


Example:

```csharp
int marks = 95;
double salary = 50000.50;
char grade = 'A';
string city = "Pune";
bool isActive = true;
```

<br>

### Variable Naming Rules

Valid:

```csharp
int age;
string firstName;
double accountBalance;
```

Invalid:

```csharp
int 1age;
string first-name;
```

Rules:

* Cannot start with a number
* Cannot contain spaces
* Cannot use special characters (except _)
* Should use meaningful names

Good:

```csharp
int employeeAge;
```

Bad:

```csharp
int x;
```

<br>

### Updating Variables

```csharp
int age = 21;

age = 22;
```

The latest value replaces the old value.

<br>

## 2.2 Constants

A constant is a value that cannot change after initialization.

Syntax:

```csharp
const double PI = 3.14159;
```

Example:

```csharp
const int MAX_USERS = 100;

Console.WriteLine(MAX_USERS);
```

Invalid:

```csharp
MAX_USERS = 200;
```

Compilation error.

<br>

### When to Use Constants

Use constants for fixed values:

```csharp
const double GST_RATE = 0.18;
const int MAX_RETRY = 3;
const string COMPANY_NAME = "ABC Ltd";
```

### Naming Conventions
<div align = "center">
    <img width="400"  alt="image" src="https://github.com/user-attachments/assets/420001f7-0367-4cc9-bc67-dbeb44025677" />



</div>




<br>

## 2.3 Overflowing

### What is Overflow?

Overflow occurs when a value exceeds the storage capacity of its data type.

Example:

```csharp
byte number = 255;
```

A byte can store:

```text
0 to 255
```

Now:

```csharp
byte number = 255;
number++;
```

Result:

```text
0
```

The value wraps around because the limit was exceeded.

<br>

### Visual Example

```text
Byte Range

0 ............ 255

255 + 1

↓

0
```

<br>

### Checked Keyword

Detect overflow explicitly:

```csharp
checked
{
    byte number = 255;
    number++;
}
```

This throws an exception instead of silently wrapping.

<br>

### Unchecked Keyword

Ignore overflow checking:

```csharp
unchecked
{
    byte number = 255;
    number++;
}
```

Result:

```text
0
```

<br>

### Why Developers Care

Overflow can cause:

* Incorrect calculations
* Financial bugs
* Data corruption

Common in:

* Banking systems
* Inventory systems
* Large calculations

<br>

## 2.4 Scope

### What is Scope?

Scope defines where a variable can be accessed.

A variable only exists inside the block where it is declared.

<br>

### Local Scope

```csharp
static void Main()
{
    int age = 21;

    Console.WriteLine(age);
}
```

Valid because age exists inside Main().

<br>

### Invalid Access

```csharp
static void Main()
{
    int age = 21;
}

Console.WriteLine(age);
```

Compilation error.

The variable exists only inside Main().

<br>

### Block Scope

```csharp
if (true)
{
    int number = 10;
}

Console.WriteLine(number);
```

Compilation error.

number exists only inside the if block.

<br>

### Visual Example

```text
Main()
{
    age -> Accessible

    if()
    {
        number -> Accessible only here
    }
}
```

<br>

### Why Scope Matters

Benefits:

* Prevents accidental modification
* Reduces bugs
* Improves readability
* Saves memory

<br>

## 2.5 Type Conversion

Sometimes we need to convert one data type into another.

Example:

```csharp
int number = 10;
double result = number;
```

<br>

### Implicit Conversion

Performed automatically when there is no risk of data loss.

```csharp
int number = 10;

double result = number;
```

Result:

```text
10 → 10.0
```

<br>

### Explicit Conversion (Casting)

Performed manually when data loss may occur.

```csharp
double price = 10.99;

int value = (int)price;
```

Result:

```text
10
```

Decimal part is removed.

<br>

### Convert Class

---
> [!Tip]
> Recall **C++** `to_string()` for int to string
> 
> and
>
> **C++** `stoi()` for string to int
---

```csharp
string ageText = "25";

int age = Convert.ToInt32(ageText);
```

Result:

```text
25
```

Useful when reading user input or data from files/databases.

<br>

### Parse Method

```csharp
string numberText = "100";

int number = int.Parse(numberText);
```

Result:

```text
100
```

<br>

### TryParse (Recommended)

Safely converts values.

```csharp
string input = "100";

bool success = int.TryParse(input, out int number);
```

If conversion succeeds:

```text
success = true
number = 100
```

If conversion fails:

```text
success = false
```

No exception is thrown.

Used heavily in production applications.

<br>

## Common Type Conversions

```csharp
int → double
double → int
string → int
string → double
string → DateTime
```

Examples:

```csharp
int age = 21;
double ageDouble = age;

double price = 99.99;
int rounded = (int)price;

string text = "100";
int number = Convert.ToInt32(text);
```


<br>

---

<br>

