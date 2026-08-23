---
description: Walking a user through a sequence of questions, one message at a time.
---

# Step-by-step commands

Some things cannot be asked in one message. To collect a name and then a date of birth, the bot has to remember where the user is in the conversation and route their next message to the right place.

That is what **`StepTelegram`** does: it registers the method that should handle the user's *next* message, and it carries a cache between the steps.

## The idea

```
"stepstart"  →  register StepOne
next message →  StepOne runs, registers StepTwo
next message →  StepTwo runs, registers StepThree
next message →  StepThree runs, clears the handler
```

Only the user who started the sequence is affected, and only until it is cleared or expires.

## Somewhere to keep the answers

Implement `ITelegramCache` with the fields you are collecting:

```csharp
public class StepCache : ITelegramCache
{
    public string Name { get; set; }
    public string BirthDay { get; set; }

    public bool ClearData()
    {
        this.BirthDay = string.Empty;
        this.Name = string.Empty;
        return true;
    }
}
```

## Registering the steps

```csharp
/// <summary>
/// Step-by-step command execution.
/// </summary>
public class ExampleStepCommand
{
    /// <summary>
    /// Write "stepstart" in the chat.
    /// Registers the first step.
    /// </summary>
    [ReplyMenuHandler("stepstart")]
    public static async Task StepStart(IBotContext context)
    {
        string msg = "Testing step-by-step execution\nWrite your name";

        // Register the handler for the next message, together with the cache.
        context.RegisterStepHandler(new StepTelegram(StepOne, new StepCache()));

        await MessageSender.Send(context, msg);
    }

    /// <summary>
    /// Runs on the user's next message, whatever it says.
    /// </summary>
    public static async Task StepOne(IBotContext context)
    {
        string msg = $"Step 1 - your name is {context.Update.Message.Text}" +
                     $"\nEnter your date of birth";

        // The handler currently registered for this user.
        var handler = context.GetStepHandler<StepTelegram>();

        // Keep the answer.
        handler!.GetCache<StepCache>().Name = context.Update.Message.Text;

        // Register the step after this one.
        handler.RegisterNextStep(StepTwo);

        await MessageSender.Send(context, msg);
    }

    /// <summary>
    /// Runs on the next message again.
    /// </summary>
    public static async Task StepTwo(IBotContext context)
    {
        string msg = $"Step 2 - date of birth {context.Update.Message.Text}" +
                     $"\nWrite anything to see the result";

        var handler = context.GetStepHandler<StepTelegram>();
        handler!.GetCache<StepCache>().BirthDay = context.Update.Message.Text;

        // This step must be answered within five minutes of being registered.
        handler.RegisterNextStep(StepThree, DateTime.Now.AddMinutes(5));

        var option = new OptionMessage();

        // An otherwise empty reply keyboard carrying a "Main menu" button.
        // A registered command wins over the next step, so this button is a way out.
        option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(
            1, new List<string>(), true, "Main menu");

        await MessageSender.Send(context, msg, option);
    }

    /// <summary>
    /// The last step.
    /// </summary>
    public static async Task StepThree(IBotContext context)
    {
        var handler = context.GetStepHandler<StepTelegram>();
        var cache = handler!.GetCache<StepCache>();

        string msg = $"Step 3 - result: name {cache.Name}, date of birth {cache.BirthDay}" +
                     $"\nThe sequence has been cleared.";

        // Nothing further is expected — drop the handler.
        context.ClearStepUserHandler();

        await MessageSender.Send(context, msg);
    }
}
```

The methods after the first carry **no attribute**. They are not commands; they are reached only because the previous step registered them.

## Getting out of a sequence

A user is not trapped. Any registered command still wins over the pending step, so writing a command that exists — or pressing a menu button — abandons the sequence and runs that command instead.

To check for, or deliberately ignore, a pending step:

```csharp
/// <summary>
/// Runs even when a next step is pending, and abandons it.
/// </summary>
[ReplyMenuHandler("ignorestep")]
public static async Task IgnoreStep(IBotContext context)
{
    string msg = context.HasStepHandler()
        ? "The next step was ignored"
        : "There was no next step";

    await MessageSender.Send(context, msg);
}
```

`IgnoreBasicCommands` on `StepTelegram` inverts this: set it, and the step wins over commands, so the user has to finish or wait for the timeout. Use it sparingly — it is the difference between a wizard and a trap.

## Expiry

`RegisterNextStep` takes an optional deadline, either as a `TimeSpan` from now or an absolute `DateTime`:

```csharp
handler.RegisterNextStep(StepThree, TimeSpan.FromMinutes(5));
handler.RegisterNextStep(StepThree, DateTime.Now.AddMinutes(5));
```

Once the deadline passes, the handler is cleared and the step returns `ExecuteStepResult.ExpiredTime` instead of running. Without a deadline the step waits indefinitely, which means a user who wanders off is still mid-wizard tomorrow.

## Methods

The extension methods live in [`StepExtension`](api/extension-methods/stepextension.md):

| Method | What it does |
| --- | --- |
| `context.RegisterStepHandler(StepTelegram handler)` | Starts a sequence for this user. |
| `context.GetStepHandler<T>()` | The handler currently registered, or null. |
| `context.HasStepHandler()` | Whether a step is pending. |
| `context.ClearStepUserHandler()` | Ends the sequence. |

On `StepTelegram` itself:

| Member | What it does |
| --- | --- |
| `RegisterNextStep(Func<IBotContext, Task> nextStep)` | The next step, with no deadline. |
| `RegisterNextStep(nextStep, TimeSpan addTime)` | With a deadline relative to now. |
| `RegisterNextStep(nextStep, DateTime? expiredTime)` | With an absolute deadline. |
| `RegisterNextStep(nextStep, expiredTime, bool ignoreBasicCommands)` | As above, and whether the step outranks commands. |
| `GetCache<T>()` | The cache carried through the sequence. |
| `CanExecute()` | Whether the step is still within its deadline. |

{% hint style="warning" %}
The pending step lives in memory in the bot process. It does not survive a restart, and it is not shared between instances. A user halfway through a wizard when the bot restarts simply finds their next message treated as an ordinary one.
{% endhint %}
