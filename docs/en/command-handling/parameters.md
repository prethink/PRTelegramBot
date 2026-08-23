---
description: The parameters shared by the handler attributes.
---

# Parameters

The handler attributes take the same handful of parameters. This page describes them once.

## BotId

Which bot the command belongs to. Set it to `-1` and the command works for **every** bot in the project.

Omit it and the command is bound to the bot whose `BotId` is `0` — which is also the default a bot gets when you never call `SetBotId`. That is why the simplest setup needs no ids anywhere.

## Commands

The commands the bot reacts to.

* For `ReplyMenuHandlerAttribute`, `ReplyMenuDynamicHandlerAttribute` and `SlashHandlerAttribute` these are `string`s.
* For `InlineCallbackHandlerAttribute` it is an `enum` value.

## CommandComparison

How the command text is matched: the message must **equal** the command, or merely **contain** it.

It applies to `ReplyMenuHandlerAttribute`, `ReplyMenuDynamicHandlerAttribute` and `SlashHandlerAttribute`.

Defaults differ by attribute:

| Attribute | Default |
| --- | --- |
| `ReplyMenuHandlerAttribute` | `Equals` |
| `ReplyMenuDynamicHandlerAttribute` | `Equals` |
| `SlashHandlerAttribute` | `Contains` |

## StringComparison

The standard .NET [`StringComparison`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison), controlling case and culture sensitivity. Used by `ReplyMenuHandlerAttribute`, `ReplyMenuDynamicHandlerAttribute` and `SlashHandlerAttribute`; `InlineCallbackHandlerAttribute` ignores it, since it matches on an enum rather than text.

The default is `OrdinalIgnoreCase`, so commands are matched without regard to case.

Further reading: [StringComparison](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison) and [best practices for comparing strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings).

## OptionMessage

A helper class holding the settings used when sending a message to Telegram — keyboards, parse mode, reply parameters, media options and the rest. It is passed as the last argument to `MessageSender.Send` and friends.
