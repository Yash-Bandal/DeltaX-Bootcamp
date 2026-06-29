# 1. Generics

<br>

> [!Note]
> `T` is not Datatype, `T` is like _blank , **Placeholder**

<br>


Generics let you write a class, method, or interface **once** and use it with **any type** — without losing type safety or performance.

```csharp
List<int>     list1 = new List<int>();
List<string>  list2 = new List<string>();
List<Circle>  list3 = new List<Circle>();
```

One `List<T>` class. Works for every type. No code duplication.

<br>

# The Problem Without Generics

Before generics (C# 1.0), you had to write separate classes per type, or fall back to `object`.

<br>
<div align = "center">
    <img width="600" alt="image" src="https://github.com/user-attachments/assets/a5d7615b-3c24-4c10-a21f-063ec4103c8e" />
</div>

<br>


## Option A — Duplicate per type

```csharp
public class IntList
{
    public void Add(int item) { }
    public int Get(int index) { }
}

public class StringList
{
    public void Add(string item) { }
    public string Get(int index) { }
}
```

Same logic, written twice. Ten types = ten classes. Unmaintainable.

## Option B — Use `object`

```csharp
public class ObjectList
{
    private object[] _items = new object[10];

    public void Add(object item)  { /* ... */ }
    public object Get(int index)  { return _items[index]; }
}
```

```csharp
ObjectList list = new ObjectList();
list.Add(42);           // Boxing — int → object → heap
list.Add("hello");      // also valid — no type safety

int n = (int)list.Get(0);   // Unboxing — explicit cast required
string s = (string)list.Get(0);  // Runtime crash — InvalidCastException
```

Problems:

```text
✗  Boxing/unboxing every value type — performance cost
✗  No compile-time type checking — errors appear at runtime
✗  Casting everywhere — noisy, fragile
```

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/b5a49ab4-36e2-4126-8472-6fa624f0b0cd" />
</div>

<br>

<br>

# The Fix — Generics

Declare a **type parameter** `T` — a placeholder resolved at compile time:

```csharp
public class GenericList<T>
{
    private T[] _items = new T[10];
    private int _count = 0;

    public void Add(T item)
    {
        _items[_count++] = item;
    }

    public T Get(int index)
    {
        return _items[index];
    }
}
```

`T` is a stand-in. When you use the class, you fill in the actual type:

```csharp
GenericList<int> intList = new GenericList<int>();
intList.Add(42);        // ✓ only int allowed
intList.Add("hello");   // ✗ compile error — caught immediately

int n = intList.Get(0); // ✓ no cast needed
```

```text
✓  No boxing — int stays int on the stack
✓  Compile-time type safety — wrong type = immediate error
✓  No casting — Get() returns exactly T
```

<br>

# The `T` Convention

`T` stands for "Type". It is just a name — but these are the standard conventions:

```text
T         →  general type parameter
TKey      →  key in a dictionary
TValue    →  value in a dictionary
TInput    →  input type
TOutput   →  output type
TResult   →  return type
```

```csharp
public class Dictionary<TKey, TValue> { }

Dictionary<string, int> scores = new Dictionary<string, int>();
scores["Alice"] = 95;
scores["Bob"]   = 87;
```

Multiple type parameters separated by commas.

<br>

# Generic Methods

You can also make individual methods generic — without making the whole class generic.

```csharp
public class Utilities
{
    public T Max<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) >= 0 ? a : b;
    }
}
```

```csharp
var utils = new Utilities();

int    bigger  = utils.Max<int>(3, 7);         // 7
string later   = utils.Max<string>("a", "z");  // z
```

The compiler can usually infer `T` from the arguments:

```csharp
int bigger = utils.Max(3, 7);     // T inferred as int
```

<br>

# Constraints — `where T : ...`

By default, `T` can be anything — so you can only call methods that exist on `object`.

**Constraints** let you tell the compiler what `T` must be, unlocking more operations.


<br>
<div align = "center">

<img width="600" alt="image" src="https://github.com/user-attachments/assets/d136945e-620c-497c-92f3-835a87290425" />
</div>

<br>

```text
where T : class          →  T must be a reference type
where T : struct         →  T must be a value type
where T : new()          →  T must have a parameterless constructor
where T : SomeClass      →  T must inherit from SomeClass
where T : IComparable    →  T must implement IComparable
where T : class, new()   →  combine multiple constraints
```

## Without constraint — limited

```csharp
public void Print<T>(T item)
{
    Console.WriteLine(item.ToString());   // only .ToString() available — it's on object
}
```

## With constraint — full access

```csharp
public void DrawAll<T>(List<T> items) where T : IDrawable
{
    foreach (T item in items)
    {
        item.Draw();   // ✓ allowed — T is guaranteed to have Draw()
    }
}
```

<br>

# Generic Classes — Real Example

```csharp
public class Stack<T>
{
    private List<T> _items = new List<T>();

    public void Push(T item)
    {
        _items.Add(item);
    }

    public T Pop()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("Stack is empty");

        T last = _items[_items.Count - 1];
        _items.RemoveAt(_items.Count - 1);
        return last;
    }

    public T Peek()
    {
        return _items[_items.Count - 1];
    }

    public int Count => _items.Count;
}
```

```csharp
Stack<int> intStack = new Stack<int>();
intStack.Push(1);
intStack.Push(2);
intStack.Push(3);

Console.WriteLine(intStack.Pop());    // 3
Console.WriteLine(intStack.Peek());   // 2
Console.WriteLine(intStack.Count);    // 2

Stack<string> strStack = new Stack<string>();
strStack.Push("hello");
strStack.Push("world");
Console.WriteLine(strStack.Pop());    // world
```

One class. Two stacks. Zero duplication.

<br>

# Generic Interfaces

Interfaces can be generic too.

```csharp
public interface IRepository<T>
{
    void Add(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Delete(int id);
}
```

```csharp
public class ProductRepository : IRepository<Product>
{
    public void Add(Product entity)      { /* ... */ }
    public Product GetById(int id)       { /* ... */ }
    public IEnumerable<Product> GetAll() { /* ... */ }
    public void Delete(int id)           { /* ... */ }
}
```

This pattern (generic repository) is one of the most common uses of generics in real applications.

<br>

# .NET Built-in Generic Collections

The entire `System.Collections.Generic` namespace is built on generics.

```text
List<T>                  →  dynamic array
Dictionary<TKey,TValue>  →  key-value pairs
HashSet<T>               →  unique items, fast lookup
Queue<T>                 →  FIFO
Stack<T>                 →  LIFO
LinkedList<T>            →  doubly linked list
```

Always prefer these over the old non-generic collections (`ArrayList`, `Hashtable`):

```csharp
// ✗ Old — no type safety, boxing
ArrayList list = new ArrayList();
list.Add(1);
list.Add("oops");   // compiles — crashes or silently wrong later

// ✓ Generic — type-safe, no boxing
List<int> list = new List<int>();
list.Add(1);
list.Add("oops");   // ✗ compile error immediately
```

<br>

# Memory Map — Generic vs Object

```text
ObjectList                        GenericList<int>
──────────                        ────────────────

Stack           Heap              Stack           (no heap for value types)
┌──────────┐    ┌──────────────┐  ┌──────────┐
│ list ref ├───►│ object[]     │  │ list ref ├───► int[] on heap
└──────────┘    │ [0]: box─────┼►│           │   [0]: 42
                │     {42}     │  │           │   [1]: 7
                │ [1]: box─────┼►│           │   [2]: 3
                │     {7}      │  └──────────┘
                └──────────────┘

Each int boxed to heap           Ints stored directly in typed array
= extra allocation per item      = zero boxing, zero extra allocation
```

<br>

# Compile-time vs Runtime

Generics are resolved at **compile time**, not runtime.

```csharp
List<int> list = new List<int>();
```

The compiler generates a version of `List<T>` specifically for `int` — `List<int>` is a concrete type, not an `object` array at runtime. No boxing, no casting, no overhead.

This is different from Java generics which use **type erasure** — Java erases `T` at runtime and treats everything as `Object`. C# preserves the type information all the way through.

```text
C# generics    →  reified — T is preserved at runtime   →  full performance
Java generics  →  erased  — T becomes Object at runtime →  still needs casting internally
```

<br>

# Summary

```text
Without generics                  With generics
───────────────                   ─────────────
Separate class per type           One class — T fills in
  or
object + boxing + casting

ObjectList                        GenericList<T>
  ✗ boxing (value types)            ✓ no boxing
  ✗ no compile-time checks          ✓ type-safe at compile time
  ✗ explicit cast to get value      ✓ returns exact type — no cast
  ✗ one type mistake = crash        ✓ wrong type = compile error
```

- `T` is a type parameter — a placeholder filled in when you use the class
- Generic classes, methods, and interfaces all use `<T>`
- `where T : constraint` restricts what types are allowed and unlocks operations
- .NET's built-in collections (`List<T>`, `Dictionary<TKey,TValue>`) are all generic
- Generics give you reuse, type safety, and performance — all three at once

<br>

# Interview Questions

### What are generics?

A way to write a class, method, or interface parameterized by a type, so it works with any type while remaining type-safe and avoiding boxing.

### What is the difference between `List<T>` and `ArrayList`?

`List<T>` is generic — type-safe, no boxing, compile-time errors on wrong types. `ArrayList` stores everything as `object` — requires boxing for value types and casting to retrieve, with no compile-time safety.

### What are generic constraints?

`where T : ...` clauses that restrict which types can be used as `T`, enabling access to specific members. E.g. `where T : IComparable<T>` lets you call `CompareTo()` on `T`.

### Can a method be generic without its class being generic?

Yes. `public T Max<T>(T a, T b) where T : IComparable<T>` is a generic method on a non-generic class.

### How are C# generics different from Java generics?

C# generics are reified — type information is preserved at runtime. Java generics use type erasure — `T` is replaced by `Object` at compile time. C# generics therefore have better performance and runtime type information.
