﻿using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using static PRTelegramBot.Extensions.StepExtension;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Маршрутизация команд.
    /// </summary>
    public sealed class Router : IExecuteCommand
    {
        #region Поля и свойства

        /// <summary>
        /// 
        /// </summary>
        public long SlashCommandCount => slashCommands.Count;

        /// <summary>
        /// 
        /// </summary>
        public long ReplyCommandCount => replyCommands.Count;

        /// <summary>
        /// 
        /// </summary>
        public long ReplyDynamicCommandCount => replyDynamicCommands.Count;

        /// <summary>
        /// 
        /// </summary>
        public long InlineCommandCount => inlineCommands.Count;

        /// <summary>
        /// Клиент для telegram бота
        /// </summary>
        private PRBot bot;

        /// <summary>
        /// 
        /// </summary>
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Словарь слеш команд
        /// </summary>
        private Dictionary<string, CommandHandler> slashCommands { get; set; } = new();

        /// <summary>
        /// Словарь reply команд
        /// </summary>
        private Dictionary<string, CommandHandler> replyCommands { get; set; } = new();

        /// <summary>
        /// Словарь reply dynamic команд
        /// </summary>
        private Dictionary<string, CommandHandler> replyDynamicCommands { get; set; } = new();

        /// <summary>
        /// Словарь Inline команд
        /// </summary>
        private Dictionary<Enum, CommandHandler> inlineCommands { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Telegram.Bot.Types.Enums.MessageType, Func<ITelegramBotClient, Update, Task>> TypeMessage { get; private set; }

        #endregion

        #region События

        /// <summary>
        /// Событие когда пользователь написал start с аргументом
        /// </summary>
        public event Func<ITelegramBotClient, Update, string, Task>? OnUserStartWithArgs;

        /// <summary>
        /// Событие когда нужно проверить привилегии перед выполнением команды
        /// </summary>
        public event Func<ITelegramBotClient, Update, Func<ITelegramBotClient, Update, Task>, int?, Task>? OnCheckPrivilege;

        /// <summary>
        /// Событие когда указан не верный тип сообщения
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnWrongTypeMessage;

        /// <summary>
        /// Событие когда указан не верный тип чат
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnWrongTypeChat;

        /// <summary>
        /// Событие когда не найдена команда
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnMissingCommand;

        /// <summary>
        /// Событие Обработки контактных данных
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnContactHandle;

        /// <summary>
        /// Событие обработки голосований
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnPollHandle;

        /// <summary>
        /// Событие обработки локации
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnLocationHandle;
        /// <summary>
        /// Событие обработки WebApps
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnWebAppsHandle;

        /// <summary>
        /// Событие когда отказано в доступе
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnAccessDenied;

        /// <summary>
        /// Событие обработки сообщением с документом
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnDocumentHandle;

        /// <summary>
        /// Событие обработки сообщением с аудио
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnAudioHandle;

        /// <summary>
        /// Событие обработки сообщением с видео
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnVideoHandle;

        /// <summary>
        /// Событие обработки сообщением с фото
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnPhotoHandle;

        /// <summary>
        /// Событие обработки сообщением с стикером
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnStickerHandle;

        /// <summary>
        /// Событие обработки сообщением с голосовым сообщением
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnVoiceHandle;

        /// <summary>
        /// Событие обработки сообщением с неизвестный типом сообщения
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnUnknownHandle;

        /// <summary>
        /// Событие обработки сообщением с местом
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnVenueHandle;

        /// <summary>
        /// Событие обработки сообщением с игрой
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnGameHandle;

        /// <summary>
        /// Событие обработки сообщением с видеозаметкой
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnVideoNoteHandle;

        /// <summary>
        /// Событие обработки сообщением с игральной кости
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnDiceHandle;

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет Reply команду.
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <param name="delegate">Метод обработки</param>
        /// <returns>True - команда добавлена / False - ошибка при добавление или команда не добавлена</returns>
        public bool RegisterReplyCommand(string command, Func<ITelegramBotClient, Update, Task> @delegate)
        {
            try
            {
                replyCommands.Add(command, new CommandHandler(@delegate));
                return true;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Добавляет Slash команду
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <param name="delegate">Метод обработки</param>
        /// <returns>True - команда добавлена / False - ошибка при добавление или команда не добавлена</returns>
        public bool RegisterSlashCommand(string command, Func<ITelegramBotClient, Update, Task> @delegate)
        {
            try
            {
                slashCommands.Add(command, new CommandHandler(@delegate));
                return true;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Добавляет inline команду
        /// </summary>
        /// <param name="@enum">Заголовок команды</param>
        /// <param name="delegate">Метод обработки</param>
        /// <returns>True - команда добавлена / False - ошибка при добавление или команда не добавлена</returns>
        public bool RegisterInlineCommand(Enum @enum, Func<ITelegramBotClient, Update, Task> @delegate)
        {
            try
            {
                ReflectionUtils.AddEnumsHeader(@enum);
                inlineCommands.Add(@enum, new CommandHandler(@delegate));
                return true;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return false;
            }
        }

        public bool RemoveReplyCommand(string command)
        {
            try
            {
                replyCommands.Remove(command);
                return true;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return false;
            }
        }

        public bool RemoveSlashCommand(string command)
        {
            try
            {
                slashCommands.Remove(command);
                return true;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return false;
            }
        }

        public bool RemoveInlineCommand(Enum @enum)
        {
            try
            {
                inlineCommands.Remove(@enum);
                return true;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Выполняет команду по сообщению reply
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="update">Обновление с телеграма</param>
        public async Task ExecuteCommandByMessage(string command, Update update)
        {
            try
            {
                //
                if (command.Contains("(") && command.Contains(")"))
                    command = command.Remove(command.LastIndexOf("(") - 1);

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
                foreach (var item in TypeMessage)
                {
                    if (item.Key == update!.Message!.Type)
                    {
                        item.Value?.Invoke(bot.botClient, update);
                        return;
                    }
                }

                var resultExecute = await ExecuteCommand(command, update, GetAllReplyCommands());

                if (resultExecute == ResultCommand.NotFound)
                    OnMissingCommand?.Invoke(bot.botClient, update);
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Выполняет inline команду
        /// </summary>
        /// <param name="update">Обновление с телеграма</param>
        public async Task ExecuteCommandByCallBack(Update update)
        {
            try
            {
                var command = InlineCallback.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    string msg = $"The user {update.GetInfoUser().Trim()} invoked the command {command.CommandType.GetDescription()}";
                    bot.InvokeCommonLog(msg, "CallBackCommand", ConsoleColor.Magenta);

                    var resultExecute = await ExecuteCommand(command.CommandType, update, inlineCommands);
                }
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
            }
        }

        public async Task OnAccessDeniedInvoke(ITelegramBotClient botClient, Update update)
        {
            OnAccessDenied?.Invoke(botClient, update);
        }

        private void UpdateEventLink()
        {
            TypeMessage = new();
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Contact, OnContactHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Location, OnLocationHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.WebAppData, OnWebAppsHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Poll, OnPollHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Document, OnDocumentHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Audio, OnAudioHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Video, OnVideoHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Photo, OnPhotoHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Sticker, OnStickerHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Voice, OnVoiceHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Unknown, OnUnknownHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Venue, OnVenueHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Game, OnGameHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.VideoNote, OnVideoNoteHandle);
            TypeMessage.Add(Telegram.Bot.Types.Enums.MessageType.Dice, OnDiceHandle);
        }

        /// <summary>
        /// Автоматическая регистрация доступных команд для выполнения через рефлексию
        /// </summary>
        private void RegisterCommands()
        {
            ReflectionUtils.FindEnumHeaders();
            RegisterCommandsViaInstanceClasses();
            RegisterStaticCommands();
        }

        private void RegisterCommandsViaInstanceClasses()
        {
            Type[] servicesToRegistration = ReflectionUtils.FindServicesToRegistration();

            foreach (var serviceType in servicesToRegistration)
            {
                var methods = serviceType.GetMethods().Where(x => !x.IsStatic).ToArray();
                RegisterMethodFromClass(typeof(ReplyMenuHandlerAttribute), methods, replyCommands);
                RegisterMethodFromClass(typeof(ReplyMenuDynamicHandlerAttribute), methods, replyDynamicCommands);
                RegisterMethodFromClass(typeof(SlashHandlerAttribute), methods, slashCommands);
                RegisterMethodFromClass(typeof(InlineCallbackHandlerAttribute<>), methods, inlineCommands);
            }
        }

        private void RegisterStaticCommands()
        {
            MethodInfo[] messageMethods = ReflectionUtils.FindStaticMessageMenuHandlers(bot.Options.BotId);
            MethodInfo[] messageDictionaryMethods = ReflectionUtils.FindStaticMessageMenuDictionaryHandlers(bot.Options.BotId);
            MethodInfo[] inlineMethods = ReflectionUtils.FindStaticInlineMenuHandlers(bot.Options.BotId);
            MethodInfo[] slashCommandMethods = ReflectionUtils.FindStaticSlashCommandHandlers(bot.Options.BotId);

            RegisterCommand(typeof(ReplyMenuHandlerAttribute), messageMethods, replyCommands);
            RegisterCommand(typeof(ReplyMenuDynamicHandlerAttribute), messageDictionaryMethods, replyDynamicCommands);
            RegisterCommand(typeof(SlashHandlerAttribute), slashCommandMethods, slashCommands);
            RegisterCommand(typeof(InlineCallbackHandlerAttribute<>), inlineMethods, inlineCommands);
        }

        private void RegisterMethodFromClass<T>(Type attributetype, MethodInfo[] methods, Dictionary<T, CommandHandler> collectionCommands)
        {
            foreach (var method in methods)
            {
                try
                {
                    var attribute = method.GetCustomAttributes().FirstOrDefault(attr => attr.GetType().Name == attributetype.Name);

                    if (attribute == null || ((IBaseQueryAttribute)attribute).BotId != bot.Options.BotId && ((IBaseQueryAttribute)attribute).BotId != -1)
                        continue;

                    bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(method);
                    if (!isValidMethod)
                    {
                        bot.InvokeErrorLog(new Exception($"The method {method.Name} has an invalid signature. " +
                            $"Required return {nameof(Task)} arg1 {nameof(ITelegramBotClient)} arg2 {nameof(Update)}"));
                        continue;
                    }

                    var telegramhandler = HandlerFactory.CreateHandler((IBaseQueryAttribute)attribute, method, _serviceProvider);
                    foreach (var command in ((ICommandStore<T>)attribute).Commands)
                        collectionCommands.Add(command, telegramhandler);
                }
                catch (Exception ex)
                {
                    bot.InvokeErrorLog(ex);
                }
            }
        }

        private void RegisterCommand<T>(Type attributetype, MethodInfo[] methods, Dictionary<T, CommandHandler> collectionCommands)
        {
            foreach (var method in methods)
            {
                try
                {
                    if (!method.IsStatic)
                        continue;

                    var attribute = method.GetCustomAttributes().FirstOrDefault(attr => attr.GetType().Name == attributetype.Name);
                    if (attribute == null)
                        continue;

                    foreach (var command in ((ICommandStore<T>)attribute).Commands)
                    {
                        bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(method);
                        if (!isValidMethod)
                        {
                            bot.InvokeErrorLog(new Exception($"The method {method.Name} has an invalid signature for the {attribute.GetType()} attribute. The method will be ignored."));
                            continue;
                        }

                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), method, false);
                        var telegramCommand = HandlerFactory.CreateHandler((IBaseQueryAttribute)attribute, (Func<ITelegramBotClient, Update, Task>)serverMessageHandler, null);
                        collectionCommands.Add(command, telegramCommand);
                    }
                }
                catch (Exception ex)
                {
                    bot.InvokeErrorLog(ex);
                }
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
                if (!update.HasStepHandler())
                    return false;

                var resultPriorityCommandExecute = await ExecuteCommand(command, update, GetAllReplyCommands());

                if (resultPriorityCommandExecute != ResultCommand.NotFound)
                {
                    update.ClearStepUserHandler();
                    return true;
                }

                var step = update.GetStepHandler().GetExecuteMethod();
                return await ExecuteMethod(update, new CommandHandler(step)) == ResultCommand.Executed;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return false;
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
                if (!command.StartsWith("/"))
                    return false;

                return await ExecuteCommand(command, update, slashCommands) != ResultCommand.NotFound;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
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
                if (!command.ToLower().Contains("start") && command.Contains(" "))
                    return false;

                var spl = command.Split(' ');
                if (spl.Length < 2 || string.IsNullOrEmpty(spl[1]))
                    return false;

                if (update.Message.Chat.Type != Telegram.Bot.Types.Enums.ChatType.Private)
                    return false;

                OnUserStartWithArgs?.Invoke(bot.botClient, update, spl[1]);
                return true;
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
                return false;
            }
        }

        private async Task<ResultCommand> ExecuteCommand<T>(T command, Update update, Dictionary<T, CommandHandler> commandList)
        {
            foreach (var commandExecute in commandList)
            {
                if (CanExecute(command, commandExecute.Key, commandExecute.Value))
                    return await ExecuteMethod(update, commandExecute.Value);
            }
            return ResultCommand.NotFound;
        }

        private bool CanExecute<T>(T currentCommand, T command, CommandHandler handler)
        {
            if (handler.CommandComparison == CommandComparison.Equals)
            {
                if (currentCommand is string stringCommand &&  handler is StringCommandHandler stringHandler && stringCommand.Equals(command as string, stringHandler.StringComparison))
                    return true;
                else if(currentCommand.Equals(command))
                    return true;
            }
            else if (handler.CommandComparison == CommandComparison.Contains)
            {
                if(handler is StringCommandHandler stringHandler && currentCommand is string cmd && cmd.Contains(command as string, stringHandler.StringComparison))
                    return true;
            }
            else
            {
                throw new NotImplementedException();
            }
            return false;
        }

        private async Task<ResultCommand> ExecuteMethod(Update update, CommandHandler handler)
        {
            var method = handler.Command.Method;
            var privilages = method.GetCustomAttribute<AccessAttribute>();
            var requireDate = method.GetCustomAttribute<RequireTypeMessageAttribute>();
            var requireUpdate = method.GetCustomAttribute<RequiredTypeChatAttribute>();
            var @delegate = handler.Command;

            if (requireUpdate != null)
            {
                if (!requireUpdate.TypesChat.Contains(update!.Message!.Chat.Type))
                {
                    OnWrongTypeChat?.Invoke(bot.botClient, update);
                    return ResultCommand.WrongChatType;
                }
            }

            if (requireDate != null)
            {
                if (!requireDate.TypeMessages.Contains(update!.Message!.Type))
                {
                    OnWrongTypeMessage?.Invoke(bot.botClient, update);
                    return ResultCommand.WrongMessageType;
                }
            }

            if (privilages != null)
            {

                OnCheckPrivilege?.Invoke(bot.botClient, update, @delegate, privilages.Mask);
                return ResultCommand.PrivilegeCheck;
            }

            await @delegate(bot.botClient, update);
            return ResultCommand.Executed;
        }

        private Dictionary<string, CommandHandler> GetAllReplyCommands()
        {
            var dict = replyCommands.ToDictionary(entry => entry.Key, entry => entry.Value);
            return dict.Union(replyDynamicCommands).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="serviceProvider">Сервис провайдер.</param>
        public Router(PRBot botClient, IServiceProvider serviceProvider)
        {
            bot = botClient;
            _serviceProvider = serviceProvider;

            RegisterCommands();
        }

        #endregion
    }
}
