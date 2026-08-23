---
description: Interface of the bot configuration provider.
---

# IBotConfigProvider

Interface of the bot configuration provider.

## Methods

| Method | Description |
| --- | --- |
| `void SetConfigPath(string configPath)` | Sets the path to the configuration file. |
| `TOptions GetOptions<TOptions>() where TOptions : class` | Gets the parameters from the configuration file. |
| `TReturn GetValue<TReturn>(string optionName)` | Gets the value of the parameter. |
| `Dictionary<string, string> GetKeysAndValues()` | Gets a key-value dictionary from the configuration file. |
| `Dictionary<string, string> GetKeysAndValuesByOptions<TOptions>() where TOptions : class` | Gets the key-value pairs from the configuration file's parameters. |

