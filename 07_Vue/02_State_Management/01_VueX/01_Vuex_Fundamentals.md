# Vuex — Complete Learning Notes

**Based on the Net Ninja Vuex Tutorial · Lessons 1–8**

## Table of Contents

1. [What Is Vuex?](#1-what-is-vuex)
2. [A Simple Vue App](#2-a-simple-vue-app)
3. [Setting Up a Central Store](#3-setting-up-a-central-store)
4. [Using Computed Properties](#4-using-computed-properties)
5. [Getters](#5-getters)
6. [Mutations](#6-mutations)
7. [Actions](#7-actions)
8. [Mapping Actions and Getters](#8-mapping-actions-and-getters)
9. [Complete Vuex Data Flow](#9-complete-vuex-data-flow)
10. [Vuex vs Pinia — A Preview](#10-vuex-vs-pinia)
11. [Quick Revision and Interview Questions](#11-quick-revision-and-interview-questions)


<br>



---


## 1. What Is Vuex?

### 1.1 The problem: Sharing data between components

Imagine you're building a movie application.

You have these components:

- `MovieList` — displays movies.
- `FavouriteMovies` — displays favourite movies.
- `Navbar` — displays the number of favourites.
- `MovieDetails` — lets the user add a movie to favourites.

All these components might need access to the same favourites data.

Without a central store, you may need to pass data through props and events across multiple component levels. This can become difficult to maintain as the application grows.

This problem is commonly called **state management**.

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/a5ac3487-b873-48b4-8eb7-6d5a81d14cc9" />
</div>
<br>


### 1.2 What is state?

State is the data that an application needs to remember while it runs.

For example:

```js
{
  movies: [],
  favourites: [],
  loggedInUser: null,
  isLoading: false
}
```

In a Vue application, state might contain a user's information, selected products, a shopping cart, or favourite movies.

<br>


### 1.3 What is Vuex?

Vuex is a state management library for Vue applications.

It provides a central store where shared application state can live. Components can read the state and request changes through defined mechanisms.

Instead of each component maintaining a separate copy of shared data, components can use the same central state.

<br>


### 1.4 How does it help?

Without a central store:

```text
Navbar ← props/events → MovieList
                         ↕
                    MovieDetails
                         ↕
                   FavouriteMovies
```

As the number of components grows, communication can become complicated.

With Vuex:

```text
       Navbar
          |
      MovieList
          |
     MovieDetails
          |
    FavouriteMovies
          |
          v
    ┌──────────────┐
    │ Vuex Store   │
    │              │
    │ favourites   │
    │ movies       │
    └──────────────┘
```

Each component can access the shared store rather than manually passing the same data through every intermediate component.

<br>


### 1.5 When do we need Vuex?

Vuex is useful when:
- Multiple components need the same data.
- Several parts of the app can update that data.
- You want a predictable way to manage changes.
- Passing props and events through many levels becomes cumbersome.

You don't need Vuex for every component. A small form with a local input value can simply use component state.

**Remember:** Vuex is not a replacement for Vue. It is a library that helps Vue applications manage shared state.

<br>

---

<br>


## 2. A Simple Vue App

### 2.1 Start without Vuex

Let's build a simple counter using Vue 2.

```html
<div id="app">
  <h2>Counter: {{ count }}</h2>

  <button @click="increment">Increase</button>
</div>
```

```js
new Vue({
  el: "#app",

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

This works perfectly well.

The `count` value belongs to this Vue instance, and Vue updates the displayed number whenever it changes.

### 2.2 Why isn't this enough for every application?

Imagine that the counter is needed by three different components.

If each component has its own `count`, they will have separate values:

```text
Navbar count:       0
Counter component:  0
Dashboard count:    0
```

If the Counter component increments its own value, the other two components won't automatically share that new value.

We could lift the state to a common parent and pass it through props. But for deeply nested or widely shared state, a central store can be simpler.

### 2.3 Local state vs shared state

| Local component state | Shared store state |
|---|---|
| Belongs to one component | Shared across components |
| Good for local input values | Good for shared favourites or cart data |
| Usually managed with `data()` in Vue 2 | Managed through Vuex |
| Components access their own state | Components access the central store |

**Key idea:** Use local state when the data is local. Use a central store when several parts of the application need to share it.

<br>

---

<br>


## 3. Setting Up a Central Store

### 3.1 Install Vuex for Vue 2

For the Vue 2 course, use Vuex 3.

```bash
npm install vuex@3
```

Vuex 4 is designed for Vue 3, while Vuex 3 is designed for Vue 2.

### 3.2 Create the store

In a typical Vue CLI project, create a file such as `src/store.js`.

```js
import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    count: 0
  }
});
```

Let's understand each part.

- `import Vuex from "vuex"` imports Vuex.
- `Vue.use(Vuex)` installs Vuex as a Vue 2 plugin.
- `new Vuex.Store(...)` creates the central store.
- `state` contains the shared data.

At this point, the store contains `count`, but we haven't connected it to our Vue application yet.

<br>


### 3.3 Register the store

In `src/main.js`:

```js
import Vue from "vue";
import App from "./App.vue";
import store from "./store";

new Vue({
  store,
  render: h => h(App)
}).$mount("#app");
```

The `store` option injects the store into the Vue component tree.

Components can then access it using `this.$store`.

<br>


### 3.4 Read store state in a component

In `App.vue`:

```vue
<template>
  <div>
    <h2>Counter: {{ $store.state.count }}</h2>
  </div>
</template>
```

The template reads `count` directly from the store.

To increase the count, we'll add a mutation in a later section.

### 3.5 Understand the connection

```text
store.js
   |
   | Creates Vuex store
   v
main.js
   |
   | Registers store with root Vue instance
   v
App.vue
   |
   | Reads this.$store.state.count
   v
Template displays shared state
```

The store is now available to the application's components through Vue's component tree.

**Important:** In a Vuex application, components should generally request state changes through mutations, directly or through actions, rather than modifying shared state arbitrarily.

<br>

---

## 4. Using Computed Properties

### 4.1 Why use a computed property with Vuex?

You can read state directly:

```html
<p>{{ $store.state.count }}</p>
```

But imagine you need to display the count in several places or calculate something from it.

A computed property can provide a convenient reactive value derived from the store.

### 4.2 Example

```vue
<template>
  <div>
    <h2>Counter: {{ count }}</h2>
  </div>
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

Here, the computed property reads the shared state from Vuex.

When the store's `count` changes, the computed property updates and Vue refreshes the UI.

### 4.3 How does the flow work?

```text
Vuex state.count = 0
        |
        v
Computed property reads count
        |
        v
Template displays 0
        |
        v
Store count changes to 1
        |
        v
Computed property updates
        |
        v
Template displays 1
```

### 4.4 Why not just copy the state into `data()`?

Consider this approach:

```js
data() {
  return {
    count: this.$store.state.count
  };
}
```

This copies the current value into the component's local state. It does not create the same reactive connection as reading the store through a computed property.

For reading shared store state, use a computed property or a Vuex helper such as `mapState`.

**Remember:** Computed properties are useful for exposing reactive store values to a component's template.

<br>

---

## 5. Getters

### 5.1 What is a getter?

A Vuex getter is a function defined in the store that derives a value from the store's state.

Think of it as a way to share commonly needed calculations across components.

Suppose your store has a list of movies:

```js
state: {
  movies: [
    { id: 1, title: "Inception", favourite: true },
    { id: 2, title: "Interstellar", favourite: false },
    { id: 3, title: "The Dark Knight", favourite: true }
  ]
}
```

Several components may need to display only favourite movies.

Without a getter, each component might repeat the same filtering logic.

### 5.2 Create a getter

Add a `getters` section to the store:

```js
export default new Vuex.Store({
  state: {
    movies: [
      { id: 1, title: "Inception", favourite: true },
      { id: 2, title: "Interstellar", favourite: false },
      { id: 3, title: "The Dark Knight", favourite: true }
    ]
  },

  getters: {
    favouriteMovies(state) {
      return state.movies.filter(movie => movie.favourite);
    }
  }
});
```

The getter receives the store's state and returns the filtered list.

### 5.3 Use the getter in a component

```vue
<template>
  <div>
    <h2>Favourite Movies</h2>

    <ul>
      <li v-for="movie in favouriteMovies" :key="movie.id">
        {{ movie.title }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  computed: {
    favouriteMovies() {
      return this.$store.getters.favouriteMovies;
    }
  }
};
</script>
```

The component uses the getter rather than repeating the filtering logic.

### 5.4 How does a getter work?

```text
Store state.movies
        |
        v
Getter filters favourite movies
        |
        v
Returns derived array
        |
        v
Component reads getter
        |
        v
Template displays matching movies
```

### 5.5 Getters vs computed properties

These two concepts are related, but they serve different scopes.

| Vuex getter | Component computed property |
|---|---|
| Defined in the central store | Defined in a component |
| Can be shared across components | Usually belongs to one component |
| Derives values from store state | Derives values from any reactive dependencies |
| Accessed through `$store.getters` | Accessed by its name in the component |

A getter is useful when multiple components need the same derived store value. A component computed property is useful for calculations specific to that component.

**Remember:** Getters derive values. They should not be used to modify store state.

<br>

---

## 6. Mutations

### 6.1 What is a mutation?

A mutation is a named function in the Vuex store that changes the store's state.

For example, we might need to increase a shared counter.

Instead of changing the store state directly from a component, we define a mutation.

### 6.2 Create a mutation

```js
export default new Vuex.Store({
  state: {
    count: 0
  },

  mutations: {
    increment(state) {
      state.count++;
    }
  }
});
```

The mutation receives the current state as its first argument.

### 6.3 Commit the mutation

From a Vue 2 component:

```js
this.$store.commit("increment");
```

The word `commit` means we're asking Vuex to run a mutation.

A component template could look like this:

```vue
<template>
  <div>
    <h2>Count: {{ count }}</h2>
    <button @click="increment">Increase</button>
  </div>
</template>

<script>
export default {
  computed: {
    count() {
      return this.$store.state.count;
    }
  },

  methods: {
    increment() {
      this.$store.commit("increment");
    }
  }
};
</script>
```

### 6.4 Passing a payload to a mutation

Sometimes a mutation needs additional information.

For example, instead of increasing the counter by one, we might want to increase it by a specified amount.

```js
mutations: {
  incrementBy(state, amount) {
    state.count += amount;
  }
}
```

Call it with:

```js
this.$store.commit("incrementBy", 5);
```

Here:
- `"incrementBy"` is the mutation name.
- `5` is the payload.

The mutation receives `5` as its second argument.

### 6.5 Why use mutations?

Mutations establish a clear place where state changes happen.

```text
Component requests change
          |
          v
commit("increment")
          |
          v
Vuex runs the mutation
          |
          v
Store state changes
          |
          v
Reactive components update
```

Vuex mutations are intended to be **synchronous**. Keep asynchronous work, such as an API request, in an action.

**Remember:** A component commits a mutation. The mutation changes the state.

<br>

---

## 7. Actions

### 7.1 Why do we need actions if mutations already exist?

Mutations change state, but they are meant to be synchronous.

Real applications often need asynchronous work before updating state.

For example:
- Fetching movies from an API.
- Logging a user in.
- Saving a new movie to a backend.
- Waiting for a request to finish.

Vuex actions are designed to handle this kind of work.

### 7.2 Create an action

Suppose our store needs to load movies from an API.

```js
export default new Vuex.Store({
  state: {
    movies: [],
    loading: false,
    error: ""
  },

  mutations: {
    setLoading(state, value) {
      state.loading = value;
    },

    setMovies(state, movies) {
      state.movies = movies;
    },

    setError(state, message) {
      state.error = message;
    }
  },

  actions: {
    async fetchMovies({ commit }) {
      commit("setLoading", true);
      commit("setError", "");

      try {
        const response = await fetch(
          "https://jsonplaceholder.typicode.com/posts"
        );

        if (!response.ok) {
          throw new Error("Unable to fetch movies");
        }

        const posts = await response.json();

        const movies = posts.slice(0, 10).map(post => ({
          id: post.id,
          title: post.title
        }));

        commit("setMovies", movies);
      } catch (error) {
        commit("setError", error.message);
      } finally {
        commit("setLoading", false);
      }
    }
  }
});
```

The endpoint returns sample posts, not actual movie data. We map the post titles into movie-like objects for this example.

### 7.3 What does `{ commit }` mean?

```js
async fetchMovies({ commit }) {
  // ...
}
```

Vuex provides an action context object containing properties such as `commit`, `dispatch`, `state`, and `getters`.

Here, JavaScript destructuring extracts the `commit` function from that context.

The action can then commit mutations to update the store.

### 7.4 Dispatch the action from a component

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
  </div>
</template>

<script>
export default {
  computed: {
    movies() {
      return this.$store.state.movies;
    },

    loading() {
      return this.$store.state.loading;
    },

    error() {
      return this.$store.state.error;
    }
  },

  methods: {
    loadMovies() {
      this.$store.dispatch("fetchMovies");
    }
  }
};
</script>
```

Notice the difference:

```js
this.$store.commit("setLoading", true);
```

commits a mutation.

```js
this.$store.dispatch("fetchMovies");
```

dispatches an action.

### 7.5 Understand the complete flow

```text
User clicks "Load Movies"
           |
           v
Component dispatches fetchMovies
           |
           v
Action commits setLoading(true)
           |
           v
Action sends API request
           |
           v
API returns data
           |
           v
Action commits setMovies(movies)
           |
           v
Store state updates
           |
           v
Component's computed properties update
           |
           v
Movie list renders
```

If the request fails, the action commits an error message instead.

### 7.6 Mutation vs action

| Mutation | Action |
|---|---|
| Changes store state | Coordinates operations and business logic |
| Intended to be synchronous | Can be asynchronous |
| Triggered using `commit()` | Triggered using `dispatch()` |
| Receives state and optional payload | Receives action context and optional payload |

**A useful rule:** Actions can commit mutations, and mutations perform the actual state changes.

<br>

---

## 8. Mapping Actions and Getters

### 8.1 Why map Vuex helpers?

In a larger component, you might repeatedly write:

```js
this.$store.getters.favouriteMovies
this.$store.dispatch("fetchMovies")
```

Vuex provides helpers that make it easier to connect store functionality to a component.

Two helpers from this lesson are:
- `mapGetters`
- `mapActions`

### 8.2 Using `mapGetters`

Suppose the store contains this getter:

```js
getters: {
  favouriteMovies(state) {
    return state.movies.filter(movie => movie.favourite);
  }
}
```

You can map it into the component's computed properties:

```vue
<script>
import { mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters(["favouriteMovies"])
  }
};
</script>
```

Now the template can use the getter directly:

```html
<li v-for="movie in favouriteMovies" :key="movie.id">
  {{ movie.title }}
</li>
```

The spread syntax `...` adds the mapped computed properties to the component's `computed` options.

### 8.3 Using `mapActions`

Suppose the store contains an action named `fetchMovies`.

```vue
<script>
import { mapActions } from "vuex";

export default {
  methods: {
    ...mapActions(["fetchMovies"])
  }
};
</script>
```

Now you can call:

```html
<button @click="fetchMovies">Load Movies</button>
```

The mapped method dispatches the Vuex action with the same name.

### 8.4 Mapping with aliases

You can use a local name different from the store getter or action name.

```js
computed: {
  ...mapGetters({
    favourites: "favouriteMovies"
  })
}
```

Now the component uses `favourites`, while the store getter remains named `favouriteMovies`.

For an action:

```js
methods: {
  ...mapActions({
    load: "fetchMovies"
  })
}
```

Now calling `load()` dispatches `fetchMovies`.

### 8.5 When should you use mapping helpers?

Use them when they make component code clearer.

They are optional. You can always access getters and dispatch actions through `this.$store`.

**Remember:**
- `mapGetters` maps store getters to computed properties.
- `mapActions` maps store actions to component methods.

<br>

---

## 9. Complete Vuex Data Flow

### 9.1 The standard flow

Vuex is designed to make shared state changes predictable.

```text
          Component
              |
              | dispatch(action)
              v
            Action
              |
              | commit(mutation)
              v
           Mutation
              |
              | changes state
              v
          Vuex State
              |
              | reactive update
              v
      Components re-render
```

### 9.2 Do all state reads need actions?

No.

A component can read state using a computed property or a getter.

Actions are useful when you need to coordinate work, especially asynchronous work. A simple synchronous state change can be handled by committing a mutation directly.

### 9.3 Do getters change the state?

No. Getters derive values from the state. Mutations change the state.

### 9.4 Can actions change state directly?

The recommended Vuex pattern is for actions to commit mutations rather than modify state directly. That keeps state changes traceable and predictable.

### 9.5 A movie-favourites example

Imagine the user clicks a star icon beside a movie.

```text
User clicks star
       |
       v
Component dispatches toggleFavourite
       |
       v
Action commits TOGGLE_FAVOURITE
       |
       v
Mutation updates movie.favourite
       |
       v
Getter returns favourite movies
       |
       v
Favourite list and navbar update
```

The benefit is that every component uses the same shared state, so the favourite status remains consistent across the application.

<br>

---

## 10. Vuex vs Pinia — A Preview

We'll study Pinia separately later. For now, understand the main conceptual differences.

| Concept | Vuex 3 (Vue 2) | Pinia (modern Vue) |
|---|---|---|
| Shared state | `state` | State returned from a store |
| Derived values | `getters` | `getters` |
| Synchronous state changes | `mutations` | Usually direct state updates or actions |
| Asynchronous operations | `actions` | `actions` |
| Access from component | `this.$store` | Call a store function such as `useMovieStore()` |
| Boilerplate | Often more structured | Generally simpler |

The important conceptual connection is that both libraries help organize shared state.

**Do not mix the APIs:** Vuex mutations are a central part of the Vuex pattern. Pinia does not use Vuex-style mutations.

<br>

---

## 11. Quick Revision

### 11.1 Key concepts

- **State:** Shared data managed by the store.
- **Store:** The central container for shared state and related logic.
- **Getter:** Derives a value from store state.
- **Mutation:** Performs a synchronous state change.
- **Action:** Coordinates logic and can perform asynchronous work.
- **Commit:** Executes a mutation.
- **Dispatch:** Triggers an action.
- **`mapGetters`:** Maps getters to component computed properties.
- **`mapActions`:** Maps actions to component methods.

### 11.2 Quick questions

**Q1. Why do we need Vuex?**

To manage shared application state in a central place and make state changes more predictable.

**Q2. What is the difference between state and getters?**

State stores the data. Getters derive values from that data.

**Q3. What is the difference between mutations and actions?**

Mutations change state synchronously. Actions coordinate operations and can handle asynchronous work.

**Q4. What is the difference between `commit()` and `dispatch()`?**

`commit()` triggers a mutation. `dispatch()` triggers an action.

**Q5. Can a component read Vuex state directly?**

Yes. It can access `$store.state`, although computed properties and mapping helpers are often cleaner.

**Q6. Why use `mapGetters`?**

It maps Vuex getters to component computed properties and reduces repetitive code.

**Q7. Why use `mapActions`?**

It maps Vuex actions to component methods so they can be called more conveniently.

**Q8. Do all components need Vuex?**

No. Local component state is enough for data used only within a component.

