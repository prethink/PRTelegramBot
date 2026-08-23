---
description: Utilities for working with groups.
---

# GroupUtils

Utilities for working with groups.

## Methods

| Method | Description |
| --- | --- |
| `static async Task<bool> IsGroupMember(IBotContext context, long groupId, long userId)` | Checks whether the user is a member of the group. |
| `static async Task<bool> IsGroupAdmin(IBotContext context, long groupId, long userId)` | Checks whether the user is an administrator of the group. |
| `static async Task<bool> IsGroupCreator(IBotContext context, long groupId, long userId)` | Checks whether the user is the creator of the group. |

