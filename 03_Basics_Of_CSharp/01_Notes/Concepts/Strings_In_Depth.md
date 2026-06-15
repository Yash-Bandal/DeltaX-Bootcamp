# Strings in C#

## What is a String?

A string represents text.

```csharp
string name = "Yash";
```

A string is actually an object of the `String` class.

```csharp
string city = "Pune";
String city2 = "Pune";
```

Both are equivalent.

<br>

## String Immutability

Strings are immutable.

Once created, they cannot be changed.

Example:

```csharp
string name = "Yash";

name = "John";
```

A new string is created.

The original string remains unchanged.

<br>

## Common String Methods

### Length

Returns the number of characters.

```csharp
string name = "Yash";

Console.WriteLine(name.Length);
```

Output:

```text
4
```

<br>

### ToUpper()

```csharp
string name = "Yash";

Console.WriteLine(name.ToUpper());
```

Output:

```text
YASH
```

<br>

### ToLower()

```csharp
Console.WriteLine(name.ToLower());
```

Output:

```text
yash
```

<br>

### Trim()

Removes spaces from the beginning and end.

```csharp
string text = "   Hello   ";

Console.WriteLine(text.Trim());
```

Output:

```text
Hello
```

<br>

### StartsWith()

```csharp
string text = "Hello World";

Console.WriteLine(text.StartsWith("Hello"));
```

Output:

```text
True
```

<br>

### EndsWith()

```csharp
Console.WriteLine(text.EndsWith("World"));
```

Output:

```text
True
```

<br>

### Contains()

```csharp
Console.WriteLine(text.Contains("World"));
```

Output:

```text
True
```

<br>

### IndexOf()

Returns the position of a character or string.

```csharp
string text = "Hello";

Console.WriteLine(text.IndexOf('e'));
```

Output:

```text
1
```

<br>

### Replace()

```csharp
string text = "Hello World";

Console.WriteLine(
    text.Replace("World", "C#")
);
```

Output:

```text
Hello C#
```

<br>

### Substring()

Extracts part of a string.

```csharp
string text = "Hello World";

Console.WriteLine(text.Substring(6));
```

Output:

```text
World
```

<br>

## String Concatenation

Combining strings.

```csharp
string firstName = "Yash";
string lastName = "Bandal";

string fullName =
    firstName + " " + lastName;
```

Result:

```text
Yash Bandal
```

<br>

## String Interpolation (Recommended)

Most commonly used in modern C#.

```csharp
string name = "Yash";
int age = 21;

string message =
    $"My name is {name} and I am {age}";
```

Output:

```text
My name is Yash and I am 21
```

Benefits:

* Cleaner
* Easier to read
* Preferred in production code

<br>

## Expression Interpolation

You can execute expressions inside `{}`.

```csharp
int a = 10;
int b = 20;

Console.WriteLine(
    $"Sum = {a + b}"
);
```

Output:

```text
Sum = 30
```

<br>

## String.Format()

Older approach.

```csharp
string message =
    String.Format(
        "Name: {0}, Age: {1}",
        "Yash",
        21
    );
```

Output:

```text
Name: Yash, Age: 21
```

<br>

### Placeholder Indexes

```csharp
String.Format(
    "{0} scored {1}",
    "John",
    90
);
```

```text
0 -> John
1 -> 90
```

<br>

## String.Join()

Combines multiple values.

```csharp
string[] names =
{
    "John",
    "Mary",
    "David"
};

string result =
    String.Join(", ", names);

Console.WriteLine(result);
```

Output:

```text
John, Mary, David
```

Very common in APIs and reporting.

<br>

## String Split()

Breaks a string into parts.

```csharp
string text = "Apple,Banana,Mango";

string[] fruits =
    text.Split(',');
```

Result:

```text
Apple
Banana
Mango
```

<br>

## Verbatim Strings (@)

Used when writing file paths or multi-line text.

Without @:

```csharp
string path =
"C:\\Users\\Yash\\Documents";
```

With @:

```csharp
string path =
@"C:\Users\Yash\Documents";
```

Cleaner and easier to read.

<br>

## Multi-Line Strings

```csharp
string text = @"
Line 1
Line 2
Line 3";
```

Output:

```text
Line 1
Line 2
Line 3
```

<br>

## Escape Sequences

| Escape | Meaning      |
| ------ | ------------ |
| \n     | New Line     |
| \t     | Tab          |
| "      | Double Quote |
| \      | Backslash    |

Example:

```csharp
Console.WriteLine("Hello\nWorld");
```

Output:

```text
Hello
World
```

<br>

## String Comparison

```csharp
string a = "Hello";
string b = "Hello";

Console.WriteLine(a == b);
```

Output:

```text
True
```

<br>

## Null or Empty Check

Very common in production code.

```csharp
string name = "";

if(String.IsNullOrEmpty(name))
{
    Console.WriteLine("Invalid");
}
```

<br>

## Null, Empty and Whitespace

Recommended approach:

```csharp
if(String.IsNullOrWhiteSpace(name))
{
    Console.WriteLine("Invalid");
}
```

Detects:

```text
null
""
" "
```

<br>

## Common Interview Question

### Why is String Immutable?

Benefits:

* Thread Safety
* Better Performance Optimization
* Security
* Reliable Memory Management

<br>

## Real-World Methods Used Daily

Most commonly used in company projects:

```csharp
Length
Trim()
Contains()
StartsWith()
EndsWith()
Replace()
Split()
Join()
Substring()
ToUpper()
ToLower()
String.Format()
String.IsNullOrWhiteSpace()
String Interpolation ($"")
```

<br>

## Key Takeaways

* Strings represent text.
* Strings are immutable.
* String interpolation (`$""`) is preferred over concatenation.
* Verbatim strings (`@"")` simplify file paths and multi-line text.
* Split() converts text into arrays.
* Join() combines arrays into text.
* IsNullOrWhiteSpace() is commonly used for validation.
* String methods are heavily used in APIs, databases, logging, and user input handling.
