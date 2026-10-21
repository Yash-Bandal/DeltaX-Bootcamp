<h1 align="center"><b> JavaScript Cheatsheet </b></h1>
 
By YB-Studios
Updated: 4 June 2025

<br>

<br>

---

<br>


## 1 JavaScript Basics

### On Page Script

Adding internal JavaScript to HTML:

```html
<script type="text/javascript">
    // JS code goes here
</script>
```

### External JS File

Adding external JavaScript to HTML:

```html
<script src="filename.js"></script>
```

### Functions

JavaScript Function syntax:

```javascript
function nameOfFunction() {
    // function body
}
```

### DOM Element

Changing content of a DOM Element:

```javascript
document.getElementById("elementID").innerHTML = "Hello World!";
```

### Output

This will print the value of a in JavaScript console:

```javascript
console.log(a);
```

<br>

---

<br>

## 2 Conditional Statements

### If Statement

```javascript
if (condition) {
    // block of code to be executed if the condition is true
}
```

### If-else Statement

```javascript
if (condition) {
    // block of code to be executed if the condition is true
} else {
    // block of code to be executed if the condition is false
}
```

### Else-if Statement

```javascript
if (condition1) {
    // block of code to be executed if condition1 is true
} else if (condition2) {
    // block of code to be executed if condition1 is false and condition2 is true
} else {
    // block of code to be executed if both are false
}
```

### Switch Statement

```javascript
switch (expression) {
    case x:
        // code block
        break;
    case y:
        // code block
        break;
    default:
        // code block
}
```

<br>

---

<br>

## 3 Iterative Statements (Loops)

### For Loop

```javascript
for (initialization; condition; incrementation) {
    // code block
}
```

Example:

```javascript
for (let i = 0; i < 5; i++) {
    text += "Iteration number: " + i + "<br>";
}
```

### While Loop

```javascript
while (condition) {
    // code block
}
```

### Do While Loop

```javascript
do {
    // run this code in block
    i++;
} while (condition);
```

<br>

---

<br>

## 4 Strings

### charAt Method

```javascript
str.charAt(3);
```

### concat Method

```javascript
str1.concat(str2);
```

### indexOf Method

```javascript
str.indexOf('substr');
```

### match Method

```javascript
str.match(/(chapter \d+(\.\d)*)/i);
```

### replace Method

```javascript
str1.replace(str2);
```

### search Method

```javascript
str.search('term');
```

### split Method

```javascript
str.split('\n');
```

### substring Method

```javascript
str.substring(0, 5);
```

<br>

---

<br>

## 5 Arrays

### variable

```javascript
var fruit = ["element1", "element2", "element3"];
```

### concat Method

```javascript
array1.concat(array2);
```

### indexOf Method

```javascript
array.indexOf(item);
```

### join Method

```javascript
array.join();
```

### pop Method

```javascript
array.pop();
```

### reverse Method

```javascript
array.reverse();
```

### sort Method

```javascript
array.sort();
```

### toString Method

```javascript
array.toString();
```

### valueOf Method

```javascript
array.valueOf();
```

<br>

---

<br>

## 6 Number Methods

JS math and number objects provide several constants and methods to perform mathematical operations.

* **toExponential method**

  Converts a number to its exponential form:

  ```js
  toExponential()
  ```

* **toPrecision method**

  Formats a number into a specified length:

  ```js
  toPrecision()
  ```

* **toString method**

  Converts an object to a string:

  ```js
  toString()
  ```

* **valueOf method**

  Returns the primitive value of a number:

  ```js
  valueOf()
  ```

<br>

---

<br>

## 7 Maths Methods

* **ceil method**

  Rounds a number upwards to the nearest integer, and returns the result:

  ```js
  Math.ceil(x)
  ```

* **exp method**

  Returns the value of E^x:

  ```js
  Math.exp(x)
  ```

* **log method**

  Returns the logarithmic value of x:

  ```js
  Math.log(x)
  ```

* **pow method**

  Returns the value of x to the power y:

  ```js
  Math.pow(x, y)
  ```

* **random method**

  Returns a random number between 0 and 1:

  ```js
  Math.random()
  ```

