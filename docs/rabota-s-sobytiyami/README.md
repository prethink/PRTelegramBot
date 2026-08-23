# Работа с событиями

Поскольку PRTelegramBot основан на библиотеке [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot), он может использовать его функционал. Из-за того что в PRTelegramBot можно создавать сразу несколько экземпляров ботов, были реализованы разные события на функционал библиотеки [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot).

События в PRBot.Events

<pre class="language-csharp"><code class="lang-csharp"><strong>    /// &#x3C;summary>
</strong>    /// События для бота.
    /// &#x3C;/summary>
    public sealed class TEvents
    {
        #region Поля и свойства

        /// &#x3C;summary>
        /// Бот для событий.
        /// &#x3C;/summary>
        public PRBotBase Bot { get; private set; }

        /// &#x3C;summary>
        /// События для обновления типа сообщения.
        /// &#x3C;/summary>
        public MessageEvents MessageEvents { get; private set; }

        /// &#x3C;summary>
        /// События обновлений.
        /// &#x3C;/summary>
        public UpdateEvents UpdateEvents { get; private set; }

        /// &#x3C;summary>
        /// События команд.
        /// &#x3C;/summary>
        public CommandsEvents CommandsEvents { get; private set; }

        #endregion

        #region События

        /// &#x3C;summary>
        /// Событие когда отказано в доступе.
        /// &#x3C;/summary>
        public event Func&#x3C;BotEventArgs, Task>? OnAccessDenied;

        /// &#x3C;summary>
        /// Событие когда пользователь написал start с аргументом.
        /// &#x3C;/summary>
        public event Func&#x3C;StartEventArgs, Task>? OnUserStartWithArgs;

        /// &#x3C;summary>
        /// Событие когда нужно проверить привилегии перед выполнением команды.
        /// &#x3C;/summary>
        public event Func&#x3C;PrivilegeEventArgs, Task>? OnCheckPrivilege;

        /// &#x3C;summary>
        /// Событие когда указан не верный тип сообщения.
        /// &#x3C;/summary>
        public event Func&#x3C;BotEventArgs, Task>? OnWrongTypeMessage;

        /// &#x3C;summary>
        /// Событие когда указан не верный тип чат.
        /// &#x3C;/summary>
        public event Func&#x3C;BotEventArgs, Task>? OnWrongTypeChat;

        /// &#x3C;summary>
        /// Событие когда не найдена команда.
        /// &#x3C;/summary>
        public event Func&#x3C;BotEventArgs, Task>? OnMissingCommand;

        /// &#x3C;summary>
        /// Событие когда произошла ошибка при обработке команды.
        /// &#x3C;/summary>
        public event Func&#x3C;BotEventArgs, Task>? OnErrorCommand;

        /// &#x3C;summary>
        /// Событие ошибки.
        /// &#x3C;/summary>
        public event Func&#x3C;ErrorLogEventArgs, Task>? OnErrorLog;

        /// &#x3C;summary>
        /// Событие общих логов.
        /// &#x3C;/summary>
        public event Func&#x3C;CommonLogEventArgs, Task>? OnCommonLog;

        #endregion

        #region Методы

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnUserStartWithArgs"/>.
        /// &#x3C;/summary>
        /// &#x3C;param name="e">Аргументы события.&#x3C;/param>
        internal void OnUserStartWithArgsInvoke(StartEventArgs e) => OnUserStartWithArgs?.Invoke(e);

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnMissingCommand"/>.
        /// &#x3C;/summary>
        /// &#x3C;param name="e">Аргументы события.&#x3C;/param>
        internal void OnMissingCommandInvoke(BotEventArgs e) => OnMissingCommand?.Invoke(e);

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnErrorCommand"/>.
        /// &#x3C;/summary>
        /// &#x3C;param name="e">Аргументы события.&#x3C;/param>
        internal void OnErrorCommandInvoke(BotEventArgs e) => OnErrorCommand?.Invoke(e);

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnAccessDenied"/>.
        /// &#x3C;/summary>
        /// &#x3C;param name="e">Аргументы события.&#x3C;/param>
        internal void OnAccessDeniedInvoke(BotEventArgs e) => OnAccessDenied?.Invoke(e);

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnCheckPrivilege"/>.
        /// &#x3C;/summary>
        /// &#x3C;param name="e">Аргументы события.&#x3C;/param>
        internal void OnCheckPrivilegeInvoke(PrivilegeEventArgs e) => OnCheckPrivilege?.Invoke(e);

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnWrongTypeMessage"/>.
        /// &#x3C;/summary>
        /// &#x3C;param name="e">Аргументы события.&#x3C;/param>
        internal void OnWrongTypeMessageInvoke(BotEventArgs e) => OnWrongTypeMessage?.Invoke(e);

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnWrongTypeChat"/>.
        /// &#x3C;/summary>
        /// &#x3C;param name="e">Аргументы события.&#x3C;/param>
        internal void OnWrongTypeChatInvoke(BotEventArgs e) => OnWrongTypeChat?.Invoke(e);

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnErrorLog"/>.
        /// &#x3C;/summary>
        /// &#x3C;param name="e">Аргументы события.&#x3C;/param>
        public void OnErrorLogInvoke(ErrorLogEventArgs e) => OnErrorLog?.Invoke(e);

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnCommonLog"/> через готовый объект аргументов.
        /// &#x3C;/summary>
        /// &#x3C;param name="e">Создатель аргументов события.&#x3C;/param>
        public void OnCommonLogInvoke(CommonLogEventArgsCreator e) =>
            OnCommonLog?.Invoke(new CommonLogEventArgs(e.Context, e));

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnCommonLog"/> с простым сообщением.
        /// &#x3C;/summary>
        /// &#x3C;param name="message">Текст сообщения.&#x3C;/param>
        public void OnCommonLogInvoke(string message) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, "Common"));

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnCommonLog"/> с указанием типа лога.
        /// &#x3C;/summary>
        /// &#x3C;param name="message">Текст сообщения.&#x3C;/param>
        /// &#x3C;param name="type">Тип лога.&#x3C;/param>
        public void OnCommonLogInvoke(string message, string type) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type));

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnCommonLog"/> с контекстом бота.
        /// &#x3C;/summary>
        /// &#x3C;param name="message">Текст сообщения.&#x3C;/param>
        /// &#x3C;param name="type">Тип лога.&#x3C;/param>
        /// &#x3C;param name="context">Контекст бота.&#x3C;/param>
        public void OnCommonLogInvoke(string message, string type, BotContext context) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, context));

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnCommonLog"/> с цветом текста.
        /// &#x3C;/summary>
        /// &#x3C;param name="message">Текст сообщения.&#x3C;/param>
        /// &#x3C;param name="type">Тип лога.&#x3C;/param>
        /// &#x3C;param name="color">Цвет текста в консоли.&#x3C;/param>
        public void OnCommonLogInvoke(string message, string type, ConsoleColor color) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color));

        /// &#x3C;summary>
        /// Вызвать событие &#x3C;see cref="OnCommonLog"/> с цветом текста и контекстом бота.
        /// &#x3C;/summary>
        /// &#x3C;param name="message">Текст сообщения.&#x3C;/param>
        /// &#x3C;param name="type">Тип лога.&#x3C;/param>
        /// &#x3C;param name="color">Цвет текста в консоли.&#x3C;/param>
        /// &#x3C;param name="context">Контекст бота.&#x3C;/param>
        public void OnCommonLogInvoke(string message, string type, ConsoleColor color, BotContext context) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color, context));

        /// &#x3C;summary>
        /// Дополнительный метод вызова &#x3C;see cref="OnCommonLog"/> с типом лога.
        /// &#x3C;/summary>
        /// &#x3C;param name="message">Текст сообщения.&#x3C;/param>
        /// &#x3C;param name="type">Тип лога.&#x3C;/param>
        public void OnCommonLogInvokeInvoke(string message, string type) =>
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type));

        #endregion

        #region Конструкторы

        /// &#x3C;summary>
        /// Конструктор.
        /// &#x3C;/summary>
        /// &#x3C;param name="bot">Бот.&#x3C;/param>
        public TEvents(PRBotBase bot)
        {
            Bot = bot;
            MessageEvents = new MessageEvents();
            UpdateEvents = new UpdateEvents();
            CommandsEvents = new CommandsEvents();
        }

        #endregion
    }
