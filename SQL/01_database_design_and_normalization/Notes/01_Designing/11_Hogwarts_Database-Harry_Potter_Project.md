# Database Design: Hogwarts School

In this tutorial, we'll design a relational database for **Hogwarts School of Witchcraft and Wizardry**.

The goal is not to memorize tables, but to learn **how to think about database design**.

Whenever designing a database, start by asking:

> **What information does the application need to store?**

For Hogwarts, we need to manage:

- Students
- Teachers
- Subjects
- Houses
- Classes
- House Cup
- Quidditch
- ...and possibly many more features.

Instead of designing everything at once, we'll build the database one requirement at a time.


<br>



# Step 1: Identify the Main Entities

Read the requirements and identify the important **nouns**.

For example:

- Student
- Teacher
- Subject
- House
- Class
- Team
- Match

Each of these represents a real-world object.

These usually become **database tables (entities).**


<br>



# Step 2: Design the Student Table

Everything begins with students.

Each student has information that belongs only to them.

Examples:

- First name
- Last name
- Date of birth
- Enrollment year

## Student

| student_id | first_name | last_name | enrollment_year |
|------------|------------|-----------|-----------------|
| 1 | Harry | Potter | 1991 |
| 2 | Hermione | Granger | 1991 |
| 3 | Ron | Weasley | 1991 |

Notice that each student has a unique primary key.

```
student_id
```

Even though names are unique in our example, multiple students could share the same name.

Primary keys should uniquely identify records.


<br>



# Step 3: Students Belong to Houses

Now the requirements say:

> Every student belongs to a Hogwarts house.

Possible houses:

- Gryffindor
- Hufflepuff
- Ravenclaw
- Slytherin

Our first thought might be:

## Student

| student | house |
|----------|-------|
| Harry | Gryffindor |
| Ron | Gryffindor |
| Draco | Slytherin |

This works...

...until someone accidentally types

```
Griffindor
```

instead of

```
Gryffindor
```

Now the database contains two different spellings for the same house.


<br>



## Better Design

Create a separate table.

### House

| house_id | name |
|-----------|------|
| 1 | Gryffindor |
| 2 | Hufflepuff |
| 3 | Ravenclaw |
| 4 | Slytherin |

Now the student stores only

```
house_id
```

instead of the house name.

### Student

| student | house_id |
|----------|-----------|
| Harry | 1 |
| Ron | 1 |
| Draco | 4 |

This improves:

- Consistency
- Data integrity
- Storage efficiency

A house can also store extra information later.

Example:

- Founder
- House colors
- Mascot

Without changing the Student table.


<br>



# Step 4: Teachers and Subjects

Now consider another requirement.

> Teachers teach subjects.

Ask yourself:

Can one teacher teach multiple subjects?

```
Professor McGonagall

↓

Transfiguration

↓

Defence Against the Dark Arts
```

Yes.

Can one subject have multiple teachers?

Also yes.

Different teachers may teach Potions over different years.

This is a **many-to-many relationship.**

```
Teacher

↕

Subject
```

A many-to-many relationship requires a junction table.

```
Teacher
        ▲
        │
        │

Subject_Teacher

teacher_id
subject_id
year

        │
        ▼

Subject
```

The extra `year` column allows the assignment to change over time.

Example:

| Teacher | Subject | Year |
|----------|----------|------|
| Snape | Potions | 1995 |
| Slughorn | Potions | 1997 |

Without the `year`, we would lose historical information.


<br>



# Step 5: Classes

A subject is not the same as a class.

Think carefully.

```
Subject

↓

Potions
```

exists forever.

A class is

```
Potions

+

Teacher

+

Academic Year
```

Example:

```
Potions

↓

Professor Snape

↓

1995
```

That is one specific class.

Another class might be

```
Potions

↓

Professor Slughorn

↓

1997
```

Therefore we create another entity.

```
Class
```

A class belongs to:
- one subject
- one teacher
- one academic year


<br>



# Step 6: Students Enroll in Classes

Now ask:

Can one student attend many classes?

Yes.

Can one class contain many students?

Also yes.

Again,

```
Many

↓

Many
```

which requires another junction table.

```
Student

        ▲
        │

Student_Class

student_id
class_id

        │
        ▼

Class
```

Each row means

> This student is enrolled in this class.


<br>



# Step 7: Head of House

Another requirement:

Every house has a Head of House.

Initially we may think:

```
House

head_teacher_id
```

But suppose Professor McGonagall becomes Head in 1995.

Later another professor replaces her.

Now we lose history.

Instead create

```
House_Head
```

Example

| house_id | teacher_id | year_started |
|-----------|------------|--------------|
| Gryffindor | McGonagall | 1995 |

Now historical changes are preserved.


<br>



# Step 8: House Cup

Every year houses compete.

We need to store:

- House
- Year
- Total Points

```
House_Points
```

| House | Year | Points |
|--------|------|---------|
| Gryffindor | 1995 | 483 |
| Slytherin | 1995 | 472 |

Finding the winner simply means finding the house with the highest score for that year.


<br>


# Step 9: Quidditch Teams

Every house has a Quidditch team.

But teams change every year.

Harry may play in 1995.

Graduate in 1998.

A new Seeker joins.

Therefore the team itself should include the year.

```
Quidditch_Team

team_id
house_id
year
```

Example

| Team | House | Year |
|------|--------|------|
| 1 | Gryffindor | 1995 |
| 2 | Gryffindor | 1996 |

These are different teams.


<br>



# Step 10: Players

Can one student play on multiple teams?

Across different years?

Yes.

Can one team contain many players?

Also yes.

Again,

```
Many

↓

Many
```

So we create another junction table.

```
Team_Player

team_id
student_id
position
captain
```

Example

| Team | Student | Position | Captain |
|------|----------|----------|----------|
| Gryffindor 1995 | Harry | Seeker | No |
| Gryffindor 1995 | Oliver | Keeper | Yes |

Notice that

```
position
```

belongs here because it describes the student's role **on that team**, not the student in general.


<br>



# Step 11: Quidditch Matches

Finally,

teams compete against each other.

Each match stores:

- Team 1
- Team 2
- Date
- Score

Example

| Match | Team 1 | Team 2 | Score |
|---------|---------|---------|-------|
| 1 | Gryffindor | Slytherin | 210–60 |

Each match references two existing teams.


<br>



# Putting Everything Together

As new requirements arrive, we don't randomly create tables.

Instead, we repeatedly ask:

1. What is the entity?
2. What information belongs to it?
3. How is it related to other entities?
4. Is the relationship One-to-One, One-to-Many, or Many-to-Many?
5. Does this relationship change over time?

Answering these questions naturally leads to a well-structured database.


<br>



# Future Enhancements

A good database is designed to evolve.

New features can be added without redesigning everything.

Examples include:

- Spell library
- Magical creatures
- Student grades
- Family relationships
- Friendships
- Clubs
- Azkaban records
- Wand ownership
- Owl deliveries

Because the database is normalized, these features can be added as new tables connected through foreign keys.



<br>



# Key Design Principles

- Start with the application's requirements.
- Identify the real-world entities.
- Each entity usually becomes a table.
- Use primary keys to uniquely identify records.
- Use foreign keys to connect related tables.
- Use junction tables to resolve many-to-many relationships.
- Store information only where it naturally belongs.
- Consider how data changes over time (such as teachers, classes, or Quidditch teams), and include attributes like `year` when needed to preserve history.
