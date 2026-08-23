---
description: >-
  Since version 0.5.0 reply, slash and inline handlers can be added and removed
  while the bot is running.
---

# Adding and removing commands at runtime

Attribute-marked handlers are discovered once, when the bot starts. Sometimes that is too early: a command that only exists for users who bought something, a set of commands loaded from a database, a feature switched on without a redeploy.

`bot.Register` adds and removes handlers on a running bot.

## Adding a command

Write the handler as a delegate:

```csharp
var method = async (IBotContext context) =>
{
    string message = "Message";
    await MessageSender.Send(context, message);
};
```

Then register it under a command name:

```csharp
// Reply
bot.Register.AddReplyCommand("Command name", method);

// Slash
bot.Register.AddSlashCommand("/Command name", method);

// Inline
bot.Register.AddInlineCommand(Enum.value, method);
```

{% hint style="warning" %}
**Command names must be unique.** A duplicate is rejected rather than silently replacing what was there.
{% endhint %}

Every method returns a `bool`: `true` when the command was added, `false` when it was not.

## Enum values must not collide

Inline commands travel as **integers**, so their values have to be unique across *every* enum in your project — not merely within one. Two enums that both start at 500 produce commands that cannot be told apart, and presses land on the wrong handler.

Values below 100 are reserved: the framework registers its own commands there.

```csharp
/// <summary>
/// Identifiers for the framework's own callback commands.
/// </summary>
[InlineCommand]
public enum THeader
{
    [Description(nameof(None))]
    None = 0,
    [Description(nameof(PickMonth))]
    PickMonth = 1,
    [Description(nameof(PickYear))]
    PickYear = 2,
    [Description(nameof(ChangeTo))]
    ChangeTo = 3,
    [Description(nameof(YearMonthPicker))]
    YearMonthPicker = 4,
    [Description(nameof(PickDate))]
    PickDate = 5,
    [Description(nameof(NextPage))]
    NextPage = 6,
    [Description(nameof(CurrentPage))]
    CurrentPage = 7,
    [Description(nameof(PreviousPage))]
    PreviousPage = 8,
}
```

### Good — ranges that do not overlap

The first enum occupies 500–504, the second 600–604. Nothing collides.

```csharp
public enum CustomTHeader
{
    [Description("Free VIP")]
    GetFreeVIP = 500,
    [Description("VIP for a day")]
    GetVipOneDay,
    [Description("VIP for a week")]
    GetVipOneWeek,
    [Description("VIP for a month")]
    GetVipOneMonth,
    [Description("VIP forever")]
    GetVipOneForever
}

public enum CustomTHeaderTwo
{
    [Description("Example 1")]
    ExampleOne = 600,
    [Description("Example 2")]
    ExampleTwo,
    [Description("Example 3")]
    ExampleThree,
    [Description("Pages example")]
    CustomPageHeader,
    [Description("Pages example 2")]
    CustomPageHeader2,
}
```

### Bad — overlapping values

Here `CustomTHeader` and `CustomTHeaderTwo` both start at 500, and `CustomTHeaderThree` starts at 501, landing inside both. Several distinct commands now share a number, and the routing has no way to tell them apart.

```csharp
public enum CustomTHeader
{
    GetFreeVIP = 500,       // 500
    GetVipOneDay,           // 501
    GetVipOneWeek,          // 502
    GetVipOneMonth,         // 503
    GetVipOneForever        // 504
}

public enum CustomTHeaderTwo
{
    ExampleOne = 500,       // 500 — collides with GetFreeVIP
    ExampleTwo,             // 501
    ExampleThree,           // 502
    CustomPageHeader,       // 503
    CustomPageHeader2       // 504
}

public enum CustomTHeaderThree
{
    CustomPageHeader = 501, // 501 — collides with both of the above
    CustomPageHeader2       // 502
}
```

Nothing warns you about this. It shows up as a button doing the wrong thing, which is a long way from the cause — so pick a range per enum and leave a gap between them.

## Removing a command

```csharp
// Reply
bot.Register.RemoveReplyCommand("Command name");

// Slash
bot.Register.RemoveSlashCommand("Command name");

// Inline
bot.Register.RemoveInlineCommand(Enum.value);
```

These return a `bool` too: `true` when the command was removed, `false` when it was not.

{% hint style="info" %}
Registrations live in memory and do not survive a restart. Anything added at runtime has to be added again when the bot starts — usually from wherever the decision to add it came from in the first place.
{% endhint %}