</code></pre>

## Пример подписки и использования событий

Создадим статический класс ExampleEvent, который будет хранить методы для событий:

```csharp
using ConsoleExample.Extension;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.EventsArgs;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples.Events
{
    public static class ExampleEvents
    {
        public static async Task OnWrongTypeChat(BotEventArgs e)
        {
            string msg = "Неверный тип чата";
            await Helpers.Message.Send(e.Context, msg);
        }

        public static async Task OnMissingCommand(BotEventArgs args)
        {
            string msg = "Не найдена команда";
            await Helpers.Message.Send(args.Context, msg);
        }

        public static async Task OnErrorCommand(BotEventArgs args)
        {
            string msg = "Произошла ошибка при обработке команды";
            await Helpers.Message.Send(args.Context, msg);
        }

        /// <summary>
        /// Событие проверки привилегий пользователя
        /// </summary>
        /// <param name="callback">callback функция выполняется в случае успеха</param>
        /// <param name="mask">Маска доступа</param>
        /// Подписка на событие проверки привелегий <see cref="Program"/>
        public static async Task OnCheckPrivilege(PrivilegeEventArgs e)
        {
            if (!e.Mask.HasValue)
            {
                // Нет маски доступа, выполняем метод.
                await e.ExecuteMethod(e.Context);
                return;
            }

            // Получаем значение маски требуемого доступа.
            var requiredAccess = e.Mask.Value;

            // Получаем флаги доступа пользователя.
            // Здесь вы на свое усмотрение реализываете логику получение флагов, например можно из базы данных получить.
            var userFlags = e.Context.Update.LoadExampleFlagPrivilege();

            if (requiredAccess.HasFlag(userFlags))
            {
                // Доступ есть, выполняем метод.
                await e.ExecuteMethod(e.Context);
                return;
            }

            // Доступа нет.
            string errorMsg = "У вас нет доступа к данной функции";
            await Helpers.Message.Send(e.Context, errorMsg);
            return;

        }

        public static async Task OnUserStartWithArgs(StartEventArgs args)
        {
            string msg = "Пользователь отправил старт с аргументом";
            await Helpers.Message.Send(args.Context, msg);
        }
        public static async Task OnWrongTypeMessage(BotEventArgs e)
        {
            string msg = "Неверный тип сообщения";
            await Helpers.Message.Send(e.Context, msg);
        }
    }
}

```

