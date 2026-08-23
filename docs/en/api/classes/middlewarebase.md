---
description: Base middleware handler.
---

# MiddlewareBase

Base middleware handler.

## Properties

| Property | Description |
| --- | --- |
| `abstract int ExecutionOrder { get; }` | The order the middleware runs in within the pipeline. A lower value means a higher priority and earlier execution. |

## Methods

| Method | Description |
| --- | --- |
| `virtual async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)` | Executes the next asynchronous middleware handler. |
| `virtual async Task InvokeOnPostUpdateAsync(IBotContext context)` | Executes the previous asynchronous middleware handler. |
| `void SetNext(MiddlewareBase next)` | Sets the next handler. |
| `void SetNext(MiddlewareBase next, MiddlewareBase previous)` | Sets the next handler. |
| `void SetPrevious(MiddlewareBase previous)` | Sets the previous handler. |

