**English** | [Русский](SECURITY.ru.md)

# Security Policy

## Supported versions

| Version | Supported |
| --- | --- |
| 1.0.x | ✅ |
| 0.9.x and earlier | ❌ |

Only the latest release receives security fixes. If you are on 0.9.x, see [Migrating to 1.0](https://prethink.gitbook.io/prtelegrambot/perekhod-na-1.0).

## Reporting a vulnerability

**Please do not open a public issue for a security problem.**

Report it privately through [GitHub Security Advisories](https://github.com/prethink/PRTelegramBot/security/advisories/new), or by writing to the maintainer directly at [@prethink](https://t.me/prethink) on Telegram.

Please include:

* what the problem is, and what an attacker could achieve with it;
* the version of PRTelegramBot you found it in;
* steps to reproduce it, or a minimal project that shows it;
* any thoughts you have on a fix.

## What happens next

* **Within 7 days** you will get an acknowledgement that the report arrived and an initial assessment.
* If the report is accepted, a fix is prepared privately and released as a patch version.
* You will be credited in the release notes unless you would rather not be.
* If the report is declined, you will be told why.

This is a project maintained in spare time, so please read those timelines as intent rather than a contractual guarantee. Serious issues are treated seriously and quickly.

## Scope

In scope: the `PRTelegramBot` library itself — anything under `PRTelegramBot/`.

Out of scope: the example projects under `Examples/` and `Templates/`. They are illustrations, not production code. Problems there are still worth reporting as ordinary issues, and they do get fixed — a webhook token validation flaw in the ASP.NET example was fixed in 1.0.0 — but they are not treated as vulnerabilities in a released package.

Also out of scope: vulnerabilities in [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot) or other dependencies. Report those to their maintainers; if a dependency issue affects this library, we will update the reference.

## Handling bot tokens

Most real-world incidents with Telegram bots are not library vulnerabilities — they are leaked tokens. A bot token is a full credential: anyone holding it controls the bot.

* Never commit a token. Use [user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets), environment variables, or a configuration file excluded from the repository.
* If a token is exposed, revoke it immediately with `/revoke` in [@BotFather](https://t.me/botfather).
* For webhook bots, always verify the `X-Telegram-Bot-Api-Secret-Token` header. It is the only thing proving a request came from Telegram.