* **sqrt method**

  Returns the square root of a number x:

  ```js
  Math.sqrt(x)
  ```

<br>

---

<br>

## 8  Dates

Date object is used to get the year, month, and day. It has methods to get and set day, month, year, hour, minute, and seconds.

* **getDate()**

  Returns the date from the date object:

  ```js
  date.getDate()
  ```

* **getDay()**

  Returns the day from the date object:

  ```js
  date.getDay()
  ```

* **getHours()**

  Returns the hours from the date object:

  ```js
  date.getHours()
  ```

* **getMinutes()**

  Returns the minutes from the date object:

  ```js
  date.getMinutes()
  ```

* **getSeconds()**

  Returns the seconds from the date object:

  ```js
  date.getSeconds()
  ```

* **getTime()**

  Returns the time from the date object:

  ```js
  date.getTime()
  ```

<br>

---

<br>

## 9 Mouse Events

Any change in the state of an object is referred to as an Event. With the help of JS, you can handle events, i.e., how any specific HTML tag will work when the user does something.

* **click**

  Fired when an element is clicked:

  ```js
  element.addEventListener('click', () => {
      // Code to be executed when the event is fired
  });
  ```

* **oncontextmenu**

  Fired when an element is right-clicked:

  ```js
  element.addEventListener('contextmenu', () => {
      // Code to be executed when the event is fired
  });
  ```

* **dblclick**

  Fired when an element is double-clicked:

  ```js
  element.addEventListener('dblclick', () => {
      // Code to be executed when the event is fired
  });
  ```

* **mouseenter**

  Fired when an element is entered by the mouse arrow:

  ```js
  element.addEventListener('mouseenter', () => {
      // Code to be executed when the event is fired
  });
  ```

* **mouseleave**

  Fired when an element is exited by the mouse arrow:

  ```js
  element.addEventListener('mouseleave', () => {
      // Code to be executed when the event is fired
  });
  ```

* **mousemove**

  Fired when the mouse is moved inside the element:

  ```js
  element.addEventListener('mousemove', () => {
      // Code to be executed when the event is fired
  });
  ```


---

### Keyboard Events

**keydown**

```javascript
element.addEventListener('keydown', () => {
    // Code to be executed when the event is fired
});
```

**keypress**

```javascript
element.addEventListener('keypress', () => {
    // Code to be executed when the event is fired
});
```

**keyup**

```javascript
element.addEventListener('keyup', () => {
    // Code to be executed when the event is fired
});
```



---


### Errors

**try and catch**

```javascript
try {
    // Block of code to try
} catch (err) {
    // Block of code to handle errors
}
```

---

### Window Methods

**alert**

```javascript
alert()
```

**blur**

```javascript
blur()
```

**setInterval**

```javascript
setInterval(() => {
    // Code to be executed
}, 1000);
```

**setTimeout**

```javascript
setTimeout(() => {
    // Code to be executed
}, 1000);
```

**close**

```javascript
window.close()
```

**confirm**

```javascript
window.confirm('Are you sure?')
```

**open**

```javascript
window.open("https://www.codewithharry.com")
```

**prompt**

```javascript
var name = prompt("What is your name?", "Harry")
```

**scrollBy**

```javascript
window.scrollBy(100, 0); // Scroll 100px to the right
```

**scrollTo**

```javascript
window.scrollTo(500, 0); // Scroll to horizontal position 500
```

**clearInterval**

```javascript
clearInterval(var)
```

**clearTimeout**

```javascript
clearTimeout(var)
```

**stop**

```javascript
stop()
```

<br>

---

<br>

## 10 DOM

### Query/Get Elements

**querySelector**

```javascript
document.querySelector('css-selectors')
```

**querySelectorAll**

```javascript
document.querySelectorAll('css-selectors')
```

**getElementsByTagName**

```javascript
document.getElementsByTagName('element-name')
```

**getElementsByClassName**

```javascript
document.getElementsByClassName('class-name')
```

**getElementById**

```javascript
document.getElementById('id')
```

<br>

---

<br>

### 11 Creating Elements

**createElement**

```javascript
document.createElement('div')
```

**createTextNode**

```javascript
document.createTextNode('some text here')
```
