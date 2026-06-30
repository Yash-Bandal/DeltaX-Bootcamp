# 3. Lambda Expressions

<br>

> [!Tip]
>  Used for shorted operations, liike Add, Print, Maths expressions, where you dont need to have function name\
>  eg instead of Add(a,b){return a+b}, we write (a,b) => a+b simple 

<br>

## 3.1 What are Lambda Expressions?

A **Lambda Expression** is a **shorter and cleaner way of writing methods**.

Instead of creating a separate method and then passing it somewhere, we can write the method **inline** using the `=>` operator.

Think of it as an **anonymous (unnamed) method**.

<br>

<div align = "center">
  <img width="577" height="300" alt="image" src="https://github.com/user-attachments/assets/7b97cd3f-df09-41f9-9298-72b0f319beb6" />
  <p>Lambda function with Generic Delegates</p>
</div>
<br>


## 3.2 Why do we need Lambda Expressions?

Imagine you want to tell someone:

> "Whenever a student is older than 18, do something."

Instead of creating an entire method just for one small task, you can simply write the logic where it is needed.

Without Lambda:

```csharp
bool IsAdult(Student s)
{
    return s.Age >= 18;
}

students.Where(IsAdult);
```

With Lambda:

```csharp
students.Where(s => s.Age >= 18);
```

Much shorter and easier to read.

<br>

## 3.3 Real-Life Analogy

Imagine ordering food.

Instead of saying:

> "Please call the chef, ask him to create a recipe called MakePizza(), then prepare the pizza."

You simply say:

> "Make me a cheese pizza."

A lambda is like giving the instruction directly instead of creating a named instruction first.

<br>

## 3.4 Understanding the Syntax

### 3.4.1 General Syntax

```csharp
(parameters) => expression
```

or

```csharp
(parameters) =>
{
    // multiple statements
}
```

The `=>` symbol is called the **Lambda Operator**.

Read it as:

> "goes to"

or

> "takes this input and produces this output."

<br>

## 3.5 Parts of a Lambda Expression

### 3.5.1 Example

```csharp
x => x * x
```

Breakdown

```
x
↑
Parameter

=> 
↑
Lambda operator

x * x
↑
Expression / Logic
```

Meaning:

```
Take x

↓

Multiply x by itself

↓

Return the result
```

<br>

## 3.6 Lambda Syntax Variations

### 3.6.1 Single Parameter

```csharp
x => x * 2
```

Equivalent Method

```csharp
int Double(int x)
{
    return x * 2;
}
```

<br>

### 3.6.2 Multiple Parameters

```csharp
(x, y) => x + y
```

Equivalent Method

```csharp
int Add(int x, int y)
{
    return x + y;
}
```

<br>

### 3.6.3 No Parameters

```csharp
() => Console.WriteLine("Hello")
```

Equivalent Method

```csharp
void Print()
{
    Console.WriteLine("Hello");
}
```

<br>

### 3.6.4 Multiple Statements

```csharp
x =>
{
    int square = x * x;
    return square;
}
```

Equivalent Method

```csharp
int Square(int x)
{
    int square = x * x;
    return square;
}
```

<br>

## 3.7 Step-by-Step: How to Write a Lambda

Suppose we have this method

```csharp
int Add(int a, int b)
{
    return a + b;
}
```

### 3.7.1 Step 1

Take the parameters.

```csharp
(a, b)
```

↓

### 3.7.2 Step 2

Remove the method name and return type.

```csharp
(a, b)
```

↓

### 3.7.3 Step 3

Replace the opening brace with `=>`

```csharp
(a, b) =>
```

↓

### 3.7.4 Step 4

Write the logic.

```csharp
(a, b) => a + b
```

Done!

<br>

## 3.8 Lambda with Delegates

Delegate

```csharp
delegate int Calculator(int a, int b);
```

Using Normal Method

```csharp
Calculator calc = Add;
```

Using Lambda

```csharp
Calculator calc = (a, b) => a + b;

Console.WriteLine(calc(10, 20));
```

<br>

## 3.9 Lambda with Func

Normal

```csharp
Func<int, int> square = delegate (int x)
{
    return x * x;
};
```

Lambda

```csharp
Func<int, int> square = x => x * x;
```

Call

```csharp
Console.WriteLine(square(5));
```

Output

```
25
```

<br>

## 3.10 Lambda with Action

```csharp
Action<string> greet = name => Console.WriteLine($"Hello {name}");

greet("Yash");
```

Output

```
Hello Yash
```

<br>

## 3.11 Lambda with Predicate

```csharp
Predicate<int> isEven = number => number % 2 == 0;

Console.WriteLine(isEven(8));
```

Output

```
True
```

<br>

## 3.12 Lambda with LINQ

Suppose

```csharp
List<int> numbers = new()
{
    2,4,5,7,8,10
};
```

### 3.12.1 Filter Even Numbers

```csharp
var evenNumbers = numbers.Where(x => x % 2 == 0);
```

Read it as

```
Take every number

↓

Check if it is divisible by 2

↓

Keep only those numbers
```

<br>

### 3.12.2 Select Squares

```csharp
var squares = numbers.Select(x => x * x);
```

Meaning

```
Take every number

↓

Multiply it by itself

↓

Return the new collection
```

<br>

## 3.13 When Should You Use Lambdas?

Use lambdas when:

- Passing logic to delegates
- Working with LINQ
- Using Action, Func, and Predicate
- Writing short methods
- Event handling
- Callback functions

Avoid lambdas when:

- The logic is very large.
- The code becomes difficult to read.
- The same logic is reused in many places (create a named method instead).

<br>

## 3.14 Advantages

- Less code
- Easier to read
- No need to create separate methods
- Works perfectly with LINQ
- Makes delegate usage much simpler
- Cleaner and more maintainable code

<br>

## 3.15 Summary

```
Normal Method
        ↓
Delegate
        ↓
Anonymous Method
        ↓
Lambda Expression
```

A lambda is simply an **anonymous method written in a much shorter and cleaner way**.

Think of it as:

```
Method
        ↓
Remove name
        ↓
Replace with =>
        ↓
Lambda Expression
```

<br>

## 3.16 Interview Questions

### 3.16.1 What is a Lambda Expression?

A lambda expression is a concise way of writing an anonymous method using the `=>` operator. It is commonly used with delegates and LINQ.

<br>

### 3.16.2 What does `=>` mean?

It is called the **Lambda Operator**. It separates the parameters from the method body.

<br>

### 3.16.3 Can a lambda have multiple statements?

Yes.

```csharp
x =>
{
    Console.WriteLine(x);
    return x * x;
}
```

<br>

### 3.16.4 What's the Difference Between an Anonymous Method and a Lambda?

Anonymous Method

```csharp
delegate(int x)
{
    return x * x;
}
```

Lambda

```csharp
x => x * x
```

Lambda expressions are shorter, cleaner, and the preferred modern syntax.

<br>

### 3.16.5 Where are Lambda Expressions Commonly Used?

- LINQ
- Delegates
- Events
- Action
- Func
- Predicate
- Callbacks
- Asynchronous programming
