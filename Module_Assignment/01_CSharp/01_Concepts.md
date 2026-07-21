## Architecture

<br>
<div align = "center">
<img width="700" alt="image" src="https://github.com/user-attachments/assets/d0d2d4a6-b8b5-4648-aeee-02ccb99c197c" />
</div>
<br>

## Model

Repo Returns
```js
Movie
{
    Id = 1,
    Name = "Avatar",

    ActorIds = 
    [
        1,2
    ],

    Producer Id = 1
}
```
mapping convert to
```js
MovieResponseDTO
{
    Id = 1,
    Name = "Avatar",

    Actors =
    [
        Actor { Name = "Sam Worthington" },
        Actor { Name = "Zoe Saldana" }
    ],

    Producer =
        Producer { Name = "James Cameron" }
}
```
DTO hides the **internal model** from the presentation layer. 

Instead of exposing complete `Movie`, `Actor`, and `Producer` objects, it exposes only the required data, making the application more secure, loosely coupled, and easier to maintain.


# Flow

# 1. Models & DTOs

## Person (Base Class)

### Purpose
Acts as the parent class for `Actor` and `Producer` to avoid duplicate properties.

### Properties
- Id
- Name
- DOB

### Why?
Both Actor and Producer contain these common properties. Instead of duplicating them, inheritance is used.


<br>



## Actor

### Purpose
Represents an Actor in the application.

### Inherits
- Person

### Properties
Inherited from Person:
- Id
- Name
- DOB


<br>



## Producer

### Purpose
Represents a Producer in the application.

### Inherits
- Person

### Properties
Inherited from Person:
- Id
- Name
- DOB


<br>



## Movie

### Purpose
Represents the Movie entity stored in the repository.

### Properties
- Id
- Name
- YearOfRelease
- Plot
- List<int> ActorIds
- int ProducerId

### Why store IDs instead of objects?

Instead of storing complete Actor and Producer objects:

Movie
- ActorIds
- ProducerId

The IDs are later converted into complete objects while displaying data.

Benefits:
- Less duplication
- Better memory usage
- Cleaner relationships




---


<br>




## MovieRequest (DTO)

### Purpose
Transfers movie data from Program.cs to MovieService while adding a movie.

### Properties
- Name
- YearOfRelease
- Plot
- ActorIds
- ProducerId

### Why?
Program should not directly create repository entities.
It sends a DTO (request object) to the service layer.


<br>





## MovieResponse (DTO)

### Purpose
Transfers movie data from MovieService to Program.cs for displaying.

### Properties
- Id
- Name
- YearOfRelease
- Plot
- List<Actor> Actors
- Producer Producer

### Why?
Program needs complete Actor and Producer objects to display names.

MovieService maps:

ActorIds → Actor Objects

ProducerId → Producer Object

before returning MovieResponse.




---

<br>




# 2. Repository Layer

## Purpose

Repository Layer is responsible only for data storage and retrieval.

Responsibilities:
- Add Data
- Get Data
- Delete Data

It should **not contain**
- Validation
- Business Logic
- Mapping


<br>



# ActorRepository

## Add(Actor actor)

### Purpose
Stores a new actor inside the repository.


<br>



## Get()

### Purpose
Returns all actors.


<br>



## Get(int id)

### Purpose
Returns the actor with the specified ID.

Returns `null` if not found.


<br>



# ProducerRepository

## Add(Producer producer)

Stores a producer.


<br>


## Get()

Returns all producers.


<br>



## Get(int id)

Returns producer by ID.

Returns `null` if not found.


<br>



# MovieRepository

## Add(Movie movie)

Stores movie.


<br>



## Get()

Returns all movies.


<br>



## Get(int id)

Returns movie by ID.

Returns `null` if not found.





<br>



## Delete(int id)

Deletes the movie.

---

<br>



# 3. Service Layer

## Purpose

Service Layer contains all Business Logic.

Responsibilities:
- Business Validation
- Repository Coordination
- DTO Mapping
- Throw Business Exceptions


<br>



# ActorService

## Add(string name, DateTime dob)

### Purpose

Adds a new actor.

### Steps
1. Validate Name & DOB
2. Create Actor object
3. Save using Repository


<br>



## Get()

Returns all actors.


<br>



## Get(int id)

Returns actor by ID.

Throws InvalidDataException if actor does not exist.


<br>



## ValidateActorIds(List<int> actorIds)

### Purpose

Validates actor selection.

Checks:
- At least one actor selected.
- Every Actor ID exists.

Throws InvalidDataException if validation fails.


<br>



# ProducerService

## Add(string name, DateTime dob)

Adds producer after validation.


<br>



## Get()

Returns all producers.


<br>



## Get(int id)

Returns producer.

Throws InvalidDataException if producer doesn't exist.


<br>



# MovieService

## Add(MovieRequest request)

### Purpose

Adds a new movie.

### Steps

1. Validate Actor IDs.
2. Validate Producer.
3. Validate Movie.
4. Convert MovieRequest → Movie.
5. Store Movie.


<br>



## Get()

### Purpose

Returns all movies.

### Mapping Process

Repository returns:

Movie

Movie contains:

- ActorIds
- ProducerId

MovieService converts:

ActorIds → Actor Objects

ProducerId → Producer Object

Returns:

List<MovieResponse>


<br>



## Get(int id)

Returns one MovieResponse after mapping ActorIds and ProducerId.


<br>



## Delete(int id)

Deletes a movie after checking it exists.


<br>



## ValidateMovie()

Checks:
- Duplicate Movie
- Invalid Year

(Optional)
- Empty Name
- Empty Plot

---


<br>


# 4. Program.cs

## Purpose

Acts as the Presentation Layer (Console UI).

