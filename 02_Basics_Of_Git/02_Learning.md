# Assignment 1 - Creating Repository and First Commits

## Objective

Learn the basic Git workflow:

* Create a repository
* Add files
* Stage changes
* Commit changes
* Push commits to remote repository


<br>




## Files Created

### File1.txt

```text
List of Programming languages:
1. C#
2. Javascript
3. Java
4. C++
5. Go
```

### File2.txt

```text
List of Front-end frameworks
1. Vue Js
2. Angular
3. React
4. Jquery
```

### File3.txt

```text
List of DBMS
1. SQL Server
2. Oracle DB
3. MySql
4. Redis
```


<br>




## Git Concepts Learned

### Working Directory

The area where files are created and modified.

### Staging Area

A temporary area where changes are prepared before committing.

### Repository

Stores permanent snapshots of the project history.

```text
Working Directory
        ↓
     git add
        ↓
   Staging Area
        ↓
   git commit
        ↓
    Repository
```


<br>




## Commands Used

### Check Repository Status

```bash
git status
```

Shows:

* Modified files
* Staged files
* Untracked files


<br>




### Stage Files

```bash
git add File1.txt
git add File2.txt
git add File3.txt
```

Moves files to the staging area.


<br>




### Create Commits

Commit 1

```bash
git commit -m "Add programming languages list"
```

Commit 2

```bash
git commit -m "Add frontend frameworks list"
```

Commit 3

```bash
git commit -m "Add DBMS list"
```

Each file was committed separately as required by the assignment.


<br>




### Push Commits

```bash
git push -u origin main
```

Uploads local commits to Bitbucket.


<br>



## Commit History

```text
Add programming languages list
Add frontend frameworks list
Add DBMS list
```

View history:

```bash
git log --oneline
```


<br>



## .gitignore

Created `.gitignore` to prevent unnecessary files from being tracked.

Example:

```gitignore
bin/
obj/
*.sln
.vs/
```


<br>




## Key Learning

* `git add` moves changes to the staging area.
* `git commit` creates a snapshot of staged changes.
* `git push` uploads commits to the remote repository.
* One logical change should be committed separately.
* Git tracks project history through commits.


<br>

---
---

<br>



# Assignment 2 - Branching and Pull Request

## Objective

Learn how to:

* Create a new branch
* Work independently from the main branch
* Commit changes on a feature branch
* Push a branch to remote
* Create a Pull Request (PR)
* Understand the purpose of code reviews


<br>




## Branch Created

Created a new branch from `main`.

```bash
git checkout -b branch1
```

Verify current branch:

```bash
git branch
```

Output:

```text
* branch1
  main
```


<br>




## Changes Made

### File1.txt

Original:

```text
List of Programming languages:
1. C#
2. Javascript
3. Java
4. C++
5. Go
```

Updated:

```text
List of Programming languages:
1. C#
2. Javascript
3. Python
4. Ruby
5. Go
```

Changes:

* Java → Python
* C++ → Ruby


<br>




## Git Workflow

### Check Changes

```bash
git status
```

### View Exact Differences

```bash
git diff
```

Output:

```diff
-3. Java
-4. C++
+3. Python
+4. Ruby
```


<br>




### Stage Changes

```bash
git add File1.txt
```

### Create Commit

```bash
git commit -m "Replace Java with Python and C++ with Ruby"
```


<br>




## Push Branch

```bash
git push -u origin branch1
```

This creates a remote copy of `branch1` and links it to the local branch.


<br>




## Pull Request

Created a Pull Request:

```text
Source Branch      : branch1
Destination Branch : main
```

Purpose:

* Review changes before merging
* Discuss modifications with team members
* Prevent accidental changes to the main branch


<br>



## Reviewer

Added mentor/buddy as reviewer.

Steps:

```text
Repository Settings
    ↓
User and Group Access
    ↓
Add Members
    ↓
Invite Mentor/Buddy
```

Then selected the reviewer while creating the Pull Request.


<br>




## Important Note

The assignment specifically required:

```text
Do NOT merge the branch
```



<br>




## Concepts Learned

### What is a Branch?

A branch is an independent line of development.

```text
main
 │
 └── branch1
```

Changes made in `branch1` do not affect `main` until a merge occurs.


<br>




### What is a Pull Request?

A Pull Request is a request to merge changes from one branch into another.

```text
branch1
   │
   ▼
main
```

It allows code review before merging.


<br>




### Source vs Destination

```text
Source      = branch containing changes
Destination = branch receiving changes
```

For this assignment:

```text
Source      = branch1
Destination = main
```


<br>



## Commands Used

```bash
git checkout -b branch1
git status
git diff
git add File1.txt
git commit -m "Replace Java with Python and C++ with Ruby"
git push -u origin branch1
git branch
git log --oneline
```


<br>



---
---

<br>


# Assignment 3 - Resolving Merge Conflicts
<img width="500"  alt="Screenshot (4)" src="https://github.com/user-attachments/assets/775135d4-80a7-49f2-b3c2-c10b02811b14" />

