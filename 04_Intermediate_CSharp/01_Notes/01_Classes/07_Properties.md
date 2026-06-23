# 1.7 Properties

Properties provide a controlled way to access the data of an object. They are one of the most commonly used features in C# and are the preferred way to expose class data.

Instead of exposing fields directly, we expose **properties**.

<br>

---

> [!Tip]
> Q What is it?\
> A class member that encapsulates a getter/setter for accessing a field
>
> Q Why do need a property?\
> To crate getter/setter with less code


> [!caution]
> Properties does not remove the use of constructors, change any extra behaviour, don't confuse
>
> They are just reducing extra lines of code and add syntax simplicity, safe input validation , and controlled access


---



<br>

<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/0ebdeb4e-311a-4e17-a09d-c5cae658f4d2" />
<p>This is without a proper property,</p>
<p>Problem with this is that, with increasing demand of private vars, defining such methods gets inconvienient</p>
 <p>So we will use</p>
    <img width="400" alt="image" src="https://github.com/user-attachments/assets/84875b04-4b1e-423d-9d5e-fc07ce11f6b1" />
</div>

`get` and `set` are csharp methods

<br>

# What is a Property?

A **property** is a class member that provides controlled access to a private field using **get** and **set** accessors.

* `get` returns the value.
* `set` assigns a value.

Properties combine the **simplicity** of _fields_ with the **safety** of _methods_. 🏷️

<br>

# Why Do We Need Properties?

Consider the following class:

```csharp
class Employee
{
    public int Age;
}
```

Anyone can assign an invalid value.

```csharp
employee.Age = -10;
```

To prevent this, make the field private and expose it through a property.

```csharp
class Employee
{
    private int age;

    public int Age
    {
        get { return age; }
        set
        {
            if (value >= 0)
                age = value;
        }
    }
}
```

Now invalid values cannot be assigned.

<br>

# Property Syntax

```csharp
private dataType fieldName;

public dataType PropertyName
{
    get
    {
        return fieldName;
    }

    set
    {
        fieldName = value;
    }
}
```

Example

```csharp
class Person
{
    private string name;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
}
```

Usage

```csharp
Person person = new Person();

person.Name = "Yash";

Console.WriteLine(person.Name);
```

<br>

# Understanding `get` and `set`

The `get` accessor executes whenever the property's value is read.

```csharp
string name = person.Name;
```

The `set` accessor executes whenever a value is assigned.

```csharp
person.Name = "Yash";
```

Inside the `set` accessor, the keyword `value` represents the value being assigned.

```csharp
set
{
    name = value;
}
```

<br>

# Automatic Properties (Auto-Implemented Properties)

If no additional logic is required, C# can automatically create the private backing field.

<div align = "center">
    <img width="550" alt="image" src="https://github.com/user-attachments/assets/e06d796f-1136-420c-86ea-880e47cb42e2" />
</div>


Instead of writing

```csharp
private string name;

public string Name
{
    get { return name; }
    set { name = value; }
}
```

you can simply write

```csharp
public string Name { get; set; }
```

This is called an **Auto-Implemented Property**.

It is the most commonly used type of property in modern C#.

<br>

# Read-Only Properties

A property with only a `get` accessor cannot be modified outside the class.

```csharp
public string Company
{
    get;
}
```

or

```csharp
public string Company { get; }
```

The value can be assigned inside the constructor.

```csharp
class Employee
{
    public string Company { get; }

    public Employee()
    {
        Company = "ABC Ltd";
    }
}
```

<br>

# Properties with Validation

One of the biggest advantages of properties is validation.

```csharp
class Employee
{
    private int age;

    public int Age
    {
        get
        {
            return age;
        }

        set
        {
            if (value >= 18)
                age = value;
        }
    }
}
```

Now only valid ages are accepted.

<br>

# Field vs Property

| Field                             | Property                       |
| --------------------------------- | ------------------------------ |
| Stores data                       | Controls access to data        |
| No validation                     | Can perform validation         |
| Usually private                   | Usually public                 |
| Simple variable                   | Uses `get` and `set` accessors |
| Not recommended for public access | Preferred for exposing data    |

<br>