Responsibilities:
- Take User Input
- Display Output
- Call Service Methods
- Handle Exceptions

No business logic is written here.


<br>



## Main()

Application Entry Point.

Displays menu continuously until Exit.


<br>



## AddActor()

### Steps

- Read Actor Name
- Read DOB
- Call ActorService.Add()


<br>



## AddProducer()

### Steps

- Read Producer Name
- Read DOB
- Call ProducerService.Add()


<br>



## AddMovie()

### Steps

- Read Movie Name
- Read Year
- Read Plot
- Display Actors
- Read Actor IDs
- Display Producers
- Read Producer ID
- Create MovieRequest
- Call MovieService.Add()


<br>



## ListMovies()

Gets List<MovieResponse> from MovieService.

Displays:
- Movie Name
- Year
- Plot
- Actor Names
- Producer Name


<br>



## DeleteMovie()

### Steps

- Display Movies
- Read Movie ID
- Call MovieService.Delete()


<br>



## ReadInt()

Reads integer safely.

Throws InvalidInputException for invalid number.


<br>



## ReadString()

Reads non-empty string.



<br>


## ReadDate()

Reads valid date.

Throws InvalidInputException for invalid date.


<br>



## ReadActorIds()

Reads comma-separated Actor IDs.

Converts them into:

List<int>

Only validates input format.

Business validation is done in ActorService.


<br>



## ReadProducerId()

Reads Producer ID.

Only validates input format.

Business validation is done in ProducerService.


<br>


---

# 5. Exception Handling

The project contains two types of validation.

---

<br>




## A. Input Validation (Presentation Layer)

Handled inside Program.cs.

Checks:
- Invalid Integer
- Invalid Date
- Empty Input
- Invalid Comma-Separated Format

Throws:

InvalidInputException

Example:

Input:
abc

Output:

Invalid Input Exception Error!


<br>



## B. Business/Data Validation (Service Layer)

Handled inside Services.

Checks:
- Future DOB
- Duplicate Movie
- Invalid Actor ID
- Invalid Producer ID
- Empty Actor Selection
- Invalid Movie Data

Throws:

InvalidDataException

Example:

Future DOB

Output:

Invalid Data Exception Error!


<br>



# Exception Flow

User
↓
Program.cs
↓
Input Validation
↓
Service Layer
↓
Business Validation
↓
Repository

If business validation fails:

Service
↓
throw InvalidDataException
↓
Program.cs

catch (InvalidDataException e)
{
    Console.WriteLine(e.Message);
}


<br>



If input validation fails:

ReadInt()
ReadDate()
ReadString()
↓
throw InvalidInputException
↓
Program.cs

catch (InvalidInputException e)
{
    Console.WriteLine(e.Message);
}


<br>



# Overall Architecture

Program.cs
↓
Service Layer
↓
Repository Layer
↓
Models

Program.cs
- Console UI
- Input Validation
- Exception Handling

Service Layer
- Business Logic
- Business Validation
- Mapping
- Coordinates Repositories

Repository Layer
- CRUD Operations
- Data Storage

Models
- Represent Application Entities

DTOs
- Transfer Data Between Layers
- MovieRequest → Input DTO
- MovieResponse → Output DTO


<br>

---

<br>



## Dependency Injection
Dependency Injection is a design pattern where an object receives the objects it depends on from the outside, instead of creating them itself.

It reduces coupling

Program.cs
```csharp
ActorService actorService = new ActorService(new ActorRepository()); // DI
```

And inside\
ActorSerrvice.cs
```csharp
private readonly IActorRepository _actorRepository;

public ActorService(IActorRepository actorRepository)
{
    _actorRepository = actorRepository;
}
```

<br>

### Normal
```csharp
ActorService actorService = new ActorService(); 
```

And inside\
ActorSerrvice.cs
```csharp
private readonly IActorRepository _actorRepository;
 
public ActorService(IActorRepository actorRepository)
{
    _actorRepository = new ActorRepository();
}
```

<br>

## Note
We build in this order
```
✓ Models
✓ Repositories
✓ Services
✓ MovieService

⬇

Program.cs
```


Q. thnks, but why program agains interface instead of class

Q. Why what when use, what if not used


## TryParse vs TryParseExact for datetime
```csharp
if (!DateTime.TryParse(Console.ReadLine(), out DateTime dob))
{
    throw new InvalidDataException("Invalid date.");
}
```

Accepts inputs like:
```
12/10/2002
12-10-2002
October 12, 2002
2002-10-12
```
The exact accepted formats depend on the current culture.


```csharp
if (!DateTime.TryParseExact(
        Console.ReadLine(),
        "dd/MM/yyyy",
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        out DateTime dob))
{
    throw new InvalidDataException("Enter date in dd/MM/yyyy format.");
}
```
This accepts only:
```
12/10/2002
```
It rejects:
```
12-10-2002
2002/10/12
October 12, 2002
```

## Future validation

```csharp
if (movie.YearOfRelease > DateTime.Now.Year)
    throw new InvalidDataException("Movie year cannot be in the future.");
```

## Why DTOs?

Currently your flow is:
```
Program.cs
      │
      ▼
MovieService.Add(...)
      │
      ▼
Movie Model
      │
      ▼
Repository
```
You're directly passing the Movie model between layers.

With DTOs, the flow becomes:
```
Program.cs
      │
      ▼
MovieRequestDTO
      │
      ▼
MovieService
      │
(converts)
      ▼
Movie Model
      │
      ▼
Repository
```
and when retrieving movies:
```
Repository
      │
Movie Model
      │
(converts)
      ▼
MovieResponseDTO
      │
      ▼
Program.cs
```
Notice:
- Repository still works with Movie
- Program never sees Movie
- Service converts between DTO and Model

This is exactly how APIs are written
