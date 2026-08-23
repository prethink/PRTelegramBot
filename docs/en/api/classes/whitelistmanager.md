---
description: White list manager.
---

# WhiteListManager

White list manager.

Inherits `IWhiteListManager`.

## Fields

| Field | Description |
| --- | --- |
| `long Count => users.Count` |  |
| `WhiteListSettings Settings` |  |

## Methods

| Method | Description |
| --- | --- |
| `Task<bool> AddUser(long userId)` |  |
| `Task<bool> AddUsers(params long[] userIds)` |  |
| `Task<List<long>> GetUsersIds()` |  |
| `Task<bool> HasUser(long userId)` |  |
| `Task<bool> Reload()` |  |
| `Task<bool> RemoveUser(long userId)` |  |
| `void SetSettings(WhiteListSettings whiteListSettings)` |  |
| `Task<bool> Initialize()` |  |

