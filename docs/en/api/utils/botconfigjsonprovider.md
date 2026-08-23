---
description: Provider that works with json configuration files.
---

# BotConfigJsonProvider

Provider that works with json configuration files.

Inherits `IBotConfigProvider`.

## Methods

| Method | Description |
| --- | --- |
| `void SetConfigPath(string configPath)` |  |
| `TOptions GetOptions<TOptions>()` |  |
| `TReturn GetValue<TReturn>(string section)` |  |
| `Dictionary<string, string> GetKeysAndValues()` |  |
| `Dictionary<string, string> GetKeysAndValuesByOptions<T>()` |  |

## Constructors

| Constructor | Description |
| --- | --- |
| `BotConfigJsonProvider() { }` | Constructor. |
| `BotConfigJsonProvider(string configPath)` | Constructor. |

