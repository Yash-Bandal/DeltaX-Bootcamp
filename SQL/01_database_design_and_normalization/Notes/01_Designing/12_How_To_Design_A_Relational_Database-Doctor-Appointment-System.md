# How to Design a Relational Database

One of the biggest mistakes beginners make is jumping straight into creating tables.

Instead, start with the **requirements**.

A database should be designed to answer the application's questions and support its features.

In this tutorial, we'll design a simple **Doctor Appointment System**.


> [!Tip]
> At start you can have only minimal required attributes, later you can add them as required

<br>



# Step 1: Understand the Requirements

Before thinking about tables, understand **what the application should do**.

Suppose the client says:

> Patients should be able to book appointments with doctors. Each appointment has a date and time. When the patient arrives and when the appointment finishes should also be recorded.



<br>
<div align = "center">
 <img width="500" alt="image" src="https://github.com/user-attachments/assets/e6d9149e-7e5f-4b63-947a-a627514b61a1" />
</div>
<br>

This short description already tells us a lot.

We need to store information about:

- Patients
- Doctors
- Appointments

Always start by writing down the requirements in plain English.

The clearer the requirements, the better the database design.



<br>



# Step 2: Find the Entities (Nouns)

Now read the requirements carefully.

Highlight the **nouns**.

> Patients should be able to book appointments with doctors.

Nouns:

- Patient
- Doctor
- Appointment

These represent real-world objects.

In database design, these objects usually become **entities**, which are implemented as **tables**.

```
Patient
Doctor
Appointment
```

Sometimes additional entities appear naturally.

For example:

- Clinic
- Department
- Prescription
- Payment

Don't worry about finding every table immediately.

Database design is an iterative process.



<br>
<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/a91a90c8-a089-4a69-9d2d-ac31032811bf" />
</div>
<br>


<br>



# Step 3: Add Attributes

Now ask:

> **What information should we store about each entity?**

## Patient

A patient might have:

- Patient ID
- First Name
- Last Name
- Date of Birth
- Phone Number
- Email


<br>
<div align = "center">
        <img width="450" height="365" alt="image" src="https://github.com/user-attachments/assets/3ed75502-c940-4169-ab11-57b7ad509f6e" />
</div>
<br>


Example:

| patient_id | first_name | last_name | date_of_birth |
|------------|------------|-----------|---------------|
| 1 | Alice | Johnson | 1998-05-21 |
| 2 | Bob | Smith | 1992-11-03 |



<br>



## Doctor

A doctor might have:

- Doctor ID
- First Name
- Last Name
- Specialty

Example:

| doctor_id | name | specialty |
|------------|------|-----------|
| 1 | Dr. Brown | Cardiology |
| 2 | Dr. Wilson | Pediatrics |



<br>



## Appointment

An appointment might store:

- Appointment ID
- Appointment Date
- Appointment Time
- Arrival Time
- Completion Time

Example:

| appointment_id | appointment_date | appointment_time |
|----------------|------------------|------------------|
| 101 | 2025-06-20 | 10:30 AM |

Notice something.

The appointment currently doesn't know:

- Which patient?
- Which doctor?

We'll solve that next.



<br>



# Step 4: Find the Relationships

Now ask:

> **How are these entities connected?**

## Patient → Appointment

Can one patient have multiple appointments?

```
Alice

↓

Appointment 1

Appointment 2

Appointment 3
```

Yes.

Can one appointment belong to multiple patients?

No.

An appointment belongs to exactly one patient.

Therefore,

```
One Patient

↓

Many Appointments
```

This is a **One-to-Many** relationship.

Instead of storing patient information repeatedly inside every appointment, store only the patient's ID.

```
Appointment

appointment_id
patient_id
appointment_date
```

The `patient_id` is a **Foreign Key** pointing to the `Patient` table.



<br>



## Doctor → Appointment

Ask the same questions.

Can one doctor have many appointments?

Yes.

```
Dr. Brown

↓

10:00

10:30

11:00

11:30
```

Can one appointment have multiple doctors?

Usually no.

Each appointment is assigned to one doctor.

Again,

```
One Doctor

↓

Many Appointments
```

So the appointment stores

```
doctor_id
```

as another foreign key.



