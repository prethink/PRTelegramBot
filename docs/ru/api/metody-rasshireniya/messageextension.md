---
description: Методы расширения для работы с сообщением
---

# MessageExtension

```csharp
/// <summary>
/// Автоматическое удаление сообщение через определенное время.
/// </summary>
/// <param name="message">Сообщение которое нужно удалить.</param>
/// <param name="seconds">Через сколько секунд будет удалено сообщение.</param>
/// <param name="botClient">Бот клиент.</param>
/// <param name="update">Update.</param>
public static async Task AutoDeleteMessage(this Message message, int seconds, ITelegramBotClient botClient, Update update)

/// <summary>
/// Автоматическое редактирования сообщения через определенное время.
/// </summary>
/// <param name="message">Сообщение которое нужно удалить.</param>
/// <param name="seconds">Через сколько секунд будет удалено сообщение.</param>
/// <param name="botClient">Бот клиент.</param>
/// <param name="update">Update.</param>
public static async Task AutoEditMessage(this Message message, string messageText, int seconds, ITelegramBotClient botClient, Update update)

/// <summary>
/// Автоматическое редактирования сообщения через определенное время в цикле.
/// </summary>
/// <param name="message">Сообщение которое нужно удалить.</param>
/// <param name="seconds">Через сколько секунд будет удалено сообщение.</param>
/// <param name="botClient">Бот клиент.</param>
/// <param name="update">Update.</param>
public static async Task AutoEditMessageCycle(this Message message, List<string> messageTexts, int seconds, ITelegramBotClient botClient, Update update)
```
