# Обработка Inline команд

Telegram позволяет использовать максимум 64 байта для содержимого callback\_data. В PRTelegramBot для удобной работы с inline кнопками используется сериализатор данных, по умолчанию это json. Начиная с версии 0.8.4 был добавлен toon сериализатор, который позволяет экономить несколько байт. Так же можно реализовать интерфейс IPRSerializer и сделать собственный инициализатор для Inline кнопок.

```csharp
var telegram = new PRBotBuilder("token")
                    .SetInlineSerializer(new ToonSerializerWrapper())
                    .Build();

var telegram = new PRBotBuilder("token")
                    .SetInlineSerializer(new JsonSerializerWrapper())
                    .Build();
```

\
В статье "[Создание Inline меню](sozdanie-inline-menyu.md)" есть информация, как обойти ограничение в 64 байта.

<figure><img src="../../.gitbook/assets/изображение (6).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (37).png" alt=""><figcaption></figcaption></figure>

[Описание параметров](../parametry.md)

Под inline командами подразумеваю inline меню и обработку этих менюшек. В PRTelegram бот можно задействовать 3 основных пунктов inline меню:

* InlineCallback – обработчик команд.
* InlineURL – Работает со ссылками.
* InlineWebApp – Работает с WebApp. (Разбор будет в следующих главах)

```csharp
/// <summary>
/// Конструктор.
/// </summary>
/// <param name="botId">Идентификатор бота.</param>
/// <param name="botIds">Идентификаторы ботов.</param>
/// <param name="commands">Команды.</param>
public InlineCallbackHandlerAttribute(params T[] commands)
public InlineCallbackHandlerAttribute(long botId, params T[] commands) 
public InlineCallbackHandlerAttribute(long[] botIds, params T[] commands)
```



Перед созданием Inline меню требуется создать новое перечисление которое будет в себе содержать набор команд. Перечисление обязательно должен быть отмечено атрибутом **InlineCommand** так же для избежания ошибок присвойте номер больше 100 первому значению.

```csharp
[InlineCommand]
public enum CustomTHeader
{
    [Description("Пример 1")]
    ExampleOne = 500,
    [Description("Пример 2")]
    ExampleTwo,
    [Description("Пример 3")]
    ExampleThree,
}
```
