# Points to Remember

## Index
1. [Script tag Sytax - Compositions vs Options](#1-script)

<br>

### 1. `<script>`

## 1. The three common approaches

A. Composition API with `<script setup>`

```vue
<script setup lang="ts">
import { ref } from 'vue'

const count = ref(0)

function increment() {
  count.value++
}
</script>
```

B. Composition API with a regular `<script>`

```vue
<script lang="ts">
import { ref } from 'vue'

export default {
  setup() {
    const count = ref(0)

    function increment() {
      count.value++
    }

    return { count, increment }
  },
}
</script>
```

C. Options API

```vue
<script lang="ts">
export default {
  data() {
    return {
      count: 0,
    }
  },
  methods: {
    increment() {
      this.count++
    },
  },
}
</script>
```


| Syntax or feature                                       | API style       |
| ------------------------------------------------------- | --------------- |
| `<script setup>`                                        | Composition API |
| `setup()` inside `export default`                       | Composition API |
| `data()`, `methods`, `computed` inside `export default` | Options API     |
