<h1 align="center"> <b> HTML Cheatsheet </b> </h1>

**By YB-Studios** <br>
**Updated: 4 June 2025**

<br>

## Index

1. [Boilerplate](#1-boilerplate)
2. [Headings](#2-headings)
3. [Containers](#3-containers)
4. [Text Formatting](#4-text-formatting)
5. [Lists](#5-lists)
6. [Media](#6-media)
7. [Table](#7-table)
8. [Links](#8-links)
9. [Form](#9-form)
10. [Form Elements](#10-form-elements)
11. [Characters & Symbols](#11-characters--symbols)
12. [Semantic Elements](#12-semantic-elements)
13. [Meta Tags](#13-meta-tags)
14. [CSS Integration](#14-css-integration)
15. [Accessibility](#15-accessibility)
16. [Responsive Design](#16-responsive-design)
17. [JavaScript Integration](#17-javascript-integration)
18. [Comments](#18-comments)
19. [Tips for HTML Beginners](#19-tips-for-html-beginners)

<br>

---

<br>

## 1. Boilerplate

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Document</title>
</head>
<body>
    <!-- Body -->
</body>
</html>
```

<br>

---

<br>

## 2. Headings

There are six headings available in HTML. `<h1>` is the largest and `<h6>` is the smallest.

* `<h1>` Tag:

  ```html
  <h1>Heading 1</h1>
  ```
* `<h2>` Tag:

  ```html
  <h2>Heading 2</h2>
  ```
* `<h3>` Tag:

  ```html
  <h3>Heading 3</h3>
  ```
* `<h4>` Tag:

  ```html
  <h4>Heading 4</h4>
  ```
* `<h5>` Tag:

  ```html
  <h5>Heading 5</h5>
  ```
* `<h6>` Tag:

  ```html
  <h6>Heading 6</h6>
  ```
  
<br>

---

<br>

## 3. Containers

Container tags hold data such as text or images.

* `<div>` Tag:

  ```html
  <div>This is a div block</div>
  ```
* `<span>` Tag:

  ```html
  <span>This is a span block</span>
  ```
* `<p>` Tag:

  ```html
  <p>This is a paragraph</p>
  ```
* `<pre>` Tag:

  ```html
  <pre>Hello World</pre>
  ```
* `<code>` Tag:

  ```html
  <code>
  import python
  </code>
  ```

<br>

---

<br>

## 4. Text Formatting

Text formatting tags make the document more readable and stylized.

* `<b>` Tag:

  ```html
  <b>I'm bold text</b>
  ```
* `<strong>` Tag:

  ```html
  <strong>I'm important text</strong>
  ```
* `<i>` Tag:

  ```html
  <i>I'm italic text</i>
  ```
* `<em>` Tag:

  ```html
  <em>Emphasized text</em>
  ```
* `<sub>` Tag:

  ```html
  <sub>Subscript</sub>
  ```
* `<sup>` Tag:

  ```html
  <sup>Superscript</sup>
  ```

<br>

---

<br>

## 5. Lists

Lists can be numbered or bulleted.

* **Ordered List**:

  ```html
  <ol>
      <li>Data 1</li>
      <li>Data 2</li>
      <li>Data 3</li>
  </ol>
  ```

* **Unordered List**:

  ```html
  <ul>
      <li>Your Data</li>
      <li>Your Data</li>
  </ul>
  ```

<br>

---

<br>

## 6. Media

Media includes audio, video, and images.

* `<audio>` Tag:

  ```html
  <audio controls>
      <source src="demo.mp3" type="audio/mpeg">
      Your browser does not support the audio element.
  </audio>
  ```

* `<img>` Tag:

  ```html
  <img src="Source_of_image" alt="Alternate text">
  ```

* `<video>` Tag:

  ```html
  <video width="480" height="320" controls>
      <source src="demo_move.mp4" type="video/mp4">
      Your browser does not support the video tag.
  </video>
  ```

<br>

---

<br>

## 7. Table

Tables represent data in rows and columns.

```html
<table>
    <caption>Demo Table</caption>
    <thead>
        <tr>
            <th>Column1</th>
            <th colspan="2">Column2</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Data1</td>
            <td>Data2</td>
            <td>Data2</td>
        </tr>
        <tr>
            <td>Data1</td>
            <td>Data2</td>
            <td>Data2</td>
        </tr>
    </tbody>
    <tfoot>
        <tr>
            <td>&nbsp;</td>
            <td>Data</td>
            <td>Data</td>
        </tr>
    </tfoot>
</table>
```

<br>

---

<br>

## 8. Links

Links allow navigation to other pages.

* `<a>` Tag:

  ```html
  <a href="https://www.codewithharry.com/">Visit CodeWithHarry.com!</a>
  ```

<br>

---

<br>

## 9. Form

Forms collect user input.

```html
<form action="/action.php" method="post">
    <textarea cols="20" name="comments" rows="5">Comment</textarea><br />
    <label><input name="terms" type="checkbox" value="tandc" />Accept terms</label> <br />
    <input type="submit" value="Submit" />
</form>
```

<br>

---

<br>

## 10. Form Elements

Form elements are various types of inputs and controls used to collect user data.

* **Text Input**:

  ```html
  <input type="text" name="username" placeholder="Enter Username">
  ```

* **Password Input**:

  ```html
  <input type="password" name="password" placeholder="Enter Password">
  ```

* **Checkbox**:

  ```html
  <input type="checkbox" name="agree" value="yes"> I agree
  ```

* **Radio Button**:

  ```html
  <input type="radio" name="gender" value="male"> Male<br>
  <input type="radio" name="gender" value="female"> Female
  ```

* **Submit Button**:

  ```html
  <input type="submit" value="Submit">
  ```

* **Button**:

  ```html
  <button type="button">Click Me</button>
  ```

* **Select (Dropdown) List**:

  ```html
  <select name="country">
      <option value="usa">United States</option>
      <option value="canada">Canada</option>
  </select>
  ```

* **Textarea**:

  ```html
  <textarea name="comments" rows="4" cols="50">Enter comments here</textarea>
  ```

* **File Input**:

  ```html
  <input type="file" name="fileupload">
  ```

* **Range Input**:

  ```html
  <input type="range" name="volume" min="0" max="100">
  ```

* **Number Input**:

  ```html
  <input type="number" name="quantity" min="1" max="10">
  ```

* **Email Input**:

  ```html
  <input type="email" name="email" placeholder="Enter Email">
  ```

* **Search Input**:

  ```html
  <input type="search" name="query" placeholder="Search">
  ```

* **URL Input**:

  ```html
  <input type="url" name="website" placeholder="Enter URL">
  ```

* **Date Input**:

  ```html
  <input type="date" name="birthdate">
  ```

<br>

---

<br>

## 11. Characters & Symbols

Display special characters using HTML entities.

* **Copyright Symbol**:

  ```html
  &copy;
  ```
* **Less Than**:

  ```html
  &lt;
  ```
* **Greater Than**:

  ```html
  &gt;
  ```
* **Ampersand**:

  ```html
  &amp;
  ```
* **Dollar**:

  ```html
  &dollar;
  ```

<br>

---

<br>

## 12. Semantic Elements

Semantic tags provide meaning to the HTML content.

* `<section>` Tag:

  ```html
  <section>This is a section</section>
  ```
* `<article>` Tag:

  ```html
  <article>Enter your data here</article>
  ```
* `<aside>` Tag:

  ```html
  <aside>Your data</aside>
  ```

<br>

---

<br>

## 13. Meta Tags

Meta tags describe metadata about the HTML document.

```html
<meta name="description" content="This is a description of the page">
<meta name="keywords" content="HTML, CSS, JavaScript">
<meta name="author" content="Author Name">
```

<br>

---

<br>

## 14. CSS Integration

Style HTML using internal or external CSS.

* **Internal CSS**:

  ```html
  <style>
    body { background-color: lightblue; }
  </style>
  ```

* **External CSS**:

  ```html
  <link rel="stylesheet" type="text/css" href="styles.css">
  ```

<br>

---

<br>

## 15. Accessibility

Enhance usability for everyone.

```html
<img src="image.jpg" alt="Description of Image">
<label for="name">Name:</label>
<input type="text" id="name" name="name">
```

<br>

---

<br>

## 16. Responsive Design

Use CSS media queries and viewport settings for responsiveness.

```html
<meta name="viewport" content="width=device-width, initial-scale=1">
<style>
  @media (max-width: 600px) {
    body { font-size: 18px; }
  }
</style>
```

<br>

---

<br>

## 17. JavaScript Integration

Add functionality with embedded or linked JS.

```html
<script>
  alert('Hello, World!');
</script>
<script src="script.js"></script>
```

<br>

---

<br>

## 18. Comments

Comments help document your HTML.

```html
<!-- This is a comment -->
```

<br>

---

<br>

## 19. Tips for HTML Beginners

1. **Use a Modern Text Editor**
   Tools like VS Code provide syntax highlighting and autocompletion.

2. **Utilize Emmet**
   Emmet lets you write shortcuts like `ul>li*5` and expand to:

   ```html
   <ul>
     <li></li>
     <li></li>
     <li></li>
     <li></li>
     <li></li>
   </ul>
   ```

3. **Indent Your Code**
   Proper indentation improves readability.

4. **Always Close Tags**
   Prevent rendering issues by closing all tags.

5. **Learn CSS and JavaScript**
   Combine them with HTML for dynamic and styled sites.

6. **Use Comments Wisely**
   Use them for notes, documentation, or debugging.

7. **Practice Regularly**
   Build different webpages to improve.

8. **Learn Responsive Design**
   Use media queries and flexible layouts for all screen sizes.


References: <br>
- [Emmet Cheatsheet](https://docs.emmet.io/cheat-sheet/)
- [W3School](https://www.w3schools.com/html/)
- [CWH](https://www.codewithharry.com/blogpost/html-cheatsheet)
