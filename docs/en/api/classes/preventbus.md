---
description: Event bus.
---

# PREventBus

Event bus.

## Methods

| Method | Description |
| --- | --- |
| `static void Subscribe(IPRGlobalSubscriber subscriber)` | Subscribes. |
| `static void Unsubscribe(IPRGlobalSubscriber subscriber)` | Unsubscribes. |
| `static void RaiseEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, IPRGlobalSubscriber` | Raises the event. |