<br>
<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/be5ec7e0-014d-4532-8041-0666a0295825" />
</div>
<br>


<br>



# Why Use Foreign Keys?

Imagine we stored the patient's information directly inside every appointment.

| Appointment | Patient |
|-------------|----------|
| 101 | Alice Johnson |
| 102 | Alice Johnson |
| 103 | Alice Johnson |

Alice changes her last name.

Now every appointment must be updated.

Instead, store

```
patient_id = 1
```

Now Alice's information exists only once.

Every appointment simply points to it.

This avoids duplicate data and keeps the database consistent.

This idea is called **normalization**.



<br>



# The Database Structure

Now we have three related tables.

```
Patient
--------
patient_id

        ▲
        │
        │ patient_id (FK)

Appointment
-----------
appointment_id
patient_id
doctor_id
appointment_date
appointment_time

        │
        │ doctor_id (FK)
        ▼

Doctor
-------
doctor_id
```

Notice that **Appointment** acts as the bridge between patients and doctors.



<br>



# Step 5: Review the Requirements

Once the design is complete, go back to the original requirements.

Ask yourself:

> Can the database answer every question the application needs?



<br>
<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/7745896e-b882-4abe-b4f3-6aee16843cfd" />
        <p>Check</p>
<img width="500" alt="image" src="https://github.com/user-attachments/assets/e0bbb5d0-a93e-4e15-9b80-0211faf83039" />
        <p>2nd</p>
        <img width="400" alt="image" src="https://github.com/user-attachments/assets/8de774ad-441b-4342-b8ce-c97569bf9218" />
        <p>2nd</p>
        <img width="400"  alt="image" src="https://github.com/user-attachments/assets/4cec4499-32f7-4f81-ae1c-deda4ffe77cc" />
</div>
<br>


For example:

### Can we find all appointments for a patient?

Yes.

```
Patient

↓

Appointment
```



<br>



### Can we find today's appointments for a doctor?

Yes.

```
Doctor

↓

Appointment
```



<br>



### Can we record arrival and completion times?

Yes.

Those are attributes of an appointment.



<br>



### Can a patient visit different doctors?

Yes.

Each appointment stores its own doctor.



<br>



### Can a doctor treat many patients?

Yes.

The doctor simply has many appointment records.



<br>



# Step 6: Improve the Design

Database design rarely finishes on the first attempt.

As new requirements appear, the database evolves.

For example:

The client says:

> Doctors work in different clinics.

Now we discover a new entity.

```
Clinic
```

A doctor belongs to a clinic.

```
Clinic

↓

Doctor

↓

Appointment
```

Later they say:

> Patients have insurance.

Another entity appears.

```
Insurance
```

Good database design is iterative.

You improve the model as you understand the business better.



<br>



# Step 7: Test Your Design

One of the best ways to validate a database is to imagine inserting real data.

Ask questions like:

- Can one patient have multiple appointments?
- Can two doctors share the same specialty?
- Can appointments exist without patients?
- Can doctors change clinics?
- What happens if a doctor leaves?

Trying realistic scenarios often reveals missing relationships or design flaws.



<br>



# Complete Database Design



<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/8b861ae0-7167-49e7-8c2b-36b3c750124a" />
</div>
<br>



<br>

---

# Design Thinking Summary

Whenever designing a relational database, follow these steps:

1. **Understand the requirements**
   - What should the application do?

2. **Identify the entities**
   - The important nouns usually become tables.

3. **Define the attributes**
   - What information belongs to each entity?

4. **Identify the relationships**
   - One-to-One
   - One-to-Many
   - Many-to-Many

5. **Normalize the data**
   - Avoid storing duplicate information.
   - Connect tables using primary and foreign keys.

6. **Review the design**
   - Can every requirement be satisfied?

7. **Refine and expand**
   - Database design is iterative.
   - New requirements often introduce new entities and relationships.


<br>

---

# Key Design Principles

- Start with the business requirements, not SQL.
- Think in terms of **entities**, **attributes**, and **relationships**.
- Avoid duplicate data by using foreign keys.
- Use normalization to keep data consistent.
- Test your design with real-world scenarios.
- Expect the database to evolve as the application's requirements grow.
