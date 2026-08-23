---
description: Inline-кнопка, которая копирует заданный текст в буфер обмена пользователя.
---

# InlineCopyText

Добавлена в версии 1.0.0.

`InlineCopyText` создаёт inline-кнопку, при нажатии на которую Telegram копирует заданный текст в буфер обмена пользователя. Обновление боту при этом не отправляется — всё происходит на стороне клиента, поэтому обработчик такой кнопке не нужен.

Удобно для промокодов, адресов кошельков, реферальных ссылок и всего остального, что пользователь должен куда-то вставить.

Класс находится в `PRTelegramBot.Models.InlineButtons` и является обёрткой над `InlineKeyboardButton.WithCopyText`.

## Конструктор

```csharp
public InlineCopyText(string buttonName, string copyText)
```

| Параметр | Назначение |
| --- | --- |
| `buttonName` | Надпись на кнопке. |
| `copyText` | Текст, который попадёт в буфер обмена. |

Текст, который копируется, доступен и меняется через свойство `CopyText`.

## Пример

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;

public static class ExampleCopyText
{
    [ReplyMenuHandler("Промокод")]
    public static async Task Promo(IBotContext context)
    {
        var keyboard = new InlineKeyboardBuilder()
            .AddButton(new InlineCopyText("Скопировать промокод", "WELCOME2026"))
            .Build();

        var option = new OptionMessage { MenuInlineKeyboardMarkup = keyboard };

        await MessageSender.Send(context, "Ваш промокод:", option);
    }
}
```

## Ограничения Telegram

Копируемый текст не должен превышать 256 символов. Кнопку нельзя использовать в inline-режиме бота — только в обычных сообщениях.
