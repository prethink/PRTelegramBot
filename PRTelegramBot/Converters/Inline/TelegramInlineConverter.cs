using PRTelegramBot.Core;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Core.CommandHandlers;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Models.InlineButtons;
using System.Text;

namespace PRTelegramBot.Converters.Inline
{
    internal class TelegramInlineConverter : IInlineMenuConverter
    {
        #region IInlineConverter

        /// <inheritdoc />
        public InlineCallback GetCommandByCallbackOrNull(string data)
        {
            try
            {
                return CurrentScope.Bot.GetSerializer().Deserialize<InlineCallback>(data);
            }
            catch(Exception ex)
            {
                CurrentScope.Context.Current.GetLogger<TelegramInlineConverter>().LogErrorInternal(ex);
                return null;
            }
        }

        /// <inheritdoc />
        public InlineCallback<T> GetCommandByCallbackOrNull<T>(string data)
            where T : TCommandBase
        {
            try
            {
                return CurrentScope.Bot.GetSerializer().Deserialize<InlineCallback<T>>(data);
            }
            catch (Exception ex)
            {
                CurrentScope.Context.Current.GetLogger<TelegramInlineConverter>().LogErrorInternal(ex);
                return null;
            }
        }

        /// <inheritdoc />
        public string GenerateCallbackData(InlineCallback inlineCallback)
        {
            try
            {
                var result = CurrentScope.Bot.GetSerializer().Serialize(inlineCallback);
                ThrowExceptionIfBytesMore64(result);
                return result;
            }
            catch (Exception ex)
            {
                CurrentScope.Context.Current.GetLogger<TelegramInlineConverter>().LogErrorInternal(ex);
                return string.Empty;
            }
        }

        /// <inheritdoc />
        public string GenerateCallbackData<T>(InlineCallback<T> inlineCallback)
            where T : TCommandBase
        {
            try
            {
                var result = CurrentScope.Bot.GetSerializer().Serialize<InlineCallback<T>>(inlineCallback);
                ThrowExceptionIfBytesMore64(result);
                return result;
            }
            catch (Exception ex)
            {
                CurrentScope.Context.Current.GetLogger<TelegramInlineConverter>().LogErrorInternal(ex);
                return string.Empty;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Выбросить исключение если результат больше чем 128 байт.
        /// </summary>
        /// <param name="result">Результат.</param>
        /// <exception cref="Exception">Исключение.</exception>
        protected void ThrowExceptionIfBytesMore64(string result)
        {
            var byteSize = Encoding.UTF8.GetBytes(result);
            if (byteSize.Length > PRConstants.MAX_SIZE_CALLBACK_DATA)
                throw new Exception($"Callback_data limit exceeded {byteSize} > {PRConstants.MAX_SIZE_CALLBACK_DATA}. Try reducing the amount of data in the command.");
        }

        #endregion
    }
}
