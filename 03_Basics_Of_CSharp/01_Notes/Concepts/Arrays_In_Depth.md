# Arrays in C#

## What is an Array?

An array is a collection of elements of the same data type stored in contiguous memory locations.

Instead of creating multiple variables:

```csharp
int mark1 = 80;
int mark2 = 85;
int mark3 = 90;
```

Use:

```csharp
int[] marks = { 80, 85, 90 };
```

<br>

## Why Use Arrays?

* Store multiple values in one variable
* Easy iteration using loops
* Better organization of related data
* Faster access using index

<br>

## Array Declaration

### Declaration Only

```csharp
int[] numbers;
```

<br>

### Declaration and Initialization

```csharp
int[] numbers = { 10, 20, 30, 40 };
```

<br>

### Using new Keyword

```csharp
int[] numbers = new int[4];
```

Creates:

```text
[0, 0, 0, 0]
```

Default value for int is 0.

<br>

## Array Indexing

Arrays are zero-indexed.

```csharp
int[] numbers = { 10, 20, 30 };
```

| Index | Value |
| ----- | ----- |
| 0     | 10    |
| 1     | 20    |
| 2     | 30    |

Accessing values:

```csharp
Console.WriteLine(numbers[0]);
Console.WriteLine(numbers[2]);
```

Output:

```text
10
30
```

<br>

## Updating Elements

```csharp
int[] numbers = { 10, 20, 30 };

numbers[1] = 100;
```

Result:

```text
10 100 30
```

<br>

## Array Length

```csharp
int[] numbers = { 10, 20, 30 };

Console.WriteLine(numbers.Length);
```

Output:

```text
3
```

<br>

## Iterating Through Arrays

### For Loop

```csharp
int[] numbers = { 10, 20, 30 };

for(int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine(numbers[i]);
}
```

<br>

### Foreach Loop

Preferred when only reading values.

```csharp
foreach(int number in numbers)
{
    Console.WriteLine(number);
}
```

<br>

# Types of Arrays

## 1. Single-Dimensional Array

Most common type.

```csharp
int[] numbers = { 10, 20, 30 };
```

Visual:

```text
[10][20][30]
```

<br>

## 2. Multi-Dimensional Array (2D Array)

Represents rows and columns.

```csharp
int[,] matrix =
{
    {1, 2, 3},
    {4, 5, 6}
};
```

Visual:

```text
1  2  3
4  5  6
```

Access:

```csharp
Console.WriteLine(matrix[0,1]);
```

Output:

```text
2
```

<br>

### Traversing a 2D Array

```csharp
for(int row = 0; row < 2; row++)
{
    for(int col = 0; col < 3; col++)
    {
        Console.WriteLine(matrix[row,col]);
    }
}
```

<br>

## 3. Jagged Array

An array of arrays.

Each row can have different lengths.

```csharp
int[][] jagged =
{
    new int[] {1, 2},
    new int[] {3, 4, 5},
    new int[] {6}
};
```

Visual:

```text
[1,2]
[3,4,5]
[6]
```

Access:

```csharp
Console.WriteLine(jagged[1][2]);
```

Output:

```text
5
```

<br>

## 2D Array vs Jagged Array

### 2D Array

```text
1 2 3
4 5 6
```

All rows have same length.

<br>

### Jagged Array

```text
1 2
3 4 5
6
```

Rows can have different lengths.

<br>

## Array Class Methods

The `Array` class provides useful operations.

<br>

### Sort()

Sorts elements in ascending order.

```csharp
int[] numbers = { 30, 10, 20 };

Array.Sort(numbers);
```

Result:

```text
10 20 30
```

<br>

### Reverse()

```csharp
int[] numbers = { 10, 20, 30 };

Array.Reverse(numbers);
```

Result:

```text
30 20 10
```

<br>

### IndexOf()

Finds position of an element.

```csharp
int[] numbers = { 10, 20, 30 };

int index =
    Array.IndexOf(numbers, 20);
```

Output:

```text
1
```

<br>

### Clear()

Resets values to default.

```csharp
int[] numbers = { 10, 20, 30 };

Array.Clear(numbers, 0, 2);
```

Result:

```text
0 0 30
```

<br>

### Copy()

Copies elements.

```csharp
int[] source = { 1, 2, 3 };

int[] destination = new int[3];

Array.Copy(
    source,
    destination,
    3
);
```

