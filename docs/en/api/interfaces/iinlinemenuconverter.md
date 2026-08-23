---
description: Interface of the InlineCallback converter.
---

# IInlineMenuConverter

Interface of the InlineCallback converter.

## Methods

| Method | Description |
| --- | --- |
| `InlineCallback GetCommandByCallbackOrNull(string callbackData)` | Converts the data into a command. |
| `InlineCallback<T> GetCommandByCallbackOrNull<T>(string callbackData)` | Converts the data into a command. |
| `string GenerateCallbackData(InlineCallback inlineCallback)` | Generates the callbackData from an InlineCallback. |
| `string GenerateCallbackData<T>(InlineCallback<T> inlineCallback)` | Generates the callbackData from an InlineCallback. |

