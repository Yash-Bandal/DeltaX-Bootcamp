# 8.2 System.IO

## What is System.IO?

`System.IO` is a namespace that provides classes for working with:

* Files
* Folders (Directories)
* File Paths
* Streams

<div align = "center">
<img width="400" alt="image" src="https://github.com/user-attachments/assets/05592a4d-e526-4b74-aa92-7b170baefca6" />
</div>

It is commonly used for:

* Reading files
* Writing files
* Creating folders
* Deleting files
* Copying files
* Logging
* Configuration files

Import the namespace:

```csharp
using System.IO;
```

<br>

# File vs FileInfo


<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/fe5bdb78-85b0-4cb0-9502-0b0a81bac489" />
<img width="600" alt="image" src="https://github.com/user-attachments/assets/71eb310d-3704-4020-a954-24dd4e01400a" />
    <p> Methods</p>
    <img width="600" alt="image" src="https://github.com/user-attachments/assets/927a8d31-9d05-4b79-a7ca-ada1ad0fb3e5" />

</div>

---
> [!Note]
> The problem with the **File**  `static methods` is , everytime ypu call static methods, a security check is made by the **OS**\
> to Ensure if user has access to file
>
> So for large operations, it affects the performance of the application, so in that case, it becomes efficient to\
> use the **FileInfo** class `instance methods`, that perform only 1 time security checkup , at time of creation of file,objects
> 
---



## File Class

`File` is a **static class** used for quick file operations.

Use it when you don't need to work with the same file repeatedly.

<br>

### Create a File

```csharp
File.Create("sample.txt");
```

<br>

### Write Text

```csharp
File.WriteAllText("sample.txt", "Hello World");
```

<br>

### Read Text

```csharp
string text = File.ReadAllText("sample.txt");

Console.WriteLine(text);
```

Output:

```text
Hello World
```

<br>

### Append Text

```csharp
File.AppendAllText("sample.txt", "\nWelcome");
```

Result:

```text
Hello World
Welcome
```

<br>

### Copy File

```csharp
File.Copy(
    "sample.txt",
    "backup.txt"
);
```

<br>

### Move File

```csharp
File.Move(
    "sample.txt",
    "data.txt"
);
```

<br>

### Delete File

```csharp
File.Delete("data.txt");
```

<br>

### Check if File Exists

```csharp
if(File.Exists("sample.txt"))
{
    Console.WriteLine("File Found");
}
```

<br>

# FileInfo Class

`FileInfo` is used when working with the same file multiple times.

Unlike `File`, it is **not static**.

Create an object first.

```csharp
FileInfo file =
    new FileInfo("sample.txt");
```

<br>

### Create File

```csharp
file.Create();
```

<br>

### Copy

```csharp
file.CopyTo("backup.txt");
```

<br>

### Move

```csharp
file.MoveTo("newfile.txt");
```

<br>

### Delete

```csharp
file.Delete();
```

<br>

### File Information

```csharp
Console.WriteLine(file.Name);

Console.WriteLine(file.Length);

Console.WriteLine(file.Extension);

Console.WriteLine(file.CreationTime);
```

<br>

## File vs FileInfo

| File               | FileInfo                  |
| ------------------ | ------------------------- |
| Static Class       | Instance Class            |
| Quick operations   | Repeated operations       |
| No object required | Object required           |
| Simple to use      | Provides file information |

<br>

# Directory vs DirectoryInfo
<div align = "center">
<img width="600" alt="image" src="https://github.com/user-attachments/assets/0254a9a1-e032-41d5-b72d-551b3f37f48a" />
 <img width="450" alt="image" src="https://github.com/user-attachments/assets/feee32bb-b95c-45b9-aa50-de32f608c735" />
</div>


## Directory Class

Used for folder operations.

It is also a static class.

<br>

### Create Directory

```csharp
Directory.CreateDirectory("Logs");
```

<br>

### Delete Directory

```csharp
Directory.Delete("Logs");
```

Delete non-empty directory:

```csharp
Directory.Delete("Logs", true);
```

<br>

### Check if Directory Exists

