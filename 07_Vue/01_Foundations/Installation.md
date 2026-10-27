# Installation Guide

## 1. Prerequisites

###  Install Node.js and npm

Download Node.js from https://nodejs.org/

Verify installation:

```bash
node -v
npm -v
```


<br>



## 2. Vue 2 Setup

### 2.1 Install Vue CLI

```bash
npm install -g @vue/cli
```

### 2.2 Check Vue CLI Version

```bash
vue --version
```

### 2.3 Create a Vue 2 Project

```bash
vue create my-vue2-app
```

Choose **Manually select features** and select Vue 2.x when prompted.

### 2.4 Navigate to the Project

```bash
cd my-vue2-app
```

### 2.5 Install Dependencies

```bash
npm install
```

### 2.6 Start the Development Server

```bash
npm run serve
```

Open the local URL shown in the terminal, commonly:

`http://localhost:8080`

### 2.7 Check the Vue Version

```bash
npm list vue
```

### 2.8 Install the Final Vue 2 Release

For an existing compatible Vue 2 project:

```bash
npm install vue@2.7.16
```

<br>

---


<br>



## 3. Vue 3 Setup

### 3.1 Create a Vue 3 Project

```bash
npm create vue@latest
```

Enter a project name, such as `my-vue3-app`.

### 3.2 Select Project Features

Choose the features you need:

- TypeScript
- JSX
- Vue Router
- Pinia
- Vitest
- End-to-end testing
- ESLint
- Prettier

Beginners can select No for optional features they don't need yet.

### 3.3 Navigate to the Project

```bash
cd my-vue3-app
```

### 3.4 Install Dependencies

```bash
npm install
```

### 3.5 Start the Development Server

```bash
npm run dev
```

Open the local URL shown in the terminal, commonly:

`http://localhost:5173`

### 3.6 Check the Vue Version

```bash
npm list vue
```


<br>



## 4. Common npm Commands

### 4.1 Install Dependencies

```bash
npm install
```

### 4.2 Install a Package

```bash
npm install axios
```

### 4.3 Install a Development Dependency

```bash
npm install -D eslint
```

### 4.4 Build for Production

```bash
npm run build
```

### 4.5 Preview a Production Build

```bash
npm run preview
```

This is commonly available in Vite-based projects.

### 4.6 Run Linting

```bash
npm run lint
```

This works only if the project defines a `lint` script.

### 4.7 Clean Install Dependencies

```bash
npm ci
```

Use this when the project has a valid `package-lock.json`.


<br>


---


<br>



##  Vue 2 vs Vue 3 Commands

###  Command Comparison

| Task | Vue 2 | Vue 3 |
|---|---|---|
| Project tool | Vue CLI | create-vue |
| Install tool | `npm install -g @vue/cli` | Not required globally |
| Create project | `vue create my-vue2-app` | `npm create vue@latest` |
| Enter project | `cd my-vue2-app` | `cd my-vue3-app` |
| Install dependencies | `npm install` | `npm install` |
| Start development | `npm run serve` | `npm run dev` |
| Production build | `npm run build` | `npm run build` |
| Preview build | Depends on configuration | `npm run preview` |
| Check version | `npm list vue` | `npm list vue` |


<br>



##  Vue Project Structure

###  Common Files and Folders

```text
my-vue-app/
├── node_modules/
├── public/
├── src/
│   ├── assets/
│   ├── components/
│   ├── App.vue
│   └── main.js
├── package.json
├── package-lock.json
└── index.html
```


<br>



The exact structure can vary by project template.

##  Important Files

- `src/App.vue` — main application component.
- `src/main.js` — application entry point in many templates.
- `src/components/` — reusable Vue components.
- `package.json` — dependencies and npm scripts.
- `package-lock.json` — records resolved dependency versions.
- `node_modules/` — installed packages.
- `index.html` — HTML entry page in typical Vite projects.


<br>



##  Troubleshooting

### Node.js or npm Is Not Recognized

Install Node.js and restart your terminal.

```bash
node -v
npm -v
```

### Vue CLI Is Not Recognized

```bash
npm install -g @vue/cli
```

Restart the terminal and check:

```bash
vue --version
```

### Dependencies Are Missing

Run inside the project folder:

```bash
npm install
```

### Port Is Already in Use

Use the alternative local URL provided by the development server.

<br>


## Official Documentation

### [Vue 2](https://v2.vuejs.org/)


### [Vue CLI](https://cli.vuejs.org/)


### [Vue 3](https://vuejs.org/guide/quick-start.html)


### [Node.js](https://nodejs.org/)



