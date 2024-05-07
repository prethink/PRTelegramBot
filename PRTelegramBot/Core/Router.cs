using PRTelegramBot.Attributes;
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
    /// Маршрутизация команд
    /// </summary>
    public class Router : IExecuteCommand
    {
        #region Events

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

        private const string PropertyCommandsName = "Commands";

        /// <summary>
        /// Клиент для telegram бота
        /// </summary>
        private PRBot bot;

        /// <summary>
        /// Словарь слеш команд
        /// </summary>
        private Dictionary<string, TelegramHandler> slashCommands { get; set; } = new();

        /// <summary>
        /// Словарь reply команд
        /// </summary>
        private Dictionary<string, TelegramHandler> replyCommands { get; set; } = new();

        /// <summary>
        /// Словарь Inline команд
        /// </summary>
        private Dictionary<Enum, TelegramHandler> inlineCommands { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Telegram.Bot.Types.Enums.MessageType, Func<ITelegramBotClient,Update,Task>> TypeMessage { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private IServiceProvider _serviceProvider;

        public Router(PRBot botClient, IServiceProvider serviceProvider)
        {
            bot            = botClient;
            _serviceProvider    = serviceProvider;

            RegisterCommands();
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
                foreach (var method in serviceType.GetMethods().Where(x => !x.IsStatic))
                {
                    try
                    {
                        var attribute = method.GetCustomAttribute(typeof(IBotIdentifier)) as IBotIdentifier;

                        if (attribute == null || attribute.BotId != bot.Options.BotId && attribute.BotId != -1)
                            continue;

                        bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(method);
                        if (!isValidMethod)
                        {
                            bot.InvokeErrorLog(new Exception($"The method {method.Name} has an invalid signature. " +
                                $"Required return {nameof(Task)} arg1 {nameof(ITelegramBotClient)} arg2 {nameof(Update)}"));
                            continue;
                        }

                        var telegramhandler = new TelegramHandler(method, _serviceProvider);

                        if (attribute is ReplyMenuHandlerAttribute replyattr)
                        {
                            foreach (var command in replyattr.Commands)
                                replyCommands.Add(command, telegramhandler);

                            continue;
                        }

                        if (attribute is ReplyMenuDynamicHandlerAttribute replyDictionary)
                        {
                            foreach (var command in replyDictionary.Commands)
                                replyCommands.Add(command, telegramhandler);

                            continue;
                        }

                        if (attribute is SlashHandlerAttribute slashAttribute)
                        {
                            foreach (var command in slashAttribute.Commands)
                                slashCommands.Add(command, telegramhandler);

                            continue;

                        }

                        if (attribute.GetType().IsGenericType && attribute.GetType()?.GetGenericTypeDefinition() == typeof(InlineCallbackHandlerAttribute<>))
                        {
                            var commandsProperty = attribute.GetType().GetProperty(PropertyCommandsName);
                            var commands = (IEnumerable<Enum>)commandsProperty.GetValue(attribute);

                            foreach (var command in commands)
                                inlineCommands.Add(command, telegramhandler);

                        }
                    }
                    catch(Exception ex)
                    {
                        bot.InvokeErrorLog(ex);
                    }
                }
            }
        }

        private void RegisterStaticCommands()
        {
            //Находим все методы которые используют наши атрибуты
            MethodInfo[] messageMethods             = ReflectionUtils.FindStaticMessageMenuHandlers(bot.Options.BotId);
            MethodInfo[] messageDictionaryMethods   = ReflectionUtils.FindStaticMessageMenuDictionaryHandlers(bot.Options.BotId);
            MethodInfo[] inlineMethods              = ReflectionUtils.FindStaticInlineMenuHandlers(bot.Options.BotId);
            MethodInfo[] slashCommandMethods        = ReflectionUtils.FindStaticSlashCommandHandlers(bot.Options.BotId);

            RegisterCommand<ReplyMenuHandlerAttribute, string>(messageMethods, replyCommands);
            RegisterCommand<ReplyMenuDynamicHandlerAttribute, string>(messageDictionaryMethods, replyCommands);
            RegisterCommand<SlashHandlerAttribute, string>(slashCommandMethods, slashCommands);
            RegisterCommand<InlineCallbackHandlerAttribute<Enum>, Enum>(inlineMethods, inlineCommands);
        }

        private void RegisterCommand<TAttribute, T>(MethodInfo[] methods, Dictionary<T, TelegramHandler> collectionCommands) where TAttribute : BaseQueryAttribute<T>
        {
            foreach (var method in methods)
            {
                try
                {
                    if (!method.IsStatic)
                        continue;

                    foreach (var command in method.GetCustomAttribute<BaseQueryAttribute<T>>().Commands)
                    {
                        bool isValidMethod = ReflectionUtils.IsValidMethodForBaseBaseQueryAttribute(method);
                        if (!isValidMethod)
                        {
                            bot.InvokeErrorLog(new Exception($"The method {method.Name} has an invalid signature for the {typeof(TAttribute)} attribute. The method will be ignored."));
                            continue;
                        }

                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(Func<ITelegramBotClient, Update, Task>), method, false);
                        var telegramCommand = new TelegramHandler((Func<ITelegramBotClient, Update, Task>)serverMessageHandler);
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
        /// Добавляет Reply команду
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <param name="delegate">Метод обработки</param>
        /// <returns>True - команда добавлена / False - ошибка при добавление или команда не добавлена</returns>
        public bool RegisterReplyCommand(string command, Func<ITelegramBotClient,Update,Task> @delegate)
        {
            try
            {
                replyCommands.Add(command, new TelegramHandler(@delegate));
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
                slashCommands.Add(command, new TelegramHandler(@delegate));
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
                inlineCommands.Add(@enum, new TelegramHandler(@delegate));
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

                var resultExecute = await ExecuteCommand(command, update, replyCommands); 

                if(resultExecute == ResultCommand.NotFound)
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
                    bot.InvokeCommonLog(msg, BaseEventTelegram.CallBackCommand, ConsoleColor.Magenta);

                    var resultExecute = await ExecuteCommand(command.CommandType, update, inlineCommands);
                }
            }
            catch (Exception ex)
            {
                bot.InvokeErrorLog(ex);
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

                var resultPriorityCommandExecute = await ExecuteCommand(command, update, replyCommands);

                if (resultPriorityCommandExecute != ResultCommand.NotFound)
                {
                    update.ClearStepUserHandler();
                    return true;
                }

                var step = update.GetStepHandler().GetExecuteMethod();
                var result = await ExecuteMethod(update, new TelegramHandler(step));
                return result == ResultCommand.Executed;
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

                var resultExecute = await ExecuteCommand(command, update, slashCommands, TypeCheckCommand.Contains);
                return resultExecute != ResultCommand.NotFound;

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
                if (string.IsNullOrEmpty(spl[1]))
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

        private async Task<ResultCommand> ExecuteCommand(string command, Update update, Dictionary<string, TelegramHandler> commandList, TypeCheckCommand typeCheck = TypeCheckCommand.Equals)
        {

            foreach (var commandExecute in commandList)
            {
                if (typeCheck == TypeCheckCommand.Equals)
                {
                    if (!command.Equals(commandExecute.Key, StringComparison.OrdinalIgnoreCase))
                        continue;
                }
                else if (typeCheck == TypeCheckCommand.Contains)
                {
                    if (command is string cmd && !cmd.Contains(commandExecute.Key,StringComparison.OrdinalIgnoreCase))
                        continue;
                }
                else
                {
                    throw new NotImplementedException();
                }

                var result = await ExecuteMethod(update, commandExecute.Value);
                return result;
            }
            return ResultCommand.NotFound;
        }

        private async Task<ResultCommand> ExecuteCommand(Enum command, Update update, Dictionary<Enum, TelegramHandler> commandList)
        {

            foreach (var commandExecute in commandList)
            {
                if (!command.Equals(commandExecute.Key))
                    continue;

                var result = await ExecuteMethod(update, commandExecute.Value);
                return result;
            }
            return ResultCommand.NotFound;
        }

        private async Task<ResultCommand> ExecuteMethod( Update update, TelegramHandler command)
        {
            var method = command.Command.Method ;
            var privilages = method.GetCustomAttribute<AccessAttribute>();
            var requireDate = method.GetCustomAttribute<RequireTypeMessageAttribute>();
            var requireUpdate = method.GetCustomAttribute<RequiredTypeChatAttribute>();
            var @delegate = command.Command;

            if (requireUpdate != null)
            {
                if (!requireUpdate.TypeUpdate.Contains(update!.Message!.Chat.Type))
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
        internal async Task OnAccessDeniedInvoke(ITelegramBotClient botClient, Update update)
        {
            OnAccessDenied?.Invoke(botClient, update);
        }
    }
}
