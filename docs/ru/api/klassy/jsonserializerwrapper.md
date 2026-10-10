# JsonSerializerWrapper

```c#
using PRTelegramBot.Interfaces;
using System.Text.Json;

namespace PRTelegramBot.Wrappers
{
    /// <summary>
    /// Сериализатор данных Json.
    /// </summary>
    public class JsonSerializerWrapper : IPRSerializer
    {
        #region Поля и свойства

        /// <summary>
        /// Опции сериализации. 
        /// </summary>
        private readonly JsonSerializerOptions options;

        #endregion

        #region IPRSerializator

        /// <inheritdoc />
        public T Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data, options);
        }

        /// <inheritdoc />
        public string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize<T>(data, options);
        }

        #endregion

        #region Конструкторы    

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options">Параметры сериализации.</param>
        public JsonSerializerWrapper(JsonSerializerOptions options = null)
        {
            this.options = options;
        }

        #endregion
    }
}

```

## Клавиатуры Telegram

В 1.1.2 JSON-конвертер Telegram.Bot восстанавливает конкретный тип клавиатуры при десериализации `ReplyMarkup`. Поддерживаются `InlineKeyboardMarkup`, `ReplyKeyboardMarkup`, `ReplyKeyboardRemove` и `ForceReplyMarkup`, в том числе при сериализации через `JsonSerializerWrapper`:

```csharp
using PRTelegramBot.Wrappers;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

var serializer = new JsonSerializerWrapper(JsonBotAPI.Options);
ReplyMarkup markup = new InlineKeyboardMarkup(
    InlineKeyboardButton.WithCallbackData("Open", "open"));
string json = serializer.Serialize<ReplyMarkup>(markup);
ReplyMarkup restored = serializer.Deserialize<ReplyMarkup>(json);
```

Для типов Telegram используйте `JsonBotAPI.Options`, чтобы JSON содержал имена свойств Bot API, например `keyboard`. Здесь `restored` — это `InlineKeyboardMarkup` с исходными callback-данными.