```csharp
if(Directory.Exists("Logs"))
{
    Console.WriteLine("Folder Exists");
}
```

<br>

### Get Files

```csharp
string[] files =
    Directory.GetFiles("Logs");
```

<br>

### Get Directories

```csharp
string[] folders =
    Directory.GetDirectories(".");
```

<br>

# DirectoryInfo Class

Used when repeatedly working with the same folder.

```csharp
DirectoryInfo folder =
    new DirectoryInfo("Logs");
```

<br>

### Create Folder

```csharp
folder.Create();
```

<br>

### Delete Folder

```csharp
folder.Delete();
```

<br>

### Get Files

```csharp
FileInfo[] files =
    folder.GetFiles();
```

<br>

### Get Subdirectories

```csharp
DirectoryInfo[] directories =
    folder.GetDirectories();
```

<br>

### Folder Information

```csharp
Console.WriteLine(folder.Name);

Console.WriteLine(folder.FullName);

Console.WriteLine(folder.CreationTime);
```

<br>

## Directory vs DirectoryInfo

| Directory           | DirectoryInfo       |
| ------------------- | ------------------- |
| Static Class        | Instance Class      |
| Quick operations    | Repeated operations |
| No object required  | Object required     |
| Limited information | More folder details |

<br>

# Path Class

<div align = "center">
<img width="382" height="378" alt="image" src="https://github.com/user-attachments/assets/b749e2e7-6b67-4a65-bff9-253c1f3f3438" />
</div>


## What is Path?

The `Path` class helps work with file and folder paths.

It does **not** create or modify files.

It only manipulates path strings.

<br>

### Combine Paths

Recommended over manually writing `\`.

```csharp
string path =
    Path.Combine(
        "C:\\Users",
        "Yash",
        "Documents",
        "notes.txt"
    );
```

Output:

```text
C:\Users\Yash\Documents\notes.txt
```

<br>

### Get File Name

```csharp
string fileName =
    Path.GetFileName(path);
```

Output:

```text
notes.txt
```

<br>

### Get Extension

```csharp
Console.WriteLine(
    Path.GetExtension(path)
);
```

Output:

```text
.txt
```

<br>

### Get File Name Without Extension

```csharp
Console.WriteLine(
    Path.GetFileNameWithoutExtension(path)
);
```

Output:

```text
notes
```

<br>

### Get Directory Name

```csharp
Console.WriteLine(
    Path.GetDirectoryName(path)
);
```

Output:

```text
C:\Users\Yash\Documents
```

<br>

### Change Extension

```csharp
Console.WriteLine(
    Path.ChangeExtension(path, ".pdf")
);
```

Output:

```text
C:\Users\Yash\Documents\notes.pdf
```

<br>

### Get Temporary File Name

```csharp
string temp =
    Path.GetTempFileName();
```

Creates a temporary file and returns its path.

<br>

## Common File Operations Example

```csharp
using System.IO;

string path = "notes.txt";

if(File.Exists(path))
{
    string content =
        File.ReadAllText(path);

    Console.WriteLine(content);
}
else
{
    File.WriteAllText(path, "Hello C#");
}
```

<br>

## Real-World Uses

System.IO is commonly used for:

* Reading configuration files
* Creating log files
* Exporting reports
* Importing CSV/Excel data
* Saving user settings
* Uploading and downloading files
* Backup systems

<br>

## Most Used Classes

| Class         | Purpose                              |
| ------------- | ------------------------------------ |
| File          | Quick file operations                |
| FileInfo      | File details and repeated operations |
| Directory     | Folder operations                    |
| DirectoryInfo | Folder details                       |
| Path          | Path manipulation                    |

<br>

## Key Takeaways

* `System.IO` is used for file and folder operations.
* `File` and `Directory` are static classes for quick tasks.
* `FileInfo` and `DirectoryInfo` provide object-oriented access and additional information.
* `Path` helps build and manipulate file paths safely.
* Use `Path.Combine()` instead of manually concatenating paths.
* Always check `File.Exists()` or `Directory.Exists()` before performing operations to avoid exceptions.
