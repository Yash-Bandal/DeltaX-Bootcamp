# List<T> in C#

## What is List<T>?

`List<T>` is a dynamic collection that stores multiple values of the same type.

Unlike arrays, a List can grow and shrink automatically.

```csharp
List<int> numbers = new List<int>();
```

<br>

## Why Use List Instead of Array?

### Array

```csharp
int[] numbers = new int[3];
```

Size is fixed.

<br>

### List

```csharp
List<int> numbers = new List<int>();
```

Size changes automatically.

You can keep adding items without worrying about capacity.

<br>

## Creating Lists

### Empty List

```csharp
List<int> numbers = new List<int>();
```

<br>

### Initialize with Values

```csharp
List<int> numbers =
    new List<int> { 10, 20, 30 };
```

<br>

### String List

```csharp
List<string> names =
    new List<string>
    {
        "John",
        "Mary",
        "David"
    };
```

<br>

## Adding Elements

### Add()

Adds one item.

```csharp
numbers.Add(10);
numbers.Add(20);
numbers.Add(30);
```

Result:

```text
10 20 30
```

<br>

### AddRange()

Adds multiple items.

```csharp
numbers.AddRange(
    new List<int> { 40, 50, 60 }
);
```

Result:

```text
10 20 30 40 50 60
```

<br>

## Accessing Elements

Lists use zero-based indexing.

```csharp
List<int> numbers =
    new List<int> {10,20,30};

Console.WriteLine(numbers[0]);
```

Output:

```text
10
```

<br>

## Updating Elements

```csharp
numbers[1] = 100;
```

Result:

```text
10 100 30
```

<br>

## Count Property

Returns total elements.

```csharp
Console.WriteLine(numbers.Count);
```

Output:

```text
3
```

<br>

## Iterating Through Lists

### For Loop

```csharp
for(int i = 0; i < numbers.Count; i++)
{
    Console.WriteLine(numbers[i]);
}
```

<br>

### Foreach Loop

Preferred when reading data.

```csharp
foreach(int number in numbers)
{
    Console.WriteLine(number);
}
```

<br>

## Removing Elements

### Remove()

Removes first matching value.

```csharp
numbers.Remove(20);
```

Before:

```text
10 20 30
```

After:

```text
10 30
```

<br>

### RemoveAt()

Removes by index.

```csharp
numbers.RemoveAt(1);
```

Before:

```text
10 20 30
```

After:

```text
10 30
```

<br>

### RemoveRange()

```csharp
numbers.RemoveRange(1, 2);
```

Arguments:

```text
StartIndex
Count
```

<br>

### Clear()

Removes all elements.

```csharp
numbers.Clear();
```

Result:

```text
Empty List
```

<br>

## Searching

### Contains()

```csharp
Console.WriteLine(
    numbers.Contains(20)
);
```

Output:

```text
True
```

<br>

### IndexOf()

Returns position.

```csharp
Console.WriteLine(
    numbers.IndexOf(20)
);
```

Output:

```text
1
```

Returns:

```text
-1
```

if not found.

<br>

### Exists()

Checks if an element matches a condition.

```csharp
bool exists =
    numbers.Exists(n => n > 50);
```

Output:

```text
True / False
```

<br>

## Sorting

### Sort()

Ascending order.

```csharp
List<int> numbers =
    new List<int> {30,10,20};

numbers.Sort();
```

Result:

```text
10 20 30
```

<br>

### Reverse()

```csharp
numbers.Reverse();
```

Result:

```text
30 20 10
```

<br>

## Insert Elements

### Insert()

```csharp
numbers.Insert(1, 50);
```

Before:

```text
10 20 30
```

After:

```text
10 50 20 30
```

<br>

### InsertRange()

```csharp
numbers.InsertRange(
    1,
    new List<int>{100,200}
);
```

<br>

## Copying Lists

```csharp
List<int> copy =
    new List<int>(numbers);
```

Creates a new list.

<br>

## Convert List to Array

```csharp
int[] array =
    numbers.ToArray();
```

<br>

## Common LINQ Methods

Requires:

```csharp
using System.Linq;
```

<br>

### Max()

```csharp
Console.WriteLine(numbers.Max());
```

<br>

### Min()

```csharp
Console.WriteLine(numbers.Min());
```

<br>

### Sum()

```csharp
Console.WriteLine(numbers.Sum());
```

<br>

### Average()

```csharp
Console.WriteLine(numbers.Average());
```

<br>

### First()

```csharp
Console.WriteLine(numbers.First());
```

Returns first element.

<br>

### Last()

```csharp
Console.WriteLine(numbers.Last());
```

Returns last element.

<br>

## Filtering Data

### Where()

Very commonly used.

```csharp
var result =
    numbers.Where(n => n > 20);
```

Example:

```csharp
List<int> numbers =
    new List<int>{10,20,30,40};

var result =
    numbers.Where(n => n > 20);
```

Result:

```text
30
40
```

<br>

## Projection

### Select()

Transforms data.

```csharp
var result =
    numbers.Select(n => n * 2);
```

Input:

```text
10 20 30
```

Output:

```text
20 40 60
```

<br>

## Ordering

### OrderBy()

```csharp
var result =
    numbers.OrderBy(n => n);
```

Ascending.

<br>

### OrderByDescending()

```csharp
var result =
    numbers.OrderByDescending(n => n);
```

Descending.

<br>

## Finding Elements

### Find()

Returns first match.

```csharp
int result =
    numbers.Find(n => n > 20);
```

Output:

```text
30
```

<br>

### FindAll()

Returns all matches.

```csharp
var result =
    numbers.FindAll(n => n > 20);
```

Output:

```text
30
40
```

<br>

## List Capacity

### Count

Number of actual elements.

```csharp
Console.WriteLine(numbers.Count);
```

<br>

### Capacity

Allocated storage size.

```csharp
Console.WriteLine(numbers.Capacity);
```

Capacity is usually larger than Count.

The list automatically increases capacity when needed.

<br>

## Reference Type Behavior

Lists are Reference Types.

```csharp
List<int> list1 =
    new List<int>{1,2,3};

List<int> list2 = list1;
```

Both point to the same list.

```csharp
list2.Add(100);
```

Result:

```text
list1 -> 1 2 3 100
list2 -> 1 2 3 100
```

<br>

## Common Interview Question

### Array vs List<T>

| Array         | List<T>         |
| ------------- | --------------- |
| Fixed Size    | Dynamic Size    |
| Faster        | Slightly Slower |
| Less Flexible | More Flexible   |
| Less Features | More Features   |

<br>

## Most Used List Methods in Real Projects

```csharp
Add()
AddRange()

Remove()
RemoveAt()
Clear()

Contains()
IndexOf()

Find()
FindAll()

Sort()
Reverse()

Where()
Select()

First()
Last()

Count
```

If you work with ASP.NET Core, APIs, databases, or Entity Framework, you'll use `List<T>` almost daily.

<br>

## Key Takeaways

* `List<T>` is a dynamic collection.
* Lists automatically resize as items are added.
* Lists use zero-based indexing.
* `Count` gives the number of elements.
* Lists are Reference Types.
* Common methods include Add, Remove, Find, Sort, and Contains.
* LINQ methods like Where and Select are heavily used in modern C# applications.
* In business applications, `List<T>` is generally preferred over arrays unless the size is fixed.


<br>

---
---

<br>
