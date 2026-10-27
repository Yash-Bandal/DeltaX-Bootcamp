# Vuetify 2 — Reusable Setup & Implementation Guide

## Table of Contents

1. [What Is Vuetify?](#1-what-is-vuetify)
2. [Understand the Setup](#2-understand-the-setup)
3. [Install Vuetify 2 in Vue 2](#3-install-vuetify-2-in-vue-2)
4. [Understand the Project Structure](#4-understand-the-project-structure)
5. [Use Vuetify Components](#5-use-vuetify-components)
6. [Understand the Grid System](#6-understand-the-grid-system)
7. [Work with Buttons and Icons](#7-work-with-buttons-and-icons)
8. [Build Forms with Validation](#8-build-forms-with-validation)
9. [Create Navigation and App Layouts](#9-create-navigation-and-app-layouts)
10. [Display Data with Lists and Cards](#10-display-data-with-lists-and-cards)
11. [Use Tables](#11-use-tables)
12. [Apply Themes and Styling](#12-apply-themes-and-styling)
13. [Understand Dialogs and Snackbars](#13-understand-dialogs-and-snackbars)
14. [Quick Revision](#14-quick-revision)

---

## 1. What Is Vuetify?

Vuetify is a **Material Design component framework for Vue.js**. It provides ready-made UI components such as buttons, cards, forms, navigation bars, dialogs, tables, and grids.

Without Vuetify, you might build a button using HTML and style it with CSS.

```html
<button class="primary-button">
  Save
</button>
```

With Vuetify, you can use a prebuilt component:

```html
<v-btn color="primary">
  Save
</v-btn>
```

Vuetify handles much of the component's appearance, spacing, interaction, and responsive behavior.

### 1.1 Why use Vuetify?

- Build interfaces faster.
- Use consistent UI components across the application.
- Create responsive layouts.
- Reduce repetitive CSS.
- Use built-in patterns for forms, navigation, tables, and dialogs.

### 1.2 Important distinction

| Technology | Main responsibility |
|---|---|
| Vue 2 | Application logic and reactive UI |
| Vuetify 2 | Prebuilt UI components and Material Design styling |
| Vue Router 3 | Navigation between pages |
| Vuex | Shared application state |

Vuetify does not replace Vue. It works on top of Vue.

<br>

## 2. Understand the Setup

For this guide, use the following combination:

- Vue 2
- Vuetify 2
- Vue Router 3, if your application needs routing
- Vuex 3, if your application needs shared state

Vuetify 2 is intended for Vue 2. Do not install Vuetify 3 into a Vue 2 project.

The usual component structure looks like this:

```text
Vue Application
      |
      v
Vuetify Components
      |
      +---- v-app
      +---- v-container
      +---- v-row / v-col
      +---- v-btn
      +---- v-card
      +---- v-text-field
```

The `v-` prefix identifies Vuetify components. For example, `v-btn` is a Vuetify button and `v-card` is a Vuetify card.

<br>

## 3. Install Vuetify 2 in Vue 2

### 3.1 Create a Vue 2 project (If not created)

If you already have a Vue 2 project, skip this step.

Using Vue CLI:

```bash
npm install -g @vue/cli
vue create my-app
```

Choose the Vue 2 version in the project setup. The available prompts depend on your Vue CLI version.

Move into the project:

```bash
cd my-app
```

### 3.2 Install Vuetify 2

```bash
vue add vuetify
```

Follow the prompts and choose the Vuetify 2-compatible setup.

Alternatively, if you are configuring an existing Vue 2 project manually:

```bash
npm install vuetify@2
```

Manual installation also requires registering Vuetify and loading its styles. The Vue CLI plugin can automate much of this setup.

<br>

> [!Note]
> | Command | 	What it does |
> | --- | --- | 
> | `vue add vuetify`	| Installs the Vue CLI Vuetify plugin and runs its setup generator. |
> | `npm install vuetify@2` |	Installs the Vuetify 2 package. It does not automatically configure your application.  |

<br>



### 3.3 Register Vuetify

A typical Vue 2 setup in `src/main.js` looks like this:

```javascript
import Vue from 'vue'
import Vuetify from 'vuetify/lib'
import 'vuetify/dist/vuetify.min.css'

import App from './App.vue'

Vue.use(Vuetify)

const vuetify = new Vuetify({})

new Vue({
  vuetify,
  render: h => h(App)
}).$mount('#app')
```

The exact imports can differ depending on the Vue CLI plugin configuration. If the plugin has already generated `src/plugins/vuetify.js`, use that configuration rather than registering Vuetify twice.

### 3.4 Start the application

```bash
npm run serve
```

**Setup checklist:**

- Vue 2 project created.
- Vuetify 2 installed.
- Vuetify registered with Vue.
- Vuetify CSS loaded.
- Development server running.

<br>

## 4. Understand the Project Structure

A typical project might look like this:

```text
src/
├── components/
│   ├── MovieCard.vue
│   └── MovieForm.vue
├── views/
│   ├── Home.vue
│   └── Movies.vue
├── plugins/
│   └── vuetify.js
├── router/
│   └── index.js
├── store/
│   └── index.js
├── App.vue
└── main.js
```

The `views`, `router`, and `store` folders are common conventions, not mandatory Vuetify folders.

| File or folder | Purpose |
|---|---|
| `main.js` | Starts Vue and registers plugins |
| `plugins/vuetify.js` | Configures Vuetify when using a separate plugin file |
| `components/` | Reusable UI components |
| `views/` | Page-level components |
| `router/` | Route definitions |
| `store/` | Vuex store, if needed |
| `App.vue` | Root component |

<br>

## 5. Use Vuetify Components

Vuetify provides components that you use inside Vue templates.

### 5.1 The `v-app` root component

In a standard Vuetify application, wrap the UI in `v-app`.

```vue
<template>
  <v-app>
    <v-main>
      <v-container>
        <h1>My Application</h1>
      </v-container>
    </v-main>
  </v-app>
</template>
```

- `v-app` provides the main application context for Vuetify.
- `v-main` provides the main content area.
- `v-container` controls content width and horizontal spacing.

### 5.2 Common components

| Component | Purpose |
|---|---|
| `v-app` | Root application wrapper |
| `v-main` | Main content region |
| `v-container` | Page container |
| `v-row` | Horizontal grid row |
| `v-col` | Grid column |
| `v-btn` | Button |
| `v-card` | Content card |
| `v-text-field` | Text input |
| `v-select` | Dropdown selection |
| `v-checkbox` | Checkbox |
| `v-list` | List of items |
| `v-dialog` | Modal dialog |
| `v-data-table` | Data table |
| `v-alert` | Alert message |
| `v-snackbar` | Temporary notification |

### 5.3 Example: Build a simple page

```vue
<template>
  <v-app>
    <v-main>
      <v-container>
        <v-card class="pa-4">
          <v-card-title>
            Movie Explorer
          </v-card-title>

          <v-card-text>
            Explore and manage your favourite movies.
          </v-card-text>

          <v-card-actions>
            <v-btn color="primary">
              View Movies
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-container>
    </v-main>
  </v-app>
</template>
```

Here:

- `v-card` creates the card surface.
- `v-card-title` displays the title.
- `v-card-text` holds the main content.
- `v-card-actions` groups actions.
- `pa-4` adds padding using Vuetify's spacing utility classes.

You do not need to write CSS for every part of this card.

<br>

## 6. Understand the Grid System

Vuetify 2 uses a 12-column grid system.

The main components are:

- `v-container`: wraps the layout.
- `v-row`: creates a row.
- `v-col`: occupies part of the row.

### 6.1 Example

```vue
<template>
  <v-container>
    <v-row>
      <v-col cols="12" md="6">
        <v-card class="pa-4">
          Left section
        </v-card>
      </v-col>

      <v-col cols="12" md="6">
        <v-card class="pa-4">
          Right section
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
```

### 6.2 How it works

`cols="12"` means the column uses all 12 grid columns.

`md="6"` means that on medium-sized screens and above, the column uses 6 of the 12 columns.

```text
Large screen:
+-------------------+-------------------+
|      cols: 6      |      cols: 6      |
+-------------------+-------------------+

Small screen:
+---------------------------------------+
|              cols: 12                |
+---------------------------------------+
+---------------------------------------+
|              cols: 12                |
+---------------------------------------+
```

### 6.3 Responsive breakpoint props

| Prop | Applies from |
|---|---|
| `xs` | Extra-small screens |
| `sm` | Small screens |
| `md` | Medium screens |
| `lg` | Large screens |
| `xl` | Extra-large screens |

For example:

```html
<v-col cols="12" sm="6" lg="4">
```

This column uses:

- 12 columns by default.
- 6 columns from the `sm` breakpoint.
- 4 columns from the `lg` breakpoint.

Use the grid system when building responsive dashboards, movie cards, product listings, and forms.

<br>

## 7. Work with Buttons and Icons

### 7.1 Buttons

```vue
<template>
  <v-container>
    <v-btn color="primary">
      Save
    </v-btn>

    <v-btn color="success">
      Submit
    </v-btn>

    <v-btn color="error" outlined>
      Cancel
    </v-btn>

    <v-btn disabled>
      Disabled
    </v-btn>
  </v-container>
</template>
```

Common button props include:

| Prop | Purpose |
|---|---|
| `color` | Applies a theme color |
| `outlined` | Displays an outlined style |
| `text` | Uses a text-style button |
| `rounded` | Gives rounded corners |
| `disabled` | Disables interaction |
| `block` | Expands the button to fill available width |
| `loading` | Displays a loading indicator |

### 7.2 Handle clicks

```vue
<script>
export default {
  methods: {
    saveMovie() {
      console.log('Movie saved')
    }
  }
}
</script>

<template>
  <v-btn color="primary" @click="saveMovie">
    Save Movie
  </v-btn>
</template>
```

Vuetify handles the button's appearance. Vue handles the click behavior.

### 7.3 Icons

Vuetify 2 commonly uses Material Design Icons.

Depending on your installation, configure an icon font such as Material Design Icons and use:

```vue
<v-btn icon>
  <v-icon>mdi-heart</v-icon>
</v-btn>
```

The icon name `mdi-heart` comes from Material Design Icons. If the icon does not appear, check that the icon font or package is installed and configured.

<br>

## 8. Build Forms with Validation

Vuetify provides form controls with built-in styling and validation support.

### 8.1 Common form components

| Component | Purpose |
|---|---|
| `v-text-field` | Text input |
| `v-textarea` | Multi-line input |
| `v-select` | Dropdown |
| `v-checkbox` | Boolean choice |
| `v-radio-group` | Choose one option |
| `v-switch` | Toggle a setting |
| `v-form` | Groups controls and coordinates validation |

### 8.2 Example: Movie form

```vue
<script>
export default {
  data() {
    return {
      movie: {
        title: '',
        year: ''
      },

      titleRules: [
        value => !!value || 'Title is required'
      ],

      yearRules: [
        value => !!value || 'Year is required',
        value =>
          (Number(value) >= 1888 && Number(value) <= 2100) ||
          'Enter a valid year'
      ]
    }
  },

  methods: {
    saveMovie() {
      console.log(this.movie)
    }
  }
}
</script>

<template>
  <v-container>
    <v-form ref="movieForm">
      <v-text-field
        v-model="movie.title"
        label="Movie Title"
        :rules="titleRules"
        required
      />

      <v-text-field
        v-model="movie.year"
        label="Year of Release"
        type="number"
        :rules="yearRules"
        required
      />

      <v-btn
        color="primary"
        @click="submitForm"
      >
        Save Movie
      </v-btn>
    </v-form>
  </v-container>
</template>
```

The template needs a `submitForm` method. Add it to the component:

```javascript
methods: {
  submitForm() {
    this.$refs.movieForm.validate()

    if (this.$refs.movieForm.validate()) {
      this.saveMovie()
    }
  },

  saveMovie() {
    console.log(this.movie)
  }
}
```

For a cleaner version, validate once and use the returned result:

```javascript
submitForm() {
  const isValid = this.$refs.movieForm.validate()

  if (isValid) {
    this.saveMovie()
  }
}
```

### 8.3 Understand the flow

1. `v-model` connects each field to the `movie` object.
2. The user enters a title and year.
3. The validation rules check the values.
4. `v-form.validate()` runs the registered rules.
5. If the form is valid, `saveMovie()` runs.

**Remember:** `v-model` handles data binding. Validation rules check whether the values meet your requirements.

Client-side validation improves the user experience, but the backend must validate incoming data too.

<br>

## 9. Create Navigation and App Layouts

A typical Vuetify application uses an app bar, a navigation drawer, and a main content area.

### 9.1 Example layout

```vue
<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <v-app-bar-nav-icon @click="drawer = !drawer" />

      <v-toolbar-title>
        Movie Explorer
      </v-toolbar-title>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" app>
      <v-list>
        <v-list-item link>
          <v-list-item-content>
            <v-list-item-title>
              Home
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item link>
          <v-list-item-content>
            <v-list-item-title>
              Movies
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <v-container>
        <h1>Welcome</h1>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      drawer: false
    }
  }
}
</script>
```

### 9.2 Understand the components

- `v-app-bar`: top navigation bar.
- `v-app-bar-nav-icon`: button that toggles the drawer.
- `v-navigation-drawer`: side navigation.
- `v-toolbar-title`: title in the app bar.
- `v-main`: main content region.

The `app` prop allows Vuetify layout components to coordinate their placement.

### 9.3 Connect navigation to Vue Router

If Vue Router 3 is installed, a navigation item can use a route:

```vue
<v-list-item :to="{ name: 'movies' }" link>
  <v-list-item-content>
    <v-list-item-title>
      Movies
    </v-list-item-title>
  </v-list-item-content>
</v-list-item>
```

Define a route named `movies` in your router configuration. Vuetify handles the UI; Vue Router handles navigation.

<br>

## 10. Display Data with Lists and Cards

Lists and cards are useful for displaying movies, products, users, and dashboard items.

### 10.1 Display a list dynamically

```vue
<script>
export default {
  data() {
    return {
      movies: [
        { id: 1, title: 'Inception', year: 2010 },
        { id: 2, title: 'Interstellar', year: 2014 },
        { id: 3, title: 'The Dark Knight', year: 2008 }
      ]
    }
  }
}
</script>

<template>
  <v-container>
    <v-row>
      <v-col
        v-for="movie in movies"
        :key="movie.id"
        cols="12"
        sm="6"
        md="4"
      >
        <v-card>
          <v-card-title>
            {{ movie.title }}
          </v-card-title>

          <v-card-text>
            Year: {{ movie.year }}
          </v-card-text>

          <v-card-actions>
            <v-btn text color="primary">
              View Details
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
```

### 10.2 Understand the flow

1. `movies` contains the data.
2. `v-for` repeats the card for each movie.
3. `:key` uniquely identifies each rendered item.
4. `{{ movie.title }}` and `{{ movie.year }}` display values.
5. The grid adjusts the number of columns according to screen size.

This combines standard Vue directives with Vuetify components.

<br>

## 11. Use Tables

Vuetify 2 provides `v-data-table` for structured data.

```vue
<script>
export default {
  data() {
    return {
      headers: [
        { text: 'Movie', value: 'title' },
        { text: 'Year', value: 'year' }
      ],

      movies: [
        { id: 1, title: 'Inception', year: 2010 },
        { id: 2, title: 'Interstellar', year: 2014 }
      ]
    }
  }
}
</script>

<template>
  <v-container>
    <v-data-table
      :headers="headers"
      :items="movies"
      item-key="id"
    />
  </v-container>
</template>
```

### How it works

- `headers` defines the displayed columns.
- `items` supplies the table data.
- `value` identifies which object property appears in a column.
- `item-key` identifies each row.

Vuetify's data table supports common features such as sorting and pagination. Server-side filtering or pagination requires additional application logic.

<br>

## 12. Apply Themes and Styling

Vuetify 2 has a theme system for consistent colors.

### 12.1 Configure a theme

A typical `src/plugins/vuetify.js` file looks like this:

```javascript
import Vue from 'vue'
import Vuetify from 'vuetify/lib'

Vue.use(Vuetify)

export default new Vuetify({
  theme: {
    themes: {
      light: {
        primary: '#1976D2',
        secondary: '#424242',
        accent: '#82B1FF',
        error: '#FF5252',
        success: '#4CAF50'
      }
    }
  }
})
```

Then register that configured instance in `main.js`:

```javascript
import Vue from 'vue'
import vuetify from './plugins/vuetify'
import App from './App.vue'

new Vue({
  vuetify,
  render: h => h(App)
}).$mount('#app')
```

If you used the Vuetify CLI plugin, it may have already created these files. Reuse that setup rather than creating a second instance.

### 12.2 Use theme colors

```html
<v-btn color="primary">
  Save
</v-btn>

<v-alert type="success">
  Movie saved successfully.
</v-alert>
```

The `primary` color comes from the theme configuration.

### 12.3 Spacing utility classes

Vuetify 2 includes spacing classes such as:

- `pa-4`: padding on all sides.
- `px-4`: horizontal padding.
- `ma-4`: margin on all sides.
- `mt-4`: top margin.
- `mb-2`: bottom margin.

For example:

```html
<v-card class="pa-4 mb-4">
  Movie details
</v-card>
```

The number represents a spacing step, not a literal pixel value.

<br>

## 13. Understand Dialogs and Snackbars

### 13.1 Dialog

A dialog displays content in an overlay, such as a confirmation prompt.

```vue
<script>
export default {
  data() {
    return {
      dialog: false
    }
  }
}
</script>

<template>
  <v-container>
    <v-btn color="primary" @click="dialog = true">
      Open Dialog
    </v-btn>

    <v-dialog v-model="dialog" max-width="500">
      <v-card>
        <v-card-title>
          Confirm Action
        </v-card-title>

        <v-card-text>
          Do you want to continue?
        </v-card-text>

        <v-card-actions>
          <v-spacer />

          <v-btn text @click="dialog = false">
            Cancel
          </v-btn>

          <v-btn
            color="primary"
            @click="dialog = false"
          >
            Confirm
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>
```

The `dialog` Boolean controls whether the dialog is open.

### 13.2 Snackbar

A snackbar displays a temporary message.

```vue
<script>
export default {
  data() {
    return {
      snackbar: false
    }
  },

  methods: {
    save() {
      this.snackbar = true
    }
  }
}
</script>

<template>
  <v-container>
    <v-btn color="primary" @click="save">
      Save
    </v-btn>

    <v-snackbar v-model="snackbar" :timeout="3000">
      Movie saved successfully.
    </v-snackbar>
  </v-container>
</template>
```

- A dialog is useful when the user must respond to an overlay.
- A snackbar is useful for brief feedback after an action.

<br>





## 14. Quick Revision

| Question | Answer |
|---|---|
| What is Vuetify? | A Material Design component framework for Vue. |
| Which version are we learning? | Vuetify 2 with Vue 2. |
| What does `v-btn` do? | Creates a button. |
| What does `v-card` do? | Creates a card surface. |
| What does `v-container` do? | Provides a content container. |
| What do `v-row` and `v-col` do? | Create responsive grid layouts. |
| What does `v-model` do? | Connects input values to Vue state. |
| What does `v-form` do? | Coordinates form validation. |
| What does `v-data-table` do? | Displays structured data in a table. |
| What does `v-dialog` do? | Displays an overlay dialog. |
| What does `v-snackbar` do? | Displays temporary feedback. |
| Does Vuetify replace Vue? | No. It provides UI components for Vue applications. |

### Final mental model

```text
Vue 2
  |
  +---- Application logic
  |       |
  |       +---- data
  |       +---- methods
  |       +---- computed
  |
  +---- Vuetify 2
          |
          +---- Layout
          +---- Buttons
          +---- Forms
          +---- Cards
          +---- Tables
          +---- Dialogs
          +---- Themes
```

