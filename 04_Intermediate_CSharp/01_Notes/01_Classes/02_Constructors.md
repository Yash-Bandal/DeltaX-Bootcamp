# 1.2 Constructors

Constructors are special methods that are automatically called when an object is created. Their primary purpose is to initialize an object with a valid starting state.

<br>

---
> [!Note]
> 1. We dont need to always define a constructor, it is only needed when we have to initialize object on creation
>     - Because there is always a default constructor
>  2. It does nothing much, it's just used to initialize fields of class to their [Default values](https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/main/04_Intermediate_CSharp/01_Notes/01_Classes/02_Constructors.md#default-values-before-constructor-runs)
>      - If any **numbers/integers**, set to initial `0`
>      - **Boolean** to `false`
>      - **Reference type** (eg Arrays) set to `null`
>      -  **Character** to `empty char`
---

---
> [!Important]
> Note, that when we create `1` or `1+`  **Custom** Constructor, the compiler does not create **Default Constructor**
>
> But, if you havent defined a custom constructor, a default constructor is auto initialized and created by compiler,
>
> You cant see it, but if you want to inspect you can check `IL` code
> 
> eg
> ```csharp    
>    public class Customer
>    {
>        public int Id;
>        public string Name;
>
>        // No default Constructor
> 
>        public Customer(int id)  //paramterized custom constructor 1
>        {
>            this.Id  = id;
>        }
>
>        public Customer(string name, int id) //paramterized custom constructor 1
>        {
>            this.Id = id;
>            this.Name = name;
>        }
>    }
>
> class Program
> {
>     public static void Main(string[] args)
>     {
>        // Customer customer = new Customer(); //Gives error
>        Customer customer = new Customer(1);
> 
>        Console.WriteLine(customer.Name);
>        Console.WriteLine(customer.Id);
>     }
> }
> ```
>
---



<br>

# What is a Constructor?

A **constructor** is a special member of a class that executes automatically whenever an object is created using the `new` keyword.

Unlike regular methods, constructors:

* Have the same name as the class.
* Do not have a return type (not even `void`).
* Are called automatically.
* Are mainly used to initialize object data.

<br>

<div align = "center">
<img width="414" height="371" alt="image" src="https://github.com/user-attachments/assets/db03d98e-2f39-479d-9a7f-cd075a4e050f" />
  <p>Defining a Constructor</p>
</div>

<br>

## Why Do We Need Constructors?

Imagine creating an object that must always contain valid information.

Without a constructor:

```csharp
Person person = new Person();

person.Name = "Yash";
person.Age = 22;
```

If you forget to assign values:

```csharp
Person person = new Person();
```

The object exists, but it may contain invalid or default values.

Constructors allow us to initialize objects immediately after creation.

```csharp
Person person = new Person("Yash", 22);
```

Now every object starts with meaningful data.

<br>

# Constructor Syntax

```csharp
class Person
{
    public Person()
    {

    }
}
```

Notice:

* Constructor name is `Person`.
* No return type.
* Automatically called when the object is created.

<br>

<div align = "center">
  <img width="400"  alt="image" src="https://github.com/user-attachments/assets/c9dbfb4a-6ba5-4edc-a0d1-f4d6f802bd50" />
</div>

<br>

# Default Constructor

A constructor without parameters is called the **Default Constructor**.

Example:

```csharp
class Person
{
    public string Name;

    public Person()
    {
        Name = "Unknown";
    }
}
```

Usage

```csharp
Person person = new Person();

Console.WriteLine(person.Name);
```

Output

```
Unknown
```

The constructor automatically initializes the `Name`.

<br>

# Parameterized Constructor

A constructor can accept parameters to initialize the object.

Example

```csharp
class Person
{
    public string Name;
    public int Age;

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

Creating objects

```csharp
Person person1 = new Person("Yash", 22);

