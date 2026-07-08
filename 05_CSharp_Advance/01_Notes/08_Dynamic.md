
# 8. Dynamic

## What is Dynamic?

`dynamic` is a type in C# where type checking happens at **runtime** instead of compile time.

Normally, C# is a strongly typed language.

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
Example:

```csharp
int number = 10;

number = "Hello";
```
 
Compilation Error.

The compiler already knows `number` is an integer.


<br>
<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/ed6638aa-c741-4891-bedf-e6d8012d192e" />
</div>
<br>


<br>

Using dynamic:

```csharp
dynamic value = 10;

value = "Hello";
value = true;
```

Allowed.

The type is decided while the program is running.

<br>

## Static Typing vs Dynamic Typing

### Static Typing

Type is checked during compilation.

Example:

```csharp
string name = "Yash";

Console.WriteLine(name.Length);
```

The compiler knows:

```text
name is a string
```

If you write:

```csharp
name.InvalidMethod();
```

Compiler gives an error immediately.

<br>

### Dynamic Typing

Type checking is delayed until runtime.

Example:

```csharp
dynamic name = "Yash";

Console.WriteLine(name.Length);
```

Works.

But:

```csharp
name.InvalidMethod();
```

Compilation succeeds.

Runtime error occurs:

```text
RuntimeBinderException
```

<br>

## How Dynamic Works

Normal variable:

```text
Compile Time

Check Type
     |
     v
Run Program
```

Dynamic:

```text
Compile Time
     |
     v
Run Program
     |
     v
Check Type
```

The compiler skips type checking.

<br>

# Dynamic Example

```csharp
dynamic value;

value = 10;

Console.WriteLine(value.GetType());

value = "Hello";

Console.WriteLine(value.GetType());
```

Output:

```text
System.Int32

System.String
```

Same variable stores different types.

<br>

# Dynamic vs var

Many beginners confuse them.

## var

Type is decided at compile time.

```csharp
var number = 10;
```

Compiler converts it to:

```csharp
int number = 10;
```

Cannot change later:

```csharp
number = "Hello";
```

Error.

<br>

## dynamic

Type can change at runtime.

```csharp
dynamic value = 10;

value = "Hello";
```

Allowed.

<br>

## var vs dynamic

| var                   | dynamic          |
| ---------------------- | --------------------- |
| Compile-time checking | Runtime checking |
| Type cannot change    | Type can change  |
| Safer                 | Less safe        |
| Better performance    | Slower           |
| Used frequently       | Used rarely      |

<br>

# Dynamic with Methods

Example:

```csharp
dynamic number = 10;

Console.WriteLine(number + 5);
```

Output:

```text
15
```

Now:

```csharp
number = "Hello";

Console.WriteLine(number + 5);
```

Output:

```text
Hello5
```

Behavior depends on runtime type.

<br>

# Dynamic Objects

Dynamic allows calling members without compiler checking.

Example:

```csharp
dynamic person = GetPerson();

Console.WriteLine(person.Name);
Console.WriteLine(person.Age);
```

The compiler trusts that these properties exist.

If they don't:

Runtime error.

<br>

# ExpandoObject

`ExpandoObject` allows creating objects dynamically.

Namespace:

```csharp
using System.Dynamic;
```

Example:

```csharp
dynamic employee =
    new ExpandoObject();

employee.Name = "Yash";
employee.Age = 21;

Console.WriteLine(employee.Name);
```

Output:

```text
Yash
```

Properties are added at runtime.

<br>

# Real-World Use Cases of Dynamic

## 1. Working with JSON

Example:

```csharp
dynamic data = jsonObject;

Console.WriteLine(data.name);
```

Useful when structure is unknown.

<br>

## 2. Reflection

Used when inspecting objects dynamically.

Example:

```text
Loading classes or methods during runtime
```

<br>

## 3. Dynamic APIs

When response structure changes frequently.

<br>

# Dynamic and IntelliSense

With normal objects:

```csharp
string name = "Yash";

name.
```

Visual Studio shows:

```text
Length
ToUpper()
Contains()
```

With dynamic:

```csharp
dynamic name = "Yash";

name.
```

No IntelliSense support because type is unknown.

<br>

# Performance Impact

Dynamic is slower because:

Normal C#:

```text
Compile Time Type Checking
Fast Execution
```

Dynamic:

```text
Runtime Type Checking
Extra Processing
```

Avoid unnecessary dynamic usage.

<br>

# Common Mistake

Using dynamic everywhere.

Bad:

```csharp
dynamic age = 21;
dynamic name = "Yash";
dynamic salary = 50000;
```

Good:

```csharp
int age = 21;
string name = "Yash";
double salary = 50000;
```

Use strong typing whenever possible.

<br>

# When Should You Use Dynamic?

Use dynamic when:

* Type is unknown until runtime
* Working with external systems
* Dynamic JSON data
* Reflection
* COM objects

Avoid dynamic for normal application code.

<br>

# Interview Questions

## Is C# dynamically typed?

No.

C# is primarily a statically typed language.

`dynamic` only allows dynamic behavior.

<br>

## Difference between object and dynamic?

### object

Compiler checks members.

```csharp
object value = "Hello";

value.Length;
```

Compilation Error.

Need casting:

```csharp
((string)value).Length;
```

<br>

### dynamic

```csharp
dynamic value = "Hello";

value.Length;
```

Works directly.

Checking happens at runtime.
