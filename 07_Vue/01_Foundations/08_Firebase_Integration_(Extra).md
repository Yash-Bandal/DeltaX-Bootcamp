# Part 8 — Firebase Integration (Vue.js)

**Lessons 43–44 · 2 lessons**

## Table of Contents

1. [Introduction to Firebase](#1-introduction-to-firebase)
2. [Posting Data to Firebase](#2-posting-data-to-firebase)
3. [Retrieving Data from Firebase](#3-retrieving-data-from-firebase)
4. [Complete Data Flow](#4-complete-data-flow)
5. [Quick Revision](#5-quick-revision)
6. [Vue 2 vs Vue 3](#6-vue-2-vs-vue-3)

<br>


---

## 1. Introduction to Firebase

### 1.1 What problem does Firebase solve?

Suppose you build a blog application in Vue.

Users can enter a title and content, but where will you store that information?

If you store it only in Vue's `data`, it exists in the running application. Reloading the page resets it unless you have added some form of persistence.

You need a backend service to store the information.

Traditionally, you might build:

- A backend API using ASP.NET Core, Node.js, Flask, or another framework.
- A database such as SQL Server or PostgreSQL.
- Endpoints to save and retrieve blog posts.

Firebase provides managed backend services, including databases, authentication, and hosting. For this example, we'll focus on **Firebase Realtime Database**.

<br>


### 1.2 How does the architecture work?

```text
Vue application
      |
      | Send or request data
      v
Firebase Realtime Database
      |
      | Stores or returns data
      v
Vue application
      |
      | Updates reactive state
      v
User sees the result
```

Firebase can handle the data storage without requiring you to build your own API server for this basic use case.

<br>


### 1.3 What is Firebase Realtime Database?

Firebase Realtime Database stores data as a JSON tree in the cloud.

For example, a blog database could contain:

```json
{
  "blogs": {
    "-blog001": {
      "title": "Learning Vue",
      "content": "Vue makes reactive interfaces easier to build."
    },
    "-blog002": {
      "title": "Learning JavaScript",
      "content": "JavaScript powers browser interactions."
    }
  }
}
```

Each blog has a unique key, such as `-blog001`.

Your Vue application can send new records to the database and read existing ones.

<br>


### 1.4 Firebase setup overview

For a real project, the general setup is:

1. Create a Firebase project.
2. Create a Realtime Database.
3. Configure the database's security rules.
4. Obtain the database URL.
5. Connect your application using the Firebase SDK or the database's REST API.

Older tutorials may use a direct HTTP endpoint. Firebase also supports SDK-based access, which is commonly used in modern applications.

**Important:** Never leave a production database publicly readable and writable. Configure security rules and authentication according to your application's requirements.

<br>

---

<br>


## 2. Posting Data to Firebase

### 2.1 What does posting data mean?

Posting means sending data to a backend service.

Imagine a user fills out a blog form:

- Title: Learning Vue
- Content: Vue makes reactive interfaces easier to build.

When the user clicks **Submit**, the application sends that data to Firebase.

Firebase stores it so it can be retrieved later.

<br>


### 2.2 Example blog form in Vue 2

```html
<div id="app">
  <h2>Create a Blog</h2>

  <form @submit.prevent="addBlog">
    <label>Title</label>
    <input v-model.trim="title" required>

    <label>Content</label>
    <textarea v-model.trim="content" required></textarea>

    <button type="submit" :disabled="saving">
      {{ saving ? "Saving..." : "Add Blog" }}
    </button>

    <p v-if="message">{{ message }}</p>
    <p v-if="error">{{ error }}</p>
  </form>
</div>
```

The form uses `v-model` to keep its inputs connected to Vue data.

The `.prevent` modifier stops the browser from performing its default form submission.

<br>


### 2.3 Send the data using Firebase's REST API

For learning purposes, we'll use the Realtime Database REST API. Replace the URL with your own database's URL and make sure the database's security rules authorize the request.

```js
new Vue({
  el: "#app",

  data: {
    title: "",
    content: "",
    saving: false,
    message: "",
    error: ""
  },

  methods: {
    async addBlog() {
      this.saving = true;
      this.message = "";
      this.error = "";

      try {
        const response = await fetch(
          "https://YOUR_DATABASE_URL/blogs.json",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
            },
            body: JSON.stringify({
              title: this.title,
              content: this.content
            })
          }
        );

        if (!response.ok) {
          throw new Error("Unable to save blog");
        }

        const result = await response.json();

        this.message = `Blog saved with ID: ${result.name}`;
        this.title = "";
        this.content = "";
      } catch (error) {
        this.error = error.message;
      } finally {
        this.saving = false;
      }
    }
  }
});
```

`YOUR_DATABASE_URL` is a placeholder. Use the database URL shown in your Firebase console, with the appropriate path and `.json` suffix for the REST API.

<br>


### 2.4 Understand the important parts

**Step 1 — Handle form submission**

```html
<form @submit.prevent="addBlog">
```

Vue calls `addBlog()` when the user submits the form.

**Step 2 — Set the loading state**

```js
this.saving = true;
```

This lets the UI show that the request is in progress.

**Step 3 — Send the POST request**

```js
fetch(url, {
  method: "POST",
  headers: {
    "Content-Type": "application/json"
  },
  body: JSON.stringify({
    title: this.title,
    content: this.content
  })
});
```

- `method: "POST"` tells Firebase that we're sending data to create a new child record.
- `Content-Type` indicates that the request body contains JSON.
- `JSON.stringify()` converts a JavaScript object into a JSON string.

**Step 4 — Wait for the response**

```js
const result = await response.json();
```

A successful Realtime Database POST response includes a generated key in its `name` property.

**Step 5 — Update the UI**

```js
this.message = `Blog saved with ID: ${result.name}`;
```

The user sees confirmation after the request succeeds.

<br>


### 2.5 What is `JSON.stringify()`?

Consider this JavaScript object:

```js
const blog = {
  title: "Learning Vue",
  content: "Vue uses reactive data."
};
```

`JSON.stringify(blog)` produces a JSON string:

```json
{
  "title": "Learning Vue",
  "content": "Vue uses reactive data."
}
```

HTTP request bodies are sent as data, so you often need to serialize JavaScript objects into JSON.

<br>


### 2.6 What is the difference between POST and PUT?

| Method | Typical Firebase Realtime Database behavior |
|---|---|
| `POST` | Adds a child using a generated key |
| `PUT` | Writes or replaces data at a specified path |
| `PATCH` | Updates selected fields at a path |
| `GET` | Reads data from a path |
| `DELETE` | Deletes data at a path |

For example, POSTing to `/blogs.json` creates a new child under `blogs`.

**Remember:** A successful request does not mean that any URL or database will accept the write. The database URL, network access, and security rules must all be correct.

<br>

---

<br>


## 3. Retrieving Data from Firebase

### 3.1 Why retrieve data?

Posting saves data. Retrieving loads it back into the application.

For a blog application, users should be able to open the page later and see previously saved blogs.

The application sends a GET request to Firebase, receives the JSON data, and stores it in Vue's reactive state.

<br>


### 3.2 Example Vue 2 template

```html
<div id="app">
  <h2>Blog Posts</h2>

  <p v-if="loading">Loading blogs...</p>
  <p v-else-if="error">{{ error }}</p>

  <ul v-else>
    <li v-for="blog in blogs" :key="blog.id">
      <h3>{{ blog.title }}</h3>
      <p>{{ blog.content }}</p>
    </li>
  </ul>
</div>
```

Here, `v-for` displays every blog in the `blogs` array.

The `:key` helps Vue track each rendered item. We'll use the Firebase record key as its ID.

<br>


### 3.3 Fetch the saved blogs

```js
new Vue({
  el: "#app",

  data: {
    blogs: [],
    loading: false,
    error: ""
  },

  created() {
    this.fetchBlogs();
  },

  methods: {
    async fetchBlogs() {
      this.loading = true;
      this.error = "";

      try {
        const response = await fetch(
          "https://YOUR_DATABASE_URL/blogs.json"
        );

        if (!response.ok) {
          throw new Error("Unable to retrieve blogs");
        }

        const data = await response.json();

        this.blogs = data
          ? Object.entries(data).map(([id, blog]) => ({
              id,
              ...blog
            }))
          : [];
      } catch (error) {
        this.error = error.message;
      } finally {
        this.loading = false;
      }
    }
  }
});
```

<br>


### 3.4 Understand the execution flow

**Step 1 — Vue initializes the state**

```js
blogs: [],
loading: false,
error: ""
```

Initially, the list is empty.

**Step 2 — The component is created**

```js
created() {
  this.fetchBlogs();
}
```

Vue starts the request from the `created` lifecycle hook.

**Step 3 — Firebase returns JSON**

The response may look like this:

```json
{
  "-blog001": {
    "title": "Learning Vue",
    "content": "Vue uses reactive data."
  },
  "-blog002": {
    "title": "Learning JavaScript",
    "content": "JavaScript runs in the browser."
  }
}
```

Notice that Firebase returns an object keyed by database IDs, rather than an array.

**Step 4 — Convert the object into an array**

```js
this.blogs = Object.entries(data).map(([id, blog]) => ({
  id,
  ...blog
}));
```

The result becomes:

```js
[
  {
    id: "-blog001",
    title: "Learning Vue",
    content: "Vue uses reactive data."
  },
  {
    id: "-blog002",
    title: "Learning JavaScript",
    content: "JavaScript runs in the browser."
  }
]
```

This shape is convenient for `v-for`.

If Firebase returns `null` because no blog records exist, the code uses an empty array.

**Step 5 — Vue renders the data**

When `this.blogs` changes, Vue updates the displayed list.

```text
Firebase response
       |
       v
Parse JSON
       |
       v
Convert object to array
       |
       v
Assign to this.blogs
       |
       v
v-for renders each blog
```

<br>


### 3.5 Does this keep the page updated automatically?

Not in this example.

The code performs one GET request when the Vue instance is created. If another user adds a blog later, this page won't automatically know about it.

To receive ongoing updates, use Firebase's realtime listeners through its SDK, or implement an appropriate refresh or polling strategy.

This is an important difference between a one-time HTTP request and a realtime data subscription.

<br>

---

<br>


## 4. Complete Data Flow

### 4.1 Posting and retrieving data together

Now connect the two operations.

```text
User fills in blog form
          |
          v
Vue stores input through v-model
          |
          v
User submits form
          |
          v
POST request to Firebase
          |
          v
Firebase saves the blog
          |
          v
Firebase returns a generated ID
```

When the application loads:

```text
Vue instance is created
          |
          v
GET request to Firebase
          |
          v
Firebase returns saved blogs
          |
          v
Vue converts response into an array
          |
          v
v-for displays the blogs
```

<br>


### 4.2 Why separate frontend state from stored data?

These are two different things:

- **Vue state:** The data currently held by the running frontend.
- **Firebase data:** The data stored in the cloud.

Updating Vue state doesn't automatically save it to Firebase. Likewise, saving data to Firebase doesn't automatically update every Vue component unless you refresh the data or use a realtime listener.

You need to connect the two operations explicitly.

### 4.3 Where do errors occur?

| Problem | What to check |
|---|---|
| Request returns 401 or 403 | Database authentication and security rules |
| Request returns 404 | Database URL or path |
| Request fails to reach Firebase | Network, URL, or browser console |
| Data appears empty | Database path or empty database |
| Save works but page remains unchanged | Refresh local state or fetch the new data |
| Data is stored but list rendering fails | Check the returned data shape and `v-for` |

**Security note:** Do not solve permission problems by permanently allowing unrestricted public writes. Use suitable Firebase rules and authentication for your application.

<br>

---

<br>



<br>

---

<br>


