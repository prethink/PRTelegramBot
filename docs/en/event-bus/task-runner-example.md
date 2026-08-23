---
description: How PRBackgroundTaskRunner subscribes to the bus, and manages its own lifetime.
---

# Using the event bus in PRBackgroundTaskRunner

`PRBackgroundTaskRunner` is a real subscriber inside the framework, and it shows the whole pattern: a typed subscriber interface, subscribing on construction, and unsubscribing on disposal.

## The subscriber interface

On top of `IPRGlobalSubscriber`, specialised interfaces describe particular scenarios. `IPRTaskRunnerSubscriber` covers control of background tasks:

```csharp
public interface IPRTaskRunnerSubscriber : IPRGlobalSubscriber
{
    void StopEvent(IEnumerable<long> botIds, Guid taskId);
    void StopEvent(Guid taskId);
}
```

It derives from `IPRGlobalSubscriber`, and its methods are exactly the events that can be raised — the interface *is* the contract.

## The implementation

```csharp
public sealed class PRBackgroundTaskRunner
    : IPRBackgroundTaskRunner, IPRTaskRunnerSubscriber
```

Implementing the subscriber interface is what makes the class eligible for the bus and obliges it to provide the methods that will be called through it.

## Subscribing on construction

```csharp
public PRBackgroundTaskRunner(PRBotBase bot)
{
    this.bot = bot;
    this.Subscribe();
}
```

The runner starts reacting the moment it exists, and nobody using it has to remember to call `Subscribe()`.

```csharp
public void Subscribe()
{
    PREventBus.Subscribe(this);
}

public void Unsubscribe()
{
    PREventBus.Unsubscribe(this);
}
```

## Unsubscribing on disposal

```csharp
public void Dispose()
{
    Unsubscribe();
}
```

This is the part not to skip. Because `PREventBus` is static, a subscriber that never unsubscribes is kept alive by it forever — and keeps receiving events after its work is finished. Pairing `Subscribe` in the constructor with `Unsubscribe` in `Dispose` guarantees that:

* the object stops receiving events once it is done;
* no dangling subscribers accumulate in the bus.

## Raising the event

Anything, anywhere, can now stop a task without holding a reference to any runner:

```csharp
PREventBus.RaiseEvent<IPRTaskRunnerSubscriber>(
    subscriber => subscriber.StopEvent(taskId)
);
```

Every runner in the process receives it. With several bots running, each has its own runner, and the overload taking `botIds` is what narrows the event to the ones you mean.
