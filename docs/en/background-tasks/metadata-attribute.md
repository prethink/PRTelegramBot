---
description: Declaring a task's schedule with an attribute on the class.
---

# A task with the metadata attribute

The shortest of the three arrangements. `[PRBackgroundTask]` implements `IPRBackgroundTaskMetadata`, so putting it on the class supplies the schedule declaratively — the class itself only holds the work.

The attribute sets the identifier, whether the task repeats, the interval, and the limits on runs and errors.

Doing it this way means:

* the configuration is visible at the top of the class, next to what it configures;
* the logic stays free of scheduling concerns;
* the framework reads the metadata during initialisation, with nothing to register by hand.

```csharp
using AspNetExample.Models;
using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;
using System.Diagnostics;

namespace AspNetExample.BackgroundTasks
{
    [PRBackgroundTask(Constants.EXAMPLE_TASK_DI_ATTRIBUTES_ID, "Test Data base", 1)]
    public class ExampleDIAttributeBackgroundTasks : IPRBackgroundTask
    {
        private readonly AppDbContext db;
        private PRBotBase bot;

        public Guid Id => Constants.EXAMPLE_DI_TASK_GUID;

        public Task<bool> CanExecute()
        {
            return Task.FromResult(true);
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var users = db.Users.ToList();
            Debug.WriteLine($"Users is {users.Count}");
        }

        public Task Initialize(PRBotBase bot)
        {
            this.bot = bot;
            return Task.CompletedTask;
        }

        public ExampleDIAttributeBackgroundTasks(AppDbContext db)
        {
            this.db = db;
        }
    }
}
```

{% hint style="info" %}
The attribute and the `Id` property appear to use two different constants, which looks like the mismatch this page warns against. They are the same value in two forms:

```csharp
public const string EXAMPLE_TASK_DI_ATTRIBUTES_ID = "a714aa26-87c4-4b42-bfbc-acdb2b184d53";
public readonly static Guid EXAMPLE_DI_TASK_GUID = Guid.Parse(EXAMPLE_TASK_DI_ATTRIBUTES_ID);
```

An attribute argument has to be a compile-time constant, and `Guid` is not one — so the attribute takes the string and the property takes the parsed value. Deriving the second from the first, as above, is what keeps them from drifting apart.
{% endhint %}

Note the constructor: this task takes an `AppDbContext`. That works because the task is registered through DI, which is where the dependency comes from. See [Background tasks](README.md#through-di).
