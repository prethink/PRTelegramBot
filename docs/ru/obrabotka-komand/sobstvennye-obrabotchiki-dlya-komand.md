# Собственные обработчики для команд

На текущий момент можно создать собственные обработчики для update типа message и callbackQuery.

Примеры уже реализованных для message:

* Reply - текстовые команды
* ReplyDynamic - динамические текстовые команды
* Slash - текстовые команды, которые используют перед текстом символ "/"

Пример для callbackQuery

* InlineCallback - обработка inline кнопок.

В прошлых статьях рассказывалась как работать с командами. Именно эти обработчики отвечают, как правильно это работает под капотом.

Начиная с версии 0.7 есть возможность создать собственные обработчики. Ваши обработчики будут приоритетными над уже существующими.

[UpdateResult](../api/perechisleniya-enum/updateresult.md)

## Message

Для создания своего обработчика, нужно создать новый класс, который реализовывает интерфейс [IMessageCommandHandler](../api/interfeisy/imessagecommandhandler.md). В реализованном методе Handle вы можете реализовываете логику обработки данных. Если данные обработали, возвращаете результат UpdateResult.Handled, это укажет системе, что не требуется вызывать следующие обработчики.

```csharp
public class MessageTestHandler : IMessageCommandHandler
{
    public async Task<UpdateResult> Handle(IBotContext context, Message updateType)
    {
        /* Если данные пришли которые вам нужны и вы их обработали возращаем результат Handled. 
         * Это будет означать, то что действие выполнено и остальные обработчики будут пропущены. */
        if (updateType.Text == "Нужные данные")
            return UpdateResult.Handled;

        // Команда не обработана, попытаемся обработать следующим обработчиком.
        return UpdateResult.Continue;
    }
}
```

При создание бота добавьте ваш новый обработчик:

```csharp
var bot = new PRBotBuilder("Token")
    .AddMessageCommandHandlers(new MessageTestHandler())
    .Build();
```

После этого, когда придет update формата message, сначала будет выполнен ваш обработчик. Если ваш обработчик не сможет обработать данные и вернет результат UpdateResult.Continue, update попытается выполнить дефолтные обработчики в следующем порядке slash, reply, replydynamic.

## CallbackQuery

Для создания своего обработчика, нужно создать новый класс, который реализовывает интерфейс[ICallbackQueryCommandHandler](../api/interfeisy/icallbackquerycommandhandler.md). В реализованном методе Handle вы можете реализовываете логику обработки данных. Если данные обработали, возвращаете результат UpdateResult.Handled, это укажет системе, что не требуется вызывать следующие обработчики.

```csharp
public class CallbackQueryTestHandler : ICallbackQueryCommandHandler
{
    public async Task<UpdateResult> Handle(IBotContext context, CallbackQuery updateType)
    {
        /* Если данные пришли которые вам нужны и вы их обработали возращаем результат Handled. 
         * Это будет означать, то что действие выполнено и остальные обработчики будут пропущены. */
        if (updateType.Data == "Нужные данные")
            return UpdateResult.Handled;

        // Команда не обработана, попытаемся обработать следующим обработчиком.
        return UpdateResult.Continue;
    }
}
```

При создание бота добавьте ваш новый обработчик:

```csharp
var bot = new PRBotBuilder("Token")
    .AddCallbackQueryCommandHandlers(new CallbackQueryTestHandler())
    .Build();
```

После этого, когда придет update формата callbackQuery, сначала будет выполнен ваш обработчик. Если ваш обработчик не сможет обработать данные и вернет результат UpdateResult.Continue, update попытается выполнить дефолтный обработчик inlineCallback.
