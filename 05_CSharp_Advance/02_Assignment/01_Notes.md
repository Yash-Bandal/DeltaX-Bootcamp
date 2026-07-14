## Questions

### 1. Difference between `First` and `FirstOrDefault`
    - `First` returns the first element that matches the condition, like where
        - If nothing found, `First` returns exception, so application crashes
    - If we use `FirstOrDefault`, it returns `null`, therefore application is not breaked

<br>


### 2. Where, select, Max, Min does not print or write anything,     

<br>


### 3. Use `Join` instead `foreach`, because
   - `foreach` prints **`Console.WriteLine()`** everytime,
   - `Join`, when used inside Console.Writeline(), it prints only once, after joining multiple list eles

```csharp
// Single time print, optimized
Console.WriteLine(string.Join(", ", data));

// Less optimized
data.ToList().ForEach(x => Console.WriteLine(x));
```

<br>


### 4.   `Select()` processes one element at a time.
Suppose the user enters:
```
1,2,3
```
Step 1: Split()
```csharp
actorInput.Split(',')
```
Produces:
```
["1", "2", "3"]
```
This is a `string[]`.

####  `Select()` processes one element at a time.
```csharp
.Select(id =>
{
    if (!int.TryParse(id.Trim(), out int parseId))
    {
        throw new InvalidMovieException("Invalid Actor ID Format");
    }

    return parseId;
})
```
Iteration 1:
```
id = "1"

parseId = 1

return 1
```
Iteration 2:
```
id = "2"

parseId = 2

return 2
```

<br>


### 6. Remember, a value type variable default is `0` , `false`, or similar `0 based`, and a reference type is `null`
   
<br>


### 7. Why is the `virtual` keyword applicable only at runtime and not at compile time?

- **Method Overloading** happens **within the same class**. The compiler sees all overloaded methods together and chooses the correct one based on the **method signature (number/type of parameters)**. Hence, it is **compile-time polymorphism**.

- **Method Overriding** happens **between a base class and a derived class**. At compile time, the compiler only knows the **reference type**, not the actual object that will be created.

- At **runtime**, the CLR checks the **actual object**. If the base class method is marked with `virtual` and the derived class provides an `override`, the derived class implementation is executed.

- **If `virtual` is not used** in the base class, the derived class **cannot override** the method. Attempting to use `override` results in a compile-time error.

- **If neither `virtual` nor `override` is used**, both classes simply have their own independent methods. The method that executes depends on the **reference type**, not the actual object, so runtime polymorphism does not occur. Compiler reads it as `public new void Method()`

**Example:**

```csharp
Calculator calculator = new AdvancedCalculator();
calculator.GetResult();
```

- With `virtual` + `override` → `AdvancedCalculator.GetResult()` executes.
- Without `virtual` + `override` → `Calculator.GetResult()` executes.

<br>


### 8. What is difference between abstract classes and interfaces?
```
Abstract Class  →  related things sharing a common base
                    They can have concrete methods and Fields
                   e.g. Shape → Circle, Rectangle


Interface       →  unrelated things sharing a capability
                    They can also have concrete methods (Introduced in C# 8+), but no Fields
                   e.g. Circle, Car, Person  all can be IDrawable

```

#### Can we use abstract classes in place of Interfaces?
Yes, but we should avoid this

In general, we use Interfaces only as a contract, and without any concrete methods (not possible <.net5)

whereas, abstract classes are flexible to have concrete methods

With abstract class
We cannot write this, because CSharp doesnt allow Multiple Inheritance
```csharp
class FileLogger : BaseLogger, ILogger
{
}
```
 C# allows only one base class.

But with an interface:
```csharp
interface ILogger
{
    void Log(string message);
}

class FileLogger : BaseLogger, ILogger
{
}
```
Perfectly valid.

This is the biggest reason interfaces are preferred.

<br>



### 9.1 Difference between new and override

> [!Tip]
>  We cannot perform override without using `override` nd `virtual` keywords

| `override`                                                        | `new`                                     |
| ----------------------------------------------------------------- | ----------------------------------------- |
| Replaces the parent method                                        | Hides the parent method                   |
| Requires parent method to be `virtual`, `abstract`, or `override` | No such requirement                       |
| Supports **runtime polymorphism**                                 | Does **not** support runtime polymorphism |
| Calls child method even through parent reference                  | Calls method based on reference type      |


### 9.2 What is override?

**What**: Changes the implementation of a parent method.

**Why**: To give a different behavior in the child class.

Requires:
```csharp
public virtual void Show()   // Parent
public override void Show()  // Child
```

### 9.3 What is new?

**What**: Hides the parent method by creating a new version in the child.

**Why**: When you want a different method but don't want polymorphism.

Example:
```csharp
class Parent
{
    public void Show() { }
}

class Child : Parent
{
    public new void Show() { }
}
```

### 9.4 What if parent method is NOT virtual?

You cannot use override.

This gives a compile-time error:
```csharp
class Parent
{
    public void Show() { }
}

class Child : Parent
{
    public override void Show() { } // Error
}
```
Because only virtual, abstract, or already override methods can be overridden.

If you still want a child method with the same name, use:
```csharp
public new void Show()
```


Keywords
```
virtual → Allows overriding.
override → Replaces parent implementation.
new → Hides parent method.
```

<br>


### 10 What to do if you dont want to allow overriding a method further?
Seal the method using `Seal` keyword


<br>


### 11 Static Keyword
Static member belongs to the class itself,

They are used, when we want to access any members without creating its object 

Think of them somewhat like reference type, 
#### 11.1 Consider cases

**Case 1**
```csharp
class Student
{
    public string Name = "Yash";

    public static void ChangeName()
    {
        Name = "Rahul";   // ❌ Error
    }
}
```
```
                  Student Class
             -----------------------
             ChangeName()   (static)
             -----------------------

                 ↓ creates

          Object s1            Object s2
      -----------------    -----------------
      Name = "Yash"       Name = "Yash"
      -----------------    -----------------
```
Usage
```csharp
class Student
{
    public string Name = "Yash";

    public static void ChangeName()
    {
        Student s = new Student();
        s.Name = "Rahul";

        Console.WriteLine(s.Name);
    }
}
```

**Case 2**
```csharp
class Student
{
    public static string Name = "Yash";

    public void ChangeName()
    {
        Name = "Rahul";   // ✔️ Works
    }
}
```
```
               Student Class
        --------------------------
        Name = "Yash"
        --------------------------

        Object s1        Object s2
      -------------    -------------
         (empty)          (empty)
```
Works

**Case 3**
```csharp
class Student
{
    public static string Name = "Yash";

    public static void ChangeName()
    {
        Name = "Rahul";
    }
}
```
```
Student Class
---------------------
Name = "Yash"

ChangeName()
---------------------

Object s1

Object s2
```
WOrks


**Case 4**
```csharp
class Student
{
    public string Name = "Yash";

    public void ChangeName()
    {
        Name = "Rahul"; //compiler sees as -> this.Name = "Rahul";
    }
}
```
```
Object s1
----------------
Name = "Yash"
----------------

Object s2
----------------
Name = "Yash"
----------------
```
works

<br>




### 12.1 Difference between const and readonly
| `const`                            | `readonly`                                       |
| ---------------------------------- | ------------------------------------------------ |
| Value is fixed at **compile time** | Value is fixed at **runtime**                    |
| Must be initialized when declared  | Can be initialized in declaration or constructor |
| Cannot change ever                 | Cannot change after constructor finishes         |
| Implicitly `static`                | Can be instance or `static`                      |
| Used for values that never change  | Used for values known only at runtime            |

