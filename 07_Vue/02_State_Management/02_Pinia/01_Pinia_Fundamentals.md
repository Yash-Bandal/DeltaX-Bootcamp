# Pinia — Reusable Setup & Implementation Guide

## Table of Contents

1. [What Is Pinia?](#1-what-is-pinia)
2. [Set Up Pinia](#2-set-up-pinia)
3. [Create Your First Store](#3-create-your-first-store)
4. [Register Pinia in the Application](#4-register-pinia-in-the-application)
5. [Use the Store in a Component](#5-use-the-store-in-a-component)
6. [Understand Getters](#6-understand-getters)
7. [Update State Using Actions](#7-update-state-using-actions)
8. [Handle API Calls with Async Actions](#8-handle-api-calls-with-async-actions)
9. [Use `storeToRefs()`](#9-use-storetorefs)
10. [Complete Example — Movie Store](#10-complete-example--movie-store)
11. [Vuex vs Pinia](#11-vuex-vs-pinia)
12. [Reusable Implementation Checklist](#12-reusable-implementation-checklist)
13. [Quick Revision](#13-quick-revision)

<br>



---

## 1. What Is Pinia?

Pinia is a **state management library for Vue**. It allows multiple components to share and manage the same data.

Consider a movie application.

- `MovieList.vue` displays movies.
- `MovieDetails.vue` displays information about one movie.
- `Navbar.vue` displays the number of favourite movies.

All three components may need access to the same movie data.

Without a store, you might pass data through multiple components. With Pinia, they can access a shared store directly.

```text
                 Pinia Store
              ┌───────────────┐
              │ State         │
              │ Getters       │
              │ Actions       │
              └───────┬───────┘
                      │
           ┌──────────┼──────────┐
           ▼          ▼          ▼
       MovieList  MovieDetails  Navbar
```

**Remember:** A Pinia store is a central place to keep and manage shared application state.

### 1.1 The three main parts

| Pinia concept | Purpose | Vuex equivalent |
|---|---|---|
| `state` | Stores the data | `state` |
| `getters` | Derives values from state | `getters` |
| `actions` | Performs operations and updates state | `actions` and mutations combined |

Pinia has no separate mutations requirement. Actions can update state directly.

<br>

---


<br>


## 2. Set Up Pinia

For a Vue 3 project created with Vue CLI or Vite, install Pinia inside your project folder.

```bash
npm install pinia
```

If you are starting a new modern Vue project, Vite is a common choice:

```bash
npm create vite@latest my-vue-app -- --template vue
cd my-vue-app
npm install
npm install pinia
npm run dev
```

If you already have a Vue 3 project, you only need to install Pinia.

**Important:** Pinia works with Vue 2.7 and Vue 3, but this guide uses Vue 3 syntax.

<br>

---


<br>


## 3. Create Your First Store

Create a folder and file with this structure:

```text
src/
├── stores/
│   └── counterStore.js
├── App.vue
└── main.js
```

Create `src/stores/counterStore.js`:

```javascript
import { defineStore } from 'pinia'

export const useCounterStore = defineStore('counter', {
  // 1. State: the data
  state: () => ({
    count: 0
  }),

  // 2. Getters: derived values
  getters: {
    doubleCount: (state) => state.count * 2
  },

  // 3. Actions: operations
  actions: {
    increment() {
      this.count++
    },

    decrement() {
      this.count--
    }
  }
})
```

<br>


### 3.1 Understand each part

**`defineStore()`**

Creates a store definition.

```javascript
defineStore('counter', { ... })
```

- `'counter'` is the unique store ID.
- The object defines the state, getters and actions.
- `useCounterStore` is the function components call to access the store.

**`state`**

```javascript
state: () => ({
  count: 0
})
```

Defines the initial state. Each application instance can maintain its own store state.

**`getters`**

```javascript
doubleCount: (state) => state.count * 2
```

Calculates a value from the existing state.

**`actions`**

```javascript
increment() {
  this.count++
}
```

Contains operations that can read or update state. Inside an action, `this` refers to the store.

<br>

---


<br>



## 4. Register Pinia in the Application

Creating a store is not enough. You must register Pinia with your Vue application.

Open `src/main.js`:

```javascript
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'

const app = createApp(App)

app.use(createPinia())

app.mount('#app')
```

### What happens here?

1. `createApp(App)` creates the Vue application.
2. `createPinia()` creates the Pinia instance.
3. `app.use(...)` registers Pinia so components can access stores.
4. `app.mount('#app')` mounts the Vue application.

**Remember:** Register Pinia once in the application entry point, usually `main.js`.

<br>

---


<br>



## 5. Use the Store in a Component

Open `src/App.vue`:

```vue
<script setup>
import { useCounterStore } from './stores/counterStore'

const counterStore = useCounterStore()
</script>

<template>
  <h2>Counter: {{ counterStore.count }}</h2>

  <p>Double: {{ counterStore.doubleCount }}</p>

  <button @click="counterStore.increment()">
    Increment
  </button>

  <button @click="counterStore.decrement()">
    Decrement
  </button>
</template>
```

### Follow the execution flow

When the application starts:

1. `App.vue` imports `useCounterStore`.
2. `useCounterStore()` retrieves the registered store instance.
3. The template reads `counterStore.count`.
4. Clicking **Increment** calls the `increment()` action.
5. The action changes `count`.
6. Vue reacts to the state change and updates the displayed values.

```text
User clicks Increment
        ↓
increment() action
        ↓
this.count++
        ↓
Pinia state changes
        ↓
Vue updates the UI
```

You do not need to manually refresh the page.

<br>

---


<br>



## 6. Understand Getters

A getter calculates a value using the store's state. It is similar to a computed property.

Add these getters to `counterStore.js`:

```javascript
getters: {
  doubleCount: (state) => state.count * 2,

  isPositive: (state) => state.count > 0
}
```

Use them in the component:

```vue
<template>
  <p>Count: {{ counterStore.count }}</p>
  <p>Double: {{ counterStore.doubleCount }}</p>
  <p>Positive: {{ counterStore.isPositive }}</p>
</template>
```

<br>

### Why use getters?

Suppose you need to display the number of favourite movies in several components.

Instead of writing the counting logic in every component, define it once in the store.

```javascript
getters: {
  favouriteCount: (state) => state.favourites.length
}
```

Now any component using the store can access:

```javascript
counterStore.favouriteCount
```

Getters are cached based on their reactive dependencies, like computed properties.

**Remember:** Use getters when you need a reusable, derived value from state.

<br>

---


<br>



## 7. Update State Using Actions

Actions are methods defined inside the store. They can read state, update state and call other actions.

Example:

```javascript
actions: {
  setCount(value) {
    this.count = value
  },

  resetCount() {
    this.count = 0
  },

  incrementBy(value) {
    this.count += value
  }
}
```

Call them from a component:

```vue
<script setup>
import { useCounterStore } from './stores/counterStore'

const counterStore = useCounterStore()
</script>

<template>
  <button @click="counterStore.incrementBy(5)">
    Add 5
  </button>

  <button @click="counterStore.resetCount()">
    Reset
  </button>
</template>
```

<br>


### Do we need mutations like Vuex?

No. In Pinia, an action can directly update state:

```javascript
actions: {
  increment() {
    this.count++
  }
}
```

You do not need to create a separate mutation and call `commit()`.

You can also modify state directly through the store, such as `counterStore.count++`. However, using actions for important business operations keeps your code easier to understand and maintain.

<br>

---


<br>



## 8. Handle API Calls with Async Actions

Real applications often need to fetch data from an API.

For example, your movie application may request movies from a backend.

A useful pattern is:

1. Set a loading state.
2. Call the API.
3. Store the response in state.
4. Handle errors.
5. Stop loading.



<br>


### 8.1 Create the movie store

Create `src/stores/movieStore.js`:

```javascript
import { defineStore } from 'pinia'

export const useMovieStore = defineStore('movies', {
  state: () => ({
    movies: [],
    loading: false,
    error: null
  }),

  getters: {
    movieCount: (state) => state.movies.length
  },

  actions: {
    async fetchMovies() {
      this.loading = true
      this.error = null

      try {
        const response = await fetch(
          'https://jsonplaceholder.typicode.com/posts'
        )

        if (!response.ok) {
          throw new Error('Failed to fetch movies')
        }

        const data = await response.json()

        // Demo only: the endpoint returns posts, not real movies.
        this.movies = data
      } catch (error) {
        this.error = error.message
      } finally {
        this.loading = false
      }
    }
  }
})
```

This example uses a public demo endpoint that returns posts. In your actual application, replace the URL with your movie API and adjust the response mapping.

<br>

### 8.2 Use the store

```vue
<script setup>
import { onMounted } from 'vue'
import { useMovieStore } from './stores/movieStore'

const movieStore = useMovieStore()

onMounted(() => {
  movieStore.fetchMovies()
})
</script>

<template>
  <p v-if="movieStore.loading">Loading...</p>

  <p v-else-if="movieStore.error">
    {{ movieStore.error }}
  </p>

  <ul v-else>
    <li v-for="movie in movieStore.movies" :key="movie.id">
      {{ movie.title }}
    </li>
  </ul>
</template>
```

<br>


### 8.3 Follow the API flow

```text
Component mounts
       ↓
fetchMovies()
       ↓
loading = true
       ↓
API request using fetch()
       ↓
Response received
       ↓
Convert response to JSON
       ↓
Save data in movies
       ↓
loading = false
       ↓
Vue renders the list
```

If the request fails, the `catch` block stores the error, and the `finally` block still resets `loading`.

**Remember:** The component decides when to request the data. The store handles the request and manages the shared state.

<br>

---


<br>



## 9. Use `storeToRefs()`

There is an important detail when extracting state from a Pinia store.

Suppose you write:

```javascript
const movieStore = useMovieStore()

const { movies, loading } = movieStore
```

This destructuring does not preserve the reactive connection for state properties. The extracted variables will not automatically follow later state changes.

Use `storeToRefs()` instead:

```javascript
import { storeToRefs } from 'pinia'
import { useMovieStore } from './stores/movieStore'

const movieStore = useMovieStore()

const { movies, loading, error } = storeToRefs(movieStore)
```

Now you can use these references in your template:

```vue
<template>
  <p v-if="loading">Loading...</p>
  <p v-else-if="error">{{ error }}</p>

  <ul v-else>
    <li v-for="movie in movies" :key="movie.id">
      {{ movie.title }}
    </li>
  </ul>
</template>
```

The extracted state and getter references remain reactive.

**Important distinction:**

- Use `storeToRefs()` for state and getters you want to extract.
- Actions can be destructured directly, although calling `movieStore.fetchMovies()` is often clearer.

<br>

---


<br>



## 10. Complete Example — Movie Store

Here is how the pieces fit together in a small application.

### 10.1 Store

`src/stores/movieStore.js`

```javascript
import { defineStore } from 'pinia'

export const useMovieStore = defineStore('movies', {
  state: () => ({
    movies: [],
    favourites: []
  }),

  getters: {
    movieCount: (state) => state.movies.length,

    favouriteCount: (state) => state.favourites.length
  },

  actions: {
    addMovie(movie) {
      this.movies.push(movie)
    },

    toggleFavourite(movieId) {
      const index = this.favourites.indexOf(movieId)

      if (index === -1) {
        this.favourites.push(movieId)
      } else {
        this.favourites.splice(index, 1)
      }
    }
  }
})
```

<br>


### 10.2 Component

`src/App.vue`

```vue
<script setup>
import { useMovieStore } from './stores/movieStore'

const movieStore = useMovieStore()

function addSampleMovie() {
  movieStore.addMovie({
    id: 1,
    title: 'Inception'
  })
}
</script>

<template>
  <h2>Movie Explorer</h2>

  <button @click="addSampleMovie">
    Add Inception
  </button>

  <p>Total movies: {{ movieStore.movieCount }}</p>
  <p>Favourites: {{ movieStore.favouriteCount }}</p>

  <ul>
    <li v-for="movie in movieStore.movies" :key="movie.id">
      {{ movie.title }}

      <button @click="movieStore.toggleFavourite(movie.id)">
        Toggle Favourite
      </button>
    </li>
  </ul>
</template>
```

<br>

### 10.3 What happens when you click the button?

When you click **Add Inception**:

1. The component calls `addMovie()`.
2. The action adds the movie to `movies`.
3. `movieCount` recalculates because its dependency changed.
4. Vue updates the list and movie count.

When you click **Toggle Favourite**:

1. The component passes the movie ID to the action.
2. The action adds or removes that ID from `favourites`.
3. `favouriteCount` updates.
4. The UI displays the new count.

This example demonstrates the main purpose of Pinia: shared, reactive state with reusable operations.

<br>

---


<br>



## 11. Vuex vs Pinia

| Vuex | Pinia |
|---|---|
| `state` | `state` |
| `getters` | `getters` |
| `mutations` | No separate mutations required |
| `actions` | `actions` |
| `commit('increment')` | `store.increment()` |
| `dispatch('fetchMovies')` | `store.fetchMovies()` |
| `mapGetters` / `mapActions` commonly used | Direct store access is common |
| More explicit mutation flow | Simpler store API |

Example:

**Vuex**

```javascript
this.$store.commit('increment')
this.$store.dispatch('fetchMovies')
```

**Pinia**

```javascript
counterStore.increment()
movieStore.fetchMovies()
```

Pinia simplifies the API while keeping the core idea of shared state management.

<br>

---


<br>



## 12. Reusable Implementation Checklist

Whenever you need a store in a Vue project, follow these steps in order.

- [ ] **Step 1:** Install Pinia using `npm install pinia`.
- [ ] **Step 2:** Create a `src/stores/` folder.
- [ ] **Step 3:** Create a store file, such as `movieStore.js`.
- [ ] **Step 4:** Import `defineStore` from `pinia`.
- [ ] **Step 5:** Define a unique store ID and add `state`, `getters` and `actions` as required.
- [ ] **Step 6:** Register Pinia in `main.js` using `app.use(createPinia())`.
- [ ] **Step 7:** Import the store in the component and call its `use...Store()` function.
- [ ] **Step 8:** Read state and getters in the template or component logic.
- [ ] **Step 9:** Call actions to perform operations.
- [ ] **Step 10:** For API calls, handle loading, errors and successful responses.
- [ ] **Step 11:** Use `storeToRefs()` when destructuring reactive state or getters.
- [ ] **Step 12:** Test that changes in one component appear in other components using the same store.

### Common mistakes to avoid

1. **Forgetting to register Pinia:** The store cannot be used until Pinia is installed on the Vue application.
2. **Using the wrong store ID:** Store IDs should be unique.
3. **Destructuring without `storeToRefs()`:** Extracted state properties lose their reactive connection.
4. **Putting all data in one global store:** Keep state local to a component when no other component needs it.
5. **Ignoring API errors:** Handle unsuccessful HTTP responses as well as network failures.
6. **Copying store state into component data unnecessarily:** Use the store directly when you need to share reactive state.

<br>


### Final mental model

```text
1. Create a store
       ↓
2. Register Pinia
       ↓
3. Access the store in a component
       ↓
4. Read state and getters
       ↓
5. Call actions
       ↓
6. State changes
       ↓
7. Vue updates the UI
```

**The key idea:** Pinia manages shared state. Components read that state and call actions. When reactive state changes, Vue updates the UI automatically.
