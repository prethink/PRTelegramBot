---
description: Interface of the user management manager.
---

# IUserManager

Interface of the user management manager.

## Properties

| Property | Description |
| --- | --- |
| `long Count { get; }` | Number of users. |

## Methods

| Method | Description |
| --- | --- |
| `Task<bool> Reload()` | Reloads the users. |
| `Task<bool> Initialize()` | Initializes the manager. |
| `Task<bool> AddUser(long userId)` | Adds a user. |
| `Task<bool> AddUsers(params long[] userIds)` | Adds users. |
| `Task<List<long>> GetUsersIds()` | Gets the user identifiers. |
| `Task<bool> RemoveUser(long userId)` | Removes a user from the list. |
| `Task<bool> HasUser(long userId)` | Checks whether the user is in the list. |

