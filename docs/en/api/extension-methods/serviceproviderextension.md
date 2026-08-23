---
description: Extension methods for ServiceProviderExtension.
---

# ServiceProviderExtension

Extension methods for ServiceProviderExtension.

## Methods

| Method | Description |
| --- | --- |
| `static IServiceCollection AddBotHandlers(this IServiceCollection services)` | Adds the bot handlers to DI with a Transient lifetime. |
| `static IServiceCollection AddScopedBotHandlers(this IServiceCollection services)` | Adds the bot handlers to DI with a Scoped lifetime. |
| `static IServiceCollection AddTransientBotHandlers(this IServiceCollection services)` | Adds the bot handlers to DI with a Transient lifetime. |
| `static IServiceCollection AddSingletonBotHandlers(this IServiceCollection services)` | Adds the bot handlers to DI with a Singleton lifetime. |