Result:

```text
1 2 3
```

<br>

## Useful LINQ Methods

Requires:

```csharp
using System.Linq;
```

<br>

### Max()

```csharp
int[] numbers = {10, 20, 30};

Console.WriteLine(numbers.Max());
```

Output:

```text
30
```

<br>

### Min()

```csharp
Console.WriteLine(numbers.Min());
```

Output:

```text
10
```

<br>

### Sum()

```csharp
Console.WriteLine(numbers.Sum());
```

Output:

```text
60
```

<br>

### Average()

```csharp
Console.WriteLine(numbers.Average());
```

Output:

```text
20
```

<br>

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

## Reference Type Behavior

Arrays are Reference Types.

```csharp
int[] arr1 = { 1, 2, 3 };

int[] arr2 = arr1;
```

Both variables point to the same array.

```csharp
arr2[0] = 100;
```

Result:

```text
arr1 -> 100 2 3
arr2 -> 100 2 3
```

This is a common interview question.

<br>

## Common Mistakes

### Index Out Of Range

```csharp
int[] numbers = {10,20,30};

Console.WriteLine(numbers[3]);
```

Error:

```text
IndexOutOfRangeException
```

Valid indexes:

```text
0
1
2
```

<br>

### Wrong Loop Condition

Wrong:

```csharp
for(int i = 0; i <= numbers.Length; i++)
```

Correct:

```csharp
for(int i = 0; i < numbers.Length; i++)
```

<br>

## When to Use Arrays

Use arrays when:

* Size is fixed
* Maximum performance is needed
* Working with matrices
* Low-level data processing

Examples:

```text
Months of Year
Days of Week
Fixed Student Marks
Matrix Calculations
```

<br>

## Arrays vs List<T>

| Array                 | List<T>                |
| --------------------- | ---------------------- |
| Fixed Size            | Dynamic Size           |
| Faster                | More Flexible          |
| Less Features         | More Features          |
| Lower Memory Overhead | Slightly Higher Memory |

### Industry Usage

* Fixed size → Array
* Variable size → List<T>

In most business applications, `List<T>` is used more frequently than arrays.

<br>

**Example:**
```csharp
using System;

namespace LearnNumbers
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
                            // 0   1   2   3   4
            var arr = new[] { 20, 30, 50, 10, 40 };

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            //20 30 50 10 40

            Console.WriteLine();


            //length
            Console.WriteLine(string.Format($"The Length of arr is {arr.Length}"));
            //The Length of arr is 5


            //get index
            int index = Array.IndexOf(arr, 20);
            Console.WriteLine(string.Format($"The index of 20 is {index}"));
            //The index of 20 is 0

            //copy
            var sorted = new int[arr.Length];
            Array.Copy(arr,sorted,arr.Length);

            Console.WriteLine("Before Sorting:");//sort
            
            for (int i = 0; i < sorted.Length; i++)
            {
                Console.Write(sorted[i] + " ");
            }
            // 20 30 50 10 40
            
            Console.WriteLine();

            //sorting
            Array.Sort(sorted);
            Console.WriteLine("Sorted Array:");

            for (int i = 0; i < sorted.Length; i++)
            {
                Console.Write(sorted[i] + " ");
            }
            //10 20 30 40 50

            Console.WriteLine();


            //reverse
            Array.Reverse(sorted);
            Console.WriteLine("Reversed sorted Array:");

            for (int i = 0; i < sorted.Length; i++)
            {
                Console.Write(sorted[i] + " ");
            }
            //50 40 30 20 10

            Console.WriteLine();

            //Clearing
            Array.Clear(sorted, 0,2);
            Console.WriteLine("Clear between Array:");

            for (int i = 0; i < sorted.Length; i++)
            {
                Console.Write(sorted[i] + " ");
            }

            //0 0 30 20 10
        }
    }
}

```

<br>

## Key Takeaways

* Arrays store multiple values of the same type.
* Arrays use zero-based indexing.
* Arrays have a fixed size.
* Common types: Single-Dimensional, Multi-Dimensional, Jagged.
* Arrays are Reference Types.
* Important methods: Sort(), Reverse(), IndexOf(), Copy(), Clear().
* Useful LINQ methods: Max(), Min(), Sum(), Average(), Contains().
* Use arrays when collection size is known beforehand.


<br>

---
---

<br>
