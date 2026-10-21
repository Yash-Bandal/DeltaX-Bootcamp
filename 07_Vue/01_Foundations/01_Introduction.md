# Part 1 — Vue Fundamentals

**Course:** Net Ninja — Vue JS 2 Tutorial  
**Lessons:** 1–8  
**Goal:** Understand Vue's basic architecture, data binding, events, and user input.

## Table of Contents

1. [Introduction](#1-introduction)
2. [The Vue Instance](#2-the-vue-instance)
3. [Data and Methods](#3-data-and-methods)
4. [Data Binding](#4-data-binding)
5. [Events](#5-events)
6. [Event Modifiers](#6-event-modifiers)
7. [Keyboard Events](#7-keyboard-events)
8. [Two-Way Data Binding](#8-two-way-data-binding)

<br>

---


# 1. Introduction

## 1.1 What is Vue.js?

### 1.1.1 Definition

Vue.js is a JavaScript framework used to build interactive user interfaces (UIs).

- **HTML** defines the structure.
- **CSS** defines the appearance.
- **JavaScript** defines the behavior.
- **Vue.js** helps connect application data with the UI and keep them synchronized.

### 1.1.2 Why use Vue.js?
Its very Lean comparitively (16kb), loads page, without everytime refreshing server

Without Vue, we often select DOM elements and update them manually.

```js
document.getElementById("message").textContent = "Hello";
```

With Vue, we can bind the UI to application data.

```html
<p>{{ message }}</p>
```

When the reactive `message` value changes, Vue updates the displayed text automatically.

### 1.1.3 Core idea: Reactivity

Reactivity means Vue tracks changes to reactive data and updates the relevant parts of the UI.

**Flow:**

```text
Data changes
     |
     v
Vue detects the change
     |
     v
Relevant UI updates
```

<br>

## 1.2 Vue Application Basics

### 1.2.1 Vue Instance in Vue 2

Vue 2 commonly creates an application using `new Vue()`.

```js
new Vue({
    el: "#app",
    data: {
        message: "Hello Vue!"
    }
});
```

- `el` specifies the element where Vue operates.
- `data` contains the component's application data.
- Vue makes the data reactive and exposes it to the template.

### 1.2.2 A Simple Vue 2 Example

```html
<div id="app">
    <h1>{{ message }}</h1>
</div>

<script>
    new Vue({
        el: "#app",
        data: {
            message: "Hello Vue!"
        }
    });
</script>
```

Output:

```text
Hello Vue!
```

### 1.2.3 Vue 3 Equivalent

Vue 3 uses `createApp()` instead of the Vue 2 root-instance pattern.

```js
const app = Vue.createApp({
    data() {
        return {
            message: "Hello Vue!"
        };
    }
});

app.mount("#app");
```

In modern Vue projects, the application is commonly created in `main.js` and mounted into the HTML element with `id="app"`.

<br>

## 1.3 Key Takeaways

### 1.3.1 Remember

- Vue is a JavaScript framework for building UIs.
- Vue connects application data with the rendered UI.
- Reactivity reduces the need for manual DOM updates.
- Vue 2 uses `new Vue()`; Vue 3 uses `createApp()`.

<br>

--- 

<br>

# 2. The Vue Instance

## 2.1 What is the Vue Instance?

### 2.1.1 Definition

In Vue 2, `new Vue()` creates a Vue instance.

The instance manages its configured data, methods, computed properties, lifecycle hooks, and other options.

```js
new Vue({
    el: "#app",
    data: {
        name: "Yash"
    }
});
```

### 2.1.2 The `el` Option

The `el` option specifies the DOM element that the Vue instance will control.

```js
el: "#app"
```

This selects:

```html
<div id="app"></div>
```

The `#` denotes an ID selector, as in CSS.

### 2.1.3 The `data` Option

The `data` option contains values used by the Vue instance.

```js
data: {
    name: "Yash",
    age: 25
}
```

These values can be accessed in the associated template:

```html
<p>{{ name }}</p>
<p>{{ age }}</p>
```

The age here is an example value, not a required property.

<br>

## 2.2 How a Vue Instance Works

### 2.2.1 Execution Flow

```text
1. JavaScript creates the Vue instance
                |
                v
2. Vue reads the configuration
                |
                v
3. Vue makes supported data reactive
                |
                v
4. Vue connects to the target element
                |
                v
5. Vue renders the template
```

### 2.2.2 Example

```html
<div id="app">
    <h2>{{ title }}</h2>
</div>

<script>
    new Vue({
        el: "#app",
        data: {
            title: "My First Vue App"
        }
    });
</script>
```

The instance provides `title` to the template, and Vue renders its value inside the `<h2>`.

### 2.2.3 Important Distinction

The Vue instance is not the HTML element itself.

- The HTML element provides the mounting location.
- The Vue instance manages the configured application behavior and reactive data.

In Vue 3, the application object created by `createApp()` and the component instance are distinct concepts.

<br>

## 2.3 Key Takeaways

### 2.3.1 Remember

- `new Vue()` is the classic Vue 2 entry point.
- `el` selects the mounting element.
- `data` provides the values used by the template.
- Vue 3 applications use `createApp()` and `.mount()`.

<br>

---

<br>

# 3. Data and Methods

## 3.1 Data

### 3.1.1 Definition

Data represents the values an application needs to display or manage.

```js
data: {
    name: "Alex",
    age: 22,
    isLoggedIn: true
}
```

Vue 2 exposes these data properties to the template.

### 3.1.2 Accessing Data

Use double curly braces, also called *interpolation*, to display a value.

```html
<div id="app">
    <p>Name: {{ name }}</p>
    <p>Age: {{ age }}</p>
</div>
```

Vue substitutes the expressions with their current values when rendering.

### 3.1.3 Updating Data

```js
new Vue({
    el: "#app",
    data: {
        count: 0
    }
});
```

A Vue 2 instance can access its data using `this`.

```js
this.count = 5;
```

When a reactive data property changes, Vue updates the UI that depends on it.

<br>

## 3.2 Methods

### 3.2.1 Definition

Methods are functions defined in the Vue instance's `methods` option.

They contain operations that the application can perform.

```js
new Vue({
    el: "#app",
    data: {
        count: 0
    },
    methods: {
        increaseCount: function() {
            this.count++;
        }
    }
});
```

### 3.2.2 Using a Method in the Template

```html
<div id="app">
    <p>{{ count }}</p>
    <button v-on:click="increaseCount">
        Increase
    </button>
</div>
```

When the button is clicked, Vue invokes `increaseCount()`.

### 3.2.3 Understanding `this`

Inside a normal Vue 2 Options API method, `this` refers to the Vue component instance.

```js
methods: {
    increaseCount() {
        this.count++;
    }
}
```

Here, `this.count` accesses the instance's `count` data property.

Avoid using an arrow function as an Options API method when you need Vue's instance-bound `this`, because arrow functions do not have their own `this`.

<br>

## 3.3 Data vs Methods

### 3.3.1 Difference

| Data | Methods |
|---|---|
| Stores values | Performs operations |
| Examples: `count`, `name` | Examples: `increaseCount()`, `login()` |
| Reactive data changes can update the UI | Methods can change data and perform other tasks |

### 3.3.2 Complete Example

```html
<div id="app">
    <h2>{{ message }}</h2>
    <p>Count: {{ count }}</p>
    <button v-on:click="increaseCount">Increase</button>
</div>

<script>
    new Vue({
        el: "#app",
        data: {
            message: "Welcome",
            count: 0
        },
        methods: {
            increaseCount() {
                this.count++;
            }
        }
    });
</script>
```

### 3.3.3 Execution Flow

```text
User clicks the button
        |
        v
Vue invokes increaseCount()
        |
        v
this.count increases
        |
        v
Vue detects the reactive change
        |
        v
The displayed count updates
```

<br>

## 3.4 Key Takeaways

### 3.4.1 Remember

- `data` stores values.
- `methods` defines functions.
- `this` accesses instance properties in normal Vue 2 Options API methods.
- Methods can update reactive data, which updates the UI.

<br>

---

<br>

# 4. Data Binding

## 4.1 What is Data Binding?

### 4.1.1 Definition

Data binding connects JavaScript data with the UI.

When the underlying reactive data changes, Vue can update the corresponding rendered content or bound attributes.

### 4.1.2 Text Interpolation

Use `{{ }}` to display a JavaScript expression's result.

```html
<h1>{{ message }}</h1>
<p>{{ 10 + 20 }}</p>
```

If `message` is `"Hello Vue"`, the output is:

```text
Hello Vue
30
```

### 4.1.3 Text Interpolation Is Not HTML Binding

Vue treats interpolated values as text, not as HTML markup.

```html
<p>{{ message }}</p>
```

If `message` contains `"<strong>Hello</strong>"`, the markup characters are displayed as text.

To render HTML dynamically, Vue provides `v-html`, but it must only be used with trusted content because untrusted HTML can introduce cross-site scripting (XSS) vulnerabilities.

<br>

## 4.2 Attribute Binding

### 4.2.1 The `v-bind` Directive

Use `v-bind` to bind an HTML attribute to a JavaScript expression.

```html
<a v-bind:href="website">Visit website</a>
```

```js
data: {
    website: "https://example.com"
}
```

Vue uses the current value of `website` as the link destination.

### 4.2.2 Shorthand Syntax

The shorthand for `v-bind` is `:`.

```html
<a :href="website">Visit website</a>
```

Both forms are equivalent.

### 4.2.3 Other Attribute Examples

```html
<img :src="imageUrl" :alt="imageDescription">
<button :disabled="isDisabled">Submit</button>
```

Vue binds the values of `imageUrl`, `imageDescription`, and `isDisabled` to the respective attributes or properties.

<br>

## 4.3 Directives

### 4.3.1 Definition

Directives are special Vue template attributes that begin with `v-`.

They tell Vue to perform specific rendering or binding behavior.

### 4.3.2 Common Directives

| Directive | Purpose | Shorthand / Alternative |
| :--- | :--- | :--- |
| `v-bind` | Binds an HTML attribute or DOM property dynamically <br> (e.g., `href`, `src`, `class`, `disabled`). | `:`<br>*Example:* `:href="url"` |
| `v-on` | Listens to DOM events <br> (e.g., `click`, `keyup`, `submit`). | `@`<br>*Example:* `@click="handleClick"` |
| `v-model` | Creates two-way data binding on form inputs <br> (`<input>`, `<textarea>`, `<select>`). | Combines `:value` and `@input`<br>*Example:* `v-model="name"` |
| `v-if` | Conditionally renders an element in the DOM (adds/removes the element completely). | `v-else-if` / `v-else` / `v-show`<br>*Example:* `v-if="isLoggedIn"` |
| `v-for` | Renders a list of items by iterating over an array or object. | Requires `:key` attribute<br>*Example:* `v-for="item in items" :key="item.id"` |
| `v-html` | Dynamically updates an element's `innerHTML` with raw HTML content. | `{{ }}` (text-only interpolation)<br>*Example:* `v-html="rawHtml"` |

### 4.3.3 Example

```html
<div id="app">
    <p>{{ message }}</p>
    <a :href="website">Open website</a>
</div>
```

- `{{ message }}` displays a value.
- `:href="website"` binds the link destination.
- Vue updates the relevant UI when its reactive dependencies change.

<br>

## 4.4 Key Takeaways

### 4.4.1 Remember

- `{{ }}` displays text or expression results.
- `v-bind` and `:` bind attributes or properties.
- Directives add Vue-specific behavior to templates.
- Interpolation escapes HTML; `v-html` renders HTML and requires trusted content.

<br>

---

<br>

# 5. Events

## 5.1 What are Events?

### 5.1.1 Definition

An event represents an interaction or occurrence, such as a click, key press, or form submission.

Vue lets us run methods or expressions when an event occurs.

### 5.1.2 The `v-on` Directive

Use `v-on` to listen for events.

```html
<button v-on:click="increaseCount">
    Increase
</button>
```

Vue calls the `increaseCount` method when the button is clicked.

### 5.1.3 Shorthand Syntax

The shorthand for `v-on` is `@`.

```html
<button @click="increaseCount">
    Increase
</button>
```

Both forms are equivalent.

<br>

## 5.2 Handling Events

### 5.2.1 Calling a Method

```js
new Vue({
    el: "#app",
    data: {
        count: 0
    },
    methods: {
        increaseCount() {
            this.count++;
        }
    }
});
```

```html
<button @click="increaseCount">Increase</button>
```

Vue invokes the method in response to the click.

### 5.2.2 Using an Inline Expression

For a simple operation, an inline expression can be used.

```html
<button @click="count++">Increase</button>
```

For more complex logic, prefer a named method because it is easier to read, reuse, and maintain.

### 5.2.3 Passing Arguments

```html
<button @click="sayHello('Alex')">Greet</button>
```

```js
methods: {
    sayHello(name) {
        alert("Hello " + name);
    }
}
```

Vue evaluates the expression and passes `"Alex"` to the method.

<br>

## 5.3 Accessing the Event Object

### 5.3.1 The Event Object

Browsers provide an event object containing information about the event.

In Vue, `$event` can be used to access that object.

```html
<button @click="handleClick($event)">
    Click me
</button>
```

### 5.3.2 Method Example

```js
methods: {
    handleClick(event) {
        console.log(event.target);
    }
}
```

`event.target` identifies the element that originated the event.

### 5.3.3 When to Use It

Use the event object when you need information about the event or its originating element.

For ordinary actions, a simple method call is often enough.

<br>

## 5.4 Key Takeaways

### 5.4.1 Remember

- `v-on:click` and `@click` are equivalent.
- Event handlers can call methods or evaluate expressions.
- Arguments can be passed to methods.
- `$event` provides access to the event object.

<br>

---

<br>

# 6. Event Modifiers

## 6.1 What are Event Modifiers?

### 6.1.1 Definition

Event modifiers are suffixes added to event directives to apply common event behavior declaratively.

They reduce the need to write repetitive event-handling code.

### 6.1.2 Syntax

```html
<button @click.stop="handleClick">
    Click me
</button>
```

The `.stop` modifier stops the event from propagating to ancestor elements.

### 6.1.3 Common Modifiers

| Modifier | Behavior |
|---|---|
| `.stop` | Stops event propagation |
| `.prevent` | Prevents the browser's default action |
| `.once` | Handles the event only once |
| `.self` | Runs the handler only when the event originates on the element itself |

<br>

## 6.2 The `.stop` Modifier

### 6.2.1 Event Bubbling

<br>
<img width="550" alt="image" src="https://cdn.hashnode.com/res/hashnode/image/upload/v1613329740519/NHfbtuyVD.gif" />
<br>

When an event occurs on a nested element, it can propagate to its ancestors. This is called **event bubbling**.

```html
<div @click="grandparentClick">
    grandparent
    <div @click="parentClick">
        parent
        <div @click="childClick">
            child
        </div>
    </div>
</div>

```

Clicking the child can invoke all handlers because the click bubbles to the parent and grandparent.

### 6.2.2 Preventing Propagation

```html
<div @click="grandparentClick">
    grandparent
    <div @click="parentClick">
        parent
        <div @click.stop="childClick">
            child
        </div>
    </div>
</div>

```


The `.stop` modifier prevents the click event from propagating beyond the button to the parent and grandparent.


### 6.2.3 Important Distinction

Stopping propagation does not prevent the browser's default action.

For example, `.stop` does not automatically prevent a link from navigating.

<br>

## 6.3 The `.prevent` Modifier

### 6.3.1 Preventing Default Behavior

Browsers have default behaviors for certain events.

For example, submitting a traditional HTML form may navigate or reload the page.

```html
<form @submit.prevent="submitForm">
    <button type="submit">Submit</button>
</form>
```

The `.prevent` modifier calls `preventDefault()` on the event before running the handler.

### 6.3.2 Common Use Cases

- Prevent a form from performing a traditional page submission.
- Prevent a link from following its destination when that is appropriate.
- Control an action through JavaScript instead of the browser's default behavior.

### 6.3.3 `.stop` vs `.prevent`

| `.stop` | `.prevent` |
|---|---|
| Stops event propagation | Prevents the default browser action |
| Affects event flow | Affects default behavior |
| Equivalent to `stopPropagation()` | Equivalent to `preventDefault()` |

<br>

## 6.4 Other Useful Modifiers

### 6.4.1 The `.once` Modifier

```html
<button @click.once="showMessage">
    Show message
</button>
```

The handler runs only once for that event listener.

### 6.4.2 The `.self` Modifier

```html
<div @click.self="handleParentClick">
    <button>Child button</button>
</div>
```

The handler runs only when the click originates on the `<div>` itself, not when it originates on a descendant and bubbles upward.

### 6.4.3 Combining Modifiers

```html
<form @submit.prevent.stop="submitForm">
    <button type="submit">Submit</button>
</form>
```

Modifiers can be chained when multiple behaviors are required.

<br>

## 6.5 Key Takeaways

### 6.5.1 Remember

- `.stop` stops propagation.
- `.prevent` prevents default behavior.
- `.once` limits handling to one occurrence.
- `.self` restricts handling to events originating on the element itself.

<br>

---

<br>

# 7. Keyboard Events

## 7.1 What are Keyboard Events?

### 7.1.1 Definition

Keyboard events occur when a user interacts with the keyboard.

They are useful for search fields, shortcuts, form interactions, and keyboard-controlled games.

### 7.1.2 Listening for Keyboard Events

```html
<input @keyup="handleKeyup">
```

The handler runs when a key is released while the input is focused.

### 7.1.3 Common Keyboard Events

| Event | When it occurs |
|---|---|
| `keydown` | A key is pressed down |
| `keyup` | A key is released |

`keydown` is useful when an action should respond as the key is pressed. `keyup` is useful when responding after the key is released.

<br>

## 7.2 Key Modifiers

### 7.2.1 Using a Specific Key

Vue provides key modifiers for common keys.

```html
<input @keyup.enter="submitForm">
```

The handler runs when Enter is released while the input is focused.

### 7.2.2 Common Key Modifiers

```html
<input @keyup.enter="submitForm">
<input @keyup.esc="cancelAction">
<input @keyup.space="performAction">
```

These examples demonstrate Enter, Escape, and Space key handling.

### 7.2.3 Other Key Modifiers

Vue also supports modifiers such as:

- `.tab`
- `.delete` — covers Delete and Backspace
- `.up`
- `.down`
- `.left`
- `.right`

Key modifiers make keyboard handling easier to express directly in templates.

<br>

## 7.3 Practical Example: Search Input

### 7.3.1 Template

```html
<div id="app">
    <input
        v-model="searchText"
        @keyup.enter="search"
        placeholder="Type and press Enter"
    >
</div>
```

### 7.3.2 Vue 2 Logic

```js
new Vue({
    el: "#app",
    data: {
        searchText: ""
    },
    methods: {
        search() {
            console.log("Searching for:", this.searchText);
        }
    }
});
```

### 7.3.3 Execution Flow

```text
User types in the input
        |
        v
v-model updates searchText
        |
        v
User presses and releases Enter
        |
        v
Vue invokes search()
        |
        v
The current searchText is processed
```

<br>

## 7.4 Key Takeaways

### 7.4.1 Remember

- `@keydown` handles key-down events.
- `@keyup` handles key-up events.
- `@keyup.enter` handles Enter-key releases.
- Keyboard modifiers are useful for shortcuts and form interactions.

<br>

---

<br>

# 8. Two-Way Data Binding

## 8.1 What is Two-Way Data Binding?

### 8.1.1 Definition

Two-way data binding synchronizes a form control's value with a data property.

It allows:

1. The data value to determine the displayed input value.
2. User input to update the data value.

Vue provides the `v-model` directive for this purpose.

### 8.1.2 Basic Syntax

```html
<input v-model="name">
<p>{{ name }}</p>
```

When the user types into the input, `name` updates and the paragraph displays the new value.

### 8.1.3 Vue 2 Example

```html
<div id="app">
    <input v-model="name">
    <p>Hello {{ name }}</p>
</div>

<script>
    new Vue({
        el: "#app",
        data: {
            name: ""
        }
    });
</script>
```

As the user types, the displayed greeting changes.

<br>

## 8.2 How `v-model` Works

### 8.2.1 Data to Input

Suppose the data initially contains:

```js
data: {
    name: "Alex"
}
```

The input is initialized with `"Alex"`.

### 8.2.2 Input to Data

When the user changes the input, Vue updates the corresponding data property.

```text
User types into input
        |
        v
Input event updates name
        |
        v
Reactive data changes
        |
        v
Other UI using name updates
```

### 8.2.3 Conceptual Equivalent

For a basic text input in Vue 2, `v-model` is conceptually similar to combining a value binding with an input event handler.

```html
<input
    :value="name"
    @input="name = $event.target.value"
>
```

This illustrates the underlying relationship between the input and the data. `v-model` is shorter and more convenient.

<br>

## 8.3 `v-model` with Different Inputs

### 8.3.1 Text Input

```html
<input v-model="username">
<p>{{ username }}</p>
```

The input and the `username` property stay synchronized.

### 8.3.2 Textarea

```html
<textarea v-model="description"></textarea>
<p>{{ description }}</p>
```

Useful for longer text.

### 8.3.3 Checkbox

```html
<input type="checkbox" v-model="isSubscribed">
```

For a single checkbox, the model commonly represents a Boolean value.

```js
data: {
    isSubscribed: false
}
```

### 8.3.4 Select Box

```html
<select v-model="selectedCity">
    <option disabled value="">Choose a city</option>
    <option value="Pune">Pune</option>
    <option value="Mumbai">Mumbai</option>
</select>
```

The selected option is synchronized with `selectedCity`.

<br>

## 8.4 Important `v-model` Modifiers

### 8.4.1 `.lazy`

```html
<input v-model.lazy="name">
```

For text inputs, synchronization occurs on the `change` event rather than on every `input` event.

### 8.4.2 `.number`

```html
<input v-model.number="age" type="number">
```

Vue attempts to convert the input value to a number. If conversion is not possible, the original value may be retained.

### 8.4.3 `.trim`

```html
<input v-model.trim="name">
```

Removes leading and trailing whitespace from the input value.

<br>

## 8.5 One-Way Binding vs Two-Way Binding

### 8.5.1 One-Way Binding

```html
<p>{{ name }}</p>
<input :value="name">
```

The data determines the displayed value, but the `:value` binding alone does not update `name` when the user types.

### 8.5.2 Two-Way Binding

```html
<input v-model="name">
```

The model synchronizes the value in both directions.

### 8.5.3 Comparison

| Feature | `:value` | `v-model` |
|---|---|---|
| Displays the data value | Yes | Yes |
| Updates the data from input | Not by itself | Yes |
| Requires a separate input handler for synchronization | Yes | No |

<br>

## 8.6 Key Takeaways

### 8.6.1 Remember

- `v-model` is mainly used for form input binding.
- It synchronizes form control values with data.
- It works with text inputs, textareas, checkboxes, and select boxes.
- `.lazy`, `.number`, and `.trim` modify input synchronization or processing behavior.

<br>

---

<br>

# 9. Part 1 — Quick Revision

## 9.1 Essential Syntax

### 9.1.1 Vue Fundamentals Cheat Sheet

| Syntax | Purpose |
|---|---|
| `new Vue({...})` | Creates a Vue 2 instance |
| `data: {...}` | Defines data |
| `methods: {...}` | Defines methods |
| `{{ message }}` | Displays a value |
| `v-bind:href="url"` | Binds an attribute |
| `:href="url"` | Shorthand for `v-bind` |
| `v-on:click="method"` | Handles an event |
| `@click="method"` | Shorthand for `v-on` |
| `@click.stop` | Stops event propagation |
| `@submit.prevent` | Prevents default form submission |
| `@keyup.enter` | Handles Enter-key release |
| `v-model="name"` | Two-way form binding |

<br>

## 9.2 Fundamental Mental Model

### 9.2.1 How Vue Connects Data and the UI

```text
Vue instance
     |
     v
Reactive data + methods
     |
     v
Template bindings + event handlers
     |
     v
Rendered UI
     |
     v
User interaction
     |
     v
Data changes
     |
     v
Vue updates the relevant UI
```

<br>

## 9.3 Self-Assessment

### 9.3.1 Questions to Test Your Understanding

1. What problem does Vue's reactivity solve?
2. What is the purpose of `el` in Vue 2?
3. What is the difference between `data` and `methods`?
4. What is the difference between `{{ message }}` and `:href="url"`?
5. What is the difference between `v-on:click` and `@click`?
6. How do `.stop` and `.prevent` differ?
7. What is the difference between `keydown` and `keyup`?
8. How does `v-model` differ from `:value`?
9. Why does `this.count` work in a normal Vue 2 Options API method?
10. What replaces the `new Vue()` root-application pattern in Vue 3?

### 9.3.2 Practical Exercise

Build a small interactive profile page with:

- A name input using `v-model`.
- A live greeting using interpolation.
- A counter with Increase and Reset buttons.
- A button that changes a message.
- A form that prevents the browser's default submission.
- A keyboard handler that responds to Enter.

**Completion criterion:** You should be able to explain how each binding or event handler works, not just reproduce the syntax.

<br>

---

<br>

# 10. Vue 2 to Vue 3 — Essential Mapping

## 10.1 Syntax Differences

### 10.1.1 Basic Instance and State

Vue 2:

```js
new Vue({
    el: "#app",
    data: {
        count: 0
    },
    methods: {
        increase() {
            this.count++;
        }
    }
});
```

Vue 3 with the Options API:

```js
Vue.createApp({
    data() {
        return {
            count: 0
        };
    },
    methods: {
        increase() {
            this.count++;
        }
    }
}).mount("#app");
```

### 10.1.2 Vue 3 with Composition API

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

### 10.1.3 What to Remember

- Vue 2 and Vue 3 both support templates, event directives, and `v-model`.
- Vue 3 supports both the Options API and Composition API.
- With Composition API, `ref()` is commonly used for reactive state.
- A ref uses `.value` in JavaScript, but top-level refs are automatically unwrapped in templates.
- Learn the original Vue 2 syntax to understand the course, while practicing modern Vue 3 syntax for new projects.

<br>

---

<br>


**Next part:** Part 2 — Reactivity & Rendering (Lessons 9–14): computed properties, dynamic CSS classes, conditional rendering, `v-for`, the punchbag game, and multiple Vue instances.
