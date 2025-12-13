using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using System.Collections.Concurrent;

namespace PRTelegramBot.Converters.Inline
{
    /// <summary>
    /// Конвертер inline меню в файлы.
    /// Сохраняет временные данные в формате файлов "{Ид бота}-{Ид пользователя}-{Ид команды}".
    /// Файлы сохраняются в папке "InlineCallbacks" в директории приложения. При создание экзепляра можно указать другое название папки.
    /// </summary>
    public class FileInlineConverter : IInlineMenuConverter
    {
        /// <summary>
        /// Базовый путь для сохранения файлов.
        /// </summary>
        protected string basePath;

        /// <summary>
        /// Локи против raceCondition.
        /// </summary>
        private static readonly ConcurrentDictionary<string, object> fileLocks = new();

        #region IInlineMenuConverter

        /// <inheritdoc />
        public string GenerateCallbackData(InlineCallback inlineCallback)
        {
            BaseConvert(inlineCallback.CommandType, () => CurrentScope.Bot.GetSerializer().Serialize(inlineCallback));
            return GetUserKey(inlineCallback.CommandType);
        }

        /// <inheritdoc />
        public string GenerateCallbackData<T>(InlineCallback<T> inlineCallback) where T : TCommandBase
        {
            BaseConvert(inlineCallback.CommandType, () => CurrentScope.Bot.GetSerializer().Serialize<InlineCallback<T>>(inlineCallback));
            return GetUserKey(inlineCallback.CommandType);
        }

        /// <inheritdoc />
        public InlineCallback? GetCommandByCallbackOrNull(string data)
        {
            var file = $"{basePath}/{data}.json";

            if (!File.Exists(file))
                return null;

            var result = File.ReadAllText(file);

            return CurrentScope.Bot.GetSerializer().Deserialize<InlineCallback>(result);
        }

        /// <inheritdoc />
        public InlineCallback<T>? GetCommandByCallbackOrNull<T>(string data) where T : TCommandBase
        {
            var file = $"{basePath}/{data}.json";

            if (!File.Exists(file))
                return null;

            var result = File.ReadAllText(file);

            return CurrentScope.Bot.GetSerializer().Deserialize<InlineCallback<T>>(result);
        }

        #endregion

        private string GetUserKey(Enum command)
        {
            return CurrentScope.Context.Update.GetInlineKey(command);
        }

        private string BaseConvert(Enum command, Func<string> convert)
        {
            try
            {
                var userKey = GetUserKey(command);
                var filePath = Path.Combine(basePath, $"{userKey}.json");
                var data = convert();

                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                File.WriteAllText(filePath, data);
                return data;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private string GetAppPath()
        {
            return AppContext.BaseDirectory;
        }

        public FileInlineConverter(string path)
        {
            basePath = Path.Combine(GetAppPath(), "path");
        }

        public FileInlineConverter()
        {
            basePath = Path.Combine(GetAppPath(), "InlineCallbacks");
        }
    }
}
