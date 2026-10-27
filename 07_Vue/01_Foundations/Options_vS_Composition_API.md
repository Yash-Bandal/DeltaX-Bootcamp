# Options API vs Composition API


<br>

---

> [!Tip]
> In short, its all about how code is `organized`

---

<br>



## 1. First, what are these APIs?

Imagine you're building your [Odyssey Study Planner](https://deepwork-odyssey.netlify.app/). You have a component that displays study sessions, tracks completed tasks, and calculates progress.

That component needs to manage three things:
- Data: What information does it store?
- Methods: What actions can it perform?
- Lifecycle: What should happen when the component loads or updates?

Vue gives you ways to define all of this. The difference is how you **organize** it.

<br>

---


<br>


## Options API — Vue 2 style

You organize code into predefined categories: `data`, `methods`, `computed`, and `mounted`.

> **Think**: organize by type of code.

<br>



## Composition API — Vue 3 style

You organize code using functions and variables, making it easier to group related logic together.

> **Think**: organize by feature or responsibility.

One important clarification: Vue 3 still supports the Options API, and Vue 2 can support some Composition API functionality through an add-on. These APIs are not exclusive to their respective versions, even though Options API is commonly taught with Vue 2 and Composition API with Vue 3.

<br>


## 2. Let's build the same component in both styles

We'll create a simple study tracker with a task counter. You can add tasks and see the remaining work.

### A. Options API (Vue 2)

This is the style you're learning in the Net Ninja Vue 2 tutorials.

```vue
<div id="app">
  <h2>Study Planner</h2>
  <p>Tasks completed: {{ completed }}</p>
  <p>Tasks remaining: {{ remaining }}</p>
  <button @click="completeTask">
    Complete a task
  </button>
</div>

<script>
new Vue({
  el: "#app",

  data: {
    completed: 0,
    totalTasks: 5
  },

  computed: {
    remaining() {
      return this.totalTasks - this.completed;
    }
  },

  methods: {
    completeTask() {
      if (this.completed < this.totalTasks) {
        this.completed++;
      }
    }
  }
});
</script>

```

Let's understand the structure:

| Option     | Purpose                                                      |
| ---------- | ------------------------------------------------------------ |
| `el`       | Connects Vue to the HTML element with ID `app`.              |
| `data`     | Stores reactive values such as `completed` and `totalTasks`. |
| `computed` | Calculates `remaining` from other reactive values.           |
| `methods`  | Defines actions, such as `completeTask()`.                   |

The key thing to notice is `this`.

```
this.completed++;
```

Here, `this` refers to the Vue component instance. Vue makes the properties returned by `data()` available on that instance.

When you click the button:

1. `completeTask()` runs.
2. `this.completed` increases by one.
3. Vue detects the state change.
4. The displayed task count updates automatically.

You don't manually modify the HTML. Vue handles that for you.


<br>


### B. Composition API (Vue 3)

Now let's build exactly the same functionality using Vue 3's Composition API.

```vue
<div id="app">
  <h2>Study Planner</h2>
  <p>Tasks completed: {{ completed }}</p>
  <p>Tasks remaining: {{ remaining }}</p>
  <button @click="completeTask">
    Complete a task
  </button>
</div>

<script type="module">
import {
  createApp,
  ref,
  computed
} from "https://unpkg.com/vue@3/dist/vue.esm-browser.js";

createApp({
  setup() {
    const completed = ref(0);
    const totalTasks = ref(5);

    const remaining = computed(() => {
      return totalTasks.value - completed.value;
    });

    function completeTask() {
      if (completed.value < totalTasks.value) {
        completed.value++;
      }
    }

    return {
      completed,
      remaining,
      completeTask
    };
  }
}).mount("#app");
</script>
```

The output and behavior are the same. But the way we write the logic has changed.

| Options API      | Composition API                 |
| ---------------- | ------------------------------- |
| `data()`         | `ref()` or `reactive()`         |
| `computed: {}`   | `computed()`                    |
| `methods: {}`    | Ordinary JavaScript functions   |
| `mounted()`      | `onMounted()` inside `setup()`  |
| `this.completed` | `completed.value` in JavaScript |

These are common equivalents, not a strict one-to-one conversion rule. The official Vue documentation explains that both styles use the same underlying reactivity system.&#x20;



<br>


---


<br>




## 3. The biggest difference: `this` vs variables

This is where you should focus your attention.

In Options API, you generally access component data through `this`.

```vue
data() {
  return {
    name: "Yash"
  };
},

methods: {
  greet() {
    console.log(this.name);
  }
}
```

In Composition API, you declare variables directly.

```vue
import { ref } from "vue";

const name = ref("Yash");

function greet() {
  console.log(name.value);
}
```

Notice the difference:

- Options API: `this.name`
- Composition API: `name.value`


<br>


## 4. Why was the Composition API introduced?

Suppose your Study Planner grows. One component now handles three features:

- Managing study tasks.
- Fetching tasks from an API.
- Tracking study progress.

With Options API, related logic can become scattered across different sections.


<br>


### Options API: organized by code type

<br>
<img width="500" alt="image" src="https://github.com/user-attachments/assets/b39001ec-1315-4774-8f30-184b9e364eef" />
<br>

<br>


### Composition API: organized by feature

Task management

<br>


<img width="500" alt="image" src="https://github.com/user-attachments/assets/62609b0c-3ea8-411e-a8cd-328cc3dac7b1" />

<br>





## 5. Composition API and React Hooks?


| Concept                  | React                                      | Vue Composition API |
| ------------------------ | ------------------------------------------ | ------------------- |
| Reactive state           | `useState()`                               | `ref()`             |
| Derived values           | Often `useMemo()` or ordinary calculations | `computed()`        |
| Run logic after mounting | `useEffect()`                              | `onMounted()`       |
| Reusable stateful logic  | Custom hooks                               | Composables         |


<br>

---


<br>


