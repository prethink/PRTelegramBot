namespace PRTelegramBot.Utils
{
    public static class MessageUtils
    {
        /// <summary>
        /// Разбивает большое сообщение на блоки.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="chunkSize">Размер блока.</param>
        /// <returns>Коллекция сообщений.</returns>
        public static IList<string> SplitIntoChunks(string text, int chunkSize)
        {
            List<string> chunks = new List<string>();
            int offset = 0;
            while (offset < text.Length)
            {
                int size = Math.Min(chunkSize, text.Length - offset);
                chunks.Add(text.Substring(offset, size));
                offset += size;
            }
            return chunks;
        }
    }
}
