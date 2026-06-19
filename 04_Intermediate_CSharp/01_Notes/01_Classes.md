# 1. Classes

Classes are the foundation of Object-Oriented Programming (OOP) in C#. Every application you build will contain classes. A class acts as a blueprint that defines what an object should contain (data) and what it can do (behavior).




<br>

# 1.1 Introduction to Classes

## What is a Class?

A **class** is a user-defined data type that groups together related data and functions into a single unit.

* Data is stored using **fields** or **properties**.
* Behavior is defined using **methods**.
* Objects are created from classes.

Think of a class as a blueprint and an object as the actual item built from that blueprint.


<div align = "center">
<img width="641" height="353" alt="image" src="https://github.com/user-attachments/assets/8827f384-8498-4507-bfac-0d22d77bbfeb" />
    <p>eg. UML Classes </p>
</div>

### Real-World Analogy


<div align = "center">
    <img width="643" height="316" alt="image" src="https://github.com/user-attachments/assets/74fc79ad-1ab4-4bca-af3d-166669de48fd" />
    <p>eg. Application Layer Classes </p>
</div>

**Blueprint → House**

A blueprint describes:

* Number of rooms
* Color
* Doors
* Windows

Using the same blueprint, multiple houses can be built.

Similarly,

* **Class = Blueprint**
* **Object = Actual House**

One class can create many objects.




<div align = "center">
<img width="548" height="395" alt="image" src="https://github.com/user-attachments/assets/8b3eea93-4b9d-4ef8-a2a8-cb0f6eb8336e" />
    <p>Class Members Instance Type and Static Type </p>
</div>


<br>



## Why Do We Need Classes?

Without classes, programs become difficult to organize as they grow.

Classes help us:

* Organize related code together.
* Reuse code.
* Reduce duplication.
* Improve readability.
* Improve maintainability.
* Model real-world entities.

Instead of storing unrelated variables everywhere, we group them inside a class.

Bad approach:

```csharp
string name;
int age;
double salary;
```

Better approach:

```csharp
class Employee
{
    public string Name;
    public int Age;
    public double Salary;
}
```

Everything related to an employee stays together.

<br>

## What Does a Class Contain?

A class can contain multiple members.

| Member         | Purpose                       |
| -------------- | ----------------------------- |
| Fields         | Store data                    |
| Properties     | Controlled access to data     |
| Constructors   | Initialize objects            |
| Methods        | Define behavior               |
| Indexers       | Access objects like arrays    |
| Events         | Notify other objects          |
| Nested Classes | Define classes inside classes |

In this chapter, we'll learn these members one by one.

<br>

## Basic Class Syntax

```csharp
class Person
{
    public string Name;

    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name}");
    }
}
```

Creating an object:

```csharp
Person person = new Person();

person.Name = "Yash";
person.Introduce();
```

Output

```
Hi, I'm Yash
```

<br>

## Understanding the Example

```csharp
class Person
```

Defines a new class named `Person`.

<br>

```csharp
public string Name;
```

A field that stores the person's name.

<br>

```csharp
public void Introduce()
```

A method that defines the behavior of the object.

<br>

```csharp
Person person = new Person();
```

Creates a new object in memory.

`new` allocates memory and returns a reference to that object.

<br>

```csharp
person.Name = "Yash";
```

Assigns a value to the object's field.

<br>

```csharp
person.Introduce();
```

Calls the object's method.

<br>

## Class vs Object

| Class                         | Object                   |
| ----------------------------- | ------------------------ |
| Blueprint                     | Instance of a class      |
| Defines structure             | Represents a real entity |
| No memory for individual data | Occupies memory          |
| Created once                  | Can create many objects  |

Example

```csharp
class Car
{
    public string Brand;
}
```

Objects

```csharp
Car car1 = new Car();
Car car2 = new Car();
Car car3 = new Car();
```

One class.

Three independent objects.

Each object has its own data.

```csharp
car1.Brand = "BMW";
car2.Brand = "Audi";
car3.Brand = "Mercedes";
```

Changing one object does not affect the others.

<br>

## How Objects Are Stored in Memory

```csharp
Person p1 = new Person();
Person p2 = new Person();
```

Memory representation

```
Stack                    Heap

p1 --------------------> Person Object

p2 --------------------> Person Object
```

The variables (`p1`, `p2`) are references stored on the stack.

The actual objects are created on the heap.

Each `new` keyword creates a completely new object.

<br>

## Naming Conventions

Class names should:

* Use PascalCase.
* Be singular.
* Represent a noun.
* Clearly describe the object.

Good examples

```text
Customer
Employee
Order
Product
Invoice
Student
```

Poor examples

```text
customer
employeeData
obj1
myClass
test
```

<br>

## Best Practices

* Keep each class focused on a single responsibility.
* Use meaningful class names.
* Group related data and behavior together.
* Avoid creating large "God Classes" that do everything.
* Follow PascalCase naming for classes.

<br>

## Common Mistakes

### Creating a class but forgetting to create an object

Incorrect

```csharp
Person.Name = "Yash";
```

Correct

```csharp
Person person = new Person();
person.Name = "Yash";
```

<br>

### Putting unrelated members in one class

Bad

```csharp
class Employee
{
    public string Name;
    public decimal Salary;

    public void CalculateArea()
    {
    }
}
```

`CalculateArea()` has nothing to do with an employee.

<br>

### Using one class for every purpose

Avoid classes that contain hundreds of methods and fields.

Instead, divide responsibilities into smaller classes.

<br>

## Interview Questions

### What is a class?

A class is a user-defined reference type that acts as a blueprint for creating objects. It groups related data and behavior into a single unit.

<br>

### What is an object?

An object is an instance of a class that occupies memory and contains its own data.

<br>

### What is the difference between a class and an object?

A class defines the structure, while an object is a real instance created from that structure.

<br>

### Can one class create multiple objects?

Yes. A single class can create any number of independent objects.

<br>

### Where are objects stored?

Objects are allocated on the heap, while reference variables are stored on the stack.

<br>

## Summary

* A class is a blueprint for creating objects.
* Objects are instances of classes.
* Classes combine data and behavior into a single unit.
* Multiple objects can be created from one class.
* Objects are created using the `new` keyword.
* Each object maintains its own independent state.
* Classes are the foundation of Object-Oriented Programming in C#.
* Well-designed classes improve readability, maintainability, and code reusability.
