using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using PRTelegramBot.Extensions;
using PRTelegramBot.Configs;
using static PRTelegramBot.Core.Router;
using Telegram.Bot.Polling;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core
{
    public class Handler : IUpdateHandler
    {
        /// <summary>
        /// Клиент телеграм бота
        /// </summary>
        private PRBot telegram;

        /// <summary>
        /// Событие вызывается до обработки update, может быть прекращено выполнение 
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task<ResultUpdate>>? OnPreUpdate;

        /// <summary>
        /// Событие вызывается после обработки update типа Message и CallbackQuery
        /// </summary>
        public event Func<ITelegramBotClient, Update, Task>? OnPostMessageUpdate;

        /// <summary>
        /// Маршрутизатор
        /// </summary>
        public Router Router { get; private set; }
        public TelegramConfig Config { get; init; }

        public Handler(PRBot botClient, TelegramConfig config, IServiceProvider serviceProvider)
        {
            telegram = botClient;
            Config = config;
            Router = new Router(telegram, Config, serviceProvider);
        }

        /// <summary>
        /// Обработчик обновлений
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота</param>
        /// <param name="update">Обновление телеграм</param>
        /// <param name="cancellationToken">Токен</param>
        /// <returns></returns>
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (OnPreUpdate != null)
                {
                    var resultUpdate = await OnPreUpdate?.Invoke(botClient, update);

                    if (resultUpdate == ResultUpdate.Stop) return;
                }

                if (Config.WhiteListUsers.Count > 0)
                {
                    if (!Config.WhiteListUsers.Contains(update.GetChatId()))
                    {
                        await Router.OnAccessDeniedInvoke(telegram.botClient, update);
                        return;
                    }
                }

                if (update.Type == UpdateType.Message)
                {
                    await HandleMessage(update);
                    return;
                }

                if (update.Type == UpdateType.CallbackQuery)
                {
                    await HandleCallbackQuery(update, cancellationToken);
                    return;
                }
                await (OnPostMessageUpdate?.Invoke(botClient, update) ?? Task.CompletedTask);
            }
            catch (Exception ex)
            {
                telegram.InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Обработчик inline callback
        /// </summary>
        /// <param name="update">Обновление телеграма</param>
        /// <param name="cancellationToken">Токен</param>
        /// <returns></returns>
        private async Task HandleCallbackQuery(Update update, CancellationToken cancellationToken)
        {
            try
            {
                Router.ExecuteCommandByCallBack(update);
            }
            catch (Exception ex)
            {
                telegram.InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// Обработчик ошибок
        /// </summary>
        /// <param name="botClient">Телеграм бот</param>
        /// <param name="exception">Ошибка</param>
        /// <param name="cancellationToken">Токен</param>
        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            try
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };
                //TODO Logging exception
                //return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                telegram.InvokeErrorLog(ex);
            }

        }

        /// <summary>
        /// Обработчик текстовый сообщений Reply
        /// </summary>
        /// <param name="update">Обновление телеграма</param>
        async Task HandleMessage(Update update)
        {
            try
            {
                string command = update.Message.Text ?? update.Message.Type.ToString();
                if (update.Message.Type == MessageType.Text)
                {
                    telegram.InvokeCommonLog($"The user {update.GetInfoUser().Trim()} sent {command}");
                }
                else
                {
                    telegram.InvokeCommonLog($"The user {update.GetInfoUser().Trim()} sent the command {command}");
                }

                Router.ExecuteCommandByMessage(command, update);
            }
            catch (Exception ex)
            {
                telegram.InvokeErrorLog(ex);
            }
        }
    }


}
