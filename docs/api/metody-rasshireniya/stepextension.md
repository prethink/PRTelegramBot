# StepExtension

```csharp
/// <summary>
/// Регистрация следующего шага.
/// </summary>
/// <param name="update">Обновление telegram.</param>
/// <param name="command">Следующая команда которая должна быть выполнена.</param>
public static void RegisterStepHandler(this Update update, IExecuteStep command)
public static void RegisterStepHandler(this IBotContext context, IExecuteStep command)

/// <summary>
/// Получает обработчик или null пользователя.
/// </summary>
/// <param name="update">Обновление telegram.</param>
/// <returns>обработчик или null.</returns>
public static TExecuteStep? GetStepHandler<TExecuteStep>(this Update update) where TExecuteStep : IExecuteStep
public static TExecuteStep? GetStepHandler<TExecuteStep>(this IBotContext context) where TExecuteStep : IExecuteStep

/// <summary>
/// Получить текущий обработчик шага.
/// </summary>
/// <param name="update">Обновление telegram.</param>
/// <returns>Обработчик или null.</returns>
public static IExecuteStep? GetStepHandler(this Update update)
public static IExecuteStep? GetStepHandler(this IBotContext context)

/// <summary>
/// Очищает шаги пользователя.
/// </summary>
/// <param name="update">Обновление telegram.</param>
public static void ClearStepUserHandler(this Update update)
public static void ClearStepUserHandler(this IBotContext context)

/// <summary>
/// Проверяет есть ли шаг у пользователя.
/// </summary>
/// <param name="update">Обновление полученное с telegram</param>
/// <returns>True - есть обработчик, False - нет обработчика.</returns>
public static bool HasStepHandler(this Update update)
public static bool HasStepHandler(this IBotContext context)
```
