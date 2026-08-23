# Работа с командами

## Один метод для всех ботов

Если в одном проекте используется несколько ботов и при этом хотелось бы, чтобы команда была доступна сразу всем - можно использовать значение "-1" для BotId.&#x20;

Пример:

```csharp
/// <summary>
/// Команда отработает для любого бота с любым botid.
/// Команда отработает при написание в чат "Команда для всех ботов".
/// </summary>
[ReplyMenuHandler(-1, "Команда для всех ботов")]
public static async Task ReplyExampleAllBots(IBotContext context)
{
   string msg = nameof(ReplyExampleAllBots);
   await MessageSender.Send(context, msg);
}
```

Работает для следующих атрибутов:

* ReplyMenuHandlerAttribute;
* ReplyMenuDynamicHandlerAttribute;
* SlashHandlerAttribute;
* InlineCallbackHandlerAttribute.

## Завершение пошагового выполнение команд на последнем шаге

Начиная с версии 0.6 есть возможность взвести флаг, который оповестит, что это последний шаг в системе и пошаговое выполнение нужно завершить.

Пример:

```csharp
var handler = context.GetStepHandler<StepTelegram>();
handler.LastStepExecuted = true;
```

## Игнорирование базовых команд при выполнение пошаговых команд

Начиная с версии 0.6 есть возможность взвести флаг, который оповестит систему, что в данный момент нужно игнорировать все команды, кроме пошаговых.

Пример:

```csharp
var handler = context.GetStepHandler<StepTelegram>();
handler.IgnoreBasicCommands = true;
```
