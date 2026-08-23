---
description: Utilities for working with access rights.
---

# AccessUtils

Utilities for working with access rights.

## Methods

| Method | Description |
| --- | --- |
| `static TEnum ReadFlags<TEnum>(int mask)` | Reads the flags. |
| `static int WriteFlags<TEnum>(TEnum flags)` | Writes the flags. |
| `static bool IsFlagsEnum<TEnum>()` | Checks whether the enum is a flags enum. |
| `static bool HasFlag<TEnum>(int mask, TEnum flag)` | Checks whether the access mask carries the required flag. |

