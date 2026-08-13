# RESTful API 

<br>

# HTTP (HyperText Transfer Protocol)

HTTP is the communication protocol used by the Web. It defines how a client (browser/app) sends requests to a server and how the server sends responses.

### Flow

```
Client (Browser/App)
       |
   HTTP Request
       |
     Server
       |
   HTTP Response
       |
Client receives data
```

### Example

```
GET /users/1 HTTP/1.1
```

Server Response

```json
{
    "id": 1,
    "name": "Yash"
}
```

<br>

# What is a Web Service?

A **Web Service** is a server-side application (piece of code) that exposes functionality over the internet so other applications can use it.

It listens for HTTP requests, executes code, and returns a response.

### Example

Suppose you have a C# method

```csharp
public User GetUser(int id)
{
    // Fetch from database
}
```

If this method is exposed over HTTP,

```
GET /users/5
```

internally calls

```csharp
GetUser(5);
```

and returns

```json
{
    "id":5,
    "name":"Yash"
}
```

This is a **Web Service**.

<br>

# What is an API?

API (Application Programming Interface) is a set of rules that allows two software applications to communicate.

An API defines

- What requests can be made
- What data should be sent
- What response will be returned

### Example

```
Weather App
        |
        | API Request
        |
Weather Server
        |
Returns Temperature
```

<br>

# What is REST?

REST (Representational State Transfer) is an **architectural style** for designing web services.

**Important**

- REST is **NOT a protocol**
- REST is **NOT an official standard**
- REST is a collection of architectural principles
- REST generally uses HTTP

Interview Definition

> REST is an architectural style that defines a set of constraints for designing scalable web APIs over HTTP.

<br>

# Motivation Behind REST

REST was created to capture the characteristics that made the Web successful.

Main ideas

- URI Addressable Resources
- HTTP Protocol
- Request → Response communication

Example

```
Client
   |
GET /users/1
   |
Server
   |
Returns JSON
```

<br>

# REST Uses HTTP Methods

REST makes proper use of HTTP methods.

| Method | Purpose |
|------|---|
| GET | Read Data |
| POST | Create Data |
| PUT | Replace Entire Resource |
| PATCH | Update Part of Resource |
| DELETE | Delete Resource |

<br>

# REST Uses Existing Standards

REST itself is not a standard.

It uses existing standards like

- HTTP
- URI / URL
- JSON
- XML
- HTML
- Images
- MIME Types

<br>

# Core Concepts of REST

## 1. Resource

A Resource is any object or data exposed by the server.

Examples

- User
- Movie
- Product
- Order

Every resource has a unique URI.

Example

```
/users
/users/1
/movies/25
/orders/15
```

<br>

## 2. URI (Uniform Resource Identifier)

A URI uniquely identifies a resource.

Example

```
https://api.company.com/users/1
```

Here,

Resource = User

Id = 1

<br>

## 3. Verbs (HTTP Methods)

HTTP methods tell the server what action should be performed.

| Verb | Meaning |
|------|---|
| GET | Read |
| POST | Create |
| PUT | Replace |
| PATCH | Partial Update |
| DELETE | Remove |

<br>

## 4. Representation

Representation is the format in which data is returned.

Most common formats

- JSON ⭐
- XML

Example JSON

```json
{
    "id":1,
    "name":"Yash"
}
```

<br>

# REST API Examples

## Get All Users

```
GET /v1/users
```

Returns

```json
[
    {
        "id":1,
        "name":"Yash"
    },
    {
        "id":2,
        "name":"Kshitij"
    }
]
```

<br>

## Get Single User

```
GET /v1/users/1
```

Returns

```json
{
    "id":1,
    "name":"Yash"
}
```

<br>

## Create User

```
POST /v1/users
```

Body

```json
{
    "firstName":"Kshitj",
    "lastName":"Nangare"
}
```

<br>

## Update Entire User

```
PUT /v1/users/2
```

Body

```json
{
    "firstName":"Kshitij",
    "lastName":"Nangare"
}
```

PUT replaces the complete resource.

<br>

## Update Only Last Name

```
PATCH /v1/users/2
```

Body

```json
{
    "lastName":"Nangare"
}
```

PATCH updates only specified fields.

<br>

## Delete User

```
DELETE /v1/users/2
```

Deletes user with Id = 2.

<br>

# ASP.NET Core

ASP.NET Core is Microsoft's

- Cross-platform
- Open-source
- High-performance

framework for building

- Web APIs
- Web Applications
- Cloud Applications
- Mobile Backends
- Microservices

Supports

- Windows
- Linux
- macOS

<br>

# MVC (Model View Controller)

MVC separates an application into three parts.

```
Request
   |
Controller
   |
Business Logic
   |
Model
   |
Database
   |
Controller
   |
View / JSON Response
```

