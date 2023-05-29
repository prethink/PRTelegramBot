using System.Reflection;
using Telegram.Bot;
using PRTelegramBot.Core;

namespace PRTelegramBot.Helpers
{
    /// <summary>
    /// Хелпер для работы с файлами
    /// </summary>
    public static class FileWorker
    {
        /// <summary>
        /// Базовая директория
        /// </summary>
        public static string BaseDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Скачивание файлов с телеграм серверов
        /// </summary>
        /// <param name="botClient">Телеграм клиент</param>
        /// <param name="telegramId">Идентификатор пользователя</param>
        /// <param name="fileId">Идентификатор файла</param>
        /// <param name="fileName">Название файла</param>
        /// <returns>Путь до файла</returns>
        public static async Task<string> DownloadFileFromTelegram(ITelegramBotClient botClient, long telegramId, string fileId, string fileName)
        {
            try
            {
                string folder = $"/Uploads/Users/{telegramId}/";
                string fullPath = BaseDir + folder + "/" +fileName;
                string dbpath = folder + "/" +fileName;
                if (!Directory.Exists(BaseDir + folder))
                {
                    Directory.CreateDirectory(BaseDir + folder);
                }
                await using Stream fileStream = System.IO.File.OpenWrite(fullPath);
                var file = await botClient.GetInfoAndDownloadFileAsync(
                    fileId: fileId,
                    destination: fileStream);
                return dbpath;
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
                return "";
            }
        }

        /// <summary>
        /// Сохранение файлов в папку пользователя
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя</param>
        /// <param name="stream">Потом</param>
        /// <param name="fileName">Название файла</param>
        /// <returns></returns>
        public static string SaveFileToUser(long telegramId,MemoryStream stream, string fileName)
        {
            try
            {
                string folder = $"/Uploads/Users/{telegramId}/";
                string fullPath = BaseDir + folder + "/" + fileName;
                string dbpath = folder + "/" + fileName;
                if (!Directory.Exists(BaseDir + folder))
                {
                    Directory.CreateDirectory(BaseDir + folder);
                }
                File.WriteAllBytes(fullPath, stream.ToArray());
                return fullPath;
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
                return "";
            }
        }

    }
}
