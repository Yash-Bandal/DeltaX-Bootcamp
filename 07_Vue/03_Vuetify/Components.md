# Vuetify 3 — Component Cheat Sheet

> **Purpose:** Reusable UI templates for Vue 3 + Vuetify 3. Copy the component you need, replace the sample values, and connect it to your application logic.

## Table of Contents

1. [Basic Page Structure](#1-basic-page-structure)
2. [Layout Components](#2-layout-components)
3. [Buttons and Icons](#3-buttons-and-icons)
4. [Cards](#4-cards)
5. [Text Inputs and Forms](#5-text-inputs-and-forms)
6. [Selection Components](#6-selection-components)
7. [Lists, Avatars and Chips](#7-lists-avatars-and-chips)
8. [Tables](#8-tables)
9. [Navigation Components](#9-navigation-components)
10. [Dialogs and Menus](#10-dialogs-and-menus)
11. [Alerts and Notifications](#11-alerts-and-notifications)
12. [Loading and Progress](#12-loading-and-progress)
13. [Images and Dividers](#13-images-and-dividers)
14. [Tabs and Expansion Panels](#14-tabs-and-expansion-panels)
15. [Tooltips](#15-tooltips)
16. [Common Props and Styling](#16-common-props-and-styling)
17. [Complete Component Selection Guide](#17-complete-component-selection-guide)

---

## 1. Basic Page Structure

Use this as the starting template for most Vuetify pages.

```vue
<template>
  <v-app>
    <v-app-bar title="My Application" />

    <v-main>
      <v-container>
        <h1>Page Title</h1>

        <!-- Add your components here -->

      </v-container>
    </v-main>
  </v-app>
</template>
```

If `v-app` is already in your root `App.vue`, don't add another one inside every page.

### Common page layouts

<text color="secondary" size="sm">Full-width page</text>

```html
<v-container fluid>
  <!-- Content stretches across available width -->
</v-container>
```

<text color="secondary" size="sm">Centered content</text>

```html
<v-container>
  <!-- Content within the standard container width -->
</v-container>
```

<text color="secondary" size="sm">Content with spacing</text>

```html
<v-container class="py-6">
  <v-row>
    <v-col cols="12">
      Your content
    </v-col>
  </v-row>
</v-container>
```

---

## 2. Layout Components

### 2.1 Container

Use `v-container` to organize page content.

```html
<v-container>
  Content
</v-container>
```

```html
<v-container fluid>
  Full-width content
</v-container>
```

### 2.2 Responsive grid

```html
<v-container>
  <v-row>
    <v-col cols="12" md="6">
      <v-card class="pa-4">Column 1</v-card>
    </v-col>

    <v-col cols="12" md="6">
      <v-card class="pa-4">Column 2</v-card>
    </v-col>
  </v-row>
</v-container>
```

### 2.3 Three-column layout

```html
<v-row>
  <v-col cols="12" sm="6" md="4">
    Card 1
  </v-col>

  <v-col cols="12" sm="6" md="4">
    Card 2
  </v-col>

  <v-col cols="12" sm="6" md="4">
    Card 3
  </v-col>
</v-row>
```

### 2.4 Horizontal layout

```html
<v-row align="center" justify="space-between">
  <v-col cols="auto">
    <h2>Movies</h2>
  </v-col>

  <v-col cols="auto">
    <v-btn color="primary">Add Movie</v-btn>
  </v-col>
</v-row>
```

### 2.5 Flex layout

```html
<v-sheet class="d-flex align-center justify-space-between pa-4">
  <span>Movie Title</span>

  <v-btn color="primary">View</v-btn>
</v-sheet>
```

Useful utility classes:

- `d-flex` — enable flex layout.
- `align-center` — align items vertically.
- `justify-center` — center items along the main axis.
- `justify-space-between` — put space between items.
- `flex-wrap` — allow items to wrap.

---

## 3. Buttons and Icons

### 3.1 Button variants

<box gap={3}>
  <row align="center" gap={3}>
    <box width="120px">
      <text color="secondary" size="sm">Flat</text>
    </box>
    <box>
      <text color="secondary" size="sm">Default filled button</text>
      ```html
      <v-btn color="primary">Save</v-btn>
      ```
    </box>
  </row>
  <divider color="subtle" />
  <row align="center" gap={3}>
    <box width="120px">
      <text color="secondary" size="sm">Outlined</text>
    </box>
    <box>
      ```html
      <v-btn variant="outlined">Cancel</v-btn>
      ```
    </box>
  </row>
  <divider color="subtle" />
  <row align="center" gap={3}>
    <box width="120px">
      <text color="secondary" size="sm">Text</text>
    </box>
    <box>
      ```html
      <v-btn variant="text">Learn More</v-btn>
      ```
    </box>
  </row>
  <divider color="subtle" />
  <row align="center" gap={3}>
    <box width="120px">
      <text color="secondary" size="sm">Tonal</text>
    </box>
    <box>
      ```html
      <v-btn variant="tonal" color="primary">
        Explore
      </v-btn>
      ```
    </box>
  </row>
</box>

### 3.2 Button sizes

```html
<v-btn size="x-small">Small</v-btn>
<v-btn size="small">Compact</v-btn>
<v-btn size="default">Default</v-btn>
<v-btn size="large">Large</v-btn>
<v-btn size="x-large">Extra Large</v-btn>
```

### 3.3 Common button props

```html
<v-btn
  color="primary"
  variant="flat"
  rounded="lg"
  size="large"
  block
>
  Submit
</v-btn>
```

Other useful props:

```html
<v-btn disabled>Disabled</v-btn>

<v-btn loading>Saving...</v-btn>

<v-btn icon="mdi-heart" color="red" />

<v-btn prepend-icon="mdi-plus">
  Add Movie
</v-btn>

<v-btn append-icon="mdi-arrow-right">
  Continue
</v-btn>
```

### 3.4 Icons

Requires a configured icon set, such as Material Design Icons.

```html
<v-icon icon="mdi-home" />

<v-icon icon="mdi-magnify" />

<v-icon icon="mdi-heart" color="red" />

<v-icon icon="mdi-pencil" size="large" />
```

Common icon names:

| Icon name | Use |
|---|---|
| `mdi-home` | Home |
| `mdi-magnify` | Search |
| `mdi-plus` | Add |
| `mdi-pencil` | Edit |
| `mdi-delete` | Delete |
| `mdi-heart` | Favourite |
| `mdi-cog` | Settings |
| `mdi-account` | User |
| `mdi-bell` | Notifications |
| `mdi-close` | Close |

---

## 4. Cards

Cards are useful for movie listings, products, dashboards and profile summaries.

### 4.1 Basic card

```html
<v-card>
  <v-card-title>Inception</v-card-title>

  <v-card-subtitle>Released in 2010</v-card-subtitle>

  <v-card-text>
    A science-fiction thriller.
  </v-card-text>

  <v-card-actions>
    <v-btn variant="text" color="primary">
      View Details
    </v-btn>
  </v-card-actions>
</v-card>
```

### 4.2 Card with image

```html
<v-card max-width="350">
  <v-img
    src="https://placehold.co/600x350"
    height="200"
    cover
  />

  <v-card-title>Movie Title</v-card-title>

  <v-card-text>
    Short movie description.
  </v-card-text>

  <v-card-actions>
    <v-btn color="primary">Explore</v-btn>
  </v-card-actions>
</v-card>
```

Replace the placeholder image with your actual image URL.

### 4.3 Horizontal card

```html
<v-card>
  <v-row no-gutters>
    <v-col cols="4">
      <v-img
        src="https://placehold.co/300x400"
        height="100%"
        cover
      />
    </v-col>

    <v-col cols="8">
      <v-card-title>Movie Title</v-card-title>

      <v-card-text>
        Movie description goes here.
      </v-card-text>
    </v-col>
  </v-row>
</v-card>
```

### 4.4 Repeated cards

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
  <v-row>
    <v-col
      v-for="movie in movies"
      :key="movie.id"
      cols="12"
      sm="6"
      md="4"
    >
      <v-card>
        <v-card-title>{{ movie.title }}</v-card-title>
        <v-card-text>{{ movie.year }}</v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>
```

---

## 5. Text Inputs and Forms

### 5.1 Text field

```vue
<script setup>
import { ref } from 'vue'

const title = ref('')
</script>

<template>
  <v-text-field
    v-model="title"
    label="Movie Title"
    placeholder="Enter title"
    variant="outlined"
    clearable
  />
</template>
```

### 5.2 Password field

```vue
<script setup>
import { ref } from 'vue'

const password = ref('')
</script>

<template>
  <v-text-field
    v-model="password"
    label="Password"
    type="password"
    prepend-inner-icon="mdi-lock"
    variant="outlined"
  />
</template>
```

### 5.3 Search field

```html
<v-text-field
  label="Search movies"
  prepend-inner-icon="mdi-magnify"
  variant="outlined"
  clearable
  hide-details
/>
```

### 5.4 Text area

```html
<v-textarea
  label="Description"
  placeholder="Enter movie description"
  variant="outlined"
  rows="4"
  auto-grow
/>
```

### 5.5 Select dropdown

```vue
<script setup>
import { ref } from 'vue'

const genre = ref(null)

const genres = [
  'Action',
  'Comedy',
  'Drama',
  'Science Fiction'
]
</script>

<template>
  <v-select
    v-model="genre"
    :items="genres"
    label="Select genre"
    variant="outlined"
  />
</template>
```

### 5.6 Date input

For a simple browser-native date picker:

```html
<v-text-field
  label="Release date"
  type="date"
  variant="outlined"
/>
```

### 5.7 File input

```vue
<script setup>
import { ref } from 'vue'

const poster = ref(null)
</script>

<template>
  <v-file-input
    v-model="poster"
    label="Upload movie poster"
    accept="image/*"
    prepend-icon="mdi-camera"
    variant="outlined"
    show-size
  />
</template>
```

### 5.8 Complete form template

```vue
<script setup>
import { ref } from 'vue'

const form = ref(null)

const movie = ref({
  title: '',
  year: '',
  genre: '',
  description: ''
})

const rules = {
  required: value => !!value || 'This field is required'
}

async function submitForm() {
  const { valid } = await form.value.validate()

  if (!valid) return

  console.log('Form data:', movie.value)
}
</script>

<template>
  <v-form ref="form" @submit.prevent="submitForm">
    <v-text-field
      v-model="movie.title"
      label="Title"
      :rules="[rules.required]"
      variant="outlined"
    />

    <v-text-field
      v-model="movie.year"
      label="Release Year"
      type="number"
      :rules="[rules.required]"
      variant="outlined"
    />

    <v-select
      v-model="movie.genre"
      :items="['Action', 'Drama', 'Comedy']"
      label="Genre"
      :rules="[rules.required]"
      variant="outlined"
    />

    <v-textarea
      v-model="movie.description"
      label="Description"
      variant="outlined"
    />

    <v-btn type="submit" color="primary">
      Save Movie
    </v-btn>
  </v-form>
</template>
```

**Remember:** `v-model` connects a form control to your state. `v-form` and `rules` help validate the input.

---

## 6. Selection Components

### 6.1 Checkbox

```vue
<script setup>
import { ref } from 'vue'

const isFavourite = ref(false)
</script>

<template>
  <v-checkbox
    v-model="isFavourite"
    label="Add to favourites"
    color="primary"
    hide-details
  />
</template>
```

### 6.2 Radio buttons

```vue
<script setup>
import { ref } from 'vue'

const rating = ref('good')
</script>

<template>
  <v-radio-group v-model="rating" label="Your rating">
    <v-radio label="Good" value="good" />
    <v-radio label="Average" value="average" />
    <v-radio label="Poor" value="poor" />
  </v-radio-group>
</template>
```

### 6.3 Switch

```vue
<script setup>
import { ref } from 'vue'

const darkMode = ref(false)
</script>

<template>
  <v-switch
    v-model="darkMode"
    label="Enable dark mode"
    color="primary"
    hide-details
  />
</template>
```

### 6.4 Autocomplete

Use autocomplete when the user needs to search through available options.

```vue
<script setup>
import { ref } from 'vue'

const actor = ref(null)

const actors = [
  'Leonardo DiCaprio',
  'Christian Bale',
  'Anne Hathaway',
  'Cillian Murphy'
]
</script>

<template>
  <v-autocomplete
    v-model="actor"
    :items="actors"
    label="Search actor"
    variant="outlined"
    clearable
  />
</template>
```

### 6.5 Slider

```vue
<script setup>
import { ref } from 'vue'

const rating = ref(5)
</script>

<template>
  <v-slider
    v-model="rating"
    label="Rating"
    min="0"
    max="10"
    step="1"
    thumb-label
  />
  <p>Rating: {{ rating }}</p>
</template>
```

### 6.6 Chip selection

```html
<v-chip-group selected-class="text-primary">
  <v-chip>Action</v-chip>
  <v-chip>Drama</v-chip>
  <v-chip>Comedy</v-chip>
</v-chip-group>
```

For controlled single or multiple selection, bind the appropriate `v-model` and item values supported by your chosen chip-group configuration.

---

## 7. Lists, Avatars and Chips

### 7.1 Simple list

```html
<v-list>
  <v-list-item
    title="Inception"
    subtitle="2010"
    prepend-icon="mdi-movie"
  />

  <v-list-item
    title="Interstellar"
    subtitle="2014"
    prepend-icon="mdi-movie"
  />
</v-list>
```

### 7.2 Clickable list

```html
<v-list>
  <v-list-item
    title="Home"
    prepend-icon="mdi-home"
    to="/"
  />

  <v-list-item
    title="Movies"
    prepend-icon="mdi-movie"
    to="/movies"
  />

  <v-list-item
    title="Settings"
    prepend-icon="mdi-cog"
    to="/settings"
  />
</v-list>
```

The `to` prop is useful when Vue Router is installed and configured.

### 7.3 Avatar

```html
<v-avatar color="primary" size="48">
  <span class="text-white">YB</span>
</v-avatar>
```

Image avatar:

```html
<v-avatar size="48">
  <v-img src="https://placehold.co/100x100" />
</v-avatar>
```

### 7.4 Chips

```html
<v-chip color="success">Available</v-chip>

<v-chip color="primary" variant="tonal">
  Action
</v-chip>

<v-chip closable>
  Science Fiction
</v-chip>

<v-chip prepend-icon="mdi-star">
  4.8
</v-chip>
```

Use chips for categories, tags, status labels and small pieces of metadata.

---

## 8. Tables

### 8.1 Simple table

Use `v-table` when you need a basic table with your own HTML rows.

```html
<v-table>
  <thead>
    <tr>
      <th>Title</th>
      <th>Year</th>
      <th>Genre</th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>Inception</td>
      <td>2010</td>
      <td>Science Fiction</td>
    </tr>

    <tr>
      <td>Interstellar</td>
      <td>2014</td>
      <td>Science Fiction</td>
    </tr>
  </tbody>
</v-table>
```

### 8.2 Data table

For sorting, pagination and richer data-table behavior, use `VDataTable` where supported by your installed Vuetify 3 version. In versions where it is a Labs component, import it explicitly.

```vue
<script setup>
import { ref } from 'vue'
import { VDataTable } from 'vuetify/labs/VDataTable'

const headers = [
  { title: 'Title', key: 'title' },
  { title: 'Year', key: 'year' },
  { title: 'Genre', key: 'genre' }
]

const movies = ref([
  { id: 1, title: 'Inception', year: 2010, genre: 'Sci-Fi' },
  { id: 2, title: 'Interstellar', year: 2014, genre: 'Sci-Fi' }
])
</script>

<template>
  <VDataTable
    :headers="headers"
    :items="movies"
    item-value="id"
  />
</template>
```

**Note:** Check your installed Vuetify version's documentation for the exact data-table import and availability. The Labs status and APIs can change between releases.

---

## 9. Navigation Components

### 9.1 App bar

```html
<v-app-bar title="Movie Explorer">
  <template #append>
    <v-btn icon="mdi-bell" />
    <v-btn icon="mdi-account" />
  </template>
</v-app-bar>
```

### 9.2 Navigation drawer

```vue
<script setup>
import { ref } from 'vue'

const drawer = ref(true)
</script>

<template>
  <v-navigation-drawer v-model="drawer">
    <v-list>
      <v-list-item title="Home" prepend-icon="mdi-home" />
      <v-list-item title="Movies" prepend-icon="mdi-movie" />
      <v-list-item title="Settings" prepend-icon="mdi-cog" />
    </v-list>
  </v-navigation-drawer>
</template>
```

Use this inside an application that already has `v-app` and the appropriate main layout.

### 9.3 Bottom navigation

```vue
<script setup>
import { ref } from 'vue'

const activeTab = ref('home')
</script>

<template>
  <v-bottom-navigation v-model="activeTab" grow>
    <v-btn value="home">
      <v-icon icon="mdi-home" />
      <span>Home</span>
    </v-btn>

    <v-btn value="movies">
      <v-icon icon="mdi-movie" />
      <span>Movies</span>
    </v-btn>

    <v-btn value="profile">
      <v-icon icon="mdi-account" />
      <span>Profile</span>
    </v-btn>
  </v-bottom-navigation>
</template>
```

### 9.4 Tabs

```vue
<script setup>
import { ref } from 'vue'

const tab = ref('details')
</script>

<template>
  <v-tabs v-model="tab">
    <v-tab value="details">Details</v-tab>
    <v-tab value="reviews">Reviews</v-tab>
    <v-tab value="cast">Cast</v-tab>
  </v-tabs>

  <v-window v-model="tab">
    <v-window-item value="details">
      Movie details go here.
    </v-window-item>

    <v-window-item value="reviews">
      Movie reviews go here.
    </v-window-item>

    <v-window-item value="cast">
      Cast information goes here.
    </v-window-item>
  </v-window>
</template>
```

---

## 10. Dialogs and Menus

### 10.1 Dialog

Useful for confirmation, editing forms and detail popups.

```vue
<script setup>
import { ref } from 'vue'

const dialog = ref(false)
</script>

<template>
  <v-btn @click="dialog = true">
    Open Dialog
  </v-btn>

  <v-dialog v-model="dialog" max-width="500">
    <v-card>
      <v-card-title>Confirm Action</v-card-title>

      <v-card-text>
        Are you sure you want to continue?
      </v-card-text>

      <v-card-actions>
        <v-spacer />

        <v-btn variant="text" @click="dialog = false">
          Cancel
        </v-btn>

        <v-btn color="primary" @click="dialog = false">
          Confirm
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
```

### 10.2 Menu

```vue
<template>
  <v-menu>
    <template #activator="{ props }">
      <v-btn
        v-bind="props"
        icon="mdi-dots-vertical"
      />
    </template>

    <v-list>
      <v-list-item title="Edit" />
      <v-list-item title="Duplicate" />
      <v-list-item title="Delete" />
    </v-list>
  </v-menu>
</template>
```

The activator slot provides `props` that connect the button to the menu's interaction behavior. Bind them using `v-bind="props"`.

### 10.3 Dropdown menu with actions

```vue
<script setup>
function editMovie() {
  console.log('Edit movie')
}

function deleteMovie() {
  console.log('Delete movie')
}
</script>

<template>
  <v-menu>
    <template #activator="{ props }">
      <v-btn v-bind="props" variant="outlined">
        Actions
      </v-btn>
    </template>

    <v-list>
      <v-list-item
        title="Edit"
        prepend-icon="mdi-pencil"
        @click="editMovie"
      />

      <v-list-item
        title="Delete"
        prepend-icon="mdi-delete"
        @click="deleteMovie"
      />
    </v-list>
  </v-menu>
</template>
```

---

## 11. Alerts and Notifications

### 11.1 Alert

Alerts remain visible until the UI changes or they are dismissed.

```html
<v-alert type="success" title="Success">
  Movie saved successfully.
</v-alert>
```

Other types:

```html
<v-alert type="info">Information message</v-alert>

<v-alert type="warning">Please check your input.</v-alert>

<v-alert type="error">Something went wrong.</v-alert>
```

### 11.2 Snackbar

Useful for brief feedback after an action.

```vue
<script setup>
import { ref } from 'vue'

const snackbar = ref(false)

function saveMovie() {
  snackbar.value = true
}
</script>

<template>
  <v-btn color="primary" @click="saveMovie">
    Save
  </v-btn>

  <v-snackbar v-model="snackbar" :timeout="3000">
    Movie saved successfully.
  </v-snackbar>
</template>
```

### 11.3 Error message below a field

```html
<v-text-field
  label="Email"
  error
  error-messages="Please enter a valid email."
/>
```

Use `error-messages` for field-level validation feedback and `v-alert` for larger page-level messages.

---

## 12. Loading and Progress

### 12.1 Linear progress

```html
<v-progress-linear
  indeterminate
  color="primary"
/>
```

Useful when the completion percentage is unknown.

### 12.2 Determinate progress

```vue
<script setup>
import { ref } from 'vue'

const progress = ref(65)
</script>

<template>
  <v-progress-linear
    v-model="progress"
    color="success"
    height="8"
    rounded
  />

  <p>{{ progress }}% complete</p>
</template>
```

### 12.3 Circular progress

```html
<v-progress-circular
  indeterminate
  color="primary"
  size="40"
/>
```

### 12.4 Loading button

```html
<v-btn
  color="primary"
  :loading="loading"
  :disabled="loading"
>
  Save
</v-btn>
```

Define `loading` in your component state.

### 12.5 Skeleton loader

Use placeholders while content is loading.

```html
<v-skeleton-loader
  type="card"
  :loading="loading"
>
  <v-card>
    <v-card-title>Movie Loaded</v-card-title>
    <v-card-text>Movie information</v-card-text>
  </v-card>
</v-skeleton-loader>
```

---

## 13. Images and Dividers

### 13.1 Image

```html
<v-img
  src="https://placehold.co/600x350"
  height="250"
  cover
  alt="Movie poster"
/>
```

### 13.2 Image with rounded corners

```html
<v-img
  src="https://placehold.co/600x350"
  height="200"
  cover
  rounded="lg"
/>
```

### 13.3 Divider

```html
<v-divider />
```

### 13.4 Vertical divider

```html
<v-divider vertical />
```

### 13.5 Spacing between elements

```html
<v-divider class="my-4" />
```

`my-4` adds vertical margin around the divider.

---

## 14. Tabs and Expansion Panels

### 14.1 Expansion panel

Useful for FAQs, settings sections and collapsible details.

```vue
<template>
  <v-expansion-panels>
    <v-expansion-panel>
      <v-expansion-panel-title>
        Movie Details
      </v-expansion-panel-title>

      <v-expansion-panel-text>
        Release year, director and genre.
      </v-expansion-panel-text>
    </v-expansion-panel>

    <v-expansion-panel>
      <v-expansion-panel-title>
        Reviews
      </v-expansion-panel-title>

      <v-expansion-panel-text>
        User reviews appear here.
      </v-expansion-panel-text>
    </v-expansion-panel>
  </v-expansion-panels>
</template>
```

### 14.2 FAQ template

```html
<v-expansion-panels>
  <v-expansion-panel
    title="What is Vuetify?"
    text="A UI component framework for Vue."
  />

  <v-expansion-panel
    title="What is Pinia?"
    text="A state management library for Vue."
  />
</v-expansion-panels>
```

Use expansion panels when content should be available without occupying the entire page at once.

---

## 15. Tooltips

A tooltip displays a short explanation when a user hovers over or focuses on a control.

```vue
<template>
  <v-tooltip text="Edit this movie">
    <template #activator="{ props }">
      <v-btn
        v-bind="props"
        icon="mdi-pencil"
        aria-label="Edit movie"
      />
    </template>
  </v-tooltip>
</template>
```

For icon-only buttons, provide an accessible name using `aria-label` as shown above.

---

## 16. Common Props and Styling

### 16.1 Button variants

| Prop | Example |
|---|---|
| `color` | `color="primary"` |
| `variant` | `variant="outlined"` |
| `size` | `size="large"` |
| `rounded` | `rounded="lg"` |
| `disabled` | `disabled` |
| `loading` | `:loading="loading"` |
| `block` | `block` |

### 16.2 Input props

| Prop | Example |
|---|---|
| `label` | `label="Movie Title"` |
| `placeholder` | `placeholder="Enter title"` |
| `variant` | `variant="outlined"` |
| `clearable` | `clearable` |
| `disabled` | `disabled` |
| `readonly` | `readonly` |
| `rules` | `:rules="[requiredRule]"` |
| `error-messages` | `:error-messages="errors"` |

### 16.3 Spacing classes

| Class | Meaning |
|---|---|
| `pa-4` | Padding on all sides |
| `px-4` | Horizontal padding |
| `py-4` | Vertical padding |
| `ma-4` | Margin on all sides |
| `mt-4` | Top margin |
| `mb-4` | Bottom margin |
| `mx-auto` | Horizontal auto margins |

### 16.4 Text and display utilities

```html
<h2 class="text-h4">Page Heading</h2>

<p class="text-body-1">Normal body text.</p>

<p class="text-caption">Small supporting text.</p>

<div class="d-flex align-center justify-space-between">
  <span>Title</span>
  <v-btn>Action</v-btn>
</div>
```

Vuetify utility classes are convenient for common styling. Use your own CSS when you need a more specific design.

### 16.5 Bind props dynamically

```vue
<script setup>
import { ref } from 'vue'

const buttonColor = ref('primary')
const isLoading = ref(false)
</script>

<template>
  <v-btn
    :color="buttonColor"
    :loading="isLoading"
    @click="isLoading = !isLoading"
  >
    Save
  </v-btn>
</template>
```

Remember:

- `color="primary"` passes a fixed string.
- `:color="buttonColor"` binds a JavaScript value.
- `@click="..."` handles an event.
- `v-model="..."` creates two-way component binding.

---

## 17. Complete Component Selection Guide

Use this table when you are building a page and need to decide which component fits your requirement.

| Requirement | Component |
|---|---|
| Main page wrapper | `v-app` |
| Main content | `v-main` |
| Centered content | `v-container` |
| Responsive columns | `v-row`, `v-col` |
| Button | `v-btn` |
| Icon | `v-icon` |
| Card | `v-card` |
| Text input | `v-text-field` |
| Multi-line text | `v-textarea` |
| Dropdown | `v-select` |
| Searchable dropdown | `v-autocomplete` |
| Checkbox | `v-checkbox` |
| Radio selection | `v-radio-group`, `v-radio` |
| Toggle | `v-switch` |
| File upload control | `v-file-input` |
| List | `v-list`, `v-list-item` |
| Profile image or initials | `v-avatar` |
| Tag or status | `v-chip` |
| Basic table | `v-table` |
| Feature-rich data table | `v-data-table` / `VDataTable` |
| Top navigation | `v-app-bar` |
| Side navigation | `v-navigation-drawer` |
| Bottom navigation | `v-bottom-navigation` |
| Tabs | `v-tabs`, `v-tab` |
| Popup | `v-dialog` |
| Dropdown menu | `v-menu` |
| Success or error message | `v-alert` |
| Temporary notification | `v-snackbar` |
| Loading indicator | `v-progress-circular` |
| Progress bar | `v-progress-linear` |
| Loading placeholder | `v-skeleton-loader` |
| Image | `v-img` |
| Separator | `v-divider` |
| Collapsible content | `v-expansion-panels` |
| Hover help | `v-tooltip` |

