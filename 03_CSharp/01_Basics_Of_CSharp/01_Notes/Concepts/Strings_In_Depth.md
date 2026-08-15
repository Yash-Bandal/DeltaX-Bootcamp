# Strings in C#


---
> [!Note]
> Strings are Immutable, you may append a new string to a old string
>
> But it looks to you a same string, internally different objects are created
>
> 
//or chainthem, as they return a method()
using System;

```csharp
namespace LearnNumbers
{
    internal partial class Program
    {

        static void Main(string[] args)
        {


            string text = "Hello";

            // we are appending, not modifying, 
            // and each text var here is a new object, not the sameone
            text += " World";
            text += "!";

            Console.WriteLine(text);

        }
    }
}

```
```
/*
 Before

text
 │
 ▼
"Hello"


After:

"Hello"          "Hello World"
                    ▲
                    │
                  text 

old object       new object
 */

```
---

<br>


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

<div align = "center">
    <img width="500"  alt="image" src="https://github.com/user-attachments/assets/d4271bb9-235d-421d-90c5-cc9a3cd40279" />
</div>

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

<div align = "center">
    <img width="488" height="262" alt="image" src="https://github.com/user-attachments/assets/8ca6cb58-2c77-4979-8e05-597fe4612aa6" />
</div>


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

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/9a901b16-a484-45a5-9d10-2d7c89fd883f" />
</div>


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

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/3fa064a2-e7b8-4ef1-b4e9-8fb4a4699337" />
</div>



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

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/ac56a034-ab61-400c-9f1a-17aa1b58a568" />
</div>



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

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/fd4df61a-5871-4465-804b-6fc0e1fedb84" />
</div>


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

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/efadd7e4-b8af-4b56-99d4-80e211ef0e1d" />
</div>

---
> [!Note]
> Prefer mostly `Convert.ToInt32()`, over `int.Parse()`, because
>
> When String is empty `""` or `NULL`,  `Convert.ToInt32()` returns 0, and `int.Parse()` throws exception
---

<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/701e0521-1ccf-415a-a024-1eff5460f595" />
</div>


### Format Strings

<div align = "center">
    <img width="600" alt="image" src="https://github.com/user-attachments/assets/c21c4c1f-fed5-4a88-8765-2f7cb74fd348" />
</div>


<br>

**Example:**
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LearnNumbers
{
    internal partial class Program
    {
        public static void printList(List<int> ls)
        {
            for (int i = 0; i < ls.Count; i++)
            {
                Console.Write(ls[i] + " ");
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // Trim 
            /*
             Removes whitespaces before and after strings,
             useful when taking user input
             */
            //var fullName = "  Yash  Bandal  ";
            var fullName = "Yash Bandal  ";
            Console.WriteLine("Before Trim : '{0}' ", fullName ); //'  Yash  Bandal  '
            Console.WriteLine("After Trim : '{0}' ", fullName.Trim() ); // 'Yash  Bandal'

            // TopUpper and ToLower
            Console.WriteLine("To Upper: '{0}' ", fullName.ToUpper() ); // YASH  BANDAL 
            Console.WriteLine("To Lower: '{0}' ", fullName.ToLower() ); // yash  bandal

            //========================== Operations

            var index = fullName.IndexOf(' ');
            //Console.WriteLine(index); //4
            var fn = fullName.Substring(0,index); //we supplied start + end ( , )
            var ln = fullName.Substring(index+1); //we supplied just starting point ( )  

            Console.WriteLine("First name :  " +  fn + "\n Last Name : " + ln);

            var strs = fullName.Split(' '); //type string array
            //strs = ["Yash" , "Bandal"] 

            //access as array
            Console.WriteLine("First name :  " + strs[0] + "\n Last Name : " + strs[1]);

            //=================================================
            //Replace
            var newName = fullName.Replace("Yash", "Yash Dada");
            Console.WriteLine(newName);

            if (String.IsNullOrEmpty(" ")) // do invalid for " ", because empty space matters
            {
                Console.WriteLine("First Invalid");
            }



            if (String.IsNullOrEmpty(" ".Trim())) 
            {
                Console.WriteLine("Second Invalid");
            }

            if (String.IsNullOrWhiteSpace(" ")) // do invalid for " ", because empty space matters
            {
                Console.WriteLine("Third Invalid");
            }


        }

    }
}

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
