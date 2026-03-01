using System.Text.RegularExpressions;

namespace PRTelegramBot.Builders
{
    /// <summary>
    /// Строитель сообщений с поддержкой именованных токенов и позиционных аргументов.
    /// Позволяет формировать строки наподобие <see cref="string.Format"/>, 
    /// но с расширением для токенов вида {QA}, {Dev} и т.д.
    /// </summary>
    public class MessageBuilder
    {
        #region Поля и свойства

        /// <summary>
        /// Шаблона сообщения.
        /// </summary>
        private string template;

        /// <summary>
        /// Словарь резолверов для именованных токенов.
        /// Ключ — имя токена, значение — функция, возвращающая строку.
        /// </summary>
        private readonly Dictionary<string, Func<string>> resolvers = new();

        /// <summary>
        /// Список позиционных аргументов для подстановки в {0}, {1} и т.д.
        /// </summary>
        private readonly List<object> args = new();

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет именованный токен с ленивым резолвером (Func&lt;string&gt;).
        /// </summary>
        /// <param name="key">Имя токена в шаблоне, например "QA".</param>
        /// <param name="resolver">Функция, возвращающая значение токена при вызове Build().</param>
        /// <returns>Текущий экземпляр <see cref="MessageBuilder"/> для fluent API.</returns>
        public MessageBuilder AddResolver(string key, Func<string> resolver)
        {
            resolvers[key] = resolver;
            return this;
        }

        /// <summary>
        /// Добавляет именованный токен со статическим значением.
        /// </summary>
        /// <param name="key">Имя токена в шаблоне.</param>
        /// <param name="value">Строковое значение токена.</param>
        /// <returns>Текущий экземпляр <see cref="MessageBuilder"/> для fluent API.</returns>
        public MessageBuilder AddResolver(string key, string value)
        {
            resolvers[key] = () => value;
            return this;
        }

        /// <summary>
        /// Добавляет один позиционный аргумент для подстановки в {0}, {1} и т.д.
        /// </summary>
        /// <param name="arg">Аргумент для подстановки.</param>
        /// <returns>Текущий экземпляр <see cref="MessageBuilder"/> для fluent API.</returns>
        public MessageBuilder AddArgument(object arg)
        {
            args.Add(arg);
            return this;
        }

        /// <summary>
        /// Добавляет несколько позиционных аргументов сразу.
        /// </summary>
        /// <param name="arguments">Массив аргументов для подстановки.</param>
        /// <returns>Текущий экземпляр <see cref="MessageBuilder"/> для fluent API.</returns>
        public MessageBuilder AddArguments(params object[] arguments)
        {
            args.AddRange(arguments);
            return this;
        }

        /// <summary>
        /// Генерирует итоговую строку, подставляя позиционные аргументы и значения именованных токенов.
        /// Не найденные токены остаются в виде {TokenName}.
        /// </summary>
        /// <returns>Сформированная строка с подставленными значениями.</returns>
        public string Build()
        {
            return Regex.Replace(template, @"\{(.*?)\}", match =>
            {
                var key = match.Groups[1].Value;

                // Проверка на позиционный аргумент
                if (int.TryParse(key, out var index))
                {
                    if (index < args.Count)
                        return args[index]?.ToString();

                    // Если индекс отсутствует, возвращаем оригинальный токен
                    return match.Value;
                }

                // Проверка именованного токена
                if (resolvers.TryGetValue(key, out var resolver))
                    return resolver()?.ToString();

                // Если токен не найден, возвращаем как есть
                return match.Value;
            });
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Инициализирует новый билдера сообщений с заданным шаблоном.
        /// </summary>
        /// <param name="template">Строка-шаблон с токенами и позиционными аргументами.</param>
        public MessageBuilder(string template)
        {
            this.template = template;
        }

        #endregion
    }
}
