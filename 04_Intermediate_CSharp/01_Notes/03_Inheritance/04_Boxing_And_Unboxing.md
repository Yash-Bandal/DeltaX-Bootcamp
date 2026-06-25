# 3.4 Boxing and Unboxing

Boxing is converting a **value type** into a **reference type** (`object`).

Unboxing is converting that reference type back into a **value type**.

These operations exist because C# has a unified type system where **every type ultimately derives from `object`** — including primitives like `int`, `bool`, `double`.

<br>


<div align = "center">
<img width="560" alt="image" src="https://github.com/user-attachments/assets/ceaab883-6601-4deb-8c62-95c9b7a4262c" />
</div>

<br>

# Prerequisites — Stack vs Heap

Before understanding boxing, you need to know where data lives in memory.

<br>

> **Stack** — Fast, LIFO, stores value types and local variables. Memory is auto-released when the method ends.
>
> **Heap** — Dynamic, stores objects (reference types). Managed by the Garbage Collector (GC).


<br>


<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/68337b34-2f6d-4c49-9868-e3aea9c3e0d4" />
</div>


<br>

```text
┌────────────────────────────────┐     ┌──────────────────────────────────┐
│             STACK              │     │              HEAP                │
│  (fast, LIFO, auto-release)    │     │  (dynamic, GC-managed)           │
├────────────────────────────────┤     ├──────────────────────────────────┤
│                                │     │                                  │
│   int i = 42;    ──►  [ 42 ]   │     │   [ object ][ object ][ ... ]    │
│   bool b = true; ──►  [ T  ]   │     │                                  │
│   double d = 3.14──►  [3.14]   │     │   objects live here              │
│                                │     │   referenced by stack pointer    │
└────────────────────────────────┘     └──────────────────────────────────┘
```

- **Value types** (`int`, `double`, `bool`, `struct`) → stored directly on the **Stack**
- **Reference types** (`class`, `string`, `object`) → stored on the **Heap**, stack holds the reference (pointer)

<br>

# Value Types vs Reference Types

```csharp
// Value Type — lives on Stack
int i = 42;

// Reference Type — reference on Stack, object on Heap
object o = new object();
```

```text
Stack               Heap
┌──────────┐        ┌────────────────┐
│  i = 42  │        │  object data   │
├──────────┤        └────────────────┘
│  o ──────┼───────►      ▲
└──────────┘
```

<br>

# What is Boxing?

**Boxing** is the process of wrapping a value type into an `object` on the Heap.

```csharp
int i = 42;

object o = i; // Boxing — implicit

(we converted intege (value type) to obect (reference type)
```

The CLR:
1. Allocates a new object on the **Heap**
2. **Copies** the value of `i` into that object
3. Stores a reference to the heap object in `o`

```text
        Boxing
          │
Stack     │     Heap
┌──────┐  │  ┌──────────────┐
│ i=42 │  │  │  boxed int   │
│      │  │  │  [ value:42 ]│
│ o ───┼──┼─►└──────────────┘
└──────┘  │
          ▼
    value copied to heap
```

> **Key**: `i` and `o` now hold **separate copies**. Changing `i` does NOT change `o`.

<br>

```csharp
int i = 42;
object o = i;     // Boxing

i = 100;

Console.WriteLine(i);           // 100
Console.WriteLine((int)o);      // 42  ← separate copy on Heap
```

<br>

# What is Unboxing?

**Unboxing** extracts the value from the heap object back into a value type on the Stack.

```csharp
int i = 42;
object o = i;      // Boxing

int j = (int)o;    // Unboxing — explicit cast required
```

The CLR:
1. Checks that `o` actually contains a boxed `int`
2. **Copies** the value from the Heap back to the Stack into `j`

```text
        Unboxing
           │
Stack      │      Heap
┌───────┐  │  ┌──────────────┐
│ j=42  │◄─┼──│  boxed int   │
│       │  │  │  [ value:42 ]│
│ o ────┼──┼─►└──────────────┘
└───────┘  │
           ▼
     value copied to stack
```

> Unboxing requires an **explicit cast**. Unlike boxing which is implicit.

<br>

# Boxing vs Unboxing — Side by Side

```csharp
int i = 42;         // value on Stack

object o = i;       // Boxing    → implicit, copies to Heap

int j = (int)o;     // Unboxing  → explicit, copies back to Stack
```

| | Boxing | Unboxing |
|---|---|---|
| Direction | Stack → Heap | Heap → Stack |
| Cast required | No (implicit) | Yes (explicit) |
| Cost | Heap allocation + copy | Type check + copy |

<br>

# Memory Map — Full Picture

<div align = "center">
  <img width="600" alt="image" src="https://github.com/user-attachments/assets/89b426b2-c625-4fb1-9b1c-b22ed46c0179" />
</div>

```text
   int i = 42;
   object o = i;      // Boxing
   int j = (int)o;    // Unboxing


   Stack                       Heap
   ┌─────────────┐             ┌─────────────────────┐
   │             │             │                     │
   │  i = 42     │             │  ┌───────────────┐  │
   │             │   Boxing    │  │  Boxed Object │  │
   │  o ─────────┼────────────►│  │  ┌─────────┐  │  │
   │             │             │  │  │ val: 42 │  │  │
   │  j = 42  ◄──┼─────────────│  │  └─────────┘  │  │
   │             │  Unboxing   │  └───────────────┘  │
   └─────────────┘             └─────────────────────┘

   i, j, o reference — all on stack
   Actual boxed value — on heap
   i and j are independent copies
```

<br>

# Why Does Boxing Exist?

C# has a **unified type system**. Everything derives from `object`.

```text
              object
             /      \
    Value Types    Reference Types
    (int, bool,    (class, string,
     double...)     array...)
```

