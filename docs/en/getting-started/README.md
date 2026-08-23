---
description: Everything you need to do to get your first bot running.
---

# Getting started

## Create a bot in BotFather

Every Telegram bot is registered through [@BotFather](https://t.me/botfather), the official Telegram service for that.

1. Open Telegram and find **BotFather**.
2. Start the conversation with **/start**.
3. Send **/newbot** to create a new bot.
4. Give the bot a name and a username when asked.
5. BotFather replies with an access token, something like `1234567890:ABCDEFGHIJKLMNOPQRSTUVXYZ`.
6. Copy it. That token is unique to your bot and is what authenticates every call to the Telegram API.

{% hint style="warning" %}
Keep the token out of version control. Use [user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets), environment variables, or a configuration file that is excluded from the repository.
{% endhint %}

## Install the package

The library targets .NET 6.0 and runs on any newer version.

```sh
dotnet new console -o MyBot
cd MyBot
dotnet add package PRTelegramBot
```

Or install **PRTelegramBot** from the NuGet package manager in your IDE. The package page is [here](https://www.nuget.org/packages/PRTelegramBot), and the source is on [GitHub](https://github.com/prethink/PRTelegramBot).

<figure><img src="../.gitbook/assets/изображение (27).png" alt="Right-click the project and choose the NuGet package manager"><figcaption>Right-click the project, then open the NuGet package manager (screenshots are from a Russian-language IDE)</figcaption></figure>

<figure><img src="../.gitbook/assets/изображение-1-1024x551.png" alt="Search for PRTelegramBot in the Browse tab and install the latest version"><figcaption>Search for <code>PRTelegramBot</code> in the Browse tab and install the latest version</figcaption></figure>

## Start the bot

```csharp
using PRTelegramBot.Builders;
using PRTelegramBot.Models.EventsArgs;

// A PRTelegramBot instance.
var telegram = new PRBotBuilder("Token").Build();

// Ordinary log messages.
telegram.Events.OnCommonLog += Telegram_OnLogCommon;
// Errors.
telegram.Events.OnErrorLog += Telegram_OnLogError;

// Start the bot.
await telegram.StartAsync();

async Task Telegram_OnLogError(ErrorLogEventArgs e)
{
    // Handle errors.
}

async Task Telegram_OnLogCommon(CommonLogEventArgs e)
{
    // Handle log messages.
}
```

<figure><img src="../.gitbook/assets/изображение-2.png" alt="The console shows the bot starting and the log events firing"><figcaption>What a started bot looks like in the console</figcaption></figure>

Everything the builder can configure — admins, white lists, middleware, converters, background tasks, webhook settings — is described on the [PRBotBuilder](../prbotbuilder.md) page.

## Add a command

A handler is an ordinary method marked with an attribute. Nothing registers it by hand — the framework finds it by reflection when the bot starts, so adding a command means adding a method.

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

public static class Commands
{
    // Runs when the user sends /start.
    [SlashHandler("/start")]
    public static async Task Start(IBotContext context)
    {
        await MessageSender.Send(context, "Hello, World!");
    }

    // Runs when the message text is exactly "Ping", ignoring case.
    [ReplyMenuHandler("Ping")]
    public static async Task Ping(IBotContext context)
    {
        await MessageSender.Send(context, "Pong");
    }
}
```

Run the project, send `/start` to your bot, and it answers.

By default updates arrive through [polling](https://core.telegram.org/bots/faq#how-do-i-get-updates), which needs no public address and is the quickest way to start developing. Running behind a public URL instead is described under [Webhook](webhook/).

## Several bots in one project

One project can run any number of bots. They are told apart by **BotId**, which you set on the builder and repeat on the handler attributes.

You might run five bots that all do the same thing, or five that each do something different — both work.

## Examples

| Example | What it shows |
| --- | --- |
| [Console](https://github.com/prethink/PRTelegramBot/tree/master/Examples/ConsoleExample) | Most of the framework in one place: commands of every kind, menus, events, middleware, background tasks. Start here. |
| [ASP.NET](https://github.com/prethink/PRTelegramBot/tree/master/Examples/AspNetExample) | A bot inside ASP.NET Core with everything resolved through dependency injection. Polling. |
| [ASP.NET webhook](https://github.com/prethink/PRTelegramBot/tree/master/Examples/AspNetWebHookExample) | Two bots on a single webhook endpoint, told apart by their secret token. |
