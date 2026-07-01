# LINQ (Language Integrated Query)



## 1. Filtering Operators
Filtering operators are used to retrieve records that satisfy a condition.
- Where()
- OfType()





### 1.1 Where()

#### Purpose
Returns all elements that satisfy a condition.

#### Syntax

```csharp
collection.Where(condition)
```

#### Example

```csharp
var result = students.Where(s => s.Marks >= 80);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Yash 95
Rahul 84
Priya 91
```

<br>

### 1.2 OfType()

#### Purpose
Returns only elements of a specified type.

#### Example

```csharp
ArrayList list = new ArrayList()
{
    10,
    "Hello",
    20,
    30.5
};

var result = list.OfType<int>();

foreach(var item in result)
{
    Console.WriteLine(item);
}
```

#### Output

```
10
20
```

<br>

---

<br>

## 2. Projection Operators

Projection operators are used to transform or select data from a collection.

- Select()
- SelectMany()

### 2.1 Select()

#### Purpose

Selects specific data from each element.

#### Syntax

```csharp
collection.Select(selector)
```

#### Example

```csharp
var result = students.Select(s => s.Name);

foreach(var name in result)
{
    Console.WriteLine(name);
}
```

#### Output

```
Yash
Dar
Rahul
Amit
Priya
```

<br>

### 2.2 SelectMany()

#### Purpose

Flattens multiple collections into a single collection.

#### Syntax

```csharp
collection.SelectMany(selector)
```

#### Example

```csharp
List<List<int>> numbers = new List<List<int>>()
{
    new List<int>() { 1, 2, 3 },
    new List<int>() { 4, 5 },
    new List<int>() { 6, 7, 8 }
};

var result = numbers.SelectMany(x => x);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
1
2
3
4
5
6
7
8
```

<br>

---

<br>

## 3. Ordering Operators

Ordering operators are used to sort data in ascending or descending order.

- OrderBy()
- OrderByDescending()
- ThenBy()
- ThenByDescending()
- Reverse()

### 3.1 OrderBy()

#### Purpose

Sorts elements in ascending order.

#### Syntax

```csharp
collection.OrderBy(keySelector)
```

#### Example

```csharp
var result = students.OrderBy(s => s.Marks);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Amit 60
Dar 72
Rahul 84
Priya 91
Yash 95
```

<br>

### 3.2 OrderByDescending()

#### Purpose

Sorts elements in descending order.

#### Syntax

```csharp
collection.OrderByDescending(keySelector)
```

#### Example

```csharp
var result = students.OrderByDescending(s => s.Marks);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Yash 95
Priya 91
Rahul 84
Dar 72
Amit 60
```

<br>

### 3.3 ThenBy()

#### Purpose

Performs a secondary ascending sort after `OrderBy()` or `OrderByDescending()`.

#### Syntax

```csharp
collection.OrderBy(...).ThenBy(...)
```

#### Example

```csharp
List<Student> students = new List<Student>()
{
    new Student("Yash",90),
    new Student("Amit",90),
    new Student("Dar",80),
    new Student("Rahul",80)
};

var result = students
                .OrderBy(s => s.Marks)
                .ThenBy(s => s.Name);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Dar 80
Rahul 80
Amit 90
Yash 90
```

<br>

### 3.4 ThenByDescending()

#### Purpose

Performs a secondary descending sort after `OrderBy()` or `OrderByDescending()`.

#### Syntax

```csharp
collection.OrderBy(...).ThenByDescending(...)
```

#### Example

```csharp
List<Student> students = new List<Student>()
{
    new Student("Yash",90),
    new Student("Amit",90),
    new Student("Dar",80),
    new Student("Rahul",80)
};

var result = students
                .OrderBy(s => s.Marks)
                .ThenByDescending(s => s.Name);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Rahul 80
Dar 80
Yash 90
Amit 90
```

<br>

### 3.5 Reverse()

#### Purpose

Reverses the order of elements in a collection.

#### Syntax

```csharp
collection.Reverse()
```

#### Example

```csharp
List<int> numbers = new List<int>()
{
    1,2,3,4,5
};

var result = numbers.Reverse();

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
5
4
3
2
1
```

<br>

---

<br>

## 4. Element Operators