This means you can write:

```csharp
object x = 42;       // int treated as object
object y = true;     // bool treated as object
object z = 3.14;     // double treated as object
```

The most common real-world case is **non-generic collections**:

```csharp
// ArrayList stores everything as object
ArrayList list = new ArrayList();

list.Add(10);           // int → Boxing happens here
list.Add(20);           // int → Boxing happens here
list.Add(30);           // int → Boxing happens here

int value = (int)list[0];  // Unboxing
```

Each `Add()` call boxes the integer → allocates memory on the heap → extra GC work.

<br>

**Example:**
```csharp
using System.Collections;

namespace CSharpdoubleermediate
{
   
    internal class Program
    {
        public static void Main(string[] args)
        {   
            // Auto boxing inside list
            // ArrayList stores objects

            ArrayList list = new ArrayList();
            list.Add(1);        // add integer (val)
            list.Add("Yash");   // add structure (ref)
            list.Add(DateTime.Today); // add object

            /*
             implicit boxing auto occurs when we store value types inside an ArrayList
             */

            var anotherList = new List<int>();
            anotherList.Add(1); //typesafety, no boxing, only take integer 
        }
    }
}

```

<br>

# Unboxing Errors

Unboxing will throw `InvalidCastException` if you cast to the wrong type.

```csharp
int i = 42;
object o = i;          // Boxed as int

double d = (double)o;  // ✗ Runtime error!
```

```text
InvalidCastException: Unable to cast object of type 'Int32' to type 'Double'
```

The type must match **exactly** what was boxed:

```csharp
int i = 42;
object o = i;

int j    = (int)o;     // ✓ Correct
long k   = (long)o;    // ✗ Error — even though int fits in long
double d = (double)o;  // ✗ Error
```

> You cannot unbox to a compatible type — it must be the **exact same type** that was boxed.

<br>

# Safe Unboxing with `is` and `as`

Use `is` to check before casting:

```csharp
object o = 42;

if (o is int value)
{
    Console.WriteLine(value); // 42
}
```

Use `as` — but note: `as` only works with **reference types**, not value types directly:

```csharp
// 'as' cannot be used on value types directly
// int? (nullable int) is a workaround

object o = 42;
int? value = o as int?;    // returns null if fails, no exception

if (value != null)
{
    Console.WriteLine(value); // 42
}
```

<br>

# Performance Cost

Boxing and unboxing are **not free**.

Each boxing operation:
- Allocates new memory on the Heap
- Copies the value
- Creates more work for the Garbage Collector

```csharp
// Expensive — boxing happens 1,000,000 times
ArrayList list = new ArrayList();

for (int i = 0; i < 1_000_000; i++)
{
    list.Add(i);    // Boxing on every iteration
}
```

```csharp
// Better — no boxing at all
List<int> list = new List<int>();

for (int i = 0; i < 1_000_000; i++)
{
    list.Add(i);    // No boxing — List<T> is generic
}
```

> **Generic collections** (`List<T>`, `Dictionary<TKey, TValue>`) avoid boxing entirely. Always prefer them over `ArrayList`.

<br>

# Common Scenarios Where Boxing Occurs

```csharp
// 1. Assigning value type to object
object o = 100;

// 2. Non-generic collection
ArrayList list = new ArrayList();
list.Add(5);                 // boxing

// 3. Passing int to a method that takes object
void Print(object value) { }
Print(42);                   // boxing

// 4. String.Concat with mixed types
Console.WriteLine("Value: " + 42);   // 42 is boxed
```

<br>

# Avoiding Boxing — Best Practices

```csharp
// ✗ ArrayList — boxes every int
ArrayList old = new ArrayList();
old.Add(1);
old.Add(2);
int x = (int)old[0];   // Unboxing

// ✓ List<int> — no boxing
List<int> modern = new List<int>();
modern.Add(1);
modern.Add(2);
int y = modern[0];     // No cast needed, no boxing
```

<br>

# Summary

```text
Value Type (Stack)
       │
       │  Boxing (implicit)
       ▼
  object on Heap
       │
       │  Unboxing (explicit cast)
       ▼
Value Type (Stack) — new copy
```

- **Boxing** wraps a value type in a heap object — implicit, costs a heap allocation
- **Unboxing** extracts the value back — explicit cast required, must match exact type
- Both produce **copies** — the original value is unaffected
- Unboxing with the wrong type → `InvalidCastException`
- Avoid boxing in loops and hot paths — use **generic collections** instead

<br>

# Interview Questions

### What is boxing?

Boxing is converting a value type to a reference type (`object`), which copies the value to the Heap.

<br>

### Is boxing implicit or explicit?

Boxing is implicit. Unboxing is explicit and requires a cast.

<br>

### What happens to the original value after boxing?

The original stays on the Stack. A **copy** is placed on the Heap. They are independent.

<br>

### What exception does wrong unboxing throw?

`InvalidCastException` — if the target type doesn't match the exact boxed type.

<br>

### Why is boxing a performance concern?

Each boxing creates a heap allocation and adds pressure to the Garbage Collector. In tight loops this adds up significantly.

<br>

### How do you avoid boxing?

Use generic collections (`List<T>`, `Dictionary<TKey, TValue>`) instead of `ArrayList`. Use `is` pattern matching for safe type checks.

<br>

### What is the difference between boxing/unboxing and upcasting/downcasting?

| | Boxing / Unboxing | Upcasting / Downcasting |
|---|---|---|
| Involves | Value types ↔ `object` | Reference types within hierarchy |
| Memory | Moves data Stack ↔ Heap | No memory movement, only reference change |
| Cost | Heap allocation | No extra allocation |
