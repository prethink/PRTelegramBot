---
description: Administrator manager.
---

# AdminListManager

Administrator manager.

Inherits `IAdminManager`.

## Fields

| Field | Description |
| --- | --- |
| `long Count => users.Count` |  |

## Methods

| Method | Description |
| --- | --- |
| `Task<bool> AddUser(long userId)` |  |
| `Task<bool> AddUsers(params long[] userIds)` |  |
| `Task<List<long>> GetUsersIds()` |  |
| `Task<bool> HasUser(long userId)` |  |
| `Task<bool> Initialize()` |  |
| `Task<bool> Reload()` |  |
| `Task<bool> RemoveUser(long userId)` |  |