## Model

Represents data.

Examples

- Movie
- Actor
- Producer

<br>

## View

Represents the User Interface.

Examples

- HTML Page
- Razor View

For Web APIs, usually JSON is returned instead of a View.

<br>

## Controller

Receives requests, calls business logic, returns responses.

Example

```
GET /movies
        |
MovieController
        |
MovieService
        |
MovieRepository
        |
Database
```

<br>

# Repository Pattern

These classes handle getting data into and out of our data store, with the important caveat that each Repository only works against a single Model class.  So, if your models are Dogs, Cats, and Rats, you would have a Repository for each, the DogRepository would not call anything in the CatRepository, and so on.



Repository handles only database operations.

Each Repository manages only one entity.

Example

```
MovieRepository
ActorRepository
ProducerRepository
```

MovieRepository should not access ActorRepository directly.

Example of operations

```csharp
MovieRepository.GetMovieById()
MovieRepository.AddMovie()
MovieRepository.DeleteMovie()
```

<br>

# Service Layer
These classes can query multiple Repository classes and combine their data to form new, more complex business objects.

Service contains business logic.

A Service can call multiple repositories.

Example

```
MovieService

        |
        |<br>- MovieRepository
        |
        |<br>- ActorRepository
        |
        |<br>- ProducerRepository
```

Example

While creating a movie,

MovieService

- Validates Producer
- Validates Actors
- Saves Movie

<br>

# Repository vs Service

Repository

- Database operations
- CRUD
- One entity only

Service

- Business logic
- Validation
- Uses multiple repositories

<br>

# Dependency Injection (DI)

Dependency Injection is a design pattern that provides required objects from outside instead of creating them manually.

Without DI

```csharp
var repo = new MovieRepository();
```

With DI

```csharp
public MovieService(IMovieRepository repo)
{
    _repo = repo;
}
```

Benefits

- Loose Coupling
- Easy Testing
- Easy Maintenance
- Better Code Reuse

<br>

# Inversion of Control (IoC)

DI is an implementation of IoC.

Instead of your class creating dependencies,

the framework provides them.

<br>

# Mocking

Mocking replaces real dependencies with fake ones during testing.

Purpose

- Test only your code
- Avoid database calls
- Faster tests

Example

Instead of

```
MovieService
      |
SQL Database
```

Use

```
MovieService
      |
Fake Repository (Mock)
```

Now testing becomes easier.

<br>

# Uploading Images to Firebase

Package

```
FirebaseStorage.net
Version 1.0.3
```

Basic Flow

```
Client
   |
Upload Image
   |
API
   |
Firebase Storage
   |
Returns Image URL
```

<br>

# Important Topics to Read

## CORS

Allows or blocks requests coming from another domain.

Example

```
Frontend
http://localhost:3000

Backend
http://localhost:5000
```

Without CORS

❌ Request Blocked

With CORS Enabled

✅ Request Allowed

<br>

## Dapper

A lightweight Micro ORM for .NET.

Converts SQL query results directly into C# objects.

<br>

## Parameterized Query

Used to prevent SQL Injection.

Bad

```sql
SELECT * FROM Users
WHERE Name = '" + name + "'";
```

Good

```sql
SELECT * FROM Users
WHERE Name = @Name;
```

<br>

## Options Pattern

Used to read configuration values from appsettings.json.

Example

```json
{
    "ConnectionStrings": {
        "Default": "..."
    }
}
```

<br>

## Anonymous Type

Create objects without defining a class.

Example

```csharp
var user = new
{
    Name = "Yash",
    Age = 22
};
```

<br>

## Integration Testing

Tests multiple components working together.

Example

```
Controller
    |
Service
    |
Repository
    |
Database
```

All tested together.

<br>

## JWT (JSON Web Token)

Used for Authentication.

Flow

```
Login

↓

Server Creates Token

↓

Client Stores Token

↓

Client Sends Token

↓

Server Verifies Token
```

<br>

## API Versioning

Allows multiple versions of an API.

Example

```
GET /api/v1/users

GET /api/v2/users
```

Older applications continue working even after new versions are released.

<br>

# Additional Reading (Optional)

- GraphQL
- API Testing
- BDD (Behavior Driven Development)

These are useful but not required for the assignment.

<br>

# Quick Interview Points

- HTTP is a protocol.
- REST is an architectural style (not a standard).
- Web Service is server-side code exposed over HTTP.
- API is a contract for communication between software.
- Resource = Data/Object exposed by API.
- URI uniquely identifies a resource.
- JSON is the most common REST response format.
- Repository handles data access.
- Service handles business logic.
- DI provides dependencies instead of creating them manually.
- Mocking replaces real dependencies during testing.
- JWT is used for authentication.
- CORS controls cross-origin requests.
