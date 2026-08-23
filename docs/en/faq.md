---
description: Problems people run into most often, and what fixes them.
---

# F.A.Q.

## The bot fails to start with "404 not found"

The token is not valid. Check that it was copied from BotFather in full, with no stray whitespace, and that it still belongs to an existing bot.

## The bot ignores inline commands, or does not respond at all

Several people have hit this and the fix each time was to regenerate the bot's token in BotFather with **/revoke**, then use the new one.

If only *some* commands are ignored, check the **BotId** instead: a handler attribute with a bot id that does not match the one on the builder is simply never called.

## "Unable to find package Telegram.Bot with version xxx"

From version 20 the Telegram.Bot team published to their own feed at `https://nuget.voids.site/`, and NuGet could not find the package on nuget.org.

This is no longer relevant — from version 22 the package is back on nuget.org, and PRTelegramBot 1.0.0 uses Telegram.Bot 22.10.2.1.

If you are pinned to an older version and hit this, add the extra source once:

```sh
dotnet nuget add source https://nuget.voids.site/v3/index.json -n voids
```

In Visual Studio the same setting lives under **Tools → Options → NuGet Package Manager → Package Sources**.

## A callback button does nothing, and the log shows a JsonException

Something is reading the callback data as a different type than the button carries. `EntityTCommand<long>` and `EntityTCommand<string>` serialise the identifier differently, so a handler expecting one and a button sending the other cannot be parsed:

```
System.Text.Json.JsonException: The JSON value could not be converted to System.String. Path: $.d.1
```

The converter catches that, logs it and returns `null`, so the handler quietly does nothing. Check that the type in `context.GetCommandByCallbackOrNull<T>()` matches the type used when the button was built.

## Where do I report a bug or ask a question?

Questions go to the [Telegram chat](https://t.me/prethinkdev). Bugs and feature requests go to [GitHub issues](https://github.com/prethink/PRTelegramBot/issues).
