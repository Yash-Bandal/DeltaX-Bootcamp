# Part 3 — Components & Project Structure

**Course:** Net Ninja — Vue JS 2 Tutorial  
**Lessons:** 15–21  
**Goal:** Understand components, refs, project tooling, `.vue` files, the root component, nested components, and scoped CSS.

## Table of Contents

1. [Introduction to Components](#1-introduction-to-components)
2. [Refs](#2-refs)
3. [Vue CLI](#3-vue-cli)
4. [Vue Files and the Root Component](#4-vue-files-and-the-root-component)
5. [Nesting Components](#5-nesting-components)
6. [Component CSS — Scoped Styles](#6-component-css--scoped-styles)
7. [Nesting Components — Practical Examples](#7-nesting-components--practical-examples)
8. [Part 3 — Quick Revision](#8-part-3--quick-revision)
9. [Vue 2 to Vue 3 — Essential Mapping](#9-vue-2-to-vue-3--essential-mapping)

---

# 1. Introduction to Components

## 1.1 What Is a Component?

### 1.1.1 Definition

A **component** is a reusable, self-contained part of a user interface.

Examples include:

- Navbar
- Movie card
- Login form
- Product list
- Footer
- Search bar

Instead of building an entire application inside one large component, we divide the UI into smaller, manageable components.

### 1.1.2 Why Use Components?

Imagine building a movie application containing 50 movie cards.

Without components, we might duplicate the HTML for every movie card.

With components, we create one `MovieCard` component and reuse it for all 50 movies.

Benefits:

- Reusability
- Easier maintenance
- Better organization
- Separation of concerns
- Easier testing and debugging

### 1.1.3 Component-Based Architecture

```text
Movie Application
       |
       v
    App.vue
       |
       +------> Navbar.vue
       |
       +------> MovieList.vue
       |             |
       |             +----> MovieCard.vue
       |             +----> MovieCard.vue
       |             +----> MovieCard.vue
       |
       +------> Footer.vue
```

Each component handles a particular part of the interface.

<br>

## 1.2 Creating a Component in Vue 2

### 1.2.1 Registering a Component

In Vue 2, we can register a component globally using `Vue.component()`.

```js
Vue.component("greeting", {
    template: "<p>Hello from a component!</p>"
});

new Vue({
    el: "#app"
});
```

The component is registered before the root Vue instance is created.

### 1.2.2 Using the Component

```html
<div id="app">
    <greeting></greeting>
    <greeting></greeting>
</div>
```

Output:

```text
Hello from a component!
Hello from a component!
```

Each `<greeting>` tag represents an instance of the registered component.

### 1.2.3 Understanding the Flow

```text
Vue.component()
       |
       v
Component is registered globally
       |
       v
Vue encounters <greeting>
       |
       v
Vue renders the component template
       |
       v
The greeting appears in the browser
```

<br>

## 1.3 Component Data

####  Normal (Plain Object) — ❌ WRONG for Components
```js
Vue.component('my-component', {
    // Plain object: BAD!
    data: {
        count: 0
    },
    template: '<button @click="count++">Clicked {{ count }} times</button>'
});
```

##### What problem occurs?
- The Shared State Bug: If you render `<my-component> twice on the same page, both components share the exact same `count` variable in memory.
- Clicking button A will magically increment the counter on button B as well, because both instances point to the exact same object reference in JavaScript memory.

<br>
 
#### With Return (Function) — ✅ CORRECT
```js
Vue.component('my-component', {
    // Function returning a fresh object: GOOD!
    data: function() {
        return {
            count: 0
        };
    },
    template: '<button @click="count++">Clicked {{ count }} times</button>'
});
```

##### Why this fixes it:
- Every time a component is placed on the page, Vue runs the `data` function.
- This creates a brand new, **isolated copy** of the data object exclusively for that single component instance. Button A and Button B now operate completely independently!


<br>

Thus,\
If multiple component instances shared the same data object, changing one counter could affect the others.

A data factory avoids that shared mutable state.

Now, different Component instances, for multiple times calling a component, instead of sharing 1
```text
Counter Instance A       Counter Instance B
       |                         |
       v                         v
   count = 0                 count = 0
       |                         |
       v                         v
  Click Increase           No change
       |                         |
       v                         v
   count = 1                 count = 0
```

<br>


### 1.3.3 Vue 3 Equivalent

Vue 3 also uses a function returning an object for component `data` when using the Options API.

With Composition API, we commonly create state inside `<script setup>`:

```vue
<script setup>
import { ref } from 'vue'

const count = ref(0)
</script>

<template>
    <p>{{ count }}</p>
    <button @click="count++">Increase</button>
</template>
```

Each component instance created from this component receives its own setup state.

<br>

## 1.4 Key Takeaways

### 1.4.1 Remember

- Components divide an application into reusable UI units.
- Vue 2 supports global registration through `Vue.component()`.
- Reusable components need independent state.
- Vue 2 and Vue 3 both support component-based architecture.

<br>

---

<br>

# 2. Refs

## 2.1 What Are Refs?

### 2.1.1 Definition in the Net Ninja Vue 2 Course

In this lesson, **refs** refer to Vue's `ref` attribute, which allows us to access a particular DOM element or child component through the component instance.

This is different from the Composition API's `ref()` function in Vue 3.

The word *ref* is used for both concepts, so keep their purposes separate.

### 2.1.2 Creating a Template Ref in Vue 2

```html
<div id="app">
    <input ref="nameInput">
    <button @click="focusInput">Focus Input</button>
</div>

<script>
new Vue({
    el: "#app",

    methods: {
        focusInput() {
            this.$refs.nameInput.focus();
        }
    }
});
</script>
```

The `ref="nameInput"` attribute registers a reference to the input.

Vue 2 exposes it through `this.$refs.nameInput`.

### 2.1.3 How It Works

```text
Vue renders the input
        |
        v
ref="nameInput" registers the reference
        |
        v
User clicks the button
        |
        v
focusInput() runs
        |
        v
this.$refs.nameInput.focus()
        |
        v
Input receives focus
```

<br>

## 2.2 Accessing Different Elements

### 2.2.1 Accessing a DOM Element

```html
<p ref="message">Hello Vue</p>
```

```js
this.$refs.message
```

This gives access to the rendered paragraph element.

### 2.2.2 Accessing a Child Component

Suppose a parent renders a child component:

```html
<user-profile ref="profile"></user-profile>
```

In Vue 2, the parent can access the child component instance through:

```js
this.$refs.profile
```

This can expose the child's public instance properties and methods.

However, parent-child communication is usually better handled through props and events rather than extensive direct instance access.

### 2.2.3 Important Limitation

Refs are not reactive state.

Changing a DOM element through `$refs` does not automatically trigger Vue to update the template.

Use refs when direct access is genuinely needed, such as focusing an input, measuring an element, or integrating with a third-party library.

<br>

## 2.3 Vue 3 Template Refs vs `ref()`

### 2.3.1 Composition API State Ref

In Vue 3:

```vue
<script setup>
import { ref } from 'vue'

const count = ref(0)

function increase() {
    count.value++
}
</script>

<template>
    <p>{{ count }}</p>
    <button @click="increase">Increase</button>
</template>
```

Here, `ref(0)` creates a reactive reference to a value.

### 2.3.2 Vue 3 Template Ref

To access a DOM element:

```vue
<script setup>
import { ref, onMounted } from 'vue'

const nameInput = ref(null)

onMounted(() => {
    nameInput.value.focus()
})
</script>

<template>
    <input ref="nameInput">
</template>
```

Here, the same `ref()` API is used to hold a DOM element after Vue mounts it.

### 2.3.3 Comparison

| Concept | Vue 2 course | Vue 3 Composition API |
|---|---|---|
| Template ref declaration | `ref="nameInput"` | `ref="nameInput"` |
| Access template ref | `this.$refs.nameInput` | `nameInput.value` |
| Reactive state | Usually `data()` | `ref()` or `reactive()` |
| DOM access timing | After rendering | Typically after mounting |

<br>

## 2.4 Key Takeaways

### 2.4.1 Remember

- A template ref provides direct access to a rendered element or child component.
- In Vue 2, use `this.$refs`.
- In Vue 3 Composition API, template refs are commonly declared with `ref(null)`.
- Do not confuse a template ref with the general reactive state API `ref()`.

<br>

---

<br>

# 3. Vue CLI

## 3.1 What Is Vue CLI?

### 3.1.1 Definition

Vue CLI is a project scaffolding tool historically used to create Vue applications.

It can generate a project structure and configure development tools, dependencies, and build processes.

The Net Ninja course uses Vue CLI because it was a common Vue 2 workflow.

### 3.1.2 Why Use a Project Tool?

A real Vue application usually needs more than one HTML file.

It may contain:

- Multiple components
- CSS files
- JavaScript modules
- Third-party packages
- Build configuration
- Development and production scripts

A project tool organizes these files and prepares the application for development and deployment.

### 3.1.3 Vue CLI vs Vite

| Feature | Vue CLI | Vite |
|---|---|---|
| Role | Project scaffolding and build tooling | Development server and build tooling |
| Typical Vue 2 course setup | Common | Available, depending on project configuration |
| Modern Vue projects | Legacy workflow for new projects | Recommended default for new projects |
| Development server | Webpack-based tooling | Native-module-based dev server with optimized dependency handling |

For new Vue projects, Vite is generally preferred.

<br>

## 3.2 Creating a Project

#### [Settup](./Installation.md)

### 3.2.1 Vue CLI Command

A traditional Vue CLI project can be created using:

```bash
npm install -g @vue/cli
vue create my-project
```

The CLI prompts you to select project options.

### 3.2.2 Vite Command

For a modern Vue 3 project, use:

```bash
npm create vue@latest
```

This runs the official Vue project creation tool, which scaffolds a Vue application using Vite.

Follow the prompts and then run:

```bash
cd my-project
npm install
npm run dev
```

Use the actual project name you selected in the setup prompts.

### 3.2.3 Important Distinction

Vue CLI and Vite are development tools, not the Vue framework itself.

Vue provides the component and reactivity system. The toolchain prepares and serves the application.



<br>

---

<br>

# 4. Vue Files and the Root Component

## 4.1 What Is a `.vue` File?

### 4.1.1 Single-File Components

A Vue Single-File Component (SFC) is a `.vue` file that groups a component's template, JavaScript logic, and styles.

It commonly contains three sections:

```vue
<script>
    // JavaScript logic
</script>

<template>
    <!-- UI structure -->
</template>

<style>
    /* CSS styles */
</style>
```

### 4.1.2 Why Use `.vue` Files?

They keep the parts of a component together.

For example, `MovieCard.vue` can contain:

- The movie card's HTML-like template.
- The data and methods needed by the card.
- The card's styles.

This makes the component easier to understand and maintain.

### 4.1.3 What Processes `.vue` Files?

Browsers do not natively understand Vue's SFC format.

A tool such as Vite, with Vue's SFC compiler plugin, processes `.vue` files and transforms them into JavaScript modules the browser can execute.

<br>

## 4.2 Understanding the Root Component

### 4.2.1 Definition

The root component is the top-level component from which an application's component tree begins.

In a typical Vite project, `App.vue` is the root component.

```text
index.html
    |
    v
main.js
    |
    v
App.vue
    |
    +----> Navbar.vue
    |
    +----> MainContent.vue
    |
    +----> Footer.vue
```

### 4.2.2 The `main.js` Entry Point

A typical Vue 3 entry file looks like this:

```js
import { createApp } from 'vue'
import App from './App.vue'

createApp(App).mount('#app')
```

Explanation:

- `createApp` creates the Vue application.
- `App` imports the root component.
- `.mount('#app')` mounts the application into the matching HTML element.

### 4.2.3 The HTML Entry Point

The `index.html` file contains the mounting element and loads the application entry point through Vite.

```html
<div id="app"></div>
<script type="module" src="/src/main.js"></script>
```

Vite processes the module and its imports during development and builds the application for production.

<br>

## 4.3 Understanding `App.vue`

### 4.3.1 Example Root Component

```vue
<script setup>
import { ref } from 'vue'

const message = ref('Welcome to Vue')
</script>

<template>
    <main>
        <h1>{{ message }}</h1>
    </main>
</template>

<style scoped>
main {
    padding: 20px;
}
</style>
```

### 4.3.2 What Each Section Does

- `<script setup>` defines the component's logic and reactive state.
- `<template>` defines the rendered UI.
- `<style scoped>` defines styles scoped to this component.

### 4.3.3 Vue 2 vs Vue 3

Vue 2 also supports `.vue` Single-File Components when configured with suitable tooling.

The `.vue` file concept is not exclusive to Vue 3. The main differences are the available APIs and the typical build-tool setup.

<br>

## 4.4 Key Takeaways

### 4.4.1 Remember

- `.vue` files group component logic, template, and styles.
- `App.vue` is commonly the root component.
- `main.js` creates and mounts the application.
- `index.html` provides the mounting element.
- Vite and the Vue SFC compiler process `.vue` files.

<br>

---

<br>

# 5. Nesting Components

<br>
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/743abea3-0d1a-4340-a44f-555a9ae3d8fe" />
</div>
<br>

## 5.1 What Is Component Nesting?

### 5.1.1 Definition

Component nesting means placing one component inside another component.

The component that includes another component is its **parent**. The included component is its **child**.

### 5.1.2 Example Structure

```text
App.vue
   |
   +----> Navbar.vue
   |
   +----> MovieList.vue
   |           |
   |           +----> MovieCard.vue
   |           +----> MovieCard.vue
   |
   +----> Footer.vue
```

This forms a component tree.

### 5.1.3 Why Nest Components?

- Break large interfaces into smaller units.
- Reuse components across multiple pages or sections.
- Keep each component focused on a particular responsibility.
- Make changes easier to maintain.

<br>

## 5.2 Registering Child Components in Vue 2

### 5.2.1 Create a Child Component

Suppose we have `Navbar.vue`:

```vue
<template>
    <nav>
        <h2>My Movie App</h2>
    </nav>
</template>

<script>
export default {
    name: "Navbar"
}
</script>
```

This file defines and exports a component.

### 5.2.2 Import and Register It

In Vue 2, a parent component can import and register a child component locally.

```vue
<template>
    <div>
        <navbar></navbar>
        <h1>Home Page</h1>
    </div>
</template>

<script>
import Navbar from './Navbar.vue'

export default {
    components: {
        Navbar
    }
}
</script>
```

The `components` option makes `Navbar` available in this parent's template.

### 5.2.3 Render the Child

```html
<navbar></navbar>
```

Vue renders the imported component at that position in the parent's template.

<br>

## 5.3 Nesting Components in Vue 3

### 5.3.1 Using `<script setup>`

In Vue 3, importing a component into `<script setup>` makes it available to the template without a separate `components` registration option.

```vue
<script setup>
import Navbar from './Navbar.vue'
</script>

<template>
    <div>
        <Navbar />
        <h1>Home Page</h1>
    </div>
</template>
```

### 5.3.2 Execution Flow

```text
App.vue imports Navbar.vue
          |
          v
Navbar becomes available in the template
          |
          v
Vue encounters <Navbar />
          |
          v
Vue renders the Navbar component
          |
          v
Navbar appears inside App.vue's output
```

### 5.3.3 Component Naming

Vue supports PascalCase component names such as `<Navbar />` and kebab-case names such as `<navbar></navbar>` in templates.

PascalCase is common in SFC templates; kebab-case is frequently used in HTML-oriented templates.

<br>

## 5.4 Component Tree vs DOM Tree

### 5.4.1 Component Tree

The component tree describes the relationships between Vue components.

```text
App
 ├── Navbar
 ├── MovieList
 │    └── MovieCard
 └── Footer
```

### 5.4.2 DOM Tree

The DOM tree describes the actual rendered HTML elements.

A component can produce many DOM elements, and components can render fragments containing multiple root nodes in Vue 3.

### 5.4.3 Important Distinction

A component is a reusable UI unit, not necessarily a single HTML element.

The component tree and DOM tree are related, but they are not identical.

<br>

## 5.5 Key Takeaways

### 5.5.1 Remember

- Parent components include child components.
- Vue 2 can register imported components with the `components` option.
- Vue 3 `<script setup>` makes imported components available directly in the template.
- Components form a tree, and each component can render multiple HTML elements.

<br>

---

<br>

# 6. Component CSS — Scoped Styles

## 6.1 What Is Scoped CSS?

### 6.1.1 Definition

Scoped CSS allows styles in a Vue Single-File Component to apply to elements associated with that component rather than matching all elements globally.

```vue
<style scoped>
h2 {
    color: green;
}
</style>
```

This is useful when multiple components contain elements with the same HTML tag or CSS class.

### 6.1.2 Without Scoped Styles

Suppose two components contain `<h2>` elements.

```css
h2 {
    color: green;
}
```

A global stylesheet can affect both components' headings.

### 6.1.3 With Scoped Styles

```vue
<style scoped>
h2 {
    color: green;
}
</style>
```

Vue's SFC compiler transforms the selector and associates it with generated scope attributes.

This limits ordinary scoped selectors to the relevant component's elements.

<br>

## 6.2 How Scoped CSS Works

### 6.2.1 Conceptual Transformation

Original component:

```vue
<template>
    <h2>Hello</h2>
</template>

<style scoped>
h2 {
    color: green;
}
</style>
```

Conceptually, the compiled output resembles:

```html
<h2 data-v-example>Hello</h2>
```

```css
h2[data-v-example] {
    color: green;
}
```

The exact generated attribute is determined by the build tool; `data-v-example` is illustrative.

### 6.2.2 Important Limitation

Scoped CSS is not the same as Shadow DOM.

It uses selector transformation rather than creating a fully isolated browser style boundary.

For example, styles can still affect child component root elements in certain situations, and deep selectors can intentionally target descendants.

### 6.2.3 Global Styles

Global styles are still appropriate for:

- Body defaults
- Typography
- CSS resets
- Shared design tokens
- Reusable utility classes

Use scoped styles for component-specific appearance and global CSS for shared application-wide rules.

<br>

## 6.3 Parent and Child Component Styles

### 6.3.1 Parent Scoped CSS

A parent's scoped styles generally do not automatically target every internal element of a child component.

However, a child component's root element can be affected by certain parent scoped styles. This allows parent components to control some layout aspects of their children.

### 6.3.2 Child Scoped CSS

A child's own scoped styles govern its internal elements, subject to Vue's scoped-selector rules.

### 6.3.3 Practical Example

`Navbar.vue`:

```vue
<template>
    <nav class="navbar">
        <h2>Movie App</h2>
    </nav>
</template>

<style scoped>
.navbar {
    background: #222;
    color: white;
    padding: 12px;
}
</style>
```

The navbar styles remain associated with the component's scoped selectors.

<br>

## 6.4 Key Takeaways

### 6.4.1 Remember

- Add `scoped` to a component's `<style>` block for component-scoped styling.
- Vue compiles scoped selectors using generated scope attributes.
- Scoped CSS is not Shadow DOM.
- Use global CSS for shared rules and scoped CSS for component-specific styling.

<br>

---

<br>

# 7. Nesting Components — Practical Examples

## 7.1 Building a Simple Movie Application

### 7.1.1 Component Structure

```text
src/
├── main.js
├── App.vue
└── components/
    ├── Navbar.vue
    ├── MovieList.vue
    └── MovieCard.vue
```

This example uses Vue 3 with Vite and `<script setup>`.

### 7.1.2 Create `MovieCard.vue`

```vue
<script setup>
defineProps({
    title: String,
    year: Number
})
</script>

<template>
    <article class="movie-card">
        <h3>{{ title }}</h3>
        <p>Year: {{ year }}</p>
    </article>
</template>

<style scoped>
.movie-card {
    border: 1px solid #ccc;
    border-radius: 8px;
    padding: 12px;
    margin-bottom: 12px;
}
</style>
```

This example introduces props, which will be studied in detail in Part 4.

### 7.1.3 Create `MovieList.vue`

```vue
<script setup>
import MovieCard from './MovieCard.vue'

const movies = [
    { id: 1, title: 'Inception', year: 2010 },
    { id: 2, title: 'Interstellar', year: 2014 }
]
</script>

<template>
    <section>
        <h2>Movies</h2>

        <MovieCard
            v-for="movie in movies"
            :key="movie.id"
            :title="movie.title"
            :year="movie.year"
        />
    </section>
</template>
```

The list component imports and renders multiple instances of the movie card component.

<br>

## 7.2 Create the Root Component

### 7.2.1 `App.vue`

```vue
<script setup>
import Navbar from './components/Navbar.vue'
import MovieList from './components/MovieList.vue'
</script>

<template>
    <Navbar />
    <main>
        <MovieList />
    </main>
</template>
```

The root component imports the components it needs and arranges them into the page layout.

### 7.2.2 `Navbar.vue`

```vue
<template>
    <header class="navbar">
        <h1>Movie Library</h1>
    </header>
</template>

<style scoped>
.navbar {
    background: #222;
    color: white;
    padding: 16px;
}
</style>
```

### 7.2.3 Execution Flow

```text
main.js mounts App.vue
        |
        v
App.vue renders Navbar
        |
        v
App.vue renders MovieList
        |
        v
MovieList iterates through movies
        |
        v
MovieCard renders for each movie
        |
        v
The browser displays the movie library
```

<br>

## 7.3 What This Example Teaches

### 7.3.1 Key Concepts

- `App.vue` is the root component.
- Components are imported and nested.
- `MovieList.vue` owns the movie collection.
- `MovieCard.vue` represents one movie.
- Props pass data from the list to each card.
- Scoped CSS keeps component styling organized.
- `v-for` renders multiple card instances.

<br>

---

<br>

# 8. Part 3 — Quick Revision

## 8.1 Essential Syntax

### 8.1.1 Cheat Sheet

| Syntax | Purpose |
|---|---|
| `Vue.component()` | Globally registers a Vue 2 component |
| `data()` | Returns component data |
| `ref="nameInput"` | Declares a template ref |
| `this.$refs.nameInput` | Accesses a Vue 2 template ref |
| `ref(null)` | Creates a Vue 3 reactive ref, also used for template refs |
| `<script setup>` | Defines a component using Vue 3 Composition API syntax |
| `<template>` | Defines the component's UI |
| `<style scoped>` | Applies component-scoped CSS |
| `import Navbar from './Navbar.vue'` | Imports a component |
| `components: { Navbar }` | Registers a component in the Vue 2/Options API pattern |
| `<Navbar />` | Renders the imported component in an SFC template |
| `createApp(App).mount('#app')` | Mounts a Vue 3 application |

<br>

## 8.2 Self-Assessment

### 8.2.1 Questions

1. What problem do components solve?
2. Why must reusable component data be returned from a function in the Vue 2 Options API?
3. What is the difference between a template ref and a reactive state ref?
4. What is Vue CLI, and how does it differ from Vite?
5. What is a Single-File Component?
6. What role do `main.js`, `App.vue`, and `index.html` play?
7. What is the difference between a parent and a child component?
8. How do component imports work in `<script setup>`?
9. What does the `scoped` attribute do?
10. What is the difference between a component tree and a DOM tree?

### 8.2.2 Practical Exercise

Build a small movie library using separate Vue components.

Requirements:

- Create `Navbar.vue`.
- Create `MovieList.vue`.
- Create `MovieCard.vue`.
- Import the components into `App.vue`.
- Render several movie cards using `v-for`.
- Pass movie titles and years into each card.
- Add scoped CSS to the navbar and movie cards.
- Use a template ref to focus an input if you want an additional challenge.

**Completion criterion:** You should be able to explain how the root component renders children, how imported components are made available to templates, and how component styles are scoped.

<br>

---

<br>

# 9. Vue 2 to Vue 3 — Essential Mapping

## 9.1 Component Registration

### 9.1.1 Vue 2

```js
Vue.component('greeting', {
    template: '<p>Hello!</p>'
});
```

### 9.1.2 Vue 3

```vue
<script setup>
import Greeting from './Greeting.vue'
</script>

<template>
    <Greeting />
</template>
```

In modern Vue 3 SFCs, importing a component into `<script setup>` makes it available in the template.

<br>

## 9.2 Component State

### 9.2.1 Vue 2 Options API

```js
export default {
    data() {
        return {
            count: 0
        };
    }
}
```

### 9.2.2 Vue 3 Composition API

```vue
<script setup>
import { ref } from 'vue'

const count = ref(0)
</script>
```

Both approaches support independent component state.

<br>

## 9.3 Template Refs

### 9.3.1 Vue 2

```html
<input ref="searchInput">
```

```js
this.$refs.searchInput.focus();
```

### 9.3.2 Vue 3

```vue
<script setup>
import { ref, onMounted } from 'vue'

const searchInput = ref(null)

onMounted(() => {
    searchInput.value.focus()
})
</script>

<template>
    <input ref="searchInput">
</template>
```

In Vue 3, template refs are populated after the relevant element is rendered. Accessing the element after mounting is a common pattern.

<br>

## 9.4 Project Tooling

### 9.4.1 Vue 2 Course Workflow

The course uses Vue CLI and the Vue 2 Options API.

### 9.4.2 Modern Vue 3 Workflow

A common modern setup is:

- Vue 3
- Vite
- Single-File Components
- Composition API with `<script setup>`
- Vue Router 4 for routing
- Pinia for shared application state when needed

The concepts of components, nesting, and scoped CSS transfer directly between versions.

<br>

---

<br>