Person person2 = new Person("Rahul", 25);
```

Each object receives different values.

<br>

# Multiple Constructors (Constructor Overloading)


- Constructor Overloading is simply having multiple constructors (Obviously with the same name 😅) in a class,
- What uniquely identifies a method is its `Signature` (Like 2 people with same name shall be identified with their different signature)
    - Signatures include return type, var names, types,and number of parameters
 - We **`Need`** Constructor Overloading  / Multiple Constructors to simplify initialization, sometimes we may just know name, sometimes name and id
 - So based on Availability of arguments we have to pass, we use multiple Constructors

<br>

<div align = "center">
<img width="600"  alt="image" src="https://github.com/user-attachments/assets/46dec33a-9c80-44f8-9606-513dac2d0617" />
</div>



A class can have multiple constructors as long as their parameter lists are different.

Example

```csharp
class Person
{
    public string Name;
    public int Age;

    public Person()
    {
        Name = "Unknown";
        Age = 0;
    }

    public Person(string name)
    {
        Name = name;
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

- If we know nothing, we can set default values and `Default Constructor` will be auto calledd
- If we just know **name**, we can call Single `Parameterized Constructor` Person(name)
- If we know both **name**, and **age**, we  can have Double `Parameterized Constructor` Person(name, age)

Usage

```csharp
Person p1 = new Person();

Person p2 = new Person("Yash");

Person p3 = new Person("Yash", 22);
```

This is called **Constructor Overloading**.

<br>

# Constructor Chaining

Instead of repeating initialization code, one constructor can call another using the `this` keyword.



Example

```csharp
class Person
{
    public string Name;
    public int Age;

    public Person()
        : this("Unknown", 0)
    {
    }

    public Person(string name)
        : this(name, 0)
    {
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

Benefits

* Eliminates duplicate code.
* Keeps initialization logic in one place.
* Easier to maintain.

<br>

## `this` keyword
- The `this` keyword refers to the current object (current instance of the class).
- `this` is often used to distinguish between **a field** and a **parameter** with the *same name*.


<div align = "center">
<img width="600"  alt="image" src="https://github.com/user-attachments/assets/4174caec-e03e-4e2e-a497-05d2f1161044" />
</div>

```csharp
public class Person
{
    public string Name;

    public Person(string Name)
    {
        this.Name = Name;
    }
}

// Left side is member of class `Name`
// Right side is recieved one `name`
```
Here:

- **`this.Name`** → Field (belongs to the object)
- **`Name`** → Constructor parameter

Without this, C# wouldn't know which **`Name`** you mean.

<br>

# Constructor Execution Flow

```csharp
Person person = new Person("Yash", 22);
```

Execution steps:

1. Memory is allocated for the object.
2. Fields receive default values.
3. The matching constructor executes.
4. Constructor initializes the object.
5. Reference to the object is returned.

<br>

# Default Values Before Constructor Runs

Before any constructor executes, fields receive default values.

| Data Type       | Default Value |
| --------------- | ------------- |
| int             | 0             |
| double          | 0.0           |
| bool            | false         |
| char            | '\0'          |
| string          | null          |
| Reference Types | null          |

Example

```csharp
class Student
{
    public int Age;
    public string Name;

    public Student()
    {
        Console.WriteLine(Age);
        Console.WriteLine(Name);
    }
}
```

Output

```
0
null
```

After this, the constructor can assign meaningful values.

<br>

# Constructors vs Methods

| Constructor                   | Method                       |
| ----------------------------- | ---------------------------- |
| Same name as class            | Can have any valid name      |
| No return type                | Has a return type or `void`  |
| Called automatically          | Called explicitly            |
| Initializes objects           | Performs operations          |
| Runs once per object creation | Can be called multiple times |

<br>




### **Constructors Example:**

**Program.cs**
```csharp
namespace CSharpIntermediate
{
    internal partial class Program
    {
        public static void Main(string[] args)
        {
            // ==================================================
            // Default Constructor usage
            Console.WriteLine("Default Constructor call");
            
            Customer customer1 = new Customer();
            Console.WriteLine(customer1.Name);
            Console.WriteLine(customer1.Id);

            Order order = new Order();
            customer1.Orders.Add(order);
            /*
            Default Constructor call
                 <-- (No value here)
            0
            */

            // ====================================================

            // Parametrized
            Console.WriteLine("Parametrized Constructor call");

            Customer customer2 = new Customer(1);
            customer2.Name = "Darshan";

            //customer2.Orders.Add(order); can use only if param costruc has Order defined

            Console.WriteLine(customer2.Name);
            Console.WriteLine(customer2.Id);
            /*
            Parametrized Constructor call
            Darshan
            1
            */

            // ========================================================

            // Paramterized full
            Console.WriteLine("Parametrized Constructor Full Call");
            
            Customer customer3 = new Customer("Yash",2);
            // No need because of Constructor
            Console.WriteLine(customer3.Name);
            Console.WriteLine(customer3.Id);
            /*
            Parametrized Constructor Full Call
            Yash
            2
            */  

            // ==========================================================
        }
    }
}
```
**Order.cs**
```csharp
namespace CSharpIntermediate
{
        public class Order
        {
        }
}
```

**Customer.cs**
```csharp
using System.Collections.Generic;
namespace CSharpIntermediate
{
    public class Customer
    {
        public int Id;
        public string Name;
        public List<Order> Orders;
        public Customer()
        {
            Orders = new List<Order>();
        }
        public Customer(int id)
        {
            this.Id = id;
        }

        public Customer(string name, int id) 
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
```
Using this() to avoid repeated re writing and re initializing vars
```csharp
using System.Collections.Generic;
namespace CSharpIntermediate
{
    public class Customer
    {
        public int Id;
        public string Name;
        public List<Order> Orders;
        public Customer()
        {
            Orders = new List<Order>();
        }
        public Customer(int id) : this() //this() tells the compiler to call Default constructor before current paramterized
        {
            this.Id = id;
        }

        public Customer(string name, int id) : this(id)
        {
            this.Name = name;
        }
    }
}

/*
 think this represents the constructor
this() -> Default Constructor
this(1) -> parametrized constructor 1
 */
```
**Output**
```
 Default Constructor call
     <-- (No value here)
0

Parametrized Constructor call
Darshan
1

Parametrized Constructor Full Call
Yash
2
```



<br>

# When Should You Use Constructors?

Use constructors when:

* An object requires mandatory data.
* Default values should be assigned.
* Initialization logic must run automatically.
* Dependencies need to be provided during object creation.

Examples

* Database connection.
* Employee details.
* Product information.
* File configuration.
* API client initialization.

<br>

# Best Practices

* Keep constructors simple and focused on initialization.
* Initialize objects into a valid state.
* Avoid complex business logic inside constructors.
* Use constructor overloading when multiple initialization options are needed.
* Use constructor chaining to avoid duplicate code.

<br>

# Common Mistakes

## Forgetting the constructor name must match the class

Incorrect

```csharp
class Person
{
    public void Person()
    {
    }
}
```

This is **not** a constructor.

It is a normal method because of the `void` keyword.

Correct

```csharp
class Person
{
    public Person()
    {
    }
}
```

<br>

## Adding a return type

Incorrect

```csharp
public int Person()
{
}
```

Constructors never have a return type.

<br>

## Duplicating initialization code

Bad

```csharp
public Person()
{
    Name = "Unknown";
    Age = 0;
}

public Person(string name)
{
    Name = name;
    Age = 0;
}
```

Better

```csharp
public Person()
    : this("Unknown", 0)
{
}

public Person(string name)
    : this(name, 0)
{
}
```

<br>

# Interview Questions

### What is a constructor?

A constructor is a special member of a class that is automatically executed when an object is created. It is primarily used to initialize the object's state.

<br>

### Can a constructor return a value?

No. Constructors never have a return type.

<br>

### Can a class have multiple constructors?

Yes. This is called **constructor overloading**.

<br>

### What is a default constructor?

A constructor with no parameters.

<br>

### What is a parameterized constructor?

A constructor that accepts one or more parameters to initialize an object.

<br>

### What is constructor chaining?

Constructor chaining is the process of calling one constructor from another using the `this` keyword to reuse initialization logic.

<br>

### When is a constructor executed?

Immediately after an object is created using the `new` keyword.

<br>

## Summary

* Constructors initialize objects during creation.
* They have the same name as the class and no return type.
* Constructors execute automatically when an object is instantiated.
* Constructors can be overloaded.
* Constructor chaining reduces duplicate initialization code.
* Constructors help ensure every object starts in a valid state.
* Keep constructors simple and focused on initialization.
