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
using PRTelegramBot.Models;
using PRTelegramBot.Extensions;
using PRTelegramBot.Commands;
using PRTelegramBot.Configs;

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
        delegate Task MessageCommand(ITelegramBotClient botclient, Update update);

        /// <summary>
        /// 
        /// </summary>
        private ITelegramBotClient _botClient;

        /// <summary>
        /// Словарь слеш команд
        /// </summary>
        private Dictionary<string, MessageCommand> slashCommands;

        /// <summary>
        /// Словарь reply команд
        /// </summary>
        private Dictionary<string, MessageCommand> messageCommands;

        /// <summary>
        /// Словарь приоритетных reply команд
        /// </summary>
        private Dictionary<string, MessageCommand> messageCommandsPriority;

        /// <summary>
        /// Словарь Inline команд
        /// </summary>
        private Dictionary<CallbackId, MessageCommand> inlineCommands;


        public Router(ITelegramBotClient botClient)
        {
            _botClient                  = botClient;
            messageCommands             = new Dictionary<string, MessageCommand>();
            messageCommandsPriority     = new Dictionary<string, MessageCommand>();
            inlineCommands              = new Dictionary<CallbackId, MessageCommand>();
            slashCommands               = new Dictionary<string, MessageCommand>();

            RegisterCommnad();
        }



        /// <summary>
        /// Автоматическая регистрация доступных команд для выполнения через рефлексию
        /// </summary>
        public void RegisterCommnad()
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
                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(MessageCommand), method, false);
                        messageCommands.Add(command, (MessageCommand)serverMessageHandler);
                        if (priority)
                        {
                            messageCommandsPriority.Add(command, (MessageCommand)serverMessageHandler);
                        }
                    }
                }

                //Регистрируем Inline команды
                foreach (var method in inlineMethods)
                {
                    foreach (var command in method.GetCustomAttribute<InlineCallbackHandlerAttribute>().Commands)
                    {
                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(MessageCommand), method, false);
                        inlineCommands.Add(command, (MessageCommand)serverMessageHandler);
                    }
                }

                //Регистрируем слеш команды
                foreach (var method in slashCommandMethods)
                {
                    foreach (var command in method.GetCustomAttribute<SlashHandlerAttribute>().Commands)
                    {
                        Delegate serverMessageHandler = Delegate.CreateDelegate(typeof(MessageCommand), method, false);
                        slashCommands.Add(command, (MessageCommand)serverMessageHandler);
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
        public async Task<bool> IsSlashCommand(string command, Update update)
        {
            try
            {
                if (!command.Contains("/"))
                    return false;

                foreach (var commandExecute in slashCommands)
                {
                    if (command.ToLower().Contains(commandExecute.Key.ToLower()))
                    {
                        var requireUpdate = commandExecute.Value.Method.GetCustomAttribute<RequiredTypeUpdateAttribute>();
                        if (requireUpdate != null)
                        {
                            if (!requireUpdate.TypeUpdate.Contains(update.Message.Chat.Type))
                            {
                                await AccessCommand.PrivilagesMissing(_botClient, update);
                                return true;
                            }
                        }
                        var privilages = commandExecute.Value.Method.GetCustomAttribute<AccessAttribute>();
                        if (privilages != null && privilages.RequiredPrivilege != null)
                        {
                            //TODO: Проверка привилегий есть требуется
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
        public async Task ExecuteCommandByMessage(string command, Update update)
        {
            try
            {
                if (command.Contains("(") && command.Contains(")"))
                {
                    command = command.Remove(command.LastIndexOf("(") - 1);
                }

                if (await StartHasDeepLink(command, update))
                    return;

                if (await IsSlashCommand(command, update))
                    return;

                if (await IsHaveNextStep(command, update))
                    return;

                foreach (var commandExecute in messageCommands)
                {

                    if (command.ToLower() == commandExecute.Key.ToLower())
                    {
                        var privilages = commandExecute.Value.Method.GetCustomAttribute<AccessAttribute>();
                        var requireDate = commandExecute.Value.Method.GetCustomAttribute<RequireDateAttribute>();
                        var requireUpdate = commandExecute.Value.Method.GetCustomAttribute<RequiredTypeUpdateAttribute>();
                        if (requireUpdate != null)
                        {
                            if (!requireUpdate.TypeUpdate.Contains(update.Message.Chat.Type))
                            {
                                await AccessCommand.PrivilagesMissing(_botClient, update);
                                return;
                            }
                        }
                        if (privilages != null && privilages.RequiredPrivilege != null)
                        {
                            if (requireDate != null)
                            {

                            }
                            else
                            {

                            }
                            //TODO: Проверка привилегий есть требуется
                        }
                        else
                        {
                            if (requireDate != null)
                            {
                                if (requireDate.TypeData == update.Message.Type)
                                {
                                    await commandExecute.Value(_botClient, update);
                                }
                                else
                                {
                                    await AccessCommand.IncorrectTypeData(_botClient, update);
                                }
                            }
                            else
                            {
                                await commandExecute.Value(_botClient, update);
                            }

                        }

                        return;
                    }
                }

                await AccessCommand.CommandMissing(_botClient, update);
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
        public async Task ExecuteCommandByCallBack(Update update)
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
                            var requireUpdate = commandCallback.Value.Method.GetCustomAttribute<RequiredTypeUpdateAttribute>();
                            if (requireUpdate != null)
                            {
                                if (!requireUpdate.TypeUpdate.Contains(update.CallbackQuery.Message.Chat.Type))
                                {
                                    await AccessCommand.PrivilagesMissing(_botClient, update);
                                    return;
                                }
                            }
                            var privilages = commandCallback.Value.Method.GetCustomAttribute<AccessAttribute>();
                            if (privilages != null && privilages.RequiredPrivilege != null)
                            {
                                //TODO: Проверка привилегий есть требуется
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
        public async Task<bool> IsHaveNextStep(string command, Update update)
        {
            try
            {
                if (update.HasStep())
                {
                    foreach (var commandExecute in messageCommandsPriority)
                    {
                        if (command.ToLower() == commandExecute.Key.ToLower())
                        {
                            var requireUpdatex = commandExecute.Value.Method.GetCustomAttribute<RequiredTypeUpdateAttribute>();
                            if (requireUpdatex != null)
                            {
                                if (!requireUpdatex.TypeUpdate.Contains(update.Message.Chat.Type))
                                {
                                    await AccessCommand.PrivilagesMissing(_botClient, update);
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
                    var requireDate = cmd.Method.GetCustomAttribute<RequireDateAttribute>();
                    var requireUpdate = cmd.Method.GetCustomAttribute<RequiredTypeUpdateAttribute>();
                    if (requireUpdate != null)
                    {
                        if (!requireUpdate.TypeUpdate.Contains(update.Message.Chat.Type))
                        {
                            await AccessCommand.PrivilagesMissing(_botClient, update);
                            return true;
                        }
                    }
                    if (privilages != null && privilages.RequiredPrivilege != null)
                    {
                        if (requireDate != null)
                        {
                            if (requireDate.TypeData == update.Message.Type)
                            {

                            }
                            else
                            {

                            }
                        }
                        //TODO: Проверка привилегий есть требуется
                    }
                    else
                    {
                        if (requireDate != null)
                        {
                            if (requireDate.TypeData == update.Message.Type)
                            {
                                await cmd(_botClient, update);
                            }
                            else
                            {
                                await AccessCommand.IncorrectTypeData(_botClient, update);
                            }
                        }
                        else
                        {
                            await cmd(_botClient, update);
                        }

                    }
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
        public async Task<bool> StartHasDeepLink(string command, Update update)
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
                            await StartCommand.StartWithArguments(_botClient, update, spl[1]);
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
    }
}
