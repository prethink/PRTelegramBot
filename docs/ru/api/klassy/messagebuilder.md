---
description: Сборка текста сообщения по шаблону с позиционными аргументами и именованными токенами.
---

# MessageBuilder

`MessageBuilder` собирает текст сообщения из шаблона. В шаблоне два вида подстановок, и обе записываются в фигурных скобках:

* **позиционные аргументы** — `{0}`, `{1}`, `{2}`, подставляются по порядку добавления;
* **именованные токены** — `{QA}`, `{Имя}`, `{total}`, подставляются по ключу.

Именованный токен можно задать как готовым значением, так и функцией. Функция вычисляется в момент вызова `Build`, поэтому в токен удобно класть то, что должно быть получено как можно позже — текущее время, счётчик, значение из базы.

Если для токена не нашлось ни аргумента с таким индексом, ни резолвера с таким ключом, он остаётся в тексте как есть. Это сделано намеренно: незаполненный `{Итого}` заметен в сообщении, а не превращается в пустоту.

Класс находится в `PRTelegramBot.Builders`.

## Методы

```csharp
// Шаблон задаётся при создании.
public MessageBuilder(string template)

// Именованный токен с готовым значением.
public MessageBuilder AddResolver(string key, string value)

// Именованный токен со значением, которое вычисляется при Build.
public MessageBuilder AddResolver(string key, Func<string> resolver)

// Один позиционный аргумент.
public MessageBuilder AddArgument(object arg)

// Несколько позиционных аргументов сразу.
public MessageBuilder AddArguments(params object[] arguments)

// Собирает итоговый текст.
public string Build()
```

Все методы, кроме `Build`, возвращают сам билдер, поэтому их можно объединять в цепочку.

## Пример

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

public static class ExampleMessageBuilder
{
    [ReplyMenuHandler("Профиль")]
    public static async Task Profile(IBotContext context)
    {
        var text = new MessageBuilder("Привет, {0}! Ваш баланс: {Баланс}. Сейчас {Время}.")
            .AddArgument("Илья")
            .AddResolver("Баланс", "1500 ₽")
            .AddResolver("Время", () => DateTime.Now.ToString("HH:mm"))
            .Build();

        await MessageSender.Send(context, text);
    }
}
```

Результат:

```
Привет, Илья! Ваш баланс: 1500 ₽. Сейчас 14:32.
```

## Когда это удобнее интерполяции строк

Обычная интерполяция `$"Привет, {name}"` короче и в простых случаях лучше. `MessageBuilder` начинает выигрывать, когда шаблон живёт отдельно от кода — например, лежит в конфигурационном файле или приходит из базы. Тогда набор токенов известен, а сам текст можно менять без пересборки проекта.
