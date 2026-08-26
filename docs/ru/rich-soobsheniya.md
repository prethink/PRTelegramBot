---
description: Отправка сообщений, собранных из блоков — заголовков, списков, таблиц, цитат и медиа.
---

# Rich-сообщения

Rich-сообщение — это не то же самое, что форматированное. Форматированное — это один сплошной текст, поверх которого разложены сущности: тут жирный, там ссылка. Rich-сообщение собрано из **блоков**: заголовки, абзацы, списки, таблицы, цитаты, разделители, встроенные фото и видео, раскрывающиеся секции. Вёрсткой занимается Telegram.

Появились в Bot API 10.1; в 10.3 к ним добавили кнопки внутри, вложенные документы, раскрывающиеся цитаты и компактные таблицы.

## Отправка

Фреймворк отправляет rich-сообщение так же, как любое другое, поэтому все настройки [`OptionMessage`](api/klassy/optionmessage.md) продолжают работать:

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

[ReplyMenuHandler("Отчёт")]
public static async Task Report(IBotContext context)
{
    const string html = """
        <h1>Недельный отчёт</h1>
        <p>Выручка выросла на <b>12%</b> к прошлой неделе.</p>
        <ul>
            <li>Новых пользователей: 1 204</li>
            <li>Отток: 0.8%</li>
        </ul>
        <blockquote>Рост удержался и в выходные.</blockquote>
        """;

    await MessageSender.SendRichMessage(context, html);
}
```

{% hint style="warning" %}
Здесь **диалект HTML для rich-сообщений**, а не тот, который понимает `ParseMode.Html`. `<h1>`, `<ul>` и `<table>` что-то значат в rich-сообщении и ничего не значат в форматированном. Список тегов — на странице rich message formatting options в документации Bot API.

Фреймворк передаёт HTML как HTML — разбирает его Telegram. На нашей стороне ничего не валидируется.
{% endhint %}

## Настройки

Всё, что rich-сообщение может нести, мапится ровно так же, как для обычного: меню, параметры ответа, идентификатор темы, защита контента, тихая доставка, бизнес-подключение, эффект сообщения, платная рассылка, топик личных сообщений, параметры предлагаемого поста и [эфемерные параметры](efemernye-soobsheniya.md).

{% hint style="warning" %}
На rich-сообщении эфемерные параметры действуют по тем же правилам, что и везде: если отвечать не на callback query, Telegram потребует, чтобы бот был администратором чата. См. [Эфемерные сообщения](efemernye-soobsheniya.md).
{% endhint %}

```csharp
var option = new OptionMessage
{
    MenuInlineKeyboardMarkup = keyboard,
    ProtectedContent = true,
    EphemeralMessageParameters = context.Update.GetUserId()
};

await MessageSender.SendRichMessage(context, html, option);
```

Соответствия здесь нет только у `ParseMode`, `Entities` и `DisableWebPagePreview` — блоки несут собственную структуру, и применять эти настройки не к чему.

## Сборка вручную

Когда содержимое не пишется руками, а собирается кодом, есть перегрузка, принимающая `InputRichMessage` напрямую:

```csharp
var rich = new InputRichMessage
{
    Blocks = new InputRichBlock[]
    {
        new InputRichBlockSectionHeading { Text = "Недельный отчёт" },
        new InputRichBlockParagraph { Text = "Выручка выросла на 12% к прошлой неделе." },
        new InputRichBlockDivider(),
    }
};

await MessageSender.SendRichMessage(context, rich);
```

В `Telegram.Bot.Types` 26 типов блоков и около 30 типов текста — фреймворк их не оборачивает, они используются напрямую, как и любые другие типы Telegram.Bot.

## Приём

Входящее rich-сообщение поднимает `OnRichMessageHandle`, а опознаётся оно по `MessageType.RichMessage`. См. [События для типа update message](rabota-s-sobytiyami/sobytiya-dlya-tipa-update-message.md).

Круговой сценарий работает: `msg.RichMessage.ToHtml()` отдаёт HTML со ссылками на медиа, а переданный обратно в `SendRichMessage` тот же HTML их снова разрешает — сообщение можно прочитать, отредактировать и отправить дальше, не потеряв картинки.