<img width="500"  alt="Screenshot (5)" src="https://github.com/user-attachments/assets/d19b8d9a-197c-43df-a76f-96acd07a9328" />


<img width="500" alt="Screenshot (6)" src="https://github.com/user-attachments/assets/2c5347e8-4908-44ae-8d18-c68dac564b9d" />


## Objective

Learn how to:

- Create another branch from main
- Make independent changes
- Merge branches
- Resolve merge conflicts manually
- Create a Pull Request after conflict resolution


<br>




## Branch Created

Created a new branch from `main`.

```bash
git checkout main
git checkout -b branch2
```


<br>




## Changes Made

### File1.txt

Original:

```text
List of Programming languages:
1. C#
2. Javascript
3. Java
4. C++
5. Go
```

Updated:

```text
List of Programming languages:
1. C#
2. Javascript
3. Java
4. Kotlin
5. Go
6. Swift
```

Changes:

- C++ → Kotlin
- Added Swift


<br>




### Commit 1

```bash
git add File1.txt
git commit -m "Replace C++ with Kotlin and add Swift"
```


<br>




### File2.txt

Original:

```text
List of Front-end frameworks
1. Vue Js
2. Angular
3. React
4. Jquery
```

Updated:

```text
List of Front-end frameworks
1. Vue Js
2. Angular
3. React
4. Svelte
```

Changes:

- Jquery → Svelte


<br>



### Commit 2

```bash
git add File2.txt
git commit -m "Replace JQuery with Svelte"
```


<br>




## Merge Branch1 into Branch2

While on `branch2`:

```bash
git pull origin branch1
```

Git attempted to merge the changes from `branch1`.


<br>




## Merge Conflict

Conflict occurred because both branches modified the same lines in `File1.txt`.

### branch1

```text
3. Python
4. Ruby
```

### branch2

```text
3. Java
4. Kotlin
6. Swift
```

Git could not determine which version was correct.


<br>




## Conflict Resolution

Final file content:

```text
List of Programming languages:
1. C#
2. Javascript
3. Python
4. Kotlin
5. Go
6. Swift
```

Resolution:

- Python from branch1
- Kotlin from branch2
- Swift from branch2


<br>




## Complete Merge

```bash
git add File1.txt
git commit
```

This created a merge commit.

Example:

```text
Merge branch 'branch1'
```


<br>



## Push Branch

```bash
git push -u origin branch2
```


<br>




## Pull Request

Created Pull Request:

```text
Source      : branch2
Destination : main
```


<br>




## Concepts Learned

### Merge Conflict

Occurs when two branches modify the same part of a file and Git cannot automatically decide which change to keep.

### Merge Commit

A special commit created when combining histories from two branches.

```text
branch1 ----\
             \
              Merge Commit
             /
branch2 ----/
```


<br>




## Commands Used

```bash
git checkout main
git checkout -b branch2

git add File1.txt
git commit -m "Replace C++ with Kotlin and add Swift"

git add File2.txt
git commit -m "Replace JQuery with Svelte"

git pull origin branch1

git add File1.txt
git commit

git push -u origin branch2
```


<br>



---
---


<br>




# Assignment 4 - Cherry Picking

## Objective

Learn how to:

- Create a new branch
- Copy selected commits from other branches
- Understand cherry-pick workflow
- Avoid merging entire branches when only specific commits are needed


<br>



## What is Cherry Pick?

Cherry-pick allows copying a specific commit from another branch.

Instead of:

```bash
git merge branch1
```

which brings all commits,

we can use:

```bash
git cherry-pick <commit-id>
```

to bring only one commit.


<br>




## Branch Created

Created a new branch from `main`.

```bash
git checkout main
git checkout -b branch3
```


<br>




## Commits Selected

### From branch1

Commit:

```text
078aa9e
```

Purpose:

```text
Replace Java → Python
Replace C++ → Ruby
```


<br>



### From branch2

Commit:

```text
6163a38
```

Purpose:

```text
Replace JQuery → Svelte
```


<br>




## Cherry Pick Commands

### Copy File1 Changes

```bash
git cherry-pick 078aa9e
```


<br>




### Copy File2 Changes

```bash
git cherry-pick 6163a38
```


<br>




## Final File1.txt

```text
List of Programming languages:
1. C#
2. Javascript
3. Python
4. Ruby
5. Go
```


<br>




## Final File2.txt

```text
List of Front-end frameworks
1. Vue Js
2. Angular
3. React
4. Svelte
```


<br>




## Push Branch

```bash
git push -u origin branch3
```


<br>




## Concepts Learned

### Merge vs Cherry Pick

#### Merge

```bash
git merge branch1
```

Brings all commits from a branch.


<br>




#### Cherry Pick

```bash
git cherry-pick 078aa9e
```

Brings only the selected commit.


<br>




### Visual Representation

```text
branch1
├── Commit A
├── Commit B
└── Commit C

branch3
    │
    └── Cherry Pick Commit B
```

Only the selected commit is copied.


<br>




## Commands Used

```bash
git checkout main
git checkout -b branch3

git cherry-pick 078aa9e
git cherry-pick 6163a38

git push -u origin branch3
```


<br>


---
---

<br>






