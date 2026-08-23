---
description: Interface for checking commands before they run.
---

# IInternalCheck

Interface for checking commands before they run.

## Methods

| Method | Description |
| --- | --- |
| `Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler)` | Runs a check before the command is executed. |

