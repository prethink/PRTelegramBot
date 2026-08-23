---
description: Letting a command run only for users who hold the right privileges.
---

# Restricted access to commands

The framework can gate a command behind a set of privileges. It deliberately does not decide *what* your privileges are or *where* they come from — you define the set, you look the user up, and the framework only asks you the question at the right moment.

## 1. Define the privileges

Declare an enum marked with `[Flags]`. If that attribute is new to you, see the [documentation](https://learn.microsoft.com/en-us/dotnet/api/system.flagsattribute).

```csharp
/// <summary>
/// User privileges.
/// </summary>
[Flags]
public enum UserPrivilege
{
    [Description("Guest")]
    Guest = 1,
    [Description("Registered")]
    Registered = 2,
    [Description("Administrator")]
    Admin = 4,
    [Description("VIP")]
    VIP = 8,
    [Description("Moderator")]
    Moderator = 16,
}
```

## 2. Mark the command

```csharp
/// <summary>
/// Runs for the bot with botId 0.
/// Runs when the user writes "Access check" in the chat.
/// Before the method runs, the privilege check event fires.
/// </summary>
[Access((int)(UserPrivilege.Guest | UserPrivilege.Registered))]
[ReplyMenuHandler("Access check")]
public static async Task ExampleAccess(IBotContext context)
{
    string msg = nameof(ExampleAccess);
    await MessageSender.Send(context, msg);
}
```

The `Access` attribute takes an `int` rather than your enum type. That is deliberate: it lets every project bring its own privilege enum. The flags are cast to `int` on the way in and back to flags on the way out.

## 3. Supply the user's privileges

Somewhere you need to answer "what is this user allowed to do". In a real bot that is a database lookup; here it is a stub:

```csharp
public static UserPrivilege LoadExampleFlagPrivilege(this Update update)
{
    return UserPrivilege.Registered;
}
```

## 4. Subscribe to the check

After creating the bot, subscribe to the privilege check event. Nothing runs until your handler calls `ExecuteMethod` — that call *is* the permission.

```csharp
telegram.Events.OnCheckPrivilege += OnCheckPrivilege;

/// <summary>
/// Privilege check for a user.
/// </summary>
public static async Task OnCheckPrivilege(PrivilegeEventArgs e)
{
    if (!e.Mask.HasValue)
    {
        // No access mask on the command — run it.
        await e.ExecuteMethod(e.Context);
        return;
    }

    // What the command requires.
    var requiredAccess = e.Mask.Value;

    // What the user has. Implement this however suits you —
    // a database lookup, a cache, a claim on the update.
    var userFlags = e.Context.Update.LoadExampleFlagPrivilege();

    if (requiredAccess.HasFlag(userFlags))
    {
        // Allowed — run the command.
        await e.ExecuteMethod(e.Context);
        return;
    }

    // Not allowed.
    await MessageSender.Send(e.Context, "You do not have access to this function.");
}
```

{% hint style="info" %}
Note what `requiredAccess.HasFlag(userFlags)` actually asks: *is every flag the user holds among the ones the command allows*. That works while a user carries a single privilege, but a user holding `Guest | Admin` fails a command marked `Guest | Registered`, even though they are a Guest.

If you want "the user holds at least one of the allowed privileges", test the intersection instead:

```csharp
if ((requiredAccess & userFlags) != 0)
```

This handler is your code, so the framework imposes neither reading — but the difference matters as soon as a user can hold more than one privilege at a time.
{% endhint %}

## Related

For the simpler case of "administrators only", see [Bot administrators](bot-administrators.md), which needs no privilege enum at all.
