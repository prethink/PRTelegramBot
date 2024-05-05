using PRTelegramBot.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;

namespace PRTelegramBot.Core
{
    public class PRBotBuilder
    {
        #region Поля и свойства

        private TelegramOptions options;
        private ReceiverOptions recevierOptions;
        private IServiceProvider serviceProvider;
        private CancellationTokenSource cancellationToken;

        #endregion

        #region Методы

        /// <summary>
        /// Сбилдить новый экземпляр класса PRBot.
        /// </summary>
        /// <returns>Экземпляр класса PRBot.</returns>
        public PRBot Build()
        {
            return new PRBot(options, recevierOptions, cancellationToken, serviceProvider);
        }

        /// <summary>
        /// Установить токен в билдере.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetToken(string token)
        {
            options.Token = token;
            return this;
        }

        /// <summary>
        /// Установить идентификатор бота.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetBotId(long botId)
        {
            options.BotId = botId;
            return this;
        }

        /// <summary>
        /// Сбрасывать все обновление при запуске бота.
        /// </summary>
        /// <param name="flag">True - да, False - нет.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder SetClearUpdatesOnStart(bool flag)
        {
            options.ClearUpdatesOnStart = flag;
            return this;
        }

        /// <summary>
        /// Добавить динамическую команду.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddReplyDynamicCommand(string key, string value)
        {
            options.ReplyDynamicCommands.Add(key, value);
            return this;
        }

        /// <summary>
        /// Добавить динамические команды.
        /// </summary>
        /// <param name="dynamicCommands">Коллекция динамических команд.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddReplyDynamicCommands(Dictionary<string, string> dynamicCommands)
        {
            foreach (var command in dynamicCommands)
                dynamicCommands.Add(command.Key, command.Value);
            return this;
        }

        /// <summary>
        /// Добавить администратора бота.
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddAdmin(long telegramId)
        {
            options.Admins.Add(telegramId);
            return this;
        }

        /// <summary>
        /// Добавить администраторов бота.
        /// </summary>
        /// <param name="telegramIds">Коллекция идентификаторов пользователей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddAdmins(List<long> telegramIds)
        {
            options.Admins.AddRange(telegramIds);
            return this;
        }

        /// <summary>
        /// Добавить пользователя в белый список.
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUserWhiteList(long telegramId)
        {
            options.WhiteListUsers.Add(telegramId);
            return this;
        }

        /// <summary>
        /// Добавить пользователей в белый список.
        /// </summary>
        /// <param name="telegramIds">Коллекция идентификаторов пользователей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddUsersWhiteList(List<long> telegramIds)
        {
            options.WhiteListUsers.AddRange(telegramIds);
            return this;
        }

        /// <summary>
        /// Добавить путь до конфигурационного файла.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="path">Путь.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddConfigPath(string key, string path)
        {
            options.ConfigPaths.Add(key, path);
            return this;
        }

        /// <summary>
        /// Добавить путь до конфигурационных файлов.
        /// </summary>
        /// <param name="configPaths">Коллекция путей.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AdditionalConfigPaths(Dictionary<string, string> configPaths)
        {
            foreach (var configPath in configPaths)
                options.ConfigPaths.Add(configPath.Key, configPath.Value);
            return this;
        }

        /// <summary>
        /// Добавить сервис провайдер в бот.
        /// </summary>
        /// <param name="serviceProvider">Сервис провайдер для DI.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddServiceProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            return this;
        }

        /// <summary>
        /// Добавить параметры приемника.
        /// </summary>
        /// <param name="recevierOptions">параметры приемника.</param>
        /// <returns>Builder.</returns>
        public PRBotBuilder AddRecevingOptions(ReceiverOptions recevierOptions)
        {
            this.recevierOptions = recevierOptions;
            return this;
        }

        #endregion

        #region Конструкторы

        public PRBotBuilder(string token)
        {
            options = new TelegramOptions();
            SetToken(token);
            AddRecevingOptions(new ReceiverOptions() { AllowedUpdates = { } });
        }

        #endregion
    }
}