Element operators are used to retrieve one or more specific elements from a collection.

- First()
- FirstOrDefault()
- Last()
- LastOrDefault()
- Single()
- SingleOrDefault()
- ElementAt()
- ElementAtOrDefault()

### 4.1 First()

#### Purpose

Returns the first element that satisfies a condition.

Throws an exception if no matching element is found.

#### Syntax

```csharp
collection.First(condition)
```

#### Example

```csharp
var result = students.First(s => s.Marks >= 90);

Console.WriteLine($"{result.Name} {result.Marks}");
```

#### Output

```
Yash 95
```

<br>

### 4.2 FirstOrDefault()

#### Purpose

Returns the first matching element or the default value if no match is found.

#### Syntax

```csharp
collection.FirstOrDefault(condition)
```

#### Example

```csharp
var result = students.FirstOrDefault(s => s.Marks > 100);

Console.WriteLine(result == null ? "No Student Found" : result.Name);
```

#### Output

```
No Student Found
```

<br>

### 4.3 Last()

#### Purpose

Returns the last element that satisfies a condition.

Throws an exception if no matching element is found.

#### Syntax

```csharp
collection.Last(condition)
```

#### Example

```csharp
var result = students.Last(s => s.Marks >= 80);

Console.WriteLine($"{result.Name} {result.Marks}");
```

#### Output

```
Priya 91
```

<br>

### 4.4 LastOrDefault()

#### Purpose

Returns the last matching element or the default value if no match is found.

#### Syntax

```csharp
collection.LastOrDefault(condition)
```

#### Example

```csharp
var result = students.LastOrDefault(s => s.Marks > 100);

Console.WriteLine(result == null ? "No Student Found" : result.Name);
```

#### Output

```
No Student Found
```

<br>

### 4.5 Single()

#### Purpose

Returns the only element that satisfies a condition.

Throws an exception if no element or more than one element matches.

#### Syntax

```csharp
collection.Single(condition)
```

#### Example

```csharp
var result = students.Single(s => s.Name == "Rahul");

Console.WriteLine($"{result.Name} {result.Marks}");
```

#### Output

```
Rahul 84
```

<br>

### 4.6 SingleOrDefault()

#### Purpose

Returns the only matching element or the default value if no match is found.

Throws an exception if more than one matching element exists.

#### Syntax

```csharp
collection.SingleOrDefault(condition)
```

#### Example

```csharp
var result = students.SingleOrDefault(s => s.Name == "Rohan");

Console.WriteLine(result == null ? "No Student Found" : result.Name);
```

#### Output

```
No Student Found
```

<br>

### 4.7 ElementAt()

#### Purpose

Returns the element at the specified index.

Throws an exception if the index is out of range.

#### Syntax

```csharp
collection.ElementAt(index)
```

#### Example

```csharp
var result = students.ElementAt(2);

Console.WriteLine($"{result.Name} {result.Marks}");
```

#### Output

```
Rahul 84
```

<br>

### 4.8 ElementAtOrDefault()

#### Purpose

Returns the element at the specified index or the default value if the index is out of range.

#### Syntax

```csharp
collection.ElementAtOrDefault(index)
```

#### Example

```csharp
var result = students.ElementAtOrDefault(10);

Console.WriteLine(result == null ? "No Student Found" : result.Name);
```

#### Output

```
No Student Found
```

<br>

---

<br>

## 5. Quantifier Operators

Quantifier operators are used to determine whether some or all elements in a collection satisfy a condition.

- Any()
- All()
- Contains()

### 5.1 Any()

#### Purpose

Returns `true` if at least one element satisfies the condition.

#### Syntax

```csharp
collection.Any(condition)
```

#### Example

```csharp
bool result = students.Any(s => s.Marks < 35);

Console.WriteLine(result);
```

#### Output

```
False
```

<br>

### 5.2 All()

#### Purpose

Returns `true` if all elements satisfy the condition.

#### Syntax

```csharp
collection.All(condition)
```

#### Example

```csharp
bool result = students.All(s => s.Marks >= 35);

Console.WriteLine(result);
```

#### Output

```
True
```

<br>

### 5.3 Contains()

#### Purpose

Checks whether a collection contains a specified element.

