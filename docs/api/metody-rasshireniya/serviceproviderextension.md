# ServiceProviderExtension

```csharp
/// <summary>
/// Добавить обработчики ботов с временным временем жизни (Transient) в DI.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
/// <returns>Обновленная коллекция сервисов.</returns>
public static IServiceCollection AddBotHandlers(this IServiceCollection services)

/// <summary>
/// Добавить обработчики ботов с областью видимости (Scoped) в DI.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
/// <returns>Обновленная коллекция сервисов.</returns>
public static IServiceCollection AddScopedBotHandlers(this IServiceCollection services)
/// <summary>
/// Добавить обработчики ботов с временным временем жизни (Transient) в DI.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
/// <returns>Обновленная коллекция сервисов.</returns>
public static IServiceCollection AddTransientBotHandlers(this IServiceCollection services)

/// <summary>
/// Добавить обработчики ботов с одиночным временем жизни (Singleton) в DI.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
/// <returns>Обновленная коллекция сервисов.</returns>
public static IServiceCollection AddSingletonBotHandlers(this IServiceCollection services)
```
