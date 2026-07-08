# C# Advanced

# Dynamic

## What is dynamic?

`dynamic` allows a variable's type to be decided at runtime.

C# normally checks types during compilation, but `dynamic` delays checking until execution.



<br>
<div align = "center">
  <img width="500" alt="image" src="https://github.com/user-attachments/assets/2621b4f2-bce6-4bd5-9183-2a46459276d2" />
  <img width="400" alt="image" src="https://github.com/user-attachments/assets/69051f33-8d74-43d0-9a72-eb075f7afb8b" />

</div>
<br>




**My example**
```csharp
using System;
using System.Runtime.CompilerServices;

namespace CSharpAdvanced
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic name = 10;
            name++;
            Console.WriteLine(name);  //Currently at runtume 'int'

            name = "Yash"; //implicit  conv
            //name++; //errir
            Console.WriteLine(name); //Currently at runtime 'string'
        }
    }
}
```

<br>

<br>
<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/ed6638aa-c741-4891-bedf-e6d8012d192e" />
</div>
<br>


## Syntax

```csharp
dynamic variableName = value;
```

Example:

```csharp
dynamic data = 10;

data = "Hello";
data = true;
```

Allowed because type is resolved at runtime.

<br>

## dynamic Example

```csharp
dynamic value = "Yash";

Console.WriteLine(value.Length);
```

Output:

```text
4
```

But:

```csharp
dynamic value = "Yash";

value.InvalidMethod();
```

Compiles successfully.

Fails at runtime.

<br>

## var vs dynamic

### var

Type decided at compile time.

```csharp
var number = 10;

number = "Hello"; // Error
```

<br>

### dynamic

Type decided at runtime.

```csharp
dynamic number = 10;

number = "Hello"; // Works
```

<br>

## object vs dynamic

### object

Requires casting.

```csharp
object name = "Yash";

int length =
    ((string)name).Length;
```

<br>

### dynamic

No casting required.

```csharp
dynamic name = "Yash";

int length = name.Length;
```

<br>

# ExpandoObject

Used to create objects dynamically.

Namespace:

```csharp
using System.Dynamic;
```

<br>

## Steps

### 1. Create Dynamic Object

```csharp
dynamic employee =
    new ExpandoObject();
```

<br>

### 2. Add Properties

```csharp
employee.Name = "Yash";
employee.Age = 21;
```

<br>

### 3. Access Properties

```csharp
Console.WriteLine(employee.Name);
Console.WriteLine(employee.Age);
```

Output:

```text
Yash
21
```

<br>

# Common Use Cases

Used with:

* Dynamic JSON data
* Reflection
* COM objects (Excel Automation)
* Unknown runtime objects

Example:

```csharp
dynamic response = GetApiResponse();

Console.WriteLine(response.Name);
```

<br>

# Important Points

* Avoid using dynamic unnecessarily.
* No compile-time checking.
* Runtime errors are possible.
* No IntelliSense support.
* Slower than normal typed variables.

<br>

# Quick Revision

| Feature      | var          | dynamic   |
| ------------ | ------------ | --------- |
| Type decided | Compile time | Runtime   |
| Type change  | No           | Yes       |
| Safe         | Yes          | Less safe |
| Usage        | Common       | Rare      |

<br>

# Key Takeaways

* `dynamic` skips compile-time type checking.
* Type is resolved during runtime.
* Useful when object structure is unknown.
* Prefer normal types unless dynamic behavior is required.
