---
description: >-
  Набор функций для проверки пользователя внутри группы. Возможно понадобиться
  добавить бота в группу и выдать права администратора
---

# GroupUtils

Будет доступно начиная с версии 0.5.5

**Проверка участника в группе**

```csharp
/// <summary>
/// Проверяет находится ли пользователь в группе.
/// </summary>
/// <param name="botClient">Телеграм бот клиент</param>
/// <param name="groupId">Идентификатор группы</param>
/// <param name="userId">Идентификатор пользователя</param>
/// <returns>True - есть иначе false</returns>
public static async Task<bool> IsGroupMember(ITelegramBotClient botClient, long groupId, long userId)
```

**Проверка является ли пользователь администратором в группе**

```csharp
/// <summary>
/// Проверяет является ли администратором группы.
/// </summary>
/// <param name="botClient">Телеграм бот клиент</param>
/// <param name="groupId">Идентификатор группы</param>
/// <param name="userId">Идентификатор пользователя</param>
/// <returns>True - администратор иначе false</returns>
public static async Task<bool> IsGroupAdmin(ITelegramBotClient botClient, long groupId, long userId)
```

**Проверка является ли пользователь создателем группы**

```csharp
/// <summary>
/// Проверяет является ли создателем группы.
/// </summary>
/// <param name="botClient">Телеграм бот клиент</param>
/// <param name="groupId">Идентификатор группы</param>
/// <param name="userId">Идентификатор пользователя</param>
/// <returns>True - создатель иначе false</returns>
public static async Task<bool> IsGroupCreator(ITelegramBotClient botClient, long groupId, long userId)
```
