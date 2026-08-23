---
description: Broadcasting events between parts of an application without direct references.
---

# Event bus

An **event bus** lets components exchange messages without knowing about each other. A publisher raises an event; whoever subscribed receives it. Neither side holds a reference to the other, so the coupling that would otherwise build up between them never forms.

Two pieces make it work:

* [`PREventBus`](https://prethink.gitbook.io/prtelegrambot/ru/api/klassy/preventbus) — a static class that registers subscribers, removes them, and raises events to them.
* [`IPRGlobalSubscriber`](https://prethink.gitbook.io/prtelegrambot/ru/api/interfeisy/iprglobalsubscriber) — the base interface every subscriber derives from.

A class that wants to receive events implements an interface **derived from** `IPRGlobalSubscriber` rather than that interface directly. Any number of subscribers may share a type — all of them receive the event.

## What makes this different from the bot's events

The [events](../events/) on `bot.Events` describe things Telegram did: a photo arrived, a command was not found. They belong to one bot instance and the framework raises them.

The event bus is for things **your application** does, and it is process-wide. A payment webhook in your ASP.NET controller — nothing to do with Telegram — can raise an event that a bot component reacts to, without either side referencing the other.

## Typed, not stringly

The bus does not raise abstract named events. It calls **methods on a subscriber interface**:

```csharp
public interface IPRTaskRunnerSubscriber : IPRGlobalSubscriber
{
    void StopEvent(IEnumerable<long> botIds, Guid taskId);
    void StopEvent(Guid taskId);
}
```

Which means the compiler is involved: the events are strongly typed, "find all references" works on them, and there is no way to raise an event nobody supports or to mistype its name.

## Raising an event

From anywhere in the application:

```csharp
PREventBus.RaiseEvent<IPRTaskRunnerSubscriber>(
    subscriber => subscriber.StopEvent(taskId)
);
```

The bus finds every subscriber of that type, calls the method on each, and contains a failure in one subscriber so it does not take down the others.

## Subscribing

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

{% hint style="warning" %}
The bus is static, so it holds a reference to every subscriber for as long as they stay registered. A subscriber that is never removed is never collected. Implement `IDisposable` and unsubscribe there — see the worked example.
{% endhint %}

## Example

* [Using the event bus in PRBackgroundTaskRunner](task-runner-example.md)
