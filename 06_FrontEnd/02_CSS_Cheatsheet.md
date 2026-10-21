
<h1 align="center"> <b> CSS Cheatsheet </b> </h1>

**By YB-Studios** <br>
**Updated: 4 June 2025**

<br>

## Index

1. [Syntax](#syntax)
2. [Selectors](#selectors)
3. [Base](#base)
4. [Font, Text and Typography](#1-font--text-and-typography)\
   4.1. [Font](#a-font)\
   4.2. [Text](#b-text)
5. [Background](#2-background)
6. [Positioning & Display](#3-positioning--display)
7. [Border](#4-border)
8. [Box Model](#5-box-model)
9. [Colors](#6-colors)
10. [Template Layout](#7-template-layout)
11. [Table](#8-table)
12. [Columns](#9-columns)
13. [List & Markers](#10-list--markers)
14. [Animations](#11-animations)
15. [Transitions and Transforms](#12-transitions-and-transforms)
16. [CSS Flexbox](#13-css-flexbox)\
    16.1. [Parent Properties (Flex Container)](#a-parent-properties-flex-container)\
    16.2. [Child Properties (Flex Items)](#b-child-properties-flex-items)
17. [CSS Grid](#15-css-grid)\
    17.1. [Parent Properties (Grid Container)](#a-parent-properties-grid-container)\
    17.2. [Child Properties (Grid Items)](#b-child-properties-grid-items)
18. [CSS Variables](#css-variables)
19. [Optional but Useful to Include](#optional-but-useful-to-include)
20. [References](#references)


<br>

### More
1. [Pseudo  Classes](#1-pseudo-classes)

<br>

---

<br>

### Syntax
```
 selector {
     property: value;
} 
```

<br>

##  Selectors 
```css

element { }          /* Type selector */
.className { }       /* Class selector */
#idName { }          /* ID selector */
[attr="value"] { }   /* Attribute selector */
element:hover { }    /* Pseudo-class selector */
```


---
## Base
* **Units**
    ```
    width: 100px;
    height: 50%;
    margin: 1em;
    padding: 2rem;
    font-size: 12pt;
    line-height: 1.5;
    ```
 * **Comments**
    ```css
    /* This is a CSS comment */
    ```
---

<br>

---


## 1 Font , Text and Typography
### A. Font 

There are many properties related to the font, such as the face, weight, style, etc. These properties allow you to change the style or complete look of your text.

* **Font-Family**

  ```css
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  ```

* **Font-Style**

  ```css
  font-style: italic;
  ```

* **Font-Variant**

  ```css
  font-variant: small-caps;
  ```

* **Font-Weight**

  ```css
  font-weight: bold;
  ```

* **Font-Size**

  ```css
  font-size: larger;
  ```

* **Font**

  ```css
  font: style variant weight size family;
  ```

<br>

---

<br>

### B.  Text

Text properties allow one to manipulate alignment, spacing, decoration, indentation, etc., in the document.

* **Text-Align**

  ```css
  text-align: justify;
  ```

* **Letter-Spacing**

  ```css
  letter-spacing: .15em;
  ```

* **Text-Decoration**

  ```css
  text-decoration: underline;
  ```

* **Word-Spacing**

  ```css
  word-spacing: 0.25em;
  ```

* **Text-Transform**

  ```css
  text-transform: uppercase;
  ```

* **Text-Indent**

  ```css
  text-indent: 0.5cm;
  ```

* **Line-Height**

  ```css
  line-height: normal;
  ```

<br>

---

<br>

## 2 Background

As the name suggests, these properties are related to background, i.e., you can change the color, image, position, size, etc., of the document.

* **Background-Image**

  ```css
  background-image: url("Path");
  ```

* **Background-Position**

  ```css
  background-position: right top;
  ```

* **Background-Size**

  ```css
  background-size: cover;
  ```

* **Background-Repeat**

  ```css
  background-repeat: no-repeat;
  ```

* **Background-Attachment**

  ```css
  background-attachment: scroll;
  ```

* **Background-Color**

  ```css
  background-color: fuchsia;
  ```

* **Background**

  ```css
  background: color image repeat attachment position;
  ```

<br>

---

<br>

## 3 Positioning & Display 

* **Display**
    ```css
    display: block;
    display: inline;
    display: none;
    display: flex;
    display: grid;
    ```

* **Position**
    ```css 
    position: static;
    position: relative;
    position: absolute;
    position: fixed;
    position: sticky;
    ```

* **Offsets**
    ```css
    top: 10px;
    right: 10px;
    bottom: 10px;
    left: 10px;
    ```

* **Stacking order**
    ```css
    z-index: 10;
    ```

* **Visibility**
    ```css
    visibility: visible;
    visibility: hidden;
    ```

* **Overflow**
    ```css
    overflow: visible;
    overflow: hidden;
    overflow: scroll;
    overflow: auto;
    ```

* **Float & Clear** (We use flex instead)
    ```css
    float: left;
    float: right;
    float: none;
    ```
    ```css
    clear: left;
    clear: right;
    clear: both;
    clear: none;
    ```


<br>

---

<br>


## 4 Border

Border properties are used to change the style, radius, color, etc., of buttons or other items of the document.

* **Border-Width**

  ```css
  border-width: 5px;
  ```

* **Border-Style**

  ```css
  border-style: solid;
  ```

* **Border-Color**

  ```css
  border-color: aqua;
  ```

* **Border-Radius**

  ```css
  border-radius: 15px;
  ```

* **Border**

  ```css
  border: width style color;
  ```

<br>

---

<br>

## 5 Box Model

In laymen's terms, the CSS box model is a container that wraps around every HTML element. It consists of margins, borders, padding, and the actual content. It is used to create the design and layout of web pages.

* **Float**

  ```css
  float: none;
  ```

* **Clear**

  ```css
  clear: both;
  ```

* **Display**

  ```css
  display: block;
  ```

* **Height**

  ```css
  height: fit-content;
  ```

* **Width**

  ```css
  width: auto;
  ```

* **Margin**

  ```css
  margin: top right bottom left;
  ```

* **Padding**

  ```css
  padding: top right bottom left;
  ```

* **Overflow**

  ```css
  overflow: hidden;
  ```

* **Visibility**

  ```css
  visibility: visible;
  ```

<br>

---

<br>

## 6 Colors

With the help of the color property, one can give color to text, shape, or any other object.

* **Color**

  ```css
  color: cornsilk;
  color: #ff0000;
  color: rgb(255, 0, 0);
  color: rgba(255, 0, 0, 0.5);
  ```

* **Opacity**

  ```css
  opacity: 4;
  ```

<br>

---

<br>

## 7 Template Layout

Specifies the visual look of the content inside a template.

* **Box-Align**

  ```css
  box-align: start;
  ```

* **Box-Direction**

  ```css
  box-direction: normal;
  ```

* **Box-Flex**

  ```css
  box-flex: normal;
  ```

* **Box-Flex-Group**

  ```css
  box-flex-group: 2;
  ```

* **Box-Orient**

  ```css
  box-orient: inline;
  ```

* **Box-Pack**

  ```css
  box-pack: justify;
  ```

* **Box-Sizing**

  ```css
  box-sizing: margin-box;
  ```

* **max-width**

  ```css
  max-width: 800px;
  ```

* **min-width**

  ```css
  min-width: 500px;
  ```

* **max-height**

  ```css
  max-height: 100px;
  ```

* **min-height**

  ```css
  min-height: 80px;
  ```


<br>

---

<br>



## 8  Table

Table properties are used to give style to the tables in the document. You can change many things like border spacing, table layout, caption, etc.

* **Border-Collapse**

  ```css
  border-collapse: separate;
  ```

* **Empty-Cells**

  ```css
  empty-cells: show;
  ```

* **Border-Spacing**

  ```css
  border-spacing: 2px;
  ```

* **Table-Layout**

  ```css
  table-layout: auto;
  ```
  ```css
  table-layout: fixed;
  ```
  

* **Caption-Side**

  ```css
  caption-side: bottom;
  ```

* **Border collapse**
  ```css
  border-collapse: collapse;
  ```

<br>

---

<br>

## 9 Columns

These properties are used explicitly with columns of the tables, and they are used to give the table an incredible look.

* **Column-Count**

  ```css
  column-count: 10;
  ```

* **Column-Gap**

  ```css
  column-gap: 5px;
  ```

* **Column-rule-width**

  ```css
  column-rule-width: medium;
  ```

* **Column-rule-style**

  ```css
  column-rule-style: dotted;
  ```

* **Column-rule-color**

  ```css
  column-rule-color: black;
  ```

* **Column-width**

  ```css
  column-width: 10px;
  ```

* **Column-span**

  ```css
  column-span: all;
  ```

<br>

---

<br>

## 10  List & Markers

List and marker properties are used to customize lists in the document.

* **List-style-type**

  ```css
  list-style-type: square;
  ```

* **List-style-position**

  ```css
  list-style-position: 20px; //..check if wrong
  ```
  ```css
  list-style-position: inside / outside;
  ```

* **List-style-image**

  ```css
  list-style-image: url(image.gif);
  ```

* **Marker-offset**

  ```css
  marker-offset: auto;
  ```

<br>

---

<br>

## 11 Animations

CSS animations allow one to animate transitions or other media files on the web page.

* **Keyframes**
  ```
  @keyframes myanimation {
  from { opacity: 0; }
  to { opacity: 1; } 
  }
  ```

* **Animation-name**

  ```css
  animation-name: myanimation;
  ```

* **Animation-duration**

  ```css
  animation-duration: 10s;
  ```

* **Animation-timing-function**

  ```css
  animation-timing-function: ease;
  ```

* **Animation-delay**

  ```css
  animation-delay: 5ms;
  ```

* **Animation-iteration-count**

  ```css
  animation-iteration-count: 3;
  ```

* **Animation-direction**

  ```css
  animation-direction: normal;
  ```

* **Animation-play-state**

  ```css
  animation-play-state: running;
  ```

* **Animation-fill-mode**

  ```css
  animation-fill-mode: both;
  ```

<br>

---

<br>

## 12 Transitions and Transforms

Transitions let you define the transition between two states of an element.

* **Transition-property**

  ```css
  transition-property: none;
  ```

* **Transition-duration**

  ```css
  transition-duration: 2s;
  ```

* **Transition-timing-function**

  ```css
  transition-timing-function: ease-in-out;
  ```

* **Transition-delay**

  ```css
  transition-delay: 20ms;
  ```

* **Transition-all**
  ```css
  transition: all 2s ease-in-out 20ms;
  ```

* **Transform**
  ```css
  transform: scale(1.2) rotate(45deg);
  ```

* **will-change** (performance optimization)
  ```css
    will-change: transform;
  ```

[Page](https://yash-bandal.github.io/Web-Development-Essentials_CWH/CSS/Video%2042%20Transform%20and%20Its%20Templates/)

<br>

---

<br>

## 13 CSS Flexbox

CSS Flexbox
Flexbox is a layout of CSS that lets you format HTML easily. Flexbox makes it simple to align items vertically and horizontally using rows and columns. Items will "flex" to different sizes to fill the space. And overall, it makes the responsive design more manageable.

## A. Parent Properties (flex container)

* **display**

  ```css
  display: flex;
  ```

* **flex-direction**

  ```css
  flex-direction: row | row-reverse | column | column-reverse;
  ```

* **flex-wrap**

  ```css
  flex-wrap: nowrap | wrap | wrap-reverse;
  ```

* **flex-flow**

  ```css
  flex-flow: column wrap;
  ```

* **justify-content**

  ```css
  justify-content: flex-start | flex-end | center | space-between | space-around | space-evenly | start | end | left | right /* + safe | unsafe */;
  ```

* **align-items**

  ```css
  align-items: stretch | flex-start | flex-end | center | baseline | first baseline | last baseline | start | end | self-start | self-end /* + safe | unsafe */;
  ```

* **align-content**

  ```css
  align-content: flex-start | flex-end | center | space-between | space-around | space-evenly | stretch | start | end | baseline | first baseline | last baseline /* + safe | unsafe */;
  ```

<br>

## B. Child Properties (flex items)

* **order**

  ```css
  order: 5; /* default is 0 */
  ```

* **flex-grow**

  ```css
  flex-grow: 4; /* default 0 */
  ```

* **flex-shrink**

  ```css
  flex-shrink: 3; /* default 1 */
  ```

* **flex-basis**

  ```css
  flex-basis: auto; /* default auto */
  ```

* **flex shorthand**

  ```css
  flex: none | [ <'flex-grow'> <'flex-shrink'>? || <'flex-basis'> ];
  ```

* **align-self**

  ```css
  align-self: auto | flex-start | flex-end | center | baseline | stretch;
  ```

<br>

---

<br>

## 15  CSS Grid

CSS Grid
Grid layout is a 2-Dimensional grid system to CSS that creates complex responsive web design layouts more easily and consistently across browsers.

## A. Parent Properties (Grid container)

* **display**

  ```css
  display: grid | inline-grid;
  ```

* **grid-template-columns**

  ```css
  grid-template-columns: 12px 12px 12px;
  ```

* **grid-template-rows**

  ```css
  grid-template-rows: 8px auto 12px;
  ```

* **grid-template**

  ```css
  grid-template: none | <grid-template-rows> / <grid-template-columns>;
  ```

* **column-gap**

  ```css
  column-gap: <line-size>;
  ```

* **row-gap**

  ```css
  row-gap: <line-size>;
  ```

* **grid-column-gap**

  ```css
  grid-column-gap: <line-size>;
  ```

* **grid-row-gap**

  ```css
  grid-row-gap: <line-size>;
  ```

* **gap shorthand**

  ```css
  gap: <grid-row-gap> <grid-column-gap>;
  ```

* **grid-gap shorthand**

  ```css
  grid-gap: <grid-row-gap> <grid-column-gap>;
  ```

* **justify-items**

  ```css
  justify-items: start | end | center | stretch;
  ```

* **align-items**

  ```css
  align-items: start | end | center | stretch;
  ```

* **place-items**

  ```css
  place-items: center;
  ```

* **justify-content**

  ```css
  justify-content: start | end | center | stretch | space-around | space-between | space-evenly;
  ```

* **align-content**

  ```css
  align-content: start | end | center | stretch | space-around | space-between | space-evenly;
  ```

* **place-content**

  ```css
  place-content: <align-content> / <justify-content>;
  ```

* **grid-auto-columns**

  ```css
  grid-auto-columns: <track-size> ...;
  ```

* **grid-auto-rows**

  ```css
  grid-auto-rows: <track-size> ...;
  ```

* **grid-auto-flow**

  ```css
  grid-auto-flow: row | column | row dense | column dense;
  ```

<br>

## B. Child Properties (Grid items)

* **grid-column-start**

  ```css
  grid-column-start: <number> | <name> | span <number> | span <name> | auto;
  ```

* **grid-column-end**

  ```css
  grid-column-end: <number> | <name> | span <number> | span <name> | auto;
  ```

* **grid-row-start**

  ```css
  grid-row-start: <number> | <name> | span <number> | span <name> | auto;
  ```

* **grid-row-end**

  ```css
  grid-row-end: <number> | <name> | span <number> | span <name> | auto;
  ```

* **grid-column shorthand**

  ```css
  grid-column: <start-line> / <end-line> | <start-line> / span <value>;
  ```

* **grid-row shorthand**

  ```css
  grid-row: <start-line> / <end-line> | <start-line> / span <value>;
  ```

* **grid-area**

  ```css
  grid-area: <name> | <row-start> / <column-start> / <row-end> / <column-end>;
  ```

* **justify-self**

  ```css
  justify-self: start | end | center | stretch;
  ```

* **align-self**

  ```css
  align-self: start | end | center | stretch;
  ```

* **place-self**

  ```css
  place-self: center;
  ```

  <br>


## CSS Variables

CSS variables allow you to store values in reusable custom properties, improving maintainability and flexibility.

### Syntax

```css
/* Defining a variable */
:root {
  --main-color: #3498db;
  --padding-size: 10px;
}

/* Using a variable */
.element {
  color: var(--main-color);
  padding: var(--padding-size);
}

```

<br>


### Optional but Useful to Include
1. Caret-color (for input cursor color)
2. Resize (to allow resizable elements)
3. Scroll-behavior (for smooth scrolling)
4. Writing-mode (vertical/horizontal text layout)
5. Mix-blend-mode (layer blending effect)
6. Clip-path (custom shapes)
7. Aspect-ratio (modern responsive design)

 <br>

 
References: <br>
- [Emmet Cheatsheet](https://docs.emmet.io/cheat-sheet/)
- [W3School](https://www.w3schools.com/css/default.asp)
- [CWH](https://www.codewithharry.com/blogpost/css-cheatsheet)


<br>

## More

## 1 Pseudo Classes
### Complete List of Pseudo-Classes with Code Examples

```css
/* ==========================================================================
   1. USER ACTION & LINK pseudo-classes
   ========================================================================== */

a:hover {
  color: #ff0000;
}

button:active {
  background-color: #0000ff;
}

input:focus {
  border-color: #00ff00;
}

a:visited {
  color: #800080;
}

a:link {
  color: #0000ff;
}

a:any-link {
  color: #00ffff;
}

button:focus-visible {
  outline: 2px solid #0000ff;
}

button:focus-within {
  border-color: #ff00ff;
}

/* ==========================================================================
   2. TREE-STRUCTURAL & POSITION pseudo-classes
   ========================================================================== */

:root {
  --primary-color: #333333;
}

li:first-child {
  font-weight: bold;
}

li:last-child {
  border-bottom: none;
}

li:only-child {
  margin: 0;
}

tr:nth-child(even) {
  background-color: #f2f2f2;
}

tr:nth-last-child(2) {
  background-color: #ffff00;
}

p:first-of-type {
  font-size: 1.2rem;
}

p:last-of-type {
  margin-bottom: 0;
}

p:only-of-type {
  color: #555555;
}

h2:nth-of-type(2) {
  color: #008000;
}

h2:nth-last-of-type(1) {
  text-transform: uppercase;
}

p:empty {
  display: none;
}

/* ==========================================================================
   3. LOGICAL & FUNCTIONAL pseudo-classes
   ========================================================================== */

div:not(.special) {
  opacity: 0.5;
}

:is(h1, h2, h3):hover {
  color: #ff5722;
}

:where(h1, h2, h3) {
  margin: 0;
}

article:has(img) {
  grid-template-columns: 1fr 1fr;
}

/* ==========================================================================
   4. FORM & INPUT STATE pseudo-classes
   ========================================================================== */

input[type="checkbox"]:checked {
  accent-color: #2196f3;
}

button:disabled {
  opacity: 0.5;
}

input:enabled {
  background-color: #ffffff;
}

input:required {
  border-left: 3px solid #ff0000;
}

input:optional {
  border-left: 1px solid #cccccc;
}

input:valid {
  border-color: #4caf50;
}

input:invalid {
  border-color: #f44336;
}

input:in-range {
  background-color: #e8f5e9;
}

input:out-of-range {
  background-color: #ffebee;
}

input:read-only {
  background-color: #eee;
}

input:read-write {
  background-color: #fff;
}

input:placeholder-shown {
  border-color: #aaa;
}

input:autofill {
  box-shadow: 0 0 0 30px #fff8dc inset;
}

input:default {
  outline: 2px solid #2196f3;
}

input:indeterminate {
  opacity: 0.7;
}

/* ==========================================================================
   5. OTHER / SPECIAL pseudo-classes
   ========================================================================== */

section:target {
  background-color: #fffde7;
}

p:lang(es) {
  font-style: italic;
}

:defined {
  visibility: visible;
}

:fullscreen {
  background-color: #000000;
}

:picture-in-picture {
  box-shadow: 0 0 10px #000;
}

/* ==========================================================================
   6. MEDIA & RESOURCE PLAYBACK pseudo-classes
   ========================================================================== */

video:playing {
  border: 2px solid #4caf50;
}

video:paused {
  border: 2px solid #ff9800;
}

video:seeking {
  opacity: 0.8;
}

video:buffering {
  filter: blur(1px);
}

video:stalled {
  opacity: 0.5;
}

video:muted {
  filter: grayscale(100%);
}

video:volume-locked {
  cursor: not-allowed;
}

```
