# MediaSender

```csharp
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Services.Media
{
    public class MediaSender
    {
        #region Методы

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="stream">Поток.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendPhoto(IBotContext context, long chatId, string text, Stream stream, OptionMessage option = null)

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendPhoto(IBotContext context, long chatId, string text, string filePath, OptionMessage option = null)

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="msg">Текст.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendPhotoWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage option = null)

        /// <summary>
        /// Отправка сообщения с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="msg">Текст.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendMediaWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage option = null)

        /// <summary>
        /// Отправка группы фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="filepaths">Путь к файлам.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Коллекция сообщений.</returns>
        public static async Task<Message[]> SendPhotoGroup(IBotContext context, long chatId, string text, List<string> filepaths, OptionMessage option = null)

        /// <summary>
        /// Отправка файла.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendFile(IBotContext context, long chatId, string text, string filePath, OptionMessage option = null)
    }
}

```

## Интерфейсы медиа Telegram.Bot в 1.1.2

При прямом вызове Telegram.Bot интерфейсы `IAlbumInputMedia` и `IInputRichMedia` предоставляют свойства `Type`, `Media`, `Caption`, `ParseMode` и `CaptionEntities`. JSON-сериализация поддерживает live photos в альбомах и документы в rich-сообщениях.

`IInputMediaSettings` позволяет задавать расположение подписи и спойлеры для фото, видео, анимаций и live photos через общий интерфейс:

```csharp
using Telegram.Bot.Types;

IAlbumInputMedia media = new InputMediaPhoto(InputFile.FromFileId("existing-photo"));
IInputMediaSettings settings = (IInputMediaSettings)media;
settings.ShowCaptionAboveMedia = true;
settings.HasSpoiler = true;
```

{% hint style="warning" %}
Если вы сами реализуете `IAlbumInputMedia` или `IInputRichMedia`, добавьте новые getter-свойства: `InputMediaType Type`, `InputFile Media`, `string? Caption`, `ParseMode ParseMode` и `MessageEntity[]? CaptionEntities`. В `IInputRichMedia` свойство `Type` требовалось и раньше. Перечисления находятся в `Telegram.Bot.Types.Enums`, остальные типы — в `Telegram.Bot.Types`. Встроенные классы `InputMedia*` уже реализуют эти свойства.
{% endhint %}
