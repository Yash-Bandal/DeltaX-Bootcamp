# Part 2 — Reactivity & Rendering

**Course:** Net Ninja — Vue JS 2 Tutorial  
**Lessons:** 9–14  
**Goal:** Understand computed properties, dynamic CSS classes, conditional rendering, list rendering, and how Vue manages multiple instances.

## Table of Contents

1. [Computed Properties](#1-computed-properties)
2. [Dynamic CSS Classes](#2-dynamic-css-classes)
3. [Conditionals](#3-conditionals)
4. [Looping with v-for](#4-looping-with-v-for)
5. [Simple Punchbag Game](#5-simple-punchbag-game)
6. [Multiple Vue Instances](#6-multiple-vue-instances)
7. [Part 2 — Quick Revision](#7-part-2--quick-revision)
8. [Vue 2 to Vue 3 — Essential Mapping](#8-vue-2-to-vue-3--essential-mapping)

---

<br>

# 1. Computed Properties

## 1.1 What Are Computed Properties?

<br>

> [!tip]
> If you know react, its similar to `useMemo` hook which serves the  same primary purpose: memoization and caching.
> 
> Just that
>  - Vue `computed` (**Automatic**): Vue is smart
>  - React `useMemo` (**Manual**): React doesn't track dependencies automatically

<br>


### 1.1.1 Definition

A **computed property** is a value that Vue calculates from other reactive data.

Computed properties are useful when a value depends on existing data and should update automatically when its dependencies change.

 - **With a Method** :
     -  Executes on any component state change.
     -  Whenever any state on the page changes (even an unrelated click or counter), the method re-runs. If that method contains code, logs, or internal tracking (like our methodRuns++ counter), that code executes and changes state on every single re-render.
 - **With a Computed Property**:
     -  Executes only on specific dependency change.
     -  If an unrelated state changes, the computed property does not re-run. It stays frozen in its cached memory, so its internal code doesn't execute and nothing inside it changes—it only updates when its actual dependency changes.


Example: Suppose an application stores a first name and a last name.

```js
data: {
    firstName: "Yash",
    lastName: "Bandal"
}
```

We can calculate the full name from these two values.

```js
computed: {
    fullName() {
        return this.firstName + " " + this.lastName;
    }
}
```

Use the computed property in the template:

```html
<p>{{ fullName }}</p>
```

Output:

```text
Yash Bandal
```

### 1.1.2 Why Use Computed Properties?

Consider calculating a total price from a quantity and unit price.

```js
data: {
    quantity: 3,
    price: 100
}
```

The total is derived from existing data:

```js
computed: {
    totalPrice() {
        return this.quantity * this.price;
    }
}
```

If `quantity` changes to `5`, Vue recalculates the total automatically.

The resulting total becomes `500`.

### 1.1.3 Computed Properties Are Cached

Computed properties cache their results based on reactive dependencies.

Vue recalculates a computed property when a reactive dependency it uses changes. If the dependencies haven't changed, Vue can reuse the previous result.

This is useful when a calculation is expensive or used multiple times in a template.

<br>

## 1.2 Computed Properties vs Methods

### 1.2.1 Using a Method

```js
methods: {
    fullName() {
        return this.firstName + " " + this.lastName;
    }
}
```

In the template, call it:

```html
<p>{{ fullName() }}</p>
```

### 1.2.2 Using a Computed Property

```js
computed: {
    fullName() {
        return this.firstName + " " + this.lastName;
    }
}
```

In the template, access it like a property:

```html
<p>{{ fullName }}</p>
```

### 1.2.3 Key Differences

| Feature | Computed property | Method |
|---|---|---|
| Template usage | `{{ fullName }}` | `{{ fullName() }}` |
| Purpose | Derive a value | Perform an operation or calculate a result |
| Caching | Cached based on reactive dependencies | Runs whenever invoked |
| Recalculation | When a dependency changes | Whenever the method is called |
| Best use | Derived state | Actions or calculations that should run on invocation |

**Rule of thumb:** Use a computed property for a value derived from reactive data. Use a method for an action or logic that you want to invoke.

<br>

## 1.3 Complete Example

### 1.3.1 Vue 2 Implementation

```html
<div id="app">
    <p>Quantity: {{ quantity }}</p>
    <p>Price per item: ₹{{ price }}</p>
    <p>Total: ₹{{ totalPrice }}</p>

    <button @click="quantity++">
        Add One Item
    </button>
</div>

<script>
new Vue({
    el: "#app",

    data: {
        quantity: 2,
        price: 100
    },

    computed: {
        totalPrice() {
            return this.quantity * this.price;
        }
    }
});
</script>
```



### 1.3.2 Important Rule

A computed getter should generally derive and return a value rather than modify the state it depends on.

Avoid changing `quantity` or `price` inside `totalPrice()`.

<br>

## 1.4 Key Takeaways

### 1.4.1 Remember

- Computed properties derive values from reactive data.
- Vue caches computed results until their reactive dependencies change.
- Access a computed property like a property, not a method call.
- Use computed properties for derived state and methods for actions.

<br>

---

<br>

# 2. Dynamic CSS Classes

## 2.1 What Is Dynamic CSS Binding?

### 2.1.1 Definition

Dynamic CSS binding means changing an element's CSS classes based on application data.

For example, a button might be green when a task is completed and red when it is incomplete.

Vue supports dynamic class binding through `v-bind:class`, commonly written as `:class`.

### 2.1.2 Basic Class Binding

```html
<div :class="statusClass">
    Task status
</div>
```

```js
data: {
    statusClass: "completed"
}
```

```css
.completed {
    color: green;
}
```

Vue applies the `completed` class when rendering the element.

### 2.1.3 Why Use Dynamic Classes?

Dynamic classes are useful for:

- Showing active navigation items.
- Displaying success or error states.
- Changing button appearance.
- Highlighting selected items.
- Indicating loading, disabled, or completed states.

<br>

## 2.2 Binding Classes with an Object

### 2.2.1 Object Syntax

Vue allows an object to determine which classes should be applied.

```html
<div :class="{ active: isActive }">
    Menu item
</div>
```

```js
data: {
    isActive: true
}
```

Here, the `active` class is applied because `isActive` is `true`.

### 2.2.2 Multiple Conditional Classes

```html
<div :class="{
    active: isActive,
    disabled: isDisabled
}">
    Menu item
</div>
```

Each class is controlled independently.

| Data value | Result |
|---|---|
| `isActive: true` | Adds `active` |
| `isDisabled: true` | Adds `disabled` |
| Both `true` | Adds both classes |
| Both `false` | Adds neither class |

### 2.2.3 Binding a Class Array

```html
<div :class="[baseClass, statusClass]">
    Task status
</div>
```

```js
data: {
    baseClass: "task",
    statusClass: "completed"
}
```

The element receives both classes.

<br>

### 2.2.4 Example, Steps to use Dynamic CSS class

#### **Step 1: Define the CSS class**

*  Create a standard CSS class in your stylesheet or style tag for the color you want to apply.

   ```css
   .red-text {
       color: red;
       font-weight: bold;
   }
   ```

#### **Step 2: Set up the reactive data property**

*  Define a boolean property inside your Vue instance's `data` to track whether the text should be red.

   ```javascript
   new Vue({
       el: '#vue-app',
       data: {
           isRed: false // Controls whether the class is active
       }
   });
   ```



#### **Step 3: Bind the dynamic class in the template**

*  Use the `:class` directive with object syntax (`{ 'class-name': booleanExpression }`) on your HTML element.

   ```html
   <div id="vue-app">
       <!-- The class 'red-text' is applied only when 'isRed' is true -->
       <p :class="{ 'red-text': isRed }">
           This text changes color dynamically!
       </p>
   </div>
   
   ```



#### **Step 4: Add a trigger to toggle the state**

*  Add a button with a click handler to flip the boolean value back and forth, updating the text color instantly.

   ```html
   <div id="vue-app">
       <p :class="{ 'red-text': isRed }">
           This text changes color dynamically!
       </p>
   
       <!-- Flips isRed between true and false on each click -->
       <button @click="isRed = !isRed">Toggle Text Color</button>
   </div>
   ```

<br>

---

<br>

# 3. Conditionals

## 3.1 Conditional Rendering

### 3.1.1 Definition

Conditional rendering means displaying or hiding elements based on a condition.

Vue provides directives such as `v-if`, `v-else-if`, `v-else`, and `v-show`.

### 3.1.2 The `v-if` Directive

```html
<p v-if="isLoggedIn">
    Welcome back!
</p>
```

The paragraph is rendered when `isLoggedIn` is truthy.

```js
data: {
    isLoggedIn: true
}
```

When the condition becomes false, Vue removes the element from the rendered DOM.

### 3.1.3 The `v-else` Directive

```html
<p v-if="isLoggedIn">
    Welcome back!
</p>

<p v-else>
    Please log in.
</p>
```

`v-else` must immediately follow a compatible `v-if` or `v-else-if` branch in the template.

Only the matching branch is rendered.

<br>

## 3.2 Conditional Branches

### 3.2.1 Using `v-else-if`

```html
<p v-if="score >= 80">
    Excellent
</p>

<p v-else-if="score >= 50">
    Passed
</p>

<p v-else>
    Failed
</p>
```

Vue checks the conditions in order.

The first true condition determines which branch is rendered.

### 3.2.2 Using `v-show`

```html
<p v-show="isVisible">
    Toggle my visibility
</p>
```

Unlike `v-if`, `v-show` keeps the element in the DOM and toggles its CSS `display` property.

### 3.2.3 `v-if` vs `v-show`

| Feature | `v-if` | `v-show` |
|---|---|---|
| False condition | Element removed from DOM | Element remains in DOM but is hidden |
| Initial rendering | Conditional | Element is rendered and its display is toggled |
| Toggle behavior | Creates/destroys rendered content | Changes CSS display |
| Best suited for | Conditions that change less frequently or content that should not be rendered while false | Frequent visibility toggles |


<br>

--- 

####  `v-show`
Toggle show
```html
<p style="">Error Occured</p>
```

Toggle Hide (Element present in DOM, just hidden with CSS Display)
```html
<p style="display: none;">Error Occured</p>
```

<br>

#### `v-if`
Toggle show
```html
<p>Success</p>
```

Toggle Hide (element actually removed from DOM)
```html
<!-- <p>Success</p> -->
```

---

<br>

## 3.3 Grouping Conditional Elements

### 3.3.1 Using `<template>`

Sometimes multiple elements need to share one condition.

```html
<template v-if="isLoggedIn">
    <h2>Welcome</h2>
    <p>You are logged in.</p>
    <button @click="logout">Log out</button>
</template>
```

The `<template>` wrapper itself does not create a DOM element.

### 3.3.2 Example: Show or Hide a Message

#### **Step 1: Set up the reactive data property**

*  Define a boolean property inside your Vue instance's `data` to track which element should be shown.

    ```javascript
    new Vue({
        el: '#vue-app',
        data: {
            isLoggedIn: false // Controls which conditional block appears
        }
    });
    ```



#### **Step 2: Apply `v-if` and `v-else` in the template**

*  Use the `v-if` directive to render an element conditionally, and `v-else` as an alternative fallback block right below it.

   ```html
   <div id="vue-app">
       <!-- Renders only when isLoggedIn is true -->
       <p v-if="isLoggedIn" style="color: green;">
           Welcome back, user!
       </p>
   
       <!-- Renders only when isLoggedIn is false -->
       <p v-else style="color: red;">
           Please log in to continue.
       </p>
   </div>
   ```


#### **Step 3: Add a trigger to change the conditional state**

*  Add a button with a click handler to flip the boolean value, causing Vue to add or remove elements from the DOM dynamically.

   ```html
   <div id="vue-app">
       <p v-if="isLoggedIn" style="color: green;">Welcome back, user!</p>
       <p v-else style="color: red;">Please log in to continue.</p>
   
       <!-- Flips isLoggedIn between true and false on each click -->
       <button @click="isLoggedIn = !isLoggedIn">
           Toggle Login State
       </button>
   </div>
   ```

<br>

## 3.4 Key Takeaways

### 3.4.1 Remember

- `v-if` conditionally creates or removes rendered elements.
- `v-else-if` adds additional branches.
- `v-else` handles the remaining case.
- `v-show` toggles visibility without removing the element.
- Use `<template v-if>` to group multiple elements without an extra DOM wrapper.

<br>

---

<br>

# 4. Looping with `v-for`

## 4.1 What Is `v-for`?

### 4.1.1 Definition

`v-for` is a Vue directive used to render a list by iterating over an array or object.

It is similar in purpose to JavaScript's `.map()` when rendering lists in React.

### 4.1.2 Rendering an Array

```html
<ul>
    <li v-for="name in names">
        {{ name }}
    </li>
</ul>
```

```js
data: {
    names: ["Alex", "Sam", "Jordan"]
}
```

Output:

- Alex
- Sam
- Jordan

### 4.1.3 How It Works

Vue iterates over the array and renders an `<li>` for each item.

```text
names array
    |
    +--> "Alex"   --> first <li>
    |
    +--> "Sam"    --> second <li>
    |
    +--> "Jordan" --> third <li>
```

<br>

## 4.2 Accessing the Index

### 4.2.1 Array Index

```html
<li v-for="(name, index) in names">
    {{ index }} - {{ name }}
</li>
```

The first argument is the current item; the second is its index.

### 4.2.2 Example Output

```text
0 - Alex
1 - Sam
2 - Jordan
```

Array indexes start at `0`.

### 4.2.3 Using a Range

Vue can also render a simple numeric range.

```html
<span v-for="n in 5">
    {{ n }}
</span>
```

This renders the numbers `1` through `5`.

<br>

## 4.3 Rendering Objects

### 4.3.1 Object Properties

```html
<ul>
    <li v-for="(value, key) in user">
        {{ key }}: {{ value }}
    </li>
</ul>
```

```js
data: {
    user: {
        name: "Alex",
        age: 22,
        city: "Pune"
    }
}
```

Vue iterates over the object's property values and keys.

### 4.3.2 Objects Inside an Array

```js
data: {
    movies: [
        { id: 1, title: "Movie A" },
        { id: 2, title: "Movie B" }
    ]
}
```

```html
<div v-for="movie in movies" :key="movie.id">
    {{ movie.title }}
</div>
```

This is a common pattern in real applications.

### 4.3.3 Why Use `:key`?

A `key` provides a stable identity for each rendered item.

```html
<li v-for="movie in movies" :key="movie.id">
    {{ movie.title }}
</li>
```

Keys help Vue identify which items have changed, moved, or been removed.

Prefer stable unique IDs rather than array indexes when the list can be reordered or modified.

<br>

## 4.4 Complete Example

### 4.4.1 Vue 2 Implementation

```html
<div id="app">
    <h2>My Movies</h2>

    <ul>
        <li
            v-for="movie in movies"
            :key="movie.id"
        >
            {{ movie.title }}
        </li>
    </ul>

    <button @click="addMovie">
        Add Movie
    </button>
</div>

<script>
new Vue({
    el: "#app",

    data: {
        movies: [
            { id: 1, title: "Inception" },
            { id: 2, title: "Interstellar" }
        ],
        nextId: 3
    },

    methods: {
        addMovie() {
            this.movies.push({
                id: this.nextId++,
                title: "New Movie"
            });
        }
    }
});
</script>
```



<br>



<br>

---


<br>

# 5. Simple Punchbag Game

### [Code](./Punch-game)


<br>

---

<br>

# 6. Multiple Vue Instances

## 6.1 What Are Multiple Vue Instances?

### 6.1.1 Definition

In Vue 2, an application can contain multiple Vue instances mounted to different DOM elements.

Each instance manages its own configured data and behavior.

### 6.1.2 Example

```html
<div id="app-one">
    <h2>{{ title }}</h2>
</div>

<div id="app-two">
    <h2>{{ title }}</h2>
</div>
```

```js
new Vue({
    el: "#app-one",
    data: {
        title: "First Vue Instance"
    }
});

new Vue({
    el: "#app-two",
    data: {
        title: "Second Vue Instance"
    }
});
```

Each instance renders its own title.

### 6.1.3 Separate State

The first instance has its own `title` property, and the second instance has a separate `title` property.

Changing the first instance's data does not automatically change the second instance's data.

<br>

## 6.2 Understanding Instance Boundaries

### 6.2.1 Independent Instances

```text
Vue Instance 1
    |
    +--> #app-one
    |
    +--> title = "First Vue Instance"


Vue Instance 2
    |
    +--> #app-two
    |
    +--> title = "Second Vue Instance"
```

Each instance is responsible for its own mounted application area.

### 6.2.2 Scope of the Instance

A Vue 2 instance does not automatically control every element in the page.

It controls its configured root element and the content Vue renders within that mounting boundary.

### 6.2.3 When Is This Useful?

Multiple instances are mainly useful for understanding Vue's boundaries or for isolated widgets embedded in a page.

For a typical application, a single root application containing multiple components is generally the preferred architecture.

<br>

## 6.3 Vue 3 Equivalent

### 6.3.1 Creating Multiple Applications

Vue 3 uses `createApp()` to create application instances.

```js
const appOne = Vue.createApp({
    data() {
        return {
            title: "First App"
        };
    }
});

const appTwo = Vue.createApp({
    data() {
        return {
            title: "Second App"
        };
    }
});

appOne.mount("#app-one");
appTwo.mount("#app-two");
```

Each application is mounted independently.

### 6.3.2 Application vs Component

A Vue application created with `createApp()` is not the same thing as a component instance.

- The application provides the root configuration and application-level services.
- Components represent individual parts of the UI.
- Components are typically nested inside a single application.

### 6.3.3 Modern Architecture

```text
Vue Application
       |
       v
   Root Component
       |
       +--> Header Component
       |
       +--> Main Component
       |
       +--> Footer Component
```

This is the common structure for modern Vue applications.

<br>

## 6.4 Key Takeaways

### 6.4.1 Remember

- Vue 2 supports multiple instances mounted to different elements.
- Each instance has its own state and behavior.
- A typical modern application uses one root application with many components.
- Vue 3 uses `createApp()` to create an application.

<br>

---

<br>

# 7. Part 2 — Quick Revision

## 7.1 Essential Syntax

### 7.1.1 Cheat Sheet

| Syntax | Purpose |
|---|---|
| `computed: { ... }` | Defines computed properties in the Options API |
| `:class="{ active: isActive }"` | Conditionally applies a CSS class |
| `v-if="condition"` | Conditionally renders an element |
| `v-else-if="condition"` | Adds another conditional branch |
| `v-else` | Provides the fallback branch |
| `v-show="condition"` | Toggles CSS visibility |
| `v-for="item in items"` | Renders a collection |
| `:key="item.id"` | Provides a stable item identity |
| `@click="method"` | Handles a click event |
| `:style="{ width: health + '%' }"` | Dynamically binds inline styles |
| `Vue.createApp({...})` | Creates a Vue 3 application |

<br>

## 7.2 Self-Assessment

### 7.2.1 Questions

1. What is the difference between a computed property and a method?
2. Why are computed properties cached?
3. How can you apply a CSS class based on a Boolean value?
4. What is the difference between `v-if` and `v-show`?
5. How do `v-if`, `v-else-if`, and `v-else` work together?
6. How does `v-for` render an array?
7. Why should you use a stable `:key`?
8. How can you bind an element's CSS width to a reactive number?
9. How can you stop a game from accepting further punches after completion?
10. What is the difference between multiple Vue instances and multiple components inside one application?

### 7.2.2 Practical Exercise

Build a small product list application that:

- Displays products using `v-for`.
- Calculates the total price using a computed property.
- Uses a dynamic class to highlight products that are in stock.
- Uses `v-if` to display an empty-state message.
- Includes a button that adds a product.
- Includes a button that toggles product visibility.

**Completion criterion:** You should be able to explain how each directive, computed property, and event handler contributes to the application's behavior.

<br>

---

<br>

# 8. Vue 2 to Vue 3 — Essential Mapping

## 8.1 Reactivity

### 8.1.1 Computed Properties

Vue 2 Options API:

```js
computed: {
    total() {
        return this.price * this.quantity;
    }
}
```

Vue 3 Composition API:

```vue
<script setup>
import { ref, computed } from 'vue'

const price = ref(100)
const quantity = ref(2)

const total = computed(() => price.value * quantity.value)
</script>

<template>
    <p>{{ total }}</p>
</template>
```

Computed properties work in both versions. The Composition API uses `computed()` rather than the component's `computed` option.

<br>

## 8.2 Rendering

### 8.2.1 Directives

The following directives remain fundamental in Vue 3:

- `v-if`
- `v-else-if`
- `v-else`
- `v-show`
- `v-for`
- `v-bind` / `:`
- `v-on` / `@`

The core concepts from the course transfer directly.

<br>

## 8.3 Multiple Applications

### 8.3.1 Modern Entry Point

Vue 2:

```js
new Vue({
    el: "#app"
});
```

Vue 3:

```js
Vue.createApp({}).mount("#app");
```

In a Vite project, the usual approach is to import Vue and the root component in `main.js`:

```js
import { createApp } from 'vue'
import App from './App.vue'

createApp(App).mount('#app')
```

<br>

---

<br>