#### Syntax

```csharp
collection.Contains(value)
```

#### Example

```csharp
List<int> numbers = new List<int>()
{
    10,
    20,
    30,
    40,
    50
};

bool result = numbers.Contains(30);

Console.WriteLine(result);
```

#### Output

```
True
```

<br>

---

<br>

## 6. Aggregate Operators


Aggregate operators are used to perform calculations on a collection and return a single value.

- Count()
- LongCount()
- Sum()
- Average()
- Min()
- Max()
- Aggregate()

### 6.1 Count()

#### Purpose

Returns the total number of elements or the number of elements that satisfy a condition.

#### Syntax

```csharp
collection.Count()
collection.Count(condition)
```

#### Example

```csharp
int result = students.Count(s => s.Marks >= 80);

Console.WriteLine(result);
```

#### Output

```
3
```

<br>

### 6.2 LongCount()

#### Purpose

Returns the total number of elements as a `long`.

Used for very large collections.

#### Syntax

```csharp
collection.LongCount()
```

#### Example

```csharp
long result = students.LongCount();

Console.WriteLine(result);
```

#### Output

```
5
```

<br>

### 6.3 Sum()

#### Purpose

Calculates the sum of numeric values.

#### Syntax

```csharp
collection.Sum(selector)
```

#### Example

```csharp
int result = students.Sum(s => s.Marks);

Console.WriteLine(result);
```

#### Output

```
402
```

<br>

### 6.4 Average()

#### Purpose

Calculates the average of numeric values.

#### Syntax

```csharp
collection.Average(selector)
```

#### Example

```csharp
double result = students.Average(s => s.Marks);

Console.WriteLine(result);
```

#### Output

```
80.4
```

<br>

### 6.5 Min()

#### Purpose

Returns the minimum value.

#### Syntax

```csharp
collection.Min(selector)
```

#### Example

```csharp
int result = students.Min(s => s.Marks);

Console.WriteLine(result);
```

#### Output

```
60
```

<br>

### 6.6 Max()

#### Purpose

Returns the maximum value.

#### Syntax

```csharp
collection.Max(selector)
```

#### Example

```csharp
int result = students.Max(s => s.Marks);

Console.WriteLine(result);
```

#### Output

```
95
```

<br>

### 6.7 Aggregate()

#### Purpose

Performs a custom aggregation on all elements and returns a single result.

#### Syntax

```csharp
collection.Aggregate(function)
```

#### Example

```csharp
List<int> numbers = new List<int>()
{
    10,
    20,
    30,
    40
};

int result = numbers.Aggregate((total, number) => total + number);

Console.WriteLine(result);
```

#### Output

```
100
```

<br>

---

<br>

## 7. Paging (Partitioning) Operators

Paging operators are used to retrieve a specific portion of a collection.

- Skip()
- SkipWhile()
- Take()
- TakeWhile()
- Chunk() (.NET 6+)

### 7.1 Skip()

#### Purpose

Skips the specified number of elements and returns the remaining elements.

#### Syntax

```csharp
collection.Skip(count)
```

#### Example

```csharp
var result = students.Skip(2);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Rahul 84
Amit 60
Priya 91
```

<br>

### 7.2 SkipWhile()

#### Purpose

Skips elements while the condition is true, then returns the remaining elements.

#### Syntax

```csharp
collection.SkipWhile(condition)
```

#### Example

```csharp
List<int> numbers = new List<int>()
{
    10, 20, 30, 5, 40, 50
};

var result = numbers.SkipWhile(n => n < 30);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
30
5
40
50
```

<br>

### 7.3 Take()

#### Purpose

Returns the specified number of elements from the beginning of the collection.

#### Syntax

```csharp
collection.Take(count)
```

#### Example

```csharp
var result = students.Take(3);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Yash 95
Dar 72
Rahul 84
```

<br>

### 7.4 TakeWhile()

#### Purpose

Returns elements while the condition is true.

#### Syntax

```csharp
collection.TakeWhile(condition)
```

#### Example

```csharp
List<int> numbers = new List<int>()
{
    10, 20, 30, 5, 40, 50
};

var result = numbers.TakeWhile(n => n < 30);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
10
20
```

<br>