## **Examples:**
```
Conventions:

Keep Auto properties at top

Then  Constructors 

Then Calculative Properties
```
**Example 1:**
```csharp
namespace CSharpdoubleermediate
{
    public class Employee
    {
        // ============================== TOP DECLARATION AREA =========================

        // Auto Property
        public string name { get; set; }

        // Private variable, no direct access, but can have controlled access
        private int _salary;



        // =============================== CONSTRUCTOR  =================================

        // Default constructor
        public Employee()
        {
            name = "Anonymous";
        }

        // Paramterized Constructor
        public Employee(int sal)
        {
            Salary = sal;
        }



        // ============================= PUBLIC ACCESSORS ===============================

        // Calculative property (Tell how can we access Private, add restriction < 0)
        public int Salary
        {
            get { return _salary; }
            set
            {
                if (value > 0)
                {
                    _salary = value;
                    //value = _salary; ///silly
                }
            }
        }

        // ==============================================================================
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            //Emp1 object
            //Employee emp1 = new Employee(-100); //set salary value to 0
            Employee emp1 = new Employee(100);
            emp1.name = "AP";

            Console.WriteLine(emp1.name); // AP
            Console.WriteLine(emp1.Salary); //100

            /*
             Here you need parametrized constructor

             we used traditional value setting, no initializer

             For input validation, and controlled access to private variable '_salary'
             we used property

             */

            //===============================================================

            // Emp2 Object
            Employee emp2 = new Employee()
            {
                name = "Yash", //if not passed , sets default "Anonumous"
                Salary = 200,
            };
            Console.WriteLine(emp2.name); //Yash
            Console.WriteLine(emp2.Salary); //200

            /*
             Here you need Default constructor

             but instead of traditional value set, we used initializer

             For input validation, and controlled access to private variable '_salary'
             we used property

             */

            //===============================================================
        }
    }
}

```

<br>

**Example 2:**
```csharp
namespace CSharpdoubleermediate
{
    public class Person
    {

        // Auto Properties
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime BirthDate
        {
            get;
            //set;
            private set; // if you want only 1 time bday set
        }


        // Constructor
        public Person(DateTime birthDate)
        {
            BirthDate = birthDate;   
        }

        
        // Calculative Properties
        public int Age  //Age property, set not needed for age
        {
            get 
            {
                var timeSpan = DateTime.Today - BirthDate;
                var years = timeSpan.Days / 365;
            
                return years;
            }
        }
    }

    internal partial class Program
    {
        public static void Main(string[] args)
        { 
            // With constructor
            Person person = new Person(new DateTime(2004, 09, 25));
       

            // Without constructor
            //Person person = new Person();
            //person.BirthDate = new DateTime(2004, 09,25);


            Console.WriteLine(person.Age);
            // Accesing private object
           
        }
    }
}
```
```csharp
// Traditional

namespace CSharpdoubleermediate
{
    public class Person
    {
        private DateTime _birthdate; //private object inside class

        public void SetBirthDate(DateTime birthdate) //public accesors
        {
            _birthdate = birthdate;
        }

        public DateTime GetBirthdate()
        {
            return _birthdate;
        }
    }

    internal partial class Program
    {
        public static void Main(string[] args)
        {
            Person person = new Person();

            // Accesing private object
            person.SetBirthDate(new DateTime(2004, 09, 25));
            Console.WriteLine(person.GetBirthdate());
        }
    }
}

```

<br>


# Expression-Bodied Property (Advance - Extras)

For simple read-only properties, C# provides a shorter syntax.

```csharp
public string FullName => $"{FirstName} {LastName}";
```

This is equivalent to

```csharp
public string FullName
{
    get
    {
        return $"{FirstName} {LastName}";
    }
}
```

<br>

# Best Practices

* Keep fields private.
* Expose data through properties.
* Use auto-properties whenever validation is not required.
* Use custom properties when validation or additional logic is needed.
* Give properties meaningful PascalCase names.

<br>

# Common Mistakes

## Making fields public instead of using properties

Incorrect

```csharp
public string Name;
```

Better

```csharp
public string Name { get; set; }
```

<br>

## Forgetting validation

```csharp
employee.Age = -20;
```

Use properties to validate incoming values.

<br>

## Writing unnecessary backing fields

Don't write

```csharp
private string name;

public string Name
{
    get { return name; }
    set { name = value; }
}
```

unless additional logic is needed.

Instead, use

```csharp
public string Name { get; set; }
```

<br>

# Interview Questions

### What is a property?

A property is a class member that provides controlled access to a private field using `get` and `set` accessors.

<br>

### Why are properties preferred over public fields?

Properties support validation, encapsulation, and future flexibility without changing the class interface.

<br>

### What is an auto-implemented property?

A property where the compiler automatically creates the private backing field.

Example

```csharp
public string Name { get; set; }
```

<br>

### What is the purpose of the `value` keyword?

Inside the `set` accessor, `value` represents the value being assigned to the property.

<br>

### When should you use a custom property instead of an auto-property?

When you need validation, calculations, logging, or any additional logic while getting or setting a value.

<br>

# Summary

* Properties provide controlled access to object data.
* They use `get` and `set` accessors.
* Keep fields private and expose properties instead.
* Use auto-properties for simple data storage.
* Use custom properties when validation or additional logic is required.
* Properties are an essential part of implementing encapsulation in C#.
