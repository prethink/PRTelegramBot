# HTML-виджеты

Telegram Bot API 10.4 отображает HTML-виджеты прямо внутри сообщения. В рабочем окружении Telegram отправка доступна в личных чатах, а отправленные виджеты автоматически защищены от пересылки. Группы, inline-режим и пересылку можно проверять на тестовом сервере Telegram. [Доступность виджетов](https://core.telegram.org/bots/widgets#availability).

## Отправка и обработка запроса

Добавьте регистрацию событий перед `bot.StartAsync()`, а класс команды поместите после кода запуска. Команда `/widget` отправляет виджет, кнопка которого передаёт боту `ping`.

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using Telegram.Bot;

bot.Events.UpdateEvents.OnWidgetQueryHandle += e =>
{
    var query = e.Context.Update.WidgetQuery!;
    return e.Context.BotClient.AnswerWidgetQuery(query.Id,
        data: query.Data == "ping" ? "pong" : "Unknown request",
        cancellationToken: e.Context.CancellationToken);
};

bot.Events.UpdateEvents.OnSharedWidgetQueryHandle += e =>
{
    var query = e.Context.Update.SharedWidgetQuery!;
    return e.Context.BotClient.AnswerWidgetQuery(query.Id,
        data: query.Data == "ping" ? "pong" : "Unknown request",
        cancellationToken: e.Context.CancellationToken);
};
```

```csharp
public static class WidgetCommands
{
    [SlashHandler("/widget")]
    public static Task ShowWidget(IBotContext context)
    {
        const string html = @"
            <button id='ping'>Ping</button>
            <p id='status'></p>
            <script>
              const status = document.getElementById('status');
              document.getElementById('ping').addEventListener('click', async () => {
                try {
                  const answer = await TelegramWidget.request('ping');
                  status.textContent = answer.result ?? '';
                } catch (error) {
                  status.textContent = error.code ?? 'Request failed';
                }
              });
            </script>
            ";

        return context.BotClient.SendWidget(context.GetChatId(), html,
            allowQueries: true, cancellationToken: context.CancellationToken);
    }
}
```

## События и данные

| Событие | Данные |
| --- | --- |
| `bot.Events.MessageEvents.OnWidgetHandle` | Входящее сообщение с виджетом. Объект доступен в `e.Context.Update.Message?.Widget`. |
| `bot.Events.UpdateEvents.OnWidgetQueryHandle` | Запрос от виджета в сообщении, отправленном ботом или через него. У inline-сообщения будет `InlineMessageId`. |
| `bot.Events.UpdateEvents.OnSharedWidgetQueryHandle` | Запрос от виджета в пересланном сообщении. В update есть `WidgetId` и отправитель. |

Если вы явно задаёте список `AllowedUpdates` для polling или webhook, добавьте `UpdateType.WidgetQuery` и `UpdateType.SharedWidgetQuery`. Оба запроса содержат отправителя, доступного через `e.Context.GetUserId()`. Идентификатор чата проверяйте через `TryGetChatId`. Если бот использует белый список чатов в режиме `OnPreUpdate`, для прохождения проверки нужен известный идентификатор чата.

Ответьте на запрос в течение 15 секунд. `AnswerWidgetQuery` возвращает результат тому экземпляру виджета, который его запросил. Чтобы изменить сохранённые данные, используйте `BotClient.EditMessageWidgetData` для сообщения или `BotClient.EditSharedWidgetData` для общего состояния. Чтобы создать общий виджет для повторной отправки, передайте `isShared: true` в `SendWidget` и используйте полученный `Message.Widget.Id` в `SendSharedWidget`. [Руководство Telegram по виджетам](https://core.telegram.org/bots/widgets).
