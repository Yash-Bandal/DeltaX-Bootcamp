# Part 4 — Component Communication

**Course:** Net Ninja — Vue JS 2 Tutorial  
**Lessons:** 22–28  
**Goal:** Understand props, primitive vs reference types, child-to-parent communication, event buses, lifecycle hooks, slots, and dynamic components.

## Table of Contents

1. [Props](#1-props)
2. [Primitive vs Reference Types](#2-primitive-vs-reference-types)
3. [Events — Child to Parent](#3-events--child-to-parent)
4. [The Event Bus](#4-the-event-bus)
5. [Lifecycle Hooks](#5-lifecycle-hooks)
6. [Slots](#6-slots)
7. [Dynamic Components](#7-dynamic-components)
8. [Part 4 — Quick Revision](#8-part-4--quick-revision)
9. [Vue 2 to Vue 3 — Essential Mapping](#9-vue-2-to-vue-3--essential-mapping)

---

<br>

# 1. Props

## 1.1 What Are Props?

### 1.1.1 Definition

**Props** (properties) allow a parent component to pass data to a child component.

For example, a movie list can pass each movie's title and year to a reusable movie card.

```text
Parent: MovieList
       |
       | passes title and year
       v
Child: MovieCard
       |
       v
Displays movie information
```

Props help components remain reusable without hardcoding their data.

### 1.1.2 Why Use Props?

Suppose we need to display three movies.

Without props, we might create separate components containing hardcoded titles.

With props, we create one `MovieCard` component and pass different values to each instance.

Benefits:

- Reusability
- Clear parent-child data flow
- Easier maintenance
- Separation of responsibilities

### 1.1.3 One-Way Data Flow

The normal data flow is from parent to child.

```text
Parent data
    |
    v
Props
    |
    v
Child component
    |
    v
Rendered UI
```

The child should not directly modify a prop. Instead, it can emit an event asking the parent to perform an update.

<br>

## 1.2 Passing Props in Vue 2

### 1.2.1 Registering a Prop

Suppose the child component is called `MovieCard`.

```js
Vue.component("movie-card", {
    props: ["title"],

    template: `
        <div>
            <h3>{{ title }}</h3>
        </div>
    `
});
```

The `props` option declares that the component accepts a prop named `title`.

### 1.2.2 Passing a Prop from the Parent

```html
<div id="app">
    <movie-card title="Inception"></movie-card>
    <movie-card title="Interstellar"></movie-card>
</div>
```

Each component instance receives its own title value.

Output:

```text
Inception
Interstellar
```

### 1.2.3 Static vs Dynamic Props

A static attribute passes a literal string:

```html
<movie-card title="Inception"></movie-card>
```

To pass a JavaScript expression, use `v-bind` or its shorthand `:`.

```html
<movie-card :title="movieTitle"></movie-card>
```

```js
data: {
    movieTitle: "Inception"
}
```

The second form binds the prop to the value of `movieTitle`.

<br>

## 1.3 Passing Different Data Types

### 1.3.1 String Props

```html
<movie-card title="Inception"></movie-card>
```

This passes a string.

### 1.3.2 Number and Boolean Props

```html
<movie-card :year="2010"></movie-card>
<movie-card :featured="true"></movie-card>
```

The `:` syntax evaluates the expression, allowing the parent to pass a number or Boolean rather than a string.

### 1.3.3 Object Props

```html
<movie-card :movie="movie"></movie-card>
```

```js
data: {
    movie: {
        title: "Inception",
        year: 2010
    }
}
```

The child receives the object through its `movie` prop.

<br>

## 1.4 Props in Vue 3

### 1.4.1 Declaring Props with `<script setup>`

```vue
<script setup>
defineProps({
    title: String,
    year: Number
})
</script>

<template>
    <article>
        <h3>{{ title }}</h3>
        <p>{{ year }}</p>
    </article>
</template>
```

`defineProps()` declares the inputs the component accepts. It is a compiler macro and does not need to be imported.

### 1.4.2 Passing Props from the Parent

```vue
<script setup>
import MovieCard from './MovieCard.vue'

const movie = {
    title: 'Inception',
    year: 2010
}
</script>

<template>
    <MovieCard
        :title="movie.title"
        :year="movie.year"
    />
</template>
```

### 1.4.3 Important Rule

Props are one-way inputs. Vue warns against directly mutating props because the parent is responsible for their source of truth.

If the child needs to request a change, use a custom event. If the child needs independent editable state, initialize a separate local value where appropriate.

<br>


## 1.5 Props Validation in Vue 2 and Vue 3

Vue allows us to validate props by specifying their type, whether they are required, default values, and custom validation rules.

### 1.5.1 Basic Validation

```js
export default {
    props: {
        title: String,
        year: Number,
        objectArray: {
            type: Array,
            required: true
        }
    }
}
```

- `type` specifies the expected data type.
- `required: true` makes the prop mandatory.
- Vue displays warnings when prop validation fails during development.

### 1.5.2 Default Values

Use `default` to provide a fallback value. For arrays and objects, use a factory function.

```js
props: {
    title: {
        type: String,
        default: 'Untitled'
    },
    items: {
        type: Array,
        default: () => []
    }
}
```

### 1.5.3 Custom Validation

Use `validator` to check specific conditions.

```js
props: {
    rating: {
        type: Number,
        validator(value) {
            return value >= 0 && value <= 10
        }
    }
}
```

Here, `rating` must be between 0 and 10.
> [!Note]
> These examples work in Vue 2 and Vue 3 Options API.
>
> In Vue 3, the same prop options can also be used with `defineProps()` inside `<script setup>`.
>
> Prop validation generates warnings; it does not automatically reject invalid values.


<br>

---

<br>

# 2. Primitive vs Reference Types

## 2.1 Primitive Types

### 2.1.1 What Are Primitive Types?

JavaScript primitive types include:

- String
- Number
- Boolean
- `undefined`
- `null`
- BigInt
- Symbol

Primitive values are copied by value when assigned to another variable.

### 2.1.2 Example

```js
let a = 10;
let b = a;

b = 20;

console.log(a); // 10
console.log(b); // 20
```

Changing `b` does not change `a`.

### 2.1.3 Primitive Props

Suppose a parent passes a number to a child.

```html
<counter-display :count="count"></counter-display>
```

The child receives the current numeric value as its prop.

However, the prop itself should not be reassigned by the child. If the parent updates its state, Vue updates the prop accordingly.

<br>

## 2.2 Reference Types

### 2.2.1 What Are Reference Types?

Objects and arrays are reference types.

When an object is assigned to another variable, both variables can refer to the same object.

```js
const a = { name: "Alex" };
const b = a;

b.name = "Sam";

console.log(a.name); // "Sam"
console.log(b.name); // "Sam"
```

Both variables refer to the same object.

### 2.2.2 Passing an Object as a Prop

```html
<user-card :user="user"></user-card>
```

```js
data: {
    user: {
        name: "Alex",
        age: 25
    }
}
```

The child receives a reference to the object.

Although the prop binding is one-way, nested object properties can still refer to shared mutable data.

### 2.2.3 Why This Matters

Consider a child component that changes a nested property:

```js
this.user.name = "Sam";
```

Because the parent and child may refer to the same object, the parent's object can also appear changed.

Vue does not make this a recommended communication pattern. It makes the data flow harder to understand and maintain.

<br>

## 2.3 How to Handle Object Updates Properly

### 2.3.1 Recommended Pattern: Emit an Event

The child can notify the parent that a change is requested.

Vue 2 child:

**Syntax:**
```js
$emit('event-name', parameter)
```
**Example:**
```js
this.$emit("rename-user", "Sam");
```

Vue 2 parent:

```html
<user-card
    :user="user"
    @rename-user="user.name = $event"
></user-card>
```

The parent performs the mutation.

### 2.3.2 Vue 3 Equivalent

Child:

```vue
<script setup>
const emit = defineEmits(['rename-user'])

function renameUser() {
    emit('rename-user', 'Sam')
}
</script>

<template>
    <button @click="renameUser">
        Rename user
    </button>
</template>
```

Parent:

```vue
<script setup>
import { ref } from 'vue'
import UserCard from './UserCard.vue'

const user = ref({
    name: 'Alex',
    age: 25
})
</script>

<template>
    <p>{{ user.name }}</p>
    <UserCard
        :user="user"
        @rename-user="user.name = $event"
    />
</template>
```

### 2.3.3 Key Distinction

Passing an object prop does not mean the child owns that object.

The parent should generally remain responsible for shared application state. The child communicates changes through events or a clearly defined shared-state mechanism.

<br>

## 2.4 Key Takeaways

### 2.4.1 Remember

- Primitive values are copied by value.
- Objects and arrays can share references.
- A child can accidentally mutate shared nested data.
- Props are still conceptually one-way inputs.
- Prefer events for requesting parent-owned state changes.

<br>


---

<br>

# 3. Events — Child to Parent

## 3.1 Why Do We Need Child-to-Parent Communication?

### 3.1.1 The Problem

Props allow data to flow from parent to child.

But what if a child button is clicked and the parent needs to update its data?

The child should emit an event to notify the parent.

```text
Parent
  |
  | passes props
  v
Child
  |
  | emits event
  v
Parent handles event
  |
  v
Parent updates state
```

### 3.1.2 Example Scenario

A parent displays a counter. A child component contains the Increase button.

The child does not own the counter. Instead, it emits an event requesting an increment.

### 3.1.3 Why Not Modify Parent Data Directly?

The child should not reach into the parent component's private state.

Using events makes the relationship explicit, predictable, and easier to maintain.

<br>

## 3.2 Custom Events in Vue 2

### 3.2.1 Child Component

```js
Vue.component("counter-button", {
    template: `
        <button @click="$emit('increment')">
            Increase
        </button>
    `
});
```

`$emit()` dispatches a custom component event called `increment`.

### 3.2.2 Parent Component

```html
<div id="app">
    <p>Count: {{ count }}</p>

    <counter-button
        @increment="count++"
    ></counter-button>
</div>
```

```js
new Vue({
    el: "#app",

    data: {
        count: 0
    }
});
```

When the child emits `increment`, the parent's handler increases `count`.

### 3.2.3 Passing Data with an Event

A child can include a payload when emitting an event.

```js
this.$emit("increment", 5);
```

The parent receives the payload:

```html
<counter-button
    @increment="count += $event"
></counter-button>
```

The `$event` expression refers to the emitted payload in this template handler.

<br>

## 3.3 Custom Events in Vue 3

### 3.3.1 Declare Events

```vue
<script setup>
const emit = defineEmits(['increment'])

function increment() {
    emit('increment', 5)
}
</script>

<template>
    <button @click="increment">
        Increase by 5
    </button>
</template>
```

`defineEmits()` declares the events the component can emit.

### 3.3.2 Listen in the Parent

```vue
<script setup>
import { ref } from 'vue'
import CounterButton from './CounterButton.vue'

const count = ref(0)
</script>

<template>
    <p>Count: {{ count }}</p>

    <CounterButton
        @increment="count += $event"
    />
</template>
```

The parent receives the emitted payload and updates its state.

### 3.3.3 Execution Flow

```text
User clicks child button
          |
          v
Child calls emit('increment', 5)
          |
          v
Parent's @increment handler runs
          |
          v
Parent adds 5 to count
          |
          v
Vue updates the displayed count
```

<br>

## 3.4 Props vs Events

### 3.4.1 Comparison

| Props | Custom events |
|---|---|
| Parent to child | Child to parent |
| Pass data | Notify that something happened |
| Declared using `props` / `defineProps()` | Emitted using `$emit()` / `defineEmits()` |
| Read-only inputs by design | Can carry payloads |

### 3.4.2 The Standard Pattern

```text
Parent owns state
       |
       v
Pass state through props
       |
       v
Child displays state
       |
       v
Child emits an event
       |
       v
Parent updates state
       |
       v
Updated prop reaches child
```

### 3.4.3 Key Takeaways

- Use props to pass data down.
- Use custom events to communicate changes upward.
- Events can carry payloads.
- The parent should usually own shared state.


<br>

---

<br>

# 4. The Event Bus

## 4.1 What Is an Event Bus?

### 4.1.1 Definition

An event bus is a shared object used to publish and listen for events between components without requiring a direct parent-child relationship.

In Vue 2, developers commonly implemented an event bus using a Vue instance.

### 4.1.2 Why Was It Used?

Suppose two components are far apart in the component tree.

```text
App
 |
 +----> Header
 |
 +----> Main
          |
          +----> NotificationPanel
```

If `Header` needs to notify `NotificationPanel`, passing events through every intermediate component can become inconvenient.

An event bus allows one component to emit an event and another to listen for it.

### 4.1.3 Important Limitation

An event bus creates implicit communication. It can become difficult to track who emits events and who handles them.

Modern Vue applications generally prefer props, custom events, composables, or a state-management solution such as Pinia.

<br>

## 4.2 Event Bus in Vue 2

### 4.2.1 Create a Shared Bus

```js
// event-bus.js
import Vue from 'vue'

export const eventBus = new Vue()
```

Both components import the same exported bus instance.

### 4.2.2 Emit an Event

```js
import { eventBus } from './event-bus'

eventBus.$emit('message-sent', 'Hello from Header')
```

The emitter publishes the event and its payload.

### 4.2.3 Listen for the Event

```js
import { eventBus } from './event-bus'

export default {
    created() {
        eventBus.$on('message-sent', message => {
            console.log(message)
        })
    },

    beforeDestroy() {
        eventBus.$off('message-sent')
    }
}
```

The listener receives the message.

For production code, prefer removing the specific handler rather than indiscriminately removing all listeners for an event.

<br>

## 4.3 Why the Event Bus Changed in Vue 3

### 4.3.1 Vue 2 vs Vue 3

In Vue 2, a Vue instance exposed `$on`, `$off`, and `$emit`, which made a Vue instance convenient as an event bus.

Vue 3 removed instance-level `$on`, `$off`, and `$once` APIs.

Therefore, the Vue 2 event-bus pattern cannot be copied directly into Vue 3.

### 4.3.2 Modern Alternatives

Use the communication method that matches the relationship:

- Parent to child: props.
- Child to parent: emitted events.
- Shared application state: Pinia.
- Reusable shared behavior: composables.
- Independent event-based communication: a dedicated event library, when genuinely needed.

### 4.3.3 Decision Rule

If two components need to share persistent application state, a state store is generally more appropriate than using events as a substitute for state management.

<br>

## 4.4 Key Takeaways

### 4.4.1 Remember

- A Vue 2 event bus enables communication between unrelated components.
- Vue 2 commonly used a shared Vue instance as the bus.
- Vue 3 removed the Vue instance's event-listener APIs used by that pattern.
- Modern applications generally prefer explicit communication or shared state.

<br>

---

<br>

# 5. Lifecycle Hooks

## 5.1 What Is a Component Lifecycle?

### 5.1.1 Definition

A component lifecycle describes the stages a component passes through as it is created, rendered, updated, and destroyed or unmounted.

Vue provides lifecycle hooks that allow code to run at specific stages.

<br>

> [!Tip]
> Lifecycle hooks are functions like onMounted(), onUpdated(), and onUnmounted() that allow us to execute specific code when a particular phase of a component's life occurs.
>
> For example:
> - onMounted() → when the component appears on the page.
> - onUpdated() → when the component's DOM updates.
> - onUnmounted() → when the component is removed from the page.
> Think of it this way:
>
> Component reaches a phase → Vue calls the corresponding lifecycle hook → your code inside that hook executes.

<br>

### 5.1.2 Why Use Lifecycle Hooks?

They are useful for tasks such as:

- Fetching initial data.
- Setting up subscriptions.
- Interacting with rendered DOM elements.
- Registering external event listeners.
- Cleaning up resources when a component is removed.

### 5.1.3 General Lifecycle Flow

<br>
<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/cdc75c17-7ae1-4af8-891b-a20afa146772" />
</div>
<br>

```text
Component creation
        |
        v
Initialization
        |
        v
DOM rendering
        |
        v
Mounted
        |
        v
Reactive updates
        |
        v
Updated
        |
        v
Unmounted / destroyed
```

<br>

## 5.2 Common Vue 2 Lifecycle Hooks

### 5.2.1 `created()`

Called after the Vue instance has finished setting up its data observation, computed properties, methods, and watchers.

```js
created() {
    console.log("Component instance created");
}
```

The component's DOM has not yet been mounted at this point.

### 5.2.2 `mounted()`

Called after the component has been mounted and its initial DOM has been created.

```js
mounted() {
    console.log("Component mounted");
}
```

This is a common place to access rendered DOM elements through refs.

### 5.2.3 `updated()`

Called after a component's DOM has been updated because of a reactive state change.

```js
updated() {
    console.log("Component updated");
}
```

Avoid creating uncontrolled state changes inside this hook, as that can lead to repeated update cycles.

<br>

## 5.3 Destruction Hooks in Vue 2

### 5.3.1 `beforeDestroy()`

Called immediately before the Vue instance is destroyed.

Use it to clean up resources such as external event listeners or timers.

```js
beforeDestroy() {
    window.removeEventListener('resize', this.handleResize);
}
```

### 5.3.2 `destroyed()`

Called after the Vue instance has been destroyed and its event listeners and child components have been cleaned up.

This hook is mainly relevant to understanding older Vue 2 applications.

### 5.3.3 Important Distinction

A component's lifecycle hooks should be chosen according to the task.

For example, DOM-dependent logic belongs after mounting, while cleanup belongs before destruction or unmounting.

<br>

## 5.4 Vue 3 Lifecycle Hooks

### 5.4.1 Composition API Mapping

| Vue 2 Options API | Vue 3 Composition API |
|---|---|
| `beforeCreate` | `setup()` begins the setup process; no direct equivalent hook is normally needed |
| `created` | Setup logic runs during `setup()` |
| `beforeMount` | `onBeforeMount()` |
| `mounted` | `onMounted()` |
| `beforeUpdate` | `onBeforeUpdate()` |
| `updated` | `onUpdated()` |
| `beforeDestroy` | `onBeforeUnmount()` |
| `destroyed` | `onUnmounted()` |

### 5.4.2 Example

```vue
<script setup>
import { onMounted, onUnmounted } from 'vue'

function handleResize() {
    console.log(window.innerWidth)
}

onMounted(() => {
    window.addEventListener('resize', handleResize)
})

onUnmounted(() => {
    window.removeEventListener('resize', handleResize)
})
</script>

<template>
    <p>Resize the browser window.</p>
</template>
```

The component registers a listener when mounted and removes it when unmounted.

### 5.4.3 Execution Flow

```text
Component mounts
       |
       v
onMounted() runs
       |
       v
Resize listener is registered
       |
       v
Window resizes
       |
       v
handleResize() runs
       |
       v
Component unmounts
       |
       v
onUnmounted() runs
       |
       v
Listener is removed
```

<br>

## 5.5 Key Takeaways

### 5.5.1 Remember

- Lifecycle hooks run at defined component stages.
- `created()` is useful for logic that does not require the mounted DOM.
- `mounted()` is useful for DOM-dependent work.
- `updated()` runs after a reactive update.
- Clean up listeners and other external resources during destruction or unmounting.
- Vue 3 uses Composition API hooks such as `onMounted()` and `onUnmounted()`.

<br>

---

<br>

# 6. Slots

## 6.1 What Are Slots?

### 6.1.1 Definition

Slots allow a parent component to pass template content into a child component.

Props pass data. Slots pass content.

For example, a reusable card component can define a container while allowing the parent to supply the card's heading and body.

### 6.1.2 Why Use Slots?

Without slots, a child component may need many props just to customize its internal content.

Slots let the parent decide what content appears inside a reusable layout.

### 6.1.3 Basic Relationship

```text
Parent component
       |
       | provides template content
       v
Child component
       |
       | renders content at <slot>
       v
Final rendered UI
```

<br>

## 6.2 Default Slots

### 6.2.1 Child Component

```vue
<template>
    <div class="card">
        <slot></slot>
    </div>
</template>
```

The `<slot>` element marks where parent-provided content should be rendered.

### 6.2.2 Parent Component

```html
<custom-card>
    <h2>Movie Details</h2>
    <p>Information about the movie.</p>
</custom-card>
```

The heading and paragraph appear inside the child's card container.

### 6.2.3 Fallback Content

A slot can define default content.

```html
<slot>
    No content provided.
</slot>
```

If the parent supplies no content for that slot, the fallback content is rendered.

<br>

## 6.3 Named Slots

### 6.3.1 Why Named Slots?

A component may need multiple content areas, such as a header, body, and footer.

Named slots let the parent target specific locations.

### 6.3.2 Child Component

```vue
<template>
    <section class="card">
        <header>
            <slot name="header"></slot>
        </header>

        <main>
            <slot></slot>
        </main>

        <footer>
            <slot name="footer"></slot>
        </footer>
    </section>
</template>
```

The unnamed slot is the default slot.

### 6.3.3 Parent Component

```vue
<template>
    <custom-card>
        <template v-slot:header>
            <h2>Movie Details</h2>
        </template>

        <p>Movie description goes here.</p>

        <template v-slot:footer>
            <button>Read More</button>
        </template>
    </custom-card>
</template>
```

Vue places each section in its corresponding slot.

<br>

## 6.4 Slot Shorthand and Vue 3

### 6.4.1 The `#` Shorthand

The `v-slot` directive can be shortened to `#`.

```html
<custom-card>
    <template #header>
        <h2>Movie Details</h2>
    </template>

    <template #footer>
        <button>Read More</button>
    </template>
</custom-card>
```

### 6.4.2 Scoped Slots

Scoped slots allow a child component to expose values to the slot content supplied by the parent.

Child:

```vue
<template>
    <slot :message="message"></slot>
</template>

<script>
export default {
    data() {
        return {
            message: "Hello from the child"
        }
    }
}
</script>
```

Parent:

```html
<custom-card v-slot:default="slotProps">
    <p>{{ slotProps.message }}</p>
</custom-card>
```

The parent can use the value exposed through the slot props.

### 6.4.3 Vue 2 vs Vue 3

Slots exist in both versions. Vue 3 standardized slot usage around the unified `v-slot` syntax and supports the same core ideas of default, named, and scoped slots.

<br>

## 6.5 Key Takeaways

### 6.5.1 Remember

- Props pass data into a component.
- Slots pass template content into a component.
- Default slots provide a general content area.
- Named slots provide multiple content areas.
- Scoped slots let child components expose values to supplied slot content.
- `#header` is shorthand for `v-slot:header`.

<br>

---

<br>

# 7. Dynamic Components

## 7.1 What Are Dynamic Components?

### 7.1.1 Definition

Dynamic components allow Vue to switch which component is rendered at a particular location.

They are useful for tabs, multi-step forms, dashboards, and views that change based on user selection.

### 7.1.2 Why Use Them?

Imagine an interface with three tabs:

- Profile
- Settings
- Activity

Instead of rendering every view at once and manually hiding the others, Vue can render the selected component dynamically.

### 7.1.3 The `<component>` Element

Vue provides a special `<component>` element with the `is` attribute.

```html
<component :is="currentComponent"></component>
```

The value of `currentComponent` determines which component Vue renders.

<br>

## 7.2 Dynamic Components in Vue 2

### 7.2.1 Template

```html
<div id="app">
    <button @click="currentView = 'home'">Home</button>
    <button @click="currentView = 'about'">About</button>

    <component :is="currentView"></component>
</div>
```

### 7.2.2 Register Components

```js
Vue.component("home", {
    template: "<p>Home view</p>"
});

Vue.component("about", {
    template: "<p>About view</p>"
});

new Vue({
    el: "#app",

    data: {
        currentView: "home"
    }
});
```

When `currentView` changes, Vue renders the corresponding component.

### 7.2.3 Execution Flow

```text
currentView = "home"
        |
        v
Vue renders Home
        |
        v
User clicks About
        |
        v
currentView = "about"
        |
        v
Vue switches the component
        |
        v
About is rendered
```

<br>

## 7.3 Dynamic Components in Vue 3

### 7.3.1 Import Components

```vue
<script setup>
import { ref } from 'vue'
import HomeView from './HomeView.vue'
import AboutView from './AboutView.vue'

const currentView = ref(HomeView)
</script>

<template>
    <button @click="currentView = HomeView">Home</button>
    <button @click="currentView = AboutView">About</button>

    <component :is="currentView" />
</template>
```

Here, the `is` binding receives a component definition.

### 7.3.2 Component Switching

When the value changes from `HomeView` to `AboutView`, Vue renders the selected component at that location.

### 7.3.3 State Preservation with `<KeepAlive>`

By default, switching away from a dynamic component can unmount it.

If you need to preserve component state while switching between views, Vue provides `<KeepAlive>`.

```html
<KeepAlive>
    <component :is="currentView" />
</KeepAlive>
```

This caches eligible component instances rather than destroying them whenever they are switched out.

<br>

## 7.4 Dynamic Components vs Routing

### 7.4.1 Dynamic Components

Useful when switching views within one page, such as tabs or small panels.

### 7.4.2 Vue Router

Useful when views correspond to application routes and URLs, such as `/movies`, `/profile`, or `/settings`.

### 7.4.3 Key Difference

| Dynamic components | Vue Router |
|---|---|
| Switches the component at a location | Maps URLs to views |
| Often used for tabs and panels | Used for page-level navigation |
| Can work without changing the URL | Usually integrates with browser navigation and URLs |

<br>

## 7.5 Key Takeaways

### 7.5.1 Remember

- `<component :is="...">` renders a component dynamically.
- The `is` value can identify a registered component or a component definition.
- Dynamic components are useful for tabs and changing panels.
- `<KeepAlive>` can preserve component instances.
- Use Vue Router when navigation should correspond to application URLs.

<br>

---

<br>

# 8. Part 4 — Quick Revision

## 8.1 Essential Syntax

### 8.1.1 Cheat Sheet

| Syntax | Purpose |
|---|---|
| `props: ['title']` | Declares props in the Vue 2 Options API |
| `defineProps()` | Declares props in Vue 3 `<script setup>` |
| `this.$emit('save', payload)` | Emits a custom event in Vue 2 |
| `defineEmits()` | Declares custom events in Vue 3 `<script setup>` |
| `eventBus.$emit()` | Emits an event through a Vue 2 event bus |
| `created()` | Vue 2 creation lifecycle hook |
| `mounted()` | Vue 2 mounted lifecycle hook |
| `onMounted()` | Vue 3 Composition API mounted hook |
| `<slot></slot>` | Default slot |
| `<slot name="header">` | Named slot |
| `<template #header>` | Supplies named slot content |
| `<component :is="currentView">` | Dynamic component |
| `<KeepAlive>` | Caches component instances |

<br>

## 8.2 Self-Assessment

### 8.2.1 Questions

1. What are props, and why are they used?
2. What is one-way data flow?
3. Why can mutating an object prop affect the parent?
4. How do child components notify their parents about changes?
5. What is the difference between props and custom events?
6. What is an event bus, and why is the Vue 2 pattern not directly available in Vue 3?
7. What are lifecycle hooks?
8. What is the difference between `created()` and `mounted()`?
9. What is the purpose of slots?
10. What is the difference between default slots, named slots, and scoped slots?
11. How does `<component :is="...">` work?
12. When would you use `<KeepAlive>` rather than ordinary dynamic component switching?

### 8.2.2 Practical Exercise

Build a movie dashboard with the following components:

- `MovieList.vue` owns the list of movies.
- `MovieCard.vue` receives a movie through props.
- `MovieCard.vue` emits a `delete-movie` event when its delete button is clicked.
- `MovieList.vue` handles the event and removes the movie.
- `MovieDetails.vue` uses slots for its header and footer.
- A dynamic component switches between a movie list and a settings panel.
- A lifecycle hook logs when the dashboard is mounted.

**Completion criterion:** You should be able to explain how data moves from parent to child, how events communicate changes upward, and how slots and dynamic components affect rendering.

<br>

---

<br>

# 9. Vue 2 to Vue 3 — Essential Mapping

## 9.1 Component Communication

### 9.1.1 Vue 2

```js
props: ['title']
```

```js
this.$emit('save', payload)
```

### 9.1.2 Vue 3

```js
const props = defineProps(['title'])
const emit = defineEmits(['save'])

emit('save', payload)
```

Props still pass data down, and emitted events still communicate changes upward.

<br>

## 9.2 Lifecycle Hooks

### 9.2.1 Vue 2

```js
mounted() {
    console.log('Mounted')
}
```

### 9.2.2 Vue 3

```js
import { onMounted } from 'vue'

onMounted(() => {
    console.log('Mounted')
})
```

The Composition API expresses lifecycle hooks through imported functions.

<br>

## 9.3 Event Bus

### 9.3.1 Vue 2

A shared Vue instance can act as an event bus using `$emit`, `$on`, and `$off`.

### 9.3.2 Vue 3

Vue 3 removed those instance event-listener APIs. Prefer explicit props/events, composables, or Pinia depending on the problem.

<br>

## 9.4 Slots and Dynamic Components

### 9.4.1 Slots

The core slot concepts remain the same in both Vue versions.

```html
<template #header>
    <h2>Movie Details</h2>
</template>
```

### 9.4.2 Dynamic Components

Both versions support dynamic component rendering with the special `<component>` element.

```html
<component :is="currentView" />
```

Vue 3 additionally supports modern SFC and Composition API workflows used in the examples above.

<br>

---

<br>
](https://api.chatwait.com/functions/v1/click?ad=15e16ec4-783d-4573-accb-731951a40900&device=569ba72f-6b23-40e4-86bd-f7686d3925cb&site=claude.ai&token=eyJkIjoiNTY5YmE3MmYtNmIyMy00MGU0LTg2YmQtZjc2ODZkMzkyNWNiIiwiYSI6IjE1ZTE2ZWM0LTc4M2QtNDU3My1hY2NiLTczMTk1MWE0MDkwMCIsImlhdCI6MTc5Mjc1NjkxNTYwNSwibiI6ImRiMGM3NGNiLTc5MDgtNDUyNS1iMGVlLWYzZTJjZWZiYTJhYyJ9.uJnyVdhaCWcTcbLeCscv9eobRbh1mzMIidbtJt7eHKQ)
