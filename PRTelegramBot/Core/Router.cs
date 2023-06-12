using PRTelegramBot.Attributes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using static PRTelegramBot.Extensions.Step;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Extensions;
using PRTelegramBot.Configs;
using static PRTelegramBot.Core.TelegramService;
using PRTelegramBot.Models.InlineButtons;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Маршрутизация команд
    /// </summary>
    public class Router
    {
        /// <summary>
        /// Событие когда пользователь написал start с аргументом
        /// </summary>
        public event Func<ITelegramBotClient, Update,string, Task>? OnUserStartWithArgs;

        /// <summary>
        /// Событие когда нужно проверить привелегии перед выполнением команды
        /// </summary>
        public event Func<ITelegramBotClient, Update, Func<ITelegramBotClient, Update, Task>, int?, Task>? OnCheckPrivilege;

        /// <summary>
        /// Событие когда указан не верный тип сообщения
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnWrongTypeMessage;

        /// <summary>
        /// Событие когда указан не верный тип чат
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnWrongTypeChat;

        /// <summary>
        /// Событие когда не найдена команда
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnMissingCommand;

        /// <summary>
        /// Событие Обработки контактных данных
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnContactHandle;

        /// <summary>
        /// Событие обработки голосований
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnPollHandle;

        /// <summary>
        /// Событие обработки локации
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnLocationHandle;
        /// <summary>
        /// Событие обработки WebApps
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnWebAppsHandle;
        
        /// <summary>
        /// Событие когда отказано в доступе
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnAccessDenied;

        /// <summary>
        /// Событие обработки сообщением с документом
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnDocumentHandle;

        /// <summary>
        /// Событие обработки сообщением с аудио
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnAudioHandle;

        /// <summary>
        /// Событие обработки сообщением с видео
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnVideoHandle;

        /// <summary>
        /// Событие обработки сообщением с фото
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnPhotoHandle;

        /// <summary>
        /// Событие обработки сообщением с стикером
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnStickerHandle;

        /// <summary>
        /// Событие обработки сообщением с голосовым сообщением
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnVoiceHandle;

        /// <summary>
        /// Событие обработки сообщением с неизвестный типом сообщения
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnUnknownHandle;

        /// <summary>
        /// Событие обработки сообщением с местом
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnVenueHandle;

        /// <summary>
        /// Событие обработки сообщением с игрой
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnGameHandle;

        /// <summary>
        /// Событие обработки сообщением с видеозаметкой
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnVideoNoteHandle;

        /// <summary>
        /// Событие обработки сообщением с игральной кости
        /// </summary>
        public event Func<ITelegramBotClient,Update,Task>? OnDiceHandle;


        /// <summary>
        /// Клиент для телеграм бота
        /// </summary>
        private ITelegramBotClient _botClient;

        /// <summary>
        /// Словарь слеш команд
        /// </summary>
        private Dictionary<string, Func<ITelegramBotClient,Update,Task>> slashCommands;

        /// <summary>
        /// Словарь reply команд
        /// </summary>
        private Dictionary<string, Func<ITelegramBotClient,Update,Task>> messageCommands;

        /// <summary>
        /// Словарь приоритетных reply команд
        /// </summary>
        private Dictionary<string, Func<ITelegramBotClient,Update,Task>> messageCommandsPriority;

        /// <summary>
        /// Словарь Inline команд
        /// </summary>
        private Dictionary<Enum, Func<ITelegramBotClient,Update,Task>> inlineCommands;


        /// <summary>
        /// 
        /// </summary>
        private Dictionary<Telegram.Bot.Types.Enums.MessageType, Func<ITelegramBotClient,Update,Task>> typeMessage;

        public Router(ITelegramBotClient botClient)
        {
            _botClient                  = botClient;
            messageCommands             = new Dictionary<string, Func<ITelegramBotClient,Update,Task>>();
            messageCommandsPriority     = new Dictionary<string, Func<ITelegramBotClient,Update,Task>>();
            inlineCommands              = new Dictionary<Enum, Func<ITelegramBotClient,Update,Task>>();
            slashCommands               = new Dictionary<string, Func<ITelegramBotClient,Update,Task>>();

            RegisterCommnad();
        }

        private void UpdateEventLink()
        {
            typeMessage = new();
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Contact, OnContactHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Location, OnLocationHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.WebAppData, OnWebAppsHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Poll, OnPollHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Document, OnDocumentHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Audio, OnAudioHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Video, OnVideoHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Photo, OnPhotoHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Sticker, OnStickerHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Voice, OnVoiceHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Unknown, OnUnknownHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Venue, OnVenueHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Game, OnGameHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.VideoNote, OnVideoNoteHandle);
            typeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Dice, OnDiceHandle);
        }

        /// <summary>
        /// Автоматическая регистрация доступных команд для выполнения через рефлексию
        /// </summary>
        private void RegisterCommnad()
        {
            try
            {
                //Находим все методы которые используют наши атрибуты
                var messageMethods = ReflectionFinder.FindMessageMenuHandlers();
                var inlineMethods = ReflectionFinder.FindInlineMenuHandlers();
                var slashCommandMethods = ReflectionFinder.FindSlashCommandHandlers();
                ReflectionFinder.FindEnumHeaders();

                //Регистрируем Reply команды
                foreach (var method in messageMethods)
                {
                    bool priority = method.GetCustomAttribute<ReplyMenuHandlerAttribute>().Priority;
                    foreach (var command in method.GetCustomAttribute<ReplyMenuHandlerAttribute>().Commands)
                    {
                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient,Update,Task>), method, false);
                        messageCommands.Add(command, (Func<ITelegramBotClient,Update,Task>)serverMessageHandler);
                        if (priority)
                        {
                            messageCommandsPriority.Add(command, (Func<ITelegramBotClient,Update,Task>)serverMessageHandler);
                        }
                    }
                }

                //Регистрируем Inline команды
                foreach (var method in inlineMethods)
                {
                    foreach (var attribute in method.GetCustomAttributes(true))
                    {
                        if (attribute.GetType().IsGenericType &&
                            attribute.GetType().GetGenericTypeDefinition() == typeof(InlineCallbackHandlerAttribute<>))
                        {
                            var commandsProperty = attribute.GetType().GetProperty("Commands");
                            var commands = (IEnumerable<Enum>)commandsProperty.GetValue(attribute);

                            foreach (var command in commands)
                            {
                                var serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient,Update,Task>), method, false);
                                inlineCommands.Add(command, (Func<ITelegramBotClient,Update,Task>)serverMessageHandler);
                            }
                        }
                    }
                }

                //Регистрируем слеш команды
                foreach (var method in slashCommandMethods)
                {
                    foreach (var command in method.GetCustomAttribute<SlashHandlerAttribute>().Commands)
                    {
                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient,Update,Task>), method, false);
                        slashCommands.Add(command, (Func<ITelegramBotClient,Update,Task>)serverMessageHandler);
                    }
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Проверяет это слеш команда или нет
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="update">Обновление с телеграма</param>
        /// <returns></returns>
        private async Task<bool> IsSlashCommand(string command, Update update)
        {
            try
            {
                if (!command.Contains("/"))
                    return false;

                foreach (var commandExecute in slashCommands)
                {
                    if (command.ToLower().Contains(commandExecute.Key.ToLower()))
                    {
                        var requireUpdate = commandExecute.Value.Method.GetCustomAttribute<RequiredTypeChatAttribute>();
                        if (requireUpdate != null)
                        {
                            if (!requireUpdate.TypeUpdate.Contains(update.Message.Chat.Type))
                            {
                                OnWrongTypeChat?.Invoke(_botClient, update);
                                return true;
                            }
                        }
                        var privilages = commandExecute.Value.Method.GetCustomAttribute<AccessAttribute>();
                        if (privilages != null)
                        {
                            OnCheckPrivilege?.Invoke(_botClient, update, commandExecute.Value, privilages.Flags);
                            return true;
                        }
                        else
                        {
                            await commandExecute.Value(_botClient, update);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
                return false;
            }

        }

        /// <summary>
        /// Выполняет команду по сообщению reply
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="update">Обновление с телеграма</param>
        internal async Task ExecuteCommandByMessage(string command, Update update)
        {
            try
            {
                //
                if (command.Contains("(") && command.Contains(")"))
                {
                    command = command.Remove(command.LastIndexOf("(") - 1);
                }

                //
                if (await StartHasDeepLink(command, update))
                    return;

                //
                if (await IsSlashCommand(command, update))
                    return;

                //
                if (await IsHaveNextStep(command, update))
                    return;

                UpdateEventLink();
                //Вызываем свое событие для определенных типов сообщений
                foreach (var item in typeMessage)
                {
                    if (item.Key == update.Message.Type)
                    {
                        item.Value?.Invoke(_botClient, update);
                        return;
                    }
                }

                //
                foreach (var commandExecute in messageCommands)
                {

                    if (command.ToLower() == commandExecute.Key.ToLower())
                    {
                        var privilages = commandExecute.Value.Method.GetCustomAttribute<AccessAttribute>();
                        var requireDate = commandExecute.Value.Method.GetCustomAttribute<RequireTypeMessageAttribute>();
                        var requireUpdate = commandExecute.Value.Method.GetCustomAttribute<RequiredTypeChatAttribute>();

                        if (requireUpdate != null)
                        {
                            if (!requireUpdate.TypeUpdate.Contains(update.Message.Chat.Type))
                            {
                                OnWrongTypeChat?.Invoke(_botClient, update);
                                return;
                            }
                        }
                        if (requireDate != null)
                        {
                            if (!requireDate.TypeMessages.Contains(update.Message.Type))
                            {
                                OnWrongTypeMessage?.Invoke(_botClient, update);
                                return;
                            }
                        }
                        if (privilages != null)
                        {
                            OnCheckPrivilege?.Invoke(_botClient, update, commandExecute.Value, privilages.Flags);
                            return;
                        }

                        await commandExecute.Value(_botClient, update);
                        return;
                    }
                }

                OnMissingCommand?.Invoke(_botClient, update);
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Выполняет inline команду
        /// </summary>
        /// <param name="update">Обновление с телеграма</param>
        internal async Task ExecuteCommandByCallBack(Update update)
        {
            try
            {
                var command = InlineCallback.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    string msg = $"Пользователь {update.GetInfoUser()} вызвал команду {command.CommandType.GetDescription()}";
                    TelegramService.GetInstance().InvokeCommonLog(msg, TelegramService.TelegramEvents.CommandExecute, ConsoleColor.Magenta);
                    foreach (var commandCallback in inlineCommands)
                    {
                        if (((Enum)command.CommandType).Equals(commandCallback.Key) )
                        {
                            var requireUpdate = commandCallback.Value.Method.GetCustomAttribute<RequiredTypeChatAttribute>();
                            if (requireUpdate != null)
                            {
                                if (!requireUpdate.TypeUpdate.Contains(update.CallbackQuery.Message.Chat.Type))
                                {
                                    OnWrongTypeChat?.Invoke(_botClient, update);
                                    return;
                                }
                            }
                            var privilages = commandCallback.Value.Method.GetCustomAttribute<AccessAttribute>();
                            if (privilages != null)
                            {
                                OnCheckPrivilege?.Invoke(_botClient, update, commandCallback.Value, privilages.Flags);
                            }
                            else
                            {
                                await commandCallback.Value(_botClient, update);
                            }
                            return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Проверяет есть ли следующий шаг пользователя
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="update">Обновление с телеграма</param>
        /// <returns>True/false</returns>
        private async Task<bool> IsHaveNextStep(string command, Update update)
        {
            try
            {
                if (update.HasStep())
                {
                    foreach (var commandExecute in messageCommandsPriority)
                    {
                        if (command.ToLower() == commandExecute.Key.ToLower())
                        {
                            var requireUpdatex = commandExecute.Value.Method.GetCustomAttribute<RequiredTypeChatAttribute>();
                            if (requireUpdatex != null)
                            {
                                if (!requireUpdatex.TypeUpdate.Contains(update.Message.Chat.Type))
                                {
                                    OnWrongTypeChat?.Invoke(_botClient, update);
                                    return true;
                                }
                            }
                            await commandExecute.Value(_botClient, update);
                            update.ClearStepUser();
                            return true;
                        }
                    }

                    var cmd = update.GetStepOrNull().CommandDelegate;

                    var privilages = cmd.Method.GetCustomAttribute<AccessAttribute>();
                    var requireDate = cmd.Method.GetCustomAttribute<RequireTypeMessageAttribute>();
                    var requireUpdate = cmd.Method.GetCustomAttribute<RequiredTypeChatAttribute>();
                    if (requireUpdate != null)
                    {
                        if (!requireUpdate.TypeUpdate.Contains(update.Message.Chat.Type))
                        {
                            OnWrongTypeChat?.Invoke(_botClient, update);
                            return true;
                        }
                    }
                    if (requireDate != null)
                    {
                        if(!requireDate.TypeMessages.Contains(update.Message.Type))
                        {
                            OnWrongTypeMessage?.Invoke(_botClient, update);
                            return true;
                        }
                    }
                    if (privilages != null)
                    {
                        OnCheckPrivilege?.Invoke(_botClient, update, cmd, privilages.Flags);
                        return true;
                    }
                    await cmd(_botClient, update);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
                return false;
            }

        }

        /// <summary>
        /// Проверяет есть ли у старта deeplink
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="update">Обновление с телеграма</param>
        /// <returns>True/false</returns>
        private async Task<bool> StartHasDeepLink(string command, Update update)
        {
            try
            {
                if (command.ToLower().Contains("start") && command.Contains(" "))
                {
                    var spl = command.Split(' ');
                    if (!string.IsNullOrEmpty(spl[1]))
                    {
                        if (update.Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
                        {
                            OnUserStartWithArgs?.Invoke(_botClient, update, spl[1]);
                        }
                        return true;
                    }
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
                return false;
            }
        }

        internal async Task OnAccessDeniedInvoke(ITelegramBotClient botClient, Update update)
        {
            OnAccessDenied?.Invoke(botClient, update);
        }
    }
}
