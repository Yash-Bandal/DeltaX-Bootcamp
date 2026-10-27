# Part 6 — HTTP Requests & Directives 

**Lessons 32–38 · 7 lessons**

## Table of Contents

1. [HTTP Requests — Introduction](#1-http-requests--introduction)
2. [GET Requests](#2-get-requests)
3. [Custom Directives](#3-custom-directives)
4. [Filters](#4-filters)
5. [Custom Search Filter](#5-custom-search-filter)
6. [Local Registration](#6-local-registration)
7. [Mixins](#7-mixins)
8. [Quick Revision](#8-quick-revision)
9. [Vue 2 vs Vue 3](#9-vue-2-vs-vue-3)

<br>


---

## 1. HTTP Requests — Introduction

### 1.1 What problem are HTTP requests solving?

So far, most of our Vue examples have used data written directly inside the application.

For example:

```js
data: {
  title: "My First Blog",
  author: "Yash"
}
```

But real applications get data from external sources.

For example, a blog application might need to:
- Fetch blog posts from a backend.
- Display movie details from an API.
- Submit a registration form.
- Load user information after login.

We use **HTTP requests** to communicate with servers over the web.

<br>


### 1.2 How does the flow work?

Imagine your Vue application needs to display movies from a backend API.

```text
Vue application
      |
      | HTTP request
      v
Backend API
      |
      | Queries the database
      v
Database
      |
      | Returns movie data
      v
Backend API
      |
      | HTTP response (usually JSON)
      v
Vue application
      |
      | Updates reactive data
      v
Movie list appears on the page
```

<br>


Vue handles the UI. The backend handles the API logic and usually communicates with the database.

**Important:** Vue does not automatically fetch data just because an API URL exists. We must write code to make the request.

<br>


### 1.3 What is an API?

An API provides a defined way for one application to communicate with another.

For example, a movie API might expose an endpoint like:

```text
GET /api/movies
```

It could return JSON:

```json
[
  {
    "id": 1,
    "title": "Inception",
    "year": 2010
  },
  {
    "id": 2,
    "title": "Interstellar",
    "year": 2014
  }
]
```

The Vue application can take this response and display the movies.

<br>


### 1.4 Common HTTP methods

| Method | Typical purpose |
|---|---|
| `GET` | Retrieve data |
| `POST` | Create a resource or submit data |
| `PUT` | Replace a resource |
| `PATCH` | Partially update a resource |
| `DELETE` | Delete a resource |

We'll focus mainly on GET requests in this part. The same general communication model applies to the other methods.

<br>

---

<br>


## 2. GET Requests

### 2.1 What is a GET request?

A GET request asks a server to return data.

For example:

```text
GET https://jsonplaceholder.typicode.com/posts
```

This public demo endpoint returns sample posts in JSON format.

In the Vue 2 course, you may see **Vue Resource**, a library used to make HTTP requests. It was common in older Vue tutorials, but it is no longer the recommended default for new projects.

We'll understand the concept first using the browser's built-in `fetch()` API.

<br>


### 2.2 Fetching data in Vue 2

Suppose we want to display posts from an API.

```html
<div id="app">
  <h2>Blog Posts</h2>

  <p v-if="loading">Loading posts...</p>
  <p v-else-if="error">{{ error }}</p>

  <ul v-else>
    <li v-for="post in posts" :key="post.id">
      {{ post.title }}
    </li>
  </ul>
</div>
```

```js
new Vue({
  el: "#app",

  data: {
    posts: [],
    loading: false,
    error: ""
  },

  created() {
    this.fetchPosts();
  },

  methods: {
    async fetchPosts() {
      this.loading = true;
      this.error = "";

      try {
        const response = await fetch(
          "https://jsonplaceholder.typicode.com/posts"
        );

        if (!response.ok) {
          throw new Error("Failed to fetch posts");
        }

        const data = await response.json();
        this.posts = data.slice(0, 10);
      } catch (error) {
        this.error = "Unable to load posts.";
        console.error(error);
      } finally {
        this.loading = false;
      }
    }
  }
});
```

<br>


### 2.3 Understand the execution flow

Let's follow what happens when the page loads.

**Step 1 — Vue creates the instance**

Vue initializes the properties inside `data`.

```js
posts: [],
loading: false,
error: ""
```

**Step 2 — The `created()` lifecycle hook runs**

```js
created() {
  this.fetchPosts();
}
```

Vue calls `fetchPosts()` after the instance has been initialized.

**Step 3 — Loading state becomes true**

```js
this.loading = true;
```

The page can now display `Loading posts...`.

**Step 4 — The HTTP request starts**

```js
const response = await fetch(url);
```

`fetch()` sends the request and returns a Promise.

`await` pauses this async function until the Promise settles. It does not freeze the browser's entire UI.

**Step 5 — Check the response**

```js
if (!response.ok) {
  throw new Error("Failed to fetch posts");
}
```

`fetch()` does not automatically reject its Promise for HTTP errors such as 404 or 500. Checking `response.ok` lets us handle those responses as failures.

**Step 6 — Convert the response to JSON**

```js
const data = await response.json();
```

The response body is read and parsed into JavaScript values.

**Step 7 — Update Vue's data**

```js
this.posts = data.slice(0, 10);
```

The first ten posts are assigned to `posts`. Vue reacts to the change and renders the list.

**Step 8 — Finish loading**

```js
finally {
  this.loading = false;
}
```

This runs whether the request succeeds or fails.

<br>


### 2.4 Why do we need loading and error states?

Without them, a user might see a blank page while the request is running or if it fails.

A real application should account for three states:

```text
Request starts
     |
     v
  Loading
   /   \
  /     \
Success  Failure
  |        |
Show data  Show error
```

This pattern is useful for movie lists, dashboards, product pages, and almost any application that loads remote data.

<br>


### 2.5 Why does `v-for` use `:key`?

```html
<li v-for="post in posts" :key="post.id">
  {{ post.title }}
</li>
```

The `:key` gives each rendered item a stable identity. Vue can use it to track which item is which when the list changes.

Use a unique ID where possible instead of relying on the array index.

<br>


### 2.6 Where does Vue Resource fit in?

Older Vue 2 tutorials may use syntax like this:

```js
this.$http.get(url).then(function (response) {
  // Handle the response
});
```

That syntax depends on Vue Resource being installed and registered. It is not built into Vue itself.

For modern projects, `fetch()` or a library such as Axios is generally a more relevant choice. The core concepts remain the same:

1. Send a request.
2. Wait for the response.
3. Handle errors.
4. Store the returned data.
5. Let Vue update the UI.

<br>

---

<br>


## 3. Custom Directives

### 3.1 What is a directive?

A directive is a special Vue attribute that tells Vue to apply some behavior to an element.

You already know these directives:

```html
<p v-if="isVisible">Hello</p>

<li v-for="movie in movies" :key="movie.id">
  {{ movie.title }}
</li>

<input v-model="title">
```

Here:
- `v-if` controls whether an element is rendered.
- `v-for` renders a list.
- `v-model` connects an input to data.

Vue also lets us create our own directives when we need reusable DOM behavior.

<br>


### 3.2 Example — focus an input automatically

Suppose a movie form should automatically focus the movie title input when it appears.

Instead of repeating DOM focus logic, we can create a custom directive.

**Vue 2 example:**

```js
Vue.directive("focus", {
  inserted(el) {
    el.focus();
  }
});
```

Use it in the template:

```html
<div id="app">
  <input v-focus placeholder="Enter movie title">
</div>
```

<br>


### 3.3 How does it work?

```text
Vue renders the input
         |
         v
v-focus directive runs
         |
         v
Directive receives the DOM element
         |
         v
el.focus() executes
         |
         v
Input receives focus
```

Here, `el` refers to the actual DOM element.

The `inserted` hook in Vue 2 runs when the element has been inserted into its parent DOM.

<br>


### 3.4 When should we create custom directives?

Custom directives are useful for reusable, low-level DOM behavior, such as:
- Focusing an input.
- Integrating with a DOM-based library.
- Applying a specialized element behavior.

For ordinary UI state, prefer built-in Vue features and components. A directive should not replace normal reactive data handling.

<br>

---

<br>


## 4. Filters

### 4.1 What is a filter?

In Vue 2, filters were a feature for formatting displayed values.

Suppose your application stores an author name as:

```js
author: "YASH BANDAL"
```

But you want to display it in lowercase.

A filter can transform the displayed value without changing the original data.

<br>


### 4.2 Creating a global filter in Vue 2

```js
Vue.filter("lowercase", function (value) {
  return value.toLowerCase();
});
```

Use it in a template:

```html
<p>{{ author | lowercase }}</p>
```

Output:

```text
yash bandal
```

<br>


### 4.3 How does it work?

```text
Original value
"YASH BANDAL"
       |
       v
lowercase filter
       |
       v
"yash bandal"
       |
       v
Displayed in the template
```

The filter returns a transformed value. It does not automatically overwrite `author`.

<br>


### 4.4 Important limitation

Filters were removed in Vue 3.

In modern Vue, use an appropriate JavaScript expression or a computed property instead.

For example:

```js
computed: {
  lowercaseAuthor() {
    return this.author.toLowerCase();
  }
}
```

Then display it:

```html
<p>{{ lowercaseAuthor }}</p>
```

A computed property is especially useful when formatting depends on reactive data and the result is reused.

**Remember:** Filters are relevant to understanding your Vue 2 course. For new Vue 3 applications, use expressions, methods, or computed properties as appropriate.

<br>

---

<br>


## 5. Custom Search Filter

### 5.1 What are we building?

Imagine a page that displays several movies. The user types into a search box, and the list displays only matching movies.

Example data:

```js
movies: [
  { id: 1, title: "Inception" },
  { id: 2, title: "Interstellar" },
  { id: 3, title: "The Dark Knight" }
]
```

If the user searches for `inter`, only Interstellar should appear.

<br>


### 5.2 Vue 2 example using a computed property

A computed property is a clean way to derive a filtered list from the original movie data.

```html
<div id="app">
  <input v-model="search" placeholder="Search movies">

  <ul>
    <li v-for="movie in filteredMovies" :key="movie.id">
      {{ movie.title }}
    </li>
  </ul>
</div>
```

```js
new Vue({
  el: "#app",

  data: {
    search: "",
    movies: [
      { id: 1, title: "Inception" },
      { id: 2, title: "Interstellar" },
      { id: 3, title: "The Dark Knight" }
    ]
  },

  computed: {
    filteredMovies() {
      const query = this.search.toLowerCase().trim();

      return this.movies.filter(movie =>
        movie.title.toLowerCase().includes(query)
      );
    }
  }
});
```

<br>


### 5.3 Understand the flow

```text
User types "inter"
         |
         v
v-model updates search
         |
         v
filteredMovies recalculates
         |
         v
Movie titles are checked
         |
         v
Only matching movies are returned
         |
         v
v-for renders the filtered list
```

<br>


### 5.4 Understand the important JavaScript

**`toLowerCase()`**

Makes the search case-insensitive.

```js
"INTERSTELLAR".toLowerCase();
// "interstellar"
```

**`trim()`**

Removes spaces at the beginning and end.

```js
"  inter  ".trim();
// "inter"
```

**`filter()`**

Creates a new array containing only items that pass a condition.

```js
[1, 2, 3, 4].filter(number => number > 2);
// [3, 4]
```

**`includes()`**

Checks whether a string contains another string.

```js
"interstellar".includes("inter");
// true
```

<br>


### 5.5 Why use a computed property instead of a filter?

The name “filter” here refers to the JavaScript array method, not Vue 2's removed-in-Vue-3 template filter feature.

The computed property:
- Keeps the original `movies` array unchanged.
- Recalculates when its reactive dependencies change.
- Can be reused in the template.
- Separates search logic from HTML.

For larger lists, you may also need debouncing or server-side search, but this example is sufficient for a small client-side list.

<br>

---

<br>


## 6. Local Registration

### 6.1 What does registration mean?

Vue features sometimes need to be registered before a template can use them.

For example, a global custom directive can be used across many components. But sometimes you want a directive to be available in only one component.

That is called **local registration**.

<br>


### 6.2 Global vs local registration in Vue 2

**Global directive:**

```js
Vue.directive("focus", {
  inserted(el) {
    el.focus();
  }
});
```

It is registered on the Vue constructor and can be used by components in that Vue application.

**Local directive:**

```js
new Vue({
  el: "#app",

  directives: {
    focus: {
      inserted(el) {
        el.focus();
      }
    }
  }
});
```

Now `v-focus` is available in this Vue instance's template.

<br>


### 6.3 Why choose local registration?

Use local registration when:
- Only one component needs the behavior.
- You want to keep the feature's scope clear.
- You want to avoid registering unnecessary features globally.

Global registration is convenient for widely shared functionality. Local registration keeps component-specific behavior close to the component that uses it.

<br>


### 6.4 Vue 3 equivalent

In Vue 3, a local directive can be declared in `<script setup>` using a variable whose name begins with `v`.

```vue
<script setup>
const vFocus = {
  mounted(el) {
    el.focus();
  }
};
</script>

<template>
  <input v-focus placeholder="Enter movie title">
</template>
```

Vue recognizes `vFocus` as the implementation for `v-focus` in the template.

Notice the lifecycle hook changed:
- Vue 2: `inserted`
- Vue 3: `mounted`

<br>

---

<br>


## 7. Mixins

### 7.1 What problem do mixins solve?

Imagine you have two components:
- `MovieList`
- `BlogList`

Both need the same logic to search through a list.

Without code reuse, you might write the same computed properties and methods in both components.

Duplicated code becomes harder to maintain. If the logic changes, you must update it in multiple places.

Vue 2 mixins were one way to share component options and behavior.

<br>

> [!Tip]
> A mixin combines its options with a Vue component's options, making the mixin's data, methods, computed properties, and lifecycle hooks available to that component.

<br>


### 7.2 Creating a mixin in Vue 2

```js
const searchMixin = {
  data() {
    return {
      search: ""
    };
  },

  computed: {
    matchingItems() {
      const query = this.search.toLowerCase().trim();

      return this.items.filter(item =>
        item.title.toLowerCase().includes(query)
      );
    }
  }
};
```

This mixin assumes the component using it provides an `items` array, where every item has a `title`.

Use it in a component:

```js
new Vue({
  el: "#app",

  mixins: [searchMixin],

  data: {
    items: [
      { title: "Inception" },
      { title: "Interstellar" },
      { title: "The Dark Knight" }
    ]
  }
});
```

Now the component has access to the mixin's `search` data and `matchingItems` computed property.

The template can use them:

```html
<input v-model="search" placeholder="Search">

<ul>
  <li v-for="item in matchingItems" :key="item.title">
    {{ item.title }}
  </li>
</ul>
```

<br>


### 7.3 How does a mixin work?

```text
Component options
       +
Mixin options
       |
       v
Vue combines applicable options
       |
       v
Component can use the shared behavior
```

Vue merges the mixin's options with the component's options.

Be careful when both define the same option. For example, lifecycle hooks may both run, while conflicting data properties have merge rules that can make behavior less obvious.

<br>


### 7.4 Advantages and disadvantages

**Advantages**
- Reduces duplicated logic.
- Allows multiple components to share behavior.
- Was useful in Vue 2 applications.

**Disadvantages**
- The source of a property or method can become unclear.
- Name collisions can occur.
- Dependencies between the mixin and component can be hidden.

<br>


### 7.5 What should we use in Vue 3?

Vue 3 supports the Options API, but **composables** are generally preferred over mixins for reusable Composition API logic.

A composable is a regular JavaScript function that uses Vue's Composition API to share reactive behavior.

For example:

```js
// useSearch.js
import { ref, computed } from "vue";

export function useSearch(items) {
  const search = ref("");

  const matchingItems = computed(() => {
    const query = search.value.toLowerCase().trim();

    return items.value.filter(item =>
      item.title.toLowerCase().includes(query)
    );
  });

  return {
    search,
    matchingItems
  };
}
```

This particular example expects `items` to be a ref containing an array. A component can call `useSearch(items)` and use the returned reactive values.

We'll study composables more carefully in Part 9.

<br>

---

<br>


## 8. Quick Revision

### 8.1 Key concepts

- **HTTP request:** Communication between a client and a server.
- **GET request:** Retrieves data from a server.
- **`fetch()`:** Browser API used to make HTTP requests.
- **`async` / `await`:** Makes Promise-based asynchronous code easier to read.
- **Custom directive:** Adds reusable, low-level DOM behavior.
- **Vue 2 filter:** Formats displayed values using template syntax.
- **Computed property:** Derives a value from reactive data.
- **Local registration:** Makes a feature available only in a specific component or instance.
- **Mixin:** Shares component options and behavior in Vue 2.
- **Composable:** A reusable function pattern commonly used in Vue 3.

<br>


### 8.2 Quick questions

**Q1. Does `fetch()` throw an error for a 404 response automatically?**

No. You should check `response.ok` and handle unsuccessful HTTP responses.

**Q2. Why do we use `await response.json()`?**

It reads and parses the response body into JavaScript data. It returns a Promise, so `await` lets us wait for it.

**Q3. What is the purpose of a custom directive?**

To encapsulate reusable, low-level DOM behavior, such as focusing an input.

**Q4. Are Vue 2 filters supported in Vue 3?**

No. Use JavaScript expressions, methods, or computed properties instead.

**Q5. What is the difference between a computed property and a method?**

A computed property caches its result until its reactive dependencies change. A method normally executes whenever it is called.

**Q6. What is a mixin?**

A Vue 2 pattern for sharing component options and behavior across components.

**Q7. What is the modern alternative to mixins?**

Composables are generally preferred for reusable Composition API logic in Vue 3.

<br>


### 8.3 Mini practice task

Build a small movie-list page with the following requirements:

1. Fetch movie data from an API.
2. Display a loading message while fetching.
3. Display an error if the request fails.
4. Render the movie titles using `v-for`.
5. Add a search box that filters titles as the user types.
6. Automatically focus the search box using a custom directive.

This exercise combines HTTP requests, reactive state, computed properties, list rendering, and directives.

<br>

---

<br>


## 9. Vue 2 vs Vue 3

| Topic | Vue 2 course | Modern Vue 3 |
|---|---|---|
| HTTP requests | Vue Resource may appear in older lessons | `fetch()` or Axios |
| Custom directive hook | `inserted` | `mounted` |
| Template filters | Supported | Removed |
| Search logic | Computed properties or filters | Computed properties |
| Local directives | `directives` option | `directives` option or `vName` in `<script setup>` |
| Shared behavior | Mixins | Composables are generally preferred |

<br>


### 9.1 The main takeaway

The purpose of this part is not to memorize every Vue 2 feature. It is to understand the ideas behind them.

When building a modern Vue application, you will often:

1. Fetch data from an API.
2. Store it in reactive state.
3. Show loading, success, and error states.
4. Derive filtered or formatted values with computed properties.
5. Use directives for specialized DOM behavior.
6. Extract reusable logic into composables.

These patterns will continue to matter when we move from the Vue 2 course to Vue 3.

<br>

---

<br>


