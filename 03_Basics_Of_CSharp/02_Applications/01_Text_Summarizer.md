# Problem: Text Summarization

## Problem Statement

Given a string `text` and an integer `maxLength`, return a summarized version of the text such that:

* The summary contains only **complete words**.
* The summary length does **not exceed** `maxLength`.
* If the original text length is less than or equal to `maxLength`, return the original text.
* If the text is truncated, append `"..."` to the end of the summary.




## Constraints

* `1 <= text.Length <= 10^5`
* `1 <= maxLength <= text.Length`
* Words are separated by a single space.
* Do **not** split a word in half.



## Test Cases

### Test Case 1

```text
Input:
text = "This is going to be a really long text"
maxLength = 10

Output:
"This is..."
```



### Test Case 2

```text
Input:
text = "Hello World"
maxLength = 20

Output:
"Hello World"
```

Explanation:

The text already fits within `maxLength`.

<br>

### Code

```csharp
using System;
using System.Collections.Generic;

namespace CSharpFundamentals
{
    class Program
    {
        public static string SummarizeText(string text, int maxLength) {
            //base case
            if (text.Length <= maxLength)
            { 
                return text;
            }

            //split logic
            
            //string array
            var words = text.Split(' ');
            var summaryList = new List<string>();
            var totalCharCnt = 0;


            foreach(var word in words)
            {

                summaryList.Add(word);
                totalCharCnt += word.Length + 1; //characters in the word + the space after the word
                if (totalCharCnt > maxLength)
                {
                    break;
                }
                    
            }


            //join and return
            var summary = String.Join(" ", summaryList) + "...";

            return summary;
        }
        static void Main(string[] args)
        {
            var sentence = "This is going to be a really very very very very very very long text written by me";
            var summary = SummarizeText(sentence, 20);
            Console.WriteLine(summary);
        
        }
    }
}
```
**Output:**
```
This is going to be a...
```
