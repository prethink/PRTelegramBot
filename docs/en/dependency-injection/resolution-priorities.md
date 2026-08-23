---
description: Which implementation wins when a component is available from more than one place.
---

# Component resolution priorities

**PRTelegramBot can run several bot instances at once.** Because of that the library uses a **component priority system**, which lets infrastructure dependencies — loggers, serializers, managers and so on — be configured per bot instead of globally.

It means:

* each bot can use its own implementations;
* shared components can be registered once in the DI container;
* a single bot can override any component through its builder without affecting the others.

So you can combine application-wide configuration, shared services from DI, and one-off tuning for one particular bot.

## Which components this applies to

* `ILoggerFactory`
* `ILogger` — follows whatever `ILoggerFactory` resolved to
* `IWhiteListManager`
* `IAdminManager`
* `IInlineMenuConverter`
* `IPRSerializer`

## The order

1. **Set on the builder.** If you passed the component explicitly, that is what is used.
2. **The DI container.** If the builder has nothing, the framework looks for a registration.
3. **The default.** If neither has it, the built-in implementation is used.

In short: builder → DI → default. The first one that has an answer wins, so a value set on the builder always beats one registered in DI.
