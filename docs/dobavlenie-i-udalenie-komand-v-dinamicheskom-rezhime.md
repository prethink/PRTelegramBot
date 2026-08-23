---
description: >-
  Начиная с версии 0.5.0 появилась возможность добавлять динамически команды
  обработчики для типа reply, slash и inline
---

# Добавление и удаление команд в динамическом режиме

## Создание новых команд

Пример создания нового метода обработки в коде&#x20;

```csharp
var method = async (IBotContext context) =>
{
        string message = "Сообщение";
        MessageSender.Send(context, message);
};
```

После создания метода нужно обратиться к экземпляру класса бота и добавить только что созданный метод с названием команды которая будет задействована.

_**Внимание: название команд должно быть уникальным!**_

Пример для Reply обработчика

```csharp
bot.Register.AddReplyCommand("Название команды", method);
```

Пример для Slash обработчика

```csharp
bot.Register.AddSlashCommand("/Название команды", method);
```

Пример для Inline обработчика

```csharp
bot.Register.AddInlineCommand(Enum.value, method);
```

Важно чтобы Enum был типа int, и чтобы значение были уникальны. Рекомендуется использовать значение команд не ниже 100, потому что начиная с 0 уже есть зарегистрированные команды.

```csharp
/// <summary>
/// Идентификаторы для callback команд
/// </summary>
[InlineCommand]
public enum THeader
{
    [Description(nameof(None))]
    None = 0,
    [Description(nameof(PickMonth))]
    PickMonth = 1,
    [Description(nameof(PickYear))]
    PickYear = 2,
    [Description(nameof(ChangeTo))]
    ChangeTo = 3,
    [Description(nameof(YearMonthPicker))]
    YearMonthPicker = 4,
    [Description(nameof(PickDate))]
    PickDate = 5,
    [Description(nameof(NextPage))]
    NextPage = 6,
    [Description(nameof(CurrentPage))]
    CurrentPage = 7 ,
    [Description(nameof(PreviousPage))]
    PreviousPage = 8,
}
```

Пример с уникальными значениями. Здесь видно что используются 2 разных перечисления, но int значения не пересекаются. &#x20;

В первом enum значение

* 500
* 501
* 502
* 503
* 504
* и т.д.

Во втором enum&#x20;

* 600
* 601
* 602
* 603
* и т.д.

```csharp
    public enum CustomTHeader
    {
        [Description("Бесплатный ВИП")]
        GetFreeVIP = 500,
        [Description("Вип на 1 день")]
        GetVipOneDay,
        [Description("Вип на 1 неделю")]
        GetVipOneWeek,
        [Description("Вип на 1 месяц")]
        GetVipOneMonth,
        [Description("Вип навсегда")]
        GetVipOneForever
    }

    public enum CustomTHeaderTwo
    {
        [Description("Пример 1")]
        ExampleOne = 600,
        [Description("Пример 2")]
        ExampleTwo,
        [Description("Пример 3")]
        ExampleThree,
        [Description("Пример страниц")]
        CustomPageHeader,
        [Description("Пример страниц2")]
        CustomPageHeader2,
    }
```

Плохой пример

В данном примере видно, что enum значения будут пересекаться, где-то несколько раз 500, где-то 501. Чтобы система команд работала стабильно и правильно все значения должны быть уникальны!



```csharp
    public enum CustomTHeader
    {
        [Description("Бесплатный ВИП")]
        GetFreeVIP = 500,
        [Description("Вип на 1 день")]
        GetVipOneDay,
        [Description("Вип на 1 неделю")]
        GetVipOneWeek,
        [Description("Вип на 1 месяц")]
        GetVipOneMonth,
        [Description("Вип навсегда")]
        GetVipOneForever
    }

    public enum CustomTHeaderTwo
    {
        [Description("Пример 1")]
        ExampleOne = 500,
        [Description("Пример 2")]
        ExampleTwo,
        [Description("Пример 3")]
        ExampleThree,
        [Description("Пример страниц")]
        CustomPageHeader,
        [Description("Пример страниц2")]
    }

    public enum CustomTHeaderThree
    {
        [Description("Пример страниц")]
        CustomPageHeader = 501,
        [Description("Пример страниц2")]
        CustomPageHeader2,
    }
```

Все методы возвращают true или false\
True - команда добавлена\
False - ошибка при добавление команды

## Удаление команд

Пример для Reply&#x20;

```csharp
bot.Register.RemoveReplyCommand("Название команды");
```

Пример для Slash&#x20;

```csharp
bot.Register.RemoveSlashCommand("Название команды");
```

Пример для Inline&#x20;

```csharp
bot.Register.RemoveInlineCommand(Enum.value);
```

Данные методы возвращают true или false\
True - команда удалена\
False - ошибка при удаление команды
