**English** | [Русский](CONTRIBUTING.ru.md)

# Contributing to PRTelegramBot

Thank you for considering it. Bug reports, fixes, features and documentation are all welcome.

This project follows the [Code of Conduct](CODE_OF_CONDUCT.md). By taking part, you agree to it.

## Before you start on something large

Open an issue first. A short conversation about the design costs far less than a rejected pull request, and it means the work lands the first time. For small fixes — a typo, a null check, an obviously wrong sample — just send the pull request.

## Reporting a bug

Open a [GitHub issue](https://github.com/prethink/PRTelegramBot/issues) with:

* what you expected and what happened instead;
* the version of PRTelegramBot;
* the smallest piece of code that reproduces it.

For a security problem, do **not** open a public issue — see [SECURITY.md](SECURITY.md).

## Building

The library targets .NET 6.0; the tests and examples target .NET 8.0.

```sh
dotnet build PRTelegramBot.sln
dotnet test PRTelegramBot.Tests/PRTelegramBot.Tests.csproj
```

The full suite must be green before a pull request is merged.

## Writing code

* Match the style of the file you are editing. There is no formatter to argue with; consistency with the surrounding code wins.
* XML documentation comments are **in English**, on every public member. They ship in the NuGet package and appear in people's IntelliSense.
* Source files are UTF-8 with a BOM and CRLF line endings. Please do not let an editor change that — it turns a one-line fix into a whole-file diff.
* New public API needs tests. Bug fixes need a test that fails before the fix.

## Tests

Tests live in `PRTelegramBot.Tests` and use NUnit with FluentAssertions.

`PRTelegramBot.Tests/CoverageGuards` deserves a special mention: those tests compare the framework's wiring against Telegram.Bot by reflection, and they fail when Telegram.Bot gains something the framework has not caught up with. If one of them fails after a dependency bump, that is the point — it is telling you what to add, not something to silence.

## The changelog

`CHANGELOG.md` and `CHANGELOG.ru.md` record changes to **the library only**. Changes to tests, examples and templates are not listed there — the changelog is read by people consuming the NuGet package.

The two files must stay structurally identical: same versions, same section headings, same entries in the same order. Dates are written `August 23, 2026` in the English file and `23.08.2026` in the Russian one.

Breaking changes go first, and each entry says what broke and what to do instead.

## Documentation

The documentation lives in `docs/`, synchronised with GitBook:

* `docs/ru` — Russian, currently the complete version;
* `docs/en` — English, being written.

If your change alters behaviour that is documented, please update the page. If you can only manage one language, do that one and say so in the pull request — someone will handle the other.

Code samples in the documentation should compile. Several that did not were found and fixed in 1.0.0, and each one cost somebody an afternoon.

## Pull requests

* Branch from `master`.
* Keep one concern per pull request. A rename and a bug fix in the same diff are hard to review and harder to revert.
* Describe what changed and why. If it fixes an issue, link it.
* `master` requires review, so the pull request is the only way in.

## Contributor License Agreement

There is no CLA today. Should the project join the .NET Foundation, contributors will be asked to sign theirs; you will be told before that happens rather than after.

## Questions

Ask in the [Telegram chat](https://t.me/prethinkdev). Questions about how to use the framework are better there than in issues, and you will usually get an answer faster.