### 7.5 Chunk() (.NET 6+)

#### Purpose

Splits a collection into smaller chunks of the specified size.

#### Syntax

```csharp
collection.Chunk(size)
```

#### Example

```csharp
List<int> numbers = new List<int>()
{
    1,2,3,4,5,6,7
};

var result = numbers.Chunk(3);

foreach(var chunk in result)
{
    Console.Write("[ ");

    foreach(var number in chunk)
    {
        Console.Write(number + " ");
    }

    Console.WriteLine("]");
}
```

#### Output

```
[ 1 2 3 ]
[ 4 5 6 ]
[ 7 ]
```

<br>

---

<br>

## 8. Set Operators

Set operators are used to combine, compare, or remove duplicate elements from collections.

- Distinct()
- DistinctBy() (.NET 6+)
- Union()
- UnionBy() (.NET 6+)
- Intersect()
- IntersectBy() (.NET 6+)
- Except()
- ExceptBy() (.NET 6+)

### 8.1 Distinct()

#### Purpose

Returns only unique elements by removing duplicates.

#### Syntax

```csharp
collection.Distinct()
```

#### Example

```csharp
List<int> numbers = new List<int>()
{
    10, 20, 10, 30, 20, 40
};

var result = numbers.Distinct();

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
10
20
30
40
```

<br>

### 8.2 DistinctBy() (.NET 6+)

#### Purpose

Returns unique elements based on a selected property.

#### Syntax

```csharp
collection.DistinctBy(selector)
```

#### Example

```csharp
List<Student> students = new List<Student>()
{
    new Student("Yash",95),
    new Student("Rahul",84),
    new Student("Amit",95),
    new Student("Dar",72)
};

var result = students.DistinctBy(s => s.Marks);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Yash 95
Rahul 84
Dar 72
```

<br>

### 8.3 Union()

#### Purpose

Combines two collections and removes duplicate elements.

#### Syntax

```csharp
collection1.Union(collection2)
```

#### Example

```csharp
List<int> list1 = new() { 1, 2, 3 };
List<int> list2 = new() { 3, 4, 5 };

var result = list1.Union(list2);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
1
2
3
4
5
```

<br>

### 8.4 UnionBy() (.NET 6+)

#### Purpose

Combines two collections and removes duplicates based on a selected property.

#### Syntax

```csharp
collection1.UnionBy(collection2, selector)
```

#### Example

```csharp
List<Student> list1 = new()
{
    new Student("Yash",95),
    new Student("Rahul",84)
};

List<Student> list2 = new()
{
    new Student("Amit",95),
    new Student("Dar",72)
};

var result = list1.UnionBy(list2, s => s.Marks);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Yash 95
Rahul 84
Dar 72
```

<br>

### 8.5 Intersect()

#### Purpose

Returns only the common elements present in both collections.

#### Syntax

```csharp
collection1.Intersect(collection2)
```

#### Example

```csharp
List<int> list1 = new() { 1, 2, 3, 4 };
List<int> list2 = new() { 3, 4, 5, 6 };

var result = list1.Intersect(list2);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
3
4
```

<br>

### 8.6 IntersectBy() (.NET 6+)

#### Purpose

Returns common elements based on a selected property.

#### Syntax

```csharp
collection1.IntersectBy(keys, selector)
```

#### Example

```csharp
List<Student> students = new()
{
    new Student("Yash",95),
    new Student("Rahul",84),
    new Student("Dar",72)
};

List<int> marks = new() { 72, 95 };

var result = students.IntersectBy(marks, s => s.Marks);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Yash 95
Dar 72
```

<br>

### 8.7 Except()

#### Purpose

Returns elements from the first collection that are not present in the second collection.

#### Syntax

```csharp
collection1.Except(collection2)
```

#### Example

```csharp
List<int> list1 = new() { 1, 2, 3, 4 };
List<int> list2 = new() { 3, 4 };

var result = list1.Except(list2);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
1
2
```

<br>

### 8.8 ExceptBy() (.NET 6+)

#### Purpose

Returns elements whose selected property is not present in the provided key collection.

#### Syntax

```csharp
collection.ExceptBy(keys, selector)
```

#### Example

