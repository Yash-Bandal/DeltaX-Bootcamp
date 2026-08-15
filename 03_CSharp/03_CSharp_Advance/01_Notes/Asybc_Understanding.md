# Async vs Sync Programming Dry Run

> [!Note]
> The original C# thread is free. The operation is still happening somewhere else, its not like its always idle

Assume a file download takes **5 seconds**.

# 1. Normal Synchronous Code

```csharp
public void Process()
{
    Console.WriteLine("1. Start");

    DownloadFile(); // takes 5 seconds

    Console.WriteLine("2. Completed");
}


public void DownloadFile()
{
    Thread.Sleep(5000);

    Console.WriteLine("Download Done");
}
```

## Dry Run

```text
TIME        Thread

0 sec       Thread enters Process()

            Prints:
            1. Start


0 sec       Calls DownloadFile()

            Thread.Sleep(5000)

            THREAD IS BLOCKED
            Cannot do anything else


1 sec       waiting...

2 sec       waiting...

3 sec       waiting...

4 sec       waiting...


5 sec       Download completed

            Prints:
            Download Done


5 sec       returns to Process()

            Prints:
            2. Completed
```

## Important Part

```text
Thread is stuck for 5 seconds
```

<br>

# 2. Async Code

```csharp
public async Task Process()
{
    Console.WriteLine("1. Start");

    await DownloadFileAsync();

    Console.WriteLine("2. Completed");
}


public async Task DownloadFileAsync()
{
    await Task.Delay(5000);

    Console.WriteLine("Download Done");
}
```

## Dry Run

```text
TIME        Thread

0 sec       Thread enters Process()

            Prints:
            1. Start


0 sec       Calls DownloadFileAsync()

            Hits await Task.Delay(5000)


            Method is paused here

            BUT

            THREAD IS RELEASED


1 sec       Thread can do other work

            - handle another user
            - process another request
            - update UI


2 sec       Thread still free

3 sec       Thread still free

4 sec       Thread still free


5 sec       Delay completed

            Thread comes back

            Continues after await


            Prints:
            Download Done


            returns to Process()


            Prints:
            2. Completed
```

<br>

# Both Outputs Are Same

## Sync Output

```text
1. Start
Download Done
2. Completed
```

## Async Output

```text
1. Start
Download Done
2. Completed
```

That is why async feels confusing.

The difference is **not the output**.

The difference is **how the thread behaves**.

<br>

# Sync Flow

```text
Thread:

Start

  |

Download

  |

WAIT
WAIT
WAIT
WAIT

  |

Finish
```

The thread waits and cannot perform any other work.

<br>

# Async Flow

```text
Thread:

Start

  |

Start Download

  |

FREE ------------------>

Download completes

Thread comes back

  |

Finish
```

The method waits, but the thread is free.

<br>

# Real Life Example - Restaurant

## Sync Waiter

```text
Customer orders pizza

        |

Waiter stands near oven
for 20 minutes

        |

Pizza ready

        |

Waiter serves customer
```

Problem:

```text
One waiter handles only one customer.
Time is wasted.
```

<br>

## Async Waiter

```text
Customer orders pizza

        |

Waiter gives order to kitchen

        |

Waiter serves other customers

        |

Kitchen says pizza ready

        |

Any free waiter continues order
```

Result is the same.

But resources are used efficiently.

<br>

# Key Point

```text
await pauses the METHOD,
not the THREAD.
```

That is the main idea behind async/await.
