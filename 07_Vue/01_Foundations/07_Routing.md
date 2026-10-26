# Part 7 — Routing (Vue.js)

**Lessons 39–42 · 4 lessons**

## Table of Contents

1. [Setting Up Vue Router](#1-setting-up-vue-router)
2. [Hash Mode vs History Mode](#2-hash-mode-vs-history-mode)
3. [Router Links](#3-router-links)
4. [Route Parameters](#4-route-parameters)
5. [Quick Revision](#5-quick-revision)
6. [Vue 2 vs Vue 3](#6-vue-2-vs-vue-3)

---

<br>


## 1. Setting Up Vue Router

### 1.1 What problem does routing solve?

Imagine you're building a movie application with these views:

- Home
- Movies
- Movie Details
- About

Without routing, you might manually show and hide different sections using conditions. That works for simple interfaces, but it becomes harder to manage as the application grows.

**Routing connects a URL to the view that should be displayed.**

For example:

| URL | View |
|---|---|
| `/` | Home |
| `/movies` | Movie list |
| `/about` | About page |
| `/movies/10` | Details for movie 10 |

The router checks the current URL and displays the matching component.

<br>


### 1.2 What is Vue Router?

Vue Router is the routing library for Vue applications.

It lets you:
- Navigate between views.
- Connect URLs to components.
- Read parameters from URLs.
- Support browser back and forward navigation.
- Create nested routes and navigation guards when needed.

<br>


### 1.3 How routing works

```text
User clicks "Movies"
         |
         v
URL changes to /movies
         |
         v
Vue Router matches the URL
         |
         v
Router selects the Movies component
         |
         v
Component renders inside <router-view>
```

The application can change its displayed view without requesting a completely new HTML document for every navigation.

<br>


### 1.4 Install Vue Router for a Vue 2 project

For the original Vue 2 course, use Vue Router 3.

If you're using npm in a Vue 2 project:

```bash
npm install vue-router@3
```

> Version matters: Vue Router 3 is for Vue 2. Vue Router 4 is designed for Vue 3.

<br>


### 1.5 Create components

For a simple example, imagine these components are defined in a Vue 2 application:

```js
const Home = {
  template: "<h2>Home Page</h2>"
};

const Movies = {
  template: "<h2>Movies Page</h2>"
};

const About = {
  template: "<h2>About Page</h2>"
};
```

These are small component definitions. In a larger application, each would normally live in its own component file.

<br>


### 1.6 Define the routes

```js
const routes = [
  { path: "/", component: Home },
  { path: "/movies", component: Movies },
  { path: "/about", component: About }
];

const router = new VueRouter({
  routes
});
```

Here:
- `path` defines the URL pattern.
- `component` defines what should render for that URL.
- `routes` is the list of route definitions.
- `new VueRouter()` creates the router instance.

### 1.7 Connect the router to Vue

```js
new Vue({
  el: "#app",
  router
});
```

Registering the router with the root Vue instance makes router features available to the application.

<br>


### 1.8 Display the matched component

```html
<div id="app">
  <nav>
    <router-link to="/">Home</router-link>
    <router-link to="/movies">Movies</router-link>
    <router-link to="/about">About</router-link>
  </nav>

  <router-view></router-view>
</div>
```

Two important elements:

- `<router-link>` creates navigation links.
- `<router-view>` is where the component for the current route appears.

**Remember:** Defining a route does not automatically display its component anywhere. You need a `<router-view>` outlet.

<br>

---

<br>


## 2. Hash Mode vs History Mode

### 2.1 What is hash mode?

In hash mode, the URL contains a `#` before the route.

For example:

```text
https://example.com/#/movies
https://example.com/#/about
```

The part after `#` identifies the route.

The browser does not send the fragment after `#` to the server as part of the HTTP request. This is why hash routing can work without configuring the server to handle every client-side route.

<br>

> [!NOTE]
> The `#` symbol starts a URL fragment. Browsers don't include the fragment in HTTP requests.
>
> Think of it this way:
>
> - The server delivers your Vue application.
> - Vue Router reads the URL and decides which page to show.
>
> In hash mode, the server never needs to know about `/about`. In history mode, the server receives `/about`, so it needs a fallback rule to serve `index.html` rather than returning a 404.
> 
> | Feature | Hash mode | History mode |
> |---|---|---|
> | URL | `example.com/#/about` | `example.com/about` |
> | Server receives | `/` | `/about` |
> | Server configuration for client-side routes | Usually no special fallback needed | Fallback to `index.html` needed |
> | URL appearance | Contains `#` | Clean URL |

<br>

### 2.2 Configure hash mode in Vue Router 3

Hash mode is the default in Vue Router 3, but you can specify it explicitly:

```js
const router = new VueRouter({
  mode: "hash",
  routes
});
```

When the user navigates to `/movies`, the browser URL will use the hash form, such as `/#/movies`.

<br>


### 2.3 What is history mode?

History mode uses cleaner URLs without the hash:

```text
https://example.com/movies
https://example.com/about
```

Configure it in Vue Router 3:

```js
const router = new VueRouter({
  mode: "history",
  routes
});
```

This uses the browser's History API to update the URL and manage navigation.

<br>


### 2.4 The important deployment difference

Suppose a user opens your app at `/movies` and then refreshes the browser.

With history mode, the browser sends a request for `/movies` to the server. If the server is not configured to serve the SPA's `index.html` for that route, it may return a 404.

Your deployment must support a fallback to the SPA entry point for client-side routes.

Hash mode usually avoids this particular problem because the server receives the path before the hash, often just `/`.

<br>


### 2.5 Comparison

| Feature | Hash mode | History mode |
|---|---|---|
| Example URL | `/#/movies` | `/movies` |
| URL appearance | Contains `#` | Cleaner path |
| Server fallback for client routes | Usually not required | Usually required |
| Browser navigation | Supported | Supported |
| Suitable for | Simple deployments | Production SPAs with server configuration |

<br>


### 2.6 Which one should you use?

For learning, either is fine.

For a modern production Vue application, history mode is common when the server is configured correctly. Hash mode can be useful when you cannot control server fallback configuration.

<br>

---

<br>


## 3. Router Links

### 3.1 Why not use ordinary anchor tags?

A normal HTML link looks like this:

```html
<a href="/movies">Movies</a>
```

Depending on the application and server setup, clicking it may trigger a full page navigation.

Vue Router provides `<router-link>` to navigate within the application:

```html
<router-link to="/movies">Movies</router-link>
```

It navigates using the router, updates the URL, and renders the matching view without a normal full-page navigation.

<br>


### 3.2 Example — navigation menu

```html
<div id="app">
  <nav>
    <router-link to="/">Home</router-link>
    <router-link to="/movies">Movies</router-link>
    <router-link to="/about">About</router-link>
  </nav>

  <router-view></router-view>
</div>
```

When the user clicks Movies:

1. Vue Router navigates to `/movies`.
2. It finds the route definition with that path.
3. The Movies component renders inside `<router-view>`.
4. The browser URL changes.
5. Back and forward navigation are supported.

<br>


### 3.3 Active links

Vue Router can automatically apply classes to links that match the current route.

For example:

```html
<router-link to="/movies">Movies</router-link>
```

When that route is active, Vue Router applies an active class that you can style.

```css
.router-link-active {
  font-weight: bold;
}
```

Vue Router 3 also supports the `exact` attribute for more exact active matching in relevant cases:

```html
<router-link to="/" exact>Home</router-link>
```

This helps prevent the Home link from appearing active simply because a nested URL begins with `/`.

<br>


### 3.4 Navigate programmatically

Sometimes navigation should happen after an action, such as successfully creating a movie.

Vue Router 3 provides `$router.push()`:

```js
this.$router.push("/movies");
```

For example:

```js
methods: {
  createMovie() {
    // Save the movie, then navigate.
    this.$router.push("/movies");
  }
}
```

In a real application, navigate only after the save operation succeeds.

**Difference to remember:**

- `<router-link>` is useful for navigation links in the template.
- `$router.push()` is useful when navigation is triggered by JavaScript logic.

<br>

---

<br>


## 4. Route Parameters

### 4.1 Why do we need route parameters?

Imagine your movie application contains hundreds of movies.

Creating a separate route for each movie would be impractical:

```text
/movies/1
/movies/2
/movies/3
...
```

Instead, define one dynamic route that accepts a movie ID.

```text
/movies/:id
```

The `:id` part is a route parameter.

<br>


### 4.2 Define a dynamic route

```js
const MovieDetails = {
  template: "<h2>Movie Details</h2>"
};

const routes = [
  {
    path: "/movies/:id",
    component: MovieDetails
  }
];
```

Now URLs such as these match the same route:

```text
/movies/1
/movies/25
/movies/100
```

The value of `id` changes depending on the URL.

<br>


### 4.3 Read the parameter

In Vue Router 3, a component can access the current route using `this.$route`.

For example:

```js
const MovieDetails = {
  template: "<h2>Movie ID: {{ $route.params.id }}</h2>"
};
```

If the URL is:

```text
/movies/25
```

The page displays:

```text
Movie ID: 25
```

Route parameters are generally represented as strings, so convert them if your application needs a number.

<br>


### 4.4 Pass the parameter as a prop

Passing route data as a component prop can make the component less dependent on the router.

```js
const MovieDetails = {
  props: ["id"],
  template: "<h2>Movie ID: {{ id }}</h2>"
};

const routes = [
  {
    path: "/movies/:id",
    component: MovieDetails,
    props: true
  }
];
```

The `props: true` setting passes the route parameter into the component as a prop.

This makes the component easier to reuse and test.

<br>


### 4.5 Use route parameters to fetch data

In a real movie app, the route ID could be used to request the details of a specific movie.

```text
User opens /movies/25
          |
          v
Router extracts id = "25"
          |
          v
MovieDetails receives the ID
          |
          v
Application requests movie 25 from API
          |
          v
Movie details are displayed
```

For example, the API request might be:

```js
fetch("/api/movies/25");
```

In production code, build the request using the actual route parameter and handle loading, errors, and missing records.

**Important distinction:**

- `this.$router` is used to navigate.
- `this.$route` provides information about the current route, including parameters.

<br>

---

<br>


## 5. Quick Revision

### 5.1 Key concepts

- **Vue Router:** Maps URLs to Vue components.
- **Route:** A rule that connects a URL pattern to a component.
- **`<router-link>`:** Navigates between routes.
- **`<router-view>`:** Displays the component matched by the current route.
- **Hash mode:** Uses a URL fragment, such as `/#/movies`.
- **History mode:** Uses cleaner URLs, such as `/movies`.
- **Route parameter:** A dynamic value in a path, such as `:id`.
- **`$router.push()`:** Navigates programmatically in Vue Router 3.
- **`$route.params`:** Reads route parameters.

<br>


### 5.2 Quick questions

**Q1. What is Vue Router?**

It is the official routing library used to map URLs to views in a Vue application.

**Q2. What is the difference between `<router-link>` and `<router-view>`?**

`<router-link>` provides navigation. `<router-view>` displays the matched route component.

**Q3. What is the difference between `$router` and `$route`?**

`$router` controls navigation. `$route` describes the current route.

**Q4. Why might history mode return a 404 after refresh?**

The server may not be configured to serve the SPA entry point for client-side routes.

**Q5. What does `/movies/:id` mean?**

It defines a dynamic route with a parameter named `id`.

**Q6. Does changing a route always require reloading the whole page?**

No. Vue Router can update the URL and render the appropriate component within the SPA.


<br>

---

<br>


## 6. Vue 2 vs Vue 3

### 6.1 Router versions

| Vue 2 course | Modern Vue 3 |
|---|---|
| Vue Router 3 | Vue Router 4 |
| `new VueRouter({ routes })` | `createRouter({ routes, history })` |
| `mode: "hash"` | `createWebHashHistory()` |
| `mode: "history"` | `createWebHistory()` |
| `this.$router.push(...)` | `router.push(...)` in Composition API |
| `this.$route.params.id` | `route.params.id` in Composition API |

<br>


### 6.2 Modern Vue 3 setup

In Vue 3, a router is commonly configured in a separate file.

```js
// router.js
import { createRouter, createWebHistory } from "vue-router";

import Home from "./views/Home.vue";
import Movies from "./views/Movies.vue";
import MovieDetails from "./views/MovieDetails.vue";

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

Then register it in the application:

```js
// main.js
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";

createApp(App)
  .use(router)
  .mount("#app");
```

Your root component can contain:

```vue
<template>
  <nav>
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/movies">Movies</RouterLink>
  </nav>

  <RouterView />
</template>
```

In a Vue 3 component using `<script setup>`, route parameters can be read like this:

```vue
<script setup>
import { useRoute } from "vue-router";

const route = useRoute();
</script>

<template>
  <h2>Movie ID: {{ route.params.id }}</h2>
</template>
```


<br>

---

<br>

