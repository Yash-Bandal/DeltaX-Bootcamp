# Part 9 — Modern Vue 3 Mapping & Revision

**Supplementary part · Modern Vue 3**

## Table of Contents

1. [Vue 2 vs Vue 3: The Big Picture](#1-vue-2-vs-vue-3-the-big-picture)
2. [Vite and the Modern Vue Project](#2-vite-and-the-modern-vue-project)
3. [Options API vs Composition API](#3-options-api-vs-composition-api)
4. [`ref()` and `reactive()`](#4-ref-and-reactive)
5. [The `<script setup>` Syntax](#5-the-script-setup-syntax)
6. [Vue Router 4](#6-vue-router-4)
7. [Pinia for State Management](#7-pinia-for-state-management)
8. [Composables for Reusable Logic](#8-composables-for-reusable-logic)
9. [Putting It Together: A Movie List](#9-putting-it-together-a-movie-list)
10. [Quick Revision and Interview Questions](#10-quick-revision-and-interview-questions)

<br>


---

## 1. Vue 2 vs Vue 3: The Big Picture

### 1.1 Why are there two versions in our learning path?

Your Net Ninja playlist teaches Vue 2. Many of its concepts still apply to Vue 3, but some APIs and recommended patterns have changed.

The goal is not to forget Vue 2. It is to understand how the concepts you learned translate into modern Vue development.

<br>


### 1.2 Main differences

| Concept | Vue 2 course | Modern Vue 3 |
|---|---|---|
| App creation | `new Vue()` | `createApp()` |
| Common component style | Options API | Options API or Composition API |
| Reactive state | `data()` | `ref()` / `reactive()` in Composition API |
| Component files | `.vue` files supported | `.vue` files commonly used with Vite |
| Routing | Vue Router 3 | Vue Router 4 |
| State management | Vuex commonly used | Pinia commonly recommended |
| Shared logic | Mixins | Composables commonly preferred |
| Template filters | Supported | Removed; use computed properties or functions |

<br>


### 1.3 The mental model stays the same

Even in Vue 3, the central idea remains:

```text
State changes
     |
     v
Vue detects reactive changes
     |
     v
Affected UI updates
```

For example, when a movie is added to a reactive array, Vue can update the movie list without manually rebuilding the DOM.

**Remember:** Vue 3 is not an entirely different framework. It builds on the same core ideas, with improved APIs and new patterns.

<br>

---

<br>


## 2. Vite and the Modern Vue Project

### 2.1 What is Vite?

Vite is a frontend development tool that helps you create, run, and build modern applications.

It provides:
- A development server.
- Fast updates while you edit code.
- Support for Vue single-file components.
- A production build process.

Vite is not Vue itself. Vue is the framework; Vite is the tool that runs and builds your project.

<br>


### 2.2 Create a Vue 3 project

Make sure Node.js is installed. Then run:

```bash
npm create vue@latest
```

Follow the prompts to create your project.

Move into the generated folder and install dependencies:

```bash
cd your-project-name
npm install
npm run dev
```

Vite will print a local URL, usually something like:

```text
http://localhost:5173/
```

Open that URL in your browser.

<br>


### 2.3 Typical project structure

```text
src/
├── assets/
├── components/
├── views/
├── router/
├── stores/
├── App.vue
└── main.js

package.json
vite.config.js
```

This is an example structure, not a mandatory rule. A small project may not need every folder.

- `components/`: reusable UI pieces.
- `views/`: pages displayed by the router.
- `router/`: route configuration.
- `stores/`: shared application state, often using Pinia.
- `App.vue`: root component.
- `main.js`: application entry point.

<br>


### 2.4 What does `main.js` do?

A typical entry point looks like this:

```js
import { createApp } from "vue";
import App from "./App.vue";

createApp(App).mount("#app");
```

Flow:

```text
main.js executes
      |
      v
createApp(App)
      |
      v
Vue creates the application
      |
      v
mount("#app")
      |
      v
App.vue appears inside the HTML element
```

<br>


### 2.5 What is a single-file component?

A `.vue` file can contain three main sections:

```vue
<script setup>
const message = "Hello Vue 3";
</script>

<template>
  <h1>{{ message }}</h1>
</template>

<style scoped>
h1 {
  font-weight: bold;
}
</style>
```

- `<script setup>` contains JavaScript logic.
- `<template>` describes the UI.
- `<style scoped>` applies styles scoped to that component.

You can think of a single-file component as one place to organize a component's logic, template, and styles.

<br>

---

<br>


## 3. Options API vs Composition API

### 3.1 Options API — the style you learned in Vue 2

Your course uses code like this:

```js
new Vue({
  data: {
    count: 0
  },

  methods: {
    increment() {
      this.count++;
    }
  }
});
```

In Vue 3, the Options API is still supported:

```vue
<script>
export default {
  data() {
    return {
      count: 0
    };
  },

  methods: {
    increment() {
      this.count++;
    }
  }
};
</script>

<template>
  <p>{{ count }}</p>
  <button @click="increment">Add</button>
</template>
```

The Options API groups code by type:
- Data.
- Methods.
- Computed properties.
- Lifecycle hooks.

<br>


### 3.2 Composition API — a modern alternative

The Composition API lets you organize code by related functionality.

```vue
<script setup>
import { ref } from "vue";

const count = ref(0);

function increment() {
  count.value++;
}
</script>

<template>
  <p>{{ count }}</p>
  <button @click="increment">Add</button>
</template>
```

Notice that `this` is not needed to access `count`.

<br>


### 3.3 When should you use each?

- **Options API:** A good choice for learning, maintaining existing Vue 2 code, or using a style your team already follows.
- **Composition API:** Useful for organizing complex components and extracting reusable logic into composables.

Neither style is automatically correct for every project. The important thing is to understand both.

<br>

---

<br>


## 4. `ref()` and `reactive()`

These are two important ways to create reactive state with the Composition API.

### 4.1 What is `ref()`?

Use `ref()` to create a reactive value.

```js
import { ref } from "vue";

const count = ref(0);
```

When updating it in JavaScript, use `.value`:

```js
count.value++;
```

In a template, Vue unwraps refs automatically:

```vue
<template>
  <p>{{ count }}</p>
</template>
```

### 4.2 How does it work?

```text
const count = ref(0)
          |
          v
Reactive value created
          |
          v
count.value changes to 1
          |
          v
Vue detects the change
          |
          v
Template updates
```

Use `ref()` for numbers, strings, Booleans, arrays, objects, and other values when you want reactive state.

<br>


### 4.3 What is `reactive()`?

`reactive()` makes an object reactive.

```js
import { reactive } from "vue";

const movie = reactive({
  title: "Inception",
  year: 2010
});
```

You can update its properties directly:

```js
movie.title = "Interstellar";
movie.year = 2014;
```

In the template:

```vue
<template>
  <h2>{{ movie.title }}</h2>
  <p>{{ movie.year }}</p>
</template>
```

<br>


### 4.4 `ref()` vs `reactive()`

| Feature | `ref()` | `reactive()` |
|---|---|---|
| Accepts primitive values | Yes | No; designed for objects |
| JavaScript access | `.value` | Direct properties |
| Template access | Automatically unwrapped | Direct properties |
| Common example | `const count = ref(0)` | `const movie = reactive({...})` |

A common beginner-friendly approach is to use `ref()` for individual values and `reactive()` when you want to work with an object as one reactive unit.

One important detail: avoid replacing a `reactive()` object with a completely new object. If you need to replace the whole value, a `ref()` is often more convenient.

<br>

---

<br>


## 5. The `<script setup>` Syntax

### 5.1 What does `<script setup>` mean?

`<script setup>` is a convenient syntax for writing Composition API code inside a Vue single-file component.

It reduces the boilerplate needed to expose state and functions to the template.

```vue
<script setup>
import { ref } from "vue";

const movieTitle = ref("Inception");

function changeTitle() {
  movieTitle.value = "Interstellar";
}
</script>

<template>
  <h2>{{ movieTitle }}</h2>
  <button @click="changeTitle">Change movie</button>
</template>
```

You do not need to return `movieTitle` or `changeTitle`. Top-level declarations are available to the template.

<br>


### 5.2 Why is it useful?

Without `<script setup>`, a Composition API component commonly uses `setup()` and returns values for the template.

With `<script setup>`, that boilerplate is removed.

This makes components easier to read, especially when they have several reactive values and functions.

<br>


### 5.3 Important rule

Inside JavaScript:

```js
movieTitle.value = "Interstellar";
```

Inside the template:

```html
<h2>{{ movieTitle }}</h2>
```

Use `.value` in JavaScript when accessing a ref directly. Do not add `.value` in the template.

<br>

---

<br>


## 6. Vue Router 4

### 6.1 Why do we need it?

Routing lets your application display different views based on the URL.

For example:

```text
/           → Home
/movies     → Movie list
/movies/10  → Movie details
```

Vue Router 4 is the router version designed for Vue 3.

<br>


### 6.2 Install Vue Router 4

```bash
npm install vue-router@4
```

<br>


### 6.3 Define routes

```js
// src/router/index.js
import { createRouter, createWebHistory } from "vue-router";

import Home from "../views/Home.vue";
import Movies from "../views/Movies.vue";
import MovieDetails from "../views/MovieDetails.vue";

const routes = [
  { path: "/", component: Home },
  { path: "/movies", component: Movies },
  { path: "/movies/:id", component: MovieDetails }
];

export default createRouter({
  history: createWebHistory(),
  routes
});
```

<br>


### 6.4 Register the router

```js
// src/main.js
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";

createApp(App)
  .use(router)
  .mount("#app");
```

<br>


### 6.5 Display the views

```vue
<template>
  <nav>
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/movies">Movies</RouterLink>
  </nav>

  <RouterView />
</template>
```

`RouterLink` provides navigation, while `RouterView` displays the matched component.

<br>


### 6.6 Read a route parameter

Inside `MovieDetails.vue`:

```vue
<script setup>
import { useRoute } from "vue-router";

const route = useRoute();
</script>

<template>
  <h2>Movie ID: {{ route.params.id }}</h2>
</template>
```

If the URL is `/movies/10`, the component receives the ID `"10"` through `route.params.id`.

<br>

---

<br>


## 7. Pinia for State Management

### 7.1 What problem does Pinia solve?

Imagine your movie application has:
- A navigation bar displaying the user's name.
- A movie list.
- A favourites page.
- A profile page.

If several unrelated components need the same data, passing props through many layers can become difficult.

Pinia provides a shared store where you can manage application state.

<br>


### 7.2 Install Pinia

```bash
npm install pinia
```

Register it in `main.js`:

```js
import { createApp } from "vue";
import { createPinia } from "pinia";
import App from "./App.vue";

const app = createApp(App);

app.use(createPinia());
app.mount("#app");
```

<br>


### 7.3 Create a movie store

```js
// src/stores/movieStore.js
import { defineStore } from "pinia";
import { ref } from "vue";

export const useMovieStore = defineStore("movies", () => {
  const favourites = ref([]);

  function addFavourite(movie) {
    if (!favourites.value.some(item => item.id === movie.id)) {
      favourites.value.push(movie);
    }
  }

  function removeFavourite(movieId) {
    favourites.value = favourites.value.filter(
      movie => movie.id !== movieId
    );
  }

  return {
    favourites,
    addFavourite,
    removeFavourite
  };
});
```

This is a Pinia setup store. It uses `ref()` for state and functions for actions.

<br>


### 7.4 Use the store in a component

```vue
<script setup>
import { useMovieStore } from "../stores/movieStore";

const movieStore = useMovieStore();

function addInception() {
  movieStore.addFavourite({
    id: 1,
    title: "Inception"
  });
}
</script>

<template>
  <button @click="addInception">
    Add Inception to favourites
  </button>

  <p>Favourites: {{ movieStore.favourites.length }}</p>
</template>
```

Another component can access the same store instance and see the shared state.

<br>


### 7.5 When should you use Pinia?

Use it when multiple parts of your application need shared state.

You do not need Pinia for every component. Local component state is often enough for a small form or a single counter.

**Remember:** Pinia stores state in memory by default. If you need the state to survive a page reload, implement persistence, such as saving selected data to local storage or a backend.

<br>

---

<br>


## 8. Composables for Reusable Logic

### 8.1 What is a composable?

A composable is a JavaScript function that uses Vue's Composition API to share reactive behavior.

For example, multiple components may need the same search logic.

Instead of duplicating it, you can create a `useSearch()` function.

<br>


### 8.2 Create a composable

```js
// src/composables/useSearch.js
import { ref, computed } from "vue";

export function useSearch(items, getText) {
  const search = ref("");

  const filteredItems = computed(() => {
    const query = search.value.toLowerCase().trim();

    return items.value.filter(item =>
      getText(item).toLowerCase().includes(query)
    );
  });

  return {
    search,
    filteredItems
  };
}
```

This example expects `items` to be a ref containing an array. `getText` is a function that extracts the text to search.

<br>


### 8.3 Use the composable

```vue
<script setup>
import { ref } from "vue";
import { useSearch } from "../composables/useSearch";

const movies = ref([
  { id: 1, title: "Inception" },
  { id: 2, title: "Interstellar" },
  { id: 3, title: "The Dark Knight" }
]);

const { search, filteredItems } = useSearch(
  movies,
  movie => movie.title
);
</script>

<template>
  <input v-model="search" placeholder="Search movies">

  <ul>
    <li v-for="movie in filteredItems" :key="movie.id">
      {{ movie.title }}
    </li>
  </ul>
</template>
```

<br>


### 8.4 How does the flow work?

```text
Component passes movie data
           |
           v
useSearch() creates reactive search state
           |
           v
Computed property filters movies
           |
           v
Component receives filteredItems
           |
           v
Template renders matching movies
```

<br>


### 8.5 Composable vs mixin

| Vue 2 mixin | Vue 3 composable |
|---|---|
| Merges options into a component | Returns values and functions explicitly |
| Shared properties may be difficult to trace | Returned dependencies are visible |
| Can cause naming conflicts | Names can be chosen when destructuring |
| Common in older Vue 2 code | Common in Composition API code |

Composables are not simply renamed mixins. They use ordinary JavaScript functions and Composition API features, giving you more explicit control over what is shared.

<br>

---

<br>


## 9. Putting It Together: A Movie List

### 9.1 What are we building?

We'll combine several modern Vue concepts:

- Vite and a `.vue` component.
- `ref()` for reactive state.
- `onMounted()` to start loading data.
- `fetch()` to call an API.
- `computed()` to filter the movie list.

This example uses a public demo endpoint, so it demonstrates the frontend flow rather than a real production movie service.

<br>


### 9.2 Example component

```vue
<script setup>
import { ref, computed, onMounted } from "vue";

const movies = ref([]);
const search = ref("");
const loading = ref(false);
const error = ref("");

const filteredMovies = computed(() => {
  const query = search.value.toLowerCase().trim();

  return movies.value.filter(movie =>
    movie.title.toLowerCase().includes(query)
  );
});

async function fetchMovies() {
  loading.value = true;
  error.value = "";

  try {
    const response = await fetch(
      "https://jsonplaceholder.typicode.com/posts"
    );

    if (!response.ok) {
      throw new Error("Unable to load movies");
    }

    const posts = await response.json();

    movies.value = posts.slice(0, 10).map(post => ({
      id: post.id,
      title: post.title
    }));
  } catch (err) {
    error.value = err.message;
  } finally {
    loading.value = false;
  }
}

onMounted(fetchMovies);
</script>

<template>
  <section>
    <h1>Movie Explorer</h1>

    <input v-model="search" placeholder="Search titles">

    <p v-if="loading">Loading...</p>
    <p v-else-if="error">{{ error }}</p>

    <ul v-else>
      <li v-for="movie in filteredMovies" :key="movie.id">
        {{ movie.title }}
      </li>
    </ul>
  </section>
</template>
```

The demo endpoint returns posts, not movies. We use the post titles as sample data so we can practise fetching and filtering.

### 9.3 Understand the execution order

1. Vue initializes the reactive state.
2. `onMounted(fetchMovies)` registers a callback.
3. The component mounts, and `fetchMovies()` starts.
4. The application waits for the HTTP response.
5. The response is parsed and mapped into movie-like objects.
6. Assigning `movies.value` updates reactive state.
7. The computed property derives the filtered list.
8. Vue renders the matching titles.
9. When the user types, `search` updates and the filtered list recalculates.

This is a common pattern in modern Vue applications.

<br>

---

<br>


<br>




