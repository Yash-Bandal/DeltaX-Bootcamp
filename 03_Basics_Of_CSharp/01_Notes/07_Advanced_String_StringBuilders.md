

# Advanced String Handling

## [Summarizing Sentence - Application](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/03_Basics_Of_CSharp/02_Applications/01_Text_Summarizer.md)

<br>

## StringBuilder

> [!Note]
> Strings are *immutable*, `StringBuilder` class introduce mutability
>
> But with `StringBuilder`, you dont get searching methods, like searching index, char..

## What is StringBuilder?


<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/ebf5eeb6-10d6-4c34-a33d-6f69fb1391fc" />
</div>

`StringBuilder` is a class used to efficiently create and modify strings.

Unlike `string`, a `StringBuilder` object is **mutable**, meaning its content can be changed without creating a new object.

It belongs to the `System.Text` namespace.

```csharp
using System.Text;
```

<br>


## Why Do We Need StringBuilder?

<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/a2f91e56-f5ac-4920-9603-f2138b104dad" />
</div>

but for

<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/a539eef0-ef7b-42ea-8855-bedc93cacfa6" />
</div>

Strings are immutable.

Example:

```csharp
string text = "Hello";

text += " World";
text += "!";
```

Every modification creates a **new string object**, which increases memory usage and reduces performance.

Using `StringBuilder`:

```csharp
StringBuilder sb = new StringBuilder();

sb.Append("Hello");
sb.Append(" World");
sb.Append("!");
```

The same object is modified.

<br>

## String vs StringBuilder

| String                                   | StringBuilder                       |
| ---------------------------------------- | ----------------------------------- |
| Immutable                                | Mutable                             |
| Creates new object on every modification | Modifies existing object            |
| Better for fixed text                    | Better for frequently changing text |
| Slower for many modifications            | Faster for many modifications       |




<br>

## Creating a StringBuilder

### Empty StringBuilder

```csharp
StringBuilder sb = new StringBuilder();
```


<br>

### Initialize with Text

```csharp
StringBuilder sb =
    new StringBuilder("Hello");
```

<br>

### Convert to String

```csharp
StringBuilder sb =
    new StringBuilder("Hello");

string result = sb.ToString();

Console.WriteLine(result);
```

Output:

```text
Hello
```

<br>

# Common Methods

## Append()

Adds text to the end.

```csharp
StringBuilder sb =
    new StringBuilder();

sb.Append("Hello");
sb.Append(" World");

Console.WriteLine(sb);
```

Output:

```text
Hello World
```

<br>

## AppendLine()

Adds text followed by a new line.

```csharp
StringBuilder sb =
    new StringBuilder();

sb.AppendLine("Line 1");
sb.AppendLine("Line 2");

Console.WriteLine(sb);
```

Output:

```text
Line 1
Line 2
```

<br>

## Insert()

Inserts text at a specific position.

```csharp
StringBuilder sb =
    new StringBuilder("Hello");

sb.Insert(5, " World");

Console.WriteLine(sb);
```

Output:

```text
Hello World
```

<br>

## Remove()

Removes characters.

```csharp
StringBuilder sb =
    new StringBuilder("Hello World");

sb.Remove(5, 6);

Console.WriteLine(sb);
```

Output:

```text
Hello
```

Parameters:

```text
Start Index
Number of Characters
```

<br>

## Replace()

Replaces text.

```csharp
StringBuilder sb =
    new StringBuilder("Hello World");

sb.Replace("World", "C#");

Console.WriteLine(sb);
```

Output:

```text
Hello C#
```

<br>

## Clear()

Removes all content.

```csharp
StringBuilder sb =
    new StringBuilder("Hello");

sb.Clear();

Console.WriteLine(sb.Length);
```

Output:

```text
0
```

<br>

## Length Property

Returns total characters.

```csharp
StringBuilder sb =
    new StringBuilder("Hello");

Console.WriteLine(sb.Length);
```

Output:

```text
5
```

<br>

## Chaining Methods

Most methods return the same `StringBuilder` object, allowing method chaining.