```csharp
List<Student> students = new()
{
    new Student("Yash",95),
    new Student("Rahul",84),
    new Student("Dar",72)
};

List<int> marks = new() { 95 };

var result = students.ExceptBy(marks, s => s.Marks);

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Rahul 84
Dar 72
```

<br>

---

<br>

## 9. Grouping Operators

Grouping operators are used to group elements based on a common key.

- GroupBy()
- ToLookup()

### 9.1 GroupBy()

#### Purpose

Groups elements based on a specified key.

#### Syntax

```csharp
collection.GroupBy(keySelector)
```

#### Example

```csharp
var result = students.GroupBy(s => s.Marks >= 80);

foreach (var group in result)
{
    Console.WriteLine($"Group: {group.Key}");

    foreach (var student in group)
    {
        Console.WriteLine($"{student.Name} {student.Marks}");
    }

    Console.WriteLine();
}
```

#### Output

```
Group: True
Yash 95
Rahul 84
Priya 91

Group: False
Dar 72
Amit 60
```

<br>

### 9.2 ToLookup()

#### Purpose

Creates a read-only lookup (key-value collection) for fast retrieval of grouped data.

#### Syntax

```csharp
collection.ToLookup(keySelector)
```

#### Example

```csharp
var result = students.ToLookup(s => s.Marks >= 80);

foreach (var group in result)
{
    Console.WriteLine($"Group: {group.Key}");

    foreach (var student in group)
    {
        Console.WriteLine($"{student.Name} {student.Marks}");
    }

    Console.WriteLine();
}
```

#### Output

```
Group: True
Yash 95
Rahul 84
Priya 91

Group: False
Dar 72
Amit 60
```

<br>


---

<br>

## 10. Joining Operators

Joining operators are used to combine data from two collections based on a common key.

- Join()
- GroupJoin()
- Zip()

### 10.1 Join()

#### Purpose

Performs an inner join between two collections.

#### Syntax

```csharp
collection1.Join(
    collection2,
    outerKeySelector,
    innerKeySelector,
    resultSelector)
```

#### Example

```csharp
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Department(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class Student
{
    public string Name { get; set; }
    public int DepartmentId { get; set; }

    public Student(string name, int departmentId)
    {
        Name = name;
        DepartmentId = departmentId;
    }
}

List<Student> students = new()
{
    new Student("Yash",1),
    new Student("Rahul",2),
    new Student("Priya",1)
};

List<Department> departments = new()
{
    new Department(1,"Computer"),
    new Department(2,"Mechanical")
};

var result = students.Join(
    departments,
    s => s.DepartmentId,
    d => d.Id,
    (s, d) => new
    {
        StudentName = s.Name,
        DepartmentName = d.Name
    });

foreach(var item in result)
{
    Console.WriteLine($"{item.StudentName} - {item.DepartmentName}");
}
```

#### Output

```
Yash - Computer
Rahul - Mechanical
Priya - Computer
```

<br>

### 10.2 GroupJoin()

#### Purpose

Groups matching elements from the second collection for each element in the first collection.

#### Syntax

```csharp
collection1.GroupJoin(
    collection2,
    outerKeySelector,
    innerKeySelector,
    resultSelector)
```

#### Example

```csharp
var result = departments.GroupJoin(
    students,
    d => d.Id,
    s => s.DepartmentId,
    (department, studentGroup) => new
    {
        Department = department.Name,
        Students = studentGroup
    });

foreach(var group in result)
{
    Console.WriteLine(group.Department);

    foreach(var student in group.Students)
    {
        Console.WriteLine(student.Name);
    }

    Console.WriteLine();
}
```

#### Output

```
Computer
Yash
Priya

Mechanical
Rahul
```

<br>

### 10.3 Zip()

#### Purpose

Combines two collections element-by-element.

#### Syntax

```csharp
collection1.Zip(collection2, resultSelector)
```

#### Example

```csharp
List<string> names = new()
{
    "Yash",
    "Rahul",
    "Priya"
};

List<int> marks = new()
{
    95,
    84,
    91
};

var result = names.Zip(marks,
    (name, mark) => $"{name} - {mark}");

foreach(var item in result)
{
    Console.WriteLine(item);
}
```

#### Output

