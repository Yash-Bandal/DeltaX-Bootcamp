# Vuetify 3 — Reusable Setup & Implementation Guide

## Table of Contents

1. [What Is Vuetify 3?](#1-what-is-vuetify-3)
2. [Understand the Setup](#2-understand-the-setup)
3. [Create a Vue 3 Project and Install Vuetify](#3-create-a-vue-3-project-and-install-vuetify)
4. [Understand the Project Structure](#4-understand-the-project-structure)
5. [Register Vuetify in Vue 3](#5-register-vuetify-in-vue-3)
6. [Build Your First Page](#6-build-your-first-page)
7. [Understand the Grid System](#7-understand-the-grid-system)
8. [Use Buttons and Icons](#8-use-buttons-and-icons)
9. [Build Forms with Validation](#9-build-forms-with-validation)
10. [Create Navigation and Layouts](#10-create-navigation-and-layouts)
11. [Display Data with Cards and Lists](#11-display-data-with-cards-and-lists)
12. [Display Data in Tables](#12-display-data-in-tables)
13. [Configure Themes and Styling](#13-configure-themes-and-styling)
14. [Use Dialogs and Snackbars](#14-use-dialogs-and-snackbars)
15. [Connect Vuetify to Pinia and APIs](#15-connect-vuetify-to-pinia-and-apis)
16. [Vue 2 Vuetify vs Vue 3 Vuetify](#16-vue-2-vuetify-vs-vue-3-vuetify)
17. [Quick Revision](#17-quick-revision)

---

## 1. What Is Vuetify 3?

Vuetify 3 is a Material Design component framework for Vue 3.

It provides ready-made UI components such as:

- Buttons
- Cards
- Forms and inputs
- Navigation bars
- Responsive grids
- Dialogs and snackbars
- Data tables
- Progress indicators

For example, instead of writing all the HTML and CSS for a button:

```html
<button class="primary-button">
  Save Movie
</button>
```

You can use a Vuetify component:

```html
<v-btn color="primary">
  Save Movie
</v-btn>
```

Vuetify provides the styling and component behavior. Vue handles your application's data and logic.

### 1.1 Understand the responsibilities

| Technology | Responsibility |
|---|---|
| Vue 3 | Reactive UI and application logic |
| Vuetify 3 | UI components and Material Design styling |
| Vue Router 4 | Navigation between pages |
| Pinia | Shared application state |
| Fetch or Axios | HTTP requests to APIs |

**Remember:** Vuetify does not replace Vue. It makes building the user interface easier.

<br>

## 2. Understand the Setup

For this guide, we use:

- Vue 3
- Vite
- Vuetify 3
- Composition API
- `<script setup>`

The application structure looks like this:

```text
Vue 3 Application
        |
        v
    Vuetify 3
        |
        +---- v-app
        +---- v-main
        +---- v-container
        +---- v-row / v-col
        +---- v-btn
        +---- v-card
        +---- v-text-field
```

The `v-` prefix identifies Vuetify components.

For example:

- `v-btn` creates a button.
- `v-card` creates a card.
- `v-container` provides a content container.
- `v-text-field` creates a styled input.

<br>

## 3. Create a Vue 3 Project and Install Vuetify

### 3.1 Create a project

Open the terminal:

```bash
npm create vite@latest my-vuetify-app -- --template vue
cd my-vuetify-app
npm install
```

This creates a Vue 3 project using Vite.

### 3.2 Install Vuetify

```bash
npm install vuetify
npm install @mdi/font
```

The `@mdi/font` package supplies Material Design Icons as a font.

### 3.3 Install the Sass dependencies

For the standard Vuetify 3 Vite setup using the `vuetify/styles` entry point and Sass-based styles, install Sass:

```bash
npm install -D sass
```

The exact setup can vary with Vuetify's version and styling approach. Follow the installation instructions for the version you're using if you choose a different setup.

### 3.4 Start the application

```bash
npm run dev
```

Vite will display a local development URL in your terminal.

**Setup checklist:**

- Vue 3 project created.
- Vuetify installed.
- Icon font installed if using Material Design Icons.
- Sass installed for the standard Sass-based setup.
- Development server running.

<br>

## 4. Understand the Project Structure

A typical project looks like this:

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
├── stores/
│   └── movieStore.js
├── App.vue
└── main.js
```

These folders are conventions. You don't need every folder in a small application.

| File or folder | Purpose |
|---|---|
| `main.js` | Creates the Vue app and registers plugins |
| `plugins/vuetify.js` | Configures Vuetify |
| `components/` | Reusable UI components |
| `views/` | Page-level components |
| `router/` | Vue Router configuration |
| `stores/` | Pinia stores |
| `App.vue` | Root component |

<br>

## 5. Register Vuetify in Vue 3

Creating a project and installing Vuetify are not enough. You must register Vuetify with your Vue application.

### 5.1 Create `src/plugins/vuetify.js`

```javascript
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import '@mdi/font/css/materialdesignicons.css'

export default createVuetify({
  icons: {
    defaultSet: 'mdi'
  },

  theme: {
    defaultTheme: 'light'
  }
})
```

This configures Vuetify and its icon set.

### 5.2 Register it in `src/main.js`

```javascript
import { createApp } from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'

createApp(App)
  .use(vuetify)
  .mount('#app')
```

### 5.3 Understand the flow

1. `createApp(App)` creates the Vue application.
2. The Vuetify plugin creates a configured Vuetify instance.
3. `.use(vuetify)` registers it with Vue.
4. `.mount('#app')` displays the application.

```text
main.js
   |
   +---- createApp(App)
   |
   +---- use(vuetify)
   |
   +---- mount('#app')
              |
              v
        App.vue renders
```

**Remember:** Register Vuetify once in `main.js`, then use its components throughout the application.

<br>

## 6. Build Your First Page

Open `src/App.vue`:

```vue
<script setup>
import { ref } from 'vue'

const movieTitle = ref('Inception')

function changeTitle() {
  movieTitle.value = 'Interstellar'
}
</script>

<template>
  <v-app>
    <v-main>
      <v-container>
        <v-card class="pa-4">
          <v-card-title>
            Movie Explorer
          </v-card-title>

          <v-card-text>
            Current movie: {{ movieTitle }}
          </v-card-text>

          <v-btn color="primary" @click="changeTitle">
            Change Movie
          </v-btn>
        </v-card>
      </v-container>
    </v-main>
  </v-app>
</template>
```

### 6.1 What is happening?

- `ref('Inception')` creates reactive state.
- `movieTitle.value` updates the state in JavaScript.
- `{{ movieTitle }}` displays the value in the template.
- `@click="changeTitle"` calls the function when the button is clicked.
- Vuetify components provide the layout and appearance.

Notice that you don't need `.value` in the template. Vue unwraps refs in templates automatically.

This example combines Vue 3 reactivity with Vuetify components.

<br>

## 7. Understand the Grid System

Vuetify 3 uses a responsive grid system based on 12 columns.

The main components are:

- `v-container`: wraps the content.
- `v-row`: creates a row.
- `v-col`: creates a column.

### 7.1 Example

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

### 7.2 How does it work?

`cols="12"` means the column takes up the full 12-column width.

`md="6"` means the column takes up 6 of 12 columns from the medium breakpoint upward.

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

### 7.3 Responsive props

| Prop | Meaning |
|---|---|
| `cols` | Default column width |
| `sm` | Width from the small breakpoint |
| `md` | Width from the medium breakpoint |
| `lg` | Width from the large breakpoint |
| `xl` | Width from the extra-large breakpoint |

Example:

```html
<v-col cols="12" sm="6" lg="4">
```

This column uses 12 columns by default, 6 from the small breakpoint, and 4 from the large breakpoint.

Use the grid for movie cards, dashboards, product listings, and responsive forms.

<br>

## 8. Use Buttons and Icons

### 8.1 Buttons

```vue
<template>
  <v-container>
    <v-btn color="primary">
      Save
    </v-btn>

    <v-btn color="success">
      Submit
    </v-btn>

    <v-btn variant="outlined" color="error">
      Cancel
    </v-btn>

    <v-btn disabled>
      Disabled
    </v-btn>
  </v-container>
</template>
```

Common props:

| Prop | Purpose |
|---|---|
| `color` | Applies a theme color |
| `variant` | Controls the visual style |
| `rounded` | Rounds the button |
| `disabled` | Disables interaction |
| `block` | Expands to fill available width |
| `loading` | Displays a loading state |
| `size` | Sets the component size |

Vuetify 3 uses `variant` for button styles such as `flat`, `text`, and `outlined`. In Vuetify 2, you commonly see Boolean props such as `outlined` and `text`.

### 8.2 Handle clicks

```vue
<script setup>
function saveMovie() {
  console.log('Movie saved')
}
</script>

<template>
  <v-btn color="primary" @click="saveMovie">
    Save Movie
  </v-btn>
</template>
```

Vuetify provides the button. Vue handles the event.

### 8.3 Use icons

If Material Design Icons are configured:

```vue
<template>
  <v-btn icon="mdi-heart" />
</template>
```

You can also use:

```vue
<v-icon icon="mdi-magnify" />
```

For example, `mdi-heart` displays a heart icon and `mdi-magnify` displays a search icon.

If an icon doesn't appear, check that the icon package is installed and its CSS is imported.

<br>

## 9. Build Forms with Validation

Vuetify 3 provides form controls such as:

- `v-text-field`
- `v-textarea`
- `v-select`
- `v-checkbox`
- `v-radio-group`
- `v-switch`
- `v-form`

### 9.1 Create a movie form

```vue
<script setup>
import { ref } from 'vue'

const movie = ref({
  title: '',
  year: ''
})

const titleRules = [
  value => !!value || 'Title is required'
]

const yearRules = [
  value => !!value || 'Year is required',
  value =>
    (Number(value) >= 1888 && Number(value) <= 2100) ||
    'Enter a valid year'
]

const form = ref(null)

async function submitForm() {
  const { valid } = await form.value.validate()

  if (valid) {
    console.log('Movie saved:', movie.value)
  }
}
</script>

<template>
  <v-container>
    <v-form ref="form" @submit.prevent="submitForm">
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

      <v-btn type="submit" color="primary">
        Save Movie
      </v-btn>
    </v-form>
  </v-container>
</template>
```

### 9.2 Understand the flow

1. `ref()` creates reactive form data.
2. `v-model` connects the fields to the data.
3. `v-form` coordinates validation.
4. `form.value.validate()` checks the registered validation rules.
5. If the form is valid, the function processes the movie.

In Vue 3, `form` is a ref to the form component, so JavaScript accesses it through `form.value`.

**Important:** Client-side validation improves the user experience. Your backend should validate the data again before saving it.

<br>

## 10. Create Navigation and Layouts

A typical application can have an app bar, a navigation drawer, and a main content area.

```vue
<script setup>
import { ref } from 'vue'

const drawer = ref(false)
</script>

<template>
  <v-app>
    <v-app-bar color="primary">
      <v-app-bar-nav-icon @click="drawer = !drawer" />

      <v-toolbar-title>
        Movie Explorer
      </v-toolbar-title>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer">
      <v-list>
        <v-list-item
          title="Home"
          prepend-icon="mdi-home"
        />

        <v-list-item
          title="Movies"
          prepend-icon="mdi-movie"
        />
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <v-container>
        <h1>Welcome</h1>
      </v-container>
    </v-main>
  </v-app>
</template>
```

### Understand the components

- `v-app-bar`: top navigation bar.
- `v-app-bar-nav-icon`: button that toggles the drawer.
- `v-navigation-drawer`: side navigation.
- `v-list-item`: individual navigation item.
- `v-main`: main content area.

In this example, `drawer` controls whether the navigation drawer is open.

### Connect navigation to Vue Router

If Vue Router 4 is installed and configured:

```vue
<v-list-item
  title="Movies"
  prepend-icon="mdi-movie"
  :to="{ name: 'movies' }"
/>
```

Define a route named `movies` in your router configuration. Vuetify handles the navigation item's appearance, while Vue Router handles the actual route change.

<br>

## 11. Display Data with Cards and Lists

Use cards to display movie information, products, users, and other repeated items.

### 11.1 Example

```vue
<script setup>
import { ref } from 'vue'

const movies = ref([
  { id: 1, title: 'Inception', year: 2010 },
  { id: 2, title: 'Interstellar', year: 2014 },
  { id: 3, title: 'The Dark Knight', year: 2008 }
])
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
            <v-btn variant="text" color="primary">
              View Details
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
```

### 11.2 How it works

1. `movies` stores the movie data.
2. `v-for` repeats the card for each movie.
3. `:key` uniquely identifies each item.
4. Vue displays the title and year.
5. The grid adapts to the screen size.

This is a combination of standard Vue directives and Vuetify components.

<br>

## 12. Display Data in Tables

Vuetify 3 provides `v-data-table` through its Labs components in many Vuetify 3 versions. Check the documentation for your installed version before using it.

For versions where `VDataTable` is a Labs component, import it explicitly:

```vue
<script setup>
import { ref } from 'vue'
import { VDataTable } from 'vuetify/labs/VDataTable'

const headers = [
  { title: 'Movie', key: 'title' },
  { title: 'Year', key: 'year' }
]

const movies = ref([
  { id: 1, title: 'Inception', year: 2010 },
  { id: 2, title: 'Interstellar', year: 2014 }
])
</script>

<template>
  <v-container>
    <VDataTable
      :headers="headers"
      :items="movies"
      item-value="id"
    />
  </v-container>
</template>
```

The exact import path and maturity of the data table can vary between Vuetify 3 releases.

### Understand the properties

- `headers`: defines the table columns.
- `items`: supplies the data.
- `key`: identifies the property displayed in each column.
- `item-value`: identifies each row.

The table can support sorting, pagination, and selection depending on the component and configuration.

<br>

## 13. Configure Themes and Styling

Vuetify 3 supports a theme system for consistent colors.

### 13.1 Configure a theme

Update your `src/plugins/vuetify.js`:

```javascript
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import '@mdi/font/css/materialdesignicons.css'

export default createVuetify({
  icons: {
    defaultSet: 'mdi'
  },

  theme: {
    defaultTheme: 'light',

    themes: {
      light: {
        colors: {
          primary: '#1976D2',
          secondary: '#424242',
          success: '#4CAF50',
          error: '#B00020'
        }
      }
    }
  }
})
```

Keep the registration in `main.js` from Section 5.

### 13.2 Use theme colors

```vue
<template>
  <v-btn color="primary">
    Save
  </v-btn>

  <v-alert type="success">
    Movie saved successfully.
  </v-alert>
</template>
```

The `primary` color comes from the theme configuration.

### 13.3 Use spacing utilities

Vuetify includes utility classes such as:

- `pa-4`: padding on all sides.
- `px-4`: horizontal padding.
- `ma-4`: margin on all sides.
- `mt-4`: top margin.
- `mb-2`: bottom margin.

Example:

```html
<v-card class="pa-4 mb-4">
  Movie details
</v-card>
```

These classes use Vuetify's spacing scale.

<br>

## 14. Use Dialogs and Snackbars

### 14.1 Dialog

A dialog displays content in an overlay.

```vue
<script setup>
import { ref } from 'vue'

const dialog = ref(false)
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

          <v-btn variant="text" @click="dialog = false">
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

The `dialog` ref controls whether the dialog is open.

### 14.2 Snackbar

A snackbar shows temporary feedback.

```vue
<script setup>
import { ref } from 'vue'

const snackbar = ref(false)

function saveMovie() {
  snackbar.value = true
}
</script>

<template>
  <v-container>
    <v-btn color="primary" @click="saveMovie">
      Save Movie
    </v-btn>

    <v-snackbar v-model="snackbar" :timeout="3000">
      Movie saved successfully.
    </v-snackbar>
  </v-container>
</template>
```

Use:

- A **dialog** when the user needs to respond to an overlay.
- A **snackbar** when you want to display brief feedback.

<br>

## 15. Connect Vuetify to Pinia and APIs

Vuetify handles the UI. Pinia manages shared state. An API provides data from the backend.

These are separate responsibilities.

```text
Vuetify Component
       |
       v
Pinia Action
       |
       v
API Request
       |
       v
Pinia State
       |
       v
Reactive UI updates
```

For example, your movie list might call a Pinia action to fetch movies. The store manages the request and updates its state. Vuetify displays the loading indicator, errors, and movie cards.

A Vuetify button can call an action like this:

```vue
<script setup>
import { useMovieStore } from '@/stores/movieStore'

const movieStore = useMovieStore()
</script>

<template>
  <v-btn
    color="primary"
    :loading="movieStore.loading"
    @click="movieStore.fetchMovies()"
  >
    Load Movies
  </v-btn>

  <v-alert
    v-if="movieStore.error"
    type="error"
  >
    {{ movieStore.error }}
  </v-alert>
</template>
```

This assumes your Pinia store defines `loading`, `error`, and `fetchMovies`. The store and Pinia plugin must also be registered in the application.

**Remember:** Vuetify displays the state. Pinia manages the shared state. Your action handles the API request.

<br>

## 16. Vue 2 Vuetify vs Vue 3 Vuetify

| Concept | Vuetify 2 + Vue 2 | Vuetify 3 + Vue 3 |
|---|---|---|
| Vue version | Vue 2 | Vue 3 |
| UI framework | Vuetify 2 | Vuetify 3 |
| Application setup | `new Vue()` | `createApp()` |
| Common component style | Options API | Composition API or Options API |
| Modern component syntax | Options API commonly used | `<script setup>` supported |
| State example | `data()` | `ref()` / `reactive()` |
| Button outline style | `outlined` | `variant="outlined"` |
| Icon example | `<v-icon>mdi-heart</v-icon>` | `<v-icon icon="mdi-heart" />` |
| Router pairing | Vue Router 3 | Vue Router 4 |
| Store pairing | Vuex 3 commonly | Pinia commonly |

The component names are often similar, but props, defaults, imports, and some behaviors differ. Do not copy every Vuetify 2 example directly into a Vuetify 3 project.

<br>



## 17. Quick Revision

| Question | Answer |
|---|---|
| What is Vuetify 3? | A Material Design UI framework for Vue 3. |
| What creates a Vuetify instance? | `createVuetify()` |
| Where do we register it? | `main.js`, using `.use(vuetify)` |
| What does `v-btn` do? | Creates a button. |
| What does `v-card` do? | Creates a card. |
| What do `v-row` and `v-col` do? | Create responsive layouts. |
| What does `v-model` do? | Connects a component's value to reactive state. |
| What does `v-form` do? | Coordinates form validation. |
| What does `v-dialog` do? | Displays an overlay dialog. |
| What does `v-snackbar` do? | Displays temporary feedback. |
| What does Pinia do? | Manages shared application state. |
| What does Vuetify handle? | UI components and presentation. |

### Final mental model

```text
Vue 3
  |
  +---- Reactivity and logic
  |       |
  |       +---- ref()
  |       +---- reactive()
  |       +---- computed()
  |       +---- functions
  |
  +---- Vuetify 3
  |       |
  |       +---- Layout
  |       +---- Buttons
  |       +---- Forms
  |       +---- Cards
  |       +---- Tables
  |       +---- Dialogs
  |
  +---- Pinia
  |       |
  |       +---- Shared state
  |       +---- Getters
  |       +---- Actions
  |
  +---- API
          |
          +---- Fetch data
          +---- Save data
```

