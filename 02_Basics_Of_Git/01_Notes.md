# Basics of Git

## 1. Introduction to Version Control Systems (VCS)

A **Version Control System (VCS)** records changes made to code over time in a repository.

Benefits:

* Track project history
* See who made changes, when, and why
* Revert to previous versions when needed
* Enable team collaboration

### Centralized VCS

Examples:

* Subversion (SVN)
* Microsoft TFS

Characteristics:

* Single central server stores project history
* Developers connect to server to pull/push changes

Drawback:

* Single point of failure

### Distributed VCS

Examples:

* Git
* Mercurial

Characteristics:

* Every developer has a complete copy of the repository
* Includes full project history
* Can work offline

### Why Git?

* Free and open source
* Fast and scalable
* Efficient branching and merging
* Used by the majority of software projects worldwide


<br>



## 2. Ways to Use Git

### Command Line Interface (CLI)

Advantages:

* Fastest method
* Most flexible
* Works on remote servers

### IDE / Editor Integration

Examples:

* VS Code Source Control
* GitLens Extension

### GUI Tools

Examples:

* GitKraken
* SourceTree


<br>



## 3. Configuring Git



 <div align="center">
  <img src="https://github.com/dev-yash-25/DeltaX-Bootcamp/blob/6891d1080cd25a22862311bd759347a63b343af8/02_Basics_Of_Git/Assets/1.png" alt="All users" width="500" height="auto" />
</div>

<br>

### Set User Information

```bash
git config --global user.name "Yash Bandal"
git config --global user.email "yash.bandal@test.com"
```

### Set VS Code as Default Editor

```bash
git config --global core.editor "code --wait"
```

### Edit Global Configuration

```bash
git config --global -e
```

### Line Ending Configuration

#### Windows

```bash
git config --global core.autocrlf true
```

* LF → CRLF on checkout

     ```
     LF (Line Feed)
     \n

     Meaning:

     Move cursor down one line.
     ```
     ```
     CR (Carriage Return)
     \r

     Meaning:

     Move cursor to beginning of current line.
     ```



* CRLF → LF on commit

     ```
     CRLF
     \r\n

     Meaning:

     Return to start of line
     Move down to next line

     Used by:

     Windows
     ```

**Example**

- Imagine an old typewriter.
- Current position:
  ```
  Hello World|
  ```

1. CR (Carriage Return)

- Moves the carriage back:
  ```
  |Hello World
  ```
  Same line.

2. LF (Line Feed)

- Moves paper up one line:
  ```
  Hello World
  |
  ```
  Cursor stays in same column.

3. CR + LF

- Do both:
  ```
   Hello World
   |
  ```
  Start of next line.




#### Mac/Linux

```bash
git config --global core.autocrlf input
```

* CRLF → LF on commit
* Leaves checkout unchanged

### Help Commands

```bash
git config --help
git config -h
```


<br>



## 4. Basic Git Workflow

Git operates with three areas:

### Working Directory

* Actual project files
* Where development happens

### Staging Area (Index)

* Draft version of next commit
* Used to review changes before committing

### Repository

* Stored in hidden `.git` folder
* Contains commit history

```text
Working Directory (Changes Made Here)
       ↓
 Staging Area (After 'git add .' index area - Changes stay like a queue here - prefinal)
       ↓
   Repository (After 'git commit - m "" ' Final Chanegs saved)
```

<br>



## 5. Essential Commands

### Initialize Repository

```bash
git init
```

Creates:

```text
.git/
```

⚠️ Deleting `.git` removes all Git history.


<br>



### Check Status

```bash
git status
```

---

### Stage Files

```bash
git add file1.txt
git add *.txt
git add .
```


<br>



### Commit Changes

```bash
git commit -m "Initial commit"
```

Or:

```bash
git commit
```


<br>



### Commit Best Practices

* Commit frequently
* Keep commits focused
* One logical change per commit
* Write meaningful messages

Examples:

```text
Add login validation
Fix signup bug
Update README
```


<br>



### Skip Staging Area

For tracked files:

```bash
git commit -am "Fix signup bug"
```

---

## 6. Managing Files

### Remove Files

```bash
git rm file2.txt
```


<br>



### Rename or Move Files

```bash
git mv main.js file1.js
```


<br>


### Ignore Files

Create:

```text
.gitignore
```

Example:

```gitignore
logs/
*.log
bin/
node_modules/
```


<br>



### Stop Tracking Already Committed Files

```bash
git rm -r --cached bin/
```


<br>



## 7. Inspecting Changes

### Short Status

```bash
git status -s
```

| Symbol | Meaning                   |
| ------ | ------------------------- |
| ??     | Untracked file            |
| M      | Modified                  |
| A      | Added                     |
| MM     | Staged and modified again |


<br>



### View Differences

#### Unstaged Changes

```bash
git diff
```

#### Staged Changes

```bash
git diff --staged
```


<br>



### VS Code Diff Tool

```bash
git config --global diff.tool vscode

git config --global difftool.vscode.cmd "code --wait --diff \$LOCAL \$REMOTE"
```

Run:

```bash
git difftool
git difftool --staged
```


<br>



## 8. Viewing History

### Full History

```bash
git log
```

### Compact History

```bash
git log --oneline
```

### Chronological Order

```bash
git log --reverse
```


<br>



### Show Commit Details

Latest commit:

```bash
git show HEAD
```

Previous commit:

```bash
git show HEAD~1
```

Specific commit:

```bash
git show d601b90
```


<br>



### View File From Past Commit

```bash
git show HEAD~1:.gitignore
```

---

### View Tree Structure

```bash
git ls-tree HEAD~1
```


<br>



## 9. Undoing Changes

### Unstage a File

```bash
git restore --staged file1.js
```


<br>



### Discard Local Changes

```bash
git restore file1.js
```

Discard all:

```bash
git restore .
```


<br>



### Remove Untracked Files

```bash
git clean -fd
```

Flags:

* `-f` → force
* `-d` → include directories


<br>



### Restore File From Older Commit

```bash
git restore --source HEAD~1 file1.js
```


<br>



# Quick Reference

```bash
git init
git status
git add .
git commit -m "message"
git log --oneline
git diff
git restore file
git restore --staged file
git clean -fd
git rm file
git mv old new
```

<br>



# Git Workflow Summary

```text
Create/Modify Files
        ↓
git status
        ↓
git add .
        ↓
git commit -m "message"
        ↓
git log
```
