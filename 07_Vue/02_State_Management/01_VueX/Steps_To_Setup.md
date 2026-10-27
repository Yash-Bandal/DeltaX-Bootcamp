# Vuex — Reusable Setup & Implementation Guide

**Vue 2 + Vuex 3 · Practical reference**

## Table of Contents

1. [Vuex: The Basic Idea](#1-vuex-the-basic-idea)
2. [Step 1 — Install Vuex](#2-step-1--install-vuex)
3. [Step 2 — Create the Store](#3-step-2--create-the-store)
4. [Step 3 — Register the Store](#4-step-3--register-the-store)
5. [Step 4 — Read State in a Component](#5-step-4--read-state-in-a-component)
6. [Step 5 — Create Getters](#6-step-5--create-getters)
7. [Step 6 — Create Mutations](#7-step-6--create-mutations)
8. [Step 7 — Create Actions](#8-step-7--create-actions)
9. [Step 8 — Connect Everything to a Component](#9-step-8--connect-everything-to-a-component)
10. [Step 9 — Use Mapping Helpers](#10-step-9--use-mapping-helpers)
11. [Complete Execution Flow](#11-complete-execution-flow)
12. [Reusable Store Checklist](#12-reusable-store-checklist)
13. [Quick Revision](#13-quick-revision)

<br>


---

## 1. Vuex: The Basic Idea

### 1.1 What is Vuex?

Vuex is a state management library for Vue applications. It gives you a central store for data that multiple components need to share.

Imagine a movie application with a movie list, a favourites page, and a navbar displaying the number of favourites.

All three components need access to the same data.

Instead of maintaining separate copies, we can keep the shared data in a Vuex store.

```text
MovieList ───────┐
                 |
Navbar ──────────┼────> Vuex Store
                 |      - movies
Favourites ──────┘      - favourites
```

<br>


### 1.2 The five things to understand

| Concept | What it does |
|---|---|
| `state` | Stores shared data |
| `getters` | Derives values from state |
| `mutations` | Changes state synchronously |
| `actions` | Coordinates operations, including asynchronous work |
| `store` | Holds the shared state and related logic |

**The basic rule:** Components read the store, and state changes follow a predictable path through mutations.

<br>


---

<br>


## 2. Step 1 — Install Vuex

For a Vue 2 project, install Vuex 3.

```bash
npm install vuex@3
```

Vuex 3 is intended for Vue 2. Vuex 4 is intended for Vue 3.

<br>


---

<br>


## 3. Step 2 — Create the Store

### 3.1 Create the file

Create a file named:

```text
src/store.js
```

Start with this basic structure:

```js
import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    count: 0
  },

  getters: {},

  mutations: {},

  actions: {}
});
```

<br>


### 3.2 Understand the structure

- `state`: The data shared across components.
- `getters`: Calculations based on store state.
- `mutations`: Functions that update state.
- `actions`: Functions that coordinate operations and can handle asynchronous tasks.

You don't have to fill every section immediately. Add the sections your application needs.

<br>


### 3.3 Example: Store movie data

Suppose the application needs a shared movie list.

```js
state: {
  movies: [
    { id: 1, title: "Inception", favourite: false },
    { id: 2, title: "Interstellar", favourite: true }
  ]
}
```

Now the movie data lives in the store rather than inside one specific component.

<br>


---

<br>


## 4. Step 3 — Register the Store

Creating the store is not enough. You must connect it to the Vue application.

Open `src/main.js`:

```js
import Vue from "vue";
import App from "./App.vue";
import store from "./store";

new Vue({
  store,
  render: h => h(App)
}).$mount("#app");
```

The important line is:

```js
store
```

It makes the Vuex store available throughout the component tree.

After registration, components can access it through `this.$store`.

```text
store.js
   |
   v
main.js registers the store
   |
   v
App.vue and child components
   |
   v
this.$store becomes available
```

You normally register the store once in the application's entry point.

<br>


---

<br>


## 5. Step 4 — Read State in a Component

### 5.1 Access state directly

Suppose the store contains:

```js
state: {
  count: 0
}
```

A component can read the value like this:

```js
this.$store.state.count
```

Example:

```vue
<template>
  <h2>Count: {{ count }}</h2>
</template>

<script>
export default {
  computed: {
    count() {
      return this.$store.state.count;
    }
  }
};
</script>
```

<br>


### 5.2 Why use a computed property?

The computed property reads the store's reactive state.

When the store's `count` changes, the computed property updates, and Vue refreshes the displayed value.

Use computed properties when you want to expose store data to a template.

**Remember:** Reading state and changing state are two different operations.

<br>


---

<br>


## 6. Step 5 — Create Getters

### 6.1 When do you need a getter?

Use a getter when you want to derive a value from store state and potentially reuse that calculation across components.

For example, you have a movie list but want to display only favourite movies.

Add a getter inside `store.js`:

```js
getters: {
  favouriteMovies(state) {
    return state.movies.filter(movie => movie.favourite);
  }
}
```

<br>


### 6.2 Use the getter

```js
this.$store.getters.favouriteMovies
```

Or expose it through a component's computed properties:

```js
computed: {
  favouriteMovies() {
    return this.$store.getters.favouriteMovies;
  }
}
```

Then use it in the template:

```html
<li v-for="movie in favouriteMovies" :key="movie.id">
  {{ movie.title }}
</li>
```

<br>


### 6.3 How it works

```text
Store state.movies
       |
       v
Getter filters favourite movies
       |
       v
Component reads the getter
       |
       v
Template displays the result
```

**Rule:** Getters calculate values. They should not modify the store state.

<br>


---

<br>


## 7. Step 6 — Create Mutations

### 7.1 When do you need a mutation?

Whenever you want to change Vuex state, define a mutation.

For example, a user clicks a button to increase the counter.

In `store.js`:

```js
mutations: {
  increment(state) {
    state.count++;
  }
}
```

<br>


### 7.2 Commit the mutation

From a component:

```js
this.$store.commit("increment");
```

For a mutation that needs additional data, pass a payload.

Store:

```js
mutations: {
  incrementBy(state, amount) {
    state.count += amount;
  }
}
```

Component:

```js
this.$store.commit("incrementBy", 5);
```

Here, `5` is the payload passed to the mutation.

<br>


### 7.3 Understand the flow

```text
User clicks button
       |
       v
commit("increment")
       |
       v
Mutation runs
       |
       v
Store state changes
       |
       v
Components update
```

**Rule:** Vuex mutations should be synchronous. Use actions when you need asynchronous work.

<br>


---

<br>


## 8. Step 7 — Create Actions

### 8.1 When do you need an action?

Use an action when an operation involves more than a simple synchronous state change.

Typical examples:
- Fetching data from an API.
- Saving data to a backend.
- Performing asynchronous operations.
- Coordinating multiple state changes.

Add an action to `store.js`:

```js
actions: {
  async fetchMovies({ commit }) {
    const response = await fetch(
      "https://jsonplaceholder.typicode.com/posts"
    );

    if (!response.ok) {
      throw new Error("Failed to fetch movies");
    }

    const posts = await response.json();

    const movies = posts.map(post => ({
      id: post.id,
      title: post.title,
      favourite: false
    }));

    commit("setMovies", movies);
  }
}
```

This example uses a public demo endpoint that returns posts, mapping their titles into movie-like data.

Add the mutation used by the action:

```js
mutations: {
  setMovies(state, movies) {
    state.movies = movies;
  }
}
```

<br>


### 8.2 Dispatch the action

From a component:

```js
this.$store.dispatch("fetchMovies");
```

The action fetches data and commits a mutation to update the store.

<br>


### 8.3 Add error handling in a real component

Because the request can fail, the component should handle the returned Promise:

```js
methods: {
  async loadMovies() {
    try {
      await this.$store.dispatch("fetchMovies");
    } catch (error) {
      console.error(error);
    }
  }
}
```

For a real UI, display a useful error message rather than relying only on the console.

<br>


### 8.4 Commit vs dispatch

```js
this.$store.commit("setMovies", movies);
```

Runs a mutation.

```js
this.$store.dispatch("fetchMovies");
```

Runs an action.

**Rule:** Actions coordinate work. Mutations perform the state changes.

<br>


---

<br>


## 9. Step 8 — Connect Everything to a Component

This is a reusable component pattern for a store containing `movies`, a `favouriteMovies` getter, and a `fetchMovies` action.

```vue
<template>
  <div>
    <button @click="loadMovies" :disabled="loading">
      {{ loading ? "Loading..." : "Load Movies" }}
    </button>

    <p v-if="error">{{ error }}</p>

    <ul>
      <li v-for="movie in movies" :key="movie.id">
        {{ movie.title }}
      </li>
    </ul>

    <h3>Favourite Movies</h3>

    <ul>
      <li v-for="movie in favouriteMovies" :key="movie.id">
        {{ movie.title }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {
      loading: false,
      error: ""
    };
  },

  computed: {
    movies() {
      return this.$store.state.movies;
    },

    favouriteMovies() {
      return this.$store.getters.favouriteMovies;
    }
  },

  methods: {
    async loadMovies() {
      this.loading = true;
      this.error = "";

      try {
        await this.$store.dispatch("fetchMovies");
      } catch (error) {
        this.error = "Unable to load movies.";
      } finally {
        this.loading = false;
      }
    }
  }
};
</script>
```

<br>


### What is happening here?

1. The component reads movies from the store.
2. It reads favourite movies through a getter.
3. The user clicks the button.
4. The component dispatches an action.
5. The action fetches data and commits a mutation.
6. The store updates.
7. The component automatically displays the new data.

This is the general pattern you can reuse for movie lists, product lists, user data, and other shared state.

<br>


---

<br>


## 10. Step 9 — Use Mapping Helpers

When a component uses many store properties, Vuex mapping helpers reduce repetitive code.

### 10.1 Map getters

Instead of writing:

```js
computed: {
  favouriteMovies() {
    return this.$store.getters.favouriteMovies;
  }
}
```

You can write:

```js
import { mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters(["favouriteMovies"])
  }
};
```

The getter is now available as `favouriteMovies` in the component.

<br>


### 10.2 Map actions

Instead of writing a wrapper method for every action:

```js
methods: {
  loadMovies() {
    return this.$store.dispatch("fetchMovies");
  }
}
```

You can use:

```js
import { mapActions } from "vuex";

export default {
  methods: {
    ...mapActions(["fetchMovies"])
  }
};
```

Then call the mapped action from the template:

```html
<button @click="fetchMovies">Load Movies</button>
```

<br>


### 10.3 When should you use these helpers?

Use them when they make your component simpler.

They are optional. Direct access through `this.$store` is also valid.

<br>


---

<br>


## 11. Complete Execution Flow

When implementing Vuex, remember this flow:

```text
1. Component displays store data
             |
             v
2. User performs an action
             |
             v
3. Component dispatches a Vuex action
             |
             v
4. Action performs required work
   (for example, an API request)
             |
             v
5. Action commits a mutation
             |
             v
6. Mutation changes store state
             |
             v
7. Vue detects reactive changes
             |
             v
8. Components display updated data
```

For a simple synchronous change, you can commit a mutation directly without an action.

For reading data, use state, getters, or computed properties. You don't need to dispatch an action just to read a value already in the store.

<br>


---

<br>


## 12. Reusable Store Checklist

Use this checklist whenever you add Vuex to a Vue 2 project.

### Setup

- [ ] Install `vuex@3`.
- [ ] Create `src/store.js`.
- [ ] Define the required state.
- [ ] Add getters, mutations, and actions as needed.
- [ ] Register the store in `main.js`.

### Component integration

- [ ] Read shared state through computed properties.
- [ ] Use getters for reusable derived values.
- [ ] Commit mutations for synchronous state changes.
- [ ] Dispatch actions for operations such as API requests.
- [ ] Handle loading and errors for asynchronous operations.
- [ ] Use `mapGetters` and `mapActions` when they improve readability.

### Debugging

If something doesn't work, check:

- Is Vuex installed and imported?
- Is the store registered with the root Vue instance?
- Is the state or getter name correct?
- Does the mutation name passed to `commit()` exist?
- Does the action name passed to `dispatch()` exist?
- Is the action committing the correct mutation?
- Is the API request succeeding?
- Are components reading the correct state property?

<br>


---

<br>

