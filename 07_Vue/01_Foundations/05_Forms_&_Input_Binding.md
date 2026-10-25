# Part 5 — Forms & Input Binding 

Course: Net Ninja — Vue JS 2 Tutorial
Lessons: 30–32

<br>

## Table of Contents
1. [Input Binding — Creating a Blog (Part 1)](#1-input-binding--creating-a-blog-part-1)
2. [Checkbox Binding](#2-checkbox-binding)
3. [Select Box Binding](#3-select-box-binding)
4. [Quick Revision](#4-quick-revision)
5. [Vue 2 vs Vue 3](#5-vue-2-vs-vue-3)

---

## 1. Input Binding — Creating a Blog (Part 1)

### 1.1 What is input binding?

Input binding means connecting a form field to a Vue data property.

For example, when a user types a blog title, Vue can keep that title in its data.

Without Vue, we might manually read the input using JavaScript:

```js
const title = document.getElementById("title").value;
```

With Vue, we can use `v-model`.

### 1.2 Understanding `v-model`

`v-model` creates a two-way connection between a form input and Vue data.

- When the user types, the Vue data updates.
- When the Vue data changes, the input updates.

**Example — Vue 2**

```html
<div id="app">
  <label>Blog Title:</label>

  <input type="text" v-model="blogTitle">

  <p>Title: {{ blogTitle }}</p>
</div>
```

```js
new Vue({
  el: "#app",
  data: {
    blogTitle: ""
  }
});
```

### 1.3 How does it work?

Initially, `blogTitle` is an empty string.

```text
User types "My First Blog"
          ↓
      v-model
          ↓
  blogTitle gets updated
          ↓
  {{ blogTitle }} re-renders
          ↓
  Page displays "My First Blog"
```

You don't need to write an input event listener manually. Vue handles the connection.

### 1.4 Example with a blog form

Suppose you're creating a blog form with a title and content.

```html
<div id="app">
  <form @submit.prevent="createBlog">
    <label>Title:</label>
    <input type="text" v-model="title">

    <label>Content:</label>
    <textarea v-model="content"></textarea>

    <button type="submit">Create Blog</button>
  </form>

  <h3>{{ title }}</h3>
  <p>{{ content }}</p>
</div>
```

```js
new Vue({
  el: "#app",
  data: {
    title: "",
    content: ""
  },
  methods: {
    createBlog() {
      console.log(this.title);
      console.log(this.content);
    }
  }
});
```

### 1.5 Understanding the form submission

Look at this line:

```html
<form @submit.prevent="createBlog">
```

- `@submit` listens for the form's submit event.
- `.prevent` stops the browser's default form submission, which normally causes a page reload.
- `createBlog` runs when the form is submitted.

The `createBlog()` method reads the latest values from `this.title` and `this.content`.

**Important:** This example only reads the form data. It does not save a blog to a database. For that, you would send the data to a backend API.

### 1.6 Useful `v-model` modifiers

Vue provides modifiers to adjust how input values are handled.

| Modifier | Purpose | Example |
|---|---|---|
| `.lazy` | Updates on the `change` event instead of every input event | `v-model.lazy="title"` |
| `.number` | Attempts to convert the value to a number | `v-model.number="age"` |
| `.trim` | Removes leading and trailing whitespace | `v-model.trim="name"` |

Example:

```html
<input v-model.trim="name">
<input v-model.number="age">
```

Use `.number` when you need numeric values, such as an age or quantity. Remember that conversion is an attempt; an empty or non-numeric value may still produce a string.

<br>

---

## 2. Checkbox Binding

### 2.1 Why do we need checkbox binding?

Checkboxes let users select or confirm options.

Examples:
- Accepting terms and conditions.
- Selecting multiple blog categories.
- Choosing skills in a registration form.

Vue can connect checkbox selections to data using `v-model`.

### 2.2 Binding a single checkbox

A single checkbox usually represents a Boolean value: `true` or `false`.

```html
<div id="app">
  <label>
    <input type="checkbox" v-model="acceptedTerms">
    I accept the terms
  </label>

  <p>Accepted: {{ acceptedTerms }}</p>
</div>
```

```js
new Vue({
  el: "#app",
  data: {
    acceptedTerms: false
  }
});
```

**What happens?**

```text
Checkbox unchecked → acceptedTerms = false
Checkbox checked   → acceptedTerms = true
```

This is useful when you need to verify whether a user has accepted something before allowing a form submission.

### 2.3 Binding multiple checkboxes

What if a user can select multiple blog categories?

For example:
- Technology
- Education
- Travel

In this case, bind the checkboxes to an array.

```html
<div id="app">
  <p>Select categories:</p>

  <label>
    <input type="checkbox"
           value="Technology"
           v-model="categories">
    Technology
  </label>

  <label>
    <input type="checkbox"
           value="Education"
           v-model="categories">
    Education
  </label>

  <label>
    <input type="checkbox"
           value="Travel"
           v-model="categories">
    Travel
  </label>

  <p>Selected: {{ categories }}</p>
</div>
```

```js
new Vue({
  el: "#app",
  data: {
    categories: []
  }
});
```

### 2.4 How does the array update?

Suppose the user selects Technology and Travel.

```text
Initial state:
categories = []

Select Technology:
categories = ["Technology"]

Select Travel:
categories = ["Technology", "Travel"]

Uncheck Technology:
categories = ["Travel"]
```

Vue manages the array as checkboxes are selected and deselected.

**Remember:**

- One checkbox → commonly a Boolean.
- Multiple checkboxes sharing one `v-model` → commonly an array.

The array approach is useful when submitting multiple selected values to an API.

<br>

---

## 3. Select Box Binding

### 3.1 What is a select box?

A select box, created with the HTML `<select>` element, lets a user choose from a list of options.

For example, a blog form might let the user select a category from a dropdown.

Vue uses `v-model` to keep the selected option connected to data.

### 3.2 Binding a single select box

```html
<div id="app">
  <label>Choose a category:</label>

  <select v-model="selectedCategory">
    <option disabled value="">Select a category</option>
    <option value="Technology">Technology</option>
    <option value="Education">Education</option>
    <option value="Travel">Travel</option>
  </select>

  <p>Selected category: {{ selectedCategory }}</p>
</div>
```

```js
new Vue({
  el: "#app",
  data: {
    selectedCategory: ""
  }
});
```

Initially, `selectedCategory` is an empty string.

When the user chooses Technology:

```js
selectedCategory = "Technology";
```

The paragraph updates automatically.

### 3.3 Binding multiple selections

Add the `multiple` attribute to let users select more than one option.

```html
<select v-model="selectedCategories" multiple>
  <option value="Technology">Technology</option>
  <option value="Education">Education</option>
  <option value="Travel">Travel</option>
</select>

<p>{{ selectedCategories }}</p>
```

```js
data: {
  selectedCategories: []
}
```

Now `selectedCategories` is an array of selected values.

For example:

```js
["Technology", "Travel"]
```

### 3.4 Using non-string values

By default, HTML option values are strings. Vue also allows you to bind JavaScript values with `:value`.

```html
<select v-model="selectedRating">
  <option :value="1">Poor</option>
  <option :value="2">Average</option>
  <option :value="3">Excellent</option>
</select>
```

```js
data: {
  selectedRating: 1
}
```

Here, the selected value is a number, not the string `"1"`.

This matters when your backend expects numeric IDs or values.

### 3.5 Checkbox vs select box

| Feature | Checkbox | Select box |
|---|---|---|
| HTML element | `<input type="checkbox">` | `<select>` |
| One selection | Boolean or selected value | Usually one value |
| Multiple selections | Array | Array when `multiple` is used |
| Typical use | Terms, preferences, skills | Country, category, status |

<br>

---

## 4. Quick Revision

### 4.1 Key concepts

- `v-model` connects form inputs to Vue data.
- `@submit.prevent` handles form submission without a page reload.
- A single checkbox commonly binds to a Boolean.
- Multiple checkboxes sharing a model can bind to an array.
- A single select box usually binds to one value.
- A multiple select box binds to an array.
- `:value` allows an option to use a non-string JavaScript value.

### 4.2 Interview-style questions

**Q1. What is `v-model`?**

It creates a two-way connection between a form input and a Vue data property.

**Q2. Why use `@submit.prevent`?**

To handle form submission in Vue while preventing the browser's default page reload.

**Q3. How do you bind multiple checkboxes?**

Bind them to the same array property using `v-model`.

**Q4. What is the difference between `v-model` and `{{ }}`?**

`v-model` connects an input to data. Interpolation (`{{ }}`) displays data in the page.

**Q5. Does `v-model` save form data to a database?**

No. It manages the connection between the input and Vue state. Saving data requires a separate operation, such as an API request.

<br>

---

## 5. Vue 2 vs Vue 3

### 5.1 Vue 2 — Options API

The examples in this part use the Vue 2 style:

```js
new Vue({
  el: "#app",
  data: {
    title: ""
  }
});
```

### 5.2 Vue 3 — Composition API with `<script setup>`

The same form binding works in Vue 3.

```vue
<script setup>
import { ref } from "vue";

const title = ref("");
const acceptedTerms = ref(false);
const categories = ref([]);

function createBlog() {
  console.log(title.value);
  console.log(acceptedTerms.value);
  console.log(categories.value);
}
</script>

<template>
  <form @submit.prevent="createBlog">
    <input v-model="title" placeholder="Blog title">

    <label>
      <input type="checkbox" v-model="acceptedTerms">
      Accept terms
    </label>

    <label>
      <input type="checkbox" value="Technology" v-model="categories">
      Technology
    </label>

    <label>
      <input type="checkbox" value="Travel" v-model="categories">
      Travel
    </label>

    <button type="submit">Create Blog</button>
  </form>

  <p>{{ title }}</p>
  <p>{{ categories }}</p>
</template>
```

### 5.3 The main difference

The template syntax for `v-model` is almost identical in Vue 2 and Vue 3.

The main difference in this example is how state is declared and accessed:

- **Vue 2 Options API:** Declare properties inside `data`.
- **Vue 3 Composition API:** Declare reactive values using `ref()`.
- In Vue 3 JavaScript code, access a ref using `.value`.
- In the Vue template, Vue automatically unwraps refs, so you write `v-model="title"` rather than `v-model="title.value"`.

**Final takeaway:** `v-model` removes the need to manually read every input field. Vue keeps the form and its state connected, so you can focus on validation, submission, and saving the data.

