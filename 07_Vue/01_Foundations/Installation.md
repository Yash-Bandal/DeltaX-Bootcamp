# 1. Vue 2 and Vue 3 Setup

## 1.1. Prerequisites

### 1.1.1. Install Node.js and npm

Download Node.js from https://nodejs.org/

### 1.1.2. Verify Installation

```bash
node -v
npm -v
```

## 1.2. Vue 2 Setup

### 1.2.1. Install Vue CLI

```bash
npm install -g @vue/cli
```

### 1.2.2. Check Vue CLI Version

```bash
vue --version
```

### 1.2.3. Create a Vue 2 Project

```bash
vue create my-vue2-app
```

Choose **Manually select features** and select Vue 2.x when prompted.

### 1.2.4. Navigate to the Project

```bash
cd my-vue2-app
```

### 1.2.5. Install Dependencies

```bash
npm install
```

### 1.2.6. Start the Development Server

```bash
npm run serve
```

Open the local URL displayed in the terminal, commonly `http://localhost:8080`.

### 1.2.7. Check the Installed Vue Version

```bash
npm list vue
```

### 1.2.8. Install the Final Vue 2 Release

For an existing compatible Vue 2 project:

```bash
npm install vue@2.7.16
```

<br>

---

<br>

## 1.3. Vue 3 Setup

### 1.3.1. Create a Vue 3 Project

```bash
npm create vue@latest
```

Enter a project name, such as `my-vue3-app`.

### 1.3.2. Select Project Features

Choose the features you need:

- TypeScript
- JSX
- Vue Router
- Pinia
- Vitest
- End-to-end testing
- ESLint
- Prettier

Beginners can skip optional features and add them later.

### 1.3.3. Navigate to the Project

```bash
cd my-vue3-app
```

### 1.3.4. Install Dependencies

```bash
npm install
```

### 1.3.5. Start the Development Server

```bash
npm run dev
```

Open the local URL displayed in the terminal, commonly `http://localhost:5173`.

### 1.3.6. Check the Installed Vue Version

```bash
npm list vue
```

## 1.4. Common npm Commands

### 1.4.1. Install Dependencies

```bash
npm install
```

### 1.4.2. Install a Package

```bash
npm install axios
```

### 1.4.3. Install a Development Dependency

```bash
npm install -D eslint
```

### 1.4.4. Build for Production

```bash
npm run build
```

### 1.4.5. Preview a Production Build

```bash
npm run preview
```

This command is commonly available in Vite-based projects.

### 1.4.6. Run Linting

```bash
npm run lint
```

This works only if the project defines a `lint` script.

### 1.4.7. Perform a Clean Dependency Install

```bash
npm ci
```

Use this when a valid `package-lock.json` exists.

## 1.5. Vue 2 vs Vue 3 Commands

### 1.5.1. Command Comparison

| Task | Vue 2 | Vue 3 |
|---|---|---|
| Project tool | Vue CLI | create-vue |
| Install tool | `npm install -g @vue/cli` | Not required globally |
| Create project | `vue create my-vue2-app` | `npm create vue@latest` |
| Enter project | `cd my-vue2-app` | `cd my-vue3-app` |
| Install packages | `npm install` | `npm install` |
| Start server | `npm run serve` | `npm run dev` |
| Production build | `npm run build` | `npm run build` |
| Preview build | Depends on configuration | `npm run preview` |
| Check Vue version | `npm list vue` | `npm list vue` |

## 1.6. Project Structure

### 1.6.1. Common Files and Folders

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

The exact structure can vary by Vue version and project template.

### 1.6.2. Purpose of Important Files

- `src/App.vue` — main application component.
- `src/main.js` — application entry point in many templates.
- `src/components/` — reusable components.
- `package.json` — dependencies and npm scripts.
- `package-lock.json` — records the resolved dependency versions.
- `node_modules/` — installed packages.
- `index.html` — HTML entry page in typical Vite projects.

## 1.7. Troubleshooting

### 1.7.1. Node.js or npm Is Not Recognized

Install Node.js, then restart the terminal.

```bash
node -v
npm -v
```

### 1.7.2. Vue CLI Is Not Recognized

```bash
npm install -g @vue/cli
```

Reopen the terminal and verify:

```bash
vue --version
```

### 1.7.3. Dependencies Are Missing

Run this inside the project directory:

```bash
npm install
```

### 1.7.4. Port Is Already in Use

Use the alternative local URL provided by the development server.

## 1.8. Official Documentation

### 1.8.1. Vue 2

https://v2.vuejs.org/

### 1.8.2. Vue CLI

https://cli.vuejs.org/

### 1.8.3. Vue 3

https://vuejs.org/guide/quick-start.html

### 1.8.4. Node.js

https://nodejs.org/

## 1.9. Recommendation

### 1.9.1. Which Version Should You Choose?

- **Vue 2:** Use for legacy projects or courses specifically teaching Vue 2.
- **Vue 3:** Recommended for new projects.
- **Important:** Vue 2 reached end of life on December 31, 2023, so it no longer receives official security fixes.
