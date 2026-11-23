# Настройки бота и callback-запросы

В PRTelegramBot 1.1.4 используется Telegram.Bot 22.10.5 (Bot API 10.5). Через клиент бота можно получить его возможности и изменить настройки.

```csharp
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

static async Task ConfigureAsync(IBotContext context, CancellationToken token)
{
    BotFullInfo bot = await context.BotClient.GetMe(token);
    Console.WriteLine(bot.Username);

    await context.BotClient.SetMySettings(
        callbackQueryMode: SetMySettingsCallbackQueryMode.Auto,
        cancellationToken: token);
}
```

Метод `SetMySettings` также настраивает inline-запросы и частоту обратной связи, приватность в группах, бизнес-подключения, управление ботами, гостевые запросы и заявки на вступление, темы в личных чатах и общение между ботами. [Справка Telegram](https://core.telegram.org/bots/api#setmysettings).

## Как определить сообщение, на котором нажали кнопку?

Для кнопок на сообщениях, отправленных ботом, формат callback-запроса зависит от режима.

| Режим | Что передаёт Telegram |
| --- | --- |
| `Default` | Сообщение чата. Оно может быть помечено как недоступное. |
| `Auto` | Сообщение, если оно доступно, иначе — inline-идентификатор. Недавно удалённое сообщение ещё может прийти как недоступное. |
| `Inline` | Inline-идентификатор, в том числе для сообщения, отправленного ботом в чат. |

Кнопки на сообщениях, отправленных через inline-режим, уже используют inline-идентификатор. В обработчике подтвердите нажатие через `AnswerCallbackQuery`. Для изменения текста вызовите `MessageEditor.Edit(context, text)`. Метод вернёт изменённое сообщение чата или null после успешного редактирования по inline-идентификатору. Чтобы заменить клавиатуру, вызовите `MessageEditor.EditInline(context, keyboard)`. Передайте null для удаления клавиатуры. Для отправки нового сообщения и удаления исходного нужен идентификатор чата. Если действует белый список чатов, запрос без идентификатора чата будет отклонён.

## Что изменить при переходе

- `GetMe` возвращает `BotFullInfo`. Используйте этот тип или `var` для чтения флагов вроде `CanJoinGroups`, активных имён бота и настроек inline-запросов. В типизированных моках используйте `IRequest<BotFullInfo>`.
- У входящих и исходящих раскрывающихся цитат содержимое теперь находится в коллекции `Blocks`. Чтобы сохранить прежний текст, поместите его в один абзац. Для примера нужен `using Telegram.Bot.Types;`.

```csharp
var quote = new InputRichBlockExpandableBlockQuotation
{
    Blocks = new InputRichBlock[]
    {
        new InputRichBlockParagraph { Text = "Quoted text" }
    }
};
```

Событие `OnWriteAccessAllowedHandle` также передаёт необязательное поле `Message.WriteAccessAllowed.Inviter`, когда владелец бота приглашает пользователя в личный чат с ботом.
