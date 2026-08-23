---
description: Keeping a task's schedule in a class of its own.
---

# A task with a separate metadata class

Here the work and the schedule live in **two classes**. That separates the responsibilities and lets a configuration be reused independently of the implementation that runs under it.

The two are paired by `Id` — and this is the arrangement where that matters most, because nothing in the language ties them together. If the two identifiers stop matching, the task quietly never runs.

## The metadata

```csharp
using AspNetExample.Models;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;

namespace AspNetExample.BackgroundTasks
{
    public class ExampleBackgroundTasksMetadata : IPRBackgroundTaskMetadata
    {
        public HashSet<long> BotIds { get; } = new HashSet<long>();

        public Guid Id => Constants.EXAMPLE_TASK_WITHOUT_METADATA;

        public string Name => nameof(ExampleBackgroundTasksMetadata);

        public int? InitialDelaySeconds => 1;

        public int? RepeatSeconds => 1;

        public int? MaxErrorAttempts => PRConstants.INFINITY;

        public int? MaxRepeatCount => -1;
    }
}
```

## The task

```csharp
using AspNetExample.Models;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;
using System.Diagnostics;

namespace AspNetExample.BackgroundTasks
{
    public class ExampleWithoutMetadataBackgroundTasks : IPRBackgroundTask
    {
        public Guid Id => Constants.EXAMPLE_TASK_WITHOUT_METADATA;

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Execute {nameof(ExampleWithoutMetadataBackgroundTasks)}");
            return Task.CompletedTask;
        }

        public Task<bool> CanExecute()
        {
            return Task.FromResult(true);
        }

        public Task Initialize(PRBotBase bot)
        {
            return Task.CompletedTask;
        }
    }
}
```

Both refer to `Constants.EXAMPLE_TASK_WITHOUT_METADATA`. Using a shared constant rather than writing the GUID twice is the whole defence against them diverging — do the same in your own code.

## Registering

The task is resolved from DI; the metadata is handed to the builder:

```csharp
builder.Services.AddTransient<IPRBackgroundTask, ExampleWithoutMetadataBackgroundTasks>();

var bot = new PRBotBuilder("token")
    .SetServiceProvider(serviceProvider)
    .AddBackgroundTaskMetadata(new ExampleBackgroundTasksMetadata())
    .Build();
```

This is why `AddBackgroundTaskMetadata` exists as a separate method: a DI-resolved task cannot be handed to the builder as an instance, so only its schedule goes there.

## Which arrangement to choose

| | Use when |
| --- | --- |
| [Attribute](metadata-attribute.md) | The schedule is fixed and known at compile time. Shortest to write. |
| [Both interfaces](metadata-interface.md) | The schedule has to be computed — read from configuration, different per environment. |
| Separate classes | The same schedule serves several tasks, or the task comes from DI and you want its configuration kept apart from it. |
