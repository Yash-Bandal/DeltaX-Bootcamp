# Database Design: Chat Application

This guide explains how to design a database for a chat application similar to **WhatsApp**, **Messenger**, or **Telegram**.

The goal is to support:

- One-to-one chats
- Group chats
- Contacts
- Message history
- Group membership tracking

<br>

# Requirements

<br>
<div align = "center">

<img width="600" alt="image" src="https://github.com/user-attachments/assets/781b2ee3-1f89-4308-85b7-3119253514f0" />

</div>
<br>


<br>
<div align = "center">
<table>
  <tr>
    <td align="center">
      <img width="250" alt="image" src="https://github.com/user-attachments/assets/1da25aa8-1fad-45d1-9c14-c23b4ba65c7e" />
      <br>
      <strong>Message</strong>
    </td>
    <td align="center">
      <img width="400" alt="image" src="https://github.com/user-attachments/assets/6357cf89-95c8-4751-8ead-10fb5c2728e6" />
      <br>
      <strong>Sending</strong>
    </td>
  </tr>
</table>
</div>
<br>

Users should be able to:

- Send text messages.
- Save contacts.
- Create group chats.
- Add or remove members from groups.
- View previous conversations.
- Ensure members only see messages sent while they were part of the group.

<br>

# Example Scenario

Suppose we have four users.

| Contact ID | Name | Phone |
| :--------- | :--- | :---- |
| 1 | Alice | 9876543210 |
| 2 | Bob | 9876543211 |
| 3 | Charlie | 9876543212 |
| 4 | David | 9876543213 |

Alice creates a group called **College Friends**.

Members:

- Alice
- Bob
- Charlie

Later, David joins the group.

<br>

# Step 1: Contact Table

Stores user information.

### `contact`

| contact_id | first_name | last_name | phone_number |
| :--------- | :--------- | :-------- | :----------- |
| 1 | Alice | Smith | 9876543210 |
| 2 | Bob | Jones | 9876543211 |
| 3 | Charlie | Brown | 9876543212 |
| 4 | David | Wilson | 9876543213 |

<br>

# Step 2: Initial Message Table

A simple design might look like this.

### `message`

| message_id | from_number | to_number | message_text | sent_datetime |
| :--------- | :---------- | :-------- | :----------- | :------------ |
| 1 | 9876543210 | 9876543211 | Hi Bob! | 2026-07-30 10:00 |
| 2 | 9876543211 | 9876543210 | Hello! | 2026-07-30 10:01 |

### Problem

This design only supports **one-to-one messaging**.

It cannot easily support:

- Group chats
- Multiple participants
- Group history

<br>

# Step 3: Create a Conversation Table

Instead of sending messages directly to another user, every message belongs to a **conversation**.

A conversation can represent:

- A private chat
- A group chat

### `conversation`

| conversation_id | name |
| :-------------- | :--- |
| 1 | Alice & Bob |
| 2 | College Friends |

<br>

# Step 4: Group Member (Junction Table)

A conversation can have many users.

A user can belong to many conversations.

This is a **Many-to-Many Relationship**, so we use a **junction table**.

### `group_member`

| conversation_id | contact_id | joined_datetime | left_datetime |
| :-------------- | :--------- | :-------------- | :------------ |
| 2 | 1 | 2026-07-01 | NULL |
| 2 | 2 | 2026-07-01 | NULL |
| 2 | 3 | 2026-07-01 | NULL |
| 2 | 4 | 2026-07-15 | NULL |

<br>


<div align = "center">
<p>Group Table</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/26019675-4a66-4b29-bde6-33d8eb8f729b" />
<p>Conversation Table</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/c3601f79-7bab-48d5-b72d-84418b0d8897" />
<p>Enhancements</p>
<img width="600" alt="image" src="https://github.com/user-attachments/assets/be22de29-6cd8-4177-bcd3-e72e9bbeb78a" />
</div>

<br>

### Relationship

```
contact
--------
contact_id (PK)

        ▲
        │
        │

group_member
------------
contact_id (FK)
conversation_id (FK)
joined_datetime
left_datetime

        │
        ▼

conversation
------------
conversation_id (PK)
name
```

<br>

# Step 5: Refactor the Message Table

Instead of storing `to_number`, store the conversation.

### `message`

| message_id | conversation_id | sender_id | message_text | sent_datetime |
| :--------- | :-------------- | :-------- | :----------- | :------------ |
| 1 | 2 | 1 | Hello everyone! | 2026-07-30 09:00 |
| 2 | 2 | 2 | Hi Alice! | 2026-07-30 09:01 |
| 3 | 2 | 3 | Good Morning! | 2026-07-30 09:02 |

Now every message belongs to exactly one conversation.

<br>

# Complete Database Design

```
contact
--------
contact_id (PK)
first_name
last_name
phone_number

        ▲
        │
        │

group_member
------------
contact_id (FK)
conversation_id (FK)
joined_datetime
left_datetime

        │
        ▼

conversation
------------
conversation_id (PK)
name

        ▲
        │
        │

message
-------
message_id (PK)
conversation_id (FK)
sender_id (FK)
message_text
sent_datetime
```

<br>

# Example Query

## Display All Messages in a Conversation

```sql
SELECT
    c.first_name,
    m.message_text,
    m.sent_datetime
FROM message AS m
INNER JOIN contact AS c
    ON m.sender_id = c.contact_id
WHERE m.conversation_id = 2
ORDER BY m.sent_datetime;
```

### Output

| Sender | Message | Time |
| :----- | :------ | :--- |
| Alice | Hello everyone! | 09:00 |
| Bob | Hi Alice! | 09:01 |
| Charlie | Good Morning! | 09:02 |

<br>

# Example Query

## Show Members of a Group

```sql
SELECT
    c.first_name,
    gm.joined_datetime
FROM group_member AS gm
INNER JOIN contact AS c
    ON gm.contact_id = c.contact_id
WHERE gm.conversation_id = 2;
```

### Output

| Member | Joined |
| :----- | :----- |
| Alice | 2026-07-01 |
| Bob | 2026-07-01 |
| Charlie | 2026-07-01 |
| David | 2026-07-15 |

<br>

# Why Store `joined_datetime` and `left_datetime`?

Suppose the following events occur:

| Date | Event |
| :--- | :---- |
| July 1 | Alice creates the group |
| July 5 | Bob sends a message |
| July 15 | David joins |
| July 20 | Charlie sends a message |

Since David joined **after July 5**, he should **not** see Bob's earlier message.

The application can compare:

```text
message.sent_datetime
```

with

```text
group_member.joined_datetime
```

to determine which messages a user is allowed to view.

Similarly, if someone leaves the group, `left_datetime` can be used to stop showing messages sent after they left.

<br>

# Benefits of This Design

- Supports both private and group chats.
- Easily handles unlimited group members.
- Stores complete message history.
- Tracks when users join or leave groups.
- Eliminates duplicate data.
- Follows proper database normalization.

<br>

# Key Design Principles

- Store **Contacts**, **Conversations**, **Members**, and **Messages** in separate tables.
- Use a **junction table** (`group_member`) to model the many-to-many relationship between users and conversations.
- Every message belongs to exactly one conversation.
- Track membership history using `joined_datetime` and `left_datetime`.
- Refactoring the design improves scalability and simplifies querying.

<br>

# Key Takeaways

- Database design is an **iterative process**—it is common to refine the schema as requirements evolve.
- A **Conversation** represents both private chats and group chats.
- **Many-to-Many** relationships should always be modeled using a junction table.
- Messages are **historical events**, while contacts represent the current state of users.
- This design is scalable and forms the foundation of many modern messaging applications.
