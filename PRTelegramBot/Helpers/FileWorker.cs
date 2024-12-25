using System.Reflection;
using Telegram.Bot;

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
        /// Скачивание файлов с telegram серверов
        /// </summary>
        /// <param name="botClient">Telegram клиент</param>
        /// <param name="telegramId">Идентификатор пользователя</param>
        /// <param name="fileId">Идентификатор файла</param>
        /// <param name="fileName">Название файла</param>
        /// <returns>Путь до файла</returns>
        public static async Task<string> DownloadFileFromTelegram(ITelegramBotClient botClient, long telegramId, string fileId, string fileName)
        {
            string folder = $"/Uploads/Users/{telegramId}/";
            string fullPath = BaseDir + folder + "/" +fileName;
            string dbpath = folder + "/" +fileName;
            if (!Directory.Exists(BaseDir + folder))
            {
                Directory.CreateDirectory(BaseDir + folder);
            }
            await using Stream fileStream = System.IO.File.OpenWrite(fullPath);
            var file = await botClient.GetInfoAndDownloadFile(
                fileId: fileId,
                destination: fileStream);
            return dbpath;
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
    }
}
