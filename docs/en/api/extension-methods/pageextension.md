---
description: Helps split the data into pages.
---

# PageExtension

Helps split the data into pages.

## Methods

| Method | Description |
| --- | --- |
| `static Task<PagedResult<T>> GetPaged<T>(this IEnumerable<T> query, int page, int pageSize)` | Outputs the data page by page. |

