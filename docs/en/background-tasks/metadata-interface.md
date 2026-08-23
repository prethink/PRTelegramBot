---
description: A task that carries its own schedule by implementing both interfaces.
---

# A task implementing the metadata interface

Here one class implements **both** `IPRBackgroundTask` and `IPRBackgroundTaskMetadata`, so the work and its schedule live together without an attribute.

`IPRBackgroundTask` supplies the behaviour:

* `Initialize` — called once, with the bot instance;
* `CanExecute` — asked before each run;
* `ExecuteAsync` — the work.

`IPRBackgroundTaskMetadata` supplies the schedule:

* `Id` — the identifier;
* `Name` — the name used in logs;
* `InitialDelaySeconds` — delay before the first run;
* `RepeatSeconds` — the interval;
* `BotIds` — which bots it belongs to; empty means all of them;
* `MaxErrorAttempts` — how many failures to tolerate;
* `MaxRepeatCount` — how many runs at most.

The task below starts after one second, repeats every second without a run limit, tolerates any number of errors, and belongs to every bot.

```csharp
using AspNetExample.Models;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;
using System.Diagnostics;

namespace AspNetExample.BackgroundTasks
{
    public class ExampleWithMetadataBackgroundTasks : IPRBackgroundTask, IPRBackgroundTaskMetadata
    {
        public Guid Id => Constants.EXAMPLE_TASK_WITH_METADATA;

        public string Name => nameof(ExampleWithMetadataBackgroundTasks);

        public int? InitialDelaySeconds => 1;

        public int? RepeatSeconds => 1;

        public HashSet<long> BotIds => new HashSet<long>();

        public int? MaxErrorAttempts => PRConstants.INFINITY;

        public int? MaxRepeatCount => -1;

        public Task<bool> CanExecute()
        {
            return Task.FromResult(true);
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Execute {nameof(ExampleWithMetadataBackgroundTasks)}");
            return Task.CompletedTask;
        }

        public Task Initialize(PRBotBase bot)
        {
            return Task.CompletedTask;
        }
    }
}
```

Because both `Id` values come from the same property, the two halves cannot drift apart — the pairing problem the attribute version has to be careful about does not arise here at all.

The advantage over the attribute is that these are **properties, not constants**: they can be computed. A schedule read from configuration, or an interval that differs between development and production, is expressible here and is not expressible in an attribute.
