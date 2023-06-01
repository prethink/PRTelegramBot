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
        /// Делегат с сигнатурой метода как должна выглядеть команда
        /// </summary>
        public delegate Task TelegramCommand(ITelegramBotClient botclient, Update update);
        /// <summary>
        /// 
        /// </summary>
        public delegate Task TelegramCommandArgs(ITelegramBotClient botclient, Update update, string args);
        public delegate Task TelegramCommandWithPrivilage(ITelegramBotClient botclient, Update update, UserPrivilege? requiredPrivilege);

        /// <summary>
        /// Событие когда пользователь написал start с аргументом
        /// </summary>
        public event TelegramCommandArgs? OnUserStartWithArgs;

        /// <summary>
        /// Событие когда нужно проверить привелегии перед выполнением команды
        /// </summary>
        public event TelegramCommandWithPrivilage? OnCheckPrivilege;

        /// <summary>
        /// Событие когда указан не верный тип сообщения
        /// </summary>
        public event TelegramCommand? OnWrongTypeMessage;

        /// <summary>
        /// Событие когда указан не верный тип чат
        /// </summary>
        public event TelegramCommand? OnWrongTypeChat;

        /// <summary>
        /// Событие когда не найдена команда
        /// </summary>
        public event TelegramCommand? OnMissingCommand;

        /// <summary>
        /// Событие Обработки контактных данных
        /// </summary>
        public event TelegramCommand? OnContactHandle;

        /// <summary>
        /// Событие обработки голосований
        /// </summary>
        public event TelegramCommand? OnPollHandle;

        /// <summary>
        /// Событие обработки локации
        /// </summary>
        public event TelegramCommand? OnLocationHandle;
        /// <summary>
        /// Событие обработки WebApps
        /// </summary>
        public event TelegramCommand? OnWebAppsHandle;
        
        /// <summary>
        /// Событие когда отказано в доступе
        /// </summary>
        public event TelegramCommand? OnAccessDenied;

        /// <summary>
        /// Событие обработки сообщением с документом
        /// </summary>
        public event TelegramCommand? OnDocumentHandle;

        /// <summary>
        /// Событие обработки сообщением с аудио
        /// </summary>
        public event TelegramCommand? OnAudioHandle;

        /// <summary>
        /// Событие обработки сообщением с видео
        /// </summary>
        public event TelegramCommand? OnVideoHandle;

        /// <summary>
        /// Событие обработки сообщением с фото
        /// </summary>
        public event TelegramCommand? OnPhotoHandle;

        /// <summary>
        /// Событие обработки сообщением с стикером
        /// </summary>
        public event TelegramCommand? OnStickerHandle;

        /// <summary>
        /// Событие обработки сообщением с голосовым сообщением
        /// </summary>
        public event TelegramCommand? OnVoiceHandle;

        /// <summary>
        /// Событие обработки сообщением с неизвестный типом сообщения
        /// </summary>
        public event TelegramCommand? OnUnknownHandle;

        /// <summary>
        /// Событие обработки сообщением с местом
        /// </summary>
        public event TelegramCommand? OnVenueHandle;

        /// <summary>
        /// Событие обработки сообщением с игрой
        /// </summary>
        public event TelegramCommand? OnGameHandle;

        /// <summary>
        /// Событие обработки сообщением с видеозаметкой
        /// </summary>
        public event TelegramCommand? OnVideoNoteHandle;

        /// <summary>
        /// Событие обработки сообщением с игральной кости
        /// </summary>
        public event TelegramCommand? OnDiceHandle;


        /// <summary>
        /// Клиент для телеграм бота
        /// </summary>
        private ITelegramBotClient _botClient;

        /// <summary>
        /// Словарь слеш команд
        /// </summary>
        private Dictionary<string, TelegramCommand> slashCommands;

        /// <summary>
        /// Словарь reply команд
        /// </summary>
        private Dictionary<string, TelegramCommand> messageCommands;

        /// <summary>
        /// Словарь приоритетных reply команд
        /// </summary>
        private Dictionary<string, TelegramCommand> messageCommandsPriority;

        /// <summary>
        /// Словарь Inline команд
        /// </summary>
        private Dictionary<Header, TelegramCommand> inlineCommands;


        /// <summary>
        /// 
        /// </summary>
        private Dictionary<Telegram.Bot.Types.Enums.MessageType, TelegramCommand> typeMessage;

        public Router(ITelegramBotClient botClient)
        {
            _botClient                  = botClient;
            messageCommands             = new Dictionary<string, TelegramCommand>();
            messageCommandsPriority     = new Dictionary<string, TelegramCommand>();
            inlineCommands              = new Dictionary<Header, TelegramCommand>();
            slashCommands               = new Dictionary<string, TelegramCommand>();

            RegisterCommnad();
        }

        internal void UpdateEventLink()
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
        internal void RegisterCommnad()
        {
            try
            {
                //Находим все методы которые используют наши атрибуты
                var messageMethods = MethodFinder.FindMessageMenuHandlers();
                var inlineMethods = MethodFinder.FindInlineMenuHandlers();
                var slashCommandMethods = MethodFinder.FindSlashCommandHandlers();

                //Регистрируем Reply команды
                foreach (var method in messageMethods)
                {
                    bool priority = method.GetCustomAttribute<ReplyMenuHandlerAttribute>().Priority;
                    foreach (var command in method.GetCustomAttribute<ReplyMenuHandlerAttribute>().Commands)
                    {
                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(TelegramCommand), method, false);
                        messageCommands.Add(command, (TelegramCommand)serverMessageHandler);
                        if (priority)
                        {
                            messageCommandsPriority.Add(command, (TelegramCommand)serverMessageHandler);
                        }
                    }
                }

                //Регистрируем Inline команды
                foreach (var method in inlineMethods)
                {
                    foreach (var command in method.GetCustomAttribute<InlineCallbackHandlerAttribute>().Commands)
                    {
                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(TelegramCommand), method, false);
                        inlineCommands.Add(command, (TelegramCommand)serverMessageHandler);
                    }
                }

                //Регистрируем слеш команды
                foreach (var method in slashCommandMethods)
                {
                    foreach (var command in method.GetCustomAttribute<SlashHandlerAttribute>().Commands)
                    {
                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(TelegramCommand), method, false);
                        slashCommands.Add(command, (TelegramCommand)serverMessageHandler);
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
        internal async Task<bool> IsSlashCommand(string command, Update update)
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
                        if (privilages != null && privilages.RequiredPrivilege != null)
                        {
                            OnCheckPrivilege?.Invoke(_botClient, update, privilages.RequiredPrivilege);
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
                        if (privilages != null && privilages.RequiredPrivilege != null)
                        {
                            OnCheckPrivilege?.Invoke(_botClient, update, privilages.RequiredPrivilege);
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
                        if (command.CommandType == commandCallback.Key)
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
                            if (privilages != null && privilages.RequiredPrivilege != null)
                            {
                                OnCheckPrivilege?.Invoke(_botClient, update, privilages.RequiredPrivilege);
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
        internal async Task<bool> IsHaveNextStep(string command, Update update)
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
                    if (privilages != null && privilages.RequiredPrivilege != null)
                    {
                        OnCheckPrivilege?.Invoke(_botClient, update, privilages.RequiredPrivilege);
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
        internal async Task<bool> StartHasDeepLink(string command, Update update)
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