```
Yash - 95
Rahul - 84
Priya - 91
```

<br>


---


<br>

## 11. Conversion (Materialization) Operators

Conversion operators are used to convert the result of a LINQ query into another collection type.

- ToList()
- ToArray()
- ToDictionary()
- ToHashSet()
- ToLookup()

### 11.1 ToList()

#### Purpose

Converts a sequence into a `List<T>`.

#### Syntax

```csharp
collection.ToList()
```

#### Example

```csharp
var result = students
                .Where(s => s.Marks >= 80)
                .ToList();

foreach(var student in result)
{
    Console.WriteLine($"{student.Name} {student.Marks}");
}
```

#### Output

```
Yash 95
Rahul 84
Priya 91
```

<br>

### 11.2 ToArray()

#### Purpose

Converts a sequence into an array.

#### Syntax

```csharp
collection.ToArray()
```

#### Example

```csharp
var result = students
                .Select(s => s.Name)
                .ToArray();

foreach(var name in result)
{
    Console.WriteLine(name);
}
```

#### Output

```
Yash
Dar
Rahul
Amit
Priya
```

<br>

### 11.3 ToDictionary()

#### Purpose

Converts a sequence into a Dictionary using a key selector.

#### Syntax

```csharp
collection.ToDictionary(keySelector)
```

#### Example

```csharp
var result = students.ToDictionary(s => s.Name);

Console.WriteLine(result["Rahul"].Marks);
```

#### Output

```
84
```

<br>

### 11.4 ToHashSet()

#### Purpose

Converts a sequence into a `HashSet<T>` and removes duplicate elements.

#### Syntax

```csharp
collection.ToHashSet()
```

#### Example

```csharp
List<int> numbers = new()
{
    10,20,20,30,30,40
};

var result = numbers.ToHashSet();

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
10
20
30
40
```

<br>

### 11.5 ToLookup()

#### Purpose

Converts a sequence into a read-only lookup collection grouped by a key.

#### Syntax

```csharp
collection.ToLookup(keySelector)
```

#### Example

```csharp
var result = students.ToLookup(s => s.Marks >= 80);

foreach(var group in result)
{
    Console.WriteLine($"Group: {group.Key}");

    foreach(var student in group)
    {
        Console.WriteLine(student.Name);
    }

    Console.WriteLine();
}
```

#### Output

```
Group: True
Yash
Rahul
Priya

Group: False
Dar
Amit
```

<br>

---


<br>

## 12. Generation Operators

Generation operators are used to generate a new sequence of values.

- Empty()
- Range()
- Repeat()

### 12.1 Empty()

#### Purpose

Returns an empty sequence of the specified type.

#### Syntax

```csharp
Enumerable.Empty<T>()
```

#### Example

```csharp
var result = Enumerable.Empty<int>();

Console.WriteLine(result.Count());
```

#### Output

```
0
```

<br>

### 12.2 Range()

#### Purpose

Generates a sequence of consecutive integers.

#### Syntax

```csharp
Enumerable.Range(start, count)
```

#### Example

```csharp
var result = Enumerable.Range(1, 5);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
1
2
3
4
5
```

<br>

### 12.3 Repeat()

#### Purpose

Repeats the specified value a given number of times.

#### Syntax

```csharp
Enumerable.Repeat(value, count)
```

#### Example

```csharp
var result = Enumerable.Repeat("Hello", 3);

foreach(var item in result)
{
    Console.WriteLine(item);
}
```

#### Output

```
Hello
Hello
Hello
```

<br>

---

<br>

## 13. Sequence Operators

Sequence operators are used to compare, combine, or modify entire sequences.

- SequenceEqual()
- DefaultIfEmpty()
- Append()
- Prepend()
- Concat()

### 13.1 SequenceEqual()

#### Purpose

Determines whether two sequences are equal by comparing each element.

#### Syntax

```csharp
collection1.SequenceEqual(collection2)
```

#### Example

```csharp
List<int> list1 = new() { 10, 20, 30 };
List<int> list2 = new() { 10, 20, 30 };

bool result = list1.SequenceEqual(list2);

Console.WriteLine(result);
```

#### Output

```
True
```

<br>

### 13.2 DefaultIfEmpty()

#### Purpose

