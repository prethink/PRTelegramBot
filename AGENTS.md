# AGENTS.md

Working notes for AI agents on PRTelegramBot. Everything here was learned the hard way — ignoring it produces changes that look right and are not.

## What this project is

A .NET framework for building Telegram bots on top of [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot). Telegram.Bot is the transport; this repository adds command routing, menus, events, middleware, DI, background tasks and step-by-step flows.

| Project | Target | Purpose |
| --- | --- | --- |
| `PRTelegramBot` | net6.0 | the library, published to NuGet |
| `PRTelegramBot.Tests` | net8.0 | NUnit + FluentAssertions + Moq |
| `Examples/ConsoleExample` | net7.0 | most features in one bot |
| `Examples/AspNetExample` | net8.0 | ASP.NET Core with DI, polling |
| `Examples/AspNetWebHookExample` | net8.0 | two bots on one webhook endpoint |
| `Templates/FastBotTemplate` | — | quick-start template |

## Build

**`dotnet build` fails on the default SDK.** SDK 10.0.400-preview crashes with `Fatal error. Your Windows doesn't fully support CET.` Pin SDK 9 for the duration of the work, then remove the file:

```bash
echo '{ "sdk": { "version": "9.0.307", "rollForward": "latestFeature" } }' > global.json
```

`global.json` is not part of the repository. Delete it when you are done, and never commit it.

For readable output, force English: `$env:DOTNET_CLI_UI_LANGUAGE = 'en'`. Otherwise MSBuild prints Russian summaries that most log parsers miss.

## File encoding — read this before editing anything

Source files are **UTF-8 with BOM and CRLF line endings**. Six files deliberately end **without** a trailing newline.

`sed -i` and `awk` on this machine silently strip carriage returns and the BOM. A one-character fix turns into a whole-file diff. After any such edit:

```bash
sed -i 's/\r*$/\r/' "$file"                       # restore CRLF
head -c3 "$file" | grep -q $'\xef\xbb\xbf' || {   # restore BOM
  printf '\xef\xbb\xbf' > /tmp/b && cat "$file" >> /tmp/b && mv /tmp/b "$file"; }
```

Prefer the editing tools that preserve bytes over stream editors. Verify with:

```bash
printf "CR=%s LF=%s BOM=%s\n" "$(tr -cd '\r' < $f | wc -c)" "$(tr -cd '\n' < $f | wc -c)" "$(head -c3 $f | xxd -p)"
```

`CR` must equal `LF`, and `BOM` must be `efbbbf`.

Four files were once committed in CP1251 and have been converted. If a file shows mojibake, decode it rather than retyping it: `iconv -f CP1251 -t UTF-8`.

## Language

Code, comments, XML documentation and example texts are **English**. The repository keeps parallel Russian documentation:

```
README.md / README.ru.md
CHANGELOG.md / CHANGELOG.ru.md
Examples/*/README.md and README.ru.md
```

The English files are primary. `README.md` is packed into the NuGet package, so **its links must be absolute** — relative links are dead on nuget.org. Russian files are GitHub-only and may use relative links.

The only Russian left in `.cs` files is in `PRTelegramBot.Tests/ControlTests/CalendarTests.cs`: day and month names asserted against `CultureInfo("ru")`. Translating them breaks the tests.

## CHANGELOG is mandatory

Every behaviour change, rename, removal or new public member goes into **both** `CHANGELOG.md` and `CHANGELOG.ru.md`, under the current version, in the matching section: `🔄 Breaking changes`, `🚀 New functionality`, `🧩 Common` or `🐞 Bugs`.

Both files must stay structurally identical — same versions, same order, same subsection headings. Dates differ by design: English uses `August 22, 2026`, Russian uses `22.08.2026`.

When a change is breaking, say what a user has to do about it. The v0.8 entry is the model to follow: an explicit before → after table of signatures.

## Public API rules

The library ships to NuGet, so the public surface is a contract.

