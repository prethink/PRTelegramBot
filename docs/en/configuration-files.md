---
description: Keeping button captions and message texts outside the code.
---

# Configuration files for bots

Each bot instance can remember the paths to its configuration files. Register them on the builder with `AddConfigPath("key", "path")`:

```csharp
var telegram = new PRBotBuilder("Token")
                    .AddConfigPath("Buttons", ".\\Configs\\buttons.json")
                    .AddConfigPath("Messages", ".\\Configs\\messages.json")
                    .Build();
```

The key is how you refer to the file later; the path is where it lives.

## Reading a value

```csharp
// BotConfigJsonProvider — the provider that reads JSON configuration files.
// string                — the type of the value you expect back.
// "Messages"            — the key the file was registered under.
// "MSG_EXAMPLE_TEXT"    — the key of the value inside that file.
string msg = context.GetConfigValue<BotConfigJsonProvider, string>("Messages", "MSG_EXAMPLE_TEXT");
```

The file itself is ordinary key–value JSON:

```json
{
  "MSG_EXAMPLE_TEXT": "Hello! Choose an option below.",
  "MSG_ACCESS_DENIED": "You do not have access to this function."
}
```

## Reading the path

Occasionally you want the path rather than a value from it — to open the file yourself, or to check it exists:

```csharp
var configPath = context.GetBotDataOrNull().Options.ConfigPaths["Messages"];
```

## Why bother

Two reasons, and the second is the one that matters later.

**Text changes without a rebuild.** Captions and messages are what change most often and matter least to the logic. Moving them into a file means a wording fix is an edit and a restart, not a release.

**It is how a bot gets translated.** Ship one set of files per language, register the right set at startup, and every string changes while the handlers stay untouched. The same idea drives [dynamic reply commands](command-handling/reply-commands/dynamic-reply-commands.md), where the *command* the user types comes from a file as well.

## Using your own format

`BotConfigJsonProvider` is the built-in provider and JSON is the default, but the provider is a type parameter — implement your own and pass it instead to read YAML, XML, or values from a database.

```csharp
string msg = context.GetConfigValue<MyYamlProvider, string>("Messages", "MSG_EXAMPLE_TEXT");
```

{% hint style="info" %}
Paths are resolved relative to the working directory, which is not always the folder containing the executable — under IIS or a Windows service they differ. If a file is not found in production but is found in development, that is usually why: pass an absolute path, or build one from `AppContext.BaseDirectory`.
{% endhint %}
