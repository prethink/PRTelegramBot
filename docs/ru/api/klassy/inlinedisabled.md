---
description: Inline-кнопка, которая видна, но ничего не делает при нажатии.
---

# InlineDisabled

Добавлена в версии 1.1.0. Требует Bot API 10.3.

`InlineDisabled` создаёт inline-кнопку, которую Telegram рисует серой и на нажатия по которой не реагирует — обновление боту не отправляется, обработчик не вызывается.

Нужна там, где меню должно сохранять вёрстку, пока действие недоступно: шаг, до которого пользователь ещё не дошёл, пункт, не входящий в его тариф, или кнопка, занятая на время долгой операции.

Класс находится в `PRTelegramBot.Models.InlineButtons` и является обёрткой над `InlineKeyboardButton.WithDisabled`.

## Конструктор

```csharp
public InlineDisabled(string buttonName)
```

| Параметр | Назначение |
| --- | --- |
| `buttonName` | Надпись на кнопке. |

Полезной нагрузки у кнопки нет — подпись и есть всё её содержимое, поэтому `GetContent()` возвращает именно её. Сменить надпись можно через `SetButtonName`.

## Пример

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;

public static class ExampleDisabled
{
    [ReplyMenuHandler("Шаги")]
    public static async Task Steps(IBotContext context)
    {
        var keyboard = new InlineKeyboardBuilder()
            .AddButton(new InlineCallback("Шаг 1 — пройден", MyHeader.StepOne))
            .AddRowWithButton(new InlineDisabled("Шаг 2 — сначала закончите первый"))
            .AddRowWithButton(new InlineDisabled("Шаг 3 — закрыт"))
            .Build();

        var option = new OptionMessage { MenuInlineKeyboardMarkup = keyboard };

        await MessageSender.Send(context, "Открыт только первый шаг:", option);
    }
}
```

## Зачем это лучше, чем убрать кнопку

Убранная кнопка сдвигает всё меню — пользователь тянется к одному пункту, а попадает в другой. Живая кнопка, которая отказывает после нажатия, тратит его время впустую. Отключённая остаётся на месте и объясняет причину собственной подписью.

См. также [Создание Inline меню](../../obrabotka-komand/obrabotka-inline-komand/sozdanie-inline-menyu.md).