- PascalCase for every public member. Watch for a Cyrillic `С`/`А`/`О` sneaking into an identifier — it compiles and looks correct.
- Parameter names are part of the API: renaming one breaks callers using named arguments. Note it in the CHANGELOG.
- Every public member needs XML documentation. `GenerateDocumentationFile` is on, so gaps surface as CS1591.
- Do not leave empty public types as placeholders. An unfinished `Workflow` namespace shipped as empty public interfaces for three releases before it was removed.

## Nullability

`<Nullable>enable</Nullable>` is on, and the codebase is being brought in line gradually. The agreed approach:

- where `null` is genuinely possible, annotate honestly (`string?`, `OptionMessage? option = null`) — annotations are metadata and do not break callers;
- where `null` must not happen, add a guard and throw something diagnosable;
- use `!` only where an invariant holds that the compiler cannot express, and say which invariant in a comment. `Update.Type` determining which payload property is set is such a case.

Do not silence these warnings wholesale.

## Testing

NUnit with FluentAssertions. Run: `dotnet test PRTelegramBot.Tests/PRTelegramBot.Tests.csproj`.

The test project sees `internal` members through `<InternalsVisibleTo Include="PRTelegramBot.Tests" />` in the library csproj. This is what makes the cache and step handlers testable at all — both are keyed through `update.GetKeyMappingUserTelegram()`, which needs `update.AddTelegramClient(bot)` first.

Useful patterns already in place:

- `TestModels/BotClientMock.cs` — every `ITelegramBotClient` extension funnels into `SendRequest`, so mocking that one method captures the request object a service built. This is how the sending layer is tested without a network.
- `PRBotDummy` — a bot instance that does not register itself in `BotCollection`.
- Caches, steps, the event bus and the update↔bot map all live in **static** state. Tests must use unique chat and update ids and clean up in `[TearDown]`, or they will interfere with each other.

Do not write a test that only calls a method without asserting. A placeholder like that once counted toward the suite and hid the fact that the cache had no coverage at all.

Coverage is measured with `coverlet.collector`:

```bash
dotnet test PRTelegramBot.Tests/PRTelegramBot.Tests.csproj --collect:"XPlat Code Coverage"
```

## Keeping up with Telegram.Bot

Updating the Telegram.Bot version is not just a version bump. New `MessageType` and `UpdateType` values arrive with it, and events for them have to be added by hand. Several were missed across 22.8–22.10 and only surfaced when the enums were diffed against the dispatcher.

After every update, check all three:

```bash
# message types with no handler
grep -oE 'TypeMessage\.Add\(MessageType\.[A-Za-z]+' \
  PRTelegramBot/Core/UpdateDispatchers/MessageUpdateDispatcher.cs | sed 's/.*MessageType\.//' | sort -u
# update types with no route
grep -oE 'UpdateType\.[A-Za-z]+' PRTelegramBot/Core/Handler.cs | sed 's/.*UpdateType\.//' | sort -u
# then compare both against the enums in Telegram.Bot.xml
```

Declaring an event is not enough — it must be wired into the dispatcher, or it never fires. `OnPaidMessagePriceChangedHandle` sat declared and unwired for a full release.

Also check whether `SendMessageRequest` and friends gained parameters that `OptionMessage` does not expose yet.

## Packaging

`README.md`, `LICENSE` and `LogoBot.png` are packed from the repository root via `../` includes — single source of truth, no copies inside the project folder. Verify packaging after touching the csproj:

```bash
dotnet pack PRTelegramBot/PRTelegramBot.csproj -c Release -o ./artifacts
```

## Before you finish

1. `dotnet build TelegramBotTemplate.sln` — zero errors
2. `dotnet test` — everything green
3. `dotnet pack` if the csproj or packed files changed
4. CHANGELOG updated in both languages
5. Encoding of every touched file verified (CRLF, BOM)
6. `global.json` deleted

## Documentation outside this repository

The user guide lives on GitBook: <https://prethink.gitbook.io/prtelegrambot/>. It is currently Russian-only and is not part of this repository, so changes here do not update it. When a change affects documented behaviour, say so — it has to be carried over by hand.