```csharp
StringBuilder sb =
    new StringBuilder();

sb.Append("Hello")
  .Append(" ")
  .Append("World")
  .Append("!");
```

Output:

```text
Hello World!
```

<br>

## Access Individual Characters

Like strings, characters can be accessed by index.

```csharp
StringBuilder sb =
    new StringBuilder("Hello");

Console.WriteLine(sb[1]);
```

Output:

```text
e
```

Modify a character:

```csharp
sb[0] = 'Y';

Console.WriteLine(sb);
```

Output:

```text
Yello
```

<br>

## Capacity

Capacity is the amount of memory allocated.

```csharp
StringBuilder sb =
    new StringBuilder();

Console.WriteLine(sb.Capacity);
```

Capacity automatically increases as more text is added.

<br>

## Real-World Example

Generating a report.

Instead of:

```csharp
string report = "";

report += "Employee Report\n";
report += "---------------\n";
report += "John\n";
report += "Mary\n";
```

Use:

```csharp
StringBuilder report =
    new StringBuilder();

report.AppendLine("Employee Report");
report.AppendLine("---------------");
report.AppendLine("John");
report.AppendLine("Mary");

Console.WriteLine(report);
```

Cleaner and much more efficient.

<br>

**Example:**

#### Code 1 
```csharp

using System;
using System.Text;

namespace LearnNumbers
{
    internal partial class Program
    {

        static void Main(string[] args)
        {
            var builder = new StringBuilder();

            //Append
            builder.Append('-', 10);
            builder.AppendLine();

            builder.Append("Header");
            builder.AppendLine();
            builder.Append('-', 10);


            /*
                ----------
                Header
                ----------
             */

            //================


            builder.Replace('-', '+');
            /*
                ++++++++++
                Header
                ++++++++++
             */


            builder.Remove(0, 10);

            /*

                Header
                ++++++++++
             */


            builder.Insert(0, new string('-', 10));
            /*
                ----------
                Header
                ++++++++++  
             */

            Console.WriteLine(builder);

            Console.WriteLine("First Char : " + builder[0]);
        }
    }
}
```
Or chain them, 
You are able to chain them as they return a same method() `StringBuilder`

#### Code 2
```csharp

using System;
using System.Reflection;
using System.Text;

namespace LearnNumbers
{
    internal partial class Program
    {

        static void Main(string[] args)
        {
            var builder = new StringBuilder();

            //All chained
            builder.Append('-', 10)
            .AppendLine()
            .Append("Header")
            .AppendLine()
            .Append('-', 10)
            .Replace('-', '+')
            .Remove(0, 10)
            .Insert(0, new string('-', 10));/

            Console.WriteLine(builder);

            Console.WriteLine("First Char : " + builder[0]);
        }
    }
}

```

<br>

## Common Use Cases

Use `StringBuilder` when:

* Generating reports
* Building large text files
* Creating SQL queries
* Generating HTML
* Creating CSV files
* Logging
* Processing large amounts of text

<br>

## When to Use String vs StringBuilder

### Use String

* Text rarely changes
* Small strings
* Simple concatenation

Example:

```csharp
string name = "Yash";
```

<br>

### Use StringBuilder

* Frequent modifications
* Large text generation
* Loops with string concatenation

Example:

```csharp
StringBuilder sb =
    new StringBuilder();

for(int i = 1; i <= 1000; i++)
{
    sb.Append(i);
}
```

This is much faster than repeatedly using `+=`.

<br>

## Common Interview Question

### Why is StringBuilder Faster?

Because it modifies the existing object instead of creating a new string every time.

<br>

## Most Used Methods

```csharp
Append()

AppendLine()

Insert()

Remove()

Replace()

Clear()

ToString()
```

These methods cover most real-world use cases.

<br>

## Key Takeaways

* `StringBuilder` is a mutable sequence of characters.
* It belongs to the `System.Text` namespace.
* It is more efficient than `string` for repeated modifications.
* Common methods include `Append`, `AppendLine`, `Insert`, `Remove`, `Replace`, and `Clear`.
* Use `ToString()` to convert a `StringBuilder` back to a string.
* Prefer `StringBuilder` when building or modifying large amounts of text.