Returns a default value if the collection is empty.

#### Syntax

```csharp
collection.DefaultIfEmpty(defaultValue)
```

#### Example

```csharp
List<int> numbers = new();

var result = numbers.DefaultIfEmpty(100);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
100
```

<br>

### 13.3 Append()

#### Purpose

Adds an element to the end of a sequence.

#### Syntax

```csharp
collection.Append(value)
```

#### Example

```csharp
List<int> numbers = new() { 10, 20, 30 };

var result = numbers.Append(40);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
10
20
30
40
```

<br>

### 13.4 Prepend()

#### Purpose

Adds an element to the beginning of a sequence.

#### Syntax

```csharp
collection.Prepend(value)
```

#### Example

```csharp
List<int> numbers = new() { 10, 20, 30 };

var result = numbers.Prepend(5);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
5
10
20
30
```

<br>

### 13.5 Concat()

#### Purpose

Concatenates two sequences.

#### Syntax

```csharp
collection1.Concat(collection2)
```

#### Example

```csharp
List<int> list1 = new() { 10, 20 };
List<int> list2 = new() { 30, 40 };

var result = list1.Concat(list2);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
10
20
30
40
```

<br>

---


<br>

## 14. Miscellaneous Operators

Miscellaneous operators are utility operators used for type conversion and LINQ compatibility.

- Cast()
- OfType()
- AsEnumerable()

### 14.1 Cast()

#### Purpose

Converts all elements of a non-generic collection to the specified type.

Throws an exception if any element cannot be converted.

#### Syntax

```csharp
collection.Cast<T>()
```

#### Example

```csharp
ArrayList list = new()
{
    10,
    20,
    30
};

var result = list.Cast<int>();

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
10
20
30
```

<br>

### 14.2 OfType()

#### Purpose

Returns only the elements of the specified type.

#### Syntax

```csharp
collection.OfType<T>()
```

#### Example

```csharp
ArrayList list = new()
{
    10,
    "Hello",
    20,
    30.5
};

var result = list.OfType<int>();

foreach(var item in result)
{
    Console.WriteLine(item);
}
```

#### Output

```
10
20
```

<br>

### 14.3 AsEnumerable()

#### Purpose

Treats a collection as an `IEnumerable<T>`, enabling LINQ operations.

#### Syntax

```csharp
collection.AsEnumerable()
```

#### Example

```csharp
List<int> numbers = new()
{
    10,
    20,
    30
};

var result = numbers
                .AsEnumerable()
                .Where(n => n > 15);

foreach(var number in result)
{
    Console.WriteLine(number);
}
```

#### Output

```
20
30
```

<br>

---


<br>



# LINQ Operators Summary

## 1. Filtering Operators
- Where()
- OfType()

## 2. Projection Operators
- Select()
- SelectMany()

## 3. Ordering Operators
- OrderBy()
- OrderByDescending()
- ThenBy()
- ThenByDescending()
- Reverse()

## 4. Element Operators
- First()
- FirstOrDefault()
- Last()
- LastOrDefault()
- Single()
- SingleOrDefault()
- ElementAt()
- ElementAtOrDefault()

## 5. Quantifier Operators
- Any()
- All()
- Contains()

## 6. Aggregate Operators
- Count()
- LongCount()
- Sum()
- Average()
- Min()
- Max()
- Aggregate()

## 7. Paging (Partitioning) Operators
- Skip()
- SkipWhile()
- Take()
- TakeWhile()
- Chunk()

## 8. Set Operators
- Distinct()
- DistinctBy()
- Union()
- UnionBy()
- Intersect()
- IntersectBy()
- Except()
- ExceptBy()

## 9. Grouping Operators
- GroupBy()
- ToLookup()

## 10. Joining Operators
- Join()
- GroupJoin()
- Zip()

## 11. Conversion (Materialization) Operators
- ToList()
- ToArray()
- ToDictionary()
- ToHashSet()
- ToLookup()

## 12. Generation Operators
- Empty()
- Range()
- Repeat()

## 13. Sequence Operators
- SequenceEqual()
- DefaultIfEmpty()
- Append()
- Prepend()
- Concat()

## 14. Miscellaneous Operators
- Cast()
- AsEnumerable()
