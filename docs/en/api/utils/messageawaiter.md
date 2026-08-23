---
description: Sends a message before processing and deletes it afterwards.
---

# MessageAwaiter

Sends a message before processing and deletes it afterwards.

Inherits `IDisposable`.

## Methods

| Method | Description |
| --- | --- |
| `void Dispose()` |  |
| `async Task CreateAwaitMessage(string messageText)` | Sends a waiting message before the main data processing. |
| `async Task DeleteMessage()` | Deletes the message once all processing is done. |

## Constructors

| Constructor | Description |
| --- | --- |
| `MessageAwaiter(IBotContext context, long chatId)` | Constructor. |
| `MessageAwaiter(IBotContext context, string messageAwaiterText)` | Constructor. |

