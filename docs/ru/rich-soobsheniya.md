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

Фреймворк передаёт HTML через конвертер rich-сообщений Telegram.Bot, после чего разметку разбирает Telegram. PRTelegramBot не валидирует её перед отправкой.
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

Начиная с 1.1.2 Telegram.Bot также принимает HTML и массивы блоков через неявные преобразования. `RichText` превращается в блок-абзац:

```csharp
using Telegram.Bot.Types;

InputRichMessage htmlMessage = "<h1>Weekly report</h1>";
RichText paragraph = "Revenue is up 12% on last week.";
InputRichMessage blockMessage = new InputRichBlock[]
{
    new InputRichBlockSectionHeading { Text = "Weekly report" },
    paragraph,
    new InputRichBlockDivider()
};

await MessageSender.SendRichMessage(context, htmlMessage);
await MessageSender.SendRichMessage(context, blockMessage);
```

Преобразование HTML разрешает те же ссылки на медиа `tg://...?file_id=...`, что и строковая перегрузка фреймворка.

## Повторное использование медиа Telegram

В HTML можно сослаться на уже сохранённый Telegram файл и не загружать его повторно. В источнике укажите вид медиа и его `file_id`:

```html
<img src="tg://photo?file_id=<file-id>"/>
```

Поддерживаются виды `photo`, `video`, `document` и `audio`. `MessageSender.SendRichMessage` преобразует такую ссылку в медиа-вложение, ожидаемое Telegram.Bot.

## Приём

Входящее rich-сообщение поднимает `OnRichMessageHandle`, а опознаётся оно по `MessageType.RichMessage`. См. [События для типа update message](rabota-s-sobytiyami/sobytiya-dlya-tipa-update-message.md).

Круговой сценарий работает: `msg.RichMessage.ToHtml()` создаёт такие ссылки с `file_id`, а переданный обратно в `SendRichMessage` HTML снова их разрешает — сообщение можно прочитать, отредактировать и отправить дальше, не загружая медиа повторно.