Ниже представлен пример создание нового бота, который подписывается на эти события.

```csharp
// Парсинг динамических команд из json файла в формате ключ:значение.
var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
var dynamicCommands = botJsonProvider.GetKeysAndValues();

var telegram = new PRBotBuilder("")
                    .SetBotId(0)
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
                    .AddConfigPath(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json")
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(dynamicCommands)
                    .AddMiddlewares(new OneMiddleware(), new TwoMiddleware(), new ThreeMiddleware())
                    .Build();

// Подписка на простые логи.
telegram.Events.OnCommonLog += Telegram_OnLogCommon;
// Подписка на логи с ошибками.
telegram.Events.OnErrorLog += Telegram_OnLogError;
// Запуск работы бота.
await telegram.StartAsync();
// Инициализация событий для бота.
InitEvents(telegram);

void InitEvents(PRBotBase bot)
{
    // Обработка до всех update 
    bot.Events.UpdateEvents.OnPreUpdate += Handler_OnUpdate;

    // Обработка после всех update
    bot.Events.UpdateEvents.OnPostUpdate += Handler_OnPostUpdate;

    // Обработка не правильный тип сообщений
    bot.Events.OnWrongTypeMessage += ExampleEvent.OnWrongTypeMessage;

    // Обработка пользователь написал в чат start с deeplink
    bot.Events.OnUserStartWithArgs += ExampleEvent.OnUserStartWithArgs;

    // Обработка проверка привилегий
    bot.Events.OnCheckPrivilege += ExampleEvent.OnCheckPrivilege;

    // Обработка пропущенной  команды
    bot.Events.OnMissingCommand += ExampleEvent.OnMissingCommand;

    // Обработка не верного типа чата
    bot.Events.OnWrongTypeChat += ExampleEvent.OnWrongTypeChat;

    // Обработка локаций
    bot.Events.MessageEvents.OnLocationHandle += ExampleEvent.OnLocationHandle;

    // Обработка контактных данных
    bot.Events.MessageEvents.OnContactHandle += ExampleEvent.OnContactHandle;

    // Обработка голосований
    bot.Events.MessageEvents.OnPollHandle += ExampleEvent.OnPollHandle;

    // Обработка WebApps
    bot.Events.MessageEvents.OnWebAppsHandle += ExampleEvent.OnWebAppsHandle;

    // Обработка, когда пользователю отказано в доступе
    bot.Events.OnAccessDenied += ExampleEvent.OnAccessDenied;

    //Обработка сообщения с документом
    bot.Events.MessageEvents.OnDocumentHandle += ExampleEvent.OnDocumentHandle;

    //Обработка сообщения с аудио
    bot.Events.MessageEvents.OnAudioHandle += ExampleEvent.OnAudioHandle;

    //Обработка сообщения с видео
    bot.Events.MessageEvents.OnVideoHandle += ExampleEvent.OnVideoHandle;

    //Обработка сообщения с фото
    bot.Events.MessageEvents.OnPhotoHandle += ExampleEvent.OnPhotoHandle;

    //Обработка сообщения с стикером
    bot.Events.MessageEvents.OnStickerHandle += ExampleEvent.OnStickerHandle;

    //Обработка сообщения с голосовым сообщением
    bot.Events.MessageEvents.OnVoiceHandle += ExampleEvent.OnVoiceHandle;

    //Обработка сообщения с неизвестным типом
    bot.Events.MessageEvents.OnUnknownHandle += ExampleEvent.OnUnknownHandle;

    //Обработка сообщения с местоположением
    bot.Events.MessageEvents.OnVenueHandle += ExampleEvent.OnVenueHandle;

    //Обработка сообщения с игрой
    bot.Events.MessageEvents.OnGameHandle += ExampleEvent.OnGameHandle;

    //Обработка сообщения с видеозаметкой
    bot.Events.MessageEvents.OnVideoNoteHandle += ExampleEvent.OnVideoNoteHandle;

    //Обработка сообщения с игральной костью
    bot.Events.MessageEvents.OnDiceHandle += ExampleEvent.OnDiceHandle;

    //Обработка обновления изменения группы/чата
    bot.Events.UpdateEvents.OnMyChatMemberHandle += ExampleEvent.OnUpdateMyChatMember;
}

async Task<UpdateResult> Handler_OnUpdate(BotEventArgs e)
{
    return UpdateResult.Continue;
}

async Task Handler_OnWithoutMessageUpdate(BotEventArgs e)
{
    //Обработка обновление кроме message и callback
}
```
