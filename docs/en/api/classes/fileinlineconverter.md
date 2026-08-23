---
description: Converter that stores inline menus in files. Stores the temporary data as files named '{bot id}-{user id}-{command id}'. The files are saved in the 'InlineCallbacks' folder inside the application directory. A different folder name can be given when the instance is created.
---

# FileInlineConverter

Converter that stores inline menus in files. Stores the temporary data as files named "{bot id}-{user id}-{command id}". The files are saved in the "InlineCallbacks" folder inside the application directory. A different folder name can be given when the instance is created.

Inherits `IInlineMenuConverter`.

## Methods

| Method | Description |
| --- | --- |
| `string GenerateCallbackData(InlineCallback inlineCallback)` |  |
| `string GenerateCallbackData<T>(InlineCallback<T> inlineCallback) where T : TCommandBase` |  |
| `InlineCallback? GetCommandByCallbackOrNull(string data)` |  |
| `InlineCallback<T>? GetCommandByCallbackOrNull<T>(string data) where T : TCommandBase` |  |

## Constructors

| Constructor | Description |
| --- | --- |
| `FileInlineConverter(string path)` | Constructor. |
| `FileInlineConverter()` | Constructor. Uses the default "InlineCallbacks" folder. |

