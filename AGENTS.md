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
SECURITY.md / SECURITY.ru.md
CODE_OF_CONDUCT.md / CODE_OF_CONDUCT.ru.md
CONTRIBUTING.md / CONTRIBUTING.ru.md
Examples/*/README.md and README.ru.md
docs/en and docs/ru
```

The English files are primary. `README.md` is packed into the NuGet package, so **its links must be absolute** — relative links are dead on nuget.org. Russian files are GitHub-only and may use relative links.

The only Russian left in `.cs` files is in `PRTelegramBot.Tests/ControlTests/CalendarTests.cs`: day and month names asserted against `CultureInfo("ru")`. Translating them breaks the tests.

## CHANGELOG is mandatory

Every behaviour change, rename, removal or new public member goes into **both** `CHANGELOG.md` and `CHANGELOG.ru.md`, under the current version, in the matching section: `🔄 Breaking changes`, `🚀 New functionality`, `🧩 Common` or `🐞 Bugs`.

**Only the library is recorded there.** Changes under `PRTelegramBot.Tests/`, `Examples/` and `Templates/` do not go into the CHANGELOG, however serious the bug was — the file is read by someone consuming the NuGet package, and entries about demo projects bury the API changes they came for. Mention such a fix in your reply to the user instead. Entries about `README.md` and the package description do belong there: both ship inside the package.

Both files must stay structurally identical — same versions, same order, same subsection headings. Dates differ by design: English uses `August 23, 2026`, Russian uses `23.08.2026`.

Entries for **already released** versions are not rewritten after the fact. Only the entry for the version being prepared is edited.

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

Updating the Telegram.Bot version is not just a version bump. New `MessageType` and `UpdateType` values, new button kinds and new request parameters arrive with it, and each has to be wired up by hand. Nothing breaks when you miss one — the new kind of update simply never reaches anyone.

**`PRTelegramBot.Tests/CoverageGuards/` exists for exactly this.** Run it right after bumping the version:

```bash
dotnet test PRTelegramBot.Tests/PRTelegramBot.Tests.csproj --filter "FullyQualifiedName~CoverageGuards"
```

A failure there is not a broken test — it is a to-do list, and the message names what appeared:

| Guard | Catches |
| --- | --- |
| `EveryMessageTypeIsRoutedToAnEvent` | a `MessageType` with no entry in the dispatcher |
| `EveryUpdateTypeHasAnEvent` | an `UpdateType` with no `On{Type}Handle` event |
| `EveryMessageEventIsActuallyRaised` | an event declared but never registered, so it never fires |
| `EveryInlineButtonKindHasAWrapper` | a new `InlineKeyboardButton.With*` factory with no wrapper |
| `EveryReplyButtonKindHasABuilderMethod` | a new `KeyboardButton.With*` factory the builder cannot produce |
| `RequestHasNoUnreviewedParameters` | a new send/edit parameter nobody has looked at |

When a guard fails, do the work, then add the new value to the list inside the guard so it goes green. The lists are the record of what has been reviewed — keep them honest rather than silencing a failure.

These guards were written after the fact: ten message types, three update types, five send parameters, one inline button kind and one reply button kind had all been missed across 22.8–22.10. Declaring an event is not enough either — `OnPaidMessagePriceChangedHandle` sat declared and unwired for a full release.

They have since paid for themselves. The 22.10.3 upgrade tripped ten of them at once and the failures were the whole work list: one button factory (`WithDisabled`), one message type (`CommunityChatJoined`), one update type (`StoppedMessageGeneration`) and one send parameter (`EphemeralMessageParameters`, which replaced `ReceiverUserId` and `CallbackQueryId`). Bump the package, run the guards, and they will tell you what the release added.

## Packaging

`README.md`, `LICENSE` and `LogoBot.png` are packed from the repository root via `../` includes — single source of truth, no copies inside the project folder. Verify packaging after touching the csproj:

```bash
dotnet pack PRTelegramBot/PRTelegramBot.csproj -c Release -o ./artifacts
```

The csproj also carries three settings that are easy to break by accident:

| Property | Why it is there |
| --- | --- |
| `PublishRepositoryUrl` + `EmbedUntrackedSources` | Source Link. Consumers can step into this library's source from their debugger. Verify after a pack: the nuspec must contain a `<repository … commit="…"/>` element. |
| `DebugType=embedded` | Symbols live inside the DLL, so there is no separate `.pdb` to publish and nothing that can drift out of sync with the assembly. |
| `ContinuousIntegrationBuild`, guarded by `GITHUB_ACTIONS` | Normalises source paths so a CI build does not embed the builder's local ones. It is deliberately **not** set locally — that would break local debugging.

Do not add `<Deterministic>`: the SDK already defaults it to true, and a line that restates a default reads as a decision when it is not one.

## Before you finish

1. `dotnet build PRTelegramBot.sln` — zero errors
2. `dotnet test` — everything green
3. `dotnet pack` if the csproj or packed files changed
4. CHANGELOG updated in both languages
5. Documentation in `docs/` updated if documented behaviour changed — both languages where possible
6. Encoding of every touched file verified (CRLF+BOM for .cs, CRLF without BOM for docs)
7. `global.json` deleted

## Documentation

The user guide lives in **`docs/`**, in this repository, synchronised bidirectionally with GitBook:

| Directory | Space | State |
| --- | --- | --- |
| `docs/ru` | `PRTelegramBot_RU` | Complete, ~140 pages |
| `docs/en` | `PRTelegramBot_EN` | Being written |

Both are variants of one GitBook site. GitBook writes `docs.yaml` itself when the content mapping is saved — do not hand-craft it.

Rules for `docs/`:

- Files are **CRLF without a BOM** — the opposite of the `.cs` files. That is what GitBook exports, so match it.
- `SUMMARY.md` is the navigation. A page that is not listed there does not appear on the site, and a link there to a missing file breaks the build. Check both after adding a page.
- Image paths are relative to the space root, and the assets live in `docs/ru/.gitbook/assets`. Moving pages between directories breaks them; verify every `src=` still resolves.
- English pages that need a page not yet translated should link to the Russian one by absolute URL rather than leave a dead relative link.
- Code samples must compile. Several that did not shipped for years: a quick start subscribing to `telegram.OnLogCommon` (the event is `telegram.Events.OnCommonLog`), a cache example using an undeclared `update` variable, an `if` comparing a `long` to `null`. Check the identifiers against the source before writing them down.

While a documentation pull request is open, do not edit those pages in the GitBook web editor — GitBook is the source of truth for `docs/` and the two will conflict.

## Community files

`SECURITY.md`, `CODE_OF_CONDUCT.md` and `CONTRIBUTING.md` each have a Russian counterpart with a `.ru.md` suffix, matching `README.ru.md` and `CHANGELOG.ru.md`. Each pair opens with a language switcher line, and the English README links the English files while the Russian one links the Russian files.

Edit both halves of a pair, or say plainly which one you left behind.
