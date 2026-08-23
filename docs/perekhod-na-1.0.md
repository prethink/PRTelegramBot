---
description: Что нужно поправить в своём коде при переходе с 0.9.x на 1.0.0.
---

# Переход на 1.0

Версия 1.0.0 — первый стабильный релиз, и в нём собраны все ломающие изменения, которые копились в 0.9.x. Дальше публичный API следует [семантическому версионированию](https://semver.org/): ломать что-то можно будет только в 2.0.

Ниже — только то, что требует правок в вашем коде. Полный список изменений, включая исправления багов, лежит в [changelog](https://github.com/prethink/PRTelegramBot/blob/master/CHANGELOG.ru.md).

## Удалён фасад `Helpers.Message`

Это изменение затрагивает почти всех: `PRTelegramBot.Helpers.Message` был помечен устаревшим ещё в 0.9.0 и только перенаправлял вызовы. Теперь он удалён.

Сигнатуры замен совпадают один в один, меняется только имя типа:

| Было | Стало |
| --- | --- |
| `Helpers.Message.Send(...)` | `MessageSender.Send(...)` |
| `Helpers.Message.Edit(...)` | `MessageEditor.Edit(...)` |

```csharp
// было
using PRTelegramBot.Helpers;
await Message.Send(context, "Привет");

// стало
using PRTelegramBot.Services.Messages;
await MessageSender.Send(context, "Привет");
```

## Объединены пространства имён

Каждое из этих пространств имён было расщеплено надвое: файлы лежали в одной папке, но объявляли разные namespace. Теперь по одному на папку.

| Было | Стало |
| --- | --- |
| `PRTelegramBot.InlineButtons` | `PRTelegramBot.Models.InlineButtons` |
| `PRTelegramBot.Core.Factory` | `PRTelegramBot.Core.Factories` |
| `PRTelegramBot.Models.TCommands` | `PRTelegramBot.Models.CallbackCommands` |
| `PRTelegramBot.Core.UpdateHandlers` | `PRTelegramBot.Core.UpdateDispatchers` |

Правится заменой `using`-директив, сами типы не менялись.

## Переименованы атрибуты

Два атрибута выражали одну идею, но назывались по-разному и ставили слова в порядке, обратном типам Telegram.Bot, которые они фильтруют.

| Было | Стало |
| --- | --- |
| `[RequiredTypeChat(...)]` | `[RequireChatType(...)]` |
| свойство `TypesChat` | свойство `ChatTypes` |
| `[RequireTypeMessage(...)]` | `[RequireMessageType(...)]` |
| свойство `TypeMessages` | свойство `MessageTypes` |

## Исправлены имена с опечатками

| Было | Стало |
| --- | --- |
| `AutoEditMessageСycle` | `AutoEditMessageCycle` |
| `OptionMessage.thumbnail` | `OptionMessage.Thumbnail` |

В старом `AutoEditMessageСycle` буква «С» была кириллической, поэтому имя выглядело правильным, но не совпадало при поиске по коду.

## Удалено то, что не работало

* `PRTelegramBot.Models.InlineButton` — нигде не использовался, а его `GetContent` всегда бросал `NotImplementedException`. Положить такую кнопку в меню было невозможно.
* `IInlineStorage` — интерфейс, который никогда никем не реализовывался.
* Пространство имён `PRTelegramBot.Workflow` — незавершённые пустые заготовки.

Если что-то из этого встречалось в вашем коде, оно и раньше не работало.

## Скрыто из публичного API

* `PRLoggerEvents<T>` и `PRLoggerEventsFactory` стали `internal`. Это внутренний fallback, который обеспечивает логирование через события, когда не задан `ILoggerFactory`. Пользоваться нужно `ILogger` — см. [Логирование](logirovaniya.md).
* `InlineCallbackWithConfirmation.DataCollection` больше не публичное. Ожидающие подтверждения ищет сам фреймворк; заодно они теперь не копятся бесконечно, а отбрасываются через час.

## Изменения поведения

Эти правки не ломают компиляцию, но меняют то, что происходит во время работы.

**`GetChatId`, `GetMessageId` и `GetUserId`** теперь бросают `InvalidOperationException` с внятным сообщением вместо `NullReferenceException`, когда в обновлении нужных данных нет. Если вы ловили `NullReferenceException` — поправьте тип.

**`UpdateExtension.TryGetBot`** объявляет `out`-параметр как `PRBotBase?`, потому что при неудаче он равен `null`. Компилятор теперь об этом предупредит.

**`FileInlineConverter(string path)`** раньше игнорировал переданное имя папки и всегда использовал папку с буквальным именем `path`. Теперь имя учитывается. Если вы пользовались этим конструктором, данные inline-кнопок переедут в ту папку, которую вы просили, и подтверждения, ожидавшие ответа на момент обновления, найдены не будут.

**`InlineUtils.GetInlineButton`** больше не разбирает конкретные типы кнопок через `switch`, а вызывает `GetInlineButton()` у самой кнопки. Встроенные кнопки работают как раньше, но теперь учитывается конвертация, переопределённая в наследнике, и работают типы, которых в `switch` не было.

## Что появилось нового

Заодно стоит посмотреть, что добавилось в 1.0:

* [`InlineCopyText`](api/klassy/inlinecopytext.md) — кнопка, копирующая текст в буфер обмена.
* [`MessageBuilder`](api/klassy/messagebuilder.md) — сборка текста по шаблону.
* `ReplyKeyboardBuilder.AddRequestManagedBot` — кнопка, предлагающая выбрать бота.
* `OptionMessage.ShowCaptionAboveMedia` и другие параметры отправки, которые Telegram поддерживает, а библиотека раньше не пробрасывала.
* События сообщений и update, пропущенные при обновлениях Telegram.Bot.
